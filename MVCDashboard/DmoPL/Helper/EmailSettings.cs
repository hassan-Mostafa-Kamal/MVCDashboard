using DemoDAL.Models;
using System.Net;
using System.Net.Mail;

namespace DmoPL.Helper
{
	public static class EmailSettings
	{
		public static void SendEmail(Email email)
		{
			//var client = new SmtpClient("linkdev.com", 444);
			//client.EnableSsl= true;
			//client.Credentials = new NetworkCredential("email@linkdev.com", "password");
			//client.Send("email@linkdev.com", email.To, email.Subject, email.Body);
			var client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl= true;
			client.Credentials = new NetworkCredential("khaledelnassag@gmail.com", "tnvtmvqczntkoqwl");
			client.Send("khaledelnassag@gmail.com", email.To, email.Subject, email.Body);
		}
	}
}
