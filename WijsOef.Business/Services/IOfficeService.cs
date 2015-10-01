using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WijsOef.Bussiness.Domain;
using WijsOef.Data.Domain;

namespace WijsOef.Bussiness.Services
{
    public interface IOfficeService
    {
        IList<OfficeBusinessDistance> GetNearestOffices(OfficeQuery qry);
    }
}
