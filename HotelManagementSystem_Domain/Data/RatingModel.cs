using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_Domain.Data
{
    public class RatingModel
    {
        public int Id { get; set; }
        [Range(0, 5)]
        public string StarRating { get; set; }
    }
}
