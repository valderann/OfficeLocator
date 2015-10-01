using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WijsOef.Models
{
    public class OfficeDistanceDto
    {
        public string Street { get; set; }
        public string City { get; set; }
        private double DistanceRaw { get; set; }
        public string Distance { get; set; }
        public bool HasHelpDesk { get; set; }
        public bool IsOpenOnWeekends { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}