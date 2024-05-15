using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_Domain.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(50, ErrorMessage = "name cannot  be more than  characters.")]
        public required string Name { get; set; }
        [Required]

        [Display(Name = "Street Address")]
        public required string StreetAddress { get; set; }

        //[Required]
        //[MaxLength(20, ErrorMessage = "Phone number cannot be more than  characters.")]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "Phone number must contain only numbers.")]
        //[Display(Name = "Phone Number")]
        //public required string PhoneNumber { get; set; }
        [Display(Name = "Postal Code")]
        [MaxLength(8, ErrorMessage = "Postal Code cannot be more than 10 characters.")]
        [RegularExpression("^[0-9]*$")]
        [Required]

        public required string PostalCode { get; set; }

        public string Role { get; set; }
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

        [Display(Name = "City")]
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }

        [NotMapped]
        [Display(Name ="Country")]
        public int CountryId { get; set; }
        [Display(Name = "State")]
        public int StateId {  get; set; }   
    }
}
