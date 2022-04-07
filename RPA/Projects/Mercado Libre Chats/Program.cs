using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Configuration;
using System.Data;

namespace Automatiza
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static IWebDriver dr1 = null;

        [STAThread]
        static void Main(string[] args)
        {
            //Procesar pp = new Procesar();
            //pp.TotalByLOB();

            //DataTable dt = Data.getAgents();
            //int dd = dt.Rows.Count;

            //DataTable dt2 = Data.dtAgents;
            //dd = dt.Rows.Count;

            //DataTable dt3 = Data.getAgents();
            //int dd3 = dt3.Rows.Count;

            log4net.Config.XmlConfigurator.Configure(); // start logging
            log.Info(string.Format("Start {0}", DateTime.Now));

            dr1 = new ChromeDriver();
            dr1.Navigate().GoToUrl("www.google.com");


        }
    }
}
