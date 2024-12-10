using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;

 
  public  class UTL_CS_GENERATOR
    {

    private const String Q = @"""";
    private const String T1 = "\t";
    private const String T2 = "\t\t";
    private const String T3 = "\t\t\t";
    private const String T4 = "\t\t\t\t";
    private const String T5 = "\t\t\t\t\t";
    private const String NL = "\n";
    //  private const String AFTER_METHOD = "////END OF METHOD ////////////////////////////////////////////////////////////" + "\n" + "\n";

    private const String LOG_FILE = null;
    private String _SQL_WHERE = " WHERE " + Environment.NewLine;
    private Boolean _PK = false;
    private Boolean _PK_CONTAINS_IDENTITY_FIELD = false;
    private int _COLUMNS_IN_PK = 0;



    private String _CS_MODEL_FOLDER;
    private String _CS_MODEL_CLASS_FILE_NAME;
    private String _CS_MODEL_CLASS;

    private String _CS_READ_FOLDER;
    private String _CS_READ_CLASS_FILE_NAME;
    private String _CS_READ_CLASS;

    private String _CS_IUD_FOLDER;
    private String _CS_IUD_CLASS_FILE_NAME;
    private String _CS_IUD_CLASS;

    private String _CS_SQL_FOLDER;
 
    public System.String CS_MODEL_FOLDER
    {
        get { return _CS_MODEL_FOLDER; }
        set { _CS_MODEL_FOLDER = value; }

    }

    public System.String CS_READ_FOLDER
    {
        get { return _CS_READ_FOLDER; }
        set { _CS_READ_FOLDER = value; }

    }
    public System.String CS_IUD_FOLDER
    {
        get { return _CS_IUD_FOLDER; }
        set { _CS_IUD_FOLDER = value; }

    }
    public System.String CS_SQL_FOLDER
    {
        get { return _CS_SQL_FOLDER; }
        set { _CS_SQL_FOLDER = value; }

    }


    public System.String CS_READ_CLASS
    {
        get { return _CS_READ_CLASS; }

    }
    public System.String CS_IUD_CLASS
    {
        get { return _CS_IUD_CLASS; }

    }
    public System.String CS_MODEL_CLASS
    {
        get { return _CS_MODEL_CLASS; }

    }


    public System.String CS_READ_CLASS_FILE_NAME
    {
        get { return _CS_READ_CLASS_FILE_NAME; }
       
    }
    public System.String CS_IUD_CLASS_FILE_NAME
    {
        get { return _CS_IUD_CLASS_FILE_NAME; }
        
    }
    public System.String CS_MODEL_CLASS_FILE_NAME
    {
        get { return _CS_MODEL_CLASS_FILE_NAME; }
        
    }
    
    private List<CS_METHOD> CS_METHOD_LIST;




    #region MAIN ENTRY
    public void GENERATE_DEFAULT(String SERVER_NAME, String DATABASE_NAME, Int32 OBJECT_ID, String OBJECT_NAME, Boolean View)
    {
        CS_METHOD_LIST = new List<CS_METHOD>();
        if (View)
        {
            List<FREDS_MATRIX> ATTRIBUTES = UTL_GET_ATTRIBUTES_VIEW(SERVER_NAME, DATABASE_NAME.ToUpper(), OBJECT_ID);
            GENERATE_MODEL_CLASS(ATTRIBUTES, OBJECT_NAME);
            GENERATE_READ_CLASS_DEFAULT(  ATTRIBUTES, OBJECT_NAME, DATABASE_NAME, View);

        }
        else
        {
            List<FREDS_MATRIX> ATTRIBUTES = UTL_GET_ATTRIBUTES_TABLE(SERVER_NAME, DATABASE_NAME.ToUpper(), OBJECT_ID, false);
            List<FREDS_MATRIX> ATTRIBUTES_PK = UTL_GET_ATTRIBUTES_TABLE(SERVER_NAME, DATABASE_NAME.ToUpper(), OBJECT_ID, true);
            
            GENERATE_MODEL_CLASS(ATTRIBUTES, OBJECT_NAME);
            GENERATE_READ_CLASS_DEFAULT(  ATTRIBUTES, OBJECT_NAME, DATABASE_NAME, View);
            GENERATE_IUD_CLASS_DEFAULT(  ATTRIBUTES, ATTRIBUTES_PK, OBJECT_NAME, DATABASE_NAME);
            //GENERATE_GUI_DEFAULT(ATTRIBUTES, ATTRIBUTES_PK, OBJECT_NAME, DATABASE_NAME);

        }
       

    }

    public void GENERATE_MODEL_IGNORE_CASE(String SERVER_NAME, String DATABASE_NAME, Int32 OBJECT_ID, String OBJECT_NAME, Boolean View)
    {
        CS_METHOD_LIST = new List<CS_METHOD>();
        if (View)
        {
            //List<FREDS_MATRIX> ATTRIBUTES = UTL_GET_ATTRIBUTES_TABLE_IGNORE_CASE(SERVER_NAME, DATABASE_NAME.ToUpper(), OBJECT_ID);
            //GENERATE_MODEL_CLASS_IGNORE_CASE(ATTRIBUTES, OBJECT_NAME);
             

        }
        else
        {
            List<FREDS_MATRIX> ATTRIBUTES = UTL_GET_ATTRIBUTES_TABLE_IGNORE_CASE(SERVER_NAME, DATABASE_NAME.ToUpper(), OBJECT_ID, false);


            GENERATE_MODEL_CLASS_IGNORE_CASE(ATTRIBUTES, OBJECT_NAME);
           

        }


    }








    #endregion






    #region GENERATE C# METHODS


    private void METHOD_UPD_A_ROW_DEFAULT(  List<FREDS_MATRIX> COLUMNS_TO_UPDATE, List<FREDS_MATRIX> COLUMNS_IN_WHERE_CLAUSE, String OBJECT_NAME)
    {


        CS_METHOD CS_METHOD = new CS_METHOD
        {
            METHOD_TYPE = "UPDATE",

        };
        
            CS_METHOD.CUSTOM_METHOD = false;
            CS_METHOD.METHOD_NAME = "UPD_A_ROW_BY_PK";
       
        CS_METHOD.CS += T1 + "public System.Boolean " + CS_METHOD.METHOD_NAME + "("  + UTL_GENERATE_MODEL_CLASS_NAME(OBJECT_NAME.ToUpper() ) + " o)" + Environment.NewLine;
        CS_METHOD.CS += T1 + "{" + Environment.NewLine;
        CS_METHOD.CS += T2 + "try" + Environment.NewLine;
        CS_METHOD.CS += T2 + "{" + Environment.NewLine;
       
            CS_METHOD.CS += T3 + "cmd = new System.Data.SqlClient.SqlCommand(" + Q + "usp_UPD_A_ROW_BY_PK_" + OBJECT_NAME.ToUpper() + Q + ");" + Environment.NewLine;
        
        CS_METHOD.CS += T3 + "////COLUMNS TO UPDATE///////" + Environment.NewLine + UTL_GENERATE_CS_PARAMETERS(COLUMNS_TO_UPDATE, "UPD1" );
        CS_METHOD.CS += T3 + "////COLUMNS IN WHERE CLAUSE///////" + Environment.NewLine + UTL_GENERATE_CS_PARAMETERS(COLUMNS_IN_WHERE_CLAUSE, "UPD2" );
        CS_METHOD.CS += T3 + "cmd.CommandType = System.Data.CommandType.StoredProcedure;" + Environment.NewLine;
        CS_METHOD.CS += T3 + "return RUN_SQL_COMMAND(DEFAULT_DATABASE_NAME, DEFAULT_OBJECT_NAME);" + Environment.NewLine;
        CS_METHOD.CS += T2 + "}///TRY" + Environment.NewLine;
        CS_METHOD.CS += UTL_GENERATE_CATCH_SECTION_OF_METHOD(false);
        CS_METHOD_LIST.Add(CS_METHOD);
    }
    private void METHOD_INS_A_ROW_DEFAULT(  List<FREDS_MATRIX> COLUMNS_TO_INSERT, String OBJECT_NAME)
    {
        CS_METHOD CS_METHOD = new CS_METHOD
        {
            METHOD_TYPE = "INSERT",
        };
        
            CS_METHOD.CUSTOM_METHOD = false;
            CS_METHOD.METHOD_NAME = "INS_A_ROW";
         
        CS_METHOD.CS += T1 + "public System.Boolean " + CS_METHOD.METHOD_NAME + "("  +UTL_GENERATE_MODEL_CLASS_NAME(OBJECT_NAME.ToUpper() ) + " o)" + Environment.NewLine;
        CS_METHOD.CS += T1 + "{" + Environment.NewLine;
        CS_METHOD.CS += T2 + "try" + Environment.NewLine;
        CS_METHOD.CS += T2 + "{" + Environment.NewLine;

        CS_METHOD.CS += T3 + "cmd = new System.Data.SqlClient.SqlCommand(" + Q + "usp_INS_A_ROW_" + OBJECT_NAME.ToUpper() + Q + ");" + Environment.NewLine;
        CS_METHOD.CS += T3 + "////COLUMNS TO INSERT///////" + Environment.NewLine + UTL_GENERATE_CS_PARAMETERS(COLUMNS_TO_INSERT, "INS1" );

        CS_METHOD.CS += T3 + "cmd.CommandType = System.Data.CommandType.StoredProcedure;" + Environment.NewLine;
        CS_METHOD.CS += T3 + "return RUN_SQL_COMMAND(DEFAULT_DATABASE_NAME, DEFAULT_OBJECT_NAME);" + Environment.NewLine;
        CS_METHOD.CS += T2 + "}///TRY" + Environment.NewLine;
        CS_METHOD.CS += UTL_GENERATE_CATCH_SECTION_OF_METHOD(false);
        CS_METHOD_LIST.Add(CS_METHOD);
    }
    
    private void METHOD_DEL_A_ROW_BY_PK_DEFAULT(  List<FREDS_MATRIX> COLUMNS_IN_WHERE_CLAUSE, String OBJECT_NAME)
    {
            CS_METHOD CS_METHOD = new CS_METHOD
            {
                METHOD_TYPE = "DELETE",
            };
            
                CS_METHOD.CUSTOM_METHOD = false;
                CS_METHOD.METHOD_NAME = "DEL_A_ROW_BY_PK";
             
            
            CS_METHOD.CS += T1 + "public System.Boolean " + CS_METHOD.METHOD_NAME + "(" + UTL_GENERATE_MODEL_CLASS_NAME( OBJECT_NAME.ToUpper()  ) + " o)" + Environment.NewLine;
            CS_METHOD.CS += T1 + "{" + Environment.NewLine;
            CS_METHOD.CS += T2 + "try" + Environment.NewLine;
            CS_METHOD.CS += T2 + "{" + Environment.NewLine;
           
                //cmd = new System.Data.SqlClient.SqlCommand("usp_INS_TOTARA_JOB_ASSIGNMENT_GROUP");
                CS_METHOD.CS += T3 + "cmd = new System.Data.SqlClient.SqlCommand(" + Q + "usp_DEL_A_ROW_BY_PK_" + OBJECT_NAME.ToUpper() + Q + ");" + Environment.NewLine;
            
            CS_METHOD.CS += T3 + "////COLUMNS TO DELETE BY///////" + Environment.NewLine + UTL_GENERATE_CS_PARAMETERS(COLUMNS_IN_WHERE_CLAUSE, "DEL" );

            CS_METHOD.CS += T3 + "cmd.CommandType = System.Data.CommandType.StoredProcedure;" + Environment.NewLine;
            CS_METHOD.CS += T3 + "return RUN_SQL_COMMAND(DEFAULT_DATABASE_NAME, DEFAULT_OBJECT_NAME);" + Environment.NewLine;
            CS_METHOD.CS += T2 + "}" + Environment.NewLine;
            CS_METHOD.CS += UTL_GENERATE_CATCH_SECTION_OF_METHOD(false);
            CS_METHOD_LIST.Add(CS_METHOD);
    }
    private void METHOD_DEL_ALL_ROWS_DEFAULT(  String OBJECT_NAME)
    {
        CS_METHOD CS_METHOD = new CS_METHOD
        {
            METHOD_TYPE = "DELETE",
        };
       
            CS_METHOD.CUSTOM_METHOD = false;
            CS_METHOD.METHOD_NAME = "DEL_ALL_ROWS";
        
        CS_METHOD.CS += T1 + "public System.Boolean " + CS_METHOD.METHOD_NAME + "()" + Environment.NewLine;
        CS_METHOD.CS += T1 + "{" + Environment.NewLine;
        CS_METHOD.CS += T2 + "try" + Environment.NewLine;
        CS_METHOD.CS += T2 + "{" + Environment.NewLine;
       
            CS_METHOD.CS += T3 + "cmd = new System.Data.SqlClient.SqlCommand(" + Q + "usp_DEL_ALL_ROWS_" + OBJECT_NAME.ToUpper() + Q + ");" + Environment.NewLine;
      
     

        CS_METHOD.CS += T3 + "cmd.CommandType = System.Data.CommandType.StoredProcedure;" + Environment.NewLine;
        CS_METHOD.CS += T3 + "return RUN_SQL_COMMAND(DEFAULT_DATABASE_NAME, DEFAULT_OBJECT_NAME);" + Environment.NewLine;
        CS_METHOD.CS += T2 + "}" + Environment.NewLine;
        CS_METHOD.CS += UTL_GENERATE_CATCH_SECTION_OF_METHOD(false);
        CS_METHOD_LIST.Add(CS_METHOD);
    }
    private String METHOD_SEL_A_ROW_BY_PK_DEFAULT(  List<FREDS_MATRIX> COLUMNS_TO_SELECT, String OBJECT_NAME)
    {

        if (_PK)
        {
            CS_METHOD CS_METHOD = new CS_METHOD
            {
                METHOD_TYPE = "SELECT",
                METHOD_NAME = "SEL_A_ROW_BY_PK",
               // OPTION_DEFAULT = CS_METHOD_OPTION_DEFAULT.SELECT_BY_PK,
                CUSTOM_METHOD = false
            };
            
               
            

            CS_METHOD.CS += T1 + "public System.Data.DataSet " + CS_METHOD.METHOD_NAME + "( " + UTL_GENERATE_MODEL_CLASS_NAME(OBJECT_NAME  ) + " o)" + Environment.NewLine;
            CS_METHOD.CS += T1 + "{" + Environment.NewLine;
            CS_METHOD.CS += T2 + "try" + Environment.NewLine;
            CS_METHOD.CS += T2 + "{" + Environment.NewLine;


            
            CS_METHOD.CS += T3 + "cmd = new System.Data.SqlClient.SqlCommand(" + Q + UTL_GENERATE_USP_SEL_BY_PK(OBJECT_NAME) + Q + ");" + Environment.NewLine;
            

            CS_METHOD.CS += T3 + "////COLUMNS TO SELECT BY///////" + Environment.NewLine + UTL_GENERATE_CS_PARAMETERS(COLUMNS_TO_SELECT, "SEL" ) + Environment.NewLine + Environment.NewLine;
            CS_METHOD.CS += T3 + "cmd.CommandType = System.Data.CommandType.StoredProcedure;" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            CS_METHOD.CS += T3 + "return GET_SP_DATASET(DEFAULT_DATABASE_NAME, cmd);  " + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            CS_METHOD.CS += T2 + "}" + Environment.NewLine;
            CS_METHOD.CS += UTL_GENERATE_CATCH_SECTION_OF_METHOD(true);
            CS_METHOD_LIST.Add(CS_METHOD);
            return CS_METHOD.CS;
        }
        else
        {
            return Environment.NewLine;
        }




    }
    private String METHOD_SEL_ALL_DEFAULT(  List<FREDS_MATRIX> COLUMNS_TO_SELECT ,String OBJECT_NAME)
    {
        CS_METHOD CS_METHOD = new CS_METHOD
        {
            METHOD_TYPE = "SELECT",
            CUSTOM_METHOD = false,
            METHOD_NAME ="SEL_ALL",
        

        };
        CS_METHOD.CS += T1 + "public System.Data.DataSet " + CS_METHOD.METHOD_NAME + "()" + Environment.NewLine;
        CS_METHOD.CS += T1 + "{" + Environment.NewLine;
        CS_METHOD.CS += T2 + "try" + Environment.NewLine;
        CS_METHOD.CS += T2 + "{" + Environment.NewLine;
        CS_METHOD.CS += T3 + "cmd = new System.Data.SqlClient.SqlCommand(" + Q + UTL_GENERATE_USP_SEL_ALL(OBJECT_NAME) + Q + ");" + Environment.NewLine + Environment.NewLine;
        CS_METHOD.CS += T3 + "cmd.CommandType = System.Data.CommandType.StoredProcedure;" + Environment.NewLine + Environment.NewLine;
        CS_METHOD.CS += T3 + "return GET_SP_DATASET(DEFAULT_DATABASE_NAME, cmd);  " + Environment.NewLine + Environment.NewLine;
        CS_METHOD.CS += T2 + "}" + Environment.NewLine;
        CS_METHOD.CS += UTL_GENERATE_CATCH_SECTION_OF_METHOD(true);
        CS_METHOD_LIST.Add(CS_METHOD);
        return CS_METHOD.CS;



    }
    public String METHOD_SEL_CUSTOM(List<FREDS_MATRIX> COLUMNS_TO_SELECT, List<FREDS_MATRIX> COLUMNS_IN_WHERE_CLAUSE, String OBJECT_NAME)
    {
        CS_METHOD CS_METHOD = new CS_METHOD
        {
            METHOD_TYPE = "SELECT",
            CUSTOM_METHOD = true,
            METHOD_NAME = "SEL_CUSTOM" 
        };
        CS_METHOD.CS += T1 + "public System.Data.DataSet " + CS_METHOD.METHOD_NAME + "( " + UTL_GENERATE_MODEL_CLASS_NAME(OBJECT_NAME) + " o)" + Environment.NewLine;
        CS_METHOD.CS += T1 + "{" + Environment.NewLine;
        CS_METHOD.CS += T2 + "try" + Environment.NewLine;
        CS_METHOD.CS += T2 + "{" + Environment.NewLine;
        CS_METHOD.CS += T3 + "cmd = new System.Data.SqlClient.SqlCommand(" + Q + UTL_GENERATE_USP_DML_CUSTOM(OBJECT_NAME, "SEL") + Q + ");" + Environment.NewLine + Environment.NewLine;

        CS_METHOD.CS += T3 + "////COLUMNS TO SELECT BY///////" + Environment.NewLine + Environment.NewLine + 
            UTL_GENERATE_CS_PARAMETERS(COLUMNS_IN_WHERE_CLAUSE) + Environment.NewLine + Environment.NewLine;

        CS_METHOD.CS += T3 + "cmd.CommandType = System.Data.CommandType.StoredProcedure;" + Environment.NewLine + Environment.NewLine;
        CS_METHOD.CS += T3 + "return GET_SP_DATASET(DEFAULT_DATABASE_NAME, cmd);  " + Environment.NewLine + Environment.NewLine;
        CS_METHOD.CS += T2 + "}" + Environment.NewLine;
        CS_METHOD.CS += UTL_GENERATE_CATCH_SECTION_OF_METHOD(true);
       
        return CS_METHOD.CS;
       


    }
    public string  METHOD_SEL_DISTINCT_CUSTOM(  String DistinctColumn, List<FREDS_MATRIX> COLUMNS_IN_WHERE_CLAUSE, String OBJECT_NAME)
    {
        CS_METHOD CS_METHOD = new CS_METHOD
        {
            METHOD_TYPE = "SELECT",
            CUSTOM_METHOD = true,
            METHOD_NAME = "SEL_DISTINCT_CUSTOM"


        };

        CS_METHOD.CS += T1 + "public System.Data.DataSet " + CS_METHOD.METHOD_NAME + "( " + UTL_GENERATE_MODEL_CLASS_NAME(OBJECT_NAME) + " o)" + Environment.NewLine;
        CS_METHOD.CS += T1 + "{" + Environment.NewLine;
        CS_METHOD.CS += T2 + "try" + Environment.NewLine;
        CS_METHOD.CS += T2 + "{" + Environment.NewLine;
        CS_METHOD.CS += T3 + "cmd = new System.Data.SqlClient.SqlCommand(" + Q + UTL_GENERATE_USP_DML_CUSTOM(OBJECT_NAME, "SEL_DISTINCT") + Q + ");" + Environment.NewLine + Environment.NewLine;

        CS_METHOD.CS += T3 + "////COLUMNS TO SELECT BY///////" + Environment.NewLine + Environment.NewLine +
            UTL_GENERATE_CS_PARAMETERS(COLUMNS_IN_WHERE_CLAUSE) + Environment.NewLine + Environment.NewLine;

        CS_METHOD.CS += T3 + "cmd.CommandType = System.Data.CommandType.StoredProcedure;" + Environment.NewLine + Environment.NewLine;
        CS_METHOD.CS += T3 + "return GET_SP_DATASET(DEFAULT_DATABASE_NAME, cmd);  " + Environment.NewLine + Environment.NewLine;
        CS_METHOD.CS += T2 + "}" + Environment.NewLine;
        CS_METHOD.CS += UTL_GENERATE_CATCH_SECTION_OF_METHOD(true);

        return CS_METHOD.CS;
    }




    public string METHOD_INS_CUSTOM(  List<FREDS_MATRIX> COLUMNS_TO_INSERT, String OBJECT_NAME)
    {
        CS_METHOD CS_METHOD = new CS_METHOD
        {
            METHOD_TYPE = "INSERT",
            CUSTOM_METHOD = true,
            METHOD_NAME = "INS_CUSTOM"
        };

        CS_METHOD.CS += T1 + "public System.Data.DataSet " + CS_METHOD.METHOD_NAME + "( " + UTL_GENERATE_MODEL_CLASS_NAME(OBJECT_NAME) + " o)" + Environment.NewLine;
        CS_METHOD.CS += T1 + "{" + Environment.NewLine;
        CS_METHOD.CS += T2 + "try" + Environment.NewLine;
        CS_METHOD.CS += T2 + "{" + Environment.NewLine;
        CS_METHOD.CS += T3 + "cmd = new System.Data.SqlClient.SqlCommand(" + Q + UTL_GENERATE_USP_DML_CUSTOM(OBJECT_NAME, "INS") + Q + ");" + Environment.NewLine + Environment.NewLine;

        CS_METHOD.CS += T3 + "////COLUMNS TO INSERT///////" + Environment.NewLine + Environment.NewLine +
            UTL_GENERATE_CS_PARAMETERS(COLUMNS_TO_INSERT) + Environment.NewLine + Environment.NewLine;

        CS_METHOD.CS += T3 + "cmd.CommandType = System.Data.CommandType.StoredProcedure;" + Environment.NewLine + Environment.NewLine;
        CS_METHOD.CS += T3 + "return GET_SP_DATASET(DEFAULT_DATABASE_NAME, cmd);  " + Environment.NewLine + Environment.NewLine;
        CS_METHOD.CS += T2 + "}" + Environment.NewLine;
        CS_METHOD.CS += UTL_GENERATE_CATCH_SECTION_OF_METHOD(true);

        return CS_METHOD.CS;
    }

    public string METHOD_INS_WHERE_NOT_EXISTS_CUSTOM(List<FREDS_MATRIX> COLUMNS_TO_INSERT, List<FREDS_MATRIX> COLUMNS_IN_WHERE_NOT_EXISTS, String OBJECT_NAME)
    {
        CS_METHOD CS_METHOD = new CS_METHOD
        {
            METHOD_TYPE = "UPDATE",
            CUSTOM_METHOD = true,
            METHOD_NAME = "UPD_WHERE_NOT_EXISTS_CUSTOM"
        };

        CS_METHOD.CS += T1 + "public System.Data.DataSet " + CS_METHOD.METHOD_NAME + "( " + UTL_GENERATE_MODEL_CLASS_NAME(OBJECT_NAME) + " o)" + Environment.NewLine;
        CS_METHOD.CS += T1 + "{" + Environment.NewLine;
        CS_METHOD.CS += T2 + "try" + Environment.NewLine;
        CS_METHOD.CS += T2 + "{" + Environment.NewLine;
        CS_METHOD.CS += T3 + "cmd = new System.Data.SqlClient.SqlCommand(" + Q + UTL_GENERATE_USP_DML_CUSTOM(OBJECT_NAME, "UPD_WHERE_NOT_EXISTS") + Q + ");" + Environment.NewLine + Environment.NewLine;

        CS_METHOD.CS += T3 + "////COLUMNS TO INSERT///////" + Environment.NewLine + Environment.NewLine +
           UTL_GENERATE_CS_PARAMETERS(COLUMNS_TO_INSERT) + Environment.NewLine + Environment.NewLine;

        CS_METHOD.CS += T3 + "////COLUMNS IN NOT EXISTS///////" + Environment.NewLine + Environment.NewLine +
            UTL_GENERATE_CS_PARAMETERS(COLUMNS_IN_WHERE_NOT_EXISTS) + Environment.NewLine + Environment.NewLine;

        CS_METHOD.CS += T3 + "cmd.CommandType = System.Data.CommandType.StoredProcedure;" + Environment.NewLine + Environment.NewLine;
        CS_METHOD.CS += T3 + "return GET_SP_DATASET(DEFAULT_DATABASE_NAME, cmd);  " + Environment.NewLine + Environment.NewLine;
        CS_METHOD.CS += T2 + "}" + Environment.NewLine;
        CS_METHOD.CS += UTL_GENERATE_CATCH_SECTION_OF_METHOD(true);

        return CS_METHOD.CS;
    }

    public string METHOD_DEL_CUSTOM(List<FREDS_MATRIX> COLUMNS_IN_WHERE_CLAUSE, String OBJECT_NAME)
    {
        CS_METHOD CS_METHOD = new CS_METHOD
        {
            METHOD_TYPE = "DELETE",
            CUSTOM_METHOD = true,
            METHOD_NAME = "DEL_CUSTOM"


        };

        CS_METHOD.CS += T1 + "public System.Data.DataSet " + CS_METHOD.METHOD_NAME + "( " + UTL_GENERATE_MODEL_CLASS_NAME(OBJECT_NAME) + " o)" + Environment.NewLine;
        CS_METHOD.CS += T1 + "{" + Environment.NewLine;
        CS_METHOD.CS += T2 + "try" + Environment.NewLine;
        CS_METHOD.CS += T2 + "{" + Environment.NewLine;
        CS_METHOD.CS += T3 + "cmd = new System.Data.SqlClient.SqlCommand(" + Q + UTL_GENERATE_USP_DML_CUSTOM(OBJECT_NAME, "DEL") + Q + ");" + Environment.NewLine + Environment.NewLine;

        CS_METHOD.CS += T3 + "////COLUMNS TO DELETE BY///////" + Environment.NewLine + Environment.NewLine +
            UTL_GENERATE_CS_PARAMETERS(COLUMNS_IN_WHERE_CLAUSE) + Environment.NewLine + Environment.NewLine;

        CS_METHOD.CS += T3 + "cmd.CommandType = System.Data.CommandType.StoredProcedure;" + Environment.NewLine + Environment.NewLine;
        CS_METHOD.CS += T3 + "return GET_SP_DATASET(DEFAULT_DATABASE_NAME, cmd);  " + Environment.NewLine + Environment.NewLine;
        CS_METHOD.CS += T2 + "}" + Environment.NewLine;
        CS_METHOD.CS += UTL_GENERATE_CATCH_SECTION_OF_METHOD(true);

        return CS_METHOD.CS;
    }



    public string METHOD_UPD_CUSTOM(List<FREDS_MATRIX> COLUMNS_TO_UPDATE, List<FREDS_MATRIX> COLUMNS_IN_WHERE_CLAUSE, String OBJECT_NAME)
    {
        CS_METHOD CS_METHOD = new CS_METHOD
        {
            METHOD_TYPE = "UPDATE",
            CUSTOM_METHOD = true,
            METHOD_NAME = "UPD_CUSTOM"
        };

        CS_METHOD.CS += T1 + "public System.Data.DataSet " + CS_METHOD.METHOD_NAME + "( " + UTL_GENERATE_MODEL_CLASS_NAME(OBJECT_NAME) + " o)" + Environment.NewLine;
        CS_METHOD.CS += T1 + "{" + Environment.NewLine;
        CS_METHOD.CS += T2 + "try" + Environment.NewLine;
        CS_METHOD.CS += T2 + "{" + Environment.NewLine;
        CS_METHOD.CS += T3 + "cmd = new System.Data.SqlClient.SqlCommand(" + Q + UTL_GENERATE_USP_DML_CUSTOM(OBJECT_NAME, "UPD") + Q + ");" + Environment.NewLine + Environment.NewLine;

        CS_METHOD.CS += T3 + "////COLUMNS TO UPDATE///////" + Environment.NewLine + Environment.NewLine +
           UTL_GENERATE_CS_PARAMETERS(COLUMNS_TO_UPDATE) + Environment.NewLine + Environment.NewLine;

        CS_METHOD.CS += T3 + "////COLUMNS TO UPDATE BY///////" + Environment.NewLine + Environment.NewLine +
            UTL_GENERATE_CS_PARAMETERS(COLUMNS_IN_WHERE_CLAUSE) + Environment.NewLine + Environment.NewLine;

        CS_METHOD.CS += T3 + "cmd.CommandType = System.Data.CommandType.StoredProcedure;" + Environment.NewLine + Environment.NewLine;
        CS_METHOD.CS += T3 + "return GET_SP_DATASET(DEFAULT_DATABASE_NAME, cmd);  " + Environment.NewLine + Environment.NewLine;
        CS_METHOD.CS += T2 + "}" + Environment.NewLine;
        CS_METHOD.CS += UTL_GENERATE_CATCH_SECTION_OF_METHOD(true);

        return CS_METHOD.CS;
    }









    #endregion


    #region GENERATE C# CLASS
    private void GENERATE_MODEL_CLASS(List<FREDS_MATRIX> CLASS_PROPERTIES, String OBJECT_NAME)
        {
            _CS_MODEL_CLASS_FILE_NAME = UTL_GENERATE_MODEL_CLASS_NAME(OBJECT_NAME) + ".cs";
            _CS_MODEL_CLASS = "///////MODEL CLASS CREATED ON " + DateTime.Now + Environment.NewLine + Environment.NewLine +
            "public class " + UTL_GENERATE_MODEL_CLASS_NAME(OBJECT_NAME) + Environment.NewLine +
            "{" + Environment.NewLine;
            foreach(FREDS_MATRIX f in CLASS_PROPERTIES)
            {
                _CS_MODEL_CLASS += T1 + f.CS_PRIVATE_VARIABLE;
            }
            _CS_MODEL_CLASS += Environment.NewLine + Environment.NewLine;
            foreach (FREDS_MATRIX f in CLASS_PROPERTIES)
            {
                _CS_MODEL_CLASS += T1 + f.CS_PUBLIC_PROPERTY + Environment.NewLine;
            }
            _CS_MODEL_CLASS +=     "}   /////END OF CLASS" + Environment.NewLine + Environment.NewLine;
            UTL_SAVE_FILE(CS_MODEL_FOLDER, CS_MODEL_CLASS_FILE_NAME, CS_MODEL_CLASS);
        }


    private void GENERATE_MODEL_CLASS_IGNORE_CASE(List<FREDS_MATRIX> CLASS_PROPERTIES, String OBJECT_NAME)
    {
        _CS_MODEL_CLASS_FILE_NAME = UTL_GENERATE_MODEL_CLASS_NAME(OBJECT_NAME) + ".cs";
        _CS_MODEL_CLASS = "///////MODEL CLASS CREATED ON " + DateTime.Now + Environment.NewLine + Environment.NewLine +
        "public class " + UTL_GENERATE_MODEL_CLASS_NAME(OBJECT_NAME) + Environment.NewLine +
        "{" + Environment.NewLine;
        foreach (FREDS_MATRIX f in CLASS_PROPERTIES)
        {
            _CS_MODEL_CLASS += T1 + f.CS_PRIVATE_VARIABLE;
        }
        _CS_MODEL_CLASS += Environment.NewLine + Environment.NewLine;
        foreach (FREDS_MATRIX f in CLASS_PROPERTIES)
        {
            _CS_MODEL_CLASS += T1 + f.CS_PUBLIC_PROPERTY + Environment.NewLine;
        }
        _CS_MODEL_CLASS += "}   /////END OF CLASS" + Environment.NewLine + Environment.NewLine;
        UTL_SAVE_FILE(CS_MODEL_FOLDER, CS_MODEL_CLASS_FILE_NAME, CS_MODEL_CLASS);
     
    }


    //*******************************GENERATE READ CLASS
    private void GENERATE_READ_CLASS_DEFAULT(  List<FREDS_MATRIX> COLUMNS_TO_SELECT, String OBJECT_NAME, String DATABASE_NAME, bool View)
        {
                _CS_READ_CLASS_FILE_NAME = UTL_GENERATE_READ_CLASS_NAME(OBJECT_NAME ) + ".cs";
                _CS_READ_CLASS = "///////READ CLASS CREATED ON " + DateTime.Now + Environment.NewLine + Environment.NewLine +
                "public class " + UTL_GENERATE_READ_CLASS_NAME(OBJECT_NAME ) + " : dbUTL" + Environment.NewLine +
                "{" + Environment.NewLine + UTL_GENERATE_TOP_OF_CLASS(OBJECT_NAME, DATABASE_NAME);

                ///CREATE EACH METHOD THEN LOOP THROUGH THEM IN ORDER TO ADD EACH METHOD TO THE CLASS
                METHOD_SEL_ALL_DEFAULT(  COLUMNS_TO_SELECT , OBJECT_NAME);
                if (_PK)
                {
                    METHOD_SEL_A_ROW_BY_PK_DEFAULT(  COLUMNS_TO_SELECT, OBJECT_NAME);
                }
            
                foreach (  CS_METHOD m in CS_METHOD_LIST)
                {
                    _CS_READ_CLASS += m.CS;
                }
                _CS_READ_CLASS += "}   /////END OF READ CLASS" + Environment.NewLine + Environment.NewLine;
                UTL_SAVE_FILE(CS_READ_FOLDER, CS_READ_CLASS_FILE_NAME, CS_READ_CLASS);
        }
        //*******************************GENERATE READ CLASS











    private void GENERATE_IUD_CLASS_DEFAULT( List<FREDS_MATRIX> COLUMNS_IN_TABLE, List<FREDS_MATRIX> COLUMNS_IN_WHERE_CLAUSE, String OBJECT_NAME, String DATABASE_NAME)
        {
                _CS_IUD_CLASS_FILE_NAME = UTL_GENERATE_IUD_CLASS_NAME(OBJECT_NAME) + ".cs";
                _CS_IUD_CLASS = "///////IUD CLASS CREATED ON " + DateTime.Now + Environment.NewLine + Environment.NewLine +
                "public class " + UTL_GENERATE_IUD_CLASS_NAME(OBJECT_NAME ) + " : dbUTL" + Environment.NewLine +
                "{" + Environment.NewLine + UTL_GENERATE_TOP_OF_CLASS(OBJECT_NAME, DATABASE_NAME);
            
                METHOD_DEL_ALL_ROWS_DEFAULT(  OBJECT_NAME);
                METHOD_INS_A_ROW_DEFAULT(  COLUMNS_IN_TABLE, OBJECT_NAME);
                if(_PK)
                {
                    METHOD_DEL_A_ROW_BY_PK_DEFAULT(COLUMNS_IN_WHERE_CLAUSE, OBJECT_NAME);
                    METHOD_UPD_A_ROW_DEFAULT(COLUMNS_IN_TABLE, COLUMNS_IN_WHERE_CLAUSE, OBJECT_NAME);
                }
                
                foreach (CS_METHOD m in CS_METHOD_LIST)
                {
                    switch (m.METHOD_TYPE)
                    {
                        case "INSERT":
                            _CS_IUD_CLASS += m.CS;
                            break;
                        case "UPDATE":
                            _CS_IUD_CLASS += m.CS;
                            break;
                        case "DELETE":
                            _CS_IUD_CLASS += m.CS;
                            break;
                    }
                }
            _CS_IUD_CLASS += "}   /////END OF IUD CLASS" + Environment.NewLine + Environment.NewLine;
            UTL_SAVE_FILE(CS_IUD_FOLDER, CS_IUD_CLASS_FILE_NAME, CS_IUD_CLASS);
        }






    //private void GENERATE_GUI_DEFAULT(List<FREDS_MATRIX> COLUMNS_IN_TABLE, List<FREDS_MATRIX> COLUMNS_IN_WHERE_CLAUSE, String OBJECT_NAME, String DATABASE_NAME)
    //{
       
    //    if (_PK)
    //    {
    //        METHOD_DEL_A_ROW_BY_PK_DEFAULT(COLUMNS_IN_WHERE_CLAUSE, OBJECT_NAME);
    //        METHOD_UPD_A_ROW_DEFAULT(COLUMNS_IN_TABLE, COLUMNS_IN_WHERE_CLAUSE, OBJECT_NAME);
    //    }

    //    foreach (CS_METHOD m in CS_METHOD_LIST)
    //    {
    //        switch (m.METHOD_TYPE)
    //        {
    //            case "INSERT":
    //                _CS_IUD_CLASS += m.CS;
    //                break;
    //            case "UPDATE":
    //                _CS_IUD_CLASS += m.CS;
    //                break;
    //            case "DELETE":
    //                _CS_IUD_CLASS += m.CS;
    //                break;
    //        }
    //    }
    //    _CS_IUD_CLASS += "}   /////END OF IUD CLASS" + Environment.NewLine + Environment.NewLine;
    //    UTL_SAVE_FILE(CS_IUD_FOLDER, CS_IUD_CLASS_FILE_NAME, CS_IUD_CLASS);
    //}







    //public String GENERATE_CUSTOM(String SERVER_NAME, String DATABASE_NAME, Int32 OBJECT_ID, String OBJECT_NAME, Boolean View, String DML, String COLUMN_NAMES, String COLUMNS_IN_WHERE_CLAUSE)
    //{
    //    String MyString = null;
    //    CS_METHOD_LIST = new List<CS_METHOD>();
    //    if (View)
    //    {
    //        List<FREDS_MATRIX> LST_COLUMN_NAMES = UTL_GET_ATTRIBUTES_VIEW(SERVER_NAME, DATABASE_NAME.ToUpper(), OBJECT_ID);
    //        List<FREDS_MATRIX> LST_COLUMN_IN_WHERE_CLAUSE = UTL_GET_ATTRIBUTES_VIEW(SERVER_NAME, DATABASE_NAME.ToUpper(), OBJECT_ID, COLUMNS_IN_WHERE_CLAUSE);

    //       // MyString= METHOD_SEL(false, LST_COLUMN_NAMES, LST_COLUMN_IN_WHERE_CLAUSE, OBJECT_NAME);


    //    }
    //    else
    //    {
    //        List<FREDS_MATRIX> ATTRIBUTES = UTL_GET_ATTRIBUTES_TABLE(SERVER_NAME, DATABASE_NAME.ToUpper(), OBJECT_ID, false);
    //        List<FREDS_MATRIX> ATTRIBUTES_PK = UTL_GET_ATTRIBUTES_TABLE(SERVER_NAME, DATABASE_NAME.ToUpper(), OBJECT_ID, true);



    //    }
    //    return MyString;

    //}
    #endregion



    #region UTL

    private void UTL_SAVE_FILE(string FOLDER_NAME, string FILE_NAME, string FILE_CONTENT)
    {

        if (!Directory.Exists(FOLDER_NAME))
        {
            Directory.CreateDirectory(FOLDER_NAME);
        }
        if(File.Exists(FOLDER_NAME + FILE_NAME))
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


    private String UTL_GENERATE_TOP_OF_CLASS(String OBJECT_NAME, String DATABASE_NAME)
    {
        return
                T1 + "private const System.String DEFAULT_OBJECT_NAME =" + Q + OBJECT_NAME + Q + ";" + Environment.NewLine +
                T1 + "private const System.String DEFAULT_DATABASE_NAME =" + Q + DATABASE_NAME + Q + ";" + Environment.NewLine;

              //T1 + "public event EVENT_HANDLER INFORMATION_EVENT_VERBOSE_LOCAL;" + Environment.NewLine +
              //T1 + "public event EVENT_HANDLER INFORMATION_EVENT_LOCAL;" + Environment.NewLine +
              //T1 + "public event EVENT_HANDLER WARNING_EVENT_LOCAL;" + Environment.NewLine +
              //T1 + "public event EVENT_HANDLER ERROR_EVENT_LOCAL;" + Environment.NewLine +

              //T1 + "private void LOG_INFORMATION_EVENT_VERBOSE_LOCAL(string MSG)" + Environment.NewLine +
              //T1 + "{" + Environment.NewLine +
              //T2 + "if (INFORMATION_EVENT_VERBOSE_LOCAL != null)" + Environment.NewLine +
              //T2 + "{" + Environment.NewLine +
              //T3 + "INFORMATION_EVENT_VERBOSE_LOCAL(this, new EVENT_HANDLER_SUPER(MSG));" + Environment.NewLine +
              //T2 + "}" + Environment.NewLine +
              //T1 + "}" + Environment.NewLine +

              //T1 + "private void LOG_INFORMATION_EVENT_LOCAL(string MSG)" + Environment.NewLine +
              //T1 + "{" + Environment.NewLine +
              //T2 + "if (INFORMATION_EVENT_LOCAL != null)" + Environment.NewLine +
              //T2 + "{" + Environment.NewLine +
              //T3 + "INFORMATION_EVENT_LOCAL(this, new EVENT_HANDLER_SUPER(MSG));" + Environment.NewLine +
              //T2 + "}" + Environment.NewLine +
              //T1 + "}" + Environment.NewLine +

              //T1 + "private void LOG_WARNING_EVENT_LOCAL(string MSG)" + Environment.NewLine +
              //T1 + "{" + Environment.NewLine +
              //T2 + "if (WARNING_EVENT_LOCAL != null)" + Environment.NewLine +
              //T2 + "{" + Environment.NewLine +
              //T3 + "WARNING_EVENT_LOCAL(this, new EVENT_HANDLER_SUPER(MSG));" + Environment.NewLine +
              //T2 + "}" + Environment.NewLine +
              //T1 + "}" + Environment.NewLine +

              //T1 + "private void LOG_ERROR_EVENT_LOCAL(string MSG)" + Environment.NewLine +
              //T1 + "{" + Environment.NewLine +
              //T2 + "if (ERROR_EVENT_LOCAL != null)" + Environment.NewLine +
              //T2 + "{" + Environment.NewLine +
              //T3 + "ERROR_EVENT_LOCAL(this, new EVENT_HANDLER_SUPER(MSG));" + Environment.NewLine +
              //T2 + "}" + Environment.NewLine +
              //T1 + "}" + Environment.NewLine;
    }


    private String UTL_GENERATE_CATCH_SECTION_OF_METHOD(Boolean ReadMethod)
    {
        String s = null;
        s += T2 + "catch (System.Data.SqlClient.SqlException SqlException)" + Environment.NewLine;
        s += T2 + "{" + Environment.NewLine;
    //    s += T3 + "LOG_ERROR_EVENT_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|" + Q + @" + SqlException.Message);" + Environment.NewLine;
        if(ReadMethod)
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



    private string UTL_GENERATE_USP_DML_CUSTOM(string OBJECT_NAME, string DML_TYPE)
    {
        return "usp_" + DML_TYPE + "_" + OBJECT_NAME.ToUpper() + "_CUSTOM";
    }


 
    private string UTL_GENERATE_USP_SEL_ALL(string OBJECT_NAME)
    {
        return "usp_SEL_ALL_" + OBJECT_NAME.ToUpper();
    }
    private string UTL_GENERATE_USP_SEL_BY_PK(string OBJECT_NAME)
    {
        return "usp_SEL_A_ROW_BY_PK_" + OBJECT_NAME.ToUpper();
    }

    private string UTL_GENERATE_MODEL_CLASS_NAME(string OBJECT_NAME )
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


            return MyString + "Model";
                 
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
    private string UTL_GENERATE_IUD_CLASS_NAME(string OBJECT_NAME)
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


            return MyString + "IUD";

        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    private String UTL_GENERATE_CS_PARAMETERS(List<FREDS_MATRIX> CS_PARAMETERS, String DML )
    {
        String s = null;
        foreach (FREDS_MATRIX ATTRIBUTE in CS_PARAMETERS)
        {
             
                switch (DML)
                {
                    case "INS1":
                        if (ATTRIBUTE.INSERT_YN == "Y")
                        {
                            s += T3 + ATTRIBUTE.CS_PARM_STRING + Environment.NewLine;
                        }
                        break;
                    case "INS2":
                        if (ATTRIBUTE.PK_YN == "Y")
                        {
                            s += T3 + ATTRIBUTE.CS_PARM_STRING + Environment.NewLine;
                        }
                        break;
                    case "UPD1":
                        if (ATTRIBUTE.UPDATE_YN == "Y")
                        {
                            s += T3 + ATTRIBUTE.CS_PARM_STRING + Environment.NewLine;
                        }
                        break;
                    case "UPD2":
                        if (ATTRIBUTE.PK_YN == "Y")
                        {
                            s += T3 + ATTRIBUTE.CS_PARM_STRING + Environment.NewLine;
                        }
                        break;
                    case "SEL":
                        if (ATTRIBUTE.PK_YN == "Y")
                        {
                            s += T3 + ATTRIBUTE.CS_PARM_STRING + Environment.NewLine;
                        }
                        break;
                    case "DEL":
                        if (ATTRIBUTE.PK_YN == "Y")
                        {
                            s += T3 + ATTRIBUTE.CS_PARM_STRING + Environment.NewLine;
                        }
                        break;

                }
            
           

        }


        return s;

    }

    private String UTL_GENERATE_CS_PARAMETERS(List<FREDS_MATRIX> CS_PARAMETERS )
    {
        String s = null;
        foreach (FREDS_MATRIX ATTRIBUTE in CS_PARAMETERS)
        {

            
                        s += T3 + ATTRIBUTE.CS_PARM_STRING + Environment.NewLine;
                  


        }


        return s;

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
                        o.SQL_PROC_PARM_STRING = "@" + R["COLUMN_NAME"].ToString().ToUpper() + " "+ o.SQL_DATA_TYPE + "(" + o.SQL_MAX_LENGTH + ")";
                        break;
                    case "CHAR":
                        o.SQL_PROC_PARM_STRING = "@" + R["COLUMN_NAME"].ToString().ToUpper() + " "+o.SQL_DATA_TYPE + "(" + o.SQL_MAX_LENGTH + ")";
                        break;
                    case "DECIMAL":
                        o.SQL_PROC_PARM_STRING = "@" + R["COLUMN_NAME"].ToString().ToUpper() + " "+o.SQL_DATA_TYPE + "(" + o.SQL_PRECISION + "," + o.SQL_SCALE + ")";
                        break;
                }
                if (o.SQL_IDENTITY_FIELD_YN == "1")
                {
                    o.UPDATE_YN = "N";
                    o.INSERT_YN = "N";
                    o.SQL_IDENTITY_FIELD_YN = "Y";
                    if(o.PK_YN == "Y" )
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
                        o.CS_PUBLIC_PROPERTY =   "public " + R["DataType"].ToString() + " " + o.COLUMN_NAME.ToString().ToUpper() + Environment.NewLine +
                            T1 +  "{ " + Environment.NewLine +
                                T2 + "get { return _" + o.COLUMN_NAME.ToString().ToUpper() + "; }" + Environment.NewLine +
                                T2 + "set { _" + o.COLUMN_NAME.ToString().ToUpper() + " = value; }" + Environment.NewLine +
                            T1 +  "} " + Environment.NewLine;

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
    public List<FREDS_MATRIX> UTL_GET_ATTRIBUTES_TABLE_IGNORE_CASE(string SERVER_NAME, string DATABASE_NAME, int OBJECT_ID, Boolean PK_ONLY)
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
                    COLUMN_NAME = R["COLUMN_NAME"].ToString(),
                    CS_PARM_STRING = "cmd.Parameters.AddWithValue(" + Q + "@" + R["COLUMN_NAME"].ToString()  + Q + "," + "o" + "." + R["COLUMN_NAME"].ToString()  + ");",
                    DATABASE_NAME = DATABASE_NAME,
                    SQL_DATA_TYPE = R["TYPE_NAME"].ToString() ,
                    SQL_IDENTITY_FIELD_YN = R["IS_IDENTITY"].ToString(),
                    SQL_IDENT_INCR = Convert.ToInt32(R["IDENT_INCR"]),
                    SQL_IDENT_SEED = Convert.ToInt32(R["IDENT_SEED"]),
                    SQL_IS_NULLABLE = R["IS_NULLABLE"].ToString(),
                    SQL_MAX_LENGTH = Convert.ToInt32(R["MAX_LENGTH"]),
                    SQL_VARIABLE_NAME = "@" + R["COLUMN_NAME"].ToString() ,
                    SQL_PROC_PARM_STRING = "@" + R["COLUMN_NAME"].ToString()  + " " + R["TYPE_NAME"].ToString() ,
                    SQL_PRECISION = Convert.ToInt32(R["PRECISION"]),
                    SQL_SCALE = Convert.ToInt32(R["SCALE"]),
                    SQL_TABLE_SCHEMA = R["TABLE_SCHEMA"].ToString() ,
                    OBJECT_ID = OBJECT_ID,
                    OBJECT_NAME = R["TABLE_NAME"].ToString() ,
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
                    if (o.COLUMN_NAME == r["COLUMN_NAME"].ToString() )
                    {
                        o.PK_YN = "Y";
                        o.UPDATE_YN = "N";
                    }
                }
                switch (o.SQL_DATA_TYPE.ToUpper())
                {
                    case "VARCHAR":
                        o.SQL_PROC_PARM_STRING = "@" + R["COLUMN_NAME"].ToString()  + " " + o.SQL_DATA_TYPE + "(" + o.SQL_MAX_LENGTH + ")";
                        break;
                    case "CHAR":
                        o.SQL_PROC_PARM_STRING = "@" + R["COLUMN_NAME"].ToString()  + " " + o.SQL_DATA_TYPE + "(" + o.SQL_MAX_LENGTH + ")";
                        break;
                    case "DECIMAL":
                        o.SQL_PROC_PARM_STRING = "@" + R["COLUMN_NAME"].ToString()  + " " + o.SQL_DATA_TYPE + "(" + o.SQL_PRECISION + "," + o.SQL_SCALE + ")";
                        break;
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
                        o.CS_PRIVATE_VARIABLE = "private " + R["DataType"].ToString() + " _" + o.COLUMN_NAME.ToString()  + ";" + Environment.NewLine;
                        o.CS_PUBLIC_PROPERTY = "public " + R["DataType"].ToString() + " " + o.COLUMN_NAME.ToString()  + Environment.NewLine +
                            T1 + "{ " + Environment.NewLine +
                                T2 + "get { return _" + o.COLUMN_NAME.ToString()  + "; }" + Environment.NewLine +
                                T2 + "set { _" + o.COLUMN_NAME.ToString()  + " = value; }" + Environment.NewLine +
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
    public List<FREDS_MATRIX> UTL_GET_ATTRIBUTES_VIEW(string SERVER_NAME, string DATABASE_NAME, int OBJECT_ID)
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
                    SQL_PROC_PARM_STRING = "@" + R["COLUMN_NAME"].ToString().ToUpper() + " " + R["TYPE_NAME"].ToString().ToUpper(),
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

                switch (o.SQL_DATA_TYPE.ToUpper())
                {
                    case "VARCHAR":
                        o.SQL_PROC_PARM_STRING = "@" + R["COLUMN_NAME"].ToString().ToUpper() + " " + o.SQL_DATA_TYPE + "(" + o.SQL_MAX_LENGTH + ")";
                        break;
                    case "CHAR":
                        o.SQL_PROC_PARM_STRING = "@" + R["COLUMN_NAME"].ToString().ToUpper() + " " + o.SQL_DATA_TYPE + "(" + o.SQL_MAX_LENGTH + ")";
                        break;
                    case "DECIMAL":
                        o.SQL_PROC_PARM_STRING = "@" + R["COLUMN_NAME"].ToString().ToUpper() + " " + o.SQL_DATA_TYPE + "(" + o.SQL_PRECISION + "," + o.SQL_SCALE + ")";
                        break;
                }

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
                        o.CS_PUBLIC_PROPERTY = "public " + R["DataType"].ToString() + " " + o.COLUMN_NAME.ToString().ToUpper() + Environment.NewLine +
                           T1 + "{ " + Environment.NewLine +
                                T2 + "get { return _" + o.COLUMN_NAME.ToString().ToUpper() + "; }" + Environment.NewLine +
                                T2 + "set { _" + o.COLUMN_NAME.ToString().ToUpper() + " = value; }" + Environment.NewLine +
                           T1 + "} " + Environment.NewLine;


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
    private List<FREDS_MATRIX> UTL_GET_ATTRIBUTES_VIEW(string SERVER_NAME, string DATABASE_NAME, int OBJECT_ID, String COLUMN_NAMES)
    {


        String THIS_COLUMN = null;
        String[] COLUMN_NAMES_ARRAY = COLUMN_NAMES.Split('|');
        int index = 0;
        List<FREDS_MATRIX> LST = new List<FREDS_MATRIX>();
        MSSQL_CS_META_DATA db = new MSSQL_CS_META_DATA();
        DataSet DS_COLUMNS = db.GET_COLUMNS_IN_VIEW_DS(SERVER_NAME, DATABASE_NAME, OBJECT_ID);
       
        string OBJECT_NAME = null;
        try
        {
            for(index=0;index< COLUMN_NAMES_ARRAY.Length-1; index++)
            {
                THIS_COLUMN = COLUMN_NAMES_ARRAY[index];
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
                        SQL_PROC_PARM_STRING = "@" + R["COLUMN_NAME"].ToString().ToUpper() + " " + R["TYPE_NAME"].ToString().ToUpper(),
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





                    if (THIS_COLUMN == o.COLUMN_NAME)
                    {
                        LST.Add(o);
                        break;
                    }
                    o = null;

                    }
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
                }////end of main loop
            db = null;
            return LST;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    #endregion

}

