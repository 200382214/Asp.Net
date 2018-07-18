using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Configuration;
using System.Net.Mail;
using System.Net;
//using System.Net.Configuration;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SmtpSection secObj = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

        using (MailMessage mm = new MailMessage())
        {
            mm.From = new MailAddress(secObj.From); //--- Email address of the sender
            mm.To.Add("emailAddressWhereYouWantToSendEmail"); //---- Email address of the recipient.
            mm.Subject = "We are trying to send email using SMTP settings."; //---- Subject of email.
            mm.Body = "Hello this is content of the email"; //---- Content of email.

            SmtpClient smtp = new SmtpClient();
            smtp.Host = secObj.Network.Host; //---- SMTP Host Details. 
            smtp.EnableSsl = secObj.Network.EnableSsl; //---- Specify whether host accepts SSL Connections or not.
            NetworkCredential NetworkCred = new NetworkCredential(secObj.Network.UserName, secObj.Network.Password);
            //---Your Email and password
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587; //---- SMTP Server port number. This varies from host to host. 
            smtp.Send(mm);
        }
    }
}