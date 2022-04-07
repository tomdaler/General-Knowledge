using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Task
{
    class SendEmails
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static void SendEmail(string subject, string body, string sender)
        {
            SmtpClient client = null;
            MailMessage message = null;

            try
            {
                client = new SmtpClient(ConfigurationManager.AppSettings["SmtpServer"].ToString());
                client.UseDefaultCredentials = false;

                message = new MailMessage();
                message.From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"].ToString());
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                //string input = ConfigurationManager.AppSettings["EmailTo"].ToString();
               // log.Info("Sending to" + input);

                if (sender == "")
                    sender = ConfigurationManager.AppSettings["EmailTo"].ToString();

                log.Info("Sending to" + sender);
                string[] to = sender.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var address in to)
                {
                    if (!string.IsNullOrEmpty(address)) message.To.Add(new MailAddress(address));
                }

                client.Send(message);
            }
            catch (SmtpException smtpex)
            {
                log.Info("smtp error " + smtpex.ToString());
                throw smtpex;
            }
            catch (Exception ex)
            {
                log.Info(ex.ToString());
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
    }
}
