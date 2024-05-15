using HotelManagementSystem_Application.Interface.IRepository;
using HotelManagementSystem_Domain.Data;
using HotelManagementSystem_Persistence.DBContext;


namespace HotelManagementSystem_Persistence.Repository
{
    public class HotelRepository : Repository<HotelModel>, IHotelRepository
    {
        private readonly ApplicationDbContext _context;
        public HotelRepository(ApplicationDbContext context) : base(context)
        {
            _context = context; 
        }


        bool IHotelRepository.Update(HotelModel hotel)
        {
            var hotelInDb = _context.HotelModels.FirstOrDefault(pr => pr.Id == hotel.Id);
            if (hotelInDb != null)
            {
                if (hotel.ImageUrl != null)
                    hotelInDb.ImageUrl = hotel.ImageUrl;
                hotelInDb.Name = hotel.Name;
                hotelInDb.Place = hotel.Place;
                return true;
            }
            return false;
        }

        
    }
}
