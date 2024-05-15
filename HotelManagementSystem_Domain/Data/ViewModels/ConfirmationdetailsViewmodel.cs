using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_Domain.Data.ViewModels
{
    public class ConfirmationdetailsViewmodel
    {
        public BookingModel Booking { get; set; }
        public ReviewModel ReviewModel { get; set; }
        public HotelModel HotelDetails { get; set; }

        public RoomTypeModel RoomTypees { get; set; }
        public int HotelId { get; set; }


    }
}
