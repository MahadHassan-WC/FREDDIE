using System;
using System.Collections.Generic;
using SqlSvrMeta;

public class FREDDIE_UPDATE
    {
    private bool _UPDATE_A_ROW;
    private bool _UPDATE_CUSTOM;
    private const String Q = @"""";
    private const String T1 = "\t";
    private const String T2 = "\t\t";
    private const String T3 = "\t\t\t";
    private const String T4 = "\t\t\t\t";
    public bool UPDATE_A_ROW
    {
        get
        {
            return _UPDATE_A_ROW;
        }
        set
        {
            _UPDATE_A_ROW = value;
        }
    }

    public string GENERATE_UPDATE_METHODS(string SERVER_NAME, string DATABASE_NAME, string TABLE_NAME, int OBJECT_ID)
    {
        string MySTRING = null;
        SQLSVR_METADATA sqlsvr = new SQLSVR_METADATA();
        sqlsvr.SERVER_NAME = SERVER_NAME;
        sqlsvr.DATABASE_NAME = DATABASE_NAME;
        sqlsvr.TABLE_NAME = TABLE_NAME;
        sqlsvr.OBJECT_ID = OBJECT_ID.ToString();
        List<CS_SQLSERVER_COLUMN_MATRIX> lst = new List<CS_SQLSERVER_COLUMN_MATRIX>();
        lst = sqlsvr.GET_CS_COLUMN_MATRIX_TABLE();
        sqlsvr = null;

        if (UPDATE_A_ROW)
        {
            MySTRING += GENERATE_UPDATE_A_ROW(lst);
        }

        return MySTRING;
    }
    public string GENERATE_UPDATE_A_ROW(List<CS_SQLSERVER_COLUMN_MATRIX> lst)
    {



        try
        {
            string parmsPK = null;
            string parms = null;
            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].PK_YN == "Y")
                {
                    parmsPK += lst[i].CS_PARM_VALUE + Environment.NewLine;
                }
            }
            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].PK_YN != "Y")
                {




                    if (lst[i].CS_PARM_VALUE.ToUpper() != "NA") 
                    {
                        parms += lst[i].CS_PARM_VALUE + Environment.NewLine;

                    }
                   




                    
                }
            }
            return SQL_UPDATE_A_ROW(lst) +
                "public Boolean UPD(" + lst[0].CS_OBJECT_NAME + " O)" + Environment.NewLine +
                   "{" + Environment.NewLine +
                  "try{" + Environment.NewLine +
                           "DATABASE_METHOD_NAME  = System.Reflection.MethodBase.GetCurrentMethod().Name;" + Environment.NewLine +
                           "DATABASE_CMD_SQL = SQL_UPDATE_A_ROW;  " + Environment.NewLine +
                           "DATABASE_DML_TYPE = " + Q + "UPDATE" + Q + ";" + Environment.NewLine +
                           "DATABASE_MESSAGE_LEVEL = 0;  " + Environment.NewLine +

                           "SqlConnection conn = GetConnectionToDatabase(DEFAULT_DB);" + Environment.NewLine +
                           "SqlCommand cmd = new SqlCommand(this.DATABASE_CMD_SQL, conn);" + Environment.NewLine +
                           "cmd.CommandTimeout = 900000;" + Environment.NewLine +

             "DATABASE_CONNECTION_STRING = conn.ConnectionString;" + Environment.NewLine +
            " DATABASE_CONNECTION_VALUES = GET_SQLCONNECTION_DETAILS(conn);" + Environment.NewLine +

            "conn.Open();" + Environment.NewLine + Environment.NewLine +

                "///////////////COLUMNS TO UPDATE PARAMETERS" + Environment.NewLine + Environment.NewLine +
                             parms + Environment.NewLine + Environment.NewLine +




                           "///////////////PK PARAMETERS" + Environment.NewLine + Environment.NewLine +
                             parmsPK + Environment.NewLine + Environment.NewLine +


                    "DATABASE_ROWS_AFFECTED= cmd.ExecuteNonQuery();" + Environment.NewLine +
                    "conn.Close();" + Environment.NewLine +
                    "conn = null;" + Environment.NewLine +
                    "cmd = null;" + Environment.NewLine +
                       "DATABASE_MESSAGE_LEVEL = 0;  " + Environment.NewLine +


            " return true;" + Environment.NewLine +
                     " }" + Environment.NewLine +
                       " catch(SqlException SqlEx)" + Environment.NewLine +
                              "{" + Environment.NewLine +
                                      "DATABASE_MESSAGE = SqlEx.Message;" + Environment.NewLine +
                                        "DATABASE_MESSAGE_LEVEL = -1;  " + Environment.NewLine +
                                           " return false;" + Environment.NewLine +
                                              " }" + Environment.NewLine +
                                                 " }" + Environment.NewLine;

        }
        catch (Exception e)
        {
            return "ERROR: " + e.Message;
        }

    }
    private string SQL_UPDATE_A_ROW(List<CS_SQLSERVER_COLUMN_MATRIX> lst)
    {
        string SQL = "const string SQL_UPDATE_A_ROW = @" + Q + "UPDATE dbo." + lst[0].CS_DB_OBJECT_NAME + " SET " + Environment.NewLine;

        int PK_COUNT = 0;
        int PK_COUNT2 = 0;
        for (int i = 0; i < lst.Count; i++)
        {
            if (lst[i].PK_YN == "Y")
            {
                PK_COUNT++;
            }

        }
        for (int i = 0; i < lst.Count; i++)
        {
            if (lst[i].PK_YN != "Y")
            {
               
                if (i < lst.Count -1)
                {
                    SQL += T3 + lst[i].COLUMN_NAME + " = " + lst[i].DB_PARM_VALUE + " , " + Environment.NewLine;  
                }
                else
                {
                    SQL += T3 + lst[i].COLUMN_NAME + " = " + lst[i].DB_PARM_VALUE + Environment.NewLine + T2 + " WHERE " + Environment.NewLine;  
                }
            }
        }


        for (int i = 0; i < lst.Count; i++)
        {
            if (lst[i].PK_YN == "Y")
            {
                PK_COUNT2++;
                if (PK_COUNT2 < PK_COUNT)
                {
                    SQL += T3 + lst[i].COLUMN_NAME + " = " + lst[i].DB_PARM_VALUE + " AND " + Environment.NewLine;
                }
                else
                {
                    SQL += T3 + lst[i].COLUMN_NAME + " = " + lst[i].DB_PARM_VALUE + Environment.NewLine  ;
                }
            }
        }

        return SQL + T3 + Q + ";" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
    }
}
 
