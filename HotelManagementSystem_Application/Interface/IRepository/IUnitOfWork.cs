using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_Application.Interface.IRepository
{
    public interface IUnitOfWork
    {

        IApplicationRepository ApplicationRepository { get; }   
        IBookingModelRepository BookingModelRepository { get; }
        IHotelRepository HotelRepository { get; }
        IRatingModelRepository RatingModelRepository { get; }
        IReviewModelRepository ReviewModelRepository { get; }
        IRoomTypeModelRepository RoomTypeModelRepository { get; }
        IRoomRepository RoomRepository { get; }
        IpictureroomRepository pictureroomRepository { get; } 
        ICheckRepository CheckRepository { get; }   
        void savechanges();
        Task<bool> SaveChangesAsync();

    }
}
