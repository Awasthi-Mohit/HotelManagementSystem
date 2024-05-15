using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_Domain.Data.DTO
{
    public class ReviewModelDTO
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string? UserComment { get; set; }
        public int? UserRating { get; set; }
        public string ApplicationUserId { get; set; }
        public string UserId { get; set; }
        public int HotelBookingId { get; set; }
    }
}
