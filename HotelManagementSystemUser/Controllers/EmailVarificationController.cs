using HotelManagementSystem_Application.Interface.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystemUser.Controllers
{
    public class EmailVarificationController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitOfWork _unitofwork;

        public EmailVarificationController(UserManager<IdentityUser>userManager,IUnitOfWork unitOfWork)
        {       _userManager = userManager;
            _unitofwork = unitOfWork;   
        }
        [HttpPost]
                                          
        public async Task<IActionResult> CheckEmailAvailability(string Email)

        {
            var user = await _userManager.FindByEmailAsync(Email);

            System.Threading.Thread.Sleep(200);


            if (user != null)
            {
                return Json(1);
            }

            else

            {

                return Json(0);

            }

        }
        [HttpPost]

        public async Task<IActionResult> CheckuserAvailability(string username)

        {
            var user = await _userManager.FindByNameAsync(username);

            System.Threading.Thread.Sleep(200);


            if (user != null)
            {
                return Json(1);
            }

            else

            {

                return Json(0);

            }

        }
    }
}
