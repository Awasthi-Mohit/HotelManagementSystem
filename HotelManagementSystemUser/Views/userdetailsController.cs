using HotelManagementSystem_Application.Interface.IRepository;
using HotelManagementSystem_Persistence.DBContext;
using HotelManagementSystem_Persistence.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HotelManagementSystemUser.Controllers
{
    [Authorize(Roles = "Admin")]
    public class userdetailsController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly ApplicationDbContext _context;
        public userdetailsController(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _unitofwork = unitOfWork;
            _context = context;
        }

        //Admin case
        public IActionResult Index()
        {
            var bookings = _unitofwork.BookingModelRepository.GetAll().OrderByDescending(b => b.PaymentDate);
            foreach (var booking in bookings)
            {
                var user = _context.ApplicationUsers.FirstOrDefault(a => a.Id == booking.ApplicationUserId);
                booking.ApplicationUser = user;
            }
            return View(bookings);
        }





    }
}

