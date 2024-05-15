using HotelManagementSystem_Application.Interface.IRepository;
using HotelManagementSystem_Domain.Data;
using HotelManagementSystem_Persistence.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            ApplicationRepository = new ApplicationUserRepository(_context);
            BookingModelRepository=new BookingModelRepository(_context);
            HotelRepository=new HotelRepository(_context);
            RatingModelRepository=new RatingRepository(_context);
            ReviewModelRepository=new ReviewModelRepository(_context);  
            RoomTypeModelRepository=new RoomTypeRepository(_context);
            RoomRepository = new RoomRepository(_context);
            pictureroomRepository=new pictureroomRepository(_context);
            CheckRepository = new CheckinRepository(_context);
        }
        public IApplicationRepository ApplicationRepository { get; set; }

        public IBookingModelRepository BookingModelRepository { get; set; }

        public IHotelRepository HotelRepository { get; set; }

        public IRatingModelRepository RatingModelRepository { get; set; }
            
        public IReviewModelRepository ReviewModelRepository { get; set; }

        public IRoomTypeModelRepository RoomTypeModelRepository { get; set; }

        public IRoomRepository RoomRepository {get; set; }

        public IpictureroomRepository pictureroomRepository { get; set; }

        public ICheckRepository CheckRepository {  get; set; }  

        public  void savechanges()
        {
             _context.SaveChanges();
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception x)
            {

                return false;
            }
        }
    }
}
