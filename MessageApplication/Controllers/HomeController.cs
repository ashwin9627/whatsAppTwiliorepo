using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace MessageApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                TwilioClient.Init(
                    "AC395c02632d9b922ccf62ca9a445c020b", "926e04e51ec4957fe2ccd94aff126bbe");

                var message = MessageResource.Create(
                    from: "whatsapp:+14155238886",
                    to:"whatsapp:+918695572146",
                    body: "Test message");
                Session["Message"] = message;
            }
            catch(Exception ex)
            {

            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            //+6597509747
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}