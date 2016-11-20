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
                return HttpContext.Current.Session["DateFromIncoming"] as DateTime?;
            }
            set
            {
                HttpContext.Current.Session["DateFromIncoming"] = value;

            }
        }
        public static DateTime? DateToIncoming
        {
            get
            {
                return HttpContext.Current.Session["DateToIncoming"] as DateTime?;
            }
            set
            {
                HttpContext.Current.Session["DateToIncoming"] = value;

            }
        }
        public static DateTime? DateFromOutgoing
        {
            get
            {
                return HttpContext.Current.Session["DateFromOutgoing"] as DateTime?;
            }
            set
            {
                HttpContext.Current.Session["DateFromOutgoing"] = value;
            }
        }
        public static DateTime? DateToOutgoing
        {
            get
            {
                return HttpContext.Current.Session["DateToOutgoing"] as DateTime?;
            }
            set
            {
                HttpContext.Current.Session["DateToOutgoing"] = value;
            }
        }

        public static string LastRememberView
        {
            get
            {
                return HttpContext.Current.Session["LastRememberView"] as string;

            }
            set
            {
                HttpContext.Current.Session["LastRememberView"] = value;

            }
        }
    }
}