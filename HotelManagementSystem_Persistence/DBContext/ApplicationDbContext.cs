using HotelManagementSystem_Domain;
using HotelManagementSystem_Domain.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_Persistence.DBContext
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Country> Countrys { get; set; }
        public DbSet<State> States{ get; set; }
        public DbSet<City> Citys { get; set; }
        public DbSet<HotelModel>HotelModels { get; set; }   
        public DbSet<RatingModel> RatingModels { get; set; }
        public DbSet<RoomTypeModel> RoomTypes { get; set; }
        public DbSet<ReviewModel> Reviews { get; set; }
        public DbSet<BookingModel>BookingModels { get; set; }
        public DbSet<RoomType> Room {  get; set; }  
        public DbSet<Image>  Images{  get; set; }  
        public DbSet<Picturemenul>  picturemenuls{  get; set; }  
        public DbSet<Picture> Pictures {  get; set; }  
        public DbSet<checkindate> Checkindates {  get; set; }  
       
        protected override void OnModelCreating(ModelBuilder builder)
        {
           
            base.OnModelCreating(builder);

        }
    }
}
 