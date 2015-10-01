using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WijsOef.Bussiness.Domain;
using WijsOef.Bussiness.Services;
using WijsOef.Data.Domain;
using WijsOef.Data.Infrastructure.Location;
using WijsOef.Data.Repositories;

namespace WijsOef.Bussiness
{
    public class OfficeService:IOfficeService
    {
        protected IOfficeRepository OfficeRepository;
        public OfficeService(IOfficeRepository officeRepository)
        {
            OfficeRepository = officeRepository;
        }
        
        public IList<OfficeBusinessDistance> GetNearestOffices(OfficeQuery qry)
        {
            if (OfficeRepository == null) throw new ArgumentNullException("Officerepository not defined");

            return OfficeRepository.GetNearestOffices(qry).Select(t => new OfficeBusinessDistance()
            {
                City = t.city,
                HasSupportDesk = (t.has_support_desk == "Y"),
                IsOpenInWeekends = (t.is_open_in_weekends=="Y"),
                Street = t.street,
                Latitude = t.latitude,
                Longitude = t.longitude,
                Id = t.id,
                Distance = Geography.HaversineDistance(new MapPoint() { Latitude = t.latitude, Longitude = t.longitude },
                                                       new MapPoint() { Latitude=qry.Latitude,Longitude=qry.Longitude},
                                                       Geography.DistanceUnit.Kilometers)

            })
            //Correct rounding errors
            .Where(t=>t.Distance<=qry.Radius)
            //Sort by distance
            .OrderBy(t=>t.Distance).ToList();
        }
    }
}
