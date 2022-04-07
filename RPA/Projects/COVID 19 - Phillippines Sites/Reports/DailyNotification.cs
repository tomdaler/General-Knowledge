using log4net;
using System;
using System.Configuration;
using System.Data;
using System.Reflection;

namespace Reports
{
    class DailyNotification
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void Reports(string fileX)
        {
            // THIS 2 ARE DAILY REPORTS, ONLY IF VARIABLE IS YES
            string OneDay = ConfigurationManager.AppSettings["OneDay"].ToString();
            if (OneDay.ToUpper() != "YES") return;
            log.Info("Daily Notification");

            Exposure.Program common = new Exposure.Program();
            string path = ConfigurationManager.AppSettings["FILES"].ToString();
            string sendTo = ConfigurationManager.AppSettings["EmailTo_7_10"].ToString();
            string[] File = new string[1];
            Exposure.SendEmails sent = new Exposure.SendEmails();

            string FileName = "Summary of Notifications for 7th and 10th Day Reminder_" + fileX;
            string where = path + FileName;

            if (common.Generate(13, where, "", "", true))
            {
                File[0] = where;
                sent.GeneralEmail(FileName, FileName, File, sendTo);
            }
            //=========================================
            // if hourly return

            log.Info("14 day SQ COMPLETION REPORT");
            Exposure.DataBase notif = new Exposure.DataBase();
            DataTable table = notif.GetData14();
            if (table.Rows.Count == 0) return;

            Notifications nn = new Notifications();
            string today = nn.Today();
            DateTime Case_14 = Convert.ToDateTime(today).AddDays(-12);
            DateTime Case_1 = Convert.ToDateTime(today).AddDays(1);

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("Employee ID", typeof(string));
            dt2.Columns.Add("Employee Name", typeof(string));
            dt2.Columns.Add("Location", typeof(string));
            dt2.Columns.Add("Program", typeof(string));
            dt2.Columns.Add("Mobile", typeof(string));
            dt2.Columns.Add("Alternative Number", typeof(string));
            dt2.Columns.Add("Best Time To Call", typeof(string));
            dt2.Columns.Add("SQ Start Date", typeof(string));
            dt2.Columns.Add("SQ End Date", typeof(string));
            dt2.Columns.Add("Ext End Date", typeof(string));
            dt2.Columns.Add("DOH Category", typeof(string));

            dt2.Columns.Add("General Category", typeof(string));
            dt2.Columns.Add("Recommendation", typeof(string));
            dt2.Columns.Add("Latest Whereabouts", typeof(string));
            dt2.Columns.Add("Currently in Facility or Hospital", typeof(string));
            dt2.Columns.Add("Date of Confinement", typeof(string));
            dt2.Columns.Add("Is the employee in ICU", typeof(string));
            dt2.Columns.Add("Date First Symptom Manifested", typeof(string));
            dt2.Columns.Add("Symptoms/Assessment/Details", typeof(string));
            dt2.Columns.Add("Severity", typeof(string));
            dt2.Columns.Add("BHERT", typeof(string));
            dt2.Columns.Add("ID", typeof(string));

            dt2.Columns.Add("Date Reported", typeof(string));
            dt2.Columns.Add("Confirmed Case Date", typeof(string));

            Exposure.DataBase DD1 = new Exposure.DataBase();

            string lista = "101529889,101501661,724773,101511887,101576539,101591838";

            foreach (DataRow dr in table.Rows)
            {
                string EmpID = dr[0].ToString();

                if (lista.IndexOf(EmpID)>-1)
                         {
                    //int i = 0;
                    //continue;
                }
                string dtStart = dr[3].ToString();
                string dtEnd = dr[4].ToString();
                string dtExtend = dr[5].ToString();
                string Recommend = dr[13].ToString();

                string QED = dtEnd;
                string Extend = dtExtend;

                if (dtStart == "01/01/2000") continue;
                if (Recommend.Trim() == "") continue;

                DateTime dtStart1 = DD1.GetDateMMddYYYY(dtStart);
                DateTime dtEnd1 = DD1.GetDateMMddYYYY(dtExtend);
                if (dtEnd1.Year == 1900) dtEnd1 = DD1.GetDateMMddYYYY(dtEnd);
                
                //  if ((dtStart1.Day == Case_14.Day && dtStart1.Month == Case_14.Month)
                //bool Continue = false;
                if (dtEnd1.Year == 1900) continue;
                
                if (dtEnd1 > Case_1) continue;

                // && dtEnd1.Day == Case_1.Day 
                // && dtEnd1.Month == Case_1.Month)
                //    Continue = true;



                //if (!Continue)
                //{
                //    if (Recommend != ""
                //        && (QED !="" || Extend !="") )
                //        Continue = true;
                //}

                //if (Continue)
                //{
                    string EmpName = dr[1].ToString();

                    string strLoc = dr[2].ToString();
                    strLoc = strLoc.Replace("PHL ", "");

                    string program = dr[7].ToString();
                    string mobile = dr[8].ToString();
                    string landline = dr[9].ToString();
                    string besttime = dr[10].ToString();
                    string DOH = dr[11].ToString();

                string GenCat = dr[12].ToString();
                string Recommendation = dr[13].ToString();
                string WhereAbouts = dr[14].ToString();

                string Faculty = dr[15].ToString();
                string dtConfinement = dr[16].ToString();
                string ICU= dr[17].ToString();
                string dtFirst = dr[18].ToString();
                
                string Assessment = dr[19].ToString();
                string Severity = dr[20].ToString();
                string BHERT = dr[21].ToString();
                string ID = dr[22].ToString();
                                
                string DT1 = dr[23].ToString();
                string DT2 = dr[24].ToString();

                dt2.Rows.Add(EmpID, EmpName, strLoc, program, mobile, landline, besttime,
                    dtStart, dtEnd, dtExtend, DOH, GenCat, Recommendation, WhereAbouts,
                    Faculty, dtConfinement, ICU, dtFirst, Assessment, Severity, BHERT, ID,
                    DT1, DT2);
                //}
            }

            if (dt2.Rows.Count == 0)
            {
                log.Info("");
                log.Info("NO RECORDS FOUND FOR 14 day SQ COMPLETION REPORT");
                log.Info("");
                return;
            }

            FileName = "SQ Completion Report_" + fileX;
            where = path + FileName;

            Exposure.Program.ExportData(dt2, where);
            File[0] = where;
            sendTo = ConfigurationManager.AppSettings["Notif_14"].ToString();
            string subject = "SQ Completion Report_" + fileX;

            sent.GeneralEmail(subject, subject, File, sendTo);
            //}

        }
    }
}
