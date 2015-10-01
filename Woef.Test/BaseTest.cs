using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WijsOef.Bussiness;
using WijsOef.Bussiness.Services;
using WijsOef.Data;
using WijsOef.Data.Repositories;
using WijsOef.Test.Mocking;

namespace WijsOef.Test
{
    public class BaseTest
    {
        protected IOfficeService OfficeService{get;set;}
        public BaseTest()
        {
            OfficeService = new OfficeService(new OfficeRepositoryMocking());
        }
    }
}
