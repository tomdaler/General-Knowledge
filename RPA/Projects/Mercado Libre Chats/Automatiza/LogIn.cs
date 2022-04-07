using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Automatiza
{
    class LogIn
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public void Logging(IWebDriver dr1)
        {
            Funciones fx = new Funciones();
            IWebElement element = fx.FindElement(dr1, "username", "name", " Credenciales, Pagina Inicio");

            string username = "ext_todale";
            string passwd = "Levitico#7";

            username = ConfigurationManager.AppSettings["username"].ToString();
            passwd = ConfigurationManager.AppSettings["passwd"].ToString();
            System.Threading.Thread.Sleep(2000);

            // user id
            element.SendKeys(username);

            // password
            element = dr1.FindElement(By.Name("password"));
            element.SendKeys(passwd);
   
            // boton
            dr1.FindElement(By.ClassName("auth0-label-submit")).Click();
            //element = fx.FindElement(dr1, "btn-info", "class", "Button Search Pagina Principal");
            
            while (true)
            {
                fx.delay(1);
                IList<IWebElement> links = dr1.FindElements(By.TagName("a"));
                foreach (var link in links)
                {
                    if (link.Text == "Estado de agentes" || link.Text == "Status de agentes")
                    {
                        link.Click();
                        return;
                    }
                }
            }
        }

        public void EachLOB(IWebDriver dr1)
        {
            string ListLOBs = ConfigurationManager.AppSettings["LOBs"].ToString();
            string[] LOBs = ListLOBs.Split(',');
            JS js = new JS();

            Data.dtAgents = null;
            Data.dtTracker = null;
            string DataLog = "";

            foreach (string LOB in LOBs)
            {
                IJavaScriptExecutor executor = dr1 as IJavaScriptExecutor;

                string carregar = js.LoadLOB(LOB);
                executor.ExecuteScript(js.LoadLOB(LOB));
                //string pagina = dr1.PageSource;

                Scroll scroll = new Scroll();
                DataLog = DataLog + "  "+ scroll.Scrolling(dr1, LOB);
            }

            int hora = DateTime.Now.TimeOfDay.Hours;
            int minuto = DateTime.Now.TimeOfDay.Minutes;
            int seg = DateTime.Now.TimeOfDay.Seconds;

            log.Info(hora.ToString()+":"+minuto.ToString()+":"+seg.ToString()+" "+DataLog);
        }
    }
}
