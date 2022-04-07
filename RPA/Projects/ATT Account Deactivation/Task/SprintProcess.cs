using log4net;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;


namespace Task
{
    public class SprintProcess
    {
        private static ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public void DownloadAttachment(EmailMessage email, string downloadpath)
        {
            try
            {
                log.Info("DownloadAttachment1 " + email.Attachments.Count.ToString());

                for (int i = 0; i < email.Attachments.Count; i++)
                {

                    Microsoft.Exchange.WebServices.Data.Attachment emailAttach = email.Attachments[i];
                    if (emailAttach is FileAttachment)
                    {

                        string file = Path.Combine(downloadpath, emailAttach.Name);
                        log.Info("Download 1 ");
                        log.Info(i.ToString() + "  FILE " + file);

                        if (file.IndexOf(".") < 0)
                        {
                            //EmailNotify("Sprint Back Office CCR Interval Report - Invalid Attachment", "Check issue of email subject: " + email.Subject);
                            SendEmails.SendEmail("Sprint", "Wrong file " + file,"");
                            continue;
                        }

                        string extension = Path.GetExtension(file);
                        if (extension.ToUpper() != ".CSV")
                        {
                            //send email, invalid attachment
                            EmailNotify("Sprint Back Office CCR Interval Report - Invalid Attachment", "Check issue of email subject: " + email.Subject);
                        }
                        else
                        {
                            //delete existing file
                            System.IO.File.Delete(file);
                            //download file

                            ((FileAttachment)emailAttach).Load(file);
                            log.Info("Downloaded " + file);
                        }
                    }
                }

                if (email.Attachments.Count == 0)
                {
                    //send email, skip download
                    EmailNotify("Sprint Back Office CCR Interval Report - No Attachment", "Check issue of email subject: " + email.Subject);
                }
            }
            catch (Exception ex)
            {
                SendEmails.SendEmail("Sprint", ex.ToString(),"");

                throw ex;
            }
        }

