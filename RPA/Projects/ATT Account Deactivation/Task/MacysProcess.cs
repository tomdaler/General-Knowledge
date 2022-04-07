using HtmlAgilityPack;
using log4net;
using System;
using System.Configuration;
using System.Reflection;

namespace Task
{
    public class MacysProcess
    {
        const int rowcount = 12;
        const int rowcount2 = 20;
        private static log4net.ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        //public string[] ReadBody333(string body)
        //{
        //    int i = 0;
        //    string[] row = new string[rowcount];
        //    Boolean start = false;

        //    try
        //    {
        //        HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
        //        document.LoadHtml(body);

        //        string BCOM = ConfigurationManager.AppSettings["MacysBCOMSurvey"];
        //        string MCOM = ConfigurationManager.AppSettings["MacysMCOMSurvey"];

        //        foreach (HtmlNode node in document.DocumentNode.SelectNodes("//text()"))
        //        {
        //            if (node.InnerText == BCOM || node.InnerText == MCOM)
        //            {
        //                start = true;
        //            }
        //            if (start)
        //            {
        //                if (i < rowcount)
        //                {
        //                    string display = node.InnerText;
        //                    display = display.Trim();
        //                    if (display != string.Empty)
        //                    {
        //                        row[i] = display;
        //                        i = i + 1;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Info("Error " + ex.ToString());
        //        throw ex;
        //    }
        //    return row;
        //}
             
        public string[] ReadBody2(string body)
        {
            int i = 0;
            string[] row = new string[rowcount2];
            Boolean start = false;

            try
            {
                HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(body);

                string BCOM = ConfigurationManager.AppSettings["MacysBCOMSurvey2"];
                string MCOM = ConfigurationManager.AppSettings["MacysMCOMSurvey2"];

                foreach (HtmlNode node in document.DocumentNode.SelectNodes("//text()"))
                {
                    if (node.InnerText == BCOM || node.InnerText == MCOM)
                    {
                        start = true;
                    }
                    if (start)
                    {
                        if (i < rowcount2)
                        {
                            string display = node.InnerText;
                            display = display.Trim();
                            if (display != string.Empty && display != "&NBSP;" && display != "&amp;NBSP;")
                            {
                                row[i] = display;
                                i = i + 1;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Info("Error " + ex.ToString());
                throw ex;
            }
            return row;
        }

        public string[] ProcessBody(string[] result)
        {
            const string q1 = "HOW MUCH EFFORT DID YOU PERSONALLY HAVE TO PUT FORTH TO HANDLE YOUR REQUEST?";
            const string q2 = "DID THE REPRESENTATIVE FULLY RESOLVE YOUR ISSUE TODAY?";
            const string q3 = "HOW MANY TIMES DID YOU CONTACT US TO GET THIS ISSUE RESOLVED?";
            const string q4 = "WITH THE LAST PERSON YOU SPOKE WITH IN MIND, HOW WELL DID THEY LISTEN TO AND UNDERSTAND YOUR CONCERN OR QUESTION?";
            const string q4alt = "WITH THE LAST PERSON YOU SPOKE WITH IN MIND, HOW WELL";
            const string q5 = "WITH THE LAST PERSON YOU SPOKE WITH IN MIND, HOW EASY DID THEY MAKE IT TO GET YOUR QUESTION OR ISSUE RESOLVED TODAY?";
            const string q5alt = "WITH THE LAST PERSON YOU SPOKE WITH IN MIND, HOW EASY";

            string[] output = new string[rowcount];
            output[0] = result[0]; //Type
            output[1] = result[1]; //TimeStamp

            int index;

            index = Array.IndexOf(result, "CONTACT ID");
            if (index > 0 && result[index + 1] != "VRUKEY")
            {
                output[2] = result[index + 1]; //Contact Id
            }

            index = Array.IndexOf(result, "VRUKEY");
            if (index > 0 && result[index + 1] != "RESERVATION NUMBER")
            {
                output[3] = result[index + 1]; //VRUKey
            }

            index = Array.IndexOf(result, "RESERVATION NUMBER");
            if (index > 0 && result[index + 1] != "THE SURVEY WAS TAKEN IN")
            {
                output[4] = result[index + 1]; //Reservation Number
            }

            index = Array.IndexOf(result, "THE SURVEY WAS TAKEN IN");
            if (index > 0)
            {
                output[5] = result[index]; //The survey was taken in
            }

            index = Array.IndexOf(result, "THE SURVEY WAS TAKEN IN");
            if (index > 0 && result[index + 1] != q1)
            {
                output[6] = result[index + 1]; //English
            }

            index = Array.IndexOf(result, q1);
            if (index > 0 && result[index + 1] != q2)
            {
                output[7] = result[index + 1]; //Answer 1
            }

            index = Array.IndexOf(result, q2);
            if (index > 0 && result[index + 1] != q3)
            {
                output[8] = result[index + 1]; //Answer 2
            }

            index = Array.IndexOf(result, q3);
            if (index > 0 && result[index + 1] != q4)
            {
                output[9] = result[index + 1]; //Answer 3
            }

            index = Array.IndexOf(result, q4);
            if (index > 0 && result[index + 1] != q5)
            {
                output[10] = result[index + 1]; //Answer 4
            }
            else
            {
                //try again       
                index = Array.FindIndex(result, t => t != null && t.Contains(q4alt));
                if (index > 0 && result[index + 1] != q5)
                {
                    output[10] = result[index + 1]; //Answer 4
                }
            }

            index = Array.IndexOf(result, q5);
            if (index > 0)
            {
                output[11] = result[index + 1]; //Answer 5
            }
            else
            {
                //try again
                index = Array.FindIndex(result, t => t != null && t.Contains(q5alt));
                if (index > 0)
                {
                    output[11] = result[index + 1]; //Answer 5
                }
            }

            return output;
        }

    }
}
