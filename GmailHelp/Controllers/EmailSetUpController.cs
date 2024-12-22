using GmailHelp.Models;
using System;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace GmailHelp.Controllers
{
    public class EmailSetUpController : Controller
    {
        MailDBContext dc = new MailDBContext();

        #region Index - Email Home View
        public ActionResult Index()
        {
            return View();
        }
        #endregion
        static int count = 0;
        #region Send Email - Single Email by AJAX
        [HttpPost]

        public JsonResult SendSingleEmail(int emailIndex)
        {
            var emailList = dc.gmail.ToList();

           
            if (emailIndex < emailList.Count)
            {
                var email = emailList[emailIndex];
                try
                {
                    SendMail(email.PName, email.CompanyName, email.Subject, email.To);
                    count++;
                    return Json(new { success = true, message = $"Email sent Success to the Mail Address        =>>          {email.To}  and Count =>> {count}" });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = $"Error sending email to {email.To}: {ex.Message}" });
                }
            }
            else
            {
                return Json(new { success = false, message = "No more emails to send." });
            }
        }
        #endregion

        #region SendMail Method
        public void SendMail(string pname, string companyName, string subject, string to)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Hi {pname},\r\n\r\nMy name is Vipin Kumar, and I am a Full Stack .NET Developer. I am seeking an entry-level position as a .NET Developer in {companyName}. and am very interested in this position because I have experience with the skills required for this role.\r\n\r\nI believe I can contribute effectively to the team with my skills, which include:\r\n\r\n\r\nProgramming Languages: C#, Type-Script\r\nFrameworks: ASP.NET MVC, ASP.NET Web API, ASP.NET Core (MVC, Razor Pages, Web API)\r\nORM: Entity Framework, Entity Framework Core\r\nDatabase Management: Microsoft SQL Server\r\nFrontend: Angular\r\n\r\n\r\nThank you for considering my application. Please let me know if you have any questions or if you’d like to review my resume.\r\n\r\nI understand you may be busy, and it is okay if you cannot respond immediately. I will follow up in three days to ensure you have received my message.\r\n\r\nThank you.\r\n\r\nKind regards,\r\nVipin Kumar\r\nEmail: kumarvipin897946@gmail.com\r\nPhone: 8979468090");
            
            MailMessage mm = new MailMessage("kumarvipin897946@gmail.com", to)
            {
                Subject = $"Hi {pname} this Application for .NET Developer Position",
                Body = sb.ToString(),
                IsBodyHtml = false
            };

            string pdfFilePath = @"D:\vipin ssd Backup\SSD BackUp\Desktop\vipin resume\Vipin Resume 1.9.pdf";
            Attachment attachment = new Attachment(pdfFilePath, MediaTypeNames.Application.Pdf);
            mm.Attachments.Add(attachment);

            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("/*Enter your Email here*/", "/*Write your gmail password*/") // Use App Password
            };

            smtp.Send(mm);
        }
        #endregion
    }
}






