using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WijsOef.Data;
using WijsOef.Data.Infrastructure.Location;
using WijsOef.Data.Repositories;
using WijsOef.Helpers;
using WijsOef.Models;

namespace WijsOef.Controllers
{
    public class OfficeController : BaseController
    {
        private const int RadiusInKm = 16;

        protected IOfficeRepository OfficeRepository { get; set; }
        public OfficeController()
        {
            OfficeRepository = new OfficeRepository();
        }

        public ActionResult Index(string city,bool? hasSupportDesk,bool? isOpenWeekend)
        {
            return View(new OfficeSearchModel() {City=city,HasSupportDesk=hasSupportDesk?? false,IsOpenWeekend=isOpenWeekend??false });
        }

        public ActionResult GetNearestOffices(double latitude, double longitude, bool? isOpenInWeekends, bool? isWithSupportDesk)
        {
            if (OfficeRepository == null) throw new ArgumentNullException("OfficeRepository is null");

            var offices = OfficeRepository.GetNearestOffices(new Data.Domain.OfficeQuery()
            { 
                                                            Latitude = latitude, 
                                                            Longitude = longitude,
                                                            IsOpenInWeekends = isOpenInWeekends ?? false,
                                                            IsWithSupportDesk = isWithSupportDesk ?? false,
                                                            Radius = RadiusInKm
            });

            var convertedOffices=offices.Convert(new MapPoint(latitude,longitude)).OrderBy(v=>v.Distance).ToList();

            return Json(new JsonObject<List<OfficeDistanceDto>>(){Success=true,Result=convertedOffices}, JsonRequestBehavior.AllowGet);
        }
    }
}
