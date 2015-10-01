using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WijsOef.Bussiness.Services
{
    public class OfficeBusiness
    {
        public long Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsOpenInWeekends { get; set; }
        public bool HasSupportDesk { get; set; }
    }
}
