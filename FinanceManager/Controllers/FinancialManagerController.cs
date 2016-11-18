using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinanceManager.Controllers
{
    public partial class FinancialManagerController : Controller
    {
        // GET: FinancialManager
        public virtual ActionResult Index()
        {
            return View("ManageView");
        }
        public virtual ActionResult ManageAdd()
        {
            return View("AddAmontView");
        }
    }
}