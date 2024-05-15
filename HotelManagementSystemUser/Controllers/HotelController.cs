using HotelManagementSystem_Application.Interface.IRepository;
using HotelManagementSystem_Domain.Data;
using HotelManagementSystem_Domain.Data.DTO;
using HotelManagementSystem_Domain.Data.ViewModels;
using HotelManagementSystem_Domain.Utility;
using HotelManagementSystem_Persistence.DBContext;
using HotelManagementSystem_Persistence.Migrations;
using HotelManagementSystem_Persistence.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.IdentityModel.Tokens;


namespace HotelManagementSystemUser.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]

    public class HotelController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HotelController(ApplicationDbContext context, IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {

            var hotels = _unitOfWork.HotelRepository.GetAll();
            return View(hotels);
        }
        public IActionResult GetImage(string imageGuid)
        {
            if (string.IsNullOrEmpty(imageGuid))
            {
                return NotFound("Image not found");
            }

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
         //Hotel Create
        [Authorize(Roles = SD.Role_Admin)]

       public IActionResult Create()
        {
            return View();
        }   
        [HttpPost]  
        [ValidateAntiForgeryToken]
        public IActionResult CreateHotel(HotelModel hotelModel)
        {
            var webRootPath = _webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            var uploads = "";
            var dbpath = "";
            // Get the file extension
            string fileName = files[0].FileName;
            string extension = Path.GetExtension(fileName);
            // Check if extension is not empty and is of expected types
            if (!string.IsNullOrEmpty(extension) && (extension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase) || extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) || extension.Equals(".png", StringComparison.OrdinalIgnoreCase)))
            {
                // File is of expected type (JPEG or PNG)              
                 
                    uploads = Path.Combine(webRootPath, @"images\Hotels");
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    dbpath = Path.Combine(@"images\Hotels", fileName + extension);
             

                hotelModel.ImageUrl = dbpath;
                _unitOfWork.HotelRepository.Add(hotelModel);
                _unitOfWork.savechanges();
            }
            else
            {
                // File is not of expected type
                // Handle the error or provide feedback to the user
            }
           

            return RedirectToAction("Index");
        }

        //Edit the Hotel 
        public IActionResult EditHotel(int id)
        {
            var hotel = _unitOfWork.HotelRepository.Get(id);

            if (hotel == null)
            {
                return NotFound();
            }
            ViewBag.CurrentImageUrl = hotel.ImageUrl;


            return View(hotel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult EditHotel(int id, HotelModel hotelModel)
        {
            try
            {
                var webRootPath = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                var uploads = "";
                var dbpath = "";


                if (files.Count > 0)
                {
                    var fileName = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(files[0].FileName);
                    uploads = Path.Combine(webRootPath, "images", "Hotels");

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);

                    }

                    dbpath = Path.Combine("images", "Hotels", fileName + extension);
                    hotelModel.ImageUrl = dbpath;
                }

                var hotel = _unitOfWork.HotelRepository.Get(id);

                if (hotel == null)
                {
                    return NotFound();
                }

                hotel.Name = hotelModel.Name;
                hotel.Place = hotelModel.Place;
                hotel.ImageUrl = hotelModel.ImageUrl;
                hotel.TotalRoom = hotelModel.TotalRoom;
                _unitOfWork.HotelRepository.Update(hotel);
                _unitOfWork.savechanges();

                return RedirectToAction("Index");
            }
            catch (Exception m)
            {

                throw;
            }
        }

       
        //Room Creation 

        public IActionResult Room(int hotelId)
        {
            var hotel = GetHotelDetails(hotelId);
            var roomTypelist = _unitOfWork.RoomTypeModelRepository.GetAll().Where(x=>x.HotelId==hotelId).ToList();
            ViewBag.RoomTypeList = roomTypelist;
            if (hotel != null)
            {
                ViewBag.HotelId = hotelId;
                ViewBag.HotelName = hotel.Name;
                ViewBag.HotelPlace= hotel.Place;
                ViewBag.HotelImageUrl = hotel.ImageUrl;

                return View();
            }
            return NotFound();
        }

        private HotelModel GetHotelDetails(int hotelId)
        {
            var hotel = _unitOfWork.HotelRepository.Get(hotelId);
            return hotel;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateRoom(RoomTypeModel roomModel)
        {
            try
            {
                var webRootPath = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                var uploads = Path.Combine(webRootPath, "images", "Hotels");
                var dbPath = "";

                // Check if the room number already exists for the specified hotel
                var roomExists = _unitOfWork.RoomTypeModelRepository.GetAll()
                    .Any(x => x.RoomNo == roomModel.RoomNo && x.HotelId == roomModel.HotelId);

                if (roomExists)
                {
                    ModelState.AddModelError("RoomNo", "Room number already exists for this hotel.");
                }
                else
                {
                    if (files.Count > 0)
                    {
                        var fileName = Guid.NewGuid().ToString();
                        var extension = Path.GetExtension(files[0].FileName);
                        using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                        dbPath = Path.Combine("images", "Hotels", fileName + extension);
                    }

                    roomModel.ImageUrl = dbPath;
                    _unitOfWork.RoomTypeModelRepository.Add(roomModel);
                    _unitOfWork.savechanges();

                    return RedirectToAction("Room", new { hotelId = roomModel.HotelId });
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }

            var hotel = GetHotelDetails(roomModel.HotelId);
            ViewBag.RoomTypeList = _unitOfWork.RoomTypeModelRepository.GetAll().Where(x => x.HotelId == roomModel.HotelId).ToList();
            if (hotel != null)
            {
                ViewBag.HotelId = roomModel.HotelId;
                ViewBag.HotelName = hotel.Name;
                ViewBag.HotelPlace = hotel.Place;
                ViewBag.HotelImageUrl = hotel.ImageUrl;
            }
            return View("Room", roomModel);
        }
      





        //Edit the Room.
        public IActionResult editroomes(int id)
        {
            var room = _unitOfWork.RoomTypeModelRepository.Get(id);
            if (room == null)
            {
                return NotFound();
            }
            ViewBag.CurrentImageUrl = room.ImageUrl;
            var hotelId = room.HotelId; // Get the associated hotel ID
                ViewBag.HotelId = hotelId; // Add hotel ID to ViewBag for use in the view or in a redirect

            return View(room);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditRoom(int id, RoomTypeModel updatedRoom, IFormFile files)
        {
            if (updatedRoom == null)
            {
                return BadRequest("Updated room data is null.");
            }

            // Retrieve the room by ID to ensure it exists
            var existingRoom = _unitOfWork.RoomTypeModelRepository.Get(id);
            if (existingRoom == null)
            {
                return NotFound("Room not found.");
            }

            try
            {
                // Update basic room properties
                existingRoom.RoomType = updatedRoom.RoomType;
                existingRoom.Facilities = updatedRoom.Facilities;
                existingRoom.RoomNo = updatedRoom.RoomNo;
                existingRoom.Price = updatedRoom.Price;

                if (files != null)
                {
                    // Handle new image upload
                    var webRootPath = _webHostEnvironment.WebRootPath;
                    var uploads = Path.Combine(webRootPath, "images", "Hotels");
                    var fileName = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(files.FileName);

                    // Ensure the uploads directory exists
                    if (!Directory.Exists(uploads))
                    {
                        Directory.CreateDirectory(uploads);
                    }

                    // Save the uploaded file to the appropriate directory
                    var filePath = Path.Combine(uploads, fileName + extension);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        files.CopyTo(fileStream);
                    }

                    // Update the ImageUrl property of the existing room
                    existingRoom.ImageUrl = Path.Combine("images", "Hotels", fileName + extension);
                }

                // Update the room in the database
                _unitOfWork.RoomTypeModelRepository.Update(existingRoom);
                _unitOfWork.savechanges();

                return LocalRedirect($"/Hotel/roomid={id}");
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return a 500 status code with an error message
                return StatusCode(500, $"An error occurred while updating the room: {ex.Message}");
            }
        }




        //Adding Room picture
        [HttpGet]
        public IActionResult picture(int? roomid)
        {
            var pictures = _unitOfWork.pictureroomRepository.GetAll().Where(x => x.RoomId == roomid).ToList();
            
            ViewBag.Pictures = pictures;
            ViewBag.RoomId = roomid;

            return View();

        }


        
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async  Task<IActionResult> editpicture(Picturemenul picturemenul, IFormFile image, int roomId)
        {
            try
            {
                var webRootPath = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                var uploads = Path.Combine( "images", "RoomsMulti");
             
                
                if (files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        var fileName = Guid.NewGuid().ToString();
                        var extension = Path.GetExtension(file.FileName);
                        uploads = Path.Combine(webRootPath, "images", "RoomsMulti");

                        using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        var dbpath = Path.Combine("images", "RoomsMulti", fileName + extension);

                        //using (var stream = new FileStream(filePath, FileMode.Create))
                        //{
                        //    await file.CopyToAsync(stream);
                        //}

                        Picturemenul picture = new Picturemenul
                        {
                            ImageUrl = dbpath,
                            RoomId = roomId,
                            hotelid = 1// Example: Set HotelId based on your logic
                        };

                        _unitOfWork.RoomTypeModelRepository.Add(picture);
                    }
                   await _unitOfWork.SaveChangesAsync();                  
                    return LocalRedirect($"/Hotel/picture?roomid={roomId}");
                   
                }
                return LocalRedirect($"/Hotel/picture?roomid={roomId}");
            }
            catch (Exception m)
            {
                throw;
            }

        }

        

        //Delete Hotel

        public IActionResult DeleteHotel(int id)
        {
            var hotel = _unitOfWork.HotelRepository.Get(id);

            if (hotel == null) return NotFound();
            _unitOfWork.HotelRepository.Remove(hotel);
            _unitOfWork.savechanges();
            return RedirectToAction("Index");
         }
     
        public IActionResult Roomlist()
        {
            var roomTypes = _unitOfWork.RoomTypeModelRepository.GetAll();
            return View(roomTypes);
        }
        //Delete picture 

        public IActionResult Delete(int RoomId, int pictureid)
        {
            var pictures = _unitOfWork.pictureroomRepository.GetAll().Where(x => x.RoomId == RoomId).ToList();
            foreach (var picture in pictures)
            {
                var text = picture.ImageUrl.ToString();
                string[] parts = text.Split(',');
                foreach (var part in parts)
                {

                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, part);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }

                    _unitOfWork.pictureroomRepository.Remove(pictureid);
                    _unitOfWork.savechanges();

                    return LocalRedirect($"/Hotel/picture?roomid={RoomId}");        
                }

            }
            return RedirectToAction("Index");   

        }
       
        //Js For Check for Room 
        [HttpPost]
        public IActionResult CheckRoom(int room)
        {

            System.Threading.Thread.Sleep(200);
            //var Data = _unitOfWork.RoomTypeModelRepository.Get(x => x.RoomNo == Roomno).FirstOrDefault();
            var Data=_unitOfWork.RoomTypeModelRepository.GetAll(X=>X.RoomNo==room);

            if (Data.IsNullOrEmpty())

            {

                return Json(0);

            }

            else

            {

                return Json(1);

            }

        }
      
    }


}

