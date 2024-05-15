using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_Domain.Data
{
    public  class Picture
    {
        public int Id { get; set; }
        [Display(Name = "Image Url")]
        public List<Image> Images { get; set; }
        public int RoomId { get; set; }
        public RoomType Room { get; set; }
        public int hotel { get; set; }
        public int IsDeleted { get; set; }
    }
}
