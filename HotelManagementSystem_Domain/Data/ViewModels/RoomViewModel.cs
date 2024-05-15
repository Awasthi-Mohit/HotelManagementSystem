using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_Domain.Data.ViewModels
{
    public class RoomViewModel
    {
        public Dictionary<int, decimal> SelectedRooms;

        public ApplicationUser ApplicationUser { get; set; }
        public HotelModel HotelDetails { get; set; }
        public RoomTypeModel RoomType { get; set; }
        public BookingModel Booking { get; set; }
        public RoomType roomType { get; set; }

        public List<RoomTypeModel> RoomListbyhotel { get; set; }
        public List<BookingModel> Bookings { get; set; }

    }
}
