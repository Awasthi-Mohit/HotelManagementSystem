using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_Domain.Data.DTO
{
    public class RoomTypeModelDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Price { get; set; }
        public string Facilities { get; set; }
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }
        public string RoomType { get; set; }
        public int? TotalTypeRooms { get; set; }
        public int? OccupiedTypeRooms { get; set; }
        [NotMapped]
        public int? VacantTypeRooms => TotalTypeRooms - OccupiedTypeRooms;
        public int HotelId { get; set; }
        [ForeignKey("HotelId")]
        public HotelModel Hotel { get; set; }
    }
}
