using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_Domain.Data.DTO
{
    public  class pictureDto
    {

        public int id { get; set; }
        public int RoomId { get; set; }
        public string ImageUrl { get; set; }
    }
}
