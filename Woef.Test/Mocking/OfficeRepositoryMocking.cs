using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WijsOef.Data.Repositories;

namespace WijsOef.Test.Mocking
{
    public class OfficeRepositoryMocking:IOfficeRepository
    {

        public IList<Data.Data.office> GetNearestOffices(Data.Domain.OfficeQuery qry)
        {
            return new List<Data.Data.office>(){
                new Data.Data.office(){  id=1,city="kortrijk", street="teststraat 1", latitude=2, longitude=5, is_open_in_weekends="N", has_support_desk="N"},
                new Data.Data.office(){  id=1,city="kortrijk", street="teststraat 2", latitude=2.1, longitude=5, is_open_in_weekends="N", has_support_desk="N"},
                new Data.Data.office(){  id=1,city="new york", street="teststraat 1", latitude=30, longitude=35, is_open_in_weekends="N", has_support_desk="N"}
            };
        }
    }
}
