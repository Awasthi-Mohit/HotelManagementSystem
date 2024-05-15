using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_Domain.Data
{
    public class BookingModel
    {
        public int Id { get; set; }
        [Required]
        public int RoomCount { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }
        //public string OrderStatus { get; set; }
        public string CustomerName { get; set; }
        public string? CustomerPhoneNumber { get; set; }
        public string CustomerEmail { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustomerPinCode { get; set; }


        public int? TotalPayment { get; set; }

        public string? ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

        public int RoomTypeId { get; set; }
        [ForeignKey("RoomTypeId")]
        public RoomTypeModel RoomTypeModel { get; set; }
        public string? TransationId { get; set; }
        public DateTime PaymentDate { get; set; }
     
        

    }

}
