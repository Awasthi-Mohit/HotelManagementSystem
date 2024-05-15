using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_Domain.Data.DTO
{
    public class HotelModelDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }
        public int TotalRoom { get;set; }
        [Display(Name = "Rating")]
        public int? RatingId { get; set; }
        [ForeignKey("RatingId")]
        public RatingModel RatingModel { get; set; }

        public ICollection<RoomTypeModel> RoomTypes { get; set; }

    }
}
