using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WijsOef.Data.Data;
using WijsOef.Data.Infrastructure.Location;
using WijsOef.Models;

namespace WijsOef.Helpers
{
    public static class OfficeHelper
    {
        public static IList<OfficeDistanceDto> Convert(this IList<office> offices,MapPoint currentLocation=null)
        {
            return offices.Select(t =>
                new OfficeDistanceDto()
                {
                    City = t.city.ToUpper(),
                    Street = t.street.ToLower(),
                    Latitude=t.latitude,
                    Longitude=t.longitude,
                    HasHelpDesk = (t.has_support_desk == "Y"),
                    IsOpenOnWeekends=(t.is_open_in_weekends=="Y"),
                    Distance = currentLocation==null ? 0 : (int)Geography.HaversineDistance(new MapPoint() { Latitude = t.latitude, Longitude = t.longitude },
                                                           currentLocation, 
                                                           Geography.DistanceUnit.Kilometers) 
                }
            ).ToList();
        }
    }
}