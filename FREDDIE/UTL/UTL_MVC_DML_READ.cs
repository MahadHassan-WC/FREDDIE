using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;

 
    public class UTL_MVC_DML_READ
    {
    private List<CS_METHOD> CS_METHOD_LIST;
    private String _CS_READ_FOLDER;
    private String _CS_READ_CLASS_FILE_NAME;
    private String _CS_READ_CLASS;
    private const String LOG_FILE = null;
    private String _SQL_WHERE = " WHERE " + Environment.NewLine;
    private Boolean _PK = false;
    private Boolean _PK_CONTAINS_IDENTITY_FIELD = false;
    private int _COLUMNS_IN_PK = 0;
    private const String Q = @"""";
    private const String T1 = "\t";
    private const String T2 = "\t\t";
    private const String T3 = "\t\t\t";
    private const String T4 = "\t\t\t\t";
    private const String T5 = "\t\t\t\t\t";
    private const String T6 = "\t\t\t\t\t\t";
    private const String NL = "\n";
    public System.String CS_READ_FOLDER
    {
        get { return _CS_READ_FOLDER; }
        set { _CS_READ_FOLDER = value; }

    }
    public System.String CS_READ_CLASS
    {
        get { return _CS_READ_CLASS; }

    }
    public System.String CS_READ_CLASS_FILE_NAME
    {
        get { return _CS_READ_CLASS_FILE_NAME; }

    }
    
    public string GENERATE_READ_TABLE(string SERVER_NAME, string DATABASE_NAME, string OBJECT_NAME, int OBJECT_ID)
    {
        CS_METHOD_LIST = new List<CS_METHOD>();
        List<FREDS_MATRIX> COLUMNS_TO_SELECT = UTL_GET_ATTRIBUTES_TABLE(SERVER_NAME, DATABASE_NAME.ToUpper(), OBJECT_ID, false);
        _CS_READ_CLASS_FILE_NAME = UTL_GENERATE_READ_CLASS_NAME(OBJECT_NAME) + ".cs";
        _CS_READ_CLASS = "///////READ CLASS CREATED ON " + DateTime.Now + Environment.NewLine + Environment.NewLine +
            "using Microsoft.Data.SqlClient;" + Environment.NewLine +
            "using System.Collections.Generic; " + Environment.NewLine +
            "public class " + UTL_GENERATE_READ_CLASS_NAME(OBJECT_NAME) + " : dbBase" + Environment.NewLine +
            "{" + Environment.NewLine  ;

        ///CREATE EACH METHOD THEN LOOP THROUGH THEM IN ORDER TO ADD EACH METHOD TO THE CLASS
        METHOD_SEL_ALL_DEFAULT(COLUMNS_TO_SELECT, OBJECT_NAME);
        if (_PK)
        {
          //  METHOD_SEL_A_ROW_BY_PK_DEFAULT(COLUMNS_TO_SELECT, OBJECT_NAME);
        }

        foreach (CS_METHOD m in CS_METHOD_LIST)
        {
            _CS_READ_CLASS += m.CS;
        }
        _CS_READ_CLASS += "}   /////END OF READ CLASS" + Environment.NewLine + Environment.NewLine;
        UTL_SAVE_FILE(CS_READ_FOLDER, CS_READ_CLASS_FILE_NAME, CS_READ_CLASS);
        ///  List<FREDS_MATRIX> ATTRIBUTES_PK = UTL_GET_ATTRIBUTES_TABLE(SERVER_NAME, DATABASE_NAME.ToUpper(), OBJECT_ID, true);

        return _CS_READ_CLASS;
    }
    private String METHOD_SEL_ALL_DEFAULT(List<FREDS_MATRIX> COLUMNS_TO_SELECT, String OBJECT_NAME)
    {
        CS_METHOD CS_METHOD = new CS_METHOD
        {
            METHOD_TYPE = "SELECT",
            CUSTOM_METHOD = false,
            METHOD_NAME = "GetAll",


        };
        int count = 0;
        String ModelName = UTL_GENERATE_MODEL_CLASS_NAME(OBJECT_NAME);
        CS_METHOD.CS += T1 + "public List< " + ModelName + ">" + CS_METHOD.METHOD_NAME + "()" + Environment.NewLine;
        CS_METHOD.CS += T1 + "{" + Environment.NewLine;
        CS_METHOD.CS += T2 + "try" + Environment.NewLine;
        CS_METHOD.CS += T2 + "{" + Environment.NewLine;
        CS_METHOD.CS += T3 + "List< " + ModelName + "> lst = new List<" + ModelName + ">();" + Environment.NewLine;

        CS_METHOD.CS += T3 + "using (SqlConnection conn = new SqlConnection(GetConnectionString()))" + Environment.NewLine;

        CS_METHOD.CS += T3 + "{" + Environment.NewLine;



        CS_METHOD.CS += T4 + "using (SqlCommand cmd = new SqlCommand(" + Q + UTL_GENERATE_USP_SEL_ALL(OBJECT_NAME) + Q +   ", conn))" + Environment.NewLine;
        CS_METHOD.CS += T4 + "{" + Environment.NewLine;

        CS_METHOD.CS += T5 + " " + Environment.NewLine;
        CS_METHOD.CS += T5 + "conn.Open(); " + Environment.NewLine;
        CS_METHOD.CS += T5 + "cmd.CommandType = System.Data.CommandType.StoredProcedure; " + Environment.NewLine;
        CS_METHOD.CS += T5 + " SqlDataReader rdr = cmd.ExecuteReader();" + Environment.NewLine;

        CS_METHOD.CS += T5 + "while (rdr.Read())" + Environment.NewLine;
        CS_METHOD.CS += T5 + "{" + Environment.NewLine;
        CS_METHOD.CS += T6 + ModelName + " m = new " + ModelName + Environment.NewLine;



        CS_METHOD.CS += T6 + "{" + Environment.NewLine;

        foreach (FREDS_MATRIX f in COLUMNS_TO_SELECT)

        {
            count++;
            if (count < COLUMNS_TO_SELECT.Count )
            {
                CS_METHOD.CS += T6 + f.CS_READER_STRING + "," + Environment.NewLine;
            }
            else
            {
                CS_METHOD.CS += T6 + f.CS_READER_STRING + Environment.NewLine;
            }
           
        }


        CS_METHOD.CS += T6 + "}; ////end of new object" + Environment.NewLine;
        CS_METHOD.CS += T6 + "lst.Add(m);" + Environment.NewLine;
        CS_METHOD.CS += T5 + "} ///end of while loop" + Environment.NewLine;


        CS_METHOD.CS += T4 + "} ///end of cmd object" + Environment.NewLine;



        CS_METHOD.CS += T3 + "} /// end of conn object" + Environment.NewLine;


        CS_METHOD.CS += T3 + "return lst;  " + Environment.NewLine + Environment.NewLine;
        CS_METHOD.CS += T2 + "}" + Environment.NewLine;
        CS_METHOD.CS += UTL_GENERATE_CATCH_SECTION_OF_METHOD(true);
        CS_METHOD_LIST.Add(CS_METHOD);
        return CS_METHOD.CS;




        //public List<DbchangetypeModel> SEL_ALL()
        //{


        //       try
        //       {
        //           List<DbchangetypeModel> lst = new List<DbchangetypeModel>();

        //           using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        //           {
        //               using (SqlCommand cmd = new SqlCommand("usp_SEL_ALL_DBCHANGETYPE", conn))
        //               {
        //                   conn.Open();
        //                   cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                   SqlDataReader rdr = cmd.ExecuteReader();
        //                   while (rdr.Read())
        //                   {
        //                       DbchangetypeModel m = new DbchangetypeModel
        //                       {
        //                           DBCHANGETYPEID = System.Convert.ToInt32(rdr["DBCHANGETYPEID"]),
        //                           DBCHANGETYPE = (rdr["DBCHANGETYPE"]).ToString(),
        //                           INSDT = System.Convert.ToDateTime(rdr["INSDT"]),
        //                           UPDDT = System.Convert.ToDateTime(rdr["INSDT"]),
        //                           INSOPID = (rdr["INSOPID"]).ToString(),
        //                           UPDOPID = (rdr["UPDOPID"]).ToString()
        //                       };
        //                       lst.Add(m);
        //                   }
        //                   return lst;
        //               }
        //           }

        //       }
        //	catch ( SqlException ex)
        //	{
        //		return null;
        //	}
        //	finally
        //	{

        //	}
        //}



    }

    public List<FREDS_MATRIX> UTL_GET_ATTRIBUTES_TABLE(string SERVER_NAME, string DATABASE_NAME, int OBJECT_ID, Boolean PK_ONLY)
    {
        _PK = false;
        _PK_CONTAINS_IDENTITY_FIELD = false;
        _COLUMNS_IN_PK = 0;
        _SQL_WHERE = T3 + "WHERE " + Environment.NewLine;

        List<FREDS_MATRIX> LST = new List<FREDS_MATRIX>();
        MSSQL_CS_META_DATA db = new MSSQL_CS_META_DATA();
        DataSet DS_COLUMNS = db.GET_COLUMNS_IN_TABLE_DS(SERVER_NAME, DATABASE_NAME, OBJECT_ID);
        DataSet DS_PRIMARY_KEYS = db.GET_PRIMARY_KEYS_IN_TABLE_DS(SERVER_NAME, DATABASE_NAME, OBJECT_ID);
        _COLUMNS_IN_PK = DS_PRIMARY_KEYS.Tables[0].Rows.Count;
        if (_COLUMNS_IN_PK > 0)
        {
            _PK = true;
        }
        string OBJECT_NAME = null;
        try
        {
            foreach (DataRow R in DS_COLUMNS.Tables[0].Rows)
            {
                FREDS_MATRIX o = new FREDS_MATRIX
                {
                    COLUMN_ID = Convert.ToInt32(R["COLUMN_ID"]),
                    COLUMN_NAME = R["COLUMN_NAME"].ToString().ToUpper(),
                    CS_PARM_STRING = "cmd.Parameters.AddWithValue(" + Q + "@" + R["COLUMN_NAME"].ToString().ToUpper() + Q + "," + "o" + "." + R["COLUMN_NAME"].ToString().ToUpper() + ");",
                    DATABASE_NAME = DATABASE_NAME,
                    SQL_DATA_TYPE = R["TYPE_NAME"].ToString().ToUpper(),
                    SQL_IDENTITY_FIELD_YN = R["IS_IDENTITY"].ToString(),
                    SQL_IDENT_INCR = Convert.ToInt32(R["IDENT_INCR"]),
                    SQL_IDENT_SEED = Convert.ToInt32(R["IDENT_SEED"]),
                    SQL_IS_NULLABLE = R["IS_NULLABLE"].ToString(),
                    SQL_MAX_LENGTH = Convert.ToInt32(R["MAX_LENGTH"]),
                    SQL_VARIABLE_NAME = "@" + R["COLUMN_NAME"].ToString().ToUpper(),
                    SQL_PROC_PARM_STRING = "@" + R["COLUMN_NAME"].ToString().ToUpper() + " " + R["TYPE_NAME"].ToString().ToUpper(),
                    SQL_PRECISION = Convert.ToInt32(R["PRECISION"]),
                    SQL_SCALE = Convert.ToInt32(R["SCALE"]),
                    SQL_TABLE_SCHEMA = R["TABLE_SCHEMA"].ToString().ToUpper(),
                    OBJECT_ID = OBJECT_ID,
                    OBJECT_NAME = R["TABLE_NAME"].ToString().ToUpper(),
                    OBJECT_TYPE = "TABLE",
                    PK_YN = "N",
                    SERVER_NAME = SERVER_NAME,
                    INSERT_YN = "Y",
                    UPDATE_YN = "Y",
                    RESERVED_YN = "N",
                    CS_DATA_TYPE = "?",
                    CS_PRIVATE_VARIABLE = "?",
                    CS_PUBLIC_PROPERTY = "?"
                };
                OBJECT_NAME = o.OBJECT_NAME;

                foreach (DataRow r in DS_PRIMARY_KEYS.Tables[0].Rows)
                {
                    if (o.COLUMN_NAME == r["COLUMN_NAME"].ToString().ToUpper())
                    {
                        o.PK_YN = "Y";
                        o.UPDATE_YN = "N";
                    }
                }
                switch (o.SQL_DATA_TYPE.ToUpper())
                {
                    case "VARCHAR":
                        o.SQL_PROC_PARM_STRING = "@" + R["COLUMN_NAME"].ToString().ToUpper() + " " + o.SQL_DATA_TYPE + "(" + o.SQL_MAX_LENGTH + ")";
                        o.CS_READER_STRING = R["COLUMN_NAME"].ToString().ToUpper() + "=" + "(rdr[" + Q + R["COLUMN_NAME"].ToString().ToUpper() + Q + "]).ToString()";
                        break;
                    case "CHAR":
                        o.SQL_PROC_PARM_STRING = "@" + R["COLUMN_NAME"].ToString().ToUpper() + " " + o.SQL_DATA_TYPE + "(" + o.SQL_MAX_LENGTH + ")";
                        o.CS_READER_STRING = R["COLUMN_NAME"].ToString().ToUpper() + "=" + "(rdr[" + Q + R["COLUMN_NAME"].ToString().ToUpper() + Q + "]).ToString()";
                        break;
                    case "DECIMAL":
                        o.SQL_PROC_PARM_STRING = "@" + R["COLUMN_NAME"].ToString().ToUpper() + " " + o.SQL_DATA_TYPE + "(" + o.SQL_PRECISION + "," + o.SQL_SCALE + ")";
                        break;

                    case "DATETIME":
                        o.CS_READER_STRING = R["COLUMN_NAME"].ToString().ToUpper() + "=" + "System.Convert.ToDateTime(rdr[" + Q + R["COLUMN_NAME"].ToString().ToUpper() + Q + "])";
                        break;
                    case "INT":
                        o.CS_READER_STRING = R["COLUMN_NAME"].ToString().ToUpper() + "=" + "System.Convert.ToInt32(rdr[" + Q + R["COLUMN_NAME"].ToString().ToUpper() + Q + "])";
                        break;


                        //DBCHANGETYPEID = System.Convert.ToInt32(rdr["DBCHANGETYPEID"]),
                        //DBCHANGETYPE = (rdr["DBCHANGETYPE"]).ToString(),
                        //INSDT = System.Convert.ToDateTime(rdr["INSDT"]),
                        //UPDDT = System.Convert.ToDateTime(rdr["INSDT"]),
                        //INSOPID = (rdr["INSOPID"]).ToString(),
                        //UPDOPID = (rdr["UPDOPID"]).ToString()











                }
                if (o.SQL_IDENTITY_FIELD_YN == "1")
                {
                    o.UPDATE_YN = "N";
                    o.INSERT_YN = "N";
                    o.SQL_IDENTITY_FIELD_YN = "Y";
                    if (o.PK_YN == "Y")
                    {
                        _PK_CONTAINS_IDENTITY_FIELD = true;
                    }

                }
                else
                {
                    o.SQL_IDENTITY_FIELD_YN = "N";
                }
                if (Convert.ToBoolean(o.SQL_IS_NULLABLE))
                {
                    o.SQL_IS_NULLABLE = "Y";
                }
                else
                {
                    o.SQL_IS_NULLABLE = "N";
                }

                if (o.COLUMN_NAME == "INSDT" || o.COLUMN_NAME == "UPDDT")
                {
                    o.CS_PARM_STRING = "///// " + o.CS_PARM_STRING + " RESERVED COLUMN NAME AUTO GENERATED BY SQL SERVER";
                    o.RESERVED_YN = "Y";
                    o.SQL_VARIABLE_NAME = "GETDATE()";
                }
                if (o.COLUMN_NAME == "INSOPID" || o.COLUMN_NAME == "UPDOPID")
                {
                    o.CS_PARM_STRING = "///// " + o.CS_PARM_STRING + " RESERVED COLUMN NAME AUTO GENERATED BY SQL SERVER";
                    o.RESERVED_YN = "Y";
                    o.SQL_VARIABLE_NAME = "USER_NAME()";
                }


                if (o.COLUMN_NAME == "INSDT" || o.COLUMN_NAME == "INSOPID")
                {
                    o.INSERT_YN = "Y";
                    o.UPDATE_YN = "N";

                }
                if (o.COLUMN_NAME == "UPDDT" || o.COLUMN_NAME == "UPDOPID")
                {
                    o.INSERT_YN = "Y";
                    o.UPDATE_YN = "Y";
                }
                if (PK_ONLY)
                {
                    if (o.PK_YN == "Y")
                    {
                        LST.Add(o);
                    }
                }
                else
                {
                    LST.Add(o);
                }

                o = null;
            }
            DataTable DT_SCHEMA = db.GET_SCHEMA_TABLE_DT(SERVER_NAME, DATABASE_NAME, OBJECT_NAME);
            foreach (DataRow R in DT_SCHEMA.Rows)
            {
                foreach (FREDS_MATRIX o in LST)
                {
                    if (o.COLUMN_NAME.ToString().ToUpper() == R["ColumnName"].ToString().ToUpper())
                    {
                        o.CS_DATA_TYPE = R["DataType"].ToString();
                        o.CS_PRIVATE_VARIABLE = "private " + R["DataType"].ToString() + " _" + o.COLUMN_NAME.ToString().ToUpper() + ";" + Environment.NewLine;
                        o.CS_PUBLIC_PROPERTY = "public " + R["DataType"].ToString() + " " + o.COLUMN_NAME.ToString().ToUpper() + Environment.NewLine +
                            T1 + "{ " + Environment.NewLine +
                                T2 + "get { return _" + o.COLUMN_NAME.ToString().ToUpper() + "; }" + Environment.NewLine +
                                T2 + "set { _" + o.COLUMN_NAME.ToString().ToUpper() + " = value; }" + Environment.NewLine +
                            T1 + "} " + Environment.NewLine;

                    }
                }

            }
            db = null;
            int i = 0;
            if (_PK)
            {
                foreach (FREDS_MATRIX o in LST)
                {
                    i++;
                    if (o.PK_YN == "Y")
                    {
                        if (i < _COLUMNS_IN_PK)
                        {
                            _SQL_WHERE += T3 + o.COLUMN_NAME.ToString().ToUpper() + " = " + o.SQL_VARIABLE_NAME.ToString().ToUpper() + " AND " + Environment.NewLine;
                        }
                        else
                        {
                            _SQL_WHERE += T3 + o.COLUMN_NAME.ToString().ToUpper() + " = " + o.SQL_VARIABLE_NAME.ToString().ToUpper();
                        }

                    }
                }
            }
            return LST;
        }
        catch (Exception ex)
        {
            // MessageBox.Show(ex.Message);
            return null;
        }
    }

    private string UTL_GENERATE_USP_SEL_ALL(string OBJECT_NAME)
    {
        return "usp_SEL_ALL_" + OBJECT_NAME.ToUpper();
    }
    private string UTL_GENERATE_USP_SEL_BY_PK(string OBJECT_NAME)
    {
        return "usp_SEL_A_ROW_BY_PK_" + OBJECT_NAME.ToUpper();
    }

    private String UTL_GENERATE_CATCH_SECTION_OF_METHOD(Boolean ReadMethod)
    {
        String s = null;
        s += T2 + "catch ( SqlException SqlException)" + Environment.NewLine;
        s += T2 + "{" + Environment.NewLine;
        //    s += T3 + "LOG_ERROR_EVENT_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|" + Q + @" + SqlException.Message);" + Environment.NewLine;
        if (ReadMethod)
        {
            s += T3 + "return null;" + Environment.NewLine;
        }
        else
        {
            s += T3 + "return false;" + Environment.NewLine;
        }
        s += T2 + "}" + Environment.NewLine;
        s += T2 + "finally" + Environment.NewLine;
        s += T2 + "{" + Environment.NewLine;
        //   s += T3 + " LOG_INFORMATION_EVENT_VERBOSE_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|finally" + Q + @");" + Environment.NewLine;
        s += T3 + " cmd = null;" + Environment.NewLine;
        s += T2 + "}" + Environment.NewLine;
        s += T1 + "}" + Environment.NewLine + Environment.NewLine;
        return s;
    }


    private void UTL_SAVE_FILE(string FOLDER_NAME, string FILE_NAME, string FILE_CONTENT)
    {

        if (!Directory.Exists(FOLDER_NAME))
        {
            Directory.CreateDirectory(FOLDER_NAME);
        }
        if (File.Exists(FOLDER_NAME + FILE_NAME))
        {
            File.Delete(FOLDER_NAME + FILE_NAME);
        }
        FileStream FileStream = new FileStream(FOLDER_NAME + FILE_NAME, FileMode.Create, FileAccess.Write);
        StreamWriter StreamWriter = new StreamWriter(FileStream);
        StreamWriter.WriteLine(FILE_CONTENT);
        StreamWriter.Close();
        FileStream = null;
        StreamWriter = null;
    }

   
    private string UTL_GENERATE_MODEL_CLASS_NAME(string OBJECT_NAME)
    {
        try
        {
            string MyString = null;
            string MyArrayElement = null;
            string[] MyArray = OBJECT_NAME.ToLower().Split('_');

            //MessageBox.Show(MyArray.Length.ToString());
            if (MyArray.Length > 1)
            {
                for (int i = 0; i < MyArray.Length; i++)
                {
                    MyArrayElement = MyArray[i];
                    for (int j = 0; j < MyArrayElement.Length; j++)
                    {
                        if (j == 0)
                        {
                            MyString += MyArrayElement.ToUpper().Substring(j, 1);
                        }
                        else

                        { MyString += MyArrayElement.Substring(j, 1); }

                    }

                }
            }
            else
            {
                MyArrayElement = OBJECT_NAME.ToLower();
                for (int j = 0; j < MyArrayElement.Length; j++)
                {
                    if (j == 0)
                    {
                        MyString += MyArrayElement.ToUpper().Substring(j, 1);
                    }
                    else

                    { MyString += MyArrayElement.Substring(j, 1); }

                }
            }


            return MyString;

        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private string UTL_GENERATE_READ_CLASS_NAME(string OBJECT_NAME)
    {
        try
        {
            string MyString = null;
            string MyArrayElement = null;
            string[] MyArray = OBJECT_NAME.ToLower().Split('_');

            //MessageBox.Show(MyArray.Length.ToString());
            if (MyArray.Length > 1)
            {
                for (int i = 0; i < MyArray.Length; i++)
                {
                    MyArrayElement = MyArray[i];
                    for (int j = 0; j < MyArrayElement.Length; j++)
                    {
                        if (j == 0)
                        {
                            MyString += MyArrayElement.ToUpper().Substring(j, 1);
                        }
                        else

                        { MyString += MyArrayElement.Substring(j, 1); }

                    }

                }
            }
            else
            {
                MyArrayElement = OBJECT_NAME.ToLower();
                for (int j = 0; j < MyArrayElement.Length; j++)
                {
                    if (j == 0)
                    {
                        MyString += MyArrayElement.ToUpper().Substring(j, 1);
                    }
                    else

                    { MyString += MyArrayElement.Substring(j, 1); }

                }
            }


            return MyString + "Read";

        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}

