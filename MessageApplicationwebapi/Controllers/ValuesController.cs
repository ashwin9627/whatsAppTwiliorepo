using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Twilio.AspNet.Common;
using Twilio.AspNet.Mvc;
//using Twilio.Mvc;
using Twilio.TwiML;
//using Twilio.TwiML.Mvc;

namespace MessageApplicationwebapi.Controllers
{
    public class ValuesController : TwilioController//ApiController
    {
        //public class SmsController : TwilioController
        //{
            [HttpPost]
            public ActionResult Index(SmsRequest request)
            {
                var response = new TwilioResponse();
                response.Message("The Robots are coming! Head for the hills!");
                return TwiML(response);
            }
        //}
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }
        public IHttpActionResult ReceiveMessage()
        {
            //string requestData = Request.Content.ReadAsStringAsync().Result.ToString();//.Replace("<", "").Replace(">", "");
            return Ok();
        }
        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
