using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_Domain.Data.DTO
{
    public class BookingModelDTO
    {


        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

        public int RoomTypeId { get; set; }
        [ForeignKey("RoomTypeId")]
        public RoomTypeModel RoomTypeModel { get; set; }
        public int? ReviewId { get; set; }

        [ForeignKey("ReviewId")]
        public ReviewModel ReviewModel { get; set; }

        [Required]
        public int RoomCount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }

        public string OrderStatus { get; set; }
        public string PaymentStatus { get; set; }
        public int TotalPayment { get; set; }
        [NotMapped]
        public int CustomerRating { get; set; }
        [NotMapped]
        public string CustomerComment { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public string? CustomerPinCode { get; set; }
        public string? CustomerPaymentIntentId { get; set; }
        public string TxId { get; set; }
    }
}
