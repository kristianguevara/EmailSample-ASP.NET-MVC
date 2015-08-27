using EmailSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EmailSample.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Index(EmailModel model)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();

                message.To.Add(new MailAddress(model.receiver));  // Receiver email here 
                //message.To.Add(new MailAddress("one@gmail.com")); //Add more to CC people
                //message.To.Add(new MailAddress("two@gmail.com")); //Add more to CC people
                // message.To.Add(new MailAddress("three@gmail.com")); //Add more to CC people
                //message.Attachments.Add(new Attachment(HttpContext.Server.MapPath("~/App_Data/Test.docx"))); //Add attachment

                message.From = new MailAddress(model.sender);  // Sender email here
                message.Subject = model.subject;
                message.Body = string.Format(body, model.sender, model.receiver, model.message);
                message.IsBodyHtml = true; //This will render html tags sa if your coding html

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        /*A mailer account would be recommended here. Do not use your personal account unless its*/
                        UserName = "",  // Enter your email address here. This will be the sender
                        Password = ""  // Enter the password of your email address here.
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp-mail.outlook.com"; //Use smtp.gmail.com for gmail accounts
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    ViewBag.Message = "Message Sent!";
                    //return RedirectToAction("Index");
                }
            }
            return View(model);
        }
    }
}