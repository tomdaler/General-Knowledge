using log4net;
using System;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Reflection;

namespace Reports
{
   
    public class Program
    {
        private  static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static void SendEmail(string file1, string subject)
        {
            SmtpClient client = null;
            MailMessage message = null;

            try
            {
                client = new SmtpClient(ConfigurationManager.AppSettings["SmtpServer"].ToString());
                client.UseDefaultCredentials = false;
                
                message = new MailMessage();
                message.From = new MailAddress(ConfigurationManager.AppSettings["From"].ToString());
                message.Subject = subject;
                message.Body = "No data for today.";
                message.IsBodyHtml = true;

                if (file1 != "")
                {
                    System.Net.Mail.Attachment attachment;
                    attachment = new System.Net.Mail.Attachment(file1);
                    message.Attachments.Add(attachment);
                    message.Body = ".";
                }

                string sendto = ConfigurationManager.AppSettings["To"].ToString();
                string[] to = sendto.Split(';');

                foreach (var word in to)
                {
                    message.To.Add(new MailAddress(word));
                }

                client.Send(message);
                log.Info("Sent");
            }
            catch (SmtpException smtpex)
            {
                log.Info("Error " + smtpex.ToString());
                throw smtpex;
            }
            catch (Exception ex)
            {
                log.Info("Error "+ex.ToString());
                throw ex;
            }
            finally
            {
                if (message != null)
                    message.Dispose();

                if (client != null)
                    client.Dispose();
            }
        }


        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            DateTime fec = DateTime.Now.AddDays(1);

            string file1 = fec.ToString("MM-dd-yyyy");
            //file1 = file1.Replace("-", "");
            file1 = "COVID19 Case Report - " + file1+".xlsx";

            Exposure.Program common = new Exposure.Program();

            string where = Directory.GetCurrentDirectory()+"\\Temp\\"+ file1;

            if (where.IndexOf("Window")>1)
            {
                where = @"E:\Tasks\ExposureReport\Temp\" + file1;
            }

            log.Info("1.0 "+file1);

            try
            {

                if (!common.Generate(3, where,"","", true))
                {
                    log.Info("Empty");
                    SendEmail("", file1);
                }
                else
                    SendEmail(where, file1);
            }
            catch(Exception ex)
            {
                log.Info(ex.ToString());
            }
        }
    }
}
