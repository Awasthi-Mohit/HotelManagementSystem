using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_Domain.Data.ViewModels
{
    public  class Roomcreate
    {
        public RoomTypeModel RoomType { get; set; }
        public HotelModel HotelDetails { get; set; }
        public IEnumerable<RoomTypeModel> RoomList { get; set; }


    }
}
