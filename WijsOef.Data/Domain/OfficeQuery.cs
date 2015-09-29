using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WijsOef.Data.Domain
{
    public class OfficeQuery
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        //Radius in KM
        public int Radius { get; set; }
        public bool IsOpenInWeekends { get; set; }
        public bool IsWithSupportDesk { get; set; }
    }
}
