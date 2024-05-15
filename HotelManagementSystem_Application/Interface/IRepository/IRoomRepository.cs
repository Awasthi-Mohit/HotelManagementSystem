using HotelManagementSystem_Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_Application.Interface.IRepository
{
    public interface IRoomRepository : IRepository<RoomType>
    {
        List<RoomType> GetRoomsByHotelId(int hotelId);

    }
}
