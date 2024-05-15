using HotelManagementSystem_Application.Interface.IRepository;
using HotelManagementSystem_Domain.Data;
using HotelManagementSystem_Domain.Data.ViewModels;
using HotelManagementSystem_Domain.Utility;
using HotelManagementSystem_Persistence.DBContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;
using System.Security.Claims;
using Newtonsoft.Json;
using HotelManagementSystem_Persistence.Migrations;
using NuGet.Protocol;

namespace HotelManagementSystemUser.Controllers
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Individual)]
    public class UserbookingController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserbookingController(ApplicationDbContext context, IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public RoomViewModel roomViewModel { get; set; } = new();

        public IActionResult Index(int hotelId)
        {
            roomViewModel.HotelDetails = GetHotelDetails(hotelId);
            ViewBag.HotelId = hotelId;
            var hotel = _unitOfWork.RoomTypeModelRepository.GetAll(h => h.HotelId != 0);
            roomViewModel.RoomListbyhotel = hotel.Where(x => x.HotelId == hotelId).ToList();

            if (roomViewModel.HotelDetails == null)
            {

                return RedirectToAction("HotelNotFound");
            }

            return View("Index", roomViewModel);
        }
        private HotelModel GetHotelDetails(int hotelId)
        {
            var hotel = _unitOfWork.HotelRepository.Get(hotelId);
            return hotel;
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(RoomViewModel roomViewModel, string stripeToken, int roomid)
        {
            try
            {
                HotelModel hotelModel = new HotelModel();
                BookingModel bookingModel = new BookingModel();
                RoomTypeModel roomTypeModel = new RoomTypeModel();
                RoomType roomType = new RoomType();
                checkindate checkindate = new checkindate();
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


                bookingModel.ApplicationUserId = userId;


                checkindate.ApplicationUserId = userId;

                bookingModel.CustomerName = Request.Form["Booking.CustomerName"].FirstOrDefault();
                bookingModel.CustomerAddress = Request.Form["Booking.CustomerAddress"].FirstOrDefault();
                bookingModel.CustomerPhoneNumber = Request.Form["Booking.CustomerPhoneNumber"].FirstOrDefault();
                bookingModel.CustomerEmail = Request.Form["Booking.CustomerEmail"].FirstOrDefault();
                bookingModel.CustomerPinCode = Request.Form["Booking.CustomerPinCode"].FirstOrDefault();


                string totalPaymentString = Request.Form["Booking.TotalPayment"].FirstOrDefault();

                if (!string.IsNullOrEmpty(totalPaymentString))
                {
                    bookingModel.TotalPayment = Convert.ToInt32(Convert.ToDouble(totalPaymentString));
                }

                string HotelName = Request.Form["HotelDetails.Name"].FirstOrDefault();
                string HotelPlace = Request.Form["HotelDetails.Place"].FirstOrDefault();
                string[] selectedItems = Request.Form["selectedItems"].ToArray();

                List<checkindate> checkindates = new List<checkindate>();


                if (selectedItems != null && selectedItems.Length > 0)
                {
                    string combinedIntegers = string.Join(",", selectedItems);
                 
                    //if (!IsRoomAvailable(Convert.ToInt32(selectedItems[0]), checkindate.CheckInDate, checkindate.CheckOutDate))
                    //{

                    //    TempData["RoomNotAvailableMessage"] = "Room is already booked for the provided dates.";
                    //    return RedirectToAction("Index");
                    //}


                    roomType.ApplicationUserID = userId;
                    roomType.RoomTypeId = combinedIntegers;

                    Random rand = new Random();
                    int randomRoomTypeId = rand.Next(1000);
                    bookingModel.RoomTypeId = Convert.ToInt32(selectedItems[0]);


                    _unitOfWork.BookingModelRepository.Add(bookingModel);

                    _unitOfWork.RoomRepository.Add(roomType);
                    _unitOfWork.savechanges();
                    if (stripeToken == null)
                    {
                        var stripeSecretKey = "sk_test_51Oixg1SAidHJjEGP3Dt2dwN3Q3O0RAxvRaECtnVc104ds1SKvPiEZU1SGggBGizROaaM4X3k3mGZzE3fCitkX1in00MhhRqUrg";
                        StripeConfiguration.ApiKey = stripeSecretKey;
                        var domain = "https://localhost:7155/Confirmation/index";

                        var amt = (long)(bookingModel.TotalPayment * 100);

                        var options = new SessionCreateOptions
                        {
                            PaymentMethodTypes = new List<string> { "card" },
                            LineItems = new List<SessionLineItemOptions>
                        {
                            new SessionLineItemOptions
                                {
                                 PriceData = new SessionLineItemPriceDataOptions
                                 {
                                      Currency = "USD",
                                      UnitAmount =amt,
                                      ProductData = new SessionLineItemPriceDataProductDataOptions
                                      {
                                         Name = $"Customer Name: {bookingModel.CustomerName}, Address: {bookingModel.CustomerAddress}, Phone Number: {bookingModel.CustomerPhoneNumber}," +
                                         $" Email: {bookingModel.CustomerEmail}, Pin Code: {bookingModel.CustomerPinCode},room:{roomType.RoomTypeId}"
                                      }
                                 },
                                 Quantity=1
                            },
                        },
                            Mode = "payment",
                            SuccessUrl = $"{domain}",

                        };
                        var service = new SessionService();
                        try
                        {
                            bookingModel.PaymentDate = DateTime.Now;
                            Session session = await service.CreateAsync(options);
                            string sessionId = session.Id;
                            bookingModel.TransationId = sessionId;
                            _unitOfWork.BookingModelRepository.Update(bookingModel);
                            _unitOfWork.savechanges();
                            return Redirect(session.Url);
                        }
                        catch (Exception x)
                        {

                            throw;
                        }
                    }
                }
                return RedirectToAction("Bookingconfirm");
            }
            catch (Exception)
            {
                throw;
            }
        }
      

        private bool IsRoomAvailable(int roomTypeId, DateTime checkInDate, DateTime checkOutDate)
        {
            var existingBookings = _unitOfWork.CheckRepository.GetAll()
          .Where(b => b.RoomTypeId == roomTypeId && !(b.CheckOutDate <= checkInDate || b.CheckInDate >= checkOutDate))
          .ToList();

            // Check if existingBookings is not null before accessing its Count property
            return existingBookings != null && existingBookings.Count == 0;
        }

        [HttpPost]
        public async Task<IActionResult> SaveSessionData([FromBody] List<SessionDataModel> sessionDataList)
        {
           

            List<checkindate> checkindates = new List<checkindate>();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            foreach (var sessionData in sessionDataList)
            {
                

                var checkin = new checkindate
                {
                    ApplicationUserId = userId,
                    RoomTypeId = Convert.ToInt32(sessionData.RoomId),
                    CheckInDate = Convert.ToDateTime(sessionData.CheckInDate),
                    CheckOutDate = Convert.ToDateTime(sessionData.CheckOutDate)
                };
             
                checkindates.Add(checkin);

                _unitOfWork.CheckRepository.Add(checkin);
            }

            _unitOfWork.savechanges();
            return Ok("Session data received successfully!");
        }

        [HttpPost]
        public IActionResult CheckAvailability([FromBody] sessionData sessionData)
        {
            DateTime fromDate = DateTime.Parse(sessionData.fromDate);
            DateTime toDate = DateTime.Parse(sessionData.toDate);

            var allBookings = _unitOfWork.CheckRepository.GetAll();
            var bookingsWithinRange = allBookings
                .Where(booking => booking.CheckInDate >= fromDate && booking.CheckInDate <= toDate)
                .ToList();

            var bookedDatesByRoom = bookingsWithinRange
                .GroupBy(booking => booking.RoomTypeId)
                .ToDictionary(group => group.Key, group => group.Select(booking => booking.CheckInDate).ToList());

            List<checkindate> roomAvailabilities = new List<checkindate>();
            foreach (var entry in bookedDatesByRoom)
            {
                roomAvailabilities.Add(new checkindate
                {
                    RoomTypeId = entry.Key,
                });
            }

            return Json(roomAvailabilities);
        }
















        private (DateTime? CheckInDate, DateTime? CheckOutDate) GetBookingDates(int roomTypeId, DateTime checkInDate, DateTime checkOutDate)
        {
            var existingBookings = _unitOfWork.CheckRepository.GetAll()
                .Where(b => b.RoomTypeId == roomTypeId && !(b.CheckOutDate <= checkInDate || b.CheckInDate >= checkOutDate))
                .ToList();

            if (existingBookings != null && existingBookings.Count > 0)
            {
              
                return (existingBookings[0].CheckInDate, existingBookings[0].CheckOutDate);
            }
            else
            {
                
                return (null, null);
            }
        }
        public class SessionDataModel
        {
            public string RoomId { get; set; }
            public string CheckInDate { get; set; }
            public string CheckOutDate { get; set; }
        }

    }

}


public class sessionData
{
    public string fromDate { get; set; }
    public string toDate { get; set; }
}


