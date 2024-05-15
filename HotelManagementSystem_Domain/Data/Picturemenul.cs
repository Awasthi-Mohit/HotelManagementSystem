using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_Domain.Data
{
    public  class Picturemenul
    {
        [Key]
        public int id { get; set; }
        public int RoomId { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public int hotelid {  get; set; }   
    }
    public class PictureViewModel
    {
        public int id { get; set; }

        public int RoomId { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public List<Picturemenul> picturemenulslist { get; set;}

    }



}
