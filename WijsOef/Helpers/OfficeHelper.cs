using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WijsOef.Bussiness.Domain;
using WijsOef.Data.Data;
using WijsOef.Data.Infrastructure.Location;
using WijsOef.Models;

namespace WijsOef.Helpers
{
    public static class OfficeHelper
    {
        private static string ReadfriendlyDistance(double distance)
        {
            if (distance < 1) { return (int)(distance*1000) + " m"; }
            else { return( (int)distance).ToString() + " km";}
        }

        public static IList<OfficeDistanceDto> ConvertToJson(this IList<OfficeBusinessDistance> offices)
        {
            return offices.Select(t =>
                new OfficeDistanceDto()
                {
                    City = t.City.ToUpper(),
                    Street = t.Street.ToLower(),
                    Latitude=t.Latitude,
                    Longitude=t.Longitude,
                    HasHelpDesk = t.HasSupportDesk,
                    IsOpenOnWeekends=t.IsOpenInWeekends,
                    Distance=ReadfriendlyDistance(t.Distance)
                }
            ).ToList();
        }
    }
}