using System;
using System.Data;
using System.Linq;

namespace Automatiza
{
    class Procesar
    {
        public void ProcessOne(string EmpName, string NewInfo, string NewStatus, string NewCustomer)
        {
            string sql = "";

            // Cargue ultimo registro de tracker daily
            DataTable dtTracker = Data.getTracker();
            string where1 = "nome = '" + EmpName + "' ";
            DataRow[] result = dtTracker.Select(where1, "Date1 ASC");
            Informacion info;

            //id
            //   empid
            //   callinfo
            //   status
            //   customer
            //date1
            //time1
            //duration


            // If it doesn't exist, it is the first
            // Insert ML_Agents_Daily
            // Insert ML_Tracker sino es logout
            if (result.Count() == 0)
            {
                info = GetInfo(EmpName); // EmpID, TL, LOB
                sql = "INSERT INTO ML_Agents_Daily (EmpID, TL, LOB, status) VALUES (";
                sql = sql + info.EmpId + ",";
                sql = sql + info.TL + ",";
                sql = sql + info.LOB + ",";
                sql = sql + "'" + NewStatus + "' )";
                Data.ExecuteNonQuery(sql);

                CheckIfLOBCreatedToday(info.LOB);

                if (NewStatus != "Logout")
                    InsertInTracker(info.EmpId, NewCustomer, NewStatus, NewInfo);

                return;
            }

            // Si informacion no ha cambiado retorne
            string OldCust = result[0]["Customer"].ToString();
            if (NewCustomer == OldCust) return;

            string TrackerID = result[0]["id"].ToString();
            string OldStatus = result[0]["Status"].ToString();
            string LastStart = result[0]["time1"].ToString();

            info = GetInfo(EmpName); // Informacion EmpID, LOB, TL
            double lapso = Duration(LastStart);

            // Actualize ML_Tracker (duracion)
            sql = "UPDATE ML_Tracker SET Duration = " + lapso.ToString();
            sql = sql + " WHERE ID = " + TrackerID;
            Data.ExecuteNonQuery(sql);

            if (NewStatus != "Logout")
                InsertInTracker(info.EmpId, NewCustomer, NewStatus, NewInfo);

            UpdateAgentsDaily(EmpName, OldStatus, lapso);
        }

        void CheckIfLOBCreatedToday(string LOB)
        {
            DataTable dt = Data.getLOBDaily();
            DataRow[] drDaily = dt.Select("id = " + LOB);
            if (drDaily.Count() > 0) return;

            DataTable dtLOB = Data.getLOB();
            DataRow[] drLOB = dt.Select("id = " + LOB);

            DataTable dtAgents = Data.getAgents();
            int totAgents = dtAgents.Select("LOB =" + LOB).Count();

            string where1 = "LOB =" + LOB + " and status ='Arrested' ";
            int arrested = dtAgents.Select(where1).Count();

            where1 = "LOB =" + LOB + " and status ='Vacation' ";
            int vacation = dtAgents.Select(where1).Count();

            where1 = "LOB =" + LOB + " and status ='Sick' ";
            int sick = dtAgents.Select(where1).Count();

            string sql = "INSERT INTO ML_LOBs_Daily values ";
            sql = sql + " (LOB, LOB_Name, TotAgents, Arrested, Vacation, Sick) values (";
            sql = sql + LOB + ",'";
            sql = sql + drLOB[0]["LOB"].ToString() + "', ";

            sql = sql + totAgents.ToString() + ", ";
            sql = sql + arrested.ToString() + ", ";
            sql = sql + vacation.ToString() + ", ";
            sql = sql + sick.ToString() + ") ";

            Data.ExecuteNonQuery(sql);
        }

