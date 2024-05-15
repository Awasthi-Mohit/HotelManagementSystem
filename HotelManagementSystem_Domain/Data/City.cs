﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_Domain.Data
{
    public class City
    {


        public int CityId { get; set; }
        [Required]
        public required string CityName { get; set; }
        [ForeignKey("StaeId")]
        public int StateId { get; set; }
        public required State State { get; set; }
    }
}