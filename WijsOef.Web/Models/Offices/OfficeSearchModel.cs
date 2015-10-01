using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WijsOef.Models
{
    public class OfficeSearchModel
    {
       public string City {get;set;}
       public bool HasSupportDesk {get;set;}
       public bool IsOpenWeekend { get; set; }
    }
}