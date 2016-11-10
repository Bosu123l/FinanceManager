using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceManager.Models
{
    public static class GlobalViariables
    {
        public static DateTime? DateFromIncoming
        {
            get
            {
                return HttpContext.Current.Application["DateFromIncoming"] as DateTime?;
            }
            set
            {
                HttpContext.Current.Application["DateFromIncoming"] = value;

            }
        }
        public static DateTime? DateToIncoming
        {
            get
            {
                return HttpContext.Current.Application["DateToIncoming"] as DateTime?;
            }
            set
            {
                HttpContext.Current.Application["DateToIncoming"] = value;

            }
        }
        public static DateTime? DateFromOutgoing
        {
            get
            {
                return HttpContext.Current.Application["DateFromOutgoing"] as DateTime?;
            }
            set
            {
                HttpContext.Current.Application["DateFromOutgoing"] = value;
            }
        }
        public static DateTime? DateToOutgoing
        {
            get
            {
                return HttpContext.Current.Application["DateToOutgoing"] as DateTime?;
            }
            set
            {
                HttpContext.Current.Application["DateToOutgoing"] = value;
            }
        }
    }
}