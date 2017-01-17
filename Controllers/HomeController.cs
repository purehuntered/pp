using System.Web.Mvc;

namespace IdentitySample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Upload()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && (System.Web.HttpContext.Current.User.Identity.IsAuthenticated);
            if (val1)
            {
                return View();
            }
            else
            {
                return View("Index");
            }


            
        }   
             
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
