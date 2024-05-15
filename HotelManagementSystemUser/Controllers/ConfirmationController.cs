using HotelManagementSystem_Application.Interface.IRepository;
using HotelManagementSystem_Domain.Data;
using HotelManagementSystem_Domain.Data.ViewModels;
using HotelManagementSystem_Persistence.DBContext;

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HotelManagementSystemUser.Controllers
{

    public class ConfirmationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitwork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ConfirmationController(ApplicationDbContext context,IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)

        {
             _context = context;
            _unitwork = unitOfWork; 
            _webHostEnvironment = webHostEnvironment;       
        }
        public IActionResult Index()
        {
            var existingBooking = _unitwork.BookingModelRepository.GetAll()
                .Where(bookings => bookings.Id == bookings.Id && bookings.TransationId == bookings.TransationId)
                .OrderByDescending(bookings => bookings.Id)
                .FirstOrDefault();
            var hotelDetails = _unitwork.HotelRepository.Get(existingBooking.Id);

            var confirmationDetailsViewModel = new ConfirmationdetailsViewmodel
            {
                Booking = existingBooking,
                ReviewModel = new ReviewModel() 
            };

            return View(confirmationDetailsViewModel);
        }


        public IActionResult Review() 
        {
            return View();      
        }
        [HttpPost]
        public IActionResult SubmitReviewForm(ReviewModel ratingModel, int hotelId)
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

               ratingModel.ApplicationUserId = userId;
           

                ratingModel.HotelId = hotelId;

                _unitwork.ReviewModelRepository.Add(ratingModel);
                ratingModel.HotelId = hotelId;

               _unitwork.savechanges();

                return RedirectToAction("Index", "Home");
            }
            catch (Exception  )
            {
                return RedirectToAction("Error");
            }   
        }

    }
}
