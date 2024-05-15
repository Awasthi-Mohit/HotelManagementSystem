using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_Domain.Utility
{
    public static  class SD
    {
        // ROLES
        public const string Role_Admin = "Admin";
   
        public const string Role_Individual = "Individual User";


        //Room Type 
        public const string RoomTypeDeluxe = "Deluxe";
        public const string RoomTypeSuite = "Suite";
        public const string RoomTypeLuxury = "Luxury";
        public const string RoomTypeStudio = "Studio ";
        //for Payment Status
        public const string PaymentStatusPending = "Pending";
        public const string PaymentStatusApproved = "Approved";
        public const string PaymentStatusDelayPayment = "PaymentStatusDelay";
        public const string PaymentstatusRejected = "Rejected";
    }
}
