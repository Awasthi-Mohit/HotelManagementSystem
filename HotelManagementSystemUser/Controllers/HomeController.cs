
using HotelManagementSystem_Application.Interface.IRepository;
using HotelManagementSystem_Domain.Data;
using HotelManagementSystem_Persistence.DBContext;

using Microsoft.AspNetCore.Mvc;


namespace HotelManagementSystemUser.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitofwork;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork,ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _unitofwork = unitOfWork;
            _context=context; 
            _webHostEnvironment = webHostEnvironment;   
        }
      
        

        public IActionResult Index(string search)
        {
            var hotels = _unitofwork.HotelRepository.GetAll();

            if (!string.IsNullOrEmpty(search))
            {
                hotels = hotels.Where(s => s.Place.Contains(search)||s.Name.Contains(search));
            }

            return View(hotels.ToList());
             
        } 
        public IActionResult ShowReviews(int hotelId)
        {
            List<ReviewViewModel> reviewViewModels = new List<ReviewViewModel>();
            var reviews = _unitofwork.ReviewModelRepository.GetAll().Where(r => r.HotelId == hotelId).ToList();

            foreach (var item in reviews)
            {
                var user = _context.ApplicationUsers.FirstOrDefault(a => a.Id == item.ApplicationUserId);

               
                reviewViewModels.Add(new ReviewViewModel
                {
                    UserComment = item.UserComment,
                    UserRating = item.UserRating,
                    HotelId = item.HotelId,
                    ApplicationUserId = item.ApplicationUserId,
                    applicationUser = user,
                });

            }
            return View(reviewViewModels);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        //public IActionResult Rommmultiplictur(int RoomId)
        //{

        //    StringBuilder htmlBuilder = new StringBuilder();

        //    List<string> pictureUrls = new List<string>();
        //    if (RoomId != 0)
        //    {
        //        pictureUrls = _unitofwork.pictureroomRepository.GetAll()
        //                              .Where(m => m.RoomId == RoomId)
        //                              .Select(p => p.ImageUrl)
        //                              .ToList();
        //    }
        //    return View(pictureUrls);
        //}
        //Showing the MultiImages of the Room  


        public IActionResult Rommmultiplictur(int RoomId)
        {
            try
            {

                List<string> pictureUrls = new List<string>();
                if (RoomId != 0)
                {
                    pictureUrls = _unitofwork.pictureroomRepository.GetAll()
                                          .Where(m => m.RoomId == RoomId)
                                          .Select(p => p.ImageUrl)
                                          .ToList();
                }


                return PartialView("_SliderPartial", pictureUrls);
            }
            catch (Exception)
            {
                throw;
            }

        }




    }
}
