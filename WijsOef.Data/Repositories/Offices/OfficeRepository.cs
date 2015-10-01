using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WijsOef.Data.Data;
using WijsOef.Data.Domain;
using WijsOef.Data.Infrastructure.Location;
using WijsOef.Data.Repositories;

namespace WijsOef.Data
{
    public class OfficeRepository : BaseRepository, IOfficeRepository
    {
        public IList<office> GetNearestOffices(OfficeQuery qry)
        {
            using (var db = new ExcerciseEntities())
            {
                // Find coordinates in a square instead of a radius to improve performance. 
                // The index on the latitude and longitude columns will be hit in the database.
                var boundingBox = Geography.GetBoundingBox(new MapPoint() { Latitude=qry.Latitude,Longitude=qry.Longitude}, qry.Radius);

                var query = db.offices.Where(v =>
                                            (v.latitude > boundingBox.MinPoint.Latitude && v.latitude < boundingBox.MaxPoint.Latitude
                                             && v.longitude > boundingBox.MinPoint.Longitude && v.longitude < boundingBox.MaxPoint.Longitude
                                            ) 
                                            && (!qry.IsOpenInWeekends || v.is_open_in_weekends=="Y") 
                                            && (!qry.IsWithSupportDesk || v.has_support_desk=="Y")
                                        );

                return query.Distinct().ToList<office>();
            }
        }
    }
}
