namespace Task
{
    class ORACLE_SQL
    {
        public string ATTUID_SQL(string EmpID)
        {
            string query = "SELECT EMP_XREF AS ATTUID, SOC_SEC_CODE AS EMPLID from UDW.VW_XREFPERSON_ATTUID ";

            if (EmpID != "")
            {
                query = query + " WHERE SOC_SEC_CODE = '" + EmpID.Trim() + "' ";
                query = query + " AND ";
            }
            else
                query = query + " WHERE ";

            query = query + " SYSDATE >= START_DATE ";

            query = query + " AND ( END_DATE IS NULL OR EXTRACT(year FROM END_DATE) = 2099) ";
            query = query + " AND APP_CODE = 'ATUID' ";

            return query;

           

        }

        public string Verizon()
        {
            string sql = "SELECT T1.LOGID AS VerizonEmpID, T2.LOGID AS STARRID,  T2.SRC_PERSON_ID   ";
            sql = sql + "FROM  UDW.DIM_XREFPERSON T1 INNER JOIN UDW.DIM_XREFPERSON T2  ON T2.SRC_PERSON_ID = T1.SRC_PERSON_ID, ";
            sql = sql + "      udw.DIM_XREFPERSON x, udw.DIM_JOBSYSTEM j ";
            sql = sql + "WHERE j.JOB_SYSTEM_NAME = ('GAXEID') ";
            sql = sql + "and x.SRC_PERSON_ID = t1.SRC_PERSON_ID ";
            sql = sql + "and x.JOB_SYSTEM_ID = j.SK_JOBSYSTEM_ID and j.SRC_DELETE_IND = 'N' ";
            sql = sql + "AND T2.JOB_SYSTEM_ID = 32407987   ";
            sql = sql + "AND ( T1. DW_END_DT >= '31-Dec-2099' OR T1.DW_END_DT IS NULL) ";
            return sql;
        }
    }
}
