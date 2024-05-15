using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem_Domain.Data
{
    public class Image
    {
        
            public int Id { get; set; }
            public string Url { get; set; }

        public static object FromStream(MemoryStream memoryStream)
        {
            throw new NotImplementedException();
        }
    }
}
