using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using Twilio.Mvc;
using Twilio.TwiML;
//using Twilio.TwiML.Mvc;
using Twilio.AspNet.Common;
using Twilio.AspNet.Mvc;
//using Twilio.Mvc;
using Twilio.TwiML;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using MessageApplication.Models;
//using Twilio.TwiML.Mvc;

namespace MessageApplication.Controllers
{
    public class SmsController : TwilioController
    {
        [HttpGet]
        public ActionResult Index1(SmsRequest request)
        {
            try
            {
                string ResponseAnswer = null;
                QnAClass answer=MakeRequest("What packages do you offer?");
                var ans=answer.answers.Select(l => l.answer);
                foreach(var answerData in answer.answers)
                {
                    ResponseAnswer = answerData.answer;
                }

                if (ResponseAnswer != null)
                {
                    //request.Body = "Hi";
                    //string Path = System.Web.HttpContext.Current.Server.MapPath("~") + @"\Jsons\response.txt";
                    //using (System.IO.StreamWriter file =
                    //  new System.IO.StreamWriter(Path, true))
                    //{
                    //    file.WriteLine("Fourth line");
                    //}
                    //if (request.Body != null)
                    //{
                    //    string responseMessage = null;
                    //    string jsonString = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~") + @"\Jsons\ResponseMessage.json");

                    //    JObject objResponse = JObject.Parse(jsonString);
                    //    if (objResponse != null)
                    //    {
                    //        string body = request.Body.ToLower();
                    //        responseMessage = (string)objResponse[body];
                    //    }

                        //if (responseMessage != null)
                          //  if (request.Body.ToLower() == responseMessage.ToLower())
                          //  {
                                //response.Message(responseMessage);
                          //  }
                    //}
                }
            }
            catch(Exception ex)
            {
                string Path = System.Web.HttpContext.Current.Server.MapPath("~") + @"\Jsons\response.txt";
                using (System.IO.StreamWriter file =
                  new System.IO.StreamWriter(Path, true))
                {
                    file.WriteLine("Exception");
                }
            }
            return View();
        }

            [HttpPost]
        public ActionResult Index(SmsRequest request)
        {
            if (request != null)
            {
                string Path = System.Web.HttpContext.Current.Server.MapPath("~") + @"\Jsons\response.txt";
                using (System.IO.StreamWriter file =
                  new System.IO.StreamWriter(Path, true))
                {
                    file.WriteLine("Inside Fourth line");
                    file.WriteLine(request.AccountSid);
                    file.WriteLine(request.Body);
                    file.WriteLine(request.From);
                    file.WriteLine(request.FromCity);
                    file.WriteLine(request.MessageStatus);
                    file.WriteLine(request.MessagingServiceSid);
                    file.WriteLine(request.SmsSid);
                    file.WriteLine(request.To);
                    file.WriteLine(request.ToCity);
                    file.WriteLine(request.FromCountry);
                    file.WriteLine(request.ToCity);
                    file.WriteLine(request.ToCountry);
                    file.WriteLine(request.ToZip);
                    file.WriteLine(request.SmsSid);
                }
            }

            
            var response = new MessagingResponse();
            if(request!=null)
            {                
                //var responseData =request.AccountSid;
                //responseData += responseData + "   body  " + request.Body;//Fetch Data
                //string jsonData = JsonConvert.SerializeObject(responseData, Formatting.None);
                //System.IO.File.WriteAllText(Server.MapPath("jsondata.txt"), jsonData);
                if (request.Body!=null)
                {
                    string responseMessage = null;
                    //string jsonString = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~") + @"\Jsons\ResponseMessage.json");
                    string ResponseAnswer = null;
                    QnAClass answer = MakeRequest(request.Body);
                    var ans = answer.answers.Select(l => l.answer);
                    foreach (var answerData in answer.answers)
                    {
                        ResponseAnswer = answerData.answer;
                    }
                    //JObject objResponse = JObject.Parse(jsonString);
                    //if (objResponse != null)
                    //{
                    //    string body = request.Body.ToLower();
                    //    responseMessage = (string)objResponse[body];
                    //}

                    if (ResponseAnswer != null) {                        
                            response.Message(ResponseAnswer);
                      }
                }
            }
            //response.Message("Thanks for replying");
            Session["ResId"] = request.AccountSid;
            Session["ResBody"] = request.Body;
            Session["ToNumber"] = request.To;
            //response.Message("Thanks for your reply... from INextLabs. !");
            return TwiML(response);//TwiML();//response
            //return View();
        }
        static QnAClass MakeRequest(string body)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "b7bda5a33c6c4481a69df4490648d2f3");

            //var uri = "https://westus.api.cognitive.microsoft.com/qnamaker/v4.0/knowledgebases/create?" + queryString;
            var uri = "https://inlqna.cognitiveservices.azure.com/qnamaker/v5.0-preview.1/knowledgebases/e56d2430-40da-4fc1-b58a-49e2f284cc11/generateAnswer";
            HttpResponseMessage response;
            string jsonString = System.IO.File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~") + @"\Jsons\Content.json");
            jsonString = jsonString.Replace("$body$",body);
            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes(jsonString);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = client.PostAsync(uri, content).Result;
                if (response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    string res = response.Content.ReadAsStringAsync().Result;
                    QnAClass AnswerResponse = JsonConvert.DeserializeObject<QnAClass>(res);
                    if (AnswerResponse != null)
                        return AnswerResponse;
                    else
                        return new QnAClass();
                }
                else
                {
                    System.Threading.Tasks.Task<string> errorMessage = response.Content.ReadAsStringAsync();
                    return new QnAClass();
                }
                //HttpResponseMessage 
                //HttpMethod method = new HttpMethod("POST");
                //HttpRequestMessage request123 = new HttpRequestMessage(method, uri) { Content = content };
                //response = client.SendAsync(request123).Result;
            }

        }
        // GET: Sms
        public ActionResult Index()
        {
            return View();
        }
    }
}