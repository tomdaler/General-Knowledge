using log4net;
using System;
using System.Data;
using System.Reflection;

namespace Reports
{
    class Notifications
    {
        static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        bool TESTING = true;

        public void Notif()
        {
            Exposure.DataBase DD1 = new Exposure.DataBase();
            Exposure.SendEmails se = new Exposure.SendEmails();

            string today = Today();

            DateTime Case_7_Prior = Convert.ToDateTime(today).AddDays(5);

            DateTime Case_7 = Convert.ToDateTime(today).AddDays(-5);
            DateTime Case_10 = Convert.ToDateTime(today).AddDays(-8);

            DateTime Case_16 = Convert.ToDateTime(today).AddDays(-15);
            DateTime Case_20 = Convert.ToDateTime(today).AddDays(-19);
            DateTime Case_23 = Convert.ToDateTime(today).AddDays(-22);

            Exposure.DataBase notif = new Exposure.DataBase();
            DataTable HR = notif.ReadXML();
            DataTable table = notif.GetData(14,"");
            if (table != null)
            {
                int conteo = table.Rows.Count;
            }
            Exposure.DataBase db = new Exposure.DataBase();
            DataTable dt2 = db.dtNotif();
            string[] arrSent = null;
            string tipo = "";
            int rows = 0;

            foreach (DataRow dr in table.Rows)
            {
                if (rows==213)
                {
                   // int i = 0;
                }

                rows++;
                arrSent = null;
                tipo = "";
                string EmpID = dr[0].ToString();
                if (EmpID== "1063792")
                {
                   // int i = 0;
                }

                string EmpName = dr[1].ToString();
                object sss = dr[1].ToString();

                string strLoc = dr[2].ToString();
                strLoc = strLoc.Replace("PHL ", "");

                string dtStart = dr[3].ToString();
                string dtEnd = dr[4].ToString();
                string dtExt = dr[5].ToString();

                if (dtExt != null && dtExt.Trim() != "") dtEnd = dtExt;

                int ddays = 0;
                try
                {
                    ddays = Int32.Parse(dr[6].ToString());
                }
                catch (Exception) { }

                bool CheckStart = true;
                bool CheckEnd = true;

                if (dtStart == "Pending") CheckStart = false;
                if (dtStart == "N/A") CheckStart = false;

                if (dtEnd == "Pending") CheckEnd = false;
                if (dtEnd == "N/A") CheckEnd = false;

                DateTime dtStart1 = DD1.GetDate(dtStart);
                DateTime dtEnd1 = DD1.GetDate(dtEnd);
                //====================================

                if (dtStart == "01/01/2000") CheckStart = false;
                if (dtStart1.Year < 2001) CheckStart = false;
                              
                if (dtEnd == "01/01/2000") CheckEnd = false;
                if (dtEnd1.Year < 2001) CheckEnd = false;

                DateTime ahora = DateTime.Now;
                if (CheckStart)
                {
                    int dias = (ahora - dtStart1).Days;
                    if (dias > 100) dtStart1 = DateTime.Now.AddDays(100);
                }

                if (CheckEnd)
                {
                    int dias = (ahora - dtEnd1).Days;
                    if (dias > 100) dtEnd1 = DateTime.Now.AddDays(100);
                }
                TESTING = false;
                bool Prior7Days = TESTING;
                bool Ahead7Days = TESTING;
                bool Ahead10Days = TESTING;

                if (CheckEnd)
                {
                    if (dtEnd1.Day == Case_7_Prior.Day
                    && dtEnd1.Month == Case_7_Prior.Month) Prior7Days = true;
                }


                if (CheckStart)
                {
                    if (ddays == 0)
                    {
                        if (dtStart1.Day == Case_7.Day
                        && dtStart1.Month == Case_7.Month) Ahead7Days = true;

                        if (dtStart1.Day == Case_10.Day
                        && dtStart1.Month == Case_10.Month) Ahead10Days = true;
                    }

                    if (ddays == 7)
                    {
                        if (dtStart1.Day == Case_16.Day
                        && dtStart1.Month == Case_16.Month) Ahead10Days = true;
                    }

                    if (ddays == 14)
                    {
                        if (dtStart1.Day == Case_20.Day
                        && dtStart1.Month == Case_20.Month) Ahead7Days = true;

                        if (dtStart1.Day == Case_23.Day
                        && dtStart1.Month == Case_23.Month) Ahead10Days = true;
                    }
                }

                //==========================
                if (Prior7Days)
                {
                    //tipo = "Remainder Clinic";
                    //arrSent = se.sendingAllEmails(1, EmpID, EmpName, strLoc,
                    //    dtEnd, "", "", "", "", "",HR);
                    //notif.UpdateStatus(EmpID, "Notified 7 days Prior");
                }

                if (Ahead7Days)
                {
                    if (ddays > 0)
                        ddays = ddays + 14;
                    else
                        ddays = 14;

                    tipo = "7th Day of SQ";
                    try
                    {
                        arrSent = se.sendingAllEmails(9, EmpID, EmpName, strLoc,
                            dtStart, dtEnd, ddays.ToString(), "", "", "", HR);
                        notif.UpdateStatus(EmpID, "Notified 7th Day of SQ");
                    }
                    catch (Exception es) {
                        log.Info("ERROR " + es.ToString());
                        tipo = "";
                    }
                    continue;
                }

                if (Ahead10Days)
                {
                    try
                    {
                        tipo = "10th Day of SQ";
                        arrSent = se.sendingAllEmails(10, EmpID, EmpName, strLoc,
                           dtStart, dtEnd, "", "", "", "", HR);
                        notif.UpdateStatus(EmpID, "Notified 10th Day of SQ");
                    }
                    catch (Exception es)
                    {
                        log.Info("ERROR " + es.ToString());
                        tipo = "";
                    }
                }

                if (tipo != "")
                {
                    dt2.Rows.Add(DateTime.Now.ToString(), EmpID, EmpName, strLoc, tipo, arrSent[0], arrSent[1], arrSent[2], arrSent[3]);
                }
            }
            // SUMMARY
            //Exposure.SendEmails sent = new Exposure.SendEmails();
            //sent.SentSummary(dt2, "", "");
        }


        public string Today()
        {
            var timeToConvert = DateTime.Now;  //whereever you're getting the time from
            var est = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
            return TimeZoneInfo.ConvertTime(timeToConvert, est).ToString();
        }

    }
}