        // AL FINALIZAR CADA CICLO ACTUALIZA EL ML_LOBs_DAILY
        public void TotalByLOB()
        {
            DataTable dtLOBDaily = Data.getLOBDaily();
            foreach (DataRow drLOB in dtLOBDaily.Rows)
            {
                drLOB["Fechados"] = 0;
                drLOB["Abandonados"] = 0;

                drLOB["Online"] = 0;
                drLOB["Offline"] = 0;
                drLOB["Pause"] = 0;
            }

            string sql = "select * from ML_Agents_Daily ORDER BY LOB ";
            // sql = sql + " WHERE Created='" + Data.TODAY(2) + "'";
            DataTable dtAgentsDaily = Data.GetDataTable(sql);

            int LOB_Prev = 0;
            foreach (DataRow dr in dtAgentsDaily.Rows)
            {
                int LOB = System.Convert.ToInt32(dr["LOB"].ToString());
                string StrStatus = dr["Status"].ToString();
                int Fechados = Convert.ToInt32(dr["Fechado"].ToString());
                int Abandonados = Convert.ToInt32(dr["Abandonado"].ToString());

                int rows = 0;
                if (LOB_Prev != LOB)
                {
                    foreach (DataRow dr2 in dtLOBDaily.Rows)
                    {
                        int LOB2 = Convert.ToInt32(dr["LOB"].ToString());
                        if (LOB2 == LOB)
                        {
                            dr2[StrStatus] = Convert.ToInt32(dr2[StrStatus].ToString()) + 1;
                            dr2["Fechados"] = Convert.ToInt32(dr2["Fechados"].ToString()) + Fechados;
                            dr2["Abandonados"] = Convert.ToInt32(dr2["Fechados"].ToString()) + Abandonados;

                            break;
                        }
                        rows++;
                    }
                    LOB_Prev = LOB;
                }
                else
                {
                    dtLOBDaily.Rows[rows][StrStatus]= Convert.ToInt32(dtLOBDaily.Rows[rows][StrStatus].ToString()) + 1;
                    dtLOBDaily.Rows[rows]["Fechados"] = Convert.ToInt32(dtLOBDaily.Rows[rows]["Fechados"].ToString()) + 1;
                    dtLOBDaily.Rows[rows]["Abandonados"] = Convert.ToInt32(dtLOBDaily.Rows[rows]["Abandonados"].ToString()) + 1;
                }
            }

            foreach (DataRow drLOB2 in dtLOBDaily.Rows)
            {
                string online = drLOB2["Online"].ToString();
                string offline = drLOB2["Offline"].ToString();
                string pause = drLOB2["Pause"].ToString();

                string Fechados = drLOB2["Fechados"].ToString();
                string Abandonados = drLOB2["Abandonados"].ToString();

                sql = "UPDATE ML_LOBs_Daily ";
                sql = sql + " SET Online = " + online;
                sql = sql + " , Offline = " + offline;
                sql = sql + " , Pause = " + pause;

                sql = sql + " , Fechados = " + Fechados.ToString();
                sql = sql + " , Abandonados = " + Abandonados.ToString();

                sql = sql + " WHERE LOB = " + drLOB2["ID"].ToString();
                // sql = sql + " AND Created = '" + Data.TODAY(2) + "'";

                Data.ExecuteNonQuery(sql);
            }
        }

        string SumTimes(string OldStatus, double period)
        {
            string retorno = "";
            if (OldStatus == "Online") retorno = " set Online1 = Online1 + " + period.ToString();
            return retorno;
        }

        void UpdateAgentsDaily(string EmpName, string OldStatus, double lapso)
        {
            DataTable dtAgentsDaily = Data.getAgentsDaily();
            string where1 = "nome = '" + EmpName + "' ";
            DataRow[] drDaily = dtAgentsDaily.Select(where1);
            string ID = drDaily[0]["id"].ToString();

            string sql = "UPDATE ML_Agents_Daily ";
            sql = sql + SumTimes(OldStatus, lapso);
            sql = sql + " WHERE ID =" + ID;
            Data.ExecuteNonQuery(sql);
        }

        void InsertInTracker(string EmpID, string NewCustomer, string NewStatus, string NewInfo)
        {
            // Inserte ML_Tracker , cust, date, time, status (sin duracion) (1)
            string sql = "INSERT INTO ML_Tracker ";
            sql = sql + " (EmpID, Customer, Status, CallInfo) Values ( ";
            sql = sql + EmpID + ",";
            sql = sql + "'" + NewStatus + "',";
            sql = sql + "'" + NewStatus + "',";

            Data.ExecuteNonQuery(sql);
        }

        double Duration(string startTime)
        {
            //string startTime = result[0]["time1"].ToString();
            string endTime = DateTime.Now.ToString("h:mm:ss tt");
            //TimeSpan startTime = Convert.ToDateTime(time1).TimeOfDay;
            TimeSpan duration = DateTime.Parse(endTime).Subtract(DateTime.Parse(startTime));
            return duration.TotalMinutes;
        }

        Informacion GetInfo(string name)
        {
            Informacion info = new Informacion();
            string where1 = "nome = '" + name + "' ";

            DataTable dtAgents = Data.getAgents();
            DataRow[] result = dtAgents.Select(where1);
            if (result.Count() == 0) return null;
            info.EmpId = result[0]["id"].ToString();
            info.TL = result[0]["TL"].ToString();
            info.LOB = result[0]["LOB"].ToString();
            //int count1 = dt2.Select(where1).Count();
            return info;
        }
    }

    class Informacion
    {
        public string EmpId;
        public string TL;
        public string LOB;
    }
}
