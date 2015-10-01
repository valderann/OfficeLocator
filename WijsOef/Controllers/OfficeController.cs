using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WijsOef.Bussiness;
using WijsOef.Bussiness.Services;
using WijsOef.Data;
using WijsOef.Data.Infrastructure.Location;
using WijsOef.Data.Repositories;
using WijsOef.Helpers;
using WijsOef.Models;

namespace WijsOef.Controllers
{
    public class OfficeController : BaseController
    {
        private const int RadiusInKm = 15;

        protected IOfficeService OfficeService { get; set; }
        public OfficeController()
        {
            OfficeService = new OfficeService(new OfficeRepository());
        }

        public ActionResult Index(string city,bool? hasSupportDesk,bool? isOpenWeekend)
        {
            return View(new OfficeSearchModel() {City=city, HasSupportDesk=hasSupportDesk?? false, IsOpenWeekend=isOpenWeekend??false });
        }

        public ActionResult GetNearestOffices(double latitude, double longitude, bool? isOpenInWeekends, bool? isWithSupportDesk)
        {
            if (OfficeService == null) throw new ArgumentNullException("OfficeService is null");

            var offices = OfficeService.GetNearestOffices(new Data.Domain.OfficeQuery()
            { 
                                                            Latitude = latitude, 
                                                            Longitude = longitude,
                                                            IsOpenInWeekends = isOpenInWeekends ?? false,
                                                            IsWithSupportDesk = isWithSupportDesk ?? false,
                                                            Radius = RadiusInKm
            });

            return Json( new JsonObject<List<OfficeDistanceDto>>()  {Success=true, Result=offices.ConvertToJson().ToList()},  JsonRequestBehavior.AllowGet);
        }
    }
}
