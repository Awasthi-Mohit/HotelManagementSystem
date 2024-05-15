using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace HotelManagementSystem_Domain.Data
{
    public class RoomTypeModel
    {
        public int Id { get; set; }

        [AllowNull]
        public string Price { get; set; }
        [NotMapped]
        public bool Isselected { get; set; }
        public string Facilities { get; set; }
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }
        public string RoomType { get; set; }
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Can only be 9 characters long")]

        public int RoomNo { get; set; }
        public int NumberOfRooms { get; set; }
        public bool ?IsAvailable { get; set; }=true;

        public int HotelId { get; set; }
        [ForeignKey("HotelId")]
        public HotelModel Hotel { get; set; }
    }
}
