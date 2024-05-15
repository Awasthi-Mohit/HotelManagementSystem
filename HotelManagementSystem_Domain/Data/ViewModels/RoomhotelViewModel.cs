using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_Domain.Data.ViewModels
{
    public  class RoomhotelViewModel
    {
        public ApplicationUser ApplicationUser { get; set; }
        public HotelModel HotelDetails { get; set; }
        public RoomTypeModel RoomType { get; set; }
        public List<RoomTypeModel> Rooms { get; set; }


    }
}
