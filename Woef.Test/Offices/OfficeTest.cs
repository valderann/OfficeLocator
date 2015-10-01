using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WijsOef.Test
{
    [TestClass]
    public class OfficeTest:BaseTest
    {
        [TestMethod]
        public void GetOfficesTest()
        {
           var results=OfficeService.GetNearestOffices(new WijsOef.Data.Domain.OfficeQuery() {Latitude = 2 , Longitude = 5, Radius=20 });
           Assert.IsTrue(results.Count==2);
        }
    }
}
