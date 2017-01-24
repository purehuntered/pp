using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace WebPremPar.Views.Home
{
    public class UploadController : Controller
    {
        bool val1 = (System.Web.HttpContext.Current.User != null) && (System.Web.HttpContext.Current.User.Identity.IsAuthenticated);
        // GET: Upload
        public ActionResult Index()
        {
            
            if (val1)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (val1)
            {
                try
                {
                    if (file != null)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                        file.SaveAs(path);
                        var u = new Upload();
                        u.ID = Guid.NewGuid();                        
                        u.BrandName = Request.Params["Brand"].ToString();
                        u.AssetType = Request.Params["AssetType"].ToString();
                        u.ClickURL = Request.Params["ClickURL"].ToString();                        
                        u.UserLog = System.Web.HttpContext.Current.User.Identity.Name;
                        u.StartDT = DateTime.Now;
                        using (var context = new Premier_PartnerEntities())
                        {
                            context.Uploads.Add(u);
                            context.SaveChanges();
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Upload");
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            return View();

        }
    }
}