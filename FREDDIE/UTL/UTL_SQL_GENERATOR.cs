using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;

 
  public  class UTL_SQL_GENERATOR
    {

    private const String Q = @"""";
    private const String T1 = "\t";
    private const String T2 = "\t\t";
    private const String T3 = "\t\t\t";
    private const String T4 = "\t\t\t\t";
    private const String T5 = "\t\t\t\t\t";
    private const String NL = "\n";
    private const String LOG_FILE = null;
    private String _SQL_WHERE = " WHERE " + Environment.NewLine;
    private Boolean _PK = false;
    private Boolean _PK_CONTAINS_IDENTITY_FIELD = false;
    private int _COLUMNS_IN_PK = 0;

    private String _usp_DEL_BY_PK;
    private String _usp_DEL_ALL_ROWS;
    private String _usp_SEL_BY_PK;
    private String _usp_SEL_ALL;
    private String _usp_UPD_BY_PK;
    private String _usp_INS_A_ROW;
    private String _usp_INS_WHERE_NOT_EXISTS;

    private String _usp_CUSTOM;
    private String _usp_CUSTOM_FILE_NAME;
    private String _usp_DEL_BY_PK_FILE_NAME;
    private String _usp_DEL_ALL_ROWS_FILE_NAME;
    private String _usp_SEL_BY_PK_FILE_NAME;
    private String _usp_SEL_ALL_FILE_NAME;
    private String _usp_UPD_BY_PK_FILE_NAME;
    private String _usp_INS_A_ROW_FILE_NAME;
    private String _usp_INS_WHERE_NOT_EXISTS_FILE_NAME;

    
    private String _SQL_FOLDER;



    public System.String SQL_FOLDER
    {
        get { return _SQL_FOLDER; }
        set { _SQL_FOLDER = value; }

    }

   
 

     


    public System.String usp_INS_A_ROW
    {
        get { return _usp_INS_A_ROW; }
        
    }
    public System.String usp_INS_WHERE_NOT_EXISTS
    {
        get { return _usp_INS_WHERE_NOT_EXISTS; }
     
    }
    public System.String usp_DEL_BY_PK
    {
        get { return _usp_DEL_BY_PK; }
       
    }
    public System.String usp_DEL_ALL_ROWS
    {
        get { return _usp_DEL_ALL_ROWS; }
     
    }
    public System.String usp_SEL_BY_PK
    {
        get { return _usp_SEL_BY_PK; }
     
    }
    public System.String usp_SEL_ALL
    {
        get { return _usp_SEL_ALL; }
      
    }
    public System.String usp_UPD_BY_PK
    {
        get { return _usp_UPD_BY_PK; }
      
    }

    public System.String usp_CUSTOM
    {
        get { return _usp_CUSTOM; }
       
    }









    public System.String usp_CUSTOM_FILE_NAME
    {
        get { return _usp_CUSTOM_FILE_NAME; }

    }

    public System.String usp_INS_A_ROW_FILE_NAME
    {
        get { return _usp_INS_A_ROW_FILE_NAME; }

    }
    public System.String usp_INS_WHERE_NOT_EXISTS_FILE_NAME
    {
        get { return _usp_INS_WHERE_NOT_EXISTS_FILE_NAME; }

    }
    public System.String usp_DEL_BY_PK_FILE_NAME
    {
        get { return _usp_DEL_BY_PK_FILE_NAME; }

    }
    public System.String usp_DEL_ALL_ROWS_FILE_NAME
    {
        get { return _usp_DEL_ALL_ROWS_FILE_NAME; }

    }
    public System.String usp_SEL_BY_PK_FILE_NAME
    {
        get { return _usp_SEL_BY_PK_FILE_NAME; }

    }
    public System.String usp_SEL_ALL_FILE_NAME
    {
        get { return _usp_SEL_ALL_FILE_NAME; }

    }
    public System.String usp_UPD_BY_PK_FILE_NAME
    {
        get { return _usp_UPD_BY_PK_FILE_NAME; }

    }



    #region UTL


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


    private List<FREDS_MATRIX> UTL_GET_ATTRIBUTES_VIEW(string SERVER_NAME, string DATABASE_NAME, int OBJECT_ID)
    {
        List<FREDS_MATRIX> LST = new List<FREDS_MATRIX>();
        MSSQL_CS_META_DATA db = new MSSQL_CS_META_DATA();
        DataSet DS_COLUMNS = db.GET_COLUMNS_IN_VIEW_DS(SERVER_NAME, DATABASE_NAME, OBJECT_ID);
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
                    INSERT_YN = "N",
                    SQL_DATA_TYPE = R["TYPE_NAME"].ToString().ToUpper(),
                    SQL_IDENTITY_FIELD_YN = "N",
                    SQL_IDENT_INCR = 0,
                    SQL_IDENT_SEED = 0,
                    SQL_IS_NULLABLE = R["IS_NULLABLE"].ToString(),
                    SQL_MAX_LENGTH = Convert.ToInt32(R["MAX_LENGTH"]),
                    SQL_VARIABLE_NAME = "@" + R["COLUMN_NAME"].ToString().ToUpper(),
                    SQL_PRECISION = Convert.ToInt32(R["PRECISION"]),
                    SQL_SCALE = Convert.ToInt32(R["SCALE"]),
                    SQL_PROC_PARM_STRING = R["TYPE_NAME"].ToString().ToUpper(),
                    SQL_TABLE_SCHEMA = R["TABLE_SCHEMA"].ToString().ToUpper(),
                    OBJECT_ID = OBJECT_ID,
                    OBJECT_NAME = R["VIEW_NAME"].ToString().ToUpper(),
                    OBJECT_TYPE = "VIEW",
                    PK_YN = "N",
                    SERVER_NAME = SERVER_NAME,
                    UPDATE_YN = "Y",
                    CS_DATA_TYPE = "?",
                    CS_PRIVATE_VARIABLE = "?",
                    CS_PUBLIC_PROPERTY = "?"
                };
                OBJECT_NAME = o.OBJECT_NAME;
                if (o.COLUMN_NAME == "INSDT" || o.COLUMN_NAME == "UPDDT")
                {
                    o.CS_PARM_STRING = "///// RESERVED COLUMN NAME";
                    o.SQL_VARIABLE_NAME = "GETDATE()";
                }
                if (o.COLUMN_NAME == "INSOPID" || o.COLUMN_NAME == "UPDOPID")
                {
                    o.CS_PARM_STRING = "///// RESERVED COLUMN NAME";
                    o.SQL_VARIABLE_NAME = "USER_NAME()";
                }
                LST.Add(o);
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
                        o.CS_PUBLIC_PROPERTY = "public " +
                            R["DataType"].ToString() +
                            " " +
                            o.COLUMN_NAME.ToString().ToUpper() +
                            Environment.NewLine +
                             "{ " +
                             Environment.NewLine +
                             "get { return _" + o.COLUMN_NAME.ToString().ToUpper() + "; }" + Environment.NewLine +
                             "set { _" + o.COLUMN_NAME.ToString().ToUpper() + " = value; }" +
                               Environment.NewLine +
                             "} " +
                               Environment.NewLine;
                    }
                }
            }
            db = null;
            return LST;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private List<FREDS_MATRIX> UTL_GET_ATTRIBUTES_TABLE(string SERVER_NAME, string DATABASE_NAME, int OBJECT_ID, Boolean PK_ONLY)
    {
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
                    SQL_PROC_PARM_STRING = R["TYPE_NAME"].ToString().ToUpper(),
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
                        o.SQL_PROC_PARM_STRING = o.SQL_DATA_TYPE + "(" + o.SQL_MAX_LENGTH + ")";
                        break;
                    case "CHAR":
                        o.SQL_PROC_PARM_STRING = o.SQL_DATA_TYPE + "(" + o.SQL_MAX_LENGTH + ")";
                        break;
                    case "DECIMAL":
                        o.SQL_PROC_PARM_STRING = o.SQL_DATA_TYPE + "(" + o.SQL_PRECISION + "," + o.SQL_SCALE + ")";
                        break;
                }
                if (o.SQL_IDENTITY_FIELD_YN == "1")
                {
                    o.UPDATE_YN = "N";
                    o.INSERT_YN = "N";
                    o.SQL_IDENTITY_FIELD_YN = "Y";
                    _PK_CONTAINS_IDENTITY_FIELD = true;
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
                        o.CS_PUBLIC_PROPERTY = "public " +
                            R["DataType"].ToString() +
                            " " +
                            o.COLUMN_NAME.ToString().ToUpper() +
                            Environment.NewLine +
                             "{ " +
                             Environment.NewLine +
                             "get { return _" + o.COLUMN_NAME.ToString().ToUpper() + "; }" + Environment.NewLine +
                             "set { _" + o.COLUMN_NAME.ToString().ToUpper() + " = value; }" +
                               Environment.NewLine +
                             "} " +
                               Environment.NewLine;
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
    #endregion
    //private List<FREDS_MATRIX> UTL_GET_ATTRIBUTES_TABLE_FOR_UPDATE(string SERVER_NAME, string DATABASE_NAME, int OBJECT_ID )
    //{
      

    //    List<FREDS_MATRIX> LST = new List<FREDS_MATRIX>();
    //    MSSQL_CS_META_DATA db = new MSSQL_CS_META_DATA();
    //    DataSet DS_COLUMNS = db.GET_COLUMNS_IN_TABLE_DS(SERVER_NAME, DATABASE_NAME, OBJECT_ID);
    //    DataSet DS_PRIMARY_KEYS = db.GET_PRIMARY_KEYS_IN_TABLE_DS(SERVER_NAME, DATABASE_NAME, OBJECT_ID);
        
        
    //    string OBJECT_NAME = null;
    //    try
    //    {
    //        foreach (DataRow R in DS_COLUMNS.Tables[0].Rows)
    //        {
    //            FREDS_MATRIX o = new FREDS_MATRIX
    //            {
    //                COLUMN_ID = Convert.ToInt32(R["COLUMN_ID"]),
    //                COLUMN_NAME = R["COLUMN_NAME"].ToString().ToUpper(),
    //                CS_PARM_STRING = "cmd.Parameters.AddWithValue(" + Q + "@" + R["COLUMN_NAME"].ToString().ToUpper() + Q + "," + "o" + "." + R["COLUMN_NAME"].ToString().ToUpper() + ");",
    //                DATABASE_NAME = DATABASE_NAME,
    //                SQL_DATA_TYPE = R["TYPE_NAME"].ToString().ToUpper(),
    //                SQL_IDENTITY_FIELD_YN = R["IS_IDENTITY"].ToString(),
    //                SQL_IDENT_INCR = Convert.ToInt32(R["IDENT_INCR"]),
    //                SQL_IDENT_SEED = Convert.ToInt32(R["IDENT_SEED"]),
    //                SQL_IS_NULLABLE = R["IS_NULLABLE"].ToString(),
    //                SQL_MAX_LENGTH = Convert.ToInt32(R["MAX_LENGTH"]),
    //                SQL_PARM_STRING = "@" + R["COLUMN_NAME"].ToString().ToUpper(),
    //                SQL_PROC_PARM_STRING = R["TYPE_NAME"].ToString().ToUpper(),
    //                SQL_PRECISION = Convert.ToInt32(R["PRECISION"]),
    //                SQL_SCALE = Convert.ToInt32(R["SCALE"]),
    //                SQL_TABLE_SCHEMA = R["TABLE_SCHEMA"].ToString().ToUpper(),
    //                OBJECT_ID = OBJECT_ID,
    //                OBJECT_NAME = R["TABLE_NAME"].ToString().ToUpper(),
    //                OBJECT_TYPE = "TABLE",
    //                PK_YN = "N",
    //                SERVER_NAME = SERVER_NAME,
    //                INSERT_YN = "Y",
    //                UPDATE_YN = "Y",
    //                RESERVED_YN = "N",
    //                CS_DATA_TYPE = "?",
    //                CS_PRIVATE_VARIABLE = "?",
    //                CS_PUBLIC_PROPERTY = "?"
    //            };
    //            OBJECT_NAME = o.OBJECT_NAME;

    //            foreach (DataRow r in DS_PRIMARY_KEYS.Tables[0].Rows)
    //            {
    //                if (o.COLUMN_NAME == r["COLUMN_NAME"].ToString().ToUpper())
    //                {
    //                    o.PK_YN = "Y";
    //                    o.UPDATE_YN = "N";
    //                }
    //            }
    //            switch (o.SQL_DATA_TYPE.ToUpper())
    //            {
    //                case "VARCHAR":
    //                    o.SQL_PROC_PARM_STRING = o.SQL_DATA_TYPE + "(" + o.SQL_MAX_LENGTH + ")";
    //                    break;
    //                case "CHAR":
    //                    o.SQL_PROC_PARM_STRING = o.SQL_DATA_TYPE + "(" + o.SQL_MAX_LENGTH + ")";
    //                    break;
    //                case "DECIMAL":
    //                    o.SQL_PROC_PARM_STRING = o.SQL_DATA_TYPE + "(" + o.SQL_PRECISION + "," + o.SQL_SCALE + ")";
    //                    break;
    //            }
    //            if (o.SQL_IDENTITY_FIELD_YN == "1")
    //            {
    //                o.UPDATE_YN = "N";
    //                o.INSERT_YN = "N";
    //                o.SQL_IDENTITY_FIELD_YN = "Y";
    //                _PK_CONTAINS_IDENTITY_FIELD = true;
    //            }
    //            else
    //            {
    //                o.SQL_IDENTITY_FIELD_YN = "N";
    //            }
    //            if (Convert.ToBoolean(o.SQL_IS_NULLABLE))
    //            {
    //                o.SQL_IS_NULLABLE = "Y";
    //            }
    //            else
    //            {
    //                o.SQL_IS_NULLABLE = "N";
    //            }

    //            if (o.COLUMN_NAME == "INSDT" || o.COLUMN_NAME == "UPDDT")
    //            {
    //                o.CS_PARM_STRING = "///// " + o.CS_PARM_STRING + " RESERVED COLUMN NAME AUTO GENERATED BY SQL SERVER";
    //                o.RESERVED_YN = "Y";
    //                o.SQL_VARIABLE_NAME = "GETDATE()";
    //            }
    //            if (o.COLUMN_NAME == "INSOPID" || o.COLUMN_NAME == "UPDOPID")
    //            {
    //                o.CS_PARM_STRING = "///// " + o.CS_PARM_STRING + " RESERVED COLUMN NAME AUTO GENERATED BY SQL SERVER";
    //                o.RESERVED_YN = "Y";
    //                o.SQL_VARIABLE_NAME = "USER_NAME()";
    //            }
    //            if (o.COLUMN_NAME == "INSDT" || o.COLUMN_NAME == "INSOPID")
    //            {
    //                o.INSERT_YN = "Y";
    //                o.UPDATE_YN = "N";

    //            }
    //            if (o.COLUMN_NAME == "UPDDT" || o.COLUMN_NAME == "UPDOPID")
    //            {
    //                o.INSERT_YN = "Y";
    //                o.UPDATE_YN = "Y";
    //            }
                
    //                LST.Add(o);
                

    //            o = null;
    //        }
    //        DataTable DT_SCHEMA = db.GET_SCHEMA_TABLE_DT(SERVER_NAME, DATABASE_NAME, OBJECT_NAME);
    //        foreach (DataRow R in DT_SCHEMA.Rows)
    //        {
    //            foreach (FREDS_MATRIX o in LST)
    //            {
    //                if (o.COLUMN_NAME.ToString().ToUpper() == R["ColumnName"].ToString().ToUpper())
    //                {
    //                    o.CS_DATA_TYPE = R["DataType"].ToString();
    //                    o.CS_PRIVATE_VARIABLE = "private " + R["DataType"].ToString() + " _" + o.COLUMN_NAME.ToString().ToUpper() + ";" + Environment.NewLine;
    //                    o.CS_PUBLIC_PROPERTY = "public " +
    //                        R["DataType"].ToString() +
    //                        " " +
    //                        o.COLUMN_NAME.ToString().ToUpper() +
    //                        Environment.NewLine +
    //                         "{ " +
    //                         Environment.NewLine +
    //                         "get { return _" + o.COLUMN_NAME.ToString().ToUpper() + "; }" + Environment.NewLine +
    //                         "set { _" + o.COLUMN_NAME.ToString().ToUpper() + " = value; }" +
    //                           Environment.NewLine +
    //                         "} " +
    //                           Environment.NewLine;
    //                }
    //            }
    //        }
    //        db = null;
            
            
    //        return LST;
    //    }
    //    catch (Exception ex)
    //    {
    //        // MessageBox.Show(ex.Message);
    //        return null;
    //    }
    //}


    public void GENERATE_DEFAULT(String SERVER_NAME, String DATABASE_NAME, Int32 OBJECT_ID, String OBJECT_NAME, Boolean View)
    {
        if (View)
        {
            List<FREDS_MATRIX> COLUMNS = UTL_GET_ATTRIBUTES_VIEW(SERVER_NAME, DATABASE_NAME.ToUpper(), OBJECT_ID);
            TSQL_SEL_ALL_DEFAULT(  COLUMNS, OBJECT_NAME, DATABASE_NAME);

        }
        else
        {
            List<FREDS_MATRIX> COLUMNS = UTL_GET_ATTRIBUTES_TABLE(SERVER_NAME, DATABASE_NAME.ToUpper(), OBJECT_ID, false);
            List<FREDS_MATRIX> COLUMNS_PK = UTL_GET_ATTRIBUTES_TABLE(SERVER_NAME, DATABASE_NAME.ToUpper(), OBJECT_ID, true);
       
            TSQL_SEL_ALL_DEFAULT(  COLUMNS, OBJECT_NAME, DATABASE_NAME);
            TSQL_INS_A_ROW_DEFAULT(  COLUMNS, OBJECT_NAME, DATABASE_NAME);
            TSQL_DEL_ALL_ROWS_DEFAULT(OBJECT_NAME, DATABASE_NAME);
            if (COLUMNS_PK.Count > 0)
            {
           
                TSQL_SEL_A_ROW_BY_PK_DEFAULT(  COLUMNS, COLUMNS_PK, OBJECT_NAME, DATABASE_NAME);
                TSQL_UPD_A_ROW_BY_PK_DEFAULT(  COLUMNS_PK, COLUMNS, OBJECT_NAME, DATABASE_NAME);
                TSQL_DEL_A_ROW_BY_PK_DEFAULT(COLUMNS_PK, OBJECT_NAME, DATABASE_NAME);
                
            }
            

        }
    }



    #region DEFAULT TSQL SECTION


    private String TSQL_SEL_A_ROW_BY_PK_DEFAULT(  List<FREDS_MATRIX> COLUMNS_IN_TABLE, List<FREDS_MATRIX> COLUMNS_IN_WHERE_CLAUSE, String OBJECT_NAME, String DATABASE_NAME)
    {
        int ColCount = 0;
        int ParmCount = 0;

        String SP_NAME = null;
        String TSQL = null;


        String SQL_SELECT = T3+ "SELECT " + Environment.NewLine;
        String SQL_WHERE = T3 +"WHERE " + Environment.NewLine;

        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_TABLE)
        {
            ColCount++;
            if(ColCount < COLUMNS_IN_TABLE.Count)
            {
                SQL_SELECT += T4 + ATTRIBUTE.COLUMN_NAME + "," + Environment.NewLine;
            }
            else
            {
                SQL_SELECT += T4 + ATTRIBUTE.COLUMN_NAME   + Environment.NewLine;
            }

        }
        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_WHERE_CLAUSE)
        {
            ParmCount++;
            if (ParmCount < COLUMNS_IN_WHERE_CLAUSE.Count)
            {
                SQL_WHERE += T4+ ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.SQL_VARIABLE_NAME + " AND " + Environment.NewLine;
            }
            else
            {
                SQL_WHERE += T4 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.SQL_VARIABLE_NAME + Environment.NewLine;
            }

        }

        
             
                SP_NAME = "usp_SEL_A_ROW_BY_PK_" + OBJECT_NAME.ToUpper();
            
           
      


        TSQL = "USE [" + DATABASE_NAME + "]" + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "------------PROC CREATED BY Freddie ON " + DateTime.Now + Environment.NewLine + Environment.NewLine +
        "SET ANSI_NULLS ON" + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "SET QUOTED_IDENTIFIER ON" + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +

        "IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = '" + SP_NAME + "')" + Environment.NewLine + Environment.NewLine +
        "DROP PROCEDURE " + SP_NAME + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +


        "CREATE PROC [dbo].[" + SP_NAME + "]" + Environment.NewLine + Environment.NewLine + Environment.NewLine;

        ParmCount = 0;
        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_WHERE_CLAUSE)
        {
            if (ATTRIBUTE.PK_YN == "Y")
            {
                ParmCount++;
            }
        }
        ColCount = 0;
        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_WHERE_CLAUSE)
        {
            if (ATTRIBUTE.PK_YN == "Y")
            {
                ColCount++;
                if (ColCount < ParmCount)
                {
                    TSQL += T3 + ATTRIBUTE.SQL_VARIABLE_NAME + " " + ATTRIBUTE.SQL_PROC_PARM_STRING + "," + Environment.NewLine;
                }
                else
                {
                    TSQL += T3 + ATTRIBUTE.SQL_VARIABLE_NAME + " " + ATTRIBUTE.SQL_PROC_PARM_STRING + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                }
            }
        }

        if (ParmCount > 0)
        {
            TSQL += T3 + "AS" + Environment.NewLine + Environment.NewLine + Environment.NewLine + SQL_SELECT + Environment.NewLine + T3 + "FROM dbo." + OBJECT_NAME.ToUpper() + Environment.NewLine + Environment.NewLine + SQL_WHERE;

        }
        else
        {
            TSQL += T3 + "AS" + Environment.NewLine + Environment.NewLine + Environment.NewLine + SQL_SELECT + Environment.NewLine + T3 + "FROM dbo." + OBJECT_NAME.ToUpper() + Environment.NewLine +  Environment.NewLine;
        }
     
                _usp_SEL_BY_PK = TSQL;
                _usp_SEL_BY_PK_FILE_NAME = SP_NAME + ".SQL";


        
        UTL_SAVE_FILE(SQL_FOLDER, _usp_SEL_BY_PK_FILE_NAME, TSQL);
        return TSQL;
    }
    private String TSQL_SEL_ALL_DEFAULT(  List<FREDS_MATRIX> COLUMNS_IN_TABLE, String OBJECT_NAME, String DATABASE_NAME)
    {
        int ColCount = 0;


        String SP_NAME = null;
        String TSQL = null;


        String SQL_SELECT = T3 + "SELECT " + Environment.NewLine;


        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_TABLE)
        {
            ColCount++;
            if (ColCount < COLUMNS_IN_TABLE.Count)
            {
                SQL_SELECT += T4 + ATTRIBUTE.COLUMN_NAME + "," + Environment.NewLine;
            }
            else
            {
                SQL_SELECT += T4 + ATTRIBUTE.COLUMN_NAME + Environment.NewLine;
            }

        }


      
            SP_NAME = "usp_SEL_ALL_" + OBJECT_NAME.ToUpper();


        


        TSQL = "USE [" + DATABASE_NAME + "]" + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "------------PROC CREATED BY Freddie ON " + DateTime.Now + Environment.NewLine + Environment.NewLine +
        "SET ANSI_NULLS ON" + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "SET QUOTED_IDENTIFIER ON" + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +

        "IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = '" + SP_NAME + "')" + Environment.NewLine + Environment.NewLine +
        "DROP PROCEDURE " + SP_NAME + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +


        "CREATE PROC [dbo].[" + SP_NAME + "]" + Environment.NewLine + Environment.NewLine + Environment.NewLine;




        TSQL += T3 + "AS" + Environment.NewLine + Environment.NewLine + Environment.NewLine + SQL_SELECT + Environment.NewLine + T3 + "FROM dbo." + OBJECT_NAME.ToUpper() + Environment.NewLine + Environment.NewLine;

       

            _usp_SEL_ALL = TSQL;
            _usp_SEL_ALL_FILE_NAME = SP_NAME + ".SQL";
        
        UTL_SAVE_FILE(SQL_FOLDER, _usp_SEL_ALL_FILE_NAME, TSQL);

        return TSQL;
    }
    private String TSQL_INS_A_ROW_DEFAULT(  List<FREDS_MATRIX> COLUMNS_IN_TABLE, String OBJECT_NAME, String DATABASE_NAME)
    {
        int ColCount = 0;
        int ParmCount = 0;
        int ColumnsToInsert = 0;

        String SP_NAME = null;
        String TSQL = null;



        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_TABLE)
        {

            if (ATTRIBUTE.INSERT_YN == "Y")
            {
                if (ATTRIBUTE.RESERVED_YN == "N")
                {
                    ParmCount++;
                }

            }
        }
        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_TABLE)
        {
            if (ATTRIBUTE.INSERT_YN == "Y")
            {
                ColumnsToInsert++;
            }
        }

        

            SP_NAME = "usp_INS_A_ROW_" + OBJECT_NAME.ToUpper();


        


        TSQL = "USE [" + DATABASE_NAME + "]" + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "------------PROC CREATED BY Freddie ON " + DateTime.Now + Environment.NewLine + Environment.NewLine +
        "SET ANSI_NULLS ON" + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "SET QUOTED_IDENTIFIER ON" + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +

        "IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = '" + SP_NAME + "')" + Environment.NewLine + Environment.NewLine +
        "DROP PROCEDURE " + SP_NAME + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +


        "CREATE PROC [dbo].[" + SP_NAME + "]" + Environment.NewLine + Environment.NewLine + Environment.NewLine;



        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_TABLE)
        {
            if (ATTRIBUTE.INSERT_YN == "Y")
            {
                if (ATTRIBUTE.RESERVED_YN == "N")
                {
                    ColCount++;
                    if (ColCount < ParmCount)
                    {
                        TSQL += T3 + ATTRIBUTE.SQL_VARIABLE_NAME + " " + ATTRIBUTE.SQL_PROC_PARM_STRING + "," + Environment.NewLine;
                    }
                    else
                    {
                        TSQL += T3 + ATTRIBUTE.SQL_VARIABLE_NAME + " " + ATTRIBUTE.SQL_PROC_PARM_STRING + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                    }
                }
            }
        }
        TSQL += T3 + "AS" + Environment.NewLine + Environment.NewLine + Environment.NewLine + T3 + "INSERT INTO dbo." + OBJECT_NAME.ToUpper() + Environment.NewLine + Environment.NewLine + T3 + "(" + Environment.NewLine;



        ColCount = 0;
        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_TABLE)
        {
            if (ATTRIBUTE.INSERT_YN == "Y")
            {
                ColCount++;
                if (ColCount < ColumnsToInsert)
                {
                    TSQL += T3 + ATTRIBUTE.COLUMN_NAME + "," + Environment.NewLine;
                }
                else
                {
                    TSQL += T3 + ATTRIBUTE.COLUMN_NAME + Environment.NewLine + T3 + ")" + Environment.NewLine + T3 + "VALUES" + Environment.NewLine + T3 + "(" + Environment.NewLine;
                }
            }

        }
        ColCount = 0;
        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_TABLE)
        {
            if (ATTRIBUTE.INSERT_YN == "Y")
            {
                ColCount++;
                if (ColCount < ColumnsToInsert)
                {
                    TSQL += T3 + ATTRIBUTE.SQL_VARIABLE_NAME + "," + Environment.NewLine;
                }
                else
                {
                    TSQL += T3 + ATTRIBUTE.SQL_VARIABLE_NAME + Environment.NewLine + T3 + ")" + Environment.NewLine;
                }
            }

        }
        
            _usp_INS_A_ROW = TSQL;
            _usp_INS_A_ROW_FILE_NAME = SP_NAME + ".SQL";
       

        UTL_SAVE_FILE(SQL_FOLDER, _usp_INS_A_ROW_FILE_NAME, TSQL);
        return TSQL;
    }
    //private String TSQL_INSERT_WHERE_NOT_EXISTS(Boolean Default, List<FREDS_MATRIX> COLUMNS_IN_TABLE, List<FREDS_MATRIX> COLUMNS_IN_WHERE_CLAUSE, String OBJECT_NAME, String DATABASE_NAME)
    //{
    //    int ColCount = 0;
    //    int ParmCount = 0;
    //    int ColumnsToInsert = 0;
    //    int ColumnsPK = 0;
    //    String SP_NAME = null;
    //    String TSQL = null;
    //    String SQL_WHERE = null;



    //    foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_TABLE)
    //    {

    //        if (ATTRIBUTE.PK_YN == "Y")
    //        {

    //            ColumnsPK++;


    //        }
    //    }
    //    foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_TABLE)
    //    {

    //        if (ATTRIBUTE.PK_YN == "Y")
    //        {

    //            ColCount++;

    //            if (ColCount < ColumnsPK)
    //            {
    //                SQL_WHERE = T2 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.SQL_VARIABLE_NAME + " AND " + Environment.NewLine;
    //            }
    //            else
    //            {
    //                SQL_WHERE = T2 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.SQL_VARIABLE_NAME + " )" + Environment.NewLine;
    //            }


    //        }
    //    }



    //    foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_TABLE)
    //    {

    //        if (ATTRIBUTE.INSERT_YN == "Y")
    //        {
    //            if (ATTRIBUTE.RESERVED_YN == "N")
    //            {
    //                ParmCount++;
    //            }

    //        }
    //    }
    //    foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_TABLE)
    //    {
    //        if (ATTRIBUTE.INSERT_YN == "Y")
    //        {
    //            ColumnsToInsert++;
    //        }
    //    }

    //    if (Default)
    //    {

    //        SP_NAME = "usp_INS_WHERE_NOT_EXISTS_" + OBJECT_NAME.ToUpper();


    //    }
    //    else
    //    {
    //        SP_NAME = "usp_INS_WHERE_NOT_EXISTS_" + OBJECT_NAME.ToUpper() + "_CUSTOM";
    //    }


    //    TSQL = "USE [" + DATABASE_NAME + "]" + Environment.NewLine +
    //    "GO" + Environment.NewLine + Environment.NewLine +
    //    "------------PROC CREATED BY Freddie ON " + DateTime.Now + Environment.NewLine + Environment.NewLine +
    //    "SET ANSI_NULLS ON" + Environment.NewLine + Environment.NewLine +
    //    "GO" + Environment.NewLine + Environment.NewLine +
    //    "SET QUOTED_IDENTIFIER ON" + Environment.NewLine + Environment.NewLine +
    //    "GO" + Environment.NewLine + Environment.NewLine +

    //    "IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = '" + SP_NAME + "')" + Environment.NewLine + Environment.NewLine +
    //    "DROP PROCEDURE " + SP_NAME + Environment.NewLine + Environment.NewLine +
    //    "GO" + Environment.NewLine + Environment.NewLine +


    //    "CREATE PROC [dbo].[" + SP_NAME + "]" + Environment.NewLine + Environment.NewLine + Environment.NewLine;


    //    ColCount = 0;
    //    foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_TABLE)
    //    {
    //        if (ATTRIBUTE.INSERT_YN == "Y")
    //        {
    //            if (ATTRIBUTE.RESERVED_YN == "N")
    //            {
    //                ColCount++;
    //                if (ColCount < ParmCount)
    //                {
    //                    TSQL += T3 + ATTRIBUTE.SQL_VARIABLE_NAME + " " + ATTRIBUTE.SQL_PROC_PARM_STRING + "," + Environment.NewLine;
    //                }
    //                else
    //                {
    //                    TSQL += T3 + ATTRIBUTE.SQL_VARIABLE_NAME + " " + ATTRIBUTE.SQL_PROC_PARM_STRING + Environment.NewLine + Environment.NewLine + Environment.NewLine;
    //                }
    //            }
    //        }
    //    }
    //    TSQL += T3 + "AS" + Environment.NewLine + T3 + "IF NOT EXISTS (SELECT * FROM DBO." + OBJECT_NAME.ToUpper() + Environment.NewLine + T3 + " WHERE " + Environment.NewLine + T3 + SQL_WHERE;

    //    TSQL += Environment.NewLine + Environment.NewLine + T3 + "BEGIN " + Environment.NewLine + Environment.NewLine;



    //    TSQL += Environment.NewLine + Environment.NewLine + T3 + "INSERT INTO dbo." + OBJECT_NAME.ToUpper() + Environment.NewLine + Environment.NewLine + T3 + "(" + Environment.NewLine;



    //    ColCount = 0;
    //    foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_TABLE)
    //    {
    //        if (ATTRIBUTE.INSERT_YN == "Y")
    //        {
    //            ColCount++;
    //            if (ColCount < ColumnsToInsert)
    //            {
    //                TSQL += T3 + ATTRIBUTE.COLUMN_NAME + "," + Environment.NewLine;
    //            }
    //            else
    //            {
    //                TSQL += T3 + ATTRIBUTE.COLUMN_NAME + Environment.NewLine + T3 + ")" + Environment.NewLine + T3 + "VALUES" + Environment.NewLine + T3 + "(" + Environment.NewLine;
    //            }
    //        }

    //    }
    //    ColCount = 0;
    //    foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_TABLE)
    //    {
    //        if (ATTRIBUTE.INSERT_YN == "Y")
    //        {
    //            ColCount++;
    //            if (ColCount < ColumnsToInsert)
    //            {
    //                TSQL += T3 + ATTRIBUTE.SQL_VARIABLE_NAME + "," + Environment.NewLine;
    //            }
    //            else
    //            {
    //                TSQL += T3 + ATTRIBUTE.SQL_VARIABLE_NAME + Environment.NewLine + T3 + ")" + Environment.NewLine + Environment.NewLine + T3 + "END";
    //            }
    //        }

    //    }
    //    if (Default)
    //    {
    //        _usp_INS_WHERE_NOT_EXISTS = TSQL;
    //        _usp_INS_WHERE_NOT_EXISTS_FILE_NAME = SP_NAME + ".SQL";
    //    }
    //    else
    //    {
    //        _usp_CUSTOM = TSQL;
    //    }

    //    UTL_SAVE_FILE(SQL_FOLDER, _usp_INS_WHERE_NOT_EXISTS_FILE_NAME, TSQL);
    //    return TSQL;
    //}
    private String TSQL_DEL_A_ROW_BY_PK_DEFAULT(  List<FREDS_MATRIX> COLUMNS_IN_WHERE_CLAUSE, String OBJECT_NAME, String DATABASE_NAME)
    {
        int ColCount = 0;
        int ParmCount = 0;

        String SP_NAME = null;
        String TSQL = null;



        
            SP_NAME = "usp_DEL_A_ROW_BY_PK_" + OBJECT_NAME.ToUpper();
        





        TSQL = "USE [" + DATABASE_NAME + "]" + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "------------PROC CREATED BY Freddie ON " + DateTime.Now + Environment.NewLine + Environment.NewLine +
        "SET ANSI_NULLS ON" + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "SET QUOTED_IDENTIFIER ON" + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +

        "IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = '" + SP_NAME + "')" + Environment.NewLine + Environment.NewLine +
        "DROP PROCEDURE " + SP_NAME + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +


        "CREATE PROC [dbo].[" + SP_NAME + "]" + Environment.NewLine + Environment.NewLine + Environment.NewLine;


        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_WHERE_CLAUSE)
        {
            if (ATTRIBUTE.PK_YN == "Y")
            {
                ParmCount++;
            }
        }
        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_WHERE_CLAUSE)
        {
            if (ATTRIBUTE.PK_YN == "Y")
            {
                ColCount++;
                if (ColCount < ParmCount)
                {
                    TSQL += T3 + ATTRIBUTE.SQL_VARIABLE_NAME + " " + ATTRIBUTE.SQL_PROC_PARM_STRING + "," + Environment.NewLine;
                }
                else
                {
                    TSQL += T3 + ATTRIBUTE.SQL_VARIABLE_NAME + " " + ATTRIBUTE.SQL_PROC_PARM_STRING + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                }

            }
        }



        if (ParmCount > 0)
        {
            TSQL += T3 + "AS" + Environment.NewLine + Environment.NewLine + Environment.NewLine + "DELETE FROM dbo." + OBJECT_NAME.ToUpper() + Environment.NewLine + T3 + "WHERE" + Environment.NewLine + Environment.NewLine;
        }
        else
        {
            TSQL += T3 + "AS" + Environment.NewLine + Environment.NewLine + Environment.NewLine + "DELETE FROM dbo." + OBJECT_NAME.ToUpper() + Environment.NewLine + T3 + Environment.NewLine + Environment.NewLine;
        }





        ColCount = 0;




        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_WHERE_CLAUSE)
        {
            if (ATTRIBUTE.PK_YN == "Y")
            {
                ColCount++;
                if (ColCount < ParmCount)
                {
                    TSQL += T3 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.SQL_VARIABLE_NAME + " AND " + Environment.NewLine;
                }
                else
                {
                    TSQL += T3 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.SQL_VARIABLE_NAME + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                }

            }
        }

        
            _usp_DEL_BY_PK = TSQL;
            _usp_DEL_BY_PK_FILE_NAME = SP_NAME + ".SQL";
         

        UTL_SAVE_FILE(SQL_FOLDER, _usp_DEL_BY_PK_FILE_NAME, TSQL);


        return TSQL;




    }
    private String TSQL_UPD_A_ROW_BY_PK_DEFAULT(  List<FREDS_MATRIX> COLUMNS_IN_WHERE_CLAUSE, List<FREDS_MATRIX> COLUMNS_IN_TABLE, String OBJECT_NAME, String DATABASE_NAME)
    {
        int ColToUpdateCount = 0;
        int ColCount = 0;
        int ParmCount = 0;
        String SP_NAME = null;
        String TSQL = null;
        SP_NAME = "usp_UPD_A_ROW_BY_PK_" + OBJECT_NAME.ToUpper();
        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_TABLE)
        {
            if (ATTRIBUTE.UPDATE_YN == "Y")
            {
                ColToUpdateCount++;
            }
        }
        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_TABLE)
        {
            if (ATTRIBUTE.PK_YN == "Y" || ATTRIBUTE.RESERVED_YN == "N")
            {
                ParmCount++;
            }
        }
        TSQL = "USE [" + DATABASE_NAME + "]" + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "------------PROC CREATED BY Freddie ON " + DateTime.Now + Environment.NewLine + Environment.NewLine +
        "SET ANSI_NULLS ON" + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "SET QUOTED_IDENTIFIER ON" + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = '" + SP_NAME + "')" + Environment.NewLine + Environment.NewLine +
        "DROP PROCEDURE " + SP_NAME + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "CREATE PROC [dbo].[" + SP_NAME + "]" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_TABLE)
        {
            ColCount++;
            if (ATTRIBUTE.PK_YN == "Y")
            {
                if (ColCount < ParmCount)
                {
                    TSQL += T3 + ATTRIBUTE.SQL_VARIABLE_NAME + " " + ATTRIBUTE.SQL_PROC_PARM_STRING + "," + Environment.NewLine;
                }
                else
                {
                    TSQL += T3 + ATTRIBUTE.SQL_VARIABLE_NAME + " " + ATTRIBUTE.SQL_PROC_PARM_STRING + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                }
            }
            if (ATTRIBUTE.RESERVED_YN == "N" && ATTRIBUTE.PK_YN == "N")
            {
                if (ColCount < ParmCount)
                {
                    TSQL += T3 + ATTRIBUTE.SQL_VARIABLE_NAME + " " + ATTRIBUTE.SQL_PROC_PARM_STRING + "," + Environment.NewLine;
                }
                else
                {
                    TSQL += T3 + ATTRIBUTE.SQL_VARIABLE_NAME + " " + ATTRIBUTE.SQL_PROC_PARM_STRING + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                }
            }
        }
        TSQL += T3 + "AS" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
        TSQL += T3 + "UPDATE dbo." + OBJECT_NAME.ToUpper() + Environment.NewLine + Environment.NewLine;
        TSQL += T3 + "SET" + Environment.NewLine + Environment.NewLine;
        ColCount = 0;
        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_TABLE)
        {
            if (ATTRIBUTE.UPDATE_YN == "Y")
            {
                ColCount++;
                if (ColCount < ColToUpdateCount)
                {
                    TSQL += T3 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.SQL_VARIABLE_NAME + "," + Environment.NewLine;
                }
                else
                {
                    TSQL += T3 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.SQL_VARIABLE_NAME + Environment.NewLine + Environment.NewLine;
                }
            }
        }
        TSQL += T3 + "WHERE" + Environment.NewLine + Environment.NewLine;
        ColCount = 0;
        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_WHERE_CLAUSE)
        {
            ColCount++;
            if (ATTRIBUTE.PK_YN == "Y")
            {
                if (ColCount < COLUMNS_IN_WHERE_CLAUSE.Count)
                {
                    TSQL += T3 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.SQL_VARIABLE_NAME + " AND " + Environment.NewLine;
                }
                else
                {
                    TSQL += T3 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.SQL_VARIABLE_NAME + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                }
            }
        }
         _usp_UPD_BY_PK = TSQL;
         _usp_UPD_BY_PK_FILE_NAME = SP_NAME + ".SQL";
         UTL_SAVE_FILE(SQL_FOLDER, _usp_UPD_BY_PK_FILE_NAME, TSQL);
        return TSQL;
    }
    private String TSQL_DEL_ALL_ROWS_DEFAULT(String OBJECT_NAME, String DATABASE_NAME)
    {
        String SP_NAME = null;
        String TSQL = null;
        SP_NAME = "usp_DEL_ALL_ROWS_" + OBJECT_NAME.ToUpper();
        TSQL = "USE [" + DATABASE_NAME + "]" + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "------------PROC CREATED BY Freddie ON " + DateTime.Now + Environment.NewLine + Environment.NewLine +
        "SET ANSI_NULLS ON" + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "SET QUOTED_IDENTIFIER ON" + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +

        "IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = '" + SP_NAME + "')" + Environment.NewLine + Environment.NewLine +
        "DROP PROCEDURE " + SP_NAME + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +

        "CREATE PROC [dbo].[" + SP_NAME + "]" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
        TSQL += T3 + "AS" + Environment.NewLine + Environment.NewLine + Environment.NewLine + "DELETE FROM dbo." + OBJECT_NAME.ToUpper() + Environment.NewLine + Environment.NewLine;
        _usp_DEL_ALL_ROWS = TSQL;
        _usp_DEL_ALL_ROWS_FILE_NAME = SP_NAME + ".SQL";
        UTL_SAVE_FILE(SQL_FOLDER, _usp_DEL_ALL_ROWS_FILE_NAME, TSQL);
        return TSQL;
    }
    #endregion


    #region CUSTOM TSQL SECTION 


    public String TSQL_SELECT_CUSTOM(  List<FREDS_MATRIX> COLUMNS_IN_TABLE, List<FREDS_MATRIX> COLUMNS_IN_WHERE_CLAUSE, String OBJECT_NAME, String DATABASE_NAME)
    {
        int ColCount = 0;
        String SP_NAME = null;
        String TSQL = null;
        string SQL_PROC_PARMS = null;
        string SQL_WHERE_CLAUSE = "WHERE " + Environment.NewLine + Environment.NewLine;
        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_WHERE_CLAUSE)
        {
            ColCount++;
            if (ColCount < COLUMNS_IN_WHERE_CLAUSE.Count)
            {
                SQL_WHERE_CLAUSE += T4 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.SQL_VARIABLE_NAME + " AND " + Environment.NewLine;
            }
            else
            {
                SQL_WHERE_CLAUSE += T4 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.SQL_VARIABLE_NAME   + Environment.NewLine;
            }
        }
        ColCount = 0;
        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_WHERE_CLAUSE)
        {
            ColCount++;
            if (ColCount < COLUMNS_IN_WHERE_CLAUSE.Count)
            {
                SQL_PROC_PARMS += T4 + ATTRIBUTE.SQL_PROC_PARM_STRING + "," + Environment.NewLine;
            }
            else
            {
                SQL_PROC_PARMS += T4 + ATTRIBUTE.SQL_PROC_PARM_STRING + Environment.NewLine;
            }

        }
        String SQL_SELECT = T3 + "SELECT " + Environment.NewLine;
        ColCount = 0;
        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_TABLE)
        {
            ColCount++;
            if (ColCount < COLUMNS_IN_TABLE.Count)
            {
                SQL_SELECT += T4 + ATTRIBUTE.COLUMN_NAME + "," + Environment.NewLine;
            }
            else
            {
                SQL_SELECT += T4 + ATTRIBUTE.COLUMN_NAME + Environment.NewLine;
            }
        }
        SP_NAME = "usp_SEL_" + OBJECT_NAME.ToUpper() + "_CUSTOM";
        _usp_CUSTOM_FILE_NAME = SP_NAME + ".SQL";
        TSQL = "USE [" + DATABASE_NAME + "]" + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "------------PROC CREATED BY Freddie ON " + DateTime.Now + Environment.NewLine + Environment.NewLine +
        "SET ANSI_NULLS ON" + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "SET QUOTED_IDENTIFIER ON" + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = '" + SP_NAME + "')" + Environment.NewLine + Environment.NewLine +
        "DROP PROCEDURE " + SP_NAME + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "CREATE PROC [dbo].[" + SP_NAME + "]" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
        TSQL +=   SQL_PROC_PARMS + Environment.NewLine + Environment.NewLine + Environment.NewLine;
        TSQL += T3 + "AS" + Environment.NewLine + Environment.NewLine + Environment.NewLine + SQL_SELECT + Environment.NewLine + T3 + "FROM dbo." + OBJECT_NAME.ToUpper() + Environment.NewLine + Environment.NewLine;
        TSQL += T3 + SQL_WHERE_CLAUSE + Environment.NewLine;
        _usp_CUSTOM = TSQL;
         UTL_SAVE_FILE(SQL_FOLDER, _usp_CUSTOM_FILE_NAME, TSQL);
         return TSQL;
    }
    public String TSQL_SELECT_DISTINCT_CUSTOM(String DISTINCT_COLUMN, List<FREDS_MATRIX> COLUMNS_IN_WHERE_CLAUSE, String OBJECT_NAME, String DATABASE_NAME)
    {
        int ColCount = 0;
        String SP_NAME = null;
        String TSQL = null;
        string SQL_PROC_PARMS = null;
        string SQL_WHERE_CLAUSE = "WHERE " + Environment.NewLine + Environment.NewLine;
        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_WHERE_CLAUSE)
        {
            ColCount++;
            if (ColCount < COLUMNS_IN_WHERE_CLAUSE.Count)
            {
                SQL_WHERE_CLAUSE += T4 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.SQL_VARIABLE_NAME + " AND " + Environment.NewLine;
            }
            else
            {
                SQL_WHERE_CLAUSE += T4 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.SQL_VARIABLE_NAME + Environment.NewLine;
            }
        }
        ColCount = 0;
        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_WHERE_CLAUSE)
        {
            ColCount++;
            if (ColCount < COLUMNS_IN_WHERE_CLAUSE.Count)
            {
                SQL_PROC_PARMS += T4 + ATTRIBUTE.SQL_PROC_PARM_STRING + "," + Environment.NewLine;
            }
            else
            {
                SQL_PROC_PARMS += T4 + ATTRIBUTE.SQL_PROC_PARM_STRING + Environment.NewLine;
            }

        }
        String SQL_SELECT = T3 + "SELECT DISTINCT " + DISTINCT_COLUMN + Environment.NewLine;
        
        
        SP_NAME = "usp_SEL_DISTINCT_" + OBJECT_NAME.ToUpper() + "_CUSTOM";
        _usp_CUSTOM_FILE_NAME = SP_NAME + ".SQL";
        TSQL = "USE [" + DATABASE_NAME + "]" + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "------------PROC CREATED BY Freddie ON " + DateTime.Now + Environment.NewLine + Environment.NewLine +
        "SET ANSI_NULLS ON" + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "SET QUOTED_IDENTIFIER ON" + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = '" + SP_NAME + "')" + Environment.NewLine + Environment.NewLine +
        "DROP PROCEDURE " + SP_NAME + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "CREATE PROC [dbo].[" + SP_NAME + "]" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
        TSQL += SQL_PROC_PARMS + Environment.NewLine + Environment.NewLine + Environment.NewLine;
        TSQL += T3 + "AS" + Environment.NewLine + Environment.NewLine + Environment.NewLine + SQL_SELECT + Environment.NewLine + T3 + "FROM dbo." + OBJECT_NAME.ToUpper() + Environment.NewLine + Environment.NewLine;
        TSQL += T3 + SQL_WHERE_CLAUSE + Environment.NewLine;
        _usp_CUSTOM = TSQL;
        UTL_SAVE_FILE(SQL_FOLDER, _usp_CUSTOM_FILE_NAME, TSQL);
        return TSQL;
    }


    public String TSQL_DELETE_CUSTOM(  List<FREDS_MATRIX> COLUMNS_IN_WHERE_CLAUSE, String OBJECT_NAME, String DATABASE_NAME)
    {
        int ColCount = 0;
        String SP_NAME = null;
        String TSQL = null;
        string SQL_PROC_PARMS = null;
        string SQL_WHERE_CLAUSE = "WHERE " + Environment.NewLine + Environment.NewLine;
        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_WHERE_CLAUSE)
        {
            ColCount++;
            if (ColCount < COLUMNS_IN_WHERE_CLAUSE.Count)
            {
                SQL_WHERE_CLAUSE += T4 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.SQL_VARIABLE_NAME + " AND " + Environment.NewLine;
            }
            else
            {
                SQL_WHERE_CLAUSE += T4 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.SQL_VARIABLE_NAME + Environment.NewLine;
            }
        }
        ColCount = 0;
        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_WHERE_CLAUSE)
        {
            ColCount++;
            if (ColCount < COLUMNS_IN_WHERE_CLAUSE.Count)
            {
                SQL_PROC_PARMS += T4 + ATTRIBUTE.SQL_PROC_PARM_STRING + "," + Environment.NewLine;
            }
            else
            {
                SQL_PROC_PARMS += T4 + ATTRIBUTE.SQL_PROC_PARM_STRING + Environment.NewLine;
            }
        }
        String SQL_DELETE = T3 + "DELETE " + Environment.NewLine;


        SP_NAME = "usp_DEL_" + OBJECT_NAME.ToUpper() + "_CUSTOM";
        _usp_CUSTOM_FILE_NAME = SP_NAME + ".SQL";
        TSQL = "USE [" + DATABASE_NAME + "]" + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "------------PROC CREATED BY Freddie ON " + DateTime.Now + Environment.NewLine + Environment.NewLine +
        "SET ANSI_NULLS ON" + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "SET QUOTED_IDENTIFIER ON" + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = '" + SP_NAME + "')" + Environment.NewLine + Environment.NewLine +
        "DROP PROCEDURE " + SP_NAME + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "CREATE PROC [dbo].[" + SP_NAME + "]" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
        TSQL += SQL_PROC_PARMS + Environment.NewLine + Environment.NewLine + Environment.NewLine;
        TSQL += T3 + "AS" + Environment.NewLine + Environment.NewLine + Environment.NewLine + SQL_DELETE + Environment.NewLine + T3 + "FROM dbo." + OBJECT_NAME.ToUpper() + Environment.NewLine + Environment.NewLine;
        TSQL += T3 + SQL_WHERE_CLAUSE + Environment.NewLine;
        _usp_CUSTOM = TSQL;
        UTL_SAVE_FILE(SQL_FOLDER, _usp_CUSTOM_FILE_NAME, TSQL);
        return TSQL;
    }
    public String TSQL_INSERT_CUSTOM(List<FREDS_MATRIX> COLUMNS_TO_INSERT, String OBJECT_NAME, String DATABASE_NAME)
    {
        int ColCount = 0;
        String SP_NAME = null;
        String TSQL = null;
        string SQL_PROC_PARMS = null;
       
        ColCount = 0;
        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_TO_INSERT)
        {
            ColCount++;
            if (ColCount < COLUMNS_TO_INSERT.Count)
            {
                SQL_PROC_PARMS += T4 + ATTRIBUTE.SQL_PROC_PARM_STRING + "," + Environment.NewLine;
            }
            else
            {
                SQL_PROC_PARMS += T4 + ATTRIBUTE.SQL_PROC_PARM_STRING + Environment.NewLine;
            }
        }
        SP_NAME = "usp_INS_" + OBJECT_NAME.ToUpper() + "_CUSTOM";
        _usp_CUSTOM_FILE_NAME = SP_NAME + ".SQL";

        TSQL = "USE [" + DATABASE_NAME + "]" + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "------------PROC CREATED BY Freddie ON " + DateTime.Now + Environment.NewLine + Environment.NewLine +
        "SET ANSI_NULLS ON" + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "SET QUOTED_IDENTIFIER ON" + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = '" + SP_NAME + "')" + Environment.NewLine + Environment.NewLine +
        "DROP PROCEDURE " + SP_NAME + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "CREATE PROC [dbo].[" + SP_NAME + "]" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
        TSQL += SQL_PROC_PARMS + Environment.NewLine + Environment.NewLine + Environment.NewLine;
        TSQL += T3 + "AS" + Environment.NewLine + Environment.NewLine  ;

        TSQL += T3 + "INSERT INTO " + "dbo." + OBJECT_NAME.ToUpper() + Environment.NewLine + T4 + "(" + Environment.NewLine;
        ColCount = 0;
        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_TO_INSERT)
        {
            ColCount++;
            if (ColCount < COLUMNS_TO_INSERT.Count)
            {
                TSQL += T4 + ATTRIBUTE.COLUMN_NAME + "," + Environment.NewLine;
            }
            else
            {
                TSQL += T4 + ATTRIBUTE.COLUMN_NAME  + Environment.NewLine + T4 + ")" + Environment.NewLine;
            }
        }
        TSQL += T4 + "VALUES" + Environment.NewLine + T4 + "(" + Environment.NewLine;
        ColCount = 0;
        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_TO_INSERT)
        {
            ColCount++;
            if (ColCount < COLUMNS_TO_INSERT.Count)
            {
                TSQL += T4 + ATTRIBUTE.SQL_VARIABLE_NAME + "," + Environment.NewLine;
            }
            else
            {
                TSQL += T4 + ATTRIBUTE.SQL_VARIABLE_NAME + Environment.NewLine + T4 + ")" + Environment.NewLine;
            }
        }
        _usp_CUSTOM = TSQL;
        UTL_SAVE_FILE(SQL_FOLDER, _usp_CUSTOM_FILE_NAME, TSQL);
        return TSQL;
    }
    public String TSQL_UPDATE_CUSTOM(List<FREDS_MATRIX> COLUMNS_TO_UPDATE, List<FREDS_MATRIX> COLUMNS_IN_WHERE_CLAUSE, String OBJECT_NAME, String DATABASE_NAME)
    {
        int ColCount = 0;
        int ParmCount = COLUMNS_TO_UPDATE.Count + COLUMNS_IN_WHERE_CLAUSE.Count ;
        String SP_NAME = null;
        String TSQL = null;
        string SQL_PROC_PARMS = null;
        string SQL_WHERE_CLAUSE = "WHERE " + Environment.NewLine + Environment.NewLine;
        ColCount = 0;
        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_TO_UPDATE)
        {
             if(ATTRIBUTE.RESERVED_YN == "Y")
            {
                ParmCount--;
            }
        }


        


        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_TO_UPDATE)
        {
           
            if (ATTRIBUTE.RESERVED_YN != "Y")
            {
                ColCount++;
                if(ColCount  < ParmCount)
                {
                    SQL_PROC_PARMS += ATTRIBUTE.SQL_PROC_PARM_STRING + "," + Environment.NewLine;
                }
                else
                {
                    SQL_PROC_PARMS += ATTRIBUTE.SQL_PROC_PARM_STRING   + Environment.NewLine;
                }
                
            }


        }
        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_WHERE_CLAUSE)
        {

           
                ColCount++;
                if (ColCount < ParmCount)
                {
                SQL_PROC_PARMS += ATTRIBUTE.SQL_PROC_PARM_STRING + "," + Environment.NewLine;
            }
                else
                {
                    SQL_PROC_PARMS += ATTRIBUTE.SQL_PROC_PARM_STRING   + Environment.NewLine;
                }

            


        }

        SP_NAME = "usp_UPD_" + OBJECT_NAME.ToUpper() + "_CUSTOM";
        _usp_CUSTOM_FILE_NAME = SP_NAME + ".SQL";



        TSQL = "USE [" + DATABASE_NAME + "]" + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "------------PROC CREATED BY Freddie ON " + DateTime.Now + Environment.NewLine + Environment.NewLine +
        "SET ANSI_NULLS ON" + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "SET QUOTED_IDENTIFIER ON" + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = '" + SP_NAME + "')" + Environment.NewLine + Environment.NewLine +
        "DROP PROCEDURE " + SP_NAME + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "CREATE PROC [dbo].[" + SP_NAME + "]" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
        TSQL += SQL_PROC_PARMS + Environment.NewLine + Environment.NewLine + Environment.NewLine;
        TSQL += T3 + "AS" + Environment.NewLine + Environment.NewLine;

        TSQL += T3 + "UPDATE " + "dbo." + OBJECT_NAME.ToUpper() + Environment.NewLine + T4 + "SET " + Environment.NewLine;
        ColCount = 0;
        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_TO_UPDATE)
        {
            ColCount++;
            if (ColCount < COLUMNS_TO_UPDATE.Count)
            {
                TSQL += T4 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.SQL_VARIABLE_NAME + " , " + Environment.NewLine;
            }
            else
            {
                TSQL += T4 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.SQL_VARIABLE_NAME   + Environment.NewLine;
            }

        }


         


        ColCount = 0;
        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_IN_WHERE_CLAUSE)
        {
            ColCount++;
            if (ColCount < COLUMNS_IN_WHERE_CLAUSE.Count)
            {
                SQL_WHERE_CLAUSE +=  T4 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.SQL_VARIABLE_NAME + " AND " + Environment.NewLine;
            }
            else
            {
                SQL_WHERE_CLAUSE += T4 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.SQL_VARIABLE_NAME   + Environment.NewLine;
            }

        }

        _usp_CUSTOM = TSQL + SQL_WHERE_CLAUSE;
        UTL_SAVE_FILE(SQL_FOLDER, _usp_CUSTOM_FILE_NAME, TSQL);
        return TSQL;
    }




    public String TSQL_INSERT_WHERE_NOT_EXISTS_CUSTOM(List<FREDS_MATRIX> COLUMNS_TO_INSERT, List<FREDS_MATRIX> COLUMNS_WHERE_NOT_EXISTS, String OBJECT_NAME, String DATABASE_NAME)
    {
        int ColCount = 0;
        string SQL_WHERE_CLAUSE = "WHERE " + Environment.NewLine + Environment.NewLine;
        String SP_NAME = null;
        String TSQL = null;
        string SQL_PROC_PARMS = null;



        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_WHERE_NOT_EXISTS)
        {
            ColCount++;
            if (ColCount < COLUMNS_WHERE_NOT_EXISTS.Count)
            {
                SQL_WHERE_CLAUSE += T4 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.SQL_VARIABLE_NAME + " AND " + Environment.NewLine;
            }
            else
            {
                SQL_WHERE_CLAUSE += T4 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.SQL_VARIABLE_NAME + " ) " + Environment.NewLine;
            }

        }









        SP_NAME = "usp_INS_WHERE_NOT_EXISTS" + OBJECT_NAME.ToUpper() + "_CUSTOM";
        _usp_CUSTOM_FILE_NAME = SP_NAME + ".SQL";

        TSQL = "USE [" + DATABASE_NAME + "]" + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "------------PROC CREATED BY Freddie ON " + DateTime.Now + Environment.NewLine + Environment.NewLine +
        "SET ANSI_NULLS ON" + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "SET QUOTED_IDENTIFIER ON" + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = '" + SP_NAME + "')" + Environment.NewLine + Environment.NewLine +
        "DROP PROCEDURE " + SP_NAME + Environment.NewLine + Environment.NewLine +
        "GO" + Environment.NewLine + Environment.NewLine +
        "CREATE PROC [dbo].[" + SP_NAME + "]" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
        TSQL += SQL_PROC_PARMS + Environment.NewLine + Environment.NewLine + Environment.NewLine;
        TSQL += T3 + "AS" + Environment.NewLine + Environment.NewLine;


        TSQL += T3 + "BEGIN" + Environment.NewLine + Environment.NewLine;
        TSQL += T3 + "IF NOT EXISTS( SELECT * FROM " + "dbo." + OBJECT_NAME.ToUpper() + Environment.NewLine + Environment.NewLine  ;
        TSQL += T3 + SQL_WHERE_CLAUSE + Environment.NewLine + Environment.NewLine;


        TSQL += T3 + "BEGIN" + Environment.NewLine + Environment.NewLine;
        TSQL += T3 + "INSERT INTO " + "dbo." + OBJECT_NAME.ToUpper() + Environment.NewLine + T4 + "(" + Environment.NewLine;
        ColCount = 0;
        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_TO_INSERT)
        {
            ColCount++;
            if (ColCount < COLUMNS_TO_INSERT.Count)
            {
                TSQL += T4 + ATTRIBUTE.COLUMN_NAME + "," + Environment.NewLine;
            }
            else
            {
                TSQL += T4 + ATTRIBUTE.COLUMN_NAME + Environment.NewLine + T4 + ")" + Environment.NewLine;
            }
        }
        TSQL += T4 + "VALUES" + Environment.NewLine + T4 + "(" + Environment.NewLine;
        ColCount = 0;
        foreach (FREDS_MATRIX ATTRIBUTE in COLUMNS_TO_INSERT)
        {
            ColCount++;
            if (ColCount < COLUMNS_TO_INSERT.Count)
            {
                TSQL += T4 + ATTRIBUTE.SQL_VARIABLE_NAME + "," + Environment.NewLine;
            }
            else
            {
                TSQL += T4 + ATTRIBUTE.SQL_VARIABLE_NAME + Environment.NewLine + T4 + ")" + Environment.NewLine;
            }
        }
        _usp_CUSTOM = TSQL;
        UTL_SAVE_FILE(SQL_FOLDER, _usp_CUSTOM_FILE_NAME, TSQL);
        return TSQL;
    }






        #endregion



    }
 
