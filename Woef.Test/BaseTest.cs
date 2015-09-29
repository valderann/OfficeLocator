using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WijsOef.Data;
using WijsOef.Data.Repositories;

namespace WijsOef.Test
{
    public class BaseTest
    {
        protected IOfficeRepository OfficeRepository{get;set;}
        public BaseTest()
        {
            OfficeRepository = new OfficeRepository();
        }
    }
}
