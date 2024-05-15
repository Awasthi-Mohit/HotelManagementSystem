using HotelManagementSystem_Application.Interface.IRepository;
using HotelManagementSystem_Domain.Data;
using HotelManagementSystem_Persistence.DBContext;
using HotelManagementSystem_Persistence.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;

namespace HotelManagementSystemUser.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitofwork;
        private readonly ApplicationDbContext _context;    

        public PaymentController(IUnitOfWork unitOfWork )
        {
           
            _unitofwork = unitOfWork;

        }
        public IActionResult Index ()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            var user = _context.ApplicationUsers.FirstOrDefault(a => a.Id == userId);

            if (user != null)
            {
               
                var bookings = user.UserName; 

                return View(bookings);
            }
            else
            {
             
                return NotFound();
            }
        }
    
    
     
    }
}
   

