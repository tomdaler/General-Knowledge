using log4net;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Text.RegularExpressions;

namespace Task
{
    class ProcessTransfers
    {
        private static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static string FileName1 = "C:\\Temp\\ATT_TRANSFERS.xlsx";

        public static bool WithAttachment(EmailMessage email, string sender)
        {
            for (int i = 0; i < email.Attachments.Count; i++)
            {
                string nombre = email.Attachments[i].Name.ToString();
                nombre = nombre.Replace("&", "");

                if (nombre.IndexOf("ATT Transfers Report") > -1
                 || nombre.IndexOf("ATT_TRANSF")>-1)

                {
                    nombre = nombre.ToUpper();
                    if (nombre.IndexOf("XLSX") < 4)
                    {
                        string ErrMsg = "The process couldn’t load the information because it is expecting an excel file with the extension xlsx correct the format and send again the email";
                        SendEmails.SendEmail("Notification Robot, wrong format", ErrMsg, "");
                        log.Error(ErrMsg);
                        return false;
                    }

                    Attachment archivo = email.Attachments[i];
                    if (archivo is FileAttachment)
                    {
                        System.IO.File.Delete(FileName1);
                        ((FileAttachment)archivo).Load(FileName1);

                        ImportTransfersFromFile();
                        return true;
                    }
                }
                else
                {
                    string sender1 = ConfigurationManager.AppSettings["EmailTo"].ToString();
                    SendEmails.SendEmail("Notification Robot, wrong transfer file name", "Wrong file name, it must be ATT Transfers Report, not " + nombre,sender1);
                    log.Info("wrong name " + nombre);
                }
            }
            return false;
        }

        public static void ImportTransfersFromFile()
        {
            {
                // load file
                //string path = ConfigurationManager.AppSettings["Path"].ToString();
                //DateTime dt11 = DateTime.Now.AddDays(-1);
                //string file = "AT&T Transfers Report " + dt11.ToString("yyyy-MM-dd") + ".xlsx";
                //path = path + file;

                string path = FileName1;
                // path = "C:\\temp\\AT&T Transfers Report.xlsx";
                try
                {

                    log.Info("File loading " + path);
                    string ExcelConnectionString = ConfigurationManager.ConnectionStrings["Excel"].ConnectionString;
                    ExcelConnectionString = string.Format(ExcelConnectionString, path);

                    OleDbConnection connExcel = new OleDbConnection(ExcelConnectionString);
                    OleDbCommand command = new OleDbCommand("Select * from [Sheet1$]", connExcel);
                    connExcel.Open();
                    System.Data.Common.DbDataReader dr = command.ExecuteReader();

                    //DataTable dt2 = new DataTable();
                    //dt2.Load(dr);
                    //int numRows = dt2.Rows.Count;

                    //log.Info("Excel rows " + numRows.ToString());


                    string ErrMsg = "";

                    DataTable dtATTUIDs = DataAccessUDW.GetAllATTUIDs();
                    if (dtATTUIDs is null) return;

                    log.Info("Get ATTUID's " + dtATTUIDs.Rows.Count.ToString());
                    int regis = 0;
                    int loaded = 0;
                    string EmpID_prev = "";

                    while (dr.Read())
                    {
                        regis++;
                        if (regis < 8) continue;


                        // EMPLID
                        string EmplID = dr[1].ToString();
                        EmplID = EmplID.Trim();

                        if (EmplID == "" || EmplID == "Employee ID" || EmplID == EmpID_prev) continue;

                        EmpID_prev = EmplID;

                        Regex isnumber = new Regex("[^0-9]");
                        if (isnumber.IsMatch(EmplID))
                        {
                            ErrMsg = ErrMsg + "Row " + regis + "  Employee ID " + EmplID + " is not a number <br />";
                            continue;
                        }

                        //if (DataAccess.CheckDuplicate(EmplID) != "")
                        //{
                        //    ErrMsg = ErrMsg + "Row " + regis + "  Employee ID " + EmplID + " was processed previously<br />";
                        //    continue;
                        //}


                        // ATTUID
                        //********
                        string ATTUID = ""; // DataAccess.GetAttUID(EmplID, name1);
                        string where1 = "EMPLID = '" + EmplID + "'";
                        DataRow[] result = dtATTUIDs.Select(where1);

                        if (result.Length > 0) ATTUID = result[0][0].ToString();
                        ATTUID = ATTUID.Trim();


                        // EMPLOYEE NAME
                        //***************
                        string name1 = dr[2].ToString();
                        int pos1 = name1.LastIndexOf(" ");
                        if (pos1 < 2)
                        {
                            ErrMsg = ErrMsg + "Row " + regis + "  Employee ID " + EmplID + " Employee Name " + name1 + " format incorrect <br />";
                            continue;
                        }

                        string lastname1 = name1.Substring(pos1);
                        name1 = name1.Replace(lastname1, "");
                        name1 = name1.Trim();
                        lastname1 = lastname1.Trim();


                        // ATTRITION DATE
                        //****************
                        string dt1 = dr[5].ToString();
                        try
                        {
                            DateTime dt = DateTime.Parse(dt1);
                        }
                        catch (Exception)
                        {
                            ErrMsg = ErrMsg + "Row " + regis + "  Employee ID " + EmplID + " Date " + dt1 + " date format error, column 5<br />";
                            continue;
                        }


                        // LOCATION PROJECT LOB
                        //**********************
                        string location = dr[0].ToString();
                        string project = dr[4].ToString();
                        pos1 = project.IndexOf(" ");
                        if (pos1 < 2)
                        {
                            string error = "Row " + regis + "  Employee ID " + EmplID + " Project " + project + " format not found <br/>";
                            log.Info(error);

                            ErrMsg = ErrMsg + error;
                            continue;
                        }
                        string LOB = project.Substring(pos1);
                        project = project.Replace(LOB, "");
                        project = project.Trim();
                        LOB = LOB.Trim();
                        LOB = LOB.Replace("(inactive)", "");
                        if (LOB.Length > 25) LOB = LOB.Substring(24);
                        LOB = LOB.Trim();

                        // LOAD DATA
                        //***********
                        bool retorno = DataAccess.LoadIntoDataBase(ATTUID, EmplID, name1, lastname1, dt1, dt1, project, location, LOB, "Transfer", "Transfer File");
                        if (!retorno)
                        {
                            ErrMsg = ErrMsg + "Row " + regis + "  Employee ID " + EmplID + " Error in database " + retorno + " <br />";
                            continue;
                        }
                        else
                        {
                            
                            retorno = DataAccess.LoadIntoSharePoint(ATTUID, EmplID, name1, lastname1, dt1, project, location, LOB, "Transfer");
                            if (!retorno)
                            {
                                ErrMsg = ErrMsg + "Row " + regis + "  Employee ID " + EmplID + " Error when loading into sharepoint  " + retorno + " <br />";
                                continue;
                            }

                            loaded++;
                            log.Info("Loaded " + loaded.ToString());
                            ErrMsg = ErrMsg + "Loaded " + EmplID + " " + ATTUID + " " + name1 + " " + lastname1 + "   <br />";
                        }
                    }

                    connExcel.Close();
                    ErrMsg = ErrMsg + "<br /><br /> Loaded " + loaded + " records from attached file in email into sharepoint";

                    SendEmails.SendEmail("Deactivation Robot Transfers", ErrMsg,"");
                    log.Info(ErrMsg);
                }
                catch (Exception es)
                {
                    log.Info(es.ToString());
                    SendEmails.SendEmail("Robot ", "Error en transfers","");
                    //System.Environment.Exit(0);
                }
            }
        }
    }
}
