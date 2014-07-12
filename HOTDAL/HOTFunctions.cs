using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using HOTDAL;

namespace HOTDAL
{
    public class HOTFunctions
    {
        public static void SendErrorMail(string ErrorClass, string ErrorStack, string ErrorMessage, string ErrorSQL)
        {
            MailMessage objMessage = new MailMessage();
            SmtpClient smtp = new SmtpClient("mail.hottropicaltans.com");

            objMessage.Subject = "Problem in:" + ErrorClass;
            objMessage.From = new MailAddress("hotproblems@hottropicaltans.com");
            objMessage.To.Add("HOTTans@hottropicaltans.com");
            objMessage.Body = "<b>SQL:</b>" + ErrorSQL + "<br><b>Message:</b>" + ErrorMessage + "<br><b>StackTrace:</b>" + ErrorStack;
            objMessage.IsBodyHtml = true;
            smtp.Credentials = new NetworkCredential("hotproblems@hottropicaltans.com", "H0tTans.");
            smtp.Send(objMessage);
        }
    }
}