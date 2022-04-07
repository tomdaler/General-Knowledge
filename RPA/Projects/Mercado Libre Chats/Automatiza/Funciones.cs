using OpenQA.Selenium;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Windows;

namespace Automatiza
{
    class Funciones
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public void delay(int seg)
        {
            System.Threading.Thread.Sleep(seg * 1000);
        }

  
       


        public string SumTimes(string OldStatus, double period)
        {
            string retorno = "";
            if (OldStatus == "online") retorno = " set Online1 = Online1 + " + period.ToString();
            if (OldStatus == "training_ac") retorno = " set training = training + " + period.ToString();
            if (OldStatus == "training_km") retorno = " set training_km = training_km + " + period.ToString();
            if (OldStatus == "descanso") retorno = " set Break1 = Break1 + " + period.ToString();

            if (OldStatus == "offline") retorno = " set Offline1 = Offline1 + " + period.ToString();
            if (OldStatus == "help_low_priority") retorno = " set help_low = help_low + " + period.ToString();
            if (OldStatus == "evento") retorno = " set Event1 = Event1 + " + period.ToString();

            if (OldStatus == "post_chat") retorno = " set PostCall = PostCall + " + period.ToString();
            if (OldStatus == "help_low_priority") retorno = " set help_low = help_low + " + period.ToString();
            if (OldStatus == "help_exclusive") retorno = " set help = help + " + period.ToString();

            if (OldStatus == "coaching") retorno = " set coaching = coaching + " + period.ToString();
            if (OldStatus == "expert_trainer") retorno = " set expert = expert + " + period.ToString();

            if (OldStatus == "operational_failure") retorno = " set failure_op = failure_op + " + period.ToString();
            if (OldStatus == "systematic_failure") retorno = " set failure_sys = failure_sys + " + period.ToString();

            return retorno;
        }
     
        public string GetCaseNo(string Chat)
        {
            //string Chat = " do caso 123456 adfd";
            int pos1 = Chat.IndexOf("do caso");
            int pos2 = Chat.IndexOf(" ", pos1 + 5);
            string NoCaso = Chat.Substring(pos1 + 8, pos2 - pos1 - 1);
            return NoCaso;
        }

        public IWebElement FindElement(IWebDriver dr1, string id, string tipo, string elemento)
        {
            IWebElement element = null;
            int i = 0;
            while (element == null)
            {
                try
                {
                    if (tipo=="name") element = dr1.FindElement(By.Name(id));
                    if (tipo == "class") element = dr1.FindElement(By.ClassName(id));
                }
                catch (Exception)
                {

                }
                if (element == null) delay(2);
                i++;
                if (i> 100)
                {
                    // error
                    SendEmail send = new SendEmail();
                    send.SendEmails("Error MeLi","No se encontro elemento "+elemento);
                    System.Environment.Exit(0);
                }
            }
            return element;
        }
    }
}
