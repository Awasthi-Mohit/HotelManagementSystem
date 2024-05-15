using HotelManagementSystem_Application.Interface.IRepository;
using HotelManagementSystem_Domain.Data;
using HotelManagementSystem_Domain.Data.ViewModels;
using HotelManagementSystem_Domain.Utility;
using HotelManagementSystem_Persistence.DBContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.UI.Services;
using Humanizer;


namespace HotelManagementSystemUser.Controllers
{
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Individual)]

    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly NavigationManager _NavigationManager;
        private readonly IEmailSender _emailSender;


        public BookingController(ApplicationDbContext context, IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, NavigationManager navigationManager, IEmailSender emailSender)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _NavigationManager = navigationManager;
            _emailSender = emailSender;
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

                // Assign the user ID to the booking model

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
       
                if (selectedItems?.Length > 0)
                {
                    Array.Resize(ref selectedItems, selectedItems.Length + 1);
                    selectedItems[selectedItems.Length - 1] = "newItem";
                }
                if (selectedItems != null && selectedItems.Length > 0)
                {
                    string combinedIntegers = string.Join(",", selectedItems);

                    roomType.ApplicationUserID = userId;
                    roomType.RoomTypeId = combinedIntegers;

                    Random rand = new Random();
                    int randomRoomTypeId = rand.Next(1000);
                    bookingModel.RoomTypeId = Convert.ToInt32(selectedItems[0]);
                    checkindate.RoomTypeId = Convert.ToInt32(selectedItems[0]);


                    _unitOfWork.BookingModelRepository.Add(bookingModel);

                    _unitOfWork.RoomRepository.Add(roomType);
                    _unitOfWork.CheckRepository.Add(checkindate);
                    _unitOfWork.savechanges();
                    //await SendConfirmationEmail(bookingModel);
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
            catch (Exception x)
            {

                throw;
            }
        }
        private bool IsRoomAvailable(int roomTypeId, DateTime checkInDate, DateTime checkOutDate)
        {
            var existingBookings = _unitOfWork.BookingModelRepository.GetAll()
                .Where(b => b.RoomTypeId == roomTypeId &&
                            !(b.CheckOutDate <= checkInDate || b.CheckInDate >= checkOutDate))
                .ToList();
            return existingBookings.Count == 0;
        }

        public IActionResult Bookingconfirm()
        {
            return View();
        }

        [Authorize(Roles = SD.Role_Individual)]
        public IActionResult details()
        {
            var identity = User.Identity as ClaimsIdentity;
            var userId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var username = identity.Name;
            var email = identity.FindFirst(ClaimTypes.Email)?.Value;
            var userBookings = _context.BookingModels.Where(b => b.ApplicationUserId == userId).ToList();
            return View(userBookings);
        }

    }

}

