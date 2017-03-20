using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Configuration;

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
                        if (IsFileSupported(file))
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                            DateTime startDt = Convert.ToDateTime(Request.Params["StartDt"].ToString());
                            DateTime endDt = Convert.ToDateTime(Request.Params["EndDt"].ToString());
                            file.SaveAs(path);
                            var u = new Upload();
                            u.ID = Guid.NewGuid();
                            u.BrandName = Request.Params["Brand"].ToString();
                            u.AssetType = Request.Params["AssetType"].ToString();
                            u.ClickURL = Request.Params["ClickURL"].ToString();
                            u.UserLog = System.Web.HttpContext.Current.User.Identity.Name;
                            u.StartDT = Convert.ToDateTime( Request.Params["StartDt"].ToString() );
                            u.EndDT = Convert.ToDateTime( Request.Params["EndDt"].ToString() );
                            //if (CompareDt(startDt, endDt) )
                            //{
                            //    Session["errMsg"] = "10";
                            //    return RedirectToAction("Index", "Upload");
                            //}
                            u.FileName = fileName;
                            if ( Request.Params["txtCopy"] != null )
                            {
                                u.CopyBody = Request.Params["txtCopy"].ToString();
                            }
                            if (Request.Params["txtHL"] != null)
                            {
                                u.CopyHL = Request.Params["txtHL"].ToString();
                            }
                            
                            using (var context = new Premier_PartnerEntities())
                            {
                                context.Uploads.Add(u);
                                context.SaveChanges();
                            }
                            SetEmailVnd(u,fileName, User.Identity.Name);
                            SetEmailAdmins(u, fileName);
                        }
                        else
                        {
                            Session["errMsg"] = "10";
                            return RedirectToAction("Index", "Upload");
                        }
                    }
                    else
                    {
                        Session["errMsg"] = "10";
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

        private bool CompareDt(DateTime date1, DateTime date2)
        {
            int result = DateTime.Compare(date1, date2);            

            if (result > 0)
                return true;

            return true;
        }

        private void SetEmailVnd(Upload u,string File, string Email)
        {
            var mymessage = new IdentityMessage();
            mymessage.Body = "Your submission has been received.<br /><br />";
            mymessage.Body += "Brand Name: " + u.BrandName + "<br />";
            mymessage.Body += "Asset Type: " + u.AssetType + "<br />";
            mymessage.Body += "Start Date: " + u.StartDT.ToString() + "<br />";
            mymessage.Body += "End Date: " + u.EndDT.ToString() + "<br />";
            mymessage.Body += "Click Through URL: " + u.ClickURL + "<br />";
            mymessage.Body += "File Name: " + File + "<br />";
            mymessage.Destination = Email;
            mymessage.Subject = "Premier Partner";


            EmailResults(mymessage);
        }

        private void SetEmailAdmins(Upload u, string File)
        {
            var mymessage = new IdentityMessage();
            mymessage.Body = "New submission received.<br /><br />";
            mymessage.Body += "Brand Name: " + u.BrandName + "<br />";
            mymessage.Body += "Asset Type: " + u.AssetType + "<br />";
            mymessage.Body += "Start Date: " + u.StartDT.ToString() + "<br />";
            mymessage.Body += "End Date: " + u.EndDT.ToString() + "<br />";
            mymessage.Body += "Click Through URL: " + u.ClickURL + "<br />";
            mymessage.Body += "File Name: " + File + "<br />";
            //mymessage.Destination = "ade.olaibi@purered.net,Lindy.Beckhe@purered.net,Shannon.Chapman@purered.net,Kati.Budnik@purered.net,Cody.Mohon@purered.net";
            
            mymessage.Destination = ConfigurationManager.AppSettings["admins"].ToString();
            mymessage.Subject = "Premier Partner New Submission";


            EmailResults(mymessage);
        }

        private void EmailResults(IdentityMessage message)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient("172.16.0.115", 25);
            mail.To.Add(message.Destination);
            mail.From = new System.Net.Mail.MailAddress("IvieImporter@purered.net");            
            mail.IsBodyHtml = true;

            System.Net.Mail.AlternateView htmlView =
            System.Net.Mail.AlternateView.CreateAlternateViewFromString
            (
                message.Body,
                System.Text.Encoding.UTF8,
                "text/html"
            );
            try
            {
                mail.AlternateViews.Add(htmlView);
                SmtpServer.Send(mail);                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsFileSupported(HttpPostedFileBase file)
        {
            var isSupported = false;

            switch (file.ContentType)
            {

                case ("application/octet-stream"):
                    isSupported = true;
                    break;

                case ("application/postscript"):
                    isSupported = true;
                    break;

            }

            return isSupported;
        }
        public ActionResult ThankYou()
        {
            return View();
        }
    }
   
}