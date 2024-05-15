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
    public class RoomRepository : Repository<RoomType>, IRoomRepository
    {
        private readonly ApplicationDbContext _context;

        public RoomRepository(ApplicationDbContext context) : base(context)
        {
            _context = context; 
        }

        public List<RoomType> GetRoomsByHotelId(int hotelId)
        {
            throw new NotImplementedException();
        }
    }
}
