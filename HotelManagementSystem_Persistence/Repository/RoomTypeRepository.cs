using HotelManagementSystem_Application.Interface.IRepository;
using HotelManagementSystem_Domain.Data;
using HotelManagementSystem_Persistence.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_Persistence.Repository
{
    public class RoomTypeRepository : Repository<RoomTypeModel>, IRoomTypeModelRepository
    {
        private readonly ApplicationDbContext _context;
        public RoomTypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context; 
        }

        public void Add(Picturemenul picturemenul)
        {
           
                // Add the picturemenul object to the context
                _context.picturemenuls.Add(picturemenul);
               
              
          
        }
    }
}