        public void ProcessAttachment(string downloadpath, string completedpath, string processingpath, string errorpath)
        {
            try
            {
                string[] fileEntries = Directory.GetFiles(downloadpath);

                //log.Info("DownloadAttachmentfileEntries" + fileEntries.Count().ToString());

                foreach (string sourcefile in fileEntries)
                {
                    string fullfilename = Path.GetFileName(sourcefile);
                    string filename = Path.GetFileNameWithoutExtension(sourcefile);
                    string extension = Path.GetExtension(sourcefile);

                    log.Info("Download 2   " + fullfilename + " " + filename + " " + extension);

                    string errorfile = Path.Combine(errorpath, fullfilename);

                    if (extension.ToUpper() == ".CSV")
                    {
                        Boolean validfile = false;
                        String newfilename = string.Empty;

                        //parse filename
                        //log.Info("Parse");

                        List<string> filenameList = filename.Split('_').ToList();

                        string interval = string.Empty;
                        string date = string.Empty;
                        if (filenameList.Count > 1)
                        {
                            interval = filenameList[1].ToString(); //interval @ 2nd position
                        }
                        if (filenameList.Count > 3)
                        {
                            date = filenameList[3].ToString(); // date @ 4th position
                        }

                        log.Info("convert interval");
                        string fileinterval = ConvertInterval(interval);

                        if (interval != string.Empty && date != string.Empty)
                        {
                            newfilename = string.Concat("SPRINT_BORTCALLTYPE_", date, fileinterval, ".csv");
                            validfile = true;
                        }

                        if (validfile)
                        {
                            //log.Info("valid");
                            //save new updated file to processing path
                            string successfile = Path.Combine(processingpath, newfilename);
                            UpdateFileContent(sourcefile, successfile, date, fileinterval);

                            string completedfile = Path.Combine(completedpath, fullfilename);

                            //move attachment to completed path
                            System.IO.File.Delete(completedfile); //delete existing file
                            File.Move(sourcefile, completedfile);
                        }
                        else
                        {
                            //move to error path - incorrect filename
                            System.IO.File.Delete(errorfile); //delete existing file

                            File.Move(sourcefile, errorfile);
                            EmailNotify("Sprint Back Office CCR Interval Report - Incorrect filename", "Check error folder");
                        }
                    }
                    else
                    {
                        //move to error path       
                        System.IO.File.Delete(errorfile); //delete existing file
                        File.Move(sourcefile, errorfile);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Info("Error process attach 4");
                throw ex;
            }
        }


        //post processed attachment files to share and repository
        public void PostAttachment(string postingfolder, string destfolder, string post, string sharefolder)
        {
            try
            {

                string[] fileEntries = Directory.GetFiles(postingfolder);
                log.Info("PostAttachment FileEntries " + fileEntries.Count().ToString());

                foreach (string file in fileEntries)
                {
                    string filename = Path.GetFileName(file);

                    if (post == "1")
                    {
                        string sharefile = Path.Combine(sharefolder, filename);
                        File.Copy(file, sharefile, true);
                    }

                    string destffile = Path.Combine(destfolder, filename);
                    System.IO.File.Delete(destffile); //delete existing file
                    File.Move(file, destffile);
                }
            }
            catch (Exception ex)
            {
                log.Info("error PostAttchment "+ex.ToString());
                throw ex;
            }
          
        }

        //move attachments to repository
        public void MoveAttachment(string completedfolder, string destfolder)
        {
            try
            {
                log.Info("moveattachment");
                string[] fileEntries = Directory.GetFiles(completedfolder);
                foreach (string file in fileEntries)
                {
                    string filename = Path.GetFileName(file);

                    string destffile = Path.Combine(destfolder, filename);
                    System.IO.File.Delete(destffile); //delete existing file
                    File.Move(file, destffile);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //send email notifaction
        private void EmailNotify(string subject, string body)
        {
            SmtpClient client = null;
            MailMessage message = null;

            try
            {
                log.Info("EmailNotify");
                client = new SmtpClient(ConfigurationManager.AppSettings["SmtpServer"].ToString());
                client.UseDefaultCredentials = false;

                message = new MailMessage();
                message.From = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"].ToString());
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                string input = ConfigurationManager.AppSettings["SprintSendTo"].ToString();

                string[] to = input.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var address in to)
                {
                    log.Info(address);
                    if (!string.IsNullOrEmpty(address)) message.To.Add(new MailAddress(address));
                }

                client.Send(message);
            }
            catch (SmtpException smtpex)
            {
                throw smtpex;
            }
            catch (Exception ex)
            {
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

        private string ConvertInterval(string interval)
        {
            string retval;
            switch (interval)
            {
                case "0 AM": retval = "0000"; break;
                case "030 AM": retval = "0030"; break;
                case "100 AM": retval = "0100"; break;
                case "130 AM": retval = "0130"; break;
                case "200 AM": retval = "0200"; break;
                case "230 AM": retval = "0230"; break;
                case "300 AM": retval = "0300"; break;
                case "330 AM": retval = "0330"; break;
                case "400 AM": retval = "0400"; break;
                case "430 AM": retval = "0430"; break;
                case "500 AM": retval = "0500"; break;
                case "530 AM": retval = "0530"; break;
                case "600 AM": retval = "0600"; break;
                case "630 AM": retval = "0630"; break;
                case "700 AM": retval = "0700"; break;
                case "730 AM": retval = "0730"; break;
                case "800 AM": retval = "0800"; break;
                case "830 AM": retval = "0830"; break;
                case "900 AM": retval = "0900"; break;
                case "930 AM": retval = "0930"; break;
                case "1000 AM": retval = "1000"; break;
                case "1030 AM": retval = "1030"; break;
                case "1100 AM": retval = "1100"; break;
                case "1130 AM": retval = "1130"; break;
                case "1200 PM": retval = "1200"; break;
                case "1230 PM": retval = "1230"; break;
                case "100 PM": retval = "1300"; break;
                case "130 PM": retval = "1330"; break;
                case "200 PM": retval = "1400"; break;
                case "230 PM": retval = "1430"; break;
                case "300 PM": retval = "1500"; break;
                case "330 PM": retval = "1530"; break;
                case "400 PM": retval = "1600"; break;
                case "430 PM": retval = "1630"; break;
                case "500 PM": retval = "1700"; break;
                case "530 PM": retval = "1730"; break;
                case "600 PM": retval = "1800"; break;
                case "630 PM": retval = "1830"; break;
                case "700 PM": retval = "1900"; break;
                case "730 PM": retval = "1930"; break;
                case "800 PM": retval = "2000"; break;
                case "830 PM": retval = "2030"; break;
                case "900 PM": retval = "2100"; break;
                case "930 PM": retval = "2130"; break;
                case "1000 PM": retval = "2200"; break;
                case "1030 PM": retval = "2230"; break;
                case "1100 PM": retval = "2300"; break;
                case "1130 PM": retval = "2330"; break;
                default: retval = string.Empty; break;
            }
            return retval;
        }

        private void UpdateFileContent(string origfile, string newfile, string date, string interval)
        {
            try
            {
                log.Info("UpdateFileContent");
                string dtyear = date.Substring(0, 4);
                string dtmonth = date.Substring(4, 2);
                string dtday = date.Substring(6, 2);

                date = String.Concat(dtmonth, "/", dtday, "/", dtyear);
                interval = interval.Replace("AM", string.Empty);
                interval = interval.Replace("PM", string.Empty);
                interval = interval.Insert(2, ":");

                // #1 read csv file
                string[] csvdump = File.ReadAllLines(origfile);

                // #2 split data
                List<List<string>> csv = csvdump.Select(x => x.Split(',').ToList()).ToList();

                log.Info("csv.Count " + csv.Count.ToString());

                //#3 update data in 1st column
                for (int i = 0; i < csv.Count; i++)
                {
                    csv[i].Insert(0, i == 0 ? "CST Date" : date);
                }

                //#4 update data 2nd column
                for (int i = 0; i < csv.Count; i++)
                {
                    csv[i].Insert(1, i == 0 ? "CST Interval Start" : interval);
                }

                //#5 remove last row
                csv.RemoveAt(csv.Count - 1);

                //#6 write csv file
                File.WriteAllLines(newfile, csv.Select(x => string.Join(",", x)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
