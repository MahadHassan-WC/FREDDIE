using System;
using System.Collections.Generic;
using SqlSvrMeta;
using System.IO;

namespace FREDDIE
{
    public class FREDDIE_INSERT
    {
        private bool _INSERT_A_ROW;
        private bool _INSERT_A_ROW_WHERE_NOT_EXISTS;
        private bool _INSERT_CUSTOM;
        private const String Q = @"""";
        private const String T1 = "\t";
        private const String T2 = "\t\t";
        private const String T3 = "\t\t\t";
        private const String T4 = "\t\t\t\t";
       

       

        private string CONVERT_TO_PASCAL(string S)
        {
            string[] Myarray = S.Split('_');
            string ReturnValue = null;
            for(int i =0; i < Myarray.Length; i++)
            {
                if (i == 0)
                {
                    ReturnValue += Myarray[i].ToUpper();
                }
                else
                {
                    ReturnValue += S.Substring(i, 1);
                }
            }
            return ReturnValue;
        }
        private string CONVERT_TO_CAMEL(string S)
        {
            string[] Myarray = S.Split('_');
            string ReturnValue = null;
            for (int i = 0; i < S.Length; i++)
            {
                if (i == 0)
                {
                    ReturnValue += S.Substring(i, 1).ToLower();
                }
                else
                {
                    ReturnValue += S.Substring(i, 1);
                }
            }
            return ReturnValue;
        }
        public string GENERATE_INSERT_METHODS(string SERVER_NAME, string DATABASE_NAME, string TABLE_NAME, int OBJECT_ID, bool bWriteFile)
        {
            string INSERT_STRING = null;
            SQLSVR_METADATA sqlsvr = new SQLSVR_METADATA();
            sqlsvr.SERVER_NAME = SERVER_NAME;
            sqlsvr.DATABASE_NAME = DATABASE_NAME;
            sqlsvr.TABLE_NAME = TABLE_NAME;
            sqlsvr.OBJECT_ID = OBJECT_ID.ToString();
            List<CS_SQLSERVER_COLUMN_MATRIX> lst = new List<CS_SQLSERVER_COLUMN_MATRIX>();
            lst = sqlsvr.GET_CS_COLUMN_MATRIX_TABLE();
            sqlsvr = null;
            string FileName = lst[0].CS_OBJECT_NAME.ToString() + ".cs";
            if (INSERT_A_ROW)
            {
                INSERT_STRING += GENERATE_INSERT_A_ROW(lst);
            }
            if(INSERT_A_ROW_WHERE_NOT_EXISTS)
            {
                INSERT_STRING += GENERATE_INSERT_A_ROW_WHERE_NOT_EXISTS(lst);
            }
            if (bWriteFile)
            { WriteFile(@"C:\_Freddie\" + SERVER_NAME + @"\" + DATABASE_NAME + @"\TABLES\", FileName, INSERT_STRING); }
            return INSERT_STRING;
        }

        private void WriteFile(string FILE_PATH, string FILE_NAME, string MyTEXT)
        {
            if (!Directory.Exists(FILE_PATH))
            {
                Directory.CreateDirectory(FILE_PATH);
            }
            if (File.Exists(FILE_PATH + FILE_NAME))
            {
                File.Delete(FILE_PATH + FILE_NAME);
            }
            FileStream FileStream = new FileStream(FILE_PATH + FILE_NAME, FileMode.Create, FileAccess.Write);
            StreamWriter StreamWriter = new StreamWriter(FileStream);
            StreamWriter.WriteLine(MyTEXT);
            StreamWriter.Close();
            FileStream = null;
            StreamWriter = null;

        }
        public bool INSERT_A_ROW
        {
            get
            {
                return _INSERT_A_ROW;
            }
            set
            {
                _INSERT_A_ROW = value;
            }
        }
        public bool INSERT_A_ROW_WHERE_NOT_EXISTS
        {
            get
            {
                return _INSERT_A_ROW_WHERE_NOT_EXISTS;
            }
            set
            {
                _INSERT_A_ROW_WHERE_NOT_EXISTS = value;
            }
        }

        public bool INSERT_CUSTOM
        {
            get
            {
                return _INSERT_CUSTOM;
            }
            set
            {
                _INSERT_CUSTOM = value;
            }
        }
        private string GENERATE_SQL_CONSTRUCTOR(List<CS_SQLSERVER_COLUMN_MATRIX> lst)
        {
            return "DATABASE_CLASS_NAME = this.GetType().Name;" + Environment.NewLine +
                             "DATABASE_DDL_TYPE = " + Q + "NA" + Q + ";" + Environment.NewLine +
                             "DATABASE_DML_TYPE = " + Q + "INSERT" + Q + ";" + Environment.NewLine +
                             "DATABASE_NAME = DEFAULT_DB;" + Environment.NewLine +
                             "DATABASE_OBJECT_TYPE = " + Q + "TABLE" + Q + ";" + Environment.NewLine;
        }
        private string SQL_INSERT_A_ROW(List<CS_SQLSERVER_COLUMN_MATRIX> lst)
        {
            string SQL = "const string SQL_INSERT_A_ROW = @" + Q + "INSERT INTO dbo." + lst[0].CS_DB_OBJECT_NAME + Environment.NewLine + T3 + "(" + Environment.NewLine;

            for(int i = 0;i<lst.Count; i++)
            {
                if (i < lst.Count-1)
                { SQL += T3 + lst[i].COLUMN_NAME + "," + Environment.NewLine; }
                else
                { SQL += T3 + lst[i].COLUMN_NAME   + Environment.NewLine; }
            }
            SQL += T3 + ")" + Environment.NewLine;
            SQL += T3 + " VALUES " + Environment.NewLine;
            SQL += T3 + "(" + Environment.NewLine;


            for (int i = 0; i < lst.Count; i++)
            {
                if (i < lst.Count-1)
                { SQL += T3   + lst[i].DB_PARM_VALUE + "," + Environment.NewLine; }
                else
                { SQL += T3   + lst[i].DB_PARM_VALUE + Environment.NewLine; }
            }

            return  SQL  + T3 +   ")" + Q + ";" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
        }
        private string SQL_INSERT_A_ROW_WHERE_NOT_EXISTS(List<CS_SQLSERVER_COLUMN_MATRIX> lst)
        {
            string SQL = "const string SQL_INSERT_A_ROW_WHERE_NOT_EXISTS = @" + Q + "IF NOT EXISTS (SELECT * FROM dbo." + lst[0].CS_DB_OBJECT_NAME + " WHERE " + Environment.NewLine ;
            int PK_COUNT = 0;
            int PK_TICKS = 0;

            for (int i = 0; i < lst.Count; i++)
            {
                if(lst[i].PK_YN == "Y")
                {
                    PK_COUNT++;
                
                }
            }

            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].PK_YN == "Y")
                {
                    PK_TICKS++;
                    if (PK_TICKS < PK_COUNT)
                    { SQL += T4 + lst[i].COLUMN_NAME + " = " + lst[i].DB_PARM_VALUE + " AND " + Environment.NewLine; }
                    else
                    { SQL += T4 + lst[i].COLUMN_NAME + " = " + lst[i].DB_PARM_VALUE + " ) " + Environment.NewLine + Environment.NewLine + T3 + "BEGIN " + Environment.NewLine + Environment.NewLine; }
                }
            }
            SQL += T4 + "INSERT INTO DBO." + lst[0].CS_DB_OBJECT_NAME + "(" + Environment.NewLine + Environment.NewLine;

            for (int i = 0; i < lst.Count; i++)
            {
                if (i < lst.Count - 1)
                { SQL += T4 + lst[i].COLUMN_NAME + "," + Environment.NewLine; }
                else
                { SQL += T4 + lst[i].COLUMN_NAME + Environment.NewLine; }
            }
            SQL += T4 + ")" + Environment.NewLine;
            SQL += T4 + " VALUES " + Environment.NewLine;
            SQL += T4 + "(" + Environment.NewLine;


            for (int i = 0; i < lst.Count; i++)
            {
                if (i < lst.Count - 1)
                { SQL += T4 + lst[i].DB_PARM_VALUE + "," + Environment.NewLine; }
                else
                { SQL += T4 + lst[i].DB_PARM_VALUE + Environment.NewLine; }
            }

            return SQL + T4 + ")" + Environment.NewLine   +T3 + " END " + Q + ";" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
        }
        public string GENERATE_INSERT_A_ROW(List<CS_SQLSERVER_COLUMN_MATRIX> lst)
        {

 

            try
            {
                string parms = null;

                for (int i = 0; i < lst.Count; i++)
                {

                    if (lst[i].CS_PARM_VALUE.ToUpper() != "NA")
                    {
                        parms += lst[i].CS_PARM_VALUE + Environment.NewLine;

                    }
                    
                }

                return SQL_INSERT_A_ROW(lst) + 
                    "public Boolean INS(" + lst[0].CS_OBJECT_NAME + " O)" + Environment.NewLine +
                       "{" + Environment.NewLine +
                      "try{" + Environment.NewLine +
                               "DATABASE_METHOD_NAME  = System.Reflection.MethodBase.GetCurrentMethod().Name;" + Environment.NewLine +
                               "DATABASE_CMD_SQL = SQL_INSERT_A_ROW;  " + Environment.NewLine +
                                  "DATABASE_DML_TYPE = " + Q + "INSERT" + Q + ";" + Environment.NewLine +


                               "DATABASE_MESSAGE_LEVEL = 0;  " + Environment.NewLine +

                               "SqlConnection conn = GetConnectionToDatabase(DEFAULT_DB);" + Environment.NewLine +
                               "SqlCommand cmd = new SqlCommand(this.DATABASE_CMD_SQL, conn);" + Environment.NewLine +
                               "cmd.CommandTimeout = 900000;" + Environment.NewLine +

                 "DATABASE_CONNECTION_STRING = conn.ConnectionString;" + Environment.NewLine +
                " DATABASE_CONNECTION_VALUES = GET_SQLCONNECTION_DETAILS(conn);" + Environment.NewLine + 

                "conn.Open();" + Environment.NewLine + Environment.NewLine +
                               "///////////////PARAMETERS" + Environment.NewLine + Environment.NewLine +
                                 parms + Environment.NewLine + Environment.NewLine +


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
        public string GENERATE_INSERT_A_ROW_WHERE_NOT_EXISTS(List<CS_SQLSERVER_COLUMN_MATRIX> lst)
        {
            try
            {
                return SQL_INSERT_A_ROW_WHERE_NOT_EXISTS(lst) +
                 " public Boolean INS_WHERE_NOT_EXISTS(" + lst[0].CS_OBJECT_NAME + " O)" + Environment.NewLine +
                       "{" + Environment.NewLine +
                         "try{" + Environment.NewLine +
                               "DATABASE_METHOD_NAME  = System.Reflection.MethodBase.GetCurrentMethod().Name;" + Environment.NewLine +
                               "DATABASE_CMD_SQL = SQL_INSERT_A_ROW_WHERE_NOT_EXISTS;  " + Environment.NewLine +
                                         "DATABASE_DML_TYPE = " + Q + "INSERT" + Q + ";" + Environment.NewLine +
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
        public string GENERATE_INSERT_CUSTOM(List<CS_SQLSERVER_COLUMN_MATRIX> lst)
        {
            try
            {
                return "";
            }
            catch (Exception e)
            {
                return "ERROR: " + e.Message;
            }
        }


    }
}
