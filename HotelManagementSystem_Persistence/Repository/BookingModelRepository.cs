using HotelManagementSystem_Application.Interface.IRepository;
using HotelManagementSystem_Domain.Data;
using HotelManagementSystem_Domain.Data.ViewModels;
using HotelManagementSystem_Persistence.DBContext;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem_Persistence.Repository
{
    public class BookingModelRepository : Repository<BookingModel>, IBookingModelRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingModelRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

      
    }
}

