
using System.ComponentModel.DataAnnotations;


namespace HotelManagementSystem_Domain.Data
{
    public class checkindate
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }
        public int RoomTypeId { get; set; }

        public string ApplicationUserId { get; set; }


    }
}
