using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WijsOef.Data.Data;
using WijsOef.Data.Domain;

namespace WijsOef.Data.Repositories
{
    public interface IOfficeRepository
    {
        IList<office> GetNearestOffices(OfficeQuery qry);
    }
}
