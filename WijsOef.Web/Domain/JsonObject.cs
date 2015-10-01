using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WijsOef.Models
{
    public class JsonObject<T> where T:new()
    {
        public bool Success { get; set; }
        public int ErrorCode { get; set; }
        public T Result { get; set; }
    }
}