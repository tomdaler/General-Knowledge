using System;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;

namespace RunMacro
{
    class Macros
    {
        public void RunMacro()
        {
            //_Application 
                
            Excel.Application excel = new Excel.Application();
                    
            excel.Visible = true;
            string pathToExcelXlsmFile = "C:\\temp\\COVID.xlsm";

            int sheetNumber = 1;
            Workbook wb = excel.Workbooks.Open(pathToExcelXlsmFile);
            Worksheet ws = wb.Worksheets[sheetNumber];

           // RunMacro(excel, new Object[] { "Worksheet01.xlsm!First_Macro" });


            //foreach (Microsoft.Vbe.Interop.VBProject vbproject in app.VBE.VBProjects) //app=new Excel.Application()
            //{

            //    foreach (Microsoft.Vbe.Interop.VBComponent vbcomponent in vbproject.VBComponents)
            //    {

            //        //vbcomponent.CodeModule.ToString()
            //        MessageBox.Show(vbcomponent.CodeModule.ToString());
            //        vbcomponent.CodeModule.CodePane.Show();
            //    }
            //}

            excel.GetType().InvokeMember("Run",
                System.Reflection.BindingFlags.Default | System.Reflection.BindingFlags.InvokeMethod,
                null, excel, new Object[] { "COVID.xlsm!Module2", "UpdateSheets" });
            
            excel.Quit();
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(excel);
        }
    }
}
