using System.Data;
namespace ConsoleApp1
{
    static class Data
    {
        public static DataTable WK = new DataTable();
        public static DataTable getWK()
        {
            if (WK == null || WK.Rows.Count == 0)
            {
                string sql = "SELECT EmpID, FirstName, MiddleName, LastName, Gender, Location, DOB, LOCATIONADDRESS1, ";
                sql = sql + "LOCATIONADDRESS2, PRIMARYADDRESS1, PRIMARYADDRESS2, MSA from declarationformsworkday";

                Exposure.DataBase DB = new Exposure.DataBase();
                WK = DB.GetTable(sql);
            }
            return WK;
        }
    }
}
