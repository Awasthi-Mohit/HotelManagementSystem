using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_Domain.Data
{
    public class ReviewModel
    {
        public int Id { get; set; }
        public int HotelId { get; set; }

        public string? UserComment { get; set; }
        public string? UserRating { get; set; }
        public string ApplicationUserId { get; set; }
      
    }
    public class ReviewViewModel
    {
        public int Id { get; set; }
        public int HotelId { get; set; }

        public string? UserComment { get; set; }
        public string? UserRating { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser? applicationUser { get; set; }
        public string Base64Image { get; set; }


    }
}
