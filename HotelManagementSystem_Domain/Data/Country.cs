using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_Domain.Data
{
    public class Country
    {
        public int CountryId { get; set; }
        [Required]
        public  string CountryName { get; set; }
        //public ICollection<State> States { get; set; }
    }
}
