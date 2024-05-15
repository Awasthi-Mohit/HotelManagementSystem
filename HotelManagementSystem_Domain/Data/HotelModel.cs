using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HotelManagementSystem_Domain.Data
{
    public class HotelModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }
        public int TotalRoom {get;set;}
        public int Rating { get; set; } 
        
        [Display(Name = "Rating")]
        public int? RatingId { get; set; }
        [ForeignKey("RatingId")]
        public RatingModel RatingModel { get; set; }

        //public ICollection<RoomTypeModel> RoomTypes { get; set; }

    }
}
