using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using HotelManagementSystem_Domain.Data;
using HotelManagementSystem_Persistence.DBContext;
using HotelManagementSystem_Persistence.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelManagementSystemUser.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ApplicationDbContext _applicationDbContext;
        private readonly IUserClaimsPrincipalFactory<IdentityUser> _claimsFactory;
        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IWebHostEnvironment webHostEnvironment,
            ApplicationDbContext applicationDbContext,
                    IUserClaimsPrincipalFactory<IdentityUser> claimsFactory
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
            _applicationDbContext = applicationDbContext;
            _claimsFactory = claimsFactory; 
        }

        /// <summary>
        ///   your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }
        public string ImageUrl {  get; set; }   

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        /// 

        public class InputModel
        {
            
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            public string ImageUrl { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            
            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,

            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var users = _applicationDbContext.ApplicationUsers.FirstOrDefault(a => a.Id == user.Id);
            if (users == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

           
            ViewData["UserImageUrl"] = users.ImageUrl;

           
            
            await LoadAsync(users);

         
            return Page();
        }

       

        public IActionResult GetImage(string imageGuid)
        {
            var webRootPath = _webHostEnvironment.WebRootPath;
            
            var imagePath = Path.Combine(webRootPath, imageGuid);

            if (System.IO.File.Exists(imagePath))
            {
               
                return PhysicalFile(imagePath, "image/jpeg");
            }
            else
            {
                return NotFound("Image not found");
            }
        }

        public async Task<IActionResult> OnPostAsync(ApplicationUser applicationUser)
        {
            var user = await _userManager.GetUserAsync(User);

            var users = _applicationDbContext.ApplicationUsers.FirstOrDefault(a=>a.Id== user.Id);
           // applicationUser = users;
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

           


            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            var webRootPath = _webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            var uploads = Path.Combine(webRootPath, @"images\ProfilePicture");
            var dbpath = "";

            if (files.Count > 0)
            {
                var fileName = Guid.NewGuid().ToString();
                var extension = Path.GetExtension(files[0].FileName);
                var filePath = Path.Combine(uploads, fileName + extension);
                // Delete previous image
                if (!string.IsNullOrEmpty(users.ImageUrl))
                {
                    var previousImagePath = Path.Combine(webRootPath, users.ImageUrl.Replace("/", "\\"));
                    if (System.IO.File.Exists(previousImagePath))
                    {
                        System.IO.File.Delete(previousImagePath);
                    }
                }
                //Update the newimage
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                dbpath = Path.Combine(@"images\ProfilePicture", fileName + extension);
          
            }


            users.ImageUrl = dbpath;

            var result = await _userManager.UpdateAsync(users);

            if (!result.Succeeded)
            {
                StatusMessage = "Unexpected error when trying to update profile image.";
                return RedirectToPage();
            }
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }


    }
}
