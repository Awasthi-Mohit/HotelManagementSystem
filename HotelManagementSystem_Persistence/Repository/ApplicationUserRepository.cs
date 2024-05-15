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
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationRepository
    {
        private readonly ApplicationDbContext _context;
        public ApplicationUserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
