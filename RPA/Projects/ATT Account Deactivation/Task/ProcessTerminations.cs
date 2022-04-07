using log4net;
using System;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Task
{
    static class ProcessTerminations
    {
        private static ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static string WhichReason(string subject, string body)
        {
            subject = subject.ToUpper();
            int pos1 = subject.IndexOf("EMPLOYEE MOVED");
            if (pos1 > 2) return "Transfer";

            if (body.IndexOf("Misconuct") > 0) return "Misconduct";
            return "Termination";
        }


        //if (who != "Tomas.Dale@convergys.com" && msg.Length>10) return msg;   
        public static string MainProcessTermination(string body, string dtSended, string sender, string subject, string System)
        {
            body = RemoveSpecialCharacters(body);
            string Reason2 = WhichReason(subject, body);

            string EmplID = GetEmplID(body);
            if (EmplID.Length > 11) return EmplID;

            // name, lastname
            int pos1 = body.IndexOf("Employee Name");
            if (pos1 < 1) return "The information was not added to sharepoint because the process was not able to parse name in email : " + body;
            string EmplName = body.Substring(pos1 + 14);
            EmplName = EmplName.Trim();

            pos1 = EmplName.IndexOf(" Client");

            if (pos1 < 1) return "The information was not added to sharepoint because the process was not able to parse name in email : " + body;
            EmplName = EmplName.Substring(0, pos1 - 1);
            EmplName = EmplName.Trim();

            pos1 = EmplName.LastIndexOf(" ");
            if (pos1 < 1) return "The information was not added to sharepoint because the process was not able to parse lastname in email : " + body;
            string lastname1 = EmplName.Substring(pos1);
            EmplName = EmplName.Replace(lastname1, "");
            EmplName = EmplName.Trim();
            lastname1 = lastname1.Trim();

            // date termination
            string termina = CheckTerminationDate(body, dtSended, Reason2);
            if (termina.IndexOf("date incorrect") > 2) return termina;

            string project = "";
            string LOB = "";
            string location = "";
            int pos3 = 0;
            string warning = "";

            pos1 = body.IndexOf("Location");
            if (pos1 < 2)
                warning = "Information added in sharepoint but it was not able to parse location in email : ";
            else
            {
                pos3 = body.IndexOf("Project Code");
                if (pos3 < 1)
                    warning = "Information added in sharepoint but it was not able to parse project in email : ";
                else
                {
                    location = body.Substring(pos1 + 10, pos3 - pos1 - 11);
                    location = location.Trim();

                    //pos1 = body.IndexOf("Code", pos1);
                    //int pos2 = pos1 - pos3;
                    //if (pos2 < 1 || pos2 > 15) return "Could not find project code";

                    pos1 = body.IndexOf(" ", pos3 + 11);
                    if (pos1 < 2)
                        warning = "Information added in sharepoint but it was not able to parse LOB in email :";
                    else
                    {
                        project = body.Substring(pos3 + 12, 12);
                        project = project.Trim();
                        pos1 = project.IndexOf(" ");
                        project = project.Substring(0, pos1);
                        project = project.Trim();

                        pos1 = body.LastIndexOf("Program");
                        LOB = body.Substring(pos1 + 8);

                        //
                        pos1 = LOB.IndexOf(" Termination Reason");  //============
                        pos1 = LOB.IndexOf("<");  //============

                        LOB = LOB.Substring(0, pos1);
                        LOB = LOB.Trim();
                        if (LOB.Length > 49) LOB = LOB.Substring(0, 49);
                    }
                }
            }

            // ========================
            int pos4 = body.IndexOf("span><");
            if (pos4 > 1) body = body.Substring(0, pos4 - 2);
            pos4 = body.IndexOf("<[if");
            if (pos4 > 1) body = body.Substring(0, pos4);

            //log.Info("Load ATT");
            if (System == "ATT") warning = warning + LoadInfoATT(EmplID, EmplName, lastname1, termina, project, location, LOB, dtSended, Reason2, body, sender);
           
            return warning;
        }

        private static string LoadInfoATT(string EmplID, string EmplName, string lastname1, string termina, string project, string location, string LOB, string dtSended, string Reason2, string body, string sender)
        {
            //log.Info("LoadInfoATT " + EmplID + " " + EmplName);
            string ATTUID = DataAccessUDW.GetATTUID(EmplID);

            string msg = "";
            //string msg = CheckReprocess(body, ATTUID, EmplID, IsMoved);
            //if (msg != "") return msg;

            string warning = "";
            msg = "ATTUID : " + ATTUID + " emplid : " + EmplID + " Name : " + EmplName + " " + lastname1 + "  Date : " + termina + " Project " + project + " Location " + location + " LOB " + LOB + " sender " + sender;
            log.Info(msg);

            if (DataAccess.LoadIntoDataBase(ATTUID, EmplID, EmplName, lastname1, termina, dtSended, project, location, LOB, Reason2, sender))
                log.Info("Loaded into Database");
            else
            {
                string info = "(" + EmplID + ",'" + ATTUID + "','" + EmplName + " " + lastname1 + "','AGENT','Y','" + termina + "','" + project + "','" + location + "','" + LOB + "');";
                string ErrMsg = "It was not able to process data, there was a problem when loading in database for <\br><\br> " + info;
                log.Info(ErrMsg);
                return ErrMsg;
            }

            if (!DataAccess.LoadIntoSharePoint(ATTUID, EmplID, EmplName, lastname1, termina, project, location, LOB, Reason2))
            {
                msg = "It was not able to add info in sharepoint, for <br><br> " + body;
                log.Error(msg);
                return msg;
            }
            else
            {
                // LOGS TO 0-NO LOGS   1-DEVELOPMENT  2-PRODUCTION SERVER
                string server1 = ConfigurationManager.AppSettings["Logs"];
                int server = System.Convert.ToInt32(server1);
                //Log.Class1.Write(server1,)
            }
            if (warning != "") warning = warning + " " + body;
            return warning;
        }

        private static string RemoveSpecialCharacters2(string body)
        {
            int pos1 = body.IndexOf("Employee");
            if (pos1 > 1) body = body.Substring(pos1 - 1);

            body = body.Replace("\\r", "");
            body = body.Replace("\\n", "");
            body = body.Replace("\\t", "");
            return body;
        }

        public static string RemoveSpecialCharacters(string body)
        {
            body = body.Replace("&amp;", "");
            int pos1 = body.IndexOf("Employee");
            if (pos1 > 1) body = body.Substring(pos1 - 1);

            return Regex.Replace(body, "[^a-zA-Z0-9_.< :/-ñ']+", "", RegexOptions.Compiled);
        }

        private static string CheckTerminationDate(string body, string dtSended, string Reason2)
        {
            string FindStr = "Termination Date:";
            int pos2 = 17;

            if (Reason2 == "Transfer")
            {
                FindStr = "Move Date:";
                pos2 = 10;
            }

            int pos1 = dtSended.IndexOf(" ");
            dtSended = dtSended.Substring(0, pos1);
            dtSended.Trim();

            pos1 = body.IndexOf(FindStr);

            if (pos1 < 2) return "The information was not added to sharepoint because the process was not able to parse date " + body;
            string termina = body.Substring(pos1 + pos2, 13);
            termina = termina.Trim();

            pos1 = termina.IndexOf("<");
            if (pos1 < 3 || pos1 > 12) pos1 = termina.IndexOf(" ");
            if (pos1 > 10) pos1 = 10;

            if (pos1 > 1) termina = termina.Substring(0, pos1);
            termina = termina.Trim();

            try
            {
                DateTime dt = DateTime.Parse(termina);
            }
            catch (Exception)
            {
                return "Incorrect Date " + body;
            }

            if (DateTime.Parse(termina) < DateTime.Parse(dtSended)) termina = dtSended;

            termina = termina.Trim();
            return termina;
        }

        //private static string CheckReprocess(string body, string ATTUID, string EmplID, bool IsMoved)
        //{
        //    // CHECK IF THE EMPLOYEE IS IN THE tblDailyReport2
        //    string msg = DataAccess.CheckDuplicate(EmplID);
        //    if (msg == "") return "";
        //    if (msg == "Error") return "Database error CheckDuplicate process for " + body;

        //    if (!IsMoved && msg != "") return "Employee previously processed for " + body;

        //    // It is a MOVED and it is the employee ID in tblDailyReport2
        //    msg = DataAccess.CheckFieldGlassStatus(ATTUID);
        //    if (msg == "") return "";
        //    if (msg == "Error") return "Database error CheckDuplicate process for " + body;
        //    if (msg == "Closed") return "Employee previously processed for ATTUID " + ATTUID + " " + body;

        //    return "";
        //}

        private static string GetEmplID(string body)
        {
            int pos1 = body.IndexOf("Employee ID:");
            if (pos1 < 1) return "The informatin was not added in sharepoint because it was not able to find Employee ID " + body;
            string EmplID = body.Substring(pos1 + 12, 14);

            pos1 = EmplID.IndexOf(" ", 4);
            if (pos1 < 2 || pos1 > 13) return "The information was not added in sharepoint because the process was not able to parse Employee ID, mising '-' " + body;
            EmplID = EmplID.Substring(0, pos1);
            EmplID = EmplID.Trim();

            return EmplID;
        }
    }
}
