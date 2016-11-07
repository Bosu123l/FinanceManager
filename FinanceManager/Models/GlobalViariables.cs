using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceManager.Models
{
    public static class GlobalViariables
    {
        public static DateTime? DateFrom
        {
            get
            {
                return HttpContext.Current.Application["DateFrom"] as DateTime?;
            }
            set
            {
                HttpContext.Current.Application["DateFrom"] = value;

            }
        }
        public static DateTime? DateTo
        {
            get
            {
                return HttpContext.Current.Application["DateTo"] as DateTime?;
            }
            set
            {
                HttpContext.Current.Application["DateTo"] = value;

            }
        }
    }
}