//using HotelManagementSystem_Application.Interface.IRepository;
//using HotelManagementSystem_Persistence.DBContext;
//using HotelManagementSystem_Persistence.Repository;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc.ActionConstraints;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
//using System.Security.Claims;


//namespace HotelManagementSystemUser.Views.Shared
//{
//    public partial class _logout
//    {
//        private readonly SignInManager<IdentityUser> _signInManager;
//        private readonly UserManager<IdentityUser> _userManager;
//        private readonly ApplicationDbContext _applicationDbContext;
//        private readonly IUnitOfWork _unitofwork;
//        private object auth;

//        public _logout(SignInManager<IdentityUser> signInManager, ApplicationDbContext applicationDbContext,IUnitOfWork unitOfWork)
//        {
//            _signInManager = signInManager;
//            _applicationDbContext = applicationDbContext;
//            _unitofwork = unitOfWork;   
//        }
//        public IActionConstraint index(string userId)
//        {
//            var users = _applicationDbContext.ApplicationUsers.FirstOrDefault(a => a.Id == userId);
//            return Views("users");
//        }
//        private async Task LoadUserData()
//        {
//            try
//            {
//                var data = await auth.GetAuthenticationStateAsync();
//                if (data.User.Identity.IsAuthenticated)
//                {
//                    var userEmail = data.User.FindFirst(ClaimTypes.Email)?.Value;
//                    currentUser = _unitofwork.ApplicationRepository.FirstOrDefault(u => u.Email == userEmail);
//                    if (currentUser != null)
//                    {
//                        await ConvertImage(currentUser.UserImage);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                errorPopup.ErrorMessage = ($"{ex.Message}");
//                errorPopup.ShowError();
//                Console.WriteLine($"Error occurred: {ex.Message}");
//            }
//        }
//    }
//}
