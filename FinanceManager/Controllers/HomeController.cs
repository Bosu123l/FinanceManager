using System.Web.Mvc;

namespace FinanceManager.Controllers
{
    [Authorize]
    public partial class HomeController : Controller
    {
        public HomeController()
        {
        }

        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public virtual ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}