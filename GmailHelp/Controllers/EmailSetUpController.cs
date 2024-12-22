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











































































































































//using GmailHelp.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Mail;
//using System.Net.Mime;
//using System.Net;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Web;
//using System.Web.Mvc;
//using System.IO;
//using OfficeOpenXml; // For reading Excel files
//using iTextSharp.text.pdf; // For reading PDF files
//using iTextSharp.text.pdf.parser; // For extracting text from PDFs

//namespace GmailHelp.Controllers
//{
//    public class EmailSetUpController : Controller
//    {
//        MailDBContext dc = new MailDBContext();

//        #region Index - Email Home View
//        public ActionResult Index()
//        {
//            return View();
//        }
//        #endregion

//        #region Send Email - Single Email by AJAX
//        [HttpPost]
//        public JsonResult SendSingleEmail(int emailIndex)
//        {
//            var emailList = dc.gmail.ToList();

//            if (emailIndex < emailList.Count)
//            {
//                var email = emailList[emailIndex];
//                try
//                {
//                    SendMail(email.PName, email.CompanyName, email.Subject, email.To);
//                    return Json(new { success = true, message = $"Email sent successfully to the address: {email.To}" });
//                }
//                catch (Exception ex)
//                {
//                    return Json(new { success = false, message = $"Error sending email to {email.To}: {ex.Message}" });
//                }
//            }
//            else
//            {
//                return Json(new { success = false, message = "No more emails to send." });
//            }
//        }
//        #endregion

//        #region SendMail Method
//        public void SendMail(string pname, string companyName, string subject, string to)
//        {
//            StringBuilder sb = new StringBuilder();
//            sb.Append($"Hi {pname},\r\n\r\n");
//            sb.Append($"My name is Vipin Kumar, and I'm a .NET developer. I saw your post about the .NET Developer role at {companyName}.\r\n\r\n");
//            sb.Append("Thank you for considering my application.\r\n\r\nKind regards,\r\nVipin Kumar\r\n");
//            sb.Append("Email: kumarvipin897946@gmail.com\r\nPhone: 8979468090");

//            MailMessage mm = new MailMessage("kumarvipin897946@gmail.com", to)
//            {
//                Subject = subject,
//                Body = sb.ToString(),
//                IsBodyHtml = false
//            };

//            string pdfFilePath = @"D:\vipin ssd Backup\SSD BackUp\Desktop\vipin resume\Vipin Resume 1.9.pdf";
//            Attachment attachment = new Attachment(pdfFilePath, MediaTypeNames.Application.Pdf);
//            mm.Attachments.Add(attachment);

//            SmtpClient smtp = new SmtpClient
//            {
//                Host = "smtp.gmail.com",
//                Port = 587,
//                EnableSsl = true,
//                UseDefaultCredentials = false,
//                Credentials = new NetworkCredential("kumarvipin897946@gmail.com", "qinn tcbf wccc irkl") // Use App Password
//            };

//            smtp.Send(mm);
//        }
//        #endregion

//        #region Upload File and Send Emails
//        [HttpPost]
//        public JsonResult UploadFileAndSendEmails()
//        {
//            try
//            {
//                if (Request.Files.Count > 0)
//                {
//                    var file = Request.Files[0];

//                    if (file != null && (file.ContentType.Contains("xlxs") || file.ContentType.Contains(" pdf")))
//                    {
//                        var emailList = new List<string>();

//                        if (file.ContentType.Contains("excel"))
//                        {
//                            // Read Excel File
//                            using (var package = new ExcelPackage(file.InputStream))
//                            {
//                                var worksheet = package.Workbook.Worksheets.FirstOrDefault();
//                                if (worksheet != null)
//                                {
//                                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
//                                    {
//                                        var email = worksheet.Cells[row, 1].Text; // Assuming emails are in the first column
//                                        if (!string.IsNullOrEmpty(email))
//                                        {
//                                            emailList.Add(email);
//                                        }
//                                    }
//                                }
//                            }
//                        }
//                        else if (file.ContentType.Contains("pdf"))
//                        {
//                            // Read PDF File
//                            using (PdfReader reader = new PdfReader(file.InputStream))
//                            {
//                                for (int i = 1; i <= reader.NumberOfPages; i++)
//                                {
//                                    string text = PdfTextExtractor.GetTextFromPage(reader, i);
//                                    emailList.AddRange(ExtractEmailsFromText(text));
//                                }
//                            }
//                        }

//                        foreach (var email in emailList)
//                        {
//                            SendMail("Vipin", "CompanyName", "Job Opportunity", email);
//                        }

//                        return Json(new { success = true, message = $"{emailList.Count} emails sent successfully." });
//                    }
//                    else
//                    {
//                        return Json(new { success = false, message = "Invalid file format. Please upload an Excel or PDF file." });
//                    }
//                }
//                else
//                {
//                    return Json(new { success = false, message = "No file uploaded." });
//                }
//            }
//            catch (Exception ex)
//            {
//                return Json(new { success = false, message = $"Error: {ex.Message}" });
//            }
//        }
//        #endregion

//        #region Extract Emails from Text
//        private List<string> ExtractEmailsFromText(string text)
//        {
//            var emailList = new List<string>();
//            var emailPattern = @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}";

//            foreach (Match match in Regex.Matches(text, emailPattern))
//            {
//                emailList.Add(match.Value);
//            }

//            return emailList;
//        }
//        #endregion
//    }
//}
