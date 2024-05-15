using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_Domain.Data
{
    public class State
    {
        public int StateId { get; set; }
        public required string StateName { get; set; }

        [ForeignKey("CountryId")]
        public int CountryId { get; set; }
        public required Country Citys { get; set; }
        //public ICollection<City> Cities { get; set; }
    }
}
