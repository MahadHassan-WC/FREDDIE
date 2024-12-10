using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
 
    #region ENUMS AREA

    public enum CS_METHOD_OPTION_DEFAULT
    {
        SELECT_ALL,
        SELECT_BY_PK,
        SELECT_TOP_1,
        SELECT_TOP_100,
        SELECT_TOP_1000,
        INSERT_A_ROW,
        INSERT_WHERE_NOT_EXISTS_BY_PK,
        UPDATE_A_ROW_BY_PK,
        DELETE_A_ROW_BY_PK,
        DELETE_ALL_ROWS
    }
    public enum CS_METHOD_UI_OPTION_DEFAULT
    {
        SELECT_ALL_DATAGRIDVIEW,
        SELECT_ALL_COMBOBOX,
        SELECT_ALL_LISTBOX,
        SELECT_ALL_CHECKED_LISTBOX,
        SELECT_ALL_LISTVIEW,
        SELECT_ALL_CHECKED_LISTVIEW
    }

    public enum CS_METHOD_OPTION_CUSTOM
    {
        SELECT,
        SELECT_DISTINCT,
        SELECT_MIN,
        SELECT_MAX,
        SELECT_COUNT,
        SELECT_SUM,
        SELECT_AVG,
        SELECT_BETWEEN,
        INSERT,
        INSERT_WHERE_NOT_EXISTS,
        UPDATE,
        UPDATE_A_ROW_BY_PK,
        DELETE
    }

   

    #endregion
    public class MSSQL_CS_UTL : EVENT_HANDLER_SUPER
    {

        #region PRIVATE VARIABLES AREA
        private const String Q = @"""";
        private const String T1 = "\t";
        private const String T2 = "\t\t";
        private const String T3 = "\t\t\t";
        private const String T4 = "\t\t\t\t";
        private const String T5 = "\t\t\t\t\t";
        private const String NL = "\n";
        private const String AFTER_METHOD = "////END OF METHOD ////////////////////////////////////////////////////////////" + "\n" + "\n";
        private const String LOG_FOLDER = @"C:\Freddie\LOG\";
        private const String ERR_LOG_FILE = "Freddie.ERR";
        private const String LOG_FILE = null;
        private String _SQL_WHERE = " WHERE " + Environment.NewLine;


        private Boolean _PK = false;
        private Boolean _PK_CONTAINS_IDENTITY_FIELD= false;
        private int _COLUMNS_IN_PK = 0;
        private Boolean _SAVE_SOURCE_CODE_TO_FILE;



        private String _SERVER_NAME;
        private String _DATABASE_NAME;
        private Int32 _OBJECT_ID;
        private String _OBJECT_NAME;
        private Boolean _IS_VIEW;

      
        private String _CS_GUI_CLASS_FILE_FOLDER;
      
        private String _CS_READ_CLASS_FILE_FOLDER;
        private String _CS_MODEL_CLASS_FILE_FOLDER;
        private String _CS_IUD_CLASS_FILE_FOLDER;

    private String _SQL_FILE_FOLDER;




    private List<MSSQL_CS_CLASS_MODEL> _LIST_MSSQL_CS_CLASS_MODEL;
        private List<MSSQL_SP_MODEL> _LIST_MSSQL_SP_MODEL;
        private List<MSSQL_CS_METHOD> _LIST_MSSQL_CS_METHOD;

    #endregion
    #region PUBLIC PROPERTIES AREA



    public System.String SQL_FILE_FOLDER
    {
        get { return _SQL_FILE_FOLDER; }
        set { _SQL_FILE_FOLDER = value; }
    }

    public System.String CS_MODEL_CLASS_FILE_FOLDER
    {
        get { return _CS_MODEL_CLASS_FILE_FOLDER; }
        set { _CS_MODEL_CLASS_FILE_FOLDER = value; }
    }

    public System.String CS_READ_CLASS_FILE_FOLDER
    {
        get { return _CS_READ_CLASS_FILE_FOLDER; }
        set { _CS_READ_CLASS_FILE_FOLDER = value; }
    }

    public System.String CS_IUD_CLASS_FILE_FOLDER
    {
        get { return _CS_IUD_CLASS_FILE_FOLDER; }
        set { _CS_IUD_CLASS_FILE_FOLDER = value; }
    }
    public System.String CS_GUI_CLASS_FILE_FOLDER
    {
        get { return _CS_GUI_CLASS_FILE_FOLDER; }
        set { _CS_GUI_CLASS_FILE_FOLDER = value; }
    }


    public List<MSSQL_CS_CLASS_MODEL> LIST_MSSQL_CS_CLASS_MODEL
    {
        get { return _LIST_MSSQL_CS_CLASS_MODEL; }
        set { _LIST_MSSQL_CS_CLASS_MODEL = value; }
    }

    public  List<MSSQL_SP_MODEL> LIST_MSSQL_SP_MODEL
        {
            get { return _LIST_MSSQL_SP_MODEL; }
            set { _LIST_MSSQL_SP_MODEL = value; }
        }

        public System.Boolean SAVE_SOURCE_CODE_TO_FILE
        {
            get { return _SAVE_SOURCE_CODE_TO_FILE; }
            set { _SAVE_SOURCE_CODE_TO_FILE = value; }
        }
        public System.Boolean PK
        {
            get { return _PK; }
        }
        public System.Boolean PK_CONTAINS_IDENTITY_FIELD
        {
            get { return _PK_CONTAINS_IDENTITY_FIELD; }
        }
        public System.String SERVER_NAME
        {
            get { return _SERVER_NAME; }
            set { _SERVER_NAME = value; }
        }
        public System.String DATABASE_NAME
        {
            get { return _DATABASE_NAME; }
            set { _DATABASE_NAME = value; }
        }
        public System.String OBJECT_NAME
        {
            get { return _OBJECT_NAME; }
            set { _OBJECT_NAME = value; }
        }


        public System.Int32 OBJECT_ID
        {
            get { return _OBJECT_ID; }
            set { _OBJECT_ID = value; }
        }

        public System.Boolean IS_VIEW
        {
            get { return _IS_VIEW; }
            set { _IS_VIEW = value; }
        }

        public List<MSSQL_CS_METHOD> LIST_MSSQL_CS_METHOD
        {
            get { return _LIST_MSSQL_CS_METHOD; }
        }
    

        #endregion
        #region MAIN ENTRY


        public String GENERATE_ALL_DEFAULT(string server_name, string database_name, Int32 object_id, string object_name, bool is_view)
        {

            SERVER_NAME = server_name;
            DATABASE_NAME = database_name;
            OBJECT_ID = object_id;
            IS_VIEW = is_view;

            if (IS_VIEW)
            {
                CS_GENERATE_VIEW_MODEL();
            }
            else
            {
                UTL_GET_PK_DATA();
                CS_GENERATE_TABLE_MODEL();
                List<CS_METHOD_OPTION_DEFAULT> OPTIONS = new List<CS_METHOD_OPTION_DEFAULT>();
                OPTIONS.Add(CS_METHOD_OPTION_DEFAULT.SELECT_ALL);
                if (PK)
                {
                    OPTIONS.Add(CS_METHOD_OPTION_DEFAULT.SELECT_BY_PK);

                }
                CS_GENERATE_TABLE_READ_CLASS(OPTIONS);
                GENERATE_WINFORM_GUI_TABLE(server_name, database_name, object_id, object_name);


            }

            return "";
        }
        public String GENERATE_CUSTOM_INSERT(List<MSSQL_CS_ATTRIBUTE> COLUMNS_TO_INSERT)
        {
            String MSG = null;
          //  UTL_CREATE_DEFAULT_SOURCE_CODE_FOLDERS();
            CS_GENERATE_METHOD_INSERT_CUSTOM(COLUMNS_TO_INSERT);
            TSQL_GENERATE_USP_INSERT_CUSTOM(COLUMNS_TO_INSERT);
            return MSG;
        }
        public String GENERATE_CUSTOM_UPDATE(List<MSSQL_CS_ATTRIBUTE> COLUMNS_TO_UPDATE, List<MSSQL_CS_ATTRIBUTE> COLUMNS_IN_WHERE_CLAUSE)
        {
            String MSG = null;
          
            CS_GENERATE_METHOD_UPDATE_CUSTOM(COLUMNS_TO_UPDATE, COLUMNS_IN_WHERE_CLAUSE);
            TSQL_GENERATE_USP_UPADATE_CUSTOM(COLUMNS_TO_UPDATE, COLUMNS_IN_WHERE_CLAUSE);
            return MSG;
        }
        public String GENERATE_CUSTOM_DELETE(  List<MSSQL_CS_ATTRIBUTE> COLUMNS_IN_WHERE_CLAUSE)
        {
            String MSG = null;
          //  UTL_CREATE_DEFAULT_SOURCE_CODE_FOLDERS();
            CS_GENERATE_METHOD_DELETE_CUSTOM(  COLUMNS_IN_WHERE_CLAUSE);
            TSQL_GENERATE_USP_DELETE_CUSTOM( COLUMNS_IN_WHERE_CLAUSE);
            return MSG;
        }

        public String GENERATE_CUSTOM_READ(List<FREDS_MATRIX> COLUMS_IN_TABLE, List<FREDS_MATRIX> COLUMNS_IN_WHERE_CLAUSE)
        {
            String MSG = null;
          //  UTL_CREATE_DEFAULT_SOURCE_CODE_FOLDERS();
            //CS_GENERATE_METHOD_SELECT_CUSTOM(COLUMS_IN_TABLE, COLUMNS_IN_WHERE_CLAUSE);
            //TSQL_GENERATE_USP_SELECT_CUSTOM(COLUMS_IN_TABLE, COLUMNS_IN_WHERE_CLAUSE);
            return MSG;
        }
        public String GENERATE_CUSTOM_SELECT_DISTINCT(List<MSSQL_CS_ATTRIBUTE> DISTINCT_COLUMN, List<MSSQL_CS_ATTRIBUTE> COLUMNS_IN_WHERE_CLAUSE)
        {
            String MSG = null;
          //  UTL_CREATE_DEFAULT_SOURCE_CODE_FOLDERS();
            CS_GENERATE_METHOD_SELECT_DISTINCT_CUSTOM(DISTINCT_COLUMN, COLUMNS_IN_WHERE_CLAUSE);
            TSQL_GENERATE_USP_SELECT_DISTINCT_CUSTOM(DISTINCT_COLUMN, COLUMNS_IN_WHERE_CLAUSE);
            return MSG;
        }


        public String GENERATE_ALL_DEFAULT()
        {

            String MSG = null;

           // UTL_CREATE_DEFAULT_SOURCE_CODE_FOLDERS();

            if (IS_VIEW)
            {
                List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES = GET_MSSQL_CS_ATTRIBUTES_VIEW(SERVER_NAME, DATABASE_NAME.ToUpper(), OBJECT_ID);
                MSG = "LOG FOR VIEW OBJECT: " + SERVER_NAME + "|" + DATABASE_NAME + "|" + OBJECT_NAME + Environment.NewLine;

                List<CS_METHOD_OPTION_DEFAULT> OPTIONS = new List<CS_METHOD_OPTION_DEFAULT>();
                LIST_MSSQL_SP_MODEL = new List<MSSQL_SP_MODEL>();
                LIST_MSSQL_CS_CLASS_MODEL = new List<MSSQL_CS_CLASS_MODEL>();
                OPTIONS.Add(CS_METHOD_OPTION_DEFAULT.SELECT_ALL);

                TSQL_GENERATE_USP_SELECT_ALL(ATTRIBUTES);
                CS_GENERATE_VIEW_MODEL();
                CS_GENERATE_VIEW_READ_CLASS(OPTIONS);
            }
            else
            {
                LIST_MSSQL_CS_CLASS_MODEL = new List<MSSQL_CS_CLASS_MODEL>();
                MSG = "LOG FOR TABLE OBJECT: " + SERVER_NAME + "|" + DATABASE_NAME + "|" + OBJECT_NAME + Environment.NewLine;
                List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES = GET_MSSQL_CS_ATTRIBUTES_TABLE(SERVER_NAME, DATABASE_NAME.ToUpper(), OBJECT_ID);
               
                UTL_GET_PK_DATA();
                CS_GENERATE_TABLE_MODEL();
               
                List<CS_METHOD_OPTION_DEFAULT> OPTIONS = new List<CS_METHOD_OPTION_DEFAULT>();
                LIST_MSSQL_SP_MODEL = new List<MSSQL_SP_MODEL>();
           
                TSQL_GENERATE_USP_DELETE_ALL(ATTRIBUTES);
               
                TSQL_GENERATE_USP_INSERT(ATTRIBUTES);
                
                TSQL_GENERATE_USP_SELECT_ALL(ATTRIBUTES);




                OPTIONS.Add(CS_METHOD_OPTION_DEFAULT.SELECT_ALL);
                OPTIONS.Add(CS_METHOD_OPTION_DEFAULT.DELETE_ALL_ROWS);
                OPTIONS.Add(CS_METHOD_OPTION_DEFAULT.INSERT_A_ROW);
               
                if (PK)
                {
                    OPTIONS.Add(CS_METHOD_OPTION_DEFAULT.SELECT_BY_PK);
                    OPTIONS.Add(CS_METHOD_OPTION_DEFAULT.DELETE_A_ROW_BY_PK);
                    OPTIONS.Add(CS_METHOD_OPTION_DEFAULT.UPDATE_A_ROW_BY_PK);
                    TSQL_GENERATE_USP_SELECT_BY_PK(ATTRIBUTES);
                    TSQL_GENERATE_USP_UPDATE_BY_PK(ATTRIBUTES);
                    TSQL_GENERATE_USP_DELETE_BY_PK(ATTRIBUTES);
                    if(PK_CONTAINS_IDENTITY_FIELD == false)
                    {
                        OPTIONS.Add(CS_METHOD_OPTION_DEFAULT.INSERT_WHERE_NOT_EXISTS_BY_PK);
                        TSQL_GENERATE_USP_INSERT_WHERE_NOT_EXISTS(ATTRIBUTES);
                    }
                    else
                    {
                      
                        MSG += "TABLE OBJECT CONTAINS AN IDENTITY VALUE IN THE PK" + Environment.NewLine;
                    }
                   

                }
                else
                {
                  

                    MSG += "TABLE OBJECT DOES NOT CONTAIN A PK" + Environment.NewLine;
                }
                CS_GENERATE_TABLE_READ_CLASS(OPTIONS);
                CS_GENERATE_TABLE_IUD_CLASS(OPTIONS);
                GENERATE_WINFORM_GUI_TABLE(SERVER_NAME, DATABASE_NAME, OBJECT_ID, OBJECT_NAME);


            }

            return MSG;
        }


        #endregion
        #region UTL area

        
        public Boolean UTL_DOES_STORED_PROCEDURE_EXIST(string SERVER_NAME, string DATABASE_NAME, string OBJECT_NAME)
        {
            try
            {
                MSSQL_CS_META_DATA db = new MSSQL_CS_META_DATA();
                Boolean b = db.STORED_PROCEDURE_EXISTS(SERVER_NAME, DATABASE_NAME, OBJECT_NAME);
                db = null;
                return b;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        private string UTL_GENERATE_STORED_PROCEDURE_NAME(string ObjectName, string DML)
        {
            try
            {
                string MyString = null;
                string MyArrayElement = null;
                string[] MyArray = ObjectName.ToLower().Split('_');

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
                    MyArrayElement = ObjectName.ToLower();
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
                MyString = ObjectName.ToUpper();

                return "usp_" + DML + "_" + MyString;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private string UTL_GENERATE_CUSTOM_STORED_PROCEDURE_NAME(string ObjectName, string DML, string SUFIX)
        {
            try
            {
                string MyString = null;
                string MyArrayElement = null;
                string[] MyArray = ObjectName.ToLower().Split('_');

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
                    MyArrayElement = ObjectName.ToLower();
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
                MyString = ObjectName.ToUpper();

                return "usp_" + DML + "_" + MyString + SUFIX;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private string UTL_GENERATE_CUSTOM_CS_METHOD_NAME(string ObjectName, string DML, string SUFIX)
        {
            try
            {
                string MyString = null;
                string MyArrayElement = null;
                string[] MyArray = ObjectName.ToLower().Split('_');

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
                    MyArrayElement = ObjectName.ToLower();
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
                MyString = ObjectName.ToUpper();

                return    DML + "_" + MyString + SUFIX;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private void UTL_GET_PK_DATA()
        {
             
                _PK = false;
               _PK_CONTAINS_IDENTITY_FIELD = false;
                MSSQL_CS_META_DATA db = new MSSQL_CS_META_DATA();
                if (db.TABLE_HAS_PK(SERVER_NAME, DATABASE_NAME, OBJECT_ID) > 0)
                {
                    _PK = true;
                    if (db.TABLE_HAS_INDENTITY_FIELD_IN_PK(SERVER_NAME, DATABASE_NAME, OBJECT_ID) > 0)
                    {
                    _PK_CONTAINS_IDENTITY_FIELD = true;
                    }
                }
                else
                {
                    _PK = false;
                }
                 
                db = null;
            

        }

        

        //private string UTL_PRINT_ATTRIBUTES(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES)
        //{
        //    List<string> s = null;

        //    foreach(MSSQL_CS_ATTRIBUTE a in ATTRIBUTES)
        //    {
        //        s  = GetPropertiesNameOfClass(a);
        //    }
        //    //foreach (PropertyInfo pi in t.GetProperties())
        //    //{
        //    //    myDict[pi.Name] = pi.GetValue(data, null)?.ToString();

        //    //}


        //    return "";
        //}
        public List<string> GetPropertiesNameOfClass(object pObject)
        {
            List<string> propertyList = new List<string>();
            if (pObject != null)
            {
                foreach (var prop in pObject.GetType().GetProperties())
                {
                    propertyList.Add(prop.Name);
                   
                }
            }
            return propertyList;
        }

        private string UTL_GENERATE_READ_CLASS_NAME(string ObjectName)
        {
            try
            {
                string MyString = null;
                string MyArrayElement = null;
                string[] MyArray = ObjectName.ToLower().Split('_');

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
                    MyArrayElement = ObjectName.ToLower();
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


                return MyString + "_READ";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        private string UTL_GENERATE_MODEL_CLASS_NAME(string ObjectName)
        {
            try
            {
                string MyString = null;
                string MyArrayElement = null;
                string[] MyArray = ObjectName.ToLower().Split('_');

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
                    MyArrayElement = ObjectName.ToLower();
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





        //private void UTL_CREATE_DEFAULT_SOURCE_CODE_FOLDERS()
        //{
        //    if (!Directory.Exists(@"C:\Freddie\"))
        //    {
        //        Directory.CreateDirectory(@"C:\Freddie\");
        //    }


        //    if (!Directory.Exists(@"C:\Freddie\CS"))
        //    {
        //        Directory.CreateDirectory(@"C:\Freddie\CS");
        //    }



        //    if (!Directory.Exists(@"C:\Freddie\CS\MSSQL\"))
        //    {
        //        Directory.CreateDirectory(@"C:\Freddie\CS\MSSQL\");
        //    }


        //    _CS_IUD_CLASS_FILE_FOLDER = @"C:\Freddie\CS\MSSQL\" + SERVER_NAME + @"\" + DATABASE_NAME + @"\IUD\";

        //    if (!Directory.Exists(_CS_IUD_CLASS_FILE_FOLDER))
        //    {
        //        Directory.CreateDirectory(_CS_IUD_CLASS_FILE_FOLDER);
        //    }







        //    _CS_MODEL_CLASS_FILE_FOLDER = @"C:\Freddie\CS\MSSQL\" + SERVER_NAME + @"\" + DATABASE_NAME + @"\MODEL\";
        //    if (!Directory.Exists(_CS_MODEL_CLASS_FILE_FOLDER))
        //    {
        //        Directory.CreateDirectory(_CS_MODEL_CLASS_FILE_FOLDER);
        //    }
        //    _CS_READ_CLASS_FILE_FOLDER = @"C:\Freddie\CS\MSSQL\" + SERVER_NAME + @"\" + DATABASE_NAME + @"\READ\";
        //    if (!Directory.Exists(_CS_READ_CLASS_FILE_FOLDER))
        //    {
        //        Directory.CreateDirectory(_CS_READ_CLASS_FILE_FOLDER);
        //    }
           
         
          
          


         


        //}
        private void UTL_CREATE_STORED_PROCEDURE_ON_SERVER(String SQL)
        {
            MSSQL_CS_META_DATA utl = new MSSQL_CS_META_DATA();
            utl.CREATE_STORED_PROCEDURE_ON_SERVER(this.SERVER_NAME, this.DATABASE_NAME, SQL);
            utl = null;
        }

        private string UTL_GENERATE_IUD_CLASS_NAME(string ObjectName)
        {
            try
            {
                string MyString = null;
                string MyArrayElement = null;
                string[] MyArray = ObjectName.ToLower().Split('_');

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
                    MyArrayElement = ObjectName.ToLower();
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


                return MyString + "_IUD";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }




        #endregion
        #region EVENT AREA
        public event EVENT_HANDLER INFORMATION_EVENT;
        public event EVENT_HANDLER INFORMATION_EVENT_VERBOSE;
        public event EVENT_HANDLER WARNING_EVENT;
        public event EVENT_HANDLER ERROR_EVENT;

        private void LOG_ERROR_EVENT(string MSG)
        {
            if (ERROR_EVENT != null)
            {
                ERROR_EVENT(this, new EVENT_HANDLER_SUPER(MSG));
            }
            if (INFORMATION_EVENT_VERBOSE != null)
            {
                INFORMATION_EVENT_VERBOSE(this, new EVENT_HANDLER_SUPER(MSG));
            }
        }
        private void LOG_WARNING_EVENT(string MSG)
        {
            if (WARNING_EVENT != null)
            {
                WARNING_EVENT(this, new EVENT_HANDLER_SUPER(MSG));
            }
            if (INFORMATION_EVENT_VERBOSE != null)
            {
                INFORMATION_EVENT_VERBOSE(this, new EVENT_HANDLER_SUPER(MSG));
            }
        }
        private void LOG_INFO_EVENT(string MSG)
        {
            if (INFORMATION_EVENT != null)
            {
                INFORMATION_EVENT(this, new EVENT_HANDLER_SUPER(MSG));
            }
            if (INFORMATION_EVENT_VERBOSE != null)
            {
                INFORMATION_EVENT_VERBOSE(this, new EVENT_HANDLER_SUPER(MSG));
            }
        }
        private void LOG_INFO_EVENT_VERBOSE(string MSG)
        {

            if (INFORMATION_EVENT_VERBOSE != null)
            {
                INFORMATION_EVENT_VERBOSE(this, new EVENT_HANDLER_SUPER(MSG));
            }
        }


        #endregion
        #region CONSTRUCTOR AREA
        public MSSQL_CS_UTL()
        {
            _LIST_MSSQL_CS_METHOD = new List<MSSQL_CS_METHOD>();
        }
        #endregion
        #region MY NOTES AREA


        ///// FIX IT LIST

        ///// 2. CREATE A MODEL LIST OBJECT TO CONTAIN SERVER-DATABASE-TABLE-C# CODE
        ///// 3. CREATE A READ LIST OBJECT TO CONTAIN SERVER-DATABASE-TABLE-C# CODE
        ///// 4. CREATE A IUD LIST OBJECT TO CONTAIN SERVER-DATABASE-TABLE-C# CODE


        //// THIS CLASS REPRESENTS ONE DATABASE OBJECT AND ALL ITS SELECTED METHODS (BOTH DEFAULT AND CUSTOM)
        //// IT WILL RETURN A LIST OF ALL THE METHODS SELECTED
        //// IT WILL ALSO RETURN 3 COMPLETE OBJECTS (MODEL, READ, IUD)
        //// 1. THE BASE TABLE OR VIEW OBJECT
        //// 2. THE DATABASE OBJECT CONTAINING ALL THE METHODS SELECTED
        //// 3. LATER VERSION WILL RETURN
        ///     A.CODE SNIPPITS
        ///     B.WINDOWS CODE
        ///     C.MVC
        ///  

        ////CREATE MODEL



        ///CREATE IUD
        ///CREATE READ
        ///
        /// 

        ///
        /// 
        /// 
        ///CREATE THE CUSTOM METHODS
        ///01. SELECT             (C,WHERE)
        ///02. SELECT COUNT(*)    (C,WHERE)
        ///03. SELECT TOP 1       (C,WHERE)
        ///04. SELECT MIN()       (C,WHERE)
        ///05. SELECT MAX()       (C,WHERE)
        ///09. SELECT SUM()       (C,WHERE)
        ///10. SELECT BETWEEN     (C,WHERE)
        ///11. INSERT             (C,NA)
        ///12. INSERT NOT EXTIST  (C,WHERE)
        ///13. UPDATE             (C,WHERE)
        ///14. DELETE             (NA,WHERE)
        ///



        /*
        *1. ALL IUD and READ classes need to execute stored procedures
        *2. add code to create INSERT WHERE NOT EXISTS... stored procedures 
        NOTES: If the table has a PK containing an INDENTITY column, this can't be coded
        3. create a log file for each run of a table or view
        4. notify user if the stored procedure already exists
        5. take a backup of the database prior to changing it
        6. file logs. allow files to be appended to or created each time
        7. file logs. delete each time, or based on a number of days old
        8. MAKE SURE OBJECTS ARE BEING SET TO NULL WHEN THEY ARE DONE BEING USED
        9. make sure all code is being generated properly when UPDDT, INSOPID etc are in the table
        10. add text boxes on test freddie screen one each for all of the events that can fire.
        *11. ADD USE DB TO TSQL PROCS
        12. MAKE SURE COMMENTS ARE CORRECT IN PROCS
        13. MAKE SURE COMMENTS ARE CORRECT IN c#

        */

















        #endregion
        #region GUI AREA


        public string GENERATE_UI_CLASS_TABLE(List<CS_METHOD_UI_OPTION_DEFAULT> OPTIONS, string SERVER_NAME, string DATABASE_NAME, string OBJECT_NAME, int OBJECT_ID)
        {
        //List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES = GET_MSSQL_CS_ATTRIBUTES_TABLE(SERVER_NAME, DATABASE_NAME.ToUpper(), OBJECT_ID);
        ////using System;
        ////using System.Collections.Generic;
        ///////using System.Linq;
        ////using System.Text;
        ////using System.Windows.Forms;
        ////using System.Drawing;
        ////using System.Data;
        ////using System.Data.SqlClient;
        //////using System.Windows.Forms;
        //_CS_UI_CLASS_NAME = GENERATE_UI_CLASS_NAME(OBJECT_NAME.ToUpper());
        //_CS_UI_CLASS_NAME = GENERATE_UI_CLASS_NAME(OBJECT_NAME.ToUpper());
        //_CS_UI_CLASS_FILE_NAME = GENERATE_UI_CLASS_NAME(OBJECT_NAME.ToUpper()) + ".cs";


        //_CS_UI_CLASS_SOURCE_CODE = null;
        //_CS_UI_CLASS_SOURCE_CODE = "///////UI CLASS CREATED ON " + DateTime.Now + Environment.NewLine;

        //_CS_UI_CLASS_SOURCE_CODE += "using System;" + Environment.NewLine;
        //_CS_UI_CLASS_SOURCE_CODE += "using System.Data;" + Environment.NewLine;
        ////_CS_UI_CLASS_SOURCE_CODE += "" + Environment.NewLine + Environment.NewLine;
        ////_CS_UI_CLASS_SOURCE_CODE += "" + Environment.NewLine + Environment.NewLine;
        ////_CS_UI_CLASS_SOURCE_CODE += "" + Environment.NewLine + Environment.NewLine;

        //_CS_UI_CLASS_SOURCE_CODE += "using System.Windows.Forms;" + Environment.NewLine + Environment.NewLine;
        //_CS_UI_CLASS_SOURCE_CODE += "public class " + _CS_UI_CLASS_NAME + Environment.NewLine;
        //_CS_UI_CLASS_SOURCE_CODE += "{" + Environment.NewLine;
        //_CS_UI_CLASS_SOURCE_CODE += T1 + "private  " + UTL_GENERATE_READ_CLASS_NAME(OBJECT_NAME.ToUpper()) + " " + UTL_GENERATE_READ_CLASS_NAME(OBJECT_NAME.ToUpper()) + ";" + Environment.NewLine;


        //_CS_UI_CLASS_SOURCE_CODE += T1 + "public event EVENT_HANDLER INFORMATION_EVENT_VERBOSE_LOCAL;" + Environment.NewLine;
        //_CS_UI_CLASS_SOURCE_CODE += T1 + "public event EVENT_HANDLER INFORMATION_EVENT_LOCAL;" + Environment.NewLine;
        //_CS_UI_CLASS_SOURCE_CODE += T1 + "public event EVENT_HANDLER WARNING_EVENT_LOCAL;" + Environment.NewLine;
        //_CS_UI_CLASS_SOURCE_CODE += T1 + "public event EVENT_HANDLER ERROR_EVENT_LOCAL;" + Environment.NewLine;

        //_CS_UI_CLASS_SOURCE_CODE += T1 + "private void LOG_INFORMATION_EVENT_VERBOSE_LOCAL(string MSG)" + Environment.NewLine;
        //_CS_UI_CLASS_SOURCE_CODE += T1 + "{" + Environment.NewLine;
        //_CS_UI_CLASS_SOURCE_CODE += T2 + "if (INFORMATION_EVENT_VERBOSE_LOCAL != null)" + Environment.NewLine;
        //_CS_UI_CLASS_SOURCE_CODE += T2 + "{" + Environment.NewLine;
        //_CS_UI_CLASS_SOURCE_CODE += T3 + "INFORMATION_EVENT_VERBOSE_LOCAL(this, new EVENT_HANDLER_SUPER(MSG));" + Environment.NewLine;
        //_CS_UI_CLASS_SOURCE_CODE += T2 + "}" + Environment.NewLine;
        //_CS_UI_CLASS_SOURCE_CODE += T1 + "}" + Environment.NewLine;

        //_CS_UI_CLASS_SOURCE_CODE += T1 + "private void LOG_INFORMATION_EVENT_LOCAL(string MSG)" + Environment.NewLine;
        //_CS_UI_CLASS_SOURCE_CODE += T1 + "{" + Environment.NewLine;
        //_CS_UI_CLASS_SOURCE_CODE += T2 + "if (INFORMATION_EVENT_LOCAL != null)" + Environment.NewLine;
        //_CS_UI_CLASS_SOURCE_CODE += T2 + "{" + Environment.NewLine;
        //_CS_UI_CLASS_SOURCE_CODE += T3 + "INFORMATION_EVENT_LOCAL(this, new EVENT_HANDLER_SUPER(MSG));" + Environment.NewLine;
        //_CS_UI_CLASS_SOURCE_CODE += T2 + "}" + Environment.NewLine;
        //_CS_UI_CLASS_SOURCE_CODE += T1 + "}" + Environment.NewLine;

        //_CS_UI_CLASS_SOURCE_CODE += T1 + "private void LOG_WARNING_EVENT_LOCAL(string MSG)" + Environment.NewLine;
        //_CS_UI_CLASS_SOURCE_CODE += T1 + "{" + Environment.NewLine;
        //_CS_UI_CLASS_SOURCE_CODE += T2 + "if (WARNING_EVENT_LOCAL != null)" + Environment.NewLine;
        //_CS_UI_CLASS_SOURCE_CODE += T2 + "{" + Environment.NewLine;
        //_CS_UI_CLASS_SOURCE_CODE += T3 + "WARNING_EVENT_LOCAL(this, new EVENT_HANDLER_SUPER(MSG));" + Environment.NewLine;
        //_CS_UI_CLASS_SOURCE_CODE += T2 + "}" + Environment.NewLine;
        //_CS_UI_CLASS_SOURCE_CODE += T1 + "}" + Environment.NewLine;

        //_CS_UI_CLASS_SOURCE_CODE += T1 + "private void LOG_ERROR_EVENT_LOCAL(string MSG)" + Environment.NewLine;
        //_CS_UI_CLASS_SOURCE_CODE += T1 + "{" + Environment.NewLine;
        //_CS_UI_CLASS_SOURCE_CODE += T2 + "if (ERROR_EVENT_LOCAL != null)" + Environment.NewLine;
        //_CS_UI_CLASS_SOURCE_CODE += T2 + "{" + Environment.NewLine;
        //_CS_UI_CLASS_SOURCE_CODE += T3 + "ERROR_EVENT_LOCAL(this, new EVENT_HANDLER_SUPER(MSG));" + Environment.NewLine;
        //_CS_UI_CLASS_SOURCE_CODE += T2 + "}" + Environment.NewLine;
        //_CS_UI_CLASS_SOURCE_CODE += T1 + "}" + Environment.NewLine;

        //_CS_UI_CLASS_SOURCE_CODE += GENERATE_UI_METHODS(ATTRIBUTES, OPTIONS, SERVER_NAME, DATABASE_NAME, OBJECT_ID, UTL_GENERATE_READ_CLASS_NAME(OBJECT_NAME.ToUpper()));
        //_CS_UI_CLASS_SOURCE_CODE += "}   /////END OF CLASS" + Environment.NewLine + Environment.NewLine;
        //return _CS_UI_CLASS_SOURCE_CODE;
        return "";
        }

        public string GENERATE_UI_METHODS(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES, List<CS_METHOD_UI_OPTION_DEFAULT> OPTIONS, string SERVER_NAME, string DATABASE_NAME, int OBJECT_ID, string DB_CLASS_NAME)
        {
            string s = null;
            foreach (CS_METHOD_UI_OPTION_DEFAULT OPTION in OPTIONS)
            {
                s += GENERATE_UI_METHOD(ATTRIBUTES, OPTION, DB_CLASS_NAME);
            }
            return s;
        }
        public string GENERATE_UI_METHOD(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES, CS_METHOD_UI_OPTION_DEFAULT OPTION, string DB_CLASS_NAME)
        {
            string s = null;
            switch (OPTION)
            {
                case CS_METHOD_UI_OPTION_DEFAULT.SELECT_ALL_DATAGRIDVIEW:
                    s = CS_DATA_GRID_VIEW_SELECT_ALL_BIND(ATTRIBUTES, "GRD_SEL_ALL", DB_CLASS_NAME);
                    break;
                case CS_METHOD_UI_OPTION_DEFAULT.SELECT_ALL_COMBOBOX:
                    s = CS_COMBO_BOX_SELECT_ALL_BIND(ATTRIBUTES, "CBO_SEL_ALL", DB_CLASS_NAME);
                    break;
                case CS_METHOD_UI_OPTION_DEFAULT.SELECT_ALL_LISTBOX:
                    s = CS_LIST_BOX_SELECT_ALL_BIND(ATTRIBUTES, "LST_SEL_ALL", DB_CLASS_NAME);
                    break;
                case CS_METHOD_UI_OPTION_DEFAULT.SELECT_ALL_CHECKED_LISTBOX:
                    s = CS_CHECKED_LIST_BOX_SELECT_ALL_BIND(ATTRIBUTES, "LST_CHECKED_SEL_ALL", DB_CLASS_NAME);
                    break;
                case CS_METHOD_UI_OPTION_DEFAULT.SELECT_ALL_LISTVIEW:
                    s = CS_LIST_VIEW_SELECT_ALL_BIND(ATTRIBUTES, "LV_SEL_ALL_ROWS_BIND", DB_CLASS_NAME);
                    break;
                case CS_METHOD_UI_OPTION_DEFAULT.SELECT_ALL_CHECKED_LISTVIEW:
                    s = CS_CHECKED_LIST_VIEW_SELECT_ALL_BIND(ATTRIBUTES, "LV_CHECKED_SEL_ALL", DB_CLASS_NAME);
                    break;
            }
            return s;
        }
        private string CS_DATA_GRID_VIEW_SELECT_ALL_BIND(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES, string METHOD_NAME, string DB_CLASS_NAME)
        {
            MSSQL_CS_METHOD MSSQL_CS_METHOD = new MSSQL_CS_METHOD();
            try
            {
                MSSQL_CS_METHOD.MSSQL_CS_METHOD_NAME = METHOD_NAME;
                MSSQL_CS_METHOD.CS += T1 + "public void " + METHOD_NAME + "(DataGridView WIN_CONTROL)" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "try" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "WIN_CONTROL.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "WIN_CONTROL.SelectionMode = DataGridViewSelectionMode.FullRowSelect;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "WIN_CONTROL.ReadOnly = true;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "WIN_CONTROL.AllowUserToAddRows = false;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "WIN_CONTROL.AllowUserToDeleteRows = false;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "WIN_CONTROL.RowsDefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "WIN_CONTROL.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + DB_CLASS_NAME + " = new " + DB_CLASS_NAME + "();" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "WIN_CONTROL.DataSource = " + DB_CLASS_NAME + ".SEL_ALL_ROWS().Tables[0];" + Environment.NewLine;
                foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
                {
                    MSSQL_CS_METHOD.CS += T3 + "//WIN_CONTROL.Columns[" + Q + ATTRIBUTE.COLUMN_NAME + Q + "].Visible = false;" + Environment.NewLine;
                }
                MSSQL_CS_METHOD.CS += T3 + DB_CLASS_NAME + "  = null;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "catch (Exception e)" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "LOG_ERROR_EVENT_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|" + Q + @" + e.Message);" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "finally" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + " LOG_INFORMATION_EVENT_VERBOSE_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|finally" + Q + @");" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "} " + Environment.NewLine;
                _LIST_MSSQL_CS_METHOD.Add(MSSQL_CS_METHOD);
                return MSSQL_CS_METHOD.CS;
            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return
                    "EXCEPTION ERROR " + Environment.NewLine +
                    "CLASS: " + this.GetType().Name + Environment.NewLine +
                    "METHOD: " + System.Reflection.MethodBase.GetCurrentMethod().Name + Environment.NewLine +
                    "MESSAGE: " + ex.Message + Environment.NewLine;
            }
            finally
            {
                MSSQL_CS_METHOD = null;
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }

        }
        private string CS_COMBO_BOX_SELECT_ALL_BIND(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES, string METHOD_NAME, string DB_CLASS_NAME)
        {
            MSSQL_CS_METHOD MSSQL_CS_METHOD = new MSSQL_CS_METHOD();
            try
            {
                MSSQL_CS_METHOD.MSSQL_CS_METHOD_NAME = METHOD_NAME;
                MSSQL_CS_METHOD.CS += T1 + "public void " + METHOD_NAME + "(ComboBox WIN_CONTROL)" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "try" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;

                MSSQL_CS_METHOD.CS += T3 + DB_CLASS_NAME + " = new " + DB_CLASS_NAME + "();" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "WIN_CONTROL.DataSource = " + DB_CLASS_NAME + ".SEL_ALL_ROWS().Tables[0];" + Environment.NewLine;
                foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
                {
                    MSSQL_CS_METHOD.CS += T3 + "//WIN_CONTROL.DisplayMember =" + Q + ATTRIBUTE.COLUMN_NAME + Q + ";" + Environment.NewLine;
                }
                MSSQL_CS_METHOD.CS += T2 + Environment.NewLine + Environment.NewLine;
                foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
                {
                    MSSQL_CS_METHOD.CS += T3 + "//WIN_CONTROL.ValueMember =" + Q + ATTRIBUTE.COLUMN_NAME + Q + ";" + Environment.NewLine;
                }
                MSSQL_CS_METHOD.CS += T3 + DB_CLASS_NAME + "  = null;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "catch (Exception e)" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "LOG_ERROR_EVENT_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|" + Q + @" + e.Message);" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "finally" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + " LOG_INFORMATION_EVENT_VERBOSE_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|finally" + Q + @");" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "} " + Environment.NewLine;
                _LIST_MSSQL_CS_METHOD.Add(MSSQL_CS_METHOD);
                return MSSQL_CS_METHOD.CS;
            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return
                    "EXCEPTION ERROR " + Environment.NewLine +
                    "CLASS: " + this.GetType().Name + Environment.NewLine +
                    "METHOD: " + System.Reflection.MethodBase.GetCurrentMethod().Name + Environment.NewLine +
                    "MESSAGE: " + ex.Message + Environment.NewLine;
            }
            finally
            {
                MSSQL_CS_METHOD = null;
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }
        }
        private string CS_LIST_BOX_SELECT_ALL_BIND(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES, string METHOD_NAME, string DB_CLASS_NAME)
        {
            MSSQL_CS_METHOD MSSQL_CS_METHOD = new MSSQL_CS_METHOD();
            try
            {
                MSSQL_CS_METHOD.MSSQL_CS_METHOD_NAME = METHOD_NAME;
                MSSQL_CS_METHOD.CS += T1 + "public void " + METHOD_NAME + "(ListBox WIN_CONTROL)" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "try" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;

                MSSQL_CS_METHOD.CS += T3 + DB_CLASS_NAME + " = new " + DB_CLASS_NAME + "();" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "WIN_CONTROL.DataSource = " + DB_CLASS_NAME + ".SEL_ALL_ROWS().Tables[0];" + Environment.NewLine;
                foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
                {
                    MSSQL_CS_METHOD.CS += T3 + "//WIN_CONTROL.DisplayMember =" + Q + ATTRIBUTE.COLUMN_NAME + Q + ";" + Environment.NewLine;
                }
                MSSQL_CS_METHOD.CS += T2 + Environment.NewLine + Environment.NewLine;
                foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
                {
                    MSSQL_CS_METHOD.CS += T3 + "//WIN_CONTROL.ValueMember =" + Q + ATTRIBUTE.COLUMN_NAME + Q + ";" + Environment.NewLine;
                }
                MSSQL_CS_METHOD.CS += T3 + DB_CLASS_NAME + "  = null;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "catch (Exception e)" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "LOG_ERROR_EVENT_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|" + Q + @" + e.Message);" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "finally" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + " LOG_INFORMATION_EVENT_VERBOSE_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|finally" + Q + @");" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "} " + Environment.NewLine;
                _LIST_MSSQL_CS_METHOD.Add(MSSQL_CS_METHOD);
                return MSSQL_CS_METHOD.CS;
            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return
                    "EXCEPTION ERROR " + Environment.NewLine +
                    "CLASS: " + this.GetType().Name + Environment.NewLine +
                    "METHOD: " + System.Reflection.MethodBase.GetCurrentMethod().Name + Environment.NewLine +
                    "MESSAGE: " + ex.Message + Environment.NewLine;
            }
            finally
            {
                MSSQL_CS_METHOD = null;
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }
        }
        private string CS_CHECKED_LIST_BOX_SELECT_ALL_BIND(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES, string METHOD_NAME, string DB_CLASS_NAME)
        {
            MSSQL_CS_METHOD MSSQL_CS_METHOD = new MSSQL_CS_METHOD();
            try
            {
                MSSQL_CS_METHOD.MSSQL_CS_METHOD_NAME = METHOD_NAME;
                MSSQL_CS_METHOD.CS += T1 + "public void " + METHOD_NAME + "(CheckedListBox WIN_CONTROL)" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "try" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;

                MSSQL_CS_METHOD.CS += T3 + DB_CLASS_NAME + " = new " + DB_CLASS_NAME + "();" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "WIN_CONTROL.DataSource = " + DB_CLASS_NAME + ".SEL_ALL_ROWS().Tables[0];" + Environment.NewLine;
                foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
                {
                    MSSQL_CS_METHOD.CS += T3 + "//WIN_CONTROL.DisplayMember =" + Q + ATTRIBUTE.COLUMN_NAME + Q + ";" + Environment.NewLine;
                }
                MSSQL_CS_METHOD.CS += T2 + Environment.NewLine + Environment.NewLine;
                foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
                {
                    MSSQL_CS_METHOD.CS += T3 + "//WIN_CONTROL.ValueMember =" + Q + ATTRIBUTE.COLUMN_NAME + Q + ";" + Environment.NewLine;
                }
                MSSQL_CS_METHOD.CS += T3 + DB_CLASS_NAME + "  = null;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "catch (Exception e)" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "LOG_ERROR_EVENT_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|" + Q + @" + e.Message);" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "finally" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + " LOG_INFORMATION_EVENT_VERBOSE_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|finally" + Q + @");" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "} " + Environment.NewLine;
                _LIST_MSSQL_CS_METHOD.Add(MSSQL_CS_METHOD);
                return MSSQL_CS_METHOD.CS;
            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return
                    "EXCEPTION ERROR " + Environment.NewLine +
                    "CLASS: " + this.GetType().Name + Environment.NewLine +
                    "METHOD: " + System.Reflection.MethodBase.GetCurrentMethod().Name + Environment.NewLine +
                    "MESSAGE: " + ex.Message + Environment.NewLine;
            }
            finally
            {
                MSSQL_CS_METHOD = null;
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }
        }


        private string CS_LIST_VIEW_SELECT_ALL_BIND(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES, string METHOD_NAME, string DB_CLASS_NAME)
        {
            return "";
        }
        private string CS_CHECKED_LIST_VIEW_SELECT_ALL_BIND(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES, string METHOD_NAME, string DB_CLASS_NAME)
        {
            return "";
        }












        public string GENERATE_GUI_CLASS_VIEW(List<CS_METHOD_OPTION_DEFAULT> OPTIONS, string SERVER_NAME, string DATABASE_NAME, string OBJECT_NAME, int OBJECT_ID)
        {
            return "";
        }




        private string GENERATE_WINFORM_CLASS_NAME(string ObjectName)
        {
            try
            {
                string MyString = null;
                string MyArrayElement = null;
                string[] MyArray = ObjectName.ToLower().Split('_');

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
                    MyArrayElement = ObjectName.ToLower();
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


                return "frm_" + MyString;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        private string GENERATE_UI_CLASS_NAME(string ObjectName)
        {
            try
            {
                string MyString = null;
                string MyArrayElement = null;
                string[] MyArray = ObjectName.ToLower().Split('_');

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
                    MyArrayElement = ObjectName.ToLower();
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


                return MyString + "_UI";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string GENERATE_WINFORM_GUI_TABLE(string SERVER_NAME, string DATABASE_NAME, int OBJECT_ID, string OBJECT_NAME)
        {
            List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES = GET_MSSQL_CS_ATTRIBUTES_TABLE(SERVER_NAME, DATABASE_NAME.ToUpper(), OBJECT_ID);

        return "still being worked on";


        //    _CS_WIN_FORM_CLASS_NAME = GENERATE_WINFORM_CLASS_NAME(OBJECT_NAME);
        //    _CS_WIN_FORM_CLASS_FILE_NAME = GENERATE_WINFORM_CLASS_NAME(OBJECT_NAME) + ".TXT";
         //   _CS_WIN_FORM_CLASS_SOURCE_CODE = "#region FREDDIE UI CODE" + Environment.NewLine
            //  + "private bool PK = false;" + Environment.NewLine
            //  + "private string DATA_VALIDATION_ERROR_MSG = null;" + Environment.NewLine
            //   + GENERATE_GUI_CS_TEXT_BOX_DATA_IS_VALID() + Environment.NewLine
            //   + GENERATE_GUI_CS_LOAD_GRID(UTL_GENERATE_READ_CLASS_NAME(OBJECT_NAME)) + Environment.NewLine
            ////   + GENERATE_GUI_CS_SaveRecord(GENERATE_IUD_CLASS_NAME(OBJECT_NAME), GENERATE_MODEL_CLASS_NAME(OBJECT_NAME), ATTRIBUTES) + Environment.NewLine
            // //  + GENERATE_GUI_CS_DeleteRecord(GENERATE_IUD_CLASS_NAME(OBJECT_NAME), GENERATE_MODEL_CLASS_NAME(OBJECT_NAME), ATTRIBUTES) + Environment.NewLine
            //   + GENERATE_GUI_CS_ClearDataArea() + Environment.NewLine
            //   + GENERATE_GUI_CS_ResetIUDArea() + Environment.NewLine
            //   + GENERATE_GUI_CS_GET_DATA_FROM_GRID(ATTRIBUTES) + Environment.NewLine
            //   + "#endregion";
            //return _CS_WIN_FORM_CLASS_SOURCE_CODE;
        }
        private string GENERATE_GUI_CS_SaveRecord(string IUD_CLASS_NAME, string MODEL_CLASS_NAME, List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES)
        {


            string CURRENT_COLUMN = null;
            string UPDATE_COLUMNS = null;
            string PK_COLUMNS = null;
            string[] CS_DATA_TYPE = null;

            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
            {
                CS_DATA_TYPE = ATTRIBUTE.CS_DATA_TYPE.Split('.');
                if (ATTRIBUTE.RESERVED_YN == "N")
                {
                    switch (CS_DATA_TYPE[1].ToUpper())
                    {
                        case "INT32":
                            CURRENT_COLUMN = "o." + ATTRIBUTE.COLUMN_NAME + " = Convert.ToInt32(txt" + ATTRIBUTE.COLUMN_NAME + ".Text);" + Environment.NewLine;
                            break;
                        case "STRING":
                            CURRENT_COLUMN = "o." + ATTRIBUTE.COLUMN_NAME + " = txt" + ATTRIBUTE.COLUMN_NAME + ".Text;" + Environment.NewLine;
                            break;
                        case "DATETIME":
                            CURRENT_COLUMN = "o." + ATTRIBUTE.COLUMN_NAME + " = Convert.ToDateTime(txt" + ATTRIBUTE.COLUMN_NAME + ".Text);" + Environment.NewLine;
                            break;
                        case "TIMESPAN":
                            CURRENT_COLUMN = "o." + ATTRIBUTE.COLUMN_NAME + " = TimeSpan.Parse(txt" + ATTRIBUTE.COLUMN_NAME + ".Text);" + Environment.NewLine;
                            break;
                        default:
                            CURRENT_COLUMN = "////// unknown  >>>>> o." + ATTRIBUTE.COLUMN_NAME + " = txt" + ATTRIBUTE.COLUMN_NAME + ".Text;" + Environment.NewLine;
                            break;
                    }
                    if (ATTRIBUTE.PK_YN == "N")
                    {
                        UPDATE_COLUMNS += CURRENT_COLUMN;
                    }
                    else
                    {
                        PK_COLUMNS += CURRENT_COLUMN;
                    }
                }

            }

            return @"
                        private void SaveRecord()
                        {
                            ///CREATE MODEL OBJECT AND IUD OBJECT FOR GIVEN TABLE"
                                      + Environment.NewLine + MODEL_CLASS_NAME + " o = new " + MODEL_CLASS_NAME + "();"
                                      + Environment.NewLine + IUD_CLASS_NAME + " db = new " + IUD_CLASS_NAME + "(); " + @"
                            if (PK)
                            {" + Environment.NewLine + PK_COLUMNS + @"
                                db.UPDATE_A_ROW(o);
                            }
                            else
                            {" + Environment.NewLine + UPDATE_COLUMNS + @"
                                db.INSERT_A_ROW(o);
                            }
                            ResetIUDArea();
                            ClearDataArea();
                            LOAD_GRID(100);
                            o = null;
                            db = null;
                        }
                        ";
        }
        private string GENERATE_GUI_CS_DeleteRecord(string IUD_CLASS_NAME, string MODEL_CLASS_NAME, List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES)
        {

            string CURRENT_COLUMN = null;

            string PK_COLUMNS = null;
            string[] CS_DATA_TYPE = null;

            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
            {
                CS_DATA_TYPE = ATTRIBUTE.CS_DATA_TYPE.Split('.');
                if (CS_DATA_TYPE.Length == 2)
                {

                    if (ATTRIBUTE.RESERVED_YN == "N")
                    {
                        switch (CS_DATA_TYPE[1].ToUpper())
                        {
                            case "INT32":
                                CURRENT_COLUMN = "o." + ATTRIBUTE.COLUMN_NAME + " = Convert.ToInt32(txt" + ATTRIBUTE.COLUMN_NAME + ".Text);" + Environment.NewLine;
                                break;
                            case "STRING":
                                CURRENT_COLUMN = "o." + ATTRIBUTE.COLUMN_NAME + " = txt" + ATTRIBUTE.COLUMN_NAME + ".Text;" + Environment.NewLine;
                                break;
                            case "DATETIME":
                                CURRENT_COLUMN = "o." + ATTRIBUTE.COLUMN_NAME + " = Convert.ToDateTime(txt" + ATTRIBUTE.COLUMN_NAME + ".Text);" + Environment.NewLine;
                                break;
                            default:
                                CURRENT_COLUMN = "////// unknown  >>>>> o." + ATTRIBUTE.COLUMN_NAME + " = txt" + ATTRIBUTE.COLUMN_NAME + ".Text;" + Environment.NewLine;
                                break;
                        }
                        if (ATTRIBUTE.PK_YN == "Y")
                        {
                            PK_COLUMNS += CURRENT_COLUMN;
                        }

                    }
                }
                else
                {


                    return "UNKNOWN C# DATA TYPE " + ATTRIBUTE.CS_DATA_TYPE;
                }

            }


            return @"
                            private void DeleteRecord()
                            {
                                if (PK)
                                {
                                ///CREATE MODEL OBJECT AND IUD OBJECT FOR GIVEN TABLE"
                                           + Environment.NewLine + MODEL_CLASS_NAME + " o = new " + MODEL_CLASS_NAME + "();"
                                  + Environment.NewLine + IUD_CLASS_NAME + " db = new " + IUD_CLASS_NAME + "(); " + @"
                               if (PK)
                                {" + PK_COLUMNS + @"
                                    db.DELETE_A_ROW_BY_PK(o);
                                }
                                ResetIUDArea();
                                ClearDataArea();
                                LOAD_GRID(100);
                                o = null;
                                db = null;
                                }
                            }
                            ";



        }



        private string GENERATE_GUI_CS_TEXT_BOX_DATA_IS_VALID()
        {
            return @"
                        private bool TEXT_BOX_DATA_IS_VALID(GroupBox grp)
                        {
                        int ErrCount = 0;
                        foreach (Control ctrl in grp.Controls)
                        {
                            Application.DoEvents();
                            if (ctrl is TextBox)
                            {

                                if(ctrl.Tag == " + Q + "REQUIRED" + Q + @")

                                {
                            if (ctrl.Text.Length == 0)
                            {
                                ctrl.BackColor = Color.LightYellow;
                                ErrCount++;
                            }
                        }

                        }
                        }
                        if(ErrCount ==0)
                        {
                            return true;
                        }
                        else
                        {

                            return false;
                        }

                        }
                        ";
        }

        public string GENERATE_GUI_CS_GET_DATA_FROM_GRID(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES)
        {



            string CS = "private void GET_DATA_FROM_GRID(DataGridView GRD)" + Environment.NewLine
                + T1 + "{" + Environment.NewLine
                + T2 + "PK = false;" + Environment.NewLine
                + T2 + "ResetIUDArea();" + Environment.NewLine
                + T2 + "ClearDataArea();" + Environment.NewLine
                + T2 + "foreach (DataGridViewRow row in GRD.SelectedRows)" + Environment.NewLine
                + T3 + "{" + Environment.NewLine;
            ;


            foreach (MSSQL_CS_ATTRIBUTE o in ATTRIBUTES)
            {
                CS += T4 + "txt" + o.COLUMN_NAME.ToUpper() + ".Text = (GRD.SelectedRows[0].Cells[" + Q + o.COLUMN_NAME.ToUpper() + Q + "].Value.ToString());" + Environment.NewLine;
                if (o.PK_YN == "Y")
                {
                    CS += T4 + "PK = true;" + Environment.NewLine;
                }
            }
            foreach (MSSQL_CS_ATTRIBUTE o in ATTRIBUTES)
            {
                CS += T4 + "txt" + o.COLUMN_NAME.ToUpper() + ".BackColor = SystemColors.Control; " + Environment.NewLine;
            }




            CS += T4 + "btnSave.Enabled=false;" + Environment.NewLine;
            CS += T4 + "btnDelete.Enabled=false;" + Environment.NewLine;
            CS += T3 + "}" + Environment.NewLine + Environment.NewLine;
            CS += T1 + "} //// END OF GET_DATA_FROM_GRID" + Environment.NewLine + Environment.NewLine;

            return CS;
        }
        private string GENERATE_GUI_CS_LOAD_GRID(string READ_CLASS_NAME)
        {
            return @"private void LOAD_GRID(int TOP, DataGridView GRD)
                        {
                        try
                        {
                            this.Cursor = Cursors.WaitCursor;
                            GRD.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                            GRD.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                            GRD.ReadOnly = true;
                            GRD.AllowUserToAddRows = false;
                            GRD.AllowUserToDeleteRows = false;"
                                       + READ_CLASS_NAME + " db = new " + READ_CLASS_NAME + "();" + @"


                            DataSet ds = new DataSet();
                            switch (TOP)
                            {
                                case 1:
                                    GRD.RowsDefaultCellStyle.BackColor = System.Drawing.Color.LightCoral;
                                    GRD.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightSteelBlue;
                                    ds = db.SEL_TOP_1();
                                    break;
                                case 100:
                                    GRD.RowsDefaultCellStyle.BackColor = System.Drawing.Color.LightCyan;
                                    GRD.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightSteelBlue;
                                    ds = db.SEL_TOP_100();
                                    break;
                                case 1000:
                                    GRD.RowsDefaultCellStyle.BackColor = System.Drawing.Color.LightSeaGreen;
                                    GRD.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightSkyBlue;
                                    ds = db.SEL_TOP_1000();
                                    break;
                            }
                            GRD.DataSource = ds.Tables[0];
                            grpDataGrid.Text=grpDataGrid.Tag + dataGridView1.Rows.Count.ToString();

                            if (GRD.Rows.Count == 0)
                            {
                                // SET_DEFAULT_VALUES();
                            }
                            else
                            {
                                ResetIUDArea();
                            }
                            db = null;
                            this.Cursor = Cursors.Default;
                        } ////end of try
                        catch (System.Data.SqlClient.SqlException SqlException)
                        {

                        } ////end of catch
                        finally
                        {

                        }


                        }
                        ";
        }
        public string GENERATE_GUI_CS_ResetIUDArea()
        {
            return @"
                     private void ResetIUDArea()
                    {
                             foreach (Control ctrl in grpIUDArea.Controls)
                            {
                                Application.DoEvents();
                                if (ctrl is Button)
                                {
                                    if (ctrl.Text != " + Q + "CLEAR/RESET" + Q + @")
                                    {
                                        ctrl.Enabled = false;
                                    }
                                }
                        }
                        }
                        ";
        }
        public string GENERATE_GUI_CS_ClearDataArea()
        {
            return @"
                     private void ClearDataArea()
                    {
                        foreach (Control ctrl in grpDataArea.Controls)
                        {
                            Application.DoEvents();
                            if (ctrl is TextBox)
                            {
                                ctrl.Text = " + Q + Q + ";" + @"
                                ctrl.BackColor = SystemColors.Control;
                            }
                            if (ctrl is ComboBox)
                            {
                                  ctrl.Text = " + Q + Q + ";" + @"
                            }
                        }
                    }
                ";
        }
        #endregion
        #region MODEL AREA
   
      
       
        private void CS_GENERATE_TABLE_MODEL( )
        {
            List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES = GET_MSSQL_CS_ATTRIBUTES_TABLE(SERVER_NAME, DATABASE_NAME.ToUpper(), OBJECT_ID);
        MSSQL_CS_CLASS_MODEL m = new MSSQL_CS_CLASS_MODEL
        {
            CS_CLASS_NAME = UTL_GENERATE_MODEL_CLASS_NAME(OBJECT_NAME.ToUpper()),
            CLASS_TYPE = MSSQL_CS_CLASS_MODEL.CS_CLASS_TYPE.MODEL,
            CS_SOURCE_CODE = null,
            CS_FILE_NAME = UTL_GENERATE_MODEL_CLASS_NAME(OBJECT_NAME.ToUpper()) + ".cs",
            CS_FILE_FOLDER = CS_MODEL_CLASS_FILE_FOLDER

        };
            if (!Directory.Exists(m.CS_FILE_FOLDER))
            {
                Directory.CreateDirectory(m.CS_FILE_FOLDER);
            }
            m.CS_SOURCE_CODE = "///////CREATED BY Freddie ON " + DateTime.Now + Environment.NewLine + Environment.NewLine;
            m.CS_SOURCE_CODE += "public class " + m.CS_CLASS_NAME + Environment.NewLine + "{" + Environment.NewLine;
            foreach (MSSQL_CS_ATTRIBUTE o in ATTRIBUTES)
            {
            m.CS_SOURCE_CODE += o.CS_PRIVATE_VARIABLE_AND_TYPE;
            }
            m.CS_SOURCE_CODE += Environment.NewLine + Environment.NewLine;
            foreach (MSSQL_CS_ATTRIBUTE o in ATTRIBUTES)
            {
            m.CS_SOURCE_CODE += o.CS_PUBLIC_PROPERTY;
            }
            m.CS_SOURCE_CODE += "}   /////END OF CLASS" + Environment.NewLine + Environment.NewLine;
           
            if(SAVE_SOURCE_CODE_TO_FILE)
            {
                SAVE_FILE(CS_MODEL_CLASS_FILE_FOLDER, m.CS_FILE_NAME, m.CS_SOURCE_CODE, m.CS_CLASS_NAME);
            }
             LIST_MSSQL_CS_CLASS_MODEL.Add(m);
            m = null;


    }
        private void CS_GENERATE_VIEW_MODEL( )
        {

            List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES = GET_MSSQL_CS_ATTRIBUTES_VIEW(SERVER_NAME, DATABASE_NAME.ToUpper(), OBJECT_ID);

        MSSQL_CS_CLASS_MODEL m = new MSSQL_CS_CLASS_MODEL
        {
            CS_CLASS_NAME = UTL_GENERATE_MODEL_CLASS_NAME(OBJECT_NAME.ToUpper()),
            CLASS_TYPE = MSSQL_CS_CLASS_MODEL.CS_CLASS_TYPE.MODEL,
            CS_SOURCE_CODE = null,
            CS_FILE_NAME = UTL_GENERATE_MODEL_CLASS_NAME(OBJECT_NAME.ToUpper()) + ".cs",
            CS_FILE_FOLDER = CS_MODEL_CLASS_FILE_FOLDER
        };
            if (!Directory.Exists(m.CS_FILE_FOLDER))
            {
                Directory.CreateDirectory(m.CS_FILE_FOLDER);
            }
            m.CS_SOURCE_CODE = "///////CREATED BY Freddie ON " + DateTime.Now + Environment.NewLine + Environment.NewLine;
            m.CS_SOURCE_CODE += "public class " + m.CS_CLASS_NAME + Environment.NewLine + "{" + Environment.NewLine;
            foreach (MSSQL_CS_ATTRIBUTE o in ATTRIBUTES)
            {
                m.CS_SOURCE_CODE += o.CS_PRIVATE_VARIABLE_AND_TYPE;
            }
            m.CS_SOURCE_CODE += Environment.NewLine + Environment.NewLine;
            foreach (MSSQL_CS_ATTRIBUTE o in ATTRIBUTES)
            {
                m.CS_SOURCE_CODE += o.CS_PUBLIC_PROPERTY;
            }
            m.CS_SOURCE_CODE += "}   /////END OF CLASS" + Environment.NewLine + Environment.NewLine;

            if (SAVE_SOURCE_CODE_TO_FILE)
            {
                SAVE_FILE(CS_MODEL_CLASS_FILE_FOLDER, m.CS_FILE_NAME, m.CS_SOURCE_CODE, m.CS_CLASS_NAME);
            }
            LIST_MSSQL_CS_CLASS_MODEL.Add(m);
            m = null;

    }
        #endregion
        #region IUD AREA
   
        public string GENERATE_CS_TABLE_CUSTOM_METHODS(List<MSSQL_CS_METHOD> CUSTOM_METHOD_LIST)
        {

            foreach (MSSQL_CS_METHOD CUSTOM_METHOD in CUSTOM_METHOD_LIST)
            {
                switch (CUSTOM_METHOD.CS_METHOD_OPTION_CUSTOM)
                {
                    case CS_METHOD_OPTION_CUSTOM.INSERT:
                        CUSTOM_METHOD.CS = CS_INSERT_A_ROW(CUSTOM_METHOD);
                        break;
                    case CS_METHOD_OPTION_CUSTOM.INSERT_WHERE_NOT_EXISTS:
                        break;
                    case CS_METHOD_OPTION_CUSTOM.UPDATE:
                        CUSTOM_METHOD.CS = CS_GENERATE_METHOD_UPDATE(CUSTOM_METHOD);
                        break;
                    case CS_METHOD_OPTION_CUSTOM.UPDATE_A_ROW_BY_PK:
                        CUSTOM_METHOD.CS = CS_GENERATE_METHOD_UPDATE_A_ROW_BY_PK(CUSTOM_METHOD);
                        break;
                    case CS_METHOD_OPTION_CUSTOM.DELETE:
                        CUSTOM_METHOD.CS = CS_GENERATE_METHOD_DELETE(CUSTOM_METHOD);
                        break;
                }
            }

            return "";
        }
        public string GENERATE_CS_TABLE_CUSTOM_IUD_METHOD(MSSQL_CS_METHOD CUSTOM_METHOD)
        {

            string CS = null;
            switch (CUSTOM_METHOD.CS_METHOD_OPTION_CUSTOM)
            {
                case CS_METHOD_OPTION_CUSTOM.INSERT:
                    CS = CUSTOM_METHOD.CS = CS_INSERT_A_ROW(CUSTOM_METHOD);
                    break;
                case CS_METHOD_OPTION_CUSTOM.INSERT_WHERE_NOT_EXISTS:
                    CS = CUSTOM_METHOD.CS = CS_INSERT_A_ROW_WHERE_NOT_EXISTS(CUSTOM_METHOD);
                    break;
                case CS_METHOD_OPTION_CUSTOM.UPDATE:
                    CS = CUSTOM_METHOD.CS = CS_GENERATE_METHOD_UPDATE(CUSTOM_METHOD);
                    break;
                case CS_METHOD_OPTION_CUSTOM.UPDATE_A_ROW_BY_PK:
                    CS = CUSTOM_METHOD.CS = CS_GENERATE_METHOD_UPDATE_A_ROW_BY_PK(CUSTOM_METHOD);
                    break;
                case CS_METHOD_OPTION_CUSTOM.DELETE:
                    CS = CUSTOM_METHOD.CS = CS_GENERATE_METHOD_DELETE(CUSTOM_METHOD);
                    break;

            }

            return CS;

        }

        public  void  CS_GENERATE_TABLE_IUD_CLASS(List<CS_METHOD_OPTION_DEFAULT> OPTIONS )
        {
            List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES = GET_MSSQL_CS_ATTRIBUTES_TABLE(SERVER_NAME, DATABASE_NAME.ToUpper(), OBJECT_ID);
            MSSQL_CS_CLASS_MODEL m = new MSSQL_CS_CLASS_MODEL
            {
                CS_CLASS_NAME = UTL_GENERATE_IUD_CLASS_NAME(OBJECT_NAME.ToUpper()),
                CLASS_TYPE = MSSQL_CS_CLASS_MODEL.CS_CLASS_TYPE.IUD,
                CS_SOURCE_CODE = null,
                CS_FILE_NAME = UTL_GENERATE_IUD_CLASS_NAME(OBJECT_NAME.ToUpper()) + ".cs",
                 CS_FILE_FOLDER = CS_IUD_CLASS_FILE_FOLDER
                 
            };
            if (!Directory.Exists(m.CS_FILE_FOLDER))
            {
                Directory.CreateDirectory(m.CS_FILE_FOLDER);
            }

            m.CS_SOURCE_CODE = "///////IUD CLASS CREATED ON " + DateTime.Now + Environment.NewLine + Environment.NewLine;
            m.CS_SOURCE_CODE += "public class " + m.CS_CLASS_NAME + " : dbUTL" + Environment.NewLine;
            m.CS_SOURCE_CODE += "{" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "private const System.String DEFAULT_OBJECT_NAME =" + Q + OBJECT_NAME + Q + ";" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "private const System.String DEFAULT_DATABASE_NAME =" + Q + DATABASE_NAME + Q + ";" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "public event EVENT_HANDLER INFORMATION_EVENT_VERBOSE_LOCAL;" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "public event EVENT_HANDLER INFORMATION_EVENT_LOCAL;" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "public event EVENT_HANDLER WARNING_EVENT_LOCAL;" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "public event EVENT_HANDLER ERROR_EVENT_LOCAL;" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "private void LOG_INFORMATION_EVENT_VERBOSE_LOCAL(string MSG)" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "{" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "if (INFORMATION_EVENT_VERBOSE_LOCAL != null)" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "{" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "INFORMATION_EVENT_VERBOSE_LOCAL(this, new EVENT_HANDLER_SUPER(MSG));" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "}" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "}" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "private void LOG_INFORMATION_EVENT_LOCAL(string MSG)" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "{" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "if (INFORMATION_EVENT_LOCAL != null)" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "{" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "INFORMATION_EVENT_LOCAL(this, new EVENT_HANDLER_SUPER(MSG));" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "}" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "}" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "private void LOG_WARNING_EVENT_LOCAL(string MSG)" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "{" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "if (WARNING_EVENT_LOCAL != null)" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "{" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "WARNING_EVENT_LOCAL(this, new EVENT_HANDLER_SUPER(MSG));" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "}" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "}" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "private void LOG_ERROR_EVENT_LOCAL(string MSG)" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "{" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "if (ERROR_EVENT_LOCAL != null)" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "{" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "ERROR_EVENT_LOCAL(this, new EVENT_HANDLER_SUPER(MSG));" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "}" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "}" + Environment.NewLine;


            foreach (CS_METHOD_OPTION_DEFAULT OPTION in OPTIONS)
            {
                m.CS_SOURCE_CODE += GENERATE_IUD_METHOD(ATTRIBUTES, OPTION);
            }




            m.CS_SOURCE_CODE += "}   /////END OF IUD CLASS " + m.CS_CLASS_NAME + Environment.NewLine + Environment.NewLine;
            if (SAVE_SOURCE_CODE_TO_FILE)
            {
                SAVE_FILE(m.CS_FILE_FOLDER, m.CS_FILE_NAME, m.CS_SOURCE_CODE, m.CS_CLASS_NAME);
            }
            LIST_MSSQL_CS_CLASS_MODEL.Add(m);
            m = null;
    }
      
        private string CS_INSERT_A_ROW(MSSQL_CS_METHOD CUSTOM_METHOD)
        {
            MSSQL_CS_METHOD MSSQL_CS_METHOD = new MSSQL_CS_METHOD();
            try
            {
                string PK_PARAMETERS = T3 + "/////PRIMARY KEY////" + Environment.NewLine;
                string PARAMETERS = T3 + "/////TABLE COLUMNS////" + Environment.NewLine;
                foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in CUSTOM_METHOD.ATTRIBUTE_LIST_1)
                {
                    PARAMETERS += T3 + ATTRIBUTE.CS_PARM_STRING + Environment.NewLine;

                }
                MSSQL_CS_METHOD.MSSQL_CS_METHOD_NAME = CUSTOM_METHOD.MSSQL_CS_METHOD_NAME;
                MSSQL_CS_METHOD.CS = Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "public System.Boolean " + CUSTOM_METHOD.MSSQL_CS_METHOD_NAME + "(" + UTL_GENERATE_MODEL_CLASS_NAME(CUSTOM_METHOD.OBJECT_NAME.ToUpper()) + " o)" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "try" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "string SQL = @" + Q + SQL_INSERT_A_ROW(CUSTOM_METHOD.ATTRIBUTE_LIST_1) + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "cmd = new System.Data.SqlClient.SqlCommand(SQL);" + Environment.NewLine;

                MSSQL_CS_METHOD.CS += T3 + "cmd = new System.Data.SqlClient.SqlCommand(SQL);" + Environment.NewLine;




                MSSQL_CS_METHOD.CS += PARAMETERS + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "return RUN_SQL_COMMAND(DEFAULT_DATABASE_NAME, DEFAULT_OBJECT_NAME);" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}///TRY" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "catch (System.Data.SqlClient.SqlException SqlException)" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "LOG_ERROR_EVENT_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|" + Q + @" + SqlException.Message);" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "return false;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}///CATCH" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "finally" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + " LOG_INFORMATION_EVENT_VERBOSE_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|finally" + Q + @");" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + " cmd = null;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "}" + Environment.NewLine;
                _LIST_MSSQL_CS_METHOD.Add(MSSQL_CS_METHOD);
                return MSSQL_CS_METHOD.CS;

            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return
                    "EXCEPTION ERROR " + Environment.NewLine +
                    "CLASS: " + this.GetType().Name + Environment.NewLine +
                    "METHOD: " + System.Reflection.MethodBase.GetCurrentMethod().Name + Environment.NewLine +
                    "MESSAGE: " + ex.Message + Environment.NewLine;
            }
            finally
            {
                MSSQL_CS_METHOD = null;
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }

        }
        private string CS_INSERT_A_ROW_WHERE_NOT_EXISTS(MSSQL_CS_METHOD CUSTOM_METHOD)
        {
            MSSQL_CS_METHOD MSSQL_CS_METHOD = new MSSQL_CS_METHOD();
            try
            {
                string COLUMNS_WHERE_NOT_EXISTS_PARAMETERS = T3 + "/////TABLE COLUMNS WHERE NOT EXISTS////" + Environment.NewLine;
                string COLUMNS_TO_INSERT_PARAMETERS = T3 + "/////TABLE COLUMNS TO INSERT////" + Environment.NewLine;
                foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in CUSTOM_METHOD.ATTRIBUTE_LIST_1)
                {
                    COLUMNS_TO_INSERT_PARAMETERS += T3 + ATTRIBUTE.CS_PARM_STRING + Environment.NewLine;

                }
                foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in CUSTOM_METHOD.ATTRIBUTE_LIST_2)
                {
                    COLUMNS_WHERE_NOT_EXISTS_PARAMETERS += T3 + ATTRIBUTE.CS_PARM_STRING + Environment.NewLine;

                }
                MSSQL_CS_METHOD.MSSQL_CS_METHOD_NAME = CUSTOM_METHOD.MSSQL_CS_METHOD_NAME;
                MSSQL_CS_METHOD.CS = Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "public System.Boolean " + CUSTOM_METHOD.MSSQL_CS_METHOD_NAME + "(" + UTL_GENERATE_MODEL_CLASS_NAME(CUSTOM_METHOD.OBJECT_NAME.ToUpper()) + " o)" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "try" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "string SQL = @" + Q + SQL_INSERT_A_ROW_WHERE_NOT_EXISTS(CUSTOM_METHOD.ATTRIBUTE_LIST_1, CUSTOM_METHOD.ATTRIBUTE_LIST_2) + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "cmd = new System.Data.SqlClient.SqlCommand(SQL);" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += COLUMNS_TO_INSERT_PARAMETERS + Environment.NewLine;
                MSSQL_CS_METHOD.CS += COLUMNS_WHERE_NOT_EXISTS_PARAMETERS + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "return RUN_SQL_COMMAND(DEFAULT_DATABASE_NAME, DEFAULT_OBJECT_NAME);" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}///TRY" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "catch (System.Data.SqlClient.SqlException SqlException)" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "LOG_ERROR_EVENT_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|" + Q + @" + SqlException.Message);" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "return false;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}///CATCH" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "finally" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + " LOG_INFORMATION_EVENT_VERBOSE_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|finally" + Q + @");" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + " cmd = null;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "}" + Environment.NewLine;
                _LIST_MSSQL_CS_METHOD.Add(MSSQL_CS_METHOD);
                return MSSQL_CS_METHOD.CS;

            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return
                    "EXCEPTION ERROR " + Environment.NewLine +
                    "CLASS: " + this.GetType().Name + Environment.NewLine +
                    "METHOD: " + System.Reflection.MethodBase.GetCurrentMethod().Name + Environment.NewLine +
                    "MESSAGE: " + ex.Message + Environment.NewLine;
            }
            finally
            {
                MSSQL_CS_METHOD = null;
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }

        }
        private string CS_GENERATE_METHOD_INSERT_A_ROW(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES)
        {
            MSSQL_CS_METHOD MSSQL_CS_METHOD = new MSSQL_CS_METHOD();
            try
            {
                string PK_PARAMETERS = T3 + "/////PRIMARY KEY////" + Environment.NewLine;
                string PARAMETERS = T3 + "/////TABLE COLUMNS////" + Environment.NewLine;


                foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
                {
                    if (ATTRIBUTE.INSERT_YN == "Y")
                    { PARAMETERS += T3 + ATTRIBUTE.CS_PARM_STRING + Environment.NewLine; }
                }


                MSSQL_CS_METHOD.MSSQL_CS_METHOD_NAME = "INSERT_A_ROW";
                MSSQL_CS_METHOD.CS = Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "public System.Boolean INSERT_A_ROW" + "(" + UTL_GENERATE_MODEL_CLASS_NAME(ATTRIBUTES[0].OBJECT_NAME.ToUpper()) + " o)" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "try" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "cmd = new System.Data.SqlClient.SqlCommand(" + Q + UTL_GENERATE_STORED_PROCEDURE_NAME(ATTRIBUTES[0].OBJECT_NAME, "INS") + Q + ");" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += PARAMETERS + Environment.NewLine;



                MSSQL_CS_METHOD.CS += T3 + "cmd.CommandType = System.Data.CommandType.StoredProcedure;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "LOG_INFORMATION_EVENT_VERBOSE_LOCAL(GET_SQLSERVER_COMMAND_VALUES(cmd));" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "return RUN_SQL_COMMAND(DEFAULT_DATABASE_NAME, DEFAULT_OBJECT_NAME);" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "catch (System.Data.SqlClient.SqlException SqlException)" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "LOG_ERROR_EVENT_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|" + Q + @" + SqlException.Message);" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "return false;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "finally" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + " LOG_INFORMATION_EVENT_VERBOSE_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|finally" + Q + @");" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + " cmd = null;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "}" + Environment.NewLine;
                _LIST_MSSQL_CS_METHOD.Add(MSSQL_CS_METHOD);
                return MSSQL_CS_METHOD.CS;
            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return
                    "EXCEPTION ERROR " + Environment.NewLine +
                    "CLASS: " + this.GetType().Name + Environment.NewLine +
                    "METHOD: " + System.Reflection.MethodBase.GetCurrentMethod().Name + Environment.NewLine +
                    "MESSAGE: " + ex.Message + Environment.NewLine;
            }
            finally
            {
                MSSQL_CS_METHOD = null;
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }

        }
        private string CS_GENERATE_METHOD_INSERT_A_ROW_WHERE_NOT_EXISTS_BY_PK(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES)
        {
            MSSQL_CS_METHOD MSSQL_CS_METHOD = new MSSQL_CS_METHOD();
            try
            {
                string PK_PARAMETERS = T3 + "/////PRIMARY KEY////" + Environment.NewLine;
                string PARAMETERS = T3 + "/////TABLE COLUMNS////" + Environment.NewLine;
                foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
                {
                    if (ATTRIBUTE.INSERT_YN == "Y")
                    {
                        if (ATTRIBUTE.PK_YN == "N")
                        {
                            PARAMETERS += T3 + ATTRIBUTE.CS_PARM_STRING + Environment.NewLine;
                        }
                    }
                }
                foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
                {
                    if (ATTRIBUTE.PK_YN == "Y")
                    { PK_PARAMETERS += T3 + ATTRIBUTE.CS_PARM_STRING + Environment.NewLine; }
                }
                MSSQL_CS_METHOD.MSSQL_CS_METHOD_NAME = "INSERT_A_ROW_WHERE_NOT_EXISTS";
                MSSQL_CS_METHOD.CS = Environment.NewLine;
            MSSQL_CS_METHOD.CS += T1 + "public System.Boolean INSERT_A_ROW_WHERE_NOT_EXISTS" + "(" + UTL_GENERATE_MODEL_CLASS_NAME(ATTRIBUTES[0].OBJECT_NAME.ToUpper()) + " o)" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T1 + "{" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T2 + "try" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T3 + "cmd = new System.Data.SqlClient.SqlCommand(" + Q + UTL_GENERATE_STORED_PROCEDURE_NAME(ATTRIBUTES[0].OBJECT_NAME, "INS_WHERE_NOT_EXISTS") + Q + ");" + Environment.NewLine;
            // MSSQL_CS_METHOD.CS += T2 + "System.String SQL_INSERT_A_ROW_WHERE_NOT_EXISTS = @" + Q + this.SQL_INSERT_A_ROW_WHERE_NOT_EXISTS_BY_PK(ATTRIBUTES) + Environment.NewLine;
            // MSSQL_CS_METHOD.CS += T3 + "cmd = new System.Data.SqlClient.SqlCommand(SQL_INSERT_A_ROW_WHERE_NOT_EXISTS);" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += PARAMETERS + Environment.NewLine;
            MSSQL_CS_METHOD.CS += PK_PARAMETERS + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T3 + "cmd.CommandType = System.Data.CommandType.StoredProcedure;" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T3 + "return RUN_SQL_COMMAND(DEFAULT_DATABASE_NAME, DEFAULT_OBJECT_NAME);" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T2 + "}///TRY" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T2 + "catch (System.Data.SqlClient.SqlException SqlException)" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "LOG_ERROR_EVENT_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|" + Q + @" + SqlException.Message);" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "return false;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}///CATCH" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "finally" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + " LOG_INFORMATION_EVENT_VERBOSE_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|finally" + Q + @");" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + " cmd = null;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "}" + Environment.NewLine;
                _LIST_MSSQL_CS_METHOD.Add(MSSQL_CS_METHOD);
                return MSSQL_CS_METHOD.CS;
            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return
                    "EXCEPTION ERROR " + Environment.NewLine +
                    "CLASS: " + this.GetType().Name + Environment.NewLine +
                    "METHOD: " + System.Reflection.MethodBase.GetCurrentMethod().Name + Environment.NewLine +
                    "MESSAGE: " + ex.Message + Environment.NewLine;
            }
            finally
            {
                MSSQL_CS_METHOD = null;
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }

        }
        private string CS_GENERATE_METHOD_UPDATE_A_ROW_BY_PK(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES)
        {
            MSSQL_CS_METHOD MSSQL_CS_METHOD = new MSSQL_CS_METHOD();
            try
            {
                string PK_PARAMETERS = T3 + "/////PRIMARY KEY////" + Environment.NewLine;
                string PARAMETERS = T3 + "/////TABLE COLUMNS////" + Environment.NewLine;
                foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
                {
                    if (ATTRIBUTE.PK_YN == "Y")
                    {
                        PK_PARAMETERS += T3 + ATTRIBUTE.CS_PARM_STRING + Environment.NewLine;
                    }
                    else
                    {
                        PARAMETERS += T3 + ATTRIBUTE.CS_PARM_STRING + Environment.NewLine;
                    }
                }
                MSSQL_CS_METHOD.MSSQL_CS_METHOD_NAME = "UPDATE_A_ROW_BY_PK";
                MSSQL_CS_METHOD.CS = Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "public System.Boolean UPDATE_A_ROW_BY_PK(" + UTL_GENERATE_MODEL_CLASS_NAME(ATTRIBUTES[0].OBJECT_NAME.ToUpper()) + " o)" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "try" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "cmd = new System.Data.SqlClient.SqlCommand(" + Q + UTL_GENERATE_STORED_PROCEDURE_NAME(ATTRIBUTES[0].OBJECT_NAME, "UPD_BY_PK") + Q + ");" + Environment.NewLine;
                //  MSSQL_CS_METHOD.CS += T3 + "cmd = new System.Data.SqlClient.SqlCommand(SQL_UPDATE_A_ROW_BY_PK);" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += PARAMETERS + Environment.NewLine;
                MSSQL_CS_METHOD.CS += PK_PARAMETERS + Environment.NewLine;
                ///  
                ///  
                MSSQL_CS_METHOD.CS += T3 + "cmd.CommandType = System.Data.CommandType.StoredProcedure;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "LOG_INFORMATION_EVENT_VERBOSE_LOCAL(GET_SQLSERVER_COMMAND_VALUES(cmd));" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "return RUN_SQL_COMMAND(DEFAULT_DATABASE_NAME, DEFAULT_OBJECT_NAME);" + Environment.NewLine;
                // MSSQL_CS_METHOD.CS += T3 + " cmd = null;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "catch (System.Data.SqlClient.SqlException SqlException)" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "LOG_ERROR_EVENT_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|" + Q + @" + SqlException.Message);" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "return false;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "finally" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + " LOG_INFORMATION_EVENT_VERBOSE_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|finally" + Q + @");" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + " cmd = null;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "}" + Environment.NewLine;
                _LIST_MSSQL_CS_METHOD.Add(MSSQL_CS_METHOD);
                return MSSQL_CS_METHOD.CS;

            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return
                    "EXCEPTION ERROR " + Environment.NewLine +
                    "CLASS: " + this.GetType().Name + Environment.NewLine +
                    "METHOD: " + System.Reflection.MethodBase.GetCurrentMethod().Name + Environment.NewLine +
                    "MESSAGE: " + ex.Message + Environment.NewLine;
            }
            finally
            {
                MSSQL_CS_METHOD = null;
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }
        }
        private string CS_GENERATE_METHOD_UPDATE_A_ROW_BY_PK(MSSQL_CS_METHOD CUSTOM_METHOD)
        {
            MSSQL_CS_METHOD MSSQL_CS_METHOD = new MSSQL_CS_METHOD();
            try
            {


                string PK_PARAMETERS = T3 + "/////PRIMARY KEY COLUMNS////" + Environment.NewLine;
                string PARAMETERS = T3 + "/////TABLE COLUMNS TO UPDATE////" + Environment.NewLine;

                foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in CUSTOM_METHOD.ATTRIBUTE_LIST_2)
                {
                    if (ATTRIBUTE.PK_YN == "Y")
                    {
                        PK_PARAMETERS += T3 + ATTRIBUTE.CS_PARM_STRING + Environment.NewLine;
                    }

                }
                foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in CUSTOM_METHOD.ATTRIBUTE_LIST_1)
                {

                    PARAMETERS += T3 + ATTRIBUTE.CS_PARM_STRING + Environment.NewLine;

                }
                MSSQL_CS_METHOD.MSSQL_CS_METHOD_NAME = CUSTOM_METHOD.MSSQL_CS_METHOD_NAME;
                MSSQL_CS_METHOD.CS = Environment.NewLine;


                MSSQL_CS_METHOD.CS += T1 + "public System.Boolean " + CUSTOM_METHOD.MSSQL_CS_METHOD_NAME + "(" + CUSTOM_METHOD.OBJECT_NAME + " o)" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "try" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;

                MSSQL_CS_METHOD.CS += T3 + "string SQL = @" + Q + SQL_UPDATE_A_ROW_BY_PK(CUSTOM_METHOD.ATTRIBUTE_LIST_1) + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "cmd = new System.Data.SqlClient.SqlCommand(SQL);" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += PARAMETERS + Environment.NewLine;
                MSSQL_CS_METHOD.CS += PK_PARAMETERS + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "LOG_INFORMATION_EVENT_VERBOSE_LOCAL(GET_SQLSERVER_COMMAND_VALUES(cmd));" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "return RUN_SQL_COMMAND(DEFAULT_DATABASE_NAME, DEFAULT_OBJECT_NAME, SQL);" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}///TRY" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "catch (System.Data.SqlClient.SqlException SqlException)" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "LOG_ERROR_EVENT_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|" + Q + @" + SqlException.Message);" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "return false;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}///CATCH" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "finally" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + " LOG_INFORMATION_EVENT_VERBOSE_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|finally" + Q + @");" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + " cmd = null;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "}" + Environment.NewLine;
                _LIST_MSSQL_CS_METHOD.Add(MSSQL_CS_METHOD);
                return MSSQL_CS_METHOD.CS;

            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return
                    "EXCEPTION ERROR " + Environment.NewLine +
                    "CLASS: " + this.GetType().Name + Environment.NewLine +
                    "METHOD: " + System.Reflection.MethodBase.GetCurrentMethod().Name + Environment.NewLine +
                    "MESSAGE: " + ex.Message + Environment.NewLine;
            }
            finally
            {
                MSSQL_CS_METHOD = null;
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }
        }
        private string CS_GENERATE_METHOD_UPDATE(MSSQL_CS_METHOD CUSTOM_METHOD)
        {
           MSSQL_CS_METHOD MSSQL_CS_METHOD = new MSSQL_CS_METHOD();
            try
            {



                string PARAMETERS = T3 + "/////TABLE COLUMNS TO UPDATE////" + Environment.NewLine;
                string PARAMETERS2 = T3 + "/////TABLE COLUMNS TO UPDATE BY ////" + Environment.NewLine;

                foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in CUSTOM_METHOD.ATTRIBUTE_LIST_2)
                {

                    PARAMETERS2 += T3 + ATTRIBUTE.CS_PARM_STRING + Environment.NewLine;

                }
                foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in CUSTOM_METHOD.ATTRIBUTE_LIST_1)
                {

                    PARAMETERS += T3 + ATTRIBUTE.CS_PARM_STRING + Environment.NewLine;

                }
                MSSQL_CS_METHOD.MSSQL_CS_METHOD_NAME = CUSTOM_METHOD.MSSQL_CS_METHOD_NAME;
                MSSQL_CS_METHOD.CS = Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "public System.Boolean " + CUSTOM_METHOD.MSSQL_CS_METHOD_NAME + "(" + UTL_GENERATE_MODEL_CLASS_NAME(CUSTOM_METHOD.OBJECT_NAME.ToUpper()) + " o)" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "try" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "string SQL = @" + Q + SQL_UPDATE(CUSTOM_METHOD.ATTRIBUTE_LIST_1, CUSTOM_METHOD.ATTRIBUTE_LIST_2) + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "cmd = new System.Data.SqlClient.SqlCommand(SQL);" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += PARAMETERS + Environment.NewLine;
                MSSQL_CS_METHOD.CS += PARAMETERS2 + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "return RUN_SQL_COMMAND(DEFAULT_DATABASE_NAME, DEFAULT_OBJECT_NAME );" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "catch (System.Data.SqlClient.SqlException SqlException)" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "LOG_ERROR_EVENT_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|" + Q + @" + SqlException.Message);" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "return false;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "finally" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + " LOG_INFORMATION_EVENT_VERBOSE_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|finally" + Q + @");" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + " cmd = null;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "}" + Environment.NewLine;
                _LIST_MSSQL_CS_METHOD.Add(MSSQL_CS_METHOD);
                return MSSQL_CS_METHOD.CS;

            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return
                    "EXCEPTION ERROR " + Environment.NewLine +
                    "CLASS: " + this.GetType().Name + Environment.NewLine +
                    "METHOD: " + System.Reflection.MethodBase.GetCurrentMethod().Name + Environment.NewLine +
                    "MESSAGE: " + ex.Message + Environment.NewLine;
            }
            finally
            {
                MSSQL_CS_METHOD = null;
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }
        }
        private string CS_GENERATE_METHOD_DELETE_A_ROW_BY_PK(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES)
        {
            MSSQL_CS_METHOD MSSQL_CS_METHOD = new MSSQL_CS_METHOD();
            try
            {
                string PK_PARAMETERS = T3 + "/////PRIMARY KEY////" + Environment.NewLine;
                string PARAMETERS = T3 + "/////TABLE COLUMNS////" + Environment.NewLine;
                foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
                {
                    if (ATTRIBUTE.PK_YN == "Y")
                    {
                        PK_PARAMETERS += T3 + ATTRIBUTE.CS_PARM_STRING + Environment.NewLine;
                    }
                }
                MSSQL_CS_METHOD.MSSQL_CS_METHOD_NAME = "DELETE_A_ROW_BY_PK";
                MSSQL_CS_METHOD.CS = Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "public System.Boolean DELETE_A_ROW_BY_PK(" + UTL_GENERATE_MODEL_CLASS_NAME(ATTRIBUTES[0].OBJECT_NAME.ToUpper()) + " o)" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "try" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                //  MSSQL_CS_METHOD.CS += T3 + "System.String SQL_DELETE_A_ROW_BY_PK =@" + Q + this.SQL_DELETE_A_ROW_BY_PK(ATTRIBUTES) + Environment.NewLine;


                MSSQL_CS_METHOD.CS += T3 + "cmd = new System.Data.SqlClient.SqlCommand(" + Q + UTL_GENERATE_STORED_PROCEDURE_NAME(ATTRIBUTES[0].OBJECT_NAME, "DEL_BY_PK") + Q + ");" + Environment.NewLine;


                ///   MSSQL_CS_METHOD.CS += T3 + "cmd = new System.Data.SqlClient.SqlCommand(SQL_DELETE_A_ROW_BY_PK);" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += PK_PARAMETERS + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "cmd.CommandType = System.Data.CommandType.StoredProcedure;" + Environment.NewLine;

                MSSQL_CS_METHOD.CS += T3 + "LOG_INFORMATION_EVENT_VERBOSE_LOCAL(GET_SQLSERVER_COMMAND_VALUES(cmd));" + Environment.NewLine;

                ////  
                MSSQL_CS_METHOD.CS += T3 + "return RUN_SQL_COMMAND(DEFAULT_DATABASE_NAME, DEFAULT_OBJECT_NAME );" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "catch (System.Data.SqlClient.SqlException SqlException)" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "LOG_ERROR_EVENT_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|" + Q + @" + SqlException.Message);" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "return false;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "finally" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + " LOG_INFORMATION_EVENT_VERBOSE_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|finally" + Q + @");" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + " cmd = null;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "}" + Environment.NewLine;
                _LIST_MSSQL_CS_METHOD.Add(MSSQL_CS_METHOD);
                return MSSQL_CS_METHOD.CS;
            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return
                    "EXCEPTION ERROR " + Environment.NewLine +
                    "CLASS: " + this.GetType().Name + Environment.NewLine +
                    "METHOD: " + System.Reflection.MethodBase.GetCurrentMethod().Name + Environment.NewLine +
                    "MESSAGE: " + ex.Message + Environment.NewLine;
            }
            finally
            {
                MSSQL_CS_METHOD = null;
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }
        }
        private string CS_GENERATE_METHOD_DELETE_ALL_ROWS(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES)
        {
            MSSQL_CS_METHOD MSSQL_CS_METHOD = new MSSQL_CS_METHOD();
            try
            {
                MSSQL_CS_METHOD.MSSQL_CS_METHOD_NAME = "DELETE_ALL_ROWS";
                MSSQL_CS_METHOD.CS = Environment.NewLine;

                MSSQL_CS_METHOD.CS += T1 + "public System.Boolean DELETE_ALL_ROWS()" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "try" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;






                MSSQL_CS_METHOD.CS += T3 + "cmd = new System.Data.SqlClient.SqlCommand(" + Q + UTL_GENERATE_STORED_PROCEDURE_NAME(ATTRIBUTES[0].OBJECT_NAME, "DEL_ALL") + Q + ");" + Environment.NewLine;
                //  MSSQL_CS_METHOD.CS += T3 + "cmd = new System.Data.SqlClient.SqlCommand(SQL_UPDATE_A_ROW_BY_PK);" + Environment.NewLine;

                MSSQL_CS_METHOD.CS += T3 + "cmd.CommandType = System.Data.CommandType.StoredProcedure;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "LOG_INFORMATION_EVENT_VERBOSE_LOCAL(GET_SQLSERVER_COMMAND_VALUES(cmd));" + Environment.NewLine;















                MSSQL_CS_METHOD.CS += T3 + "return RUN_SQL_COMMAND(DEFAULT_DATABASE_NAME, DEFAULT_OBJECT_NAME);" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "catch (System.Data.SqlClient.SqlException SqlException)" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "LOG_ERROR_EVENT_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|" + Q + @" + SqlException.Message);" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "return false;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "finally" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "LOG_INFORMATION_EVENT_VERBOSE_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|finally" + Q + @");" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "cmd = null;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "}" + Environment.NewLine;
                _LIST_MSSQL_CS_METHOD.Add(MSSQL_CS_METHOD);
                return MSSQL_CS_METHOD.CS;

            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return
                    "EXCEPTION ERROR " + Environment.NewLine +
                    "CLASS: " + this.GetType().Name + Environment.NewLine +
                    "METHOD: " + System.Reflection.MethodBase.GetCurrentMethod().Name + Environment.NewLine +
                    "MESSAGE: " + ex.Message + Environment.NewLine;
            }
            finally
            {
                MSSQL_CS_METHOD = null;
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }
        }
        private string CS_GENERATE_METHOD_DELETE(MSSQL_CS_METHOD CUSTOM_METHOD)
        {
            MSSQL_CS_METHOD MSSQL_CS_METHOD = new MSSQL_CS_METHOD();
            try
            {
                string PARAMETERS = T3 + "/////TABLE COLUMNS TO DELETE BY (SQL WHERE CLAUSE)////" + Environment.NewLine;
                foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in CUSTOM_METHOD.ATTRIBUTE_LIST_1)
                {
                    PARAMETERS += T3 + ATTRIBUTE.CS_PARM_STRING + Environment.NewLine;
                }
                MSSQL_CS_METHOD.MSSQL_CS_METHOD_NAME = CUSTOM_METHOD.MSSQL_CS_METHOD_NAME;
                MSSQL_CS_METHOD.CS = Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "public System.Boolean " + CUSTOM_METHOD.MSSQL_CS_METHOD_NAME + "(" + UTL_GENERATE_MODEL_CLASS_NAME(CUSTOM_METHOD.OBJECT_NAME) + " o)" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "try" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;

                MSSQL_CS_METHOD.CS += T3 + "string SQL = @" + Q + SQL_DELETE(CUSTOM_METHOD.ATTRIBUTE_LIST_1) + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "cmd = new System.Data.SqlClient.SqlCommand(SQL);" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += PARAMETERS + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "return RUN_SQL_COMMAND(DEFAULT_DATABASE_NAME, DEFAULT_OBJECT_NAME);" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}///TRY" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "catch (System.Data.SqlClient.SqlException SqlException)" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "LOG_ERROR_EVENT_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|" + Q + @" + SqlException.Message);" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "return false;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}///CATCH" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "finally" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "LOG_INFORMATION_EVENT_VERBOSE_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|finally" + Q + @");" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "cmd = null;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "}" + Environment.NewLine;
                _LIST_MSSQL_CS_METHOD.Add(MSSQL_CS_METHOD);
                return MSSQL_CS_METHOD.CS;
            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return
                    "EXCEPTION ERROR " + Environment.NewLine +
                    "CLASS: " + this.GetType().Name + Environment.NewLine +
                    "METHOD: " + System.Reflection.MethodBase.GetCurrentMethod().Name + Environment.NewLine +
                    "MESSAGE: " + ex.Message + Environment.NewLine;
            }
            finally
            {
                MSSQL_CS_METHOD = null;
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }
        }



        #region CREATE SQL STRINGS - IUD




        private string SQL_INSERT_A_ROW_WHERE_NOT_EXISTS_BY_PK(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES)
        {
            try
            {
                int COLUMNS_TO_INSERT = 0;
                int COLUMNS_IN_NOT_EXISTS = 0;
                int COLUMN_COUNT = 0;
                for (int i = 0; i < ATTRIBUTES.Count; i++)
                {
                    if (ATTRIBUTES[i].INSERT_YN == "Y")
                    {
                        COLUMNS_TO_INSERT++;
                    }
                }
                for (int i = 0; i < ATTRIBUTES.Count; i++)
                {
                    if (ATTRIBUTES[i].PK_YN == "Y")
                    {
                        COLUMNS_IN_NOT_EXISTS++;
                    }

                }
                string SQL = "IF NOT EXISTS (SELECT * FROM " + ATTRIBUTES[0].MSSQL_TABLE_SCHEMA + "." + ATTRIBUTES[0].OBJECT_NAME + " WHERE " + Environment.NewLine;
                for (int i = 0; i < ATTRIBUTES.Count; i++)
                {
                    COLUMN_COUNT++;
                    if (COLUMN_COUNT < COLUMNS_IN_NOT_EXISTS)
                    {
                        SQL += T3 + ATTRIBUTES[i].COLUMN_NAME + " = " + ATTRIBUTES[i].MSSQL_PARM_STRING + " AND " + Environment.NewLine;
                    }
                    else
                    {
                        SQL += T3 + ATTRIBUTES[i].COLUMN_NAME + " = " + ATTRIBUTES[i].MSSQL_PARM_STRING + ")" + Environment.NewLine + T3 + "BEGIN " + Environment.NewLine;

                    }
                    if (COLUMN_COUNT == COLUMNS_IN_NOT_EXISTS)
                    {
                        break;
                    }
                }





                SQL += T4 + "INSERT INTO " + ATTRIBUTES[0].MSSQL_TABLE_SCHEMA + "." + ATTRIBUTES[0].OBJECT_NAME + "(" + Environment.NewLine;
                COLUMN_COUNT = 0;
                for (int i = 0; i < ATTRIBUTES.Count; i++)
                {
                    if (ATTRIBUTES[i].INSERT_YN == "Y")
                    {
                        COLUMN_COUNT++;
                        if (COLUMN_COUNT < COLUMNS_TO_INSERT)
                        {
                            SQL += T4 + ATTRIBUTES[i].COLUMN_NAME + "," + Environment.NewLine;
                        }
                        else
                        {
                            SQL += T4 + ATTRIBUTES[i].COLUMN_NAME + Environment.NewLine;
                        }
                    }
                }
                SQL += T4 + ")" + Environment.NewLine + T4 + "VALUES(" + Environment.NewLine;



                COLUMN_COUNT = 0;


                for (int i = 0; i < ATTRIBUTES.Count; i++)
                {
                    if (ATTRIBUTES[i].INSERT_YN == "Y")
                    {
                        COLUMN_COUNT++;
                        if (COLUMN_COUNT < COLUMNS_TO_INSERT)
                        {
                            SQL += T4 + ATTRIBUTES[i].MSSQL_PARM_STRING + "," + Environment.NewLine;
                        }
                        else
                        {
                            SQL += T4 + ATTRIBUTES[i].MSSQL_PARM_STRING + Environment.NewLine + T4 + ")" + Environment.NewLine + T4 + "END" + Environment.NewLine + T4 + Q + ";" + Environment.NewLine;
                        }
                    }
                }


                return SQL;
            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return
                    "////EXCEPTION ERROR " + Environment.NewLine +
                    "CLASS: " + this.GetType().Name + Environment.NewLine +
                    "METHOD: " + System.Reflection.MethodBase.GetCurrentMethod().Name + Environment.NewLine +
                    "MESSAGE: " + ex.Message + Environment.NewLine;
            }
            finally
            {
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }
        }
        private string SQL_INSERT_A_ROW_WHERE_NOT_EXISTS(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES, List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES2)
        {
            try
            {
                int COLUMNS_TO_INSERT = 0;
                int COLUMNS_IN_NOT_EXISTS = 0;
                int COLUMN_COUNT = 0;
                for (int i = 0; i < ATTRIBUTES.Count; i++)
                {
                    if (ATTRIBUTES[i].INSERT_YN == "Y")
                    {
                        COLUMNS_TO_INSERT++;
                    }
                }
                for (int i = 0; i < ATTRIBUTES2.Count; i++)
                {

                    COLUMNS_IN_NOT_EXISTS++;
                }
                string SQL = "IF NOT EXISTS (SELECT * FROM " + ATTRIBUTES2[0].MSSQL_TABLE_SCHEMA + "." + ATTRIBUTES2[0].OBJECT_NAME + " WHERE " + Environment.NewLine;
                for (int i = 0; i < ATTRIBUTES2.Count; i++)
                {
                    COLUMN_COUNT++;
                    if (COLUMN_COUNT < COLUMNS_IN_NOT_EXISTS)
                    {
                        SQL += T3 + ATTRIBUTES2[i].COLUMN_NAME + " = " + ATTRIBUTES2[i].MSSQL_PARM_STRING + " AND " + Environment.NewLine;
                    }
                    else
                    {
                        SQL += T3 + ATTRIBUTES2[i].COLUMN_NAME + " = " + ATTRIBUTES2[i].MSSQL_PARM_STRING + ")" + Environment.NewLine + T3 + "BEGIN " + Environment.NewLine;
                    }
                }















                SQL += T4 + "INSERT INTO " + ATTRIBUTES[0].MSSQL_TABLE_SCHEMA + "." + ATTRIBUTES[0].OBJECT_NAME + "(" + Environment.NewLine;
                COLUMN_COUNT = 0;
                for (int i = 0; i < ATTRIBUTES.Count; i++)
                {
                    if (ATTRIBUTES[i].INSERT_YN == "Y")
                    {
                        COLUMN_COUNT++;
                        if (COLUMN_COUNT < COLUMNS_TO_INSERT)
                        {
                            SQL += T4 + ATTRIBUTES[i].COLUMN_NAME + "," + Environment.NewLine;
                        }
                        else
                        {
                            SQL += T4 + ATTRIBUTES[i].COLUMN_NAME + Environment.NewLine;
                        }
                    }
                }
                SQL += T4 + ")" + Environment.NewLine + T3 + "VALUES(" + Environment.NewLine;



                COLUMN_COUNT = 0;


                for (int i = 0; i < ATTRIBUTES.Count; i++)
                {
                    if (ATTRIBUTES[i].INSERT_YN == "Y")
                    {
                        COLUMN_COUNT++;
                        if (COLUMN_COUNT < COLUMNS_TO_INSERT)
                        {
                            SQL += T4 + ATTRIBUTES[i].MSSQL_PARM_STRING + "," + Environment.NewLine;
                        }
                        else
                        {
                            SQL += T4 + ATTRIBUTES[i].MSSQL_PARM_STRING + Environment.NewLine + T3 + ")" + Environment.NewLine + T3 + "END" + Environment.NewLine + T3 + Q + ";" + Environment.NewLine;
                        }
                    }
                }


                return SQL;
            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return
                    "////EXCEPTION ERROR " + Environment.NewLine +
                    "CLASS: " + this.GetType().Name + Environment.NewLine +
                    "METHOD: " + System.Reflection.MethodBase.GetCurrentMethod().Name + Environment.NewLine +
                    "MESSAGE: " + ex.Message + Environment.NewLine;
            }
            finally
            {
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }
        }
        private string SQL_INSERT_A_ROW(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES)
        {

            try
            {
                int COLUMNS_TO_INSERT = 0;
                int COLUMN_COUNT = 0;


                for (int i = 0; i < ATTRIBUTES.Count; i++)
                {
                    if (ATTRIBUTES[i].INSERT_YN == "Y")
                    {
                        COLUMNS_TO_INSERT++;
                    }

                }

                string SQL = "INSERT INTO " + ATTRIBUTES[0].MSSQL_TABLE_SCHEMA + "." + ATTRIBUTES[0].OBJECT_NAME + "(" + Environment.NewLine;
                for (int i = 0; i < ATTRIBUTES.Count; i++)
                {

                    if (ATTRIBUTES[i].INSERT_YN == "Y")
                    {
                        COLUMN_COUNT++;
                        if (COLUMN_COUNT < COLUMNS_TO_INSERT)
                        {
                            SQL += T3 + ATTRIBUTES[i].COLUMN_NAME + "," + Environment.NewLine;
                        }
                        else
                        {
                            SQL += T3 + ATTRIBUTES[i].COLUMN_NAME + Environment.NewLine;
                        }
                    }


                }
                SQL += T3 + ")" + Environment.NewLine + T3 + "VALUES(" + Environment.NewLine;


                COLUMN_COUNT = 0;

                for (int i = 0; i < ATTRIBUTES.Count; i++)
                {
                    if (ATTRIBUTES[i].INSERT_YN == "Y")
                    {
                        COLUMN_COUNT++;
                        if (COLUMN_COUNT < COLUMNS_TO_INSERT)
                        {
                            SQL += T3 + ATTRIBUTES[i].MSSQL_PARM_STRING + "," + Environment.NewLine;
                        }
                        else
                        {
                            SQL += T3 + ATTRIBUTES[i].MSSQL_PARM_STRING + Environment.NewLine + T3 + ")" + Q + ";" + Environment.NewLine;
                        }
                    }
                }
                return SQL;
            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return
                    "////EXCEPTION ERROR " + Environment.NewLine +
                    "CLASS: " + this.GetType().Name + Environment.NewLine +
                    "METHOD: " + System.Reflection.MethodBase.GetCurrentMethod().Name + Environment.NewLine +
                    "MESSAGE: " + ex.Message + Environment.NewLine;
            }
            finally
            {
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }
        }
        private string SQL_UPDATE_A_ROW_BY_PK(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES)
        {

            try
            {
                int COLUMNS_TO_UPDATE = 0;
                int COLUMN_COUNT = 0;
                for (int i = 0; i < ATTRIBUTES.Count; i++)
                {
                    if (ATTRIBUTES[i].UPDATE_YN == "Y")
                    {
                        COLUMNS_TO_UPDATE++;
                    }
                }

                string SQL = "UPDATE " + ATTRIBUTES[0].MSSQL_TABLE_SCHEMA + "." + ATTRIBUTES[0].OBJECT_NAME + " SET " + Environment.NewLine;
                for (int i = 0; i < ATTRIBUTES.Count; i++)
                {
                    if (ATTRIBUTES[i].UPDATE_YN == "Y")
                    {
                        COLUMN_COUNT++;
                        if (COLUMN_COUNT < COLUMNS_TO_UPDATE)
                        {
                            SQL += T3 + ATTRIBUTES[i].COLUMN_NAME + " = " + ATTRIBUTES[i].MSSQL_PARM_STRING + "," + Environment.NewLine;
                        }
                        else
                        {
                            SQL += T3 + ATTRIBUTES[i].COLUMN_NAME + " = " + ATTRIBUTES[i].MSSQL_PARM_STRING + Environment.NewLine;
                        }
                    }
                }
                SQL += T3 + _SQL_WHERE;

                return SQL + Q + ";" + Environment.NewLine;
            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return
                    "////EXCEPTION ERROR " + Environment.NewLine +
                    "CLASS: " + this.GetType().Name + Environment.NewLine +
                    "METHOD: " + System.Reflection.MethodBase.GetCurrentMethod().Name + Environment.NewLine +
                    "MESSAGE: " + ex.Message + Environment.NewLine;
            }
            finally
            {
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }
        }
        private string SQL_UPDATE(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES1, List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES2)
        {

            try
            {
                string SQL_WHERE = Environment.NewLine + T3 + " WHERE " + Environment.NewLine;
                int COLUMNS_TO_UPDATE = 0;
                int COLUMNS_IN_WHERE = ATTRIBUTES2.Count;
                int COLUMN_COUNT = 0;
                for (int i = 0; i < ATTRIBUTES1.Count; i++)
                {
                    if (ATTRIBUTES1[i].UPDATE_YN == "Y")
                    {
                        COLUMNS_TO_UPDATE++;
                    }
                }

                string SQL = "UPDATE " + ATTRIBUTES1[0].MSSQL_TABLE_SCHEMA + "." + ATTRIBUTES1[0].OBJECT_NAME + " SET " + Environment.NewLine;
                for (int i = 0; i < ATTRIBUTES1.Count; i++)
                {
                    if (ATTRIBUTES1[i].UPDATE_YN == "Y")
                    {
                        COLUMN_COUNT++;
                        if (COLUMN_COUNT < COLUMNS_TO_UPDATE)
                        {
                            SQL += T3 + ATTRIBUTES1[i].COLUMN_NAME + " = " + ATTRIBUTES1[i].MSSQL_PARM_STRING + "," + Environment.NewLine;
                        }
                        else
                        {
                            SQL += T3 + ATTRIBUTES1[i].COLUMN_NAME + " = " + ATTRIBUTES1[i].MSSQL_PARM_STRING + Environment.NewLine;
                        }
                    }
                }


                COLUMN_COUNT = 0;
                for (int i = 0; i < ATTRIBUTES2.Count; i++)
                {
                    COLUMN_COUNT++;
                    if (COLUMN_COUNT < COLUMNS_IN_WHERE)
                    {
                        SQL_WHERE += T3 + ATTRIBUTES2[i].COLUMN_NAME + " = " + ATTRIBUTES2[i].MSSQL_PARM_STRING + " AND " + Environment.NewLine;
                    }
                    else
                    {
                        SQL_WHERE += T3 + ATTRIBUTES2[i].COLUMN_NAME + " = " + ATTRIBUTES2[i].MSSQL_PARM_STRING;
                    }

                }



                SQL += T2 + SQL_WHERE;

                return SQL + Q + ";" + Environment.NewLine;
            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return
                    "////EXCEPTION ERROR " + Environment.NewLine +
                    "CLASS: " + this.GetType().Name + Environment.NewLine +
                    "METHOD: " + System.Reflection.MethodBase.GetCurrentMethod().Name + Environment.NewLine +
                    "MESSAGE: " + ex.Message + Environment.NewLine;
            }
            finally
            {
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }
        }
        private string SQL_DELETE(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES1)
        {

            try
            {
                string SQL_WHERE = " WHERE " + Environment.NewLine;

                int COLUMNS_IN_WHERE = ATTRIBUTES1.Count;
                int COLUMN_COUNT = 0;

                for (int i = 0; i < ATTRIBUTES1.Count; i++)
                {
                    COLUMN_COUNT++;
                    if (COLUMN_COUNT < COLUMNS_IN_WHERE)
                    {
                        SQL_WHERE += T3 + ATTRIBUTES1[i].COLUMN_NAME + " = " + ATTRIBUTES1[i].MSSQL_PARM_STRING + " AND " + Environment.NewLine;
                    }
                    else
                    {
                        SQL_WHERE += T3 + ATTRIBUTES1[i].COLUMN_NAME + " = " + ATTRIBUTES1[i].MSSQL_PARM_STRING;
                    }

                }





                string SQL = "DELETE FROM " + ATTRIBUTES1[0].MSSQL_TABLE_SCHEMA + "." + ATTRIBUTES1[0].OBJECT_NAME + Environment.NewLine + T3 + SQL_WHERE;
                return SQL + Q + ";" + Environment.NewLine;

            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return
                    "////EXCEPTION ERROR " + Environment.NewLine +
                    "CLASS: " + this.GetType().Name + Environment.NewLine +
                    "METHOD: " + System.Reflection.MethodBase.GetCurrentMethod().Name + Environment.NewLine +
                    "MESSAGE: " + ex.Message + Environment.NewLine;
            }
            finally
            {
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }
        }
        private string SQL_DELETE_A_ROW_BY_PK(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES)
        {

            try
            {
                string SQL = "DELETE FROM " + ATTRIBUTES[0].MSSQL_TABLE_SCHEMA + "." + ATTRIBUTES[0].OBJECT_NAME + Environment.NewLine + T3 + _SQL_WHERE;
                return SQL + Q + ";" + Environment.NewLine;
            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return
                    "////EXCEPTION ERROR " + Environment.NewLine +
                    "CLASS: " + this.GetType().Name + Environment.NewLine +
                    "METHOD: " + System.Reflection.MethodBase.GetCurrentMethod().Name + Environment.NewLine +
                    "MESSAGE: " + ex.Message + Environment.NewLine;
            }
            finally
            {
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }
        }



        private string SQL_DELETE_A_ROW_BY_PK_STORED_PROCEDURE(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES)
        {

            try
            {

                int cnt = 0;
                string SQL = "CREATE PROCEDURE DBO.spDEL_" + ATTRIBUTES[0].OBJECT_NAME + "_PK" + Environment.NewLine;
                foreach (MSSQL_CS_ATTRIBUTE a in ATTRIBUTES)
                {
                    if (a.PK_YN == "Y")
                    {
                        cnt++;
                        if (cnt < _COLUMNS_IN_PK)
                        {
                            SQL += a.MSSQL_PARM_STRING + " " + a.MSSQL_DATA_TYPE + "," + Environment.NewLine;
                        }
                        else
                        {
                            SQL += a.MSSQL_PARM_STRING + " " + a.MSSQL_DATA_TYPE + Environment.NewLine + " AS " + Environment.NewLine + "DELETE FROM " + ATTRIBUTES[0].MSSQL_TABLE_SCHEMA + "."
                                + ATTRIBUTES[0].OBJECT_NAME + Environment.NewLine + " WHERE " + Environment.NewLine;
                        }
                    }

                }

                cnt = 0;
                foreach (MSSQL_CS_ATTRIBUTE a in ATTRIBUTES)
                {
                    if (a.PK_YN == "Y")
                    {
                        cnt++;
                        if (cnt < _COLUMNS_IN_PK)
                        {
                            SQL += a.COLUMN_NAME + " = " + a.MSSQL_PARM_STRING + " AND " + Environment.NewLine;
                        }
                        else
                        {
                            SQL += a.COLUMN_NAME + " = " + a.MSSQL_PARM_STRING + Environment.NewLine;

                        }
                    }

                }



                return SQL;
            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return
                    "////EXCEPTION ERROR " + Environment.NewLine +
                    "CLASS: " + this.GetType().Name + Environment.NewLine +
                    "METHOD: " + System.Reflection.MethodBase.GetCurrentMethod().Name + Environment.NewLine +
                    "MESSAGE: " + ex.Message + Environment.NewLine;
            }
            finally
            {
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }
        }












        private string SQL_DELETE_ALL_ROWS(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES)
        {

            try
            {
                string SQL = "DELETE FROM " + ATTRIBUTES[0].MSSQL_TABLE_SCHEMA + "." + ATTRIBUTES[0].OBJECT_NAME;
                return SQL + Q + ";" + Environment.NewLine;
            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return
                    "////EXCEPTION ERROR " + Environment.NewLine +
                    "CLASS: " + this.GetType().Name + Environment.NewLine +
                    "METHOD: " + System.Reflection.MethodBase.GetCurrentMethod().Name + Environment.NewLine +
                    "MESSAGE: " + ex.Message + Environment.NewLine;
            }
            finally
            {
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }
        }

        #endregion



        #endregion
        #region READ AREA
     
        public string GENERATE_CS_TABLE_CUSTOM_READ_METHOD(CS_METHOD_OPTION_CUSTOM CS_METHOD_OPTION_CUSTOM, List<MSSQL_CS_ATTRIBUTE> COLUMNS_IN_SELECT, List<MSSQL_CS_ATTRIBUTE> COLUMNS_IN_WHERE)
        {
            MSSQL_CS_METHOD CUSTOM_METHOD = new MSSQL_CS_METHOD();
            switch (CS_METHOD_OPTION_CUSTOM)
            {
                case CS_METHOD_OPTION_CUSTOM.SELECT:
                    break;
                case CS_METHOD_OPTION_CUSTOM.SELECT_AVG:
                    break;
                case CS_METHOD_OPTION_CUSTOM.SELECT_BETWEEN:
                    break;
                case CS_METHOD_OPTION_CUSTOM.SELECT_COUNT:
                    break;
                case CS_METHOD_OPTION_CUSTOM.SELECT_DISTINCT:
                    break;
                case CS_METHOD_OPTION_CUSTOM.SELECT_MAX:
                    break;
                case CS_METHOD_OPTION_CUSTOM.SELECT_MIN:
                    break;
                case CS_METHOD_OPTION_CUSTOM.SELECT_SUM:
                    break;

            }


            return "";
        }
     


        public void  CS_GENERATE_METHOD_SELECT_CUSTOM(  List<MSSQL_CS_ATTRIBUTE> COLUMNS_IN_SELECT, List<MSSQL_CS_ATTRIBUTE> COLUMNS_IN_WHERE_CLAUSE)
        {
        MSSQL_CS_METHOD MSSQL_CS_METHOD = new MSSQL_CS_METHOD
        {
            CS_METHOD_OPTION_CUSTOM = CS_METHOD_OPTION_CUSTOM.SELECT
        };
         

        try
        {
            String CS_METHOD_NAME = null;
            String SP_NAME = null;
            string PARAMETERS = T3 + Environment.NewLine + Environment.NewLine + T3 + "/////COLUMNS TO SELECT BY////" + Environment.NewLine + Environment.NewLine;
            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in COLUMNS_IN_WHERE_CLAUSE)
            {
                PARAMETERS += T3 + ATTRIBUTE.CS_PARM_STRING + Environment.NewLine;
            }
            string DML = "SEL";
            string SUFIX= null;
            if (COLUMNS_IN_WHERE_CLAUSE.Count == 0)
            {
                SUFIX =  "SEL_CUSTOM";
            }
            else
            {
                if (COLUMNS_IN_WHERE_CLAUSE.Count < 3)
                {
                    SUFIX =  "_BY_";
                    for (int i = 0; i < COLUMNS_IN_WHERE_CLAUSE.Count; i++)
                    {
                        if (i < COLUMNS_IN_WHERE_CLAUSE.Count - 1)
                        {
                            SUFIX += COLUMNS_IN_WHERE_CLAUSE[i].COLUMN_NAME + "_";
                        }
                        else
                        {
                            SUFIX += COLUMNS_IN_WHERE_CLAUSE[i].COLUMN_NAME;
                        }
                    }
                }
                else
                {
                    SUFIX = "_BY_" + COLUMNS_IN_WHERE_CLAUSE.Count + "_COLUMNS_CUSTOM";
                }
            }
            SP_NAME = UTL_GENERATE_CUSTOM_STORED_PROCEDURE_NAME(OBJECT_NAME.ToUpper(), DML, SUFIX);
            CS_METHOD_NAME = DML + SUFIX;
            MSSQL_CS_METHOD.CS = null;
            MSSQL_CS_METHOD.CS += T1 + "public System.Data.DataSet " + CS_METHOD_NAME + "(" + UTL_GENERATE_MODEL_CLASS_NAME(COLUMNS_IN_WHERE_CLAUSE[0].OBJECT_NAME.ToUpper()) + " o)" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T1 + "{" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T2 + "try" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T3 + "cmd = new System.Data.SqlClient.SqlCommand();" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T3 + "cmd.CommandType = System.Data.CommandType.StoredProcedure;" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T3 + "cmd.CommandText = " + Q + SP_NAME + Q + ";" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T3 + PARAMETERS;
            MSSQL_CS_METHOD.CS += T3 + "return GET_SP_DATASET(DEFAULT_DATABASE_NAME, cmd   );" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T2 + "catch (System.Data.SqlClient.SqlException SqlException)" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T3 + "LOG_ERROR_EVENT_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|" + Q + @" + SqlException.Message);" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T3 + "return null;" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T2 + "finally" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T3 + "cmd = null;" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T3 + " LOG_INFORMATION_EVENT_VERBOSE_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|finally" + Q + @");" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T1 + "} /// end of CUSTOM SELECT" + Environment.NewLine;
            LIST_MSSQL_CS_METHOD.Add(MSSQL_CS_METHOD);
        }
        catch (Exception ex)
        {
            LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);

        }
        finally
        {
            MSSQL_CS_METHOD = null;
            LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
        }
    }
        public void CS_GENERATE_METHOD_SELECT_DISTINCT_CUSTOM(List<MSSQL_CS_ATTRIBUTE> DISTINCT_COLUMN, List<MSSQL_CS_ATTRIBUTE> COLUMNS_IN_WHERE_CLAUSE)
        {

            //MSSQL_CS_METHOD MSSQL_CS_METHOD = new MSSQL_CS_METHOD();
            //try
            //{

            //String DISTINCT_COLUMN_NAME = DISTINCT_COLUMN[0].COLUMN_NAME;
            //    String CS_METHOD_NAME = null;
            //    String SP_NAME = null;
            //    string PARAMETERS =  "/////COLUMNS TO SELECT BY////" + Environment.NewLine;
            //    foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in COLUMNS_IN_WHERE_CLAUSE)
            //    {
            //        PARAMETERS += T3 + ATTRIBUTE.CS_PARM_STRING + Environment.NewLine;
            //    }

            //    string SP_PREFIX = null;

            //    if (COLUMNS_IN_WHERE_CLAUSE.Count == 0)
            //    {
            //        SP_PREFIX = "SEL_DISTINCT_" + DISTINCT_COLUMN_NAME;
            //        CS_METHOD_NAME = "SEL_DISTINCT_" + DISTINCT_COLUMN_NAME;
            //    }
            //    else
            //    {
            //        if (COLUMNS_IN_WHERE_CLAUSE.Count < 3)
            //        {

            //            SP_PREFIX = "SEL_DISTINCT_" + DISTINCT_COLUMN_NAME + "_BY_";
            //            CS_METHOD_NAME = "SEL_DISTINCT_" + DISTINCT_COLUMN_NAME + "_BY_";
            //            for (int i = 0; i < COLUMNS_IN_WHERE_CLAUSE.Count; i++)
            //            {
            //                if (i < COLUMNS_IN_WHERE_CLAUSE.Count - 1)
            //                {

            //                    SP_PREFIX += COLUMNS_IN_WHERE_CLAUSE[i].COLUMN_NAME + "_";
            //                    CS_METHOD_NAME += COLUMNS_IN_WHERE_CLAUSE[i].COLUMN_NAME + "_";
            //                }
            //                else
            //                {

            //                    SP_PREFIX += COLUMNS_IN_WHERE_CLAUSE[i].COLUMN_NAME;
            //                    CS_METHOD_NAME += COLUMNS_IN_WHERE_CLAUSE[i].COLUMN_NAME;
            //                }
            //            }
            //        }
            //        else
            //        {

            //            SP_PREFIX = "SEL_DISTINCT_" + DISTINCT_COLUMN_NAME + "_BY_" + COLUMNS_IN_WHERE_CLAUSE.Count + "_COLUMNS";
            //            CS_METHOD_NAME = "SEL_DISTINCT_" + DISTINCT_COLUMN_NAME + "_BY_" + COLUMNS_IN_WHERE_CLAUSE.Count + "_COLUMNS";
            //        }
            //    }



            //    SP_NAME = UTL_GENERATE_STORED_PROCEDURE_NAME(OBJECT_NAME.ToUpper(), SP_PREFIX);
             

            //    _CS_CUSTOM_SELECT_DISTINCT_METHOD_SOURCE_CODE = null;
            //    _CS_CUSTOM_SELECT_DISTINCT_METHOD_SOURCE_CODE += T1 + "public System.Data.DataSet " + CS_METHOD_NAME + "(" + UTL_GENERATE_MODEL_CLASS_NAME(OBJECT_NAME.ToUpper()) + " o)" + Environment.NewLine;
            //    _CS_CUSTOM_SELECT_DISTINCT_METHOD_SOURCE_CODE += T1 + "{" + Environment.NewLine;
            //    _CS_CUSTOM_SELECT_DISTINCT_METHOD_SOURCE_CODE += T2 + "try" + Environment.NewLine;
            //    _CS_CUSTOM_SELECT_DISTINCT_METHOD_SOURCE_CODE += T2 + "{" + Environment.NewLine;
            //    _CS_CUSTOM_SELECT_DISTINCT_METHOD_SOURCE_CODE += T3 + "cmd = new System.Data.SqlClient.SqlCommand();" + Environment.NewLine;
            //    _CS_CUSTOM_SELECT_DISTINCT_METHOD_SOURCE_CODE += T3 + "cmd.CommandType = System.Data.CommandType.StoredProcedure;" + Environment.NewLine + Environment.NewLine;

            //    _CS_CUSTOM_SELECT_DISTINCT_METHOD_SOURCE_CODE += T3 + "/////NAME OF STORED PROCEDURE" + Environment.NewLine ;
            //    _CS_CUSTOM_SELECT_DISTINCT_METHOD_SOURCE_CODE += T3 + "cmd.CommandText = " + Q + SP_NAME + Q + ";" + Environment.NewLine + Environment.NewLine;

            //    if (COLUMNS_IN_WHERE_CLAUSE.Count != 0)
            //    {
            //        _CS_CUSTOM_SELECT_DISTINCT_METHOD_SOURCE_CODE += T3 + PARAMETERS + Environment.NewLine + Environment.NewLine;
            //    }

                  
            //    _CS_CUSTOM_SELECT_DISTINCT_METHOD_SOURCE_CODE += T3 + "return GET_SP_DATASET(DEFAULT_DATABASE_NAME, cmd   );" + Environment.NewLine;
            //    _CS_CUSTOM_SELECT_DISTINCT_METHOD_SOURCE_CODE += T2 + "}" + Environment.NewLine;
            //    _CS_CUSTOM_SELECT_DISTINCT_METHOD_SOURCE_CODE += T2 + "catch (System.Data.SqlClient.SqlException SqlException)" + Environment.NewLine;
            //    _CS_CUSTOM_SELECT_DISTINCT_METHOD_SOURCE_CODE += T2 + "{" + Environment.NewLine;
            //    _CS_CUSTOM_SELECT_DISTINCT_METHOD_SOURCE_CODE += T3 + "LOG_ERROR_EVENT_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|" + Q + @" + SqlException.Message);" + Environment.NewLine;
            //    _CS_CUSTOM_SELECT_DISTINCT_METHOD_SOURCE_CODE += T3 + "return null;" + Environment.NewLine;
            //    _CS_CUSTOM_SELECT_DISTINCT_METHOD_SOURCE_CODE += T2 + "}" + Environment.NewLine;
            //    _CS_CUSTOM_SELECT_DISTINCT_METHOD_SOURCE_CODE += T2 + "finally" + Environment.NewLine;
            //    _CS_CUSTOM_SELECT_DISTINCT_METHOD_SOURCE_CODE += T2 + "{" + Environment.NewLine;
            //    _CS_CUSTOM_SELECT_DISTINCT_METHOD_SOURCE_CODE += T3 + "cmd = null;" + Environment.NewLine;
            //    _CS_CUSTOM_SELECT_DISTINCT_METHOD_SOURCE_CODE += T3 + " LOG_INFORMATION_EVENT_VERBOSE_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|finally" + Q + @");" + Environment.NewLine;
            //    _CS_CUSTOM_SELECT_DISTINCT_METHOD_SOURCE_CODE += T2 + "}" + Environment.NewLine;
            //    _CS_CUSTOM_SELECT_DISTINCT_METHOD_SOURCE_CODE += T1 + "} /// end of CUSTOM SELECT DISTINCT" + Environment.NewLine;


            //}
            //catch (Exception ex)
            //{
            //    LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
               
            //}
            //finally
            //{
            //    MSSQL_CS_METHOD = null;
            //    LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            //}


        }
        public void  CS_GENERATE_VIEW_READ_CLASS(List<CS_METHOD_OPTION_DEFAULT> OPTIONS )
        {

            List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES = GET_MSSQL_CS_ATTRIBUTES_VIEW(SERVER_NAME, DATABASE_NAME.ToUpper(), OBJECT_ID);



                MSSQL_CS_CLASS_MODEL m = new MSSQL_CS_CLASS_MODEL
                {
                    CS_CLASS_NAME = UTL_GENERATE_READ_CLASS_NAME(OBJECT_NAME.ToUpper()),
                    CLASS_TYPE = MSSQL_CS_CLASS_MODEL.CS_CLASS_TYPE.READ,
                    CS_SOURCE_CODE = null,
                    CS_FILE_NAME = UTL_GENERATE_READ_CLASS_NAME(OBJECT_NAME.ToUpper()) + ".cs",
                     CS_FILE_FOLDER = CS_READ_CLASS_FILE_FOLDER
                };
                if (!Directory.Exists(m.CS_FILE_FOLDER))
                {
                    Directory.CreateDirectory(m.CS_FILE_FOLDER);
                }

       
          
           
                m.CS_SOURCE_CODE = "///////READ CLASS CREATED ON " + DateTime.Now + Environment.NewLine + Environment.NewLine;
                m.CS_SOURCE_CODE += "public class " + m.CS_CLASS_NAME + " : dbUTL" + Environment.NewLine;
                m.CS_SOURCE_CODE += "{" + Environment.NewLine;
                m.CS_SOURCE_CODE += T1 + "private const System.String DEFAULT_OBJECT_NAME =" + Q + OBJECT_NAME + Q + ";" + Environment.NewLine;
                m.CS_SOURCE_CODE += T1 + "private const System.String DEFAULT_DATABASE_NAME =" + Q + DATABASE_NAME + Q + ";" + Environment.NewLine;

                m.CS_SOURCE_CODE += T1 + "public event EVENT_HANDLER INFORMATION_EVENT_VERBOSE_LOCAL;" + Environment.NewLine;

                m.CS_SOURCE_CODE += T1 + "public event EVENT_HANDLER INFORMATION_EVENT_LOCAL;" + Environment.NewLine;

                m.CS_SOURCE_CODE += T1 + "public event EVENT_HANDLER WARNING_EVENT_LOCAL;" + Environment.NewLine;

                m.CS_SOURCE_CODE += T1 + "public event EVENT_HANDLER ERROR_EVENT_LOCAL;" + Environment.NewLine;

                m.CS_SOURCE_CODE += T1 + "private void LOG_INFORMATION_EVENT_VERBOSE_LOCAL(string MSG)" + Environment.NewLine;
                m.CS_SOURCE_CODE += T1 + "{" + Environment.NewLine;
                m.CS_SOURCE_CODE += T2 + "if (INFORMATION_EVENT_VERBOSE_LOCAL != null)" + Environment.NewLine;
                m.CS_SOURCE_CODE += T2 + "{" + Environment.NewLine;
                m.CS_SOURCE_CODE += T3 + "INFORMATION_EVENT_VERBOSE_LOCAL(this, new EVENT_HANDLER_SUPER(MSG));" + Environment.NewLine;
                m.CS_SOURCE_CODE += T2 + "}" + Environment.NewLine;
                m.CS_SOURCE_CODE += T1 + "}" + Environment.NewLine;

                m.CS_SOURCE_CODE += T1 + "private void LOG_INFORMATION_EVENT_LOCAL(string MSG)" + Environment.NewLine;
                m.CS_SOURCE_CODE += T1 + "{" + Environment.NewLine;
                m.CS_SOURCE_CODE += T2 + "if (INFORMATION_EVENT_LOCAL != null)" + Environment.NewLine;
                m.CS_SOURCE_CODE += T2 + "{" + Environment.NewLine;
                m.CS_SOURCE_CODE += T3 + "INFORMATION_EVENT_LOCAL(this, new EVENT_HANDLER_SUPER(MSG));" + Environment.NewLine;
                m.CS_SOURCE_CODE += T2 + "}" + Environment.NewLine;
                m.CS_SOURCE_CODE += T1 + "}" + Environment.NewLine;

                m.CS_SOURCE_CODE += T1 + "private void LOG_WARNING_EVENT_LOCAL(string MSG)" + Environment.NewLine;
                m.CS_SOURCE_CODE += T1 + "{" + Environment.NewLine;
                m.CS_SOURCE_CODE += T2 + "if (WARNING_EVENT_LOCAL != null)" + Environment.NewLine;
                m.CS_SOURCE_CODE += T2 + "{" + Environment.NewLine;
                m.CS_SOURCE_CODE += T3 + "WARNING_EVENT_LOCAL(this, new EVENT_HANDLER_SUPER(MSG));" + Environment.NewLine;
                m.CS_SOURCE_CODE += T2 + "}" + Environment.NewLine;
                m.CS_SOURCE_CODE += T1 + "}" + Environment.NewLine;

                m.CS_SOURCE_CODE += T1 + "private void LOG_ERROR_EVENT_LOCAL(string MSG)" + Environment.NewLine;
                m.CS_SOURCE_CODE += T1 + "{" + Environment.NewLine;
                m.CS_SOURCE_CODE += T2 + "if (ERROR_EVENT_LOCAL != null)" + Environment.NewLine;
                m.CS_SOURCE_CODE += T2 + "{" + Environment.NewLine;
                m.CS_SOURCE_CODE += T3 + "ERROR_EVENT_LOCAL(this, new EVENT_HANDLER_SUPER(MSG));" + Environment.NewLine;
                m.CS_SOURCE_CODE += T2 + "}" + Environment.NewLine;
                m.CS_SOURCE_CODE += T1 + "}" + Environment.NewLine;

                    foreach (CS_METHOD_OPTION_DEFAULT OPTION in OPTIONS)
                    {
                    m.CS_SOURCE_CODE += GENERATE_READ_METHOD(ATTRIBUTES, OPTION);
                    }

                m.CS_SOURCE_CODE += "}   /////END OF CLASS" + Environment.NewLine + Environment.NewLine;
                if (SAVE_SOURCE_CODE_TO_FILE)
                {
                    SAVE_FILE(m.CS_FILE_FOLDER, m.CS_FILE_NAME, m.CS_SOURCE_CODE, m.CS_CLASS_NAME);
                }
                LIST_MSSQL_CS_CLASS_MODEL.Add(m);
                m = null;
    }




        public void CS_GENERATE_TABLE_READ_CLASS(List<CS_METHOD_OPTION_DEFAULT> OPTIONS )
        {
            List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES = GET_MSSQL_CS_ATTRIBUTES_TABLE(SERVER_NAME, DATABASE_NAME.ToUpper(), OBJECT_ID);
            MSSQL_CS_CLASS_MODEL m = new MSSQL_CS_CLASS_MODEL
            {
                CS_CLASS_NAME = UTL_GENERATE_READ_CLASS_NAME(OBJECT_NAME.ToUpper()),
                CLASS_TYPE = MSSQL_CS_CLASS_MODEL.CS_CLASS_TYPE.READ,
                CS_SOURCE_CODE = null,
                CS_FILE_NAME = UTL_GENERATE_READ_CLASS_NAME(OBJECT_NAME.ToUpper()) + ".cs",
                 CS_FILE_FOLDER = CS_READ_CLASS_FILE_FOLDER
                
            };
            if (!Directory.Exists(m.CS_FILE_FOLDER))
            {
                Directory.CreateDirectory(m.CS_FILE_FOLDER);
            }

            m.CS_SOURCE_CODE = "///////READ CLASS CREATED ON " + DateTime.Now + Environment.NewLine + Environment.NewLine;
            m.CS_SOURCE_CODE += "public class " + m.CS_CLASS_NAME + " : dbUTL" + Environment.NewLine;
            m.CS_SOURCE_CODE += "{" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "private const System.String DEFAULT_OBJECT_NAME =" + Q + OBJECT_NAME + Q + ";" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "private const System.String DEFAULT_DATABASE_NAME =" + Q + DATABASE_NAME + Q + ";" + Environment.NewLine;

            m.CS_SOURCE_CODE += T1 + "public event EVENT_HANDLER INFORMATION_EVENT_VERBOSE_LOCAL;" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "public event EVENT_HANDLER INFORMATION_EVENT_LOCAL;" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "public event EVENT_HANDLER WARNING_EVENT_LOCAL;" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "public event EVENT_HANDLER ERROR_EVENT_LOCAL;" + Environment.NewLine;

            m.CS_SOURCE_CODE += T1 + "private void LOG_INFORMATION_EVENT_VERBOSE_LOCAL(string MSG)" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "{" + Environment.NewLine;
            m.CS_SOURCE_CODE += T2 + "if (INFORMATION_EVENT_VERBOSE_LOCAL != null)" + Environment.NewLine;
            m.CS_SOURCE_CODE += T2 + "{" + Environment.NewLine;
            m.CS_SOURCE_CODE += T3 + "INFORMATION_EVENT_VERBOSE_LOCAL(this, new EVENT_HANDLER_SUPER(MSG));" + Environment.NewLine;
            m.CS_SOURCE_CODE += T2 + "}" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "}" + Environment.NewLine;

            m.CS_SOURCE_CODE += T1 + "private void LOG_INFORMATION_EVENT_LOCAL(string MSG)" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "{" + Environment.NewLine;
            m.CS_SOURCE_CODE += T2 + "if (INFORMATION_EVENT_LOCAL != null)" + Environment.NewLine;
            m.CS_SOURCE_CODE += T2 + "{" + Environment.NewLine;
            m.CS_SOURCE_CODE += T3 + "INFORMATION_EVENT_LOCAL(this, new EVENT_HANDLER_SUPER(MSG));" + Environment.NewLine;
            m.CS_SOURCE_CODE += T2 + "}" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "}" + Environment.NewLine;

            m.CS_SOURCE_CODE += T1 + "private void LOG_WARNING_EVENT_LOCAL(string MSG)" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "{" + Environment.NewLine;
            m.CS_SOURCE_CODE += T2 + "if (WARNING_EVENT_LOCAL != null)" + Environment.NewLine;
            m.CS_SOURCE_CODE += T2 + "{" + Environment.NewLine;
            m.CS_SOURCE_CODE += T3 + "WARNING_EVENT_LOCAL(this, new EVENT_HANDLER_SUPER(MSG));" + Environment.NewLine;
            m.CS_SOURCE_CODE += T2 + "}" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "}" + Environment.NewLine;

            m.CS_SOURCE_CODE += T1 + "private void LOG_ERROR_EVENT_LOCAL(string MSG)" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "{" + Environment.NewLine;
            m.CS_SOURCE_CODE += T2 + "if (ERROR_EVENT_LOCAL != null)" + Environment.NewLine;
            m.CS_SOURCE_CODE += T2 + "{" + Environment.NewLine;
            m.CS_SOURCE_CODE += T3 + "ERROR_EVENT_LOCAL(this, new EVENT_HANDLER_SUPER(MSG));" + Environment.NewLine;
            m.CS_SOURCE_CODE += T2 + "}" + Environment.NewLine;
            m.CS_SOURCE_CODE += T1 + "}" + Environment.NewLine;
            foreach (CS_METHOD_OPTION_DEFAULT OPTION in OPTIONS)
            {
                m.CS_SOURCE_CODE += GENERATE_READ_METHOD(ATTRIBUTES, OPTION);
            }
            m.CS_SOURCE_CODE += "}   /////END OF CLASS" + Environment.NewLine + Environment.NewLine;
        if (SAVE_SOURCE_CODE_TO_FILE)
        {
            SAVE_FILE(m.CS_FILE_FOLDER, m.CS_FILE_NAME, m.CS_SOURCE_CODE, m.CS_CLASS_NAME);
        }
        LIST_MSSQL_CS_CLASS_MODEL.Add(m);
        m = null;
    }

 

        public string GENERATE_IUD_METHODS(List<CS_METHOD_OPTION_DEFAULT> OPTIONS, string SERVER_NAME, string DATABASE_NAME, int OBJECT_ID)
        {
            string s = null;
            List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES = GET_MSSQL_CS_ATTRIBUTES_TABLE(SERVER_NAME, DATABASE_NAME.ToUpper(), OBJECT_ID);
            foreach (CS_METHOD_OPTION_DEFAULT OPTION in OPTIONS)
            {
                s += GENERATE_IUD_METHOD(ATTRIBUTES, OPTION);
            }
            return s;
        }
        public string GENERATE_IUD_STORED_PROCEDURES(List<CS_METHOD_OPTION_DEFAULT> OPTIONS, string SERVER_NAME, string DATABASE_NAME, int OBJECT_ID)
        {
            string s = null;
            List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES = GET_MSSQL_CS_ATTRIBUTES_TABLE(SERVER_NAME, DATABASE_NAME.ToUpper(), OBJECT_ID);


            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
            {
                if (ATTRIBUTE.PK_YN == "Y")
                {
                    _COLUMNS_IN_PK++;
                }
            }


            foreach (CS_METHOD_OPTION_DEFAULT OPTION in OPTIONS)
            {
                s += GENERATE_IUD_STORED_PROCEDURE(ATTRIBUTES, OPTION);
            }
            return s;
        }
        public string GENERATE_IUD_METHOD(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES, CS_METHOD_OPTION_DEFAULT OPTION)
        {
            string s = null;

            switch (OPTION)
            {
                case CS_METHOD_OPTION_DEFAULT.INSERT_A_ROW:
                    s += CS_GENERATE_METHOD_INSERT_A_ROW(ATTRIBUTES);
                    break;
                case CS_METHOD_OPTION_DEFAULT.INSERT_WHERE_NOT_EXISTS_BY_PK:
                    s += CS_GENERATE_METHOD_INSERT_A_ROW_WHERE_NOT_EXISTS_BY_PK(ATTRIBUTES);
                    break;
                case CS_METHOD_OPTION_DEFAULT.UPDATE_A_ROW_BY_PK:
                    s += CS_GENERATE_METHOD_UPDATE_A_ROW_BY_PK(ATTRIBUTES);
                    break;
                case CS_METHOD_OPTION_DEFAULT.DELETE_A_ROW_BY_PK:
                    s += CS_GENERATE_METHOD_DELETE_A_ROW_BY_PK(ATTRIBUTES);
                    break;
                case CS_METHOD_OPTION_DEFAULT.DELETE_ALL_ROWS:
                    s += CS_GENERATE_METHOD_DELETE_ALL_ROWS(ATTRIBUTES);
                    break;

            }


            return s;
        }
        public string GENERATE_IUD_STORED_PROCEDURE(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES, CS_METHOD_OPTION_DEFAULT OPTION)
        {
            string s = null;

            switch (OPTION)
            {
                //case CS_METHOD_OPTION_DEFAULT.INSERT_A_ROW:
                //    s += CS_INSERT_A_ROW(ATTRIBUTES);
                //    break;
                //case CS_METHOD_OPTION_DEFAULT.UPDATE_A_ROW_BY_PK:
                //    s += CS_UPDATE_A_ROW_BY_PK(ATTRIBUTES);
                //    break;
                case CS_METHOD_OPTION_DEFAULT.DELETE_A_ROW_BY_PK:
                    s += SQL_DELETE_A_ROW_BY_PK_STORED_PROCEDURE(ATTRIBUTES);
                    break;
                    //case CS_METHOD_OPTION_DEFAULT.DELETE_ALL_ROWS:
                    //    s += CS_DELETE_ALL_ROWS(ATTRIBUTES);
                    //    break;

            }


            return s;
        }
        public string GENERATE_READ_METHOD(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES, CS_METHOD_OPTION_DEFAULT OPTION)
        {
            string s = null;
            switch (OPTION)
            {
                case CS_METHOD_OPTION_DEFAULT.SELECT_ALL:
                    s = CS_SELECT_X(ATTRIBUTES, "SEL_ALL_ROWS");
                    break;
                case CS_METHOD_OPTION_DEFAULT.SELECT_BY_PK:
                    s = CS_SELECT_BY_PK(ATTRIBUTES);
                    break;
                case CS_METHOD_OPTION_DEFAULT.SELECT_TOP_1:
                    s = CS_SELECT_X(ATTRIBUTES, "SEL_TOP_1");
                    break;
                case CS_METHOD_OPTION_DEFAULT.SELECT_TOP_100:
                    s = CS_SELECT_X(ATTRIBUTES, "SEL_TOP_100");
                    break;
                case CS_METHOD_OPTION_DEFAULT.SELECT_TOP_1000:
                    s = CS_SELECT_X(ATTRIBUTES, "SEL_TOP_1000");
                    break;
            }
            return s;
        }
        private string CS_SELECT_BY_PK(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES)
        {


            MSSQL_CS_METHOD MSSQL_CS_METHOD = new MSSQL_CS_METHOD();
            try
            {

                //////// "SEL_BY_PK");
                string p = null;

                foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
                {
                    if (ATTRIBUTE.PK_YN == "Y")
                    {
                        p += T3 + ATTRIBUTE.CS_PARM_STRING + Environment.NewLine;
                    }
                }
                MSSQL_CS_METHOD.MSSQL_CS_METHOD_NAME = "SEL_BY_PK";
                // MSSQL_CS_METHOD.CS += T1 + "public System.Data.DataSet SEL_BY_PK(" + ATTRIBUTES[0].OBJECT_NAME + " o)" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "public System.Data.DataSet SEL_BY_PK(" + UTL_GENERATE_MODEL_CLASS_NAME(ATTRIBUTES[0].OBJECT_NAME.ToUpper()) + " o)" + Environment.NewLine;

                MSSQL_CS_METHOD.CS += T1 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "try" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;

                //   MSSQL_CS_METHOD.CS += T3 + "System.String SQL_SEL_BY_PK = @" + Q + "SELECT " + SQL_SELECT_ALL(ATTRIBUTES, false) + _SQL_WHERE + Q + "; " + Environment.NewLine;




                MSSQL_CS_METHOD.CS += T3 + "cmd = new System.Data.SqlClient.SqlCommand();" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "cmd.CommandType = System.Data.CommandType.StoredProcedure;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += p + Environment.NewLine;
                


                MSSQL_CS_METHOD.CS += T2 + "cmd.CommandText = " + Q + UTL_GENERATE_STORED_PROCEDURE_NAME(ATTRIBUTES[0].OBJECT_NAME, "SEL_BY_PK") + Q + ";" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "return GET_SP_DATASET(DEFAULT_DATABASE_NAME, cmd   );" + Environment.NewLine;

            


                //  MSSQL_CS_METHOD.CS += T3 + "return GET_DATASET_WITH_PARMS(DEFAULT_DATABASE_NAME, DEFAULT_OBJECT_NAME, SQL_SEL_BY_PK);" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "catch (System.Data.SqlClient.SqlException SqlException)" + Environment.NewLine;

                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "LOG_ERROR_EVENT_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|" + Q + @" + SqlException.Message);" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "return null;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;

                MSSQL_CS_METHOD.CS += T2 + "finally" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + " cmd = null;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + " LOG_INFORMATION_EVENT_VERBOSE_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|finally" + Q + @");" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;

                MSSQL_CS_METHOD.CS += T1 + "} " + Environment.NewLine;

                _LIST_MSSQL_CS_METHOD.Add(MSSQL_CS_METHOD);
                return MSSQL_CS_METHOD.CS;

            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return
                    "EXCEPTION ERROR " + Environment.NewLine +
                    "CLASS: " + this.GetType().Name + Environment.NewLine +
                    "METHOD: " + System.Reflection.MethodBase.GetCurrentMethod().Name + Environment.NewLine +
                    "MESSAGE: " + ex.Message + Environment.NewLine;
            }
            finally
            {
                MSSQL_CS_METHOD = null;
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }

        }
        private string CS_SELECT_X(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES, string CS_METHOD_NAME)
        {

            MSSQL_CS_METHOD MSSQL_CS_METHOD = new MSSQL_CS_METHOD();
            try
            {
                MSSQL_CS_METHOD.MSSQL_CS_METHOD_NAME = CS_METHOD_NAME;
                MSSQL_CS_METHOD.CS += T1 + "public System.Data.DataSet " + MSSQL_CS_METHOD.MSSQL_CS_METHOD_NAME + "()" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "try" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "cmd = new System.Data.SqlClient.SqlCommand();" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "cmd.CommandType = System.Data.CommandType.StoredProcedure;" + Environment.NewLine;
            
                MSSQL_CS_METHOD.CS += T2 + "cmd.CommandText = " + Q + UTL_GENERATE_STORED_PROCEDURE_NAME(ATTRIBUTES[0].OBJECT_NAME, "SEL_ALL") + Q + ";" + Environment.NewLine;
             
                MSSQL_CS_METHOD.CS += T3 + "return GET_SP_DATASET(DEFAULT_DATABASE_NAME, cmd   );" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "catch (System.Data.SqlClient.SqlException SqlException)" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "LOG_ERROR_EVENT_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|" + Q + @" + SqlException.Message);" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "return null;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "finally" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + "cmd = null;" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T3 + " LOG_INFORMATION_EVENT_VERBOSE_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|finally" + Q + @");" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
                MSSQL_CS_METHOD.CS += T1 + "} /// end of " + MSSQL_CS_METHOD.MSSQL_CS_METHOD_NAME + Environment.NewLine;
                _LIST_MSSQL_CS_METHOD.Add(MSSQL_CS_METHOD);
                return MSSQL_CS_METHOD.CS;

            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return
                    "EXCEPTION ERROR " + Environment.NewLine +
                    "CLASS: " + this.GetType().Name + Environment.NewLine +
                    "METHOD: " + System.Reflection.MethodBase.GetCurrentMethod().Name + Environment.NewLine +
                    "MESSAGE: " + ex.Message + Environment.NewLine;
            }
            finally
            {
                MSSQL_CS_METHOD = null;
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }
        }
        private string SQL_SELECT_ALL(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES, bool SemiColon)
        {

            try
            {
                string SQL = " " + Environment.NewLine;
                for (int i = 0; i < ATTRIBUTES.Count; i++)
                {


                    if (i < ATTRIBUTES.Count - 1)
                    {
                        SQL += T3 + ATTRIBUTES[i].COLUMN_NAME + "," + Environment.NewLine;
                    }
                    else
                    {
                        SQL += T3 + ATTRIBUTES[i].COLUMN_NAME + Environment.NewLine;
                    }

                }
                SQL += T3 + T3 + "FROM " + ATTRIBUTES[0].MSSQL_TABLE_SCHEMA + "." + ATTRIBUTES[0].OBJECT_NAME;
                if (SemiColon)
                {
                    return SQL + Q + ";" + Environment.NewLine;
                }
                else
                {
                    return SQL;
                }

            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return
                    "////EXCEPTION ERROR " + Environment.NewLine +
                    "CLASS: " + this.GetType().Name + Environment.NewLine +
                    "METHOD: " + System.Reflection.MethodBase.GetCurrentMethod().Name + Environment.NewLine +
                    "MESSAGE: " + ex.Message + Environment.NewLine;
            }
            finally
            {
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }
        }
    #endregion


    #region CUSTOM DELETE


 


    public void CS_GENERATE_METHOD_DELETE_CUSTOM(List<MSSQL_CS_ATTRIBUTE> COLUMNS_IN_WHERE_CLAUSE)
    {


        MSSQL_CS_METHOD MSSQL_CS_METHOD = new MSSQL_CS_METHOD
        {
            CS_METHOD_OPTION_CUSTOM = CS_METHOD_OPTION_CUSTOM.DELETE
        };

        try
        {
            String CS_METHOD_NAME = null;
            String SP_NAME = null;
            string PARAMETERS = T3 + "/////TABLE COLUMNS TO DELETE BY////" + Environment.NewLine;
            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in COLUMNS_IN_WHERE_CLAUSE)
            {
                PARAMETERS += T3 + ATTRIBUTE.CS_PARM_STRING + Environment.NewLine;
            }

            string SUFIX = null;

            string DML = "DEL";

            if (COLUMNS_IN_WHERE_CLAUSE.Count == 0)
            {
                SUFIX = "_CUSTOM";

            }
            else
            {
                if (COLUMNS_IN_WHERE_CLAUSE.Count < 3)
                {

                    SUFIX = "_BY_";
                    for (int i = 0; i < COLUMNS_IN_WHERE_CLAUSE.Count; i++)
                    {
                        if (i < COLUMNS_IN_WHERE_CLAUSE.Count - 1)
                        {

                            SUFIX += COLUMNS_IN_WHERE_CLAUSE[i].COLUMN_NAME + "_";

                        }
                        else
                        {

                            SUFIX += COLUMNS_IN_WHERE_CLAUSE[i].COLUMN_NAME;

                        }
                    }
                }
                else
                {

                    SUFIX = "_BY_" + COLUMNS_IN_WHERE_CLAUSE.Count + "_COLUMNS_CUSTOM";

                }
            }

            SP_NAME = UTL_GENERATE_CUSTOM_STORED_PROCEDURE_NAME(OBJECT_NAME.ToUpper(), DML, SUFIX);
            CS_METHOD_NAME = DML + SUFIX;



            MSSQL_CS_METHOD.CS = null;
            MSSQL_CS_METHOD.CS += T1 + "public System.Boolean " + CS_METHOD_NAME + "(" + UTL_GENERATE_MODEL_CLASS_NAME(COLUMNS_IN_WHERE_CLAUSE[0].OBJECT_NAME.ToUpper()) + " o)" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T1 + "{" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T2 + "try" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T3 + "cmd = new System.Data.SqlClient.SqlCommand();" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T3 + "cmd.CommandType = System.Data.CommandType.StoredProcedure;" + Environment.NewLine;

            MSSQL_CS_METHOD.CS += T3 + "cmd.CommandText = " + Q + SP_NAME + Q + ";" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T3 + PARAMETERS;


            MSSQL_CS_METHOD.CS += T3 + "LOG_INFORMATION_EVENT_VERBOSE_LOCAL(GET_SQLSERVER_COMMAND_VALUES(cmd));" + Environment.NewLine;

            ////  
            MSSQL_CS_METHOD.CS += T3 + "return RUN_SQL_COMMAND(DEFAULT_DATABASE_NAME, DEFAULT_OBJECT_NAME );" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T2 + "catch (System.Data.SqlClient.SqlException SqlException)" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T3 + "LOG_ERROR_EVENT_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|" + Q + @" + SqlException.Message);" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T3 + "return null;" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T2 + "finally" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T3 + "cmd = null;" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T3 + " LOG_INFORMATION_EVENT_VERBOSE_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|finally" + Q + @");" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T1 + "} /// end of CUSTOM DELETE" + Environment.NewLine;
            LIST_MSSQL_CS_METHOD.Add(MSSQL_CS_METHOD);

        }
        catch (Exception ex)
        {
            LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
            //return
            //    "EXCEPTION ERROR " + Environment.NewLine +
            //    "CLASS: " + this.GetType().Name + Environment.NewLine +
            //    "METHOD: " + System.Reflection.MethodBase.GetCurrentMethod().Name + Environment.NewLine +
            //    "MESSAGE: " + ex.Message + Environment.NewLine;
        }
        finally
        {
            MSSQL_CS_METHOD = null;
            LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
        }



    }






    #endregion

    #region CUSTOM INSERT


    public void CS_GENERATE_METHOD_INSERT_CUSTOM(List<MSSQL_CS_ATTRIBUTE> COLUMNS_TO_INSERT)
    {
        MSSQL_CS_METHOD MSSQL_CS_METHOD = new MSSQL_CS_METHOD
        {
            CS_METHOD_OPTION_CUSTOM = CS_METHOD_OPTION_CUSTOM.INSERT
        };

         
        try
        {
            String CS_METHOD_NAME = null;
            String SP_NAME = null;
            string PARAMETERS = T3 + "/////TABLE COLUMNS INSERT////" + Environment.NewLine;
            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in COLUMNS_TO_INSERT)
            {
                PARAMETERS += T3 + ATTRIBUTE.CS_PARM_STRING + Environment.NewLine;
            }

            
            string DML = "INS_";
            string SP_PREFIX = "usp_";
            SP_NAME = SP_PREFIX+ DML + OBJECT_NAME.ToUpper() + "_CUSTOM";
            CS_METHOD_NAME = "INS_CUSTOM";
            MSSQL_CS_METHOD.CS = null;



            MSSQL_CS_METHOD.CS += T1 + "public System.Boolean " + CS_METHOD_NAME + "(" + UTL_GENERATE_MODEL_CLASS_NAME(COLUMNS_TO_INSERT[0].OBJECT_NAME.ToUpper()) + " o)" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T1 + "{" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T2 + "try" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T3 + "cmd = new System.Data.SqlClient.SqlCommand();" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T3 + "cmd.CommandType = System.Data.CommandType.StoredProcedure;" + Environment.NewLine;

            MSSQL_CS_METHOD.CS += T3 + "cmd.CommandText = " + Q + SP_NAME + Q + ";" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T3 + PARAMETERS;


            MSSQL_CS_METHOD.CS += T3 + "LOG_INFORMATION_EVENT_VERBOSE_LOCAL(GET_SQLSERVER_COMMAND_VALUES(cmd));" + Environment.NewLine;

            ////  
            MSSQL_CS_METHOD.CS += T3 + "return RUN_SQL_COMMAND(DEFAULT_DATABASE_NAME, DEFAULT_OBJECT_NAME );" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T2 + "catch (System.Data.SqlClient.SqlException SqlException)" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T3 + "LOG_ERROR_EVENT_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|" + Q + @" + SqlException.Message);" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T3 + "return null;" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T2 + "finally" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T2 + "{" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T3 + "cmd = null;" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T3 + " LOG_INFORMATION_EVENT_VERBOSE_LOCAL(this.GetType().Name + " + Q + "|" + Q + " + System.Reflection.MethodBase.GetCurrentMethod().Name + " + Q + "|finally" + Q + @");" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T2 + "}" + Environment.NewLine;
            MSSQL_CS_METHOD.CS += T1 + "} /// end of CUSTOM ISNERT" + Environment.NewLine;
            LIST_MSSQL_CS_METHOD.Add(MSSQL_CS_METHOD);

        }
        catch (Exception ex)
        {
            LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
            //return
            //    "EXCEPTION ERROR " + Environment.NewLine +
            //    "CLASS: " + this.GetType().Name + Environment.NewLine +
            //    "METHOD: " + System.Reflection.MethodBase.GetCurrentMethod().Name + Environment.NewLine +
            //    "MESSAGE: " + ex.Message + Environment.NewLine;
        }
        finally
        {
            MSSQL_CS_METHOD = null;
            LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
        }



    }




    #endregion




    #region CUSTOM UPDATE

    public void CS_GENERATE_METHOD_UPDATE_CUSTOM(List<MSSQL_CS_ATTRIBUTE> COLUMNS_TO_UPDATE, List<MSSQL_CS_ATTRIBUTE> COLUMNS_IN_WHERE_CLAUSE)
    {
        MSSQL_CS_METHOD MSSQL_CS_METHOD = new MSSQL_CS_METHOD
        {
            CS_METHOD_OPTION_CUSTOM = CS_METHOD_OPTION_CUSTOM.UPDATE
        };
        try
        {
            String CS_METHOD_NAME = null;
            String SP_NAME = null;
            string PARAMETERS = T3 + "/////TABLE COLUMNS UPDATE////" + Environment.NewLine;
            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in COLUMNS_TO_UPDATE)
            {
                PARAMETERS += T3 + ATTRIBUTE.CS_PARM_STRING + Environment.NewLine;
            }

            string SP_PREFIX = null;
            if (COLUMNS_TO_UPDATE.Count == 0)
            {
                SP_PREFIX = "UPD_CUSTOM";
                CS_METHOD_NAME = "UPD_CUSTOM";
            }
            else
            {

            }


        }
        catch (Exception ex)
        {
            LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);

        }
        finally
        {
            MSSQL_CS_METHOD = null;
            LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
        }
    }



    #endregion

    #region GET MS_CS_ATTRIBUTES LIST AREA
    public List<MSSQL_CS_ATTRIBUTE> GET_MSSQL_CS_ATTRIBUTES_TABLE(string SERVER_NAME, string DATABASE_NAME, int OBJECT_ID)
        {
            _SQL_WHERE = T3 + "WHERE " + Environment.NewLine;

            int colCntPK = 0;
            List<MSSQL_CS_ATTRIBUTE> LST = new List<MSSQL_CS_ATTRIBUTE>();
            MSSQL_CS_META_DATA db = new MSSQL_CS_META_DATA();
            db.ERROR_EVENT += new EVENT_HANDLER(WriteErrorLog);
            //  db.INFORMATION_EVENT_VERBOSE += new EVENT_HANDLER(WriteLog);
            DataSet DS_COLUMNS = db.GET_COLUMNS_IN_TABLE_DS(SERVER_NAME, DATABASE_NAME, OBJECT_ID);
            DataSet DS_PRIMARY_KEYS = db.GET_PRIMARY_KEYS_IN_TABLE_DS(SERVER_NAME, DATABASE_NAME, OBJECT_ID);
            colCntPK = DS_PRIMARY_KEYS.Tables[0].Rows.Count;
            if (colCntPK > 0)
            {
                _PK = true;

            }
            string OBJECT_NAME = null;
            try
            {

                foreach (DataRow R in DS_COLUMNS.Tables[0].Rows)
                {
                    MSSQL_CS_ATTRIBUTE o = new MSSQL_CS_ATTRIBUTE
                    {
                        COLUMN_ID = Convert.ToInt32(R["COLUMN_ID"]),
                        COLUMN_NAME = R["COLUMN_NAME"].ToString().ToUpper(),
                        CS_PARM_STRING = "cmd.Parameters.AddWithValue(" + Q + "@" + R["COLUMN_NAME"].ToString().ToUpper() + Q + "," + "o" + "." + R["COLUMN_NAME"].ToString().ToUpper() + ");",
                        DATABASE_NAME = DATABASE_NAME,

                        MSSQL_DATA_TYPE = R["TYPE_NAME"].ToString().ToUpper(),
                        MSSQL_IDENTITY_FIELD_YN = R["IS_IDENTITY"].ToString(),
                        MSSQL_IDENT_INCR = Convert.ToInt32(R["IDENT_INCR"]),
                        MSSQL_IDENT_SEED = Convert.ToInt32(R["IDENT_SEED"]),
                        MSSQL_IS_NULLABLE = R["IS_NULLABLE"].ToString(),
                        MSSQL_MAX_LENGTH = Convert.ToInt32(R["MAX_LENGTH"]),
                        MSSQL_PARM_STRING = "@" + R["COLUMN_NAME"].ToString().ToUpper(),
                        MSSQL_PROC_PARM_STRING = R["TYPE_NAME"].ToString().ToUpper(),
                        MSSQL_PRECISION = Convert.ToInt32(R["PRECISION"]),
                        MSSQL_SCALE = Convert.ToInt32(R["SCALE"]),
                        MSSQL_TABLE_SCHEMA = R["TABLE_SCHEMA"].ToString().ToUpper(),
                        OBJECT_ID = OBJECT_ID,
                        OBJECT_NAME = R["TABLE_NAME"].ToString().ToUpper(),
                        OBJECT_TYPE = "TABLE",
                        PK_YN = "N",
                        SERVER_NAME = SERVER_NAME,
                        INSERT_YN = "Y",
                        UPDATE_YN = "Y",
                        RESERVED_YN = "N",
                        CS_DATA_TYPE = "?",
                        CS_PRIVATE_VARIABLE_AND_TYPE = "?",
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
                    switch (o.MSSQL_DATA_TYPE.ToUpper())
                    {
                        case "VARCHAR":
                            o.MSSQL_PROC_PARM_STRING = o.MSSQL_DATA_TYPE + "(" + o.MSSQL_MAX_LENGTH + ")";
                            break;
                        case "CHAR":
                            o.MSSQL_PROC_PARM_STRING = o.MSSQL_DATA_TYPE + "(" + o.MSSQL_MAX_LENGTH + ")";
                            break;
                        case "DECIMAL":
                            o.MSSQL_PROC_PARM_STRING = o.MSSQL_DATA_TYPE + "(" + o.MSSQL_PRECISION + "," + o.MSSQL_SCALE + ")";
                            break;
                    }
                    if (o.MSSQL_IDENTITY_FIELD_YN == "1")
                    {
                        o.UPDATE_YN = "N";
                        o.INSERT_YN = "N";
                        o.MSSQL_IDENTITY_FIELD_YN = "Y";
                    }
                    else
                    {
                        o.MSSQL_IDENTITY_FIELD_YN = "N";
                    }
                    if (Convert.ToBoolean(o.MSSQL_IS_NULLABLE))
                    {
                        o.MSSQL_IS_NULLABLE = "Y";
                    }
                    else
                    {
                        o.MSSQL_IS_NULLABLE = "N";
                    }

                    if (o.COLUMN_NAME == "INSDT" || o.COLUMN_NAME == "UPDDT")
                    {
                        o.CS_PARM_STRING = "///// " + o.CS_PARM_STRING + " RESERVED COLUMN NAME AUTO GENERATED BY SQL SERVER";
                        o.RESERVED_YN = "Y";
                        o.MSSQL_PARM_STRING = "GETDATE()";
                    }
                    if (o.COLUMN_NAME == "INSOPID" || o.COLUMN_NAME == "UPDOPID")
                    {
                        o.CS_PARM_STRING = "///// " + o.CS_PARM_STRING + " RESERVED COLUMN NAME AUTO GENERATED BY SQL SERVER";
                        o.RESERVED_YN = "Y";
                        o.MSSQL_PARM_STRING = "USER_NAME()";
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





                    LST.Add(o);
                    o = null;
                }
                DataTable DT_SCHEMA = db.GET_SCHEMA_TABLE_DT(SERVER_NAME, DATABASE_NAME, OBJECT_NAME);
                foreach (DataRow R in DT_SCHEMA.Rows)
                {
                    foreach (MSSQL_CS_ATTRIBUTE o in LST)
                    {
                        if (o.COLUMN_NAME.ToString().ToUpper() == R["ColumnName"].ToString().ToUpper())
                        {
                            o.CS_DATA_TYPE = R["DataType"].ToString();
                            o.CS_PRIVATE_VARIABLE_AND_TYPE = "private " + R["DataType"].ToString() + " _" + o.COLUMN_NAME.ToString().ToUpper() + ";" + Environment.NewLine;
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
                    foreach (MSSQL_CS_ATTRIBUTE o in LST)
                    {
                        i++;
                        if (o.PK_YN == "Y")
                        {
                            if (i < colCntPK)
                            {
                                _SQL_WHERE += T3 + o.COLUMN_NAME.ToString().ToUpper() + " = " + o.MSSQL_PARM_STRING.ToString().ToUpper() + " AND " + Environment.NewLine;
                            }
                            else

                            {
                                _SQL_WHERE += T3 + o.COLUMN_NAME.ToString().ToUpper() + " = " + o.MSSQL_PARM_STRING.ToString().ToUpper();
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
        public List<MSSQL_CS_ATTRIBUTE> GET_MSSQL_CS_ATTRIBUTES_VIEW(string SERVER_NAME, string DATABASE_NAME, int OBJECT_ID)
        {


            List<MSSQL_CS_ATTRIBUTE> LST = new List<MSSQL_CS_ATTRIBUTE>();
            MSSQL_CS_META_DATA db = new MSSQL_CS_META_DATA();
            db.ERROR_EVENT += new EVENT_HANDLER(WriteErrorLog);
            //  db.INFORMATION_EVENT_VERBOSE += new EVENT_HANDLER(WriteLog);
            DataSet DS_COLUMNS = db.GET_COLUMNS_IN_VIEW_DS(SERVER_NAME, DATABASE_NAME, OBJECT_ID);



            string OBJECT_NAME = null;
            try
            {

                foreach (DataRow R in DS_COLUMNS.Tables[0].Rows)
                {
                    MSSQL_CS_ATTRIBUTE o = new MSSQL_CS_ATTRIBUTE
                    {
                        COLUMN_ID = Convert.ToInt32(R["COLUMN_ID"]),
                        COLUMN_NAME = R["COLUMN_NAME"].ToString().ToUpper(),
                        CS_PARM_STRING = "cmd.Parameters.AddWithValue(" + Q + "@" + R["COLUMN_NAME"].ToString().ToUpper() + Q + "," + "o" + "." + R["COLUMN_NAME"].ToString().ToUpper() + ");",
                        DATABASE_NAME = DATABASE_NAME,
                        INSERT_YN = "N",
                        MSSQL_DATA_TYPE = R["TYPE_NAME"].ToString().ToUpper(),
                        MSSQL_IDENTITY_FIELD_YN = "N",
                        MSSQL_IDENT_INCR = 0,
                        MSSQL_IDENT_SEED = 0,
                        MSSQL_IS_NULLABLE = R["IS_NULLABLE"].ToString(),
                        MSSQL_MAX_LENGTH = Convert.ToInt32(R["MAX_LENGTH"]),
                        MSSQL_PARM_STRING = "@" + R["COLUMN_NAME"].ToString().ToUpper(),
                        MSSQL_PRECISION = Convert.ToInt32(R["PRECISION"]),
                        MSSQL_SCALE = Convert.ToInt32(R["SCALE"]),
                        MSSQL_PROC_PARM_STRING = R["TYPE_NAME"].ToString().ToUpper(),
                        MSSQL_TABLE_SCHEMA = R["TABLE_SCHEMA"].ToString().ToUpper(),
                        OBJECT_ID = OBJECT_ID,
                        OBJECT_NAME = R["VIEW_NAME"].ToString().ToUpper(),
                        OBJECT_TYPE = "VIEW",
                        PK_YN = "N",
                        SERVER_NAME = SERVER_NAME,
                        UPDATE_YN = "Y",
                        CS_DATA_TYPE = "?",
                        CS_PRIVATE_VARIABLE_AND_TYPE = "?",
                        CS_PUBLIC_PROPERTY = "?"
                    };
                    OBJECT_NAME = o.OBJECT_NAME;



                    if (o.COLUMN_NAME == "INSDT" || o.COLUMN_NAME == "UPDDT")
                    {
                        o.CS_PARM_STRING = "///// RESERVED COLUMN NAME";
                        o.MSSQL_PARM_STRING = "GETDATE()";
                    }
                    if (o.COLUMN_NAME == "INSOPID" || o.COLUMN_NAME == "UPDOPID")
                    {
                        o.CS_PARM_STRING = "///// RESERVED COLUMN NAME";
                        o.MSSQL_PARM_STRING = "USER_NAME()";
                    }







                    LST.Add(o);
                    o = null;
                }
                DataTable DT_SCHEMA = db.GET_SCHEMA_TABLE_DT(SERVER_NAME, DATABASE_NAME, OBJECT_NAME);
                foreach (DataRow R in DT_SCHEMA.Rows)
                {
                    foreach (MSSQL_CS_ATTRIBUTE o in LST)
                    {
                        if (o.COLUMN_NAME.ToString().ToUpper() == R["ColumnName"].ToString().ToUpper())
                        {
                            o.CS_DATA_TYPE = R["DataType"].ToString();
                            o.CS_PRIVATE_VARIABLE_AND_TYPE = "private " + R["DataType"].ToString() + " _" + o.COLUMN_NAME.ToString().ToUpper() + ";" + Environment.NewLine;
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
                // MessageBox.Show(ex.Message);
                return null;

            }
        }
        #endregion
        #region LOG AREA
        private void WriteErrorLog(object source, EVENT_HANDLER_SUPER e)
        {
            FileStream FileStream = new FileStream(LOG_FOLDER + ERR_LOG_FILE, FileMode.Append, FileAccess.Write);
            StreamWriter StreamWriter = new StreamWriter(FileStream);
            StreamWriter.WriteLine(e.GET_EVENT_MESSAGE() + Environment.NewLine);
            StreamWriter.Close();
            FileStream = null;
            StreamWriter = null;
        }
        private void WriteLog(object source, EVENT_HANDLER_SUPER e)
        {
            FileStream FileStream = new FileStream(LOG_FOLDER + LOG_FILE, FileMode.Append, FileAccess.Write);
            StreamWriter StreamWriter = new StreamWriter(FileStream);
            StreamWriter.WriteLine(e.GET_EVENT_MESSAGE() + Environment.NewLine);
            StreamWriter.Close();
            FileStream = null;
            StreamWriter = null;
        }
        #endregion


        #region STORED PROCEDURE CUSTOM
        private void TSQL_GENERATE_USP_INSERT_CUSTOM(List<MSSQL_CS_ATTRIBUTE> COLUMNS_TO_INSERT)
        {
            LIST_MSSQL_SP_MODEL = new List<MSSQL_SP_MODEL>();
            int ColCount = 0;
            int InsertCount = 0;
            int ParmCount = 0;
            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in COLUMNS_TO_INSERT)
            {
                if (ATTRIBUTE.RESERVED_YN == "N" && ATTRIBUTE.MSSQL_IDENTITY_FIELD_YN == "N")
                {
                    ParmCount++;
                    
                }
            }
            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in COLUMNS_TO_INSERT)
            {
                if ( ATTRIBUTE.MSSQL_IDENTITY_FIELD_YN == "N")
                {
                    InsertCount++;
                }
            }
            string SP_PREFIX = "usp_";
            
            MSSQL_SP_MODEL m = new MSSQL_SP_MODEL
            {
                SP_NAME = SP_PREFIX+ "INS_" + OBJECT_NAME.ToUpper() + "_CUSTOM",
                SP_FILE_NAME = SP_PREFIX + "INS_" + OBJECT_NAME.ToUpper()  + "_CUSTOM"  + ".sql",
                SP_TYPE = MSSQL_SP_MODEL.SQL_STORED_PROCEDURE_TYPE.INSERT_CUSTOM,
                SP_FILE_FOLDER = @"C:\Freddie\CS\MSSQL\" + SERVER_NAME + @"\" + DATABASE_NAME + @"\SQL\",
                SP_SOURCE_CODE_BODY = "na"
            };
            if (!Directory.Exists(m.SP_FILE_FOLDER))
            {
                Directory.CreateDirectory(m.SP_FILE_FOLDER);
            }
            m.SP_SOURCE_CODE_TOP = "USE [" + DATABASE_NAME + "]" + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "------------PROC CREATED BY Freddie ON " + DateTime.Now + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "SET ANSI_NULLS ON" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "SET QUOTED_IDENTIFIER ON" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = '" + m.SP_NAME + "')" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "DROP PROCEDURE " + m.SP_NAME + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_BODY = "CREATE PROC [dbo].[" + m.SP_NAME + "]" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in COLUMNS_TO_INSERT)
            {
                if (ATTRIBUTE.RESERVED_YN == "N" && ATTRIBUTE.MSSQL_IDENTITY_FIELD_YN == "N")
                {
                    ColCount++;
                    if (ColCount < ParmCount)
                    {
                        m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.MSSQL_PARM_STRING + " " + ATTRIBUTE.MSSQL_PROC_PARM_STRING + "," + Environment.NewLine;
                    }
                    else
                    {
                        m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.MSSQL_PARM_STRING + " " + ATTRIBUTE.MSSQL_PROC_PARM_STRING + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                    }
                }
            }
            m.SP_SOURCE_CODE_BODY += T3 + "AS" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_BODY += T3 + "INSERT " + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_BODY += T3 + "INTO dbo." + OBJECT_NAME.ToUpper() + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_BODY += T3 + "( " + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            ColCount = 0;
            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in COLUMNS_TO_INSERT)
            {
                ColCount++;
                if (ColCount < COLUMNS_TO_INSERT.Count)
                {
                    m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.COLUMN_NAME + "," + Environment.NewLine;
                }
                else
                {
                    m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.COLUMN_NAME   + Environment.NewLine;
                }
            }
            m.SP_SOURCE_CODE_BODY += T3 + ") " + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_BODY += T3 + " VALUES " + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_BODY += T3 + "( " + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            ColCount = 0;
            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in COLUMNS_TO_INSERT)
            {
                ColCount++;
                if (ColCount < COLUMNS_TO_INSERT.Count)
                {
                    m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.MSSQL_PARM_STRING + "," + Environment.NewLine;
                }
                else
                {
                    m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.MSSQL_PARM_STRING + Environment.NewLine;
                }
            }
            m.SP_SOURCE_CODE_BODY += T3 + ") " + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            if (SAVE_SOURCE_CODE_TO_FILE)
            {
                SAVE_FILE(
                    m.SP_FILE_FOLDER,
                    m.SP_FILE_NAME,
                    m.SP_SOURCE_CODE_TOP+   m.SP_SOURCE_CODE_BODY,
                    m.SP_NAME);
            }
            LIST_MSSQL_SP_MODEL.Add(m);
            m = null;
        }
        private void TSQL_GENERATE_USP_DELETE_CUSTOM ( List<MSSQL_CS_ATTRIBUTE> COLUMNS_IN_WHERE_CLAUSE)
        {
            LIST_MSSQL_SP_MODEL = new List<MSSQL_SP_MODEL>();
            int ColCount = 0;
            int ParmCount = COLUMNS_IN_WHERE_CLAUSE.Count;
            string SP_PREFIX = null;
            if (COLUMNS_IN_WHERE_CLAUSE.Count == 0)
            {
                SP_PREFIX = "_CUSTOM";
            }
            else
            {
                if (COLUMNS_IN_WHERE_CLAUSE.Count < 3)
                {
                    SP_PREFIX = "_BY_";
                    for (int i = 0; i < COLUMNS_IN_WHERE_CLAUSE.Count; i++)
                    {
                        if (i < COLUMNS_IN_WHERE_CLAUSE.Count - 1)
                        {

                            SP_PREFIX += COLUMNS_IN_WHERE_CLAUSE[i].COLUMN_NAME + "_";
                        }
                        else
                        {
                            SP_PREFIX += COLUMNS_IN_WHERE_CLAUSE[i].COLUMN_NAME;
                        }
                    }
                }
                else
                {
                    SP_PREFIX = "_BY_" + COLUMNS_IN_WHERE_CLAUSE.Count + "_COLUMNS_CUSTOM";
                }
            }



            MSSQL_SP_MODEL m = new MSSQL_SP_MODEL
            {
                SP_NAME = UTL_GENERATE_CUSTOM_STORED_PROCEDURE_NAME(OBJECT_NAME.ToUpper(), "DEL", SP_PREFIX),
                SP_FILE_NAME = UTL_GENERATE_CUSTOM_STORED_PROCEDURE_NAME(OBJECT_NAME.ToUpper(), "DEL", SP_PREFIX) + ".sql",
                SP_TYPE = MSSQL_SP_MODEL.SQL_STORED_PROCEDURE_TYPE.DELETE_CUSTOM,
                SP_FILE_FOLDER = @"C:\Freddie\CS\MSSQL\" + SERVER_NAME + @"\" + DATABASE_NAME + @"\SQL\",
                SP_SOURCE_CODE_BODY = "na"
            };
            if (!Directory.Exists(m.SP_FILE_FOLDER))
            {
                Directory.CreateDirectory(m.SP_FILE_FOLDER);
            }

          

            m.SP_SOURCE_CODE_TOP = "USE [" + DATABASE_NAME + "]" + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "------------PROC CREATED BY Freddie ON " + DateTime.Now + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "SET ANSI_NULLS ON" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "SET QUOTED_IDENTIFIER ON" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = '" + m.SP_NAME + "')" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "DROP PROCEDURE " + m.SP_NAME + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;






            m.SP_SOURCE_CODE_BODY = "CREATE PROC [dbo].[" + m.SP_NAME + "]" + Environment.NewLine + Environment.NewLine + Environment.NewLine;

            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in COLUMNS_IN_WHERE_CLAUSE)
            {

                ColCount++;
                if (ColCount < COLUMNS_IN_WHERE_CLAUSE.Count)
                {
                    m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.MSSQL_PARM_STRING + " " + ATTRIBUTE.MSSQL_PROC_PARM_STRING + "," + Environment.NewLine;
                }
                else
                {
                    m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.MSSQL_PARM_STRING + " " + ATTRIBUTE.MSSQL_PROC_PARM_STRING + Environment.NewLine;
                }
            }

            m.SP_SOURCE_CODE_BODY += T3 + "AS" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_BODY += T3 + "DELETE " + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_BODY += T3 + "FROM dbo." + OBJECT_NAME.ToUpper() + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_BODY += T3 + "WHERE " + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            ColCount = 0;
            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in COLUMNS_IN_WHERE_CLAUSE)
            {
                ColCount++;
                if (ColCount < COLUMNS_IN_WHERE_CLAUSE.Count)
                {
                    m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.MSSQL_PARM_STRING + " AND" + Environment.NewLine;
                }
                else
                {
                    m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.MSSQL_PARM_STRING + Environment.NewLine;
                }
            }
            if (SAVE_SOURCE_CODE_TO_FILE)
            {
                SAVE_FILE(
                    m.SP_FILE_FOLDER,
                    m.SP_FILE_NAME,
                    m.SP_SOURCE_CODE_TOP + m.SP_SOURCE_CODE_BODY,
                    m.SP_NAME);
            }
            LIST_MSSQL_SP_MODEL.Add(m);
            m = null;
        }
        private void TSQL_GENERATE_USP_SELECT_DISTINCT_CUSTOM(List<MSSQL_CS_ATTRIBUTE> DISTINCT_COLUMN, List<MSSQL_CS_ATTRIBUTE> COLUMNS_IN_WHERE_CLAUSE)
        {
            LIST_MSSQL_SP_MODEL = new List<MSSQL_SP_MODEL>();
            int ColCount = 0;
            int ParmCount = COLUMNS_IN_WHERE_CLAUSE.Count;
            string SP_PREFIX = null;
            String DISTINCT_COLUMN_NAME = DISTINCT_COLUMN[0].COLUMN_NAME;
            if (COLUMNS_IN_WHERE_CLAUSE.Count == 0)
            {
                SP_PREFIX = "_" + DISTINCT_COLUMN_NAME;

            }
            else
            {
                if (COLUMNS_IN_WHERE_CLAUSE.Count < 3)
                {

                    SP_PREFIX = "_" + DISTINCT_COLUMN_NAME + "_BY_";

                    for (int i = 0; i < COLUMNS_IN_WHERE_CLAUSE.Count; i++)
                    {
                        if (i < COLUMNS_IN_WHERE_CLAUSE.Count - 1)
                        {

                            SP_PREFIX += COLUMNS_IN_WHERE_CLAUSE[i].COLUMN_NAME + "_";

                        }
                        else
                        {

                            SP_PREFIX += COLUMNS_IN_WHERE_CLAUSE[i].COLUMN_NAME;

                        }
                    }
                }
                else
                {

                    SP_PREFIX = "_" + DISTINCT_COLUMN_NAME + "_BY_" + COLUMNS_IN_WHERE_CLAUSE.Count + "_COLUMNS";

                }
            }





            MSSQL_SP_MODEL m = new MSSQL_SP_MODEL
            {
                SP_NAME = UTL_GENERATE_CUSTOM_STORED_PROCEDURE_NAME(OBJECT_NAME.ToUpper(),"SEL_DISTINCT", SP_PREFIX),
                SP_FILE_NAME = UTL_GENERATE_CUSTOM_STORED_PROCEDURE_NAME(OBJECT_NAME.ToUpper(), "SEL_DISTINCT", SP_PREFIX) + ".sql",
                SP_TYPE = MSSQL_SP_MODEL.SQL_STORED_PROCEDURE_TYPE.SELECT_DISTINCT_CUSTOM,
                SP_FILE_FOLDER = @"C:\Freddie\CS\MSSQL\" + SERVER_NAME + @"\" + DATABASE_NAME + @"\SQL\",
                SP_SOURCE_CODE_BODY = "na"
            };
            if (!Directory.Exists(m.SP_FILE_FOLDER))
            {
                Directory.CreateDirectory(m.SP_FILE_FOLDER);
            }

            
            m.SP_SOURCE_CODE_TOP = "USE [" + DATABASE_NAME + "]" + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "------------PROC CREATED BY Freddie ON " + DateTime.Now + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "SET ANSI_NULLS ON" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "SET QUOTED_IDENTIFIER ON" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = '" + m.SP_NAME + "')" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "DROP PROCEDURE " + m.SP_NAME + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;



            m.SP_SOURCE_CODE_BODY = "CREATE PROC [dbo].[" + m.SP_SOURCE_CODE_BODY + "]" + Environment.NewLine + Environment.NewLine + Environment.NewLine;



            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in COLUMNS_IN_WHERE_CLAUSE)
            {
                ColCount++;
                if (ColCount < COLUMNS_IN_WHERE_CLAUSE.Count)
                {
                    m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.MSSQL_PARM_STRING + " " + ATTRIBUTE.MSSQL_PROC_PARM_STRING + "," + Environment.NewLine;
                }
                else
                {
                    m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.MSSQL_PARM_STRING + " " + ATTRIBUTE.MSSQL_PROC_PARM_STRING + Environment.NewLine;
                }
            }

            m.SP_SOURCE_CODE_BODY += T3 + "AS" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_BODY += T3 + "SELECT DISTINCT" + Environment.NewLine + Environment.NewLine + Environment.NewLine;

            ColCount = 0;
            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in DISTINCT_COLUMN)
            {
                ColCount++;
                if (ColCount < DISTINCT_COLUMN.Count)
                {
                    m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.COLUMN_NAME + "," + Environment.NewLine;
                }
                else
                {
                    m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.COLUMN_NAME + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                }
            }
            m.SP_SOURCE_CODE_BODY += T3 + "FROM dbo." + OBJECT_NAME.ToUpper() + Environment.NewLine + 
                Environment.NewLine + Environment.NewLine;

            if(COLUMNS_IN_WHERE_CLAUSE.Count > 0)
            {
                m.SP_SOURCE_CODE_BODY += T3 + "WHERE " + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            }
           
            ColCount = 0;
            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in COLUMNS_IN_WHERE_CLAUSE)
            {
                ColCount++;
                if (ColCount < COLUMNS_IN_WHERE_CLAUSE.Count)
                {
                    m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.MSSQL_PARM_STRING + " AND" + Environment.NewLine;
                }
                else
                {
                    m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.MSSQL_PARM_STRING + Environment.NewLine;
                }
            }
            if (SAVE_SOURCE_CODE_TO_FILE)
            {
                SAVE_FILE(
                   m.SP_FILE_FOLDER,
                    m.SP_FILE_NAME,
                    m.SP_SOURCE_CODE_TOP + m.SP_SOURCE_CODE_BODY,
                    m.SP_NAME);
            }
            LIST_MSSQL_SP_MODEL.Add(m);
            m = null;
        }
        private void TSQL_GENERATE_USP_SELECT_CUSTOM(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES, List<MSSQL_CS_ATTRIBUTE> COLUMNS_IN_WHERE_CLAUSE)
        {
            LIST_MSSQL_SP_MODEL = new List<MSSQL_SP_MODEL>();
            int ColCount = 0;
            int ParmCount = COLUMNS_IN_WHERE_CLAUSE.Count;

            string SP_PREFIX = null;

            if (COLUMNS_IN_WHERE_CLAUSE.Count == 0)
            {
                SP_PREFIX = "SEL_CUSTOM";
            }
            else
            {
                if (COLUMNS_IN_WHERE_CLAUSE.Count < 3)
                {

                    SP_PREFIX = "_BY_";
                    for (int i = 0; i < COLUMNS_IN_WHERE_CLAUSE.Count; i++)
                    {
                        if (i < COLUMNS_IN_WHERE_CLAUSE.Count - 1)
                        {

                            SP_PREFIX += COLUMNS_IN_WHERE_CLAUSE[i].COLUMN_NAME + "_";
                        }
                        else
                        {

                            SP_PREFIX += COLUMNS_IN_WHERE_CLAUSE[i].COLUMN_NAME;
                        }
                    }
                }
                else
                {

                    SP_PREFIX = "_BY_" + COLUMNS_IN_WHERE_CLAUSE.Count + "_COLUMNS_CUSTOM";
                }
            }

            MSSQL_SP_MODEL m = new MSSQL_SP_MODEL
            {
                SP_NAME = UTL_GENERATE_CUSTOM_STORED_PROCEDURE_NAME(OBJECT_NAME.ToUpper(),"SEL", SP_PREFIX),
                SP_FILE_NAME = UTL_GENERATE_CUSTOM_STORED_PROCEDURE_NAME(OBJECT_NAME.ToUpper(), "SEL", SP_PREFIX) + ".sql",
                SP_TYPE = MSSQL_SP_MODEL.SQL_STORED_PROCEDURE_TYPE.SELECT_CUSTOM,
                SP_FILE_FOLDER = @"C:\Freddie\CS\MSSQL\" + SERVER_NAME + @"\" + DATABASE_NAME + @"\SQL\",
                SP_SOURCE_CODE_BODY = "na"
            };
            if (!Directory.Exists(m.SP_FILE_FOLDER))
            {
                Directory.CreateDirectory(m.SP_FILE_FOLDER);
            }
        




           m.SP_SOURCE_CODE_TOP = "USE [" + DATABASE_NAME + "]" + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "------------PROC CREATED BY Freddie ON " + DateTime.Now + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "SET ANSI_NULLS ON" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "SET QUOTED_IDENTIFIER ON" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = '" + m.SP_NAME + "')" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "DROP PROCEDURE " + m.SP_NAME + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;

           m.SP_SOURCE_CODE_BODY= "CREATE PROC [dbo].[" + m.SP_NAME + "]" + Environment.NewLine + Environment.NewLine + Environment.NewLine;



            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in COLUMNS_IN_WHERE_CLAUSE)
            {
                
                    ColCount++;
                    if (ColCount < COLUMNS_IN_WHERE_CLAUSE.Count)
                    {
                    ///  _SQL_SELECT_BY_PK_STORED_PROCEDURE_SOURCE_CODE += T3 + ATTRIBUTE.MSSQL_PARM_STRING + " " + ATTRIBUTE.MSSQL_PROC_PARM_STRING + "," + Environment.NewLine;
                    m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.MSSQL_PARM_STRING + " " + ATTRIBUTE.MSSQL_PROC_PARM_STRING + "," + Environment.NewLine;
                    }
                    else
                    {
                    m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.MSSQL_PARM_STRING + " " + ATTRIBUTE.MSSQL_PROC_PARM_STRING + Environment.NewLine;
                    }

                 
            }

            m.SP_SOURCE_CODE_BODY += T3 + "AS" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_BODY += T3 + "SELECT " + Environment.NewLine + Environment.NewLine + Environment.NewLine;

            ColCount = 0;
            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
            {

                ColCount++;
                if (ColCount < ATTRIBUTES.Count)
                {
                    m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.COLUMN_NAME + "," + Environment.NewLine;
                }
                else
                {
                    m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.COLUMN_NAME + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                }

            }
            m.SP_SOURCE_CODE_BODY += T3 + "FROM dbo." + OBJECT_NAME.ToUpper() + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_BODY += T3 + "WHERE " + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            ColCount = 0;
            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in COLUMNS_IN_WHERE_CLAUSE)
            {
                
                    ColCount++;
                    if (ColCount < COLUMNS_IN_WHERE_CLAUSE.Count)
                    {
                        m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.MSSQL_PARM_STRING + " AND" + Environment.NewLine;
                    }
                    else
                    {
                        m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.MSSQL_PARM_STRING + Environment.NewLine;
                    }
                }

            if (SAVE_SOURCE_CODE_TO_FILE)
            {
                SAVE_FILE(
                    m.SP_FILE_FOLDER,
                    m.SP_FILE_NAME,
                    m.SP_SOURCE_CODE_TOP + m.SP_SOURCE_CODE_BODY,
                    m.SP_NAME);
            }
            LIST_MSSQL_SP_MODEL.Add(m);
            m = null;


        }
        private void TSQL_GENERATE_USP_UPADATE_CUSTOM(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES, List<MSSQL_CS_ATTRIBUTE> COLUMNS_IN_WHERE_CLAUSE)
        {


            LIST_MSSQL_SP_MODEL = new List<MSSQL_SP_MODEL>();
            int ColCount = 0;
            int ParmCount = COLUMNS_IN_WHERE_CLAUSE.Count + ATTRIBUTES.Count ;
            int UpdateColumnCount = 0;
            string SP_PREFIX = null;

            if (COLUMNS_IN_WHERE_CLAUSE.Count == 0)
            {
                SP_PREFIX = "UPD_CUSTOM";
            }
            else
            {
                if (COLUMNS_IN_WHERE_CLAUSE.Count < 3)
                {

                    SP_PREFIX = "_BY_";
                    for (int i = 0; i < COLUMNS_IN_WHERE_CLAUSE.Count; i++)
                    {
                        if (i < COLUMNS_IN_WHERE_CLAUSE.Count - 1)
                        {

                            SP_PREFIX += COLUMNS_IN_WHERE_CLAUSE[i].COLUMN_NAME + "_";
                        }
                        else
                        {

                            SP_PREFIX += COLUMNS_IN_WHERE_CLAUSE[i].COLUMN_NAME;
                        }
                    }
                }
                else
                {

                    SP_PREFIX = "_BY_" + COLUMNS_IN_WHERE_CLAUSE.Count + "_COLUMNS_CUSTOM";
                }
            }





            MSSQL_SP_MODEL m = new MSSQL_SP_MODEL
            {
                SP_NAME = UTL_GENERATE_CUSTOM_STORED_PROCEDURE_NAME(OBJECT_NAME.ToUpper(),"UPD", SP_PREFIX),
                SP_FILE_NAME = UTL_GENERATE_CUSTOM_STORED_PROCEDURE_NAME(OBJECT_NAME.ToUpper(),"UPD", SP_PREFIX) + ".sql",
                SP_TYPE = MSSQL_SP_MODEL.SQL_STORED_PROCEDURE_TYPE.UPDATE_CUSTOM,
                SP_FILE_FOLDER = @"C:\Freddie\CS\MSSQL\" + SERVER_NAME + @"\" + DATABASE_NAME + @"\SQL\",
                SP_SOURCE_CODE_BODY = "na"
            };
            if (!Directory.Exists(m.SP_FILE_FOLDER))
            {
                Directory.CreateDirectory(m.SP_FILE_FOLDER);
            }





            m.SP_SOURCE_CODE_TOP = "USE [" + DATABASE_NAME + "]" + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "------------PROC CREATED BY Freddie ON " + DateTime.Now + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "SET ANSI_NULLS ON" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "SET QUOTED_IDENTIFIER ON" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = '" + m.SP_NAME + "')" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "DROP PROCEDURE " + m.SP_NAME + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;

            m.SP_SOURCE_CODE_BODY = "CREATE PROC [dbo].[" + m.SP_NAME + "]" + Environment.NewLine + Environment.NewLine + Environment.NewLine;


            m.SP_SOURCE_CODE_BODY += T3 + "/////////// columns to update ////////////" + Environment.NewLine;
            ColCount = 0;
            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
            {

                ColCount++;
                if (ColCount < ParmCount)
                {
                    ///  _SQL_SELECT_BY_PK_STORED_PROCEDURE_SOURCE_CODE += T3 + ATTRIBUTE.MSSQL_PARM_STRING + " " + ATTRIBUTE.MSSQL_PROC_PARM_STRING + "," + Environment.NewLine;
                    m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.MSSQL_PARM_STRING + " " + ATTRIBUTE.MSSQL_PROC_PARM_STRING + "," + Environment.NewLine;
                }
                else
                {
                    m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.MSSQL_PARM_STRING + " " + ATTRIBUTE.MSSQL_PROC_PARM_STRING + Environment.NewLine;
                }
            }

            m.SP_SOURCE_CODE_BODY += Environment.NewLine + Environment.NewLine + T3+"/////////// columns in where clause ////////////" + Environment.NewLine;


            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in COLUMNS_IN_WHERE_CLAUSE)
            {

                ColCount++;
                if (ColCount < ParmCount)
                {
                    ///  _SQL_SELECT_BY_PK_STORED_PROCEDURE_SOURCE_CODE += T3 + ATTRIBUTE.MSSQL_PARM_STRING + " " + ATTRIBUTE.MSSQL_PROC_PARM_STRING + "," + Environment.NewLine;
                    m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.MSSQL_PARM_STRING + " " + ATTRIBUTE.MSSQL_PROC_PARM_STRING + "," + Environment.NewLine;
                }
                else
                {
                    m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.MSSQL_PARM_STRING + " " + ATTRIBUTE.MSSQL_PROC_PARM_STRING + Environment.NewLine;
                }
            }
            m.SP_SOURCE_CODE_BODY += Environment.NewLine +Environment.NewLine + T3 + "AS" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
         
            m.SP_SOURCE_CODE_BODY += T3 + "UPDATE dbo." + OBJECT_NAME.ToUpper() + Environment.NewLine + Environment.NewLine + T3 + "SET" + Environment.NewLine + Environment.NewLine;


            ColCount = 0;
            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
            {
                if (ATTRIBUTE.PK_YN == "N")
                {
                    if (ATTRIBUTE.UPDATE_YN == "Y")
                    {
                        UpdateColumnCount++;

                    }
                }
            }

            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
            {
                if (ATTRIBUTE.PK_YN == "N")
                {
                    if (ATTRIBUTE.UPDATE_YN == "Y")
                    {
                        ColCount++;
                        if (ColCount < UpdateColumnCount)
                        {
                            m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.MSSQL_PARM_STRING + "," + Environment.NewLine;
                        }
                        else
                        {
                            m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.MSSQL_PARM_STRING + Environment.NewLine + Environment.NewLine;
                        }
                    }
                }
            }
            m.SP_SOURCE_CODE_BODY += T3 + "WHERE" + Environment.NewLine + Environment.NewLine;

            ColCount = 0;
            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in COLUMNS_IN_WHERE_CLAUSE)
            {
                
                    ColCount++;
                    if (ColCount < COLUMNS_IN_WHERE_CLAUSE.Count)
                    {
                        m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.MSSQL_PARM_STRING + " AND" + Environment.NewLine;
                    }
                    else
                    {
                        m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.MSSQL_PARM_STRING + Environment.NewLine;
                    }
                
            }



            if (SAVE_SOURCE_CODE_TO_FILE)
            {
                SAVE_FILE(
                    m.SP_FILE_FOLDER,
                    m.SP_FILE_NAME,
                    m.SP_SOURCE_CODE_TOP + m.SP_SOURCE_CODE_BODY,
                    m.SP_NAME);
            }
            LIST_MSSQL_SP_MODEL.Add(m);
            m = null;



        }/// END OF CUSTOM UPDATE SQL
        #endregion




        #region STORED PROCEDURE  DEFAULT
        private void TSQL_GENERATE_USP_SELECT_BY_PK(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES)
        {
            int ColCount = 0;
            int PKCount = 0;
            MSSQL_SP_MODEL m = new MSSQL_SP_MODEL
            {
                SP_NAME = UTL_GENERATE_STORED_PROCEDURE_NAME(OBJECT_NAME.ToUpper(), "SEL_BY_PK"),
                SP_FILE_NAME = UTL_GENERATE_STORED_PROCEDURE_NAME(OBJECT_NAME.ToUpper(), "SEL_BY_PK") + ".sql",
                SP_TYPE = MSSQL_SP_MODEL.SQL_STORED_PROCEDURE_TYPE.SELECT_BY_PK,
                SP_FILE_FOLDER = @"C:\Freddie\CS\MSSQL\" + SERVER_NAME + @"\" + DATABASE_NAME + @"\SQL\",
                SP_SOURCE_CODE_BODY = "na"
            };
            if (!Directory.Exists(m.SP_FILE_FOLDER))
            {
                Directory.CreateDirectory(m.SP_FILE_FOLDER);
            }
            if(PK)
            {
                m.SP_SOURCE_CODE_TOP = "USE [" + DATABASE_NAME + "]" + Environment.NewLine;
                m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;
                m.SP_SOURCE_CODE_TOP += "------------PROC CREATED BY Freddie ON " + DateTime.Now + Environment.NewLine + Environment.NewLine;
                m.SP_SOURCE_CODE_TOP += "SET ANSI_NULLS ON" + Environment.NewLine + Environment.NewLine;
                m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;
                m.SP_SOURCE_CODE_TOP += "SET QUOTED_IDENTIFIER ON" + Environment.NewLine + Environment.NewLine;
                m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;

                m.SP_SOURCE_CODE_TOP += "IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = '" + m.SP_NAME + "')" + Environment.NewLine + Environment.NewLine;
                m.SP_SOURCE_CODE_TOP += "DROP PROCEDURE " + m.SP_NAME + Environment.NewLine + Environment.NewLine;
                m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;

                //////////////////////////////   BODY OF PROC STARTS HERE ///////////////////////////////////////////////////////////////////////////////////////////
                m.SP_SOURCE_CODE_BODY = "CREATE PROC [dbo].[" + m.SP_NAME + "]" + Environment.NewLine + Environment.NewLine + Environment.NewLine;


                foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
                {
                    if (ATTRIBUTE.PK_YN == "Y")
                    {
                        PKCount++;

                    }
                }
                foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
                {
                    if (ATTRIBUTE.PK_YN == "Y")
                    {
                        ColCount++;
                        if (ColCount < PKCount)
                        {
                            m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.MSSQL_PARM_STRING + " " + ATTRIBUTE.MSSQL_PROC_PARM_STRING + "," + Environment.NewLine;
                        }
                        else
                        {
                            m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.MSSQL_PARM_STRING + " " + ATTRIBUTE.MSSQL_PROC_PARM_STRING + Environment.NewLine;
                        }

                    }
                }

                m.SP_SOURCE_CODE_BODY += T3 + "AS" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                m.SP_SOURCE_CODE_BODY += T3 + "SELECT " + Environment.NewLine + Environment.NewLine + Environment.NewLine;

                ColCount = 0;
                foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
                {

                    ColCount++;
                    if (ColCount < ATTRIBUTES.Count)
                    {
                        m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.COLUMN_NAME + "," + Environment.NewLine;
                    }
                    else
                    {
                        m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.COLUMN_NAME + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                    }

                }
                m.SP_SOURCE_CODE_BODY += T3 + "FROM dbo." + OBJECT_NAME.ToUpper() + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                m.SP_SOURCE_CODE_BODY += T3 + "WHERE " + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                ColCount = 0;
                foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
                {
                    if (ATTRIBUTE.PK_YN == "Y")
                    {
                        ColCount++;
                        if (ColCount < PKCount)
                        {
                            m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.MSSQL_PARM_STRING + " AND" + Environment.NewLine;
                        }
                        else
                        {
                            m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.MSSQL_PARM_STRING + Environment.NewLine;
                        }
                    }
                }
                if (SAVE_SOURCE_CODE_TO_FILE)
                {
                    SAVE_FILE(
                        m.SP_FILE_FOLDER,
                        m.SP_FILE_NAME,
                        m.SP_SOURCE_CODE_TOP + m.SP_SOURCE_CODE_BODY,
                        m.SP_NAME);
                }
                m.SP_EXISTS = UTL_DOES_STORED_PROCEDURE_EXIST(SERVER_NAME, DATABASE_NAME, m.SP_NAME);
            }
          
           LIST_MSSQL_SP_MODEL.Add(m);
            m = null;

        }
        private void TSQL_GENERATE_USP_SELECT_ALL(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES)
        {
            int ColCount = 0;
            MSSQL_SP_MODEL m = new MSSQL_SP_MODEL
            {
                SP_NAME = UTL_GENERATE_STORED_PROCEDURE_NAME(OBJECT_NAME.ToUpper(), "SEL_ALL"),
                SP_FILE_NAME = UTL_GENERATE_STORED_PROCEDURE_NAME(OBJECT_NAME.ToUpper(), "SEL_ALL") + ".sql",
                SP_TYPE = MSSQL_SP_MODEL.SQL_STORED_PROCEDURE_TYPE.SELECT_ALL,
                SP_FILE_FOLDER = @"C:\Freddie\CS\MSSQL\" + SERVER_NAME + @"\" + DATABASE_NAME + @"\SQL\"
            };
            if (!Directory.Exists(m.SP_FILE_FOLDER))
            {
                Directory.CreateDirectory(m.SP_FILE_FOLDER);
            }

            m.SP_SOURCE_CODE_TOP = "USE [" + DATABASE_NAME + "]" + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "------------PROC CREATED BY Freddie ON " + DateTime.Now + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "SET ANSI_NULLS ON" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "SET QUOTED_IDENTIFIER ON" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;

            m.SP_SOURCE_CODE_TOP += "IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = '" + m.SP_NAME + "')" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "DROP PROCEDURE " + m.SP_NAME + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;

            m.SP_SOURCE_CODE_BODY = "CREATE PROC [dbo].[" + m.SP_NAME + "]" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_BODY += T3 + "AS" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_BODY += T3 + "SELECT " + Environment.NewLine + Environment.NewLine + Environment.NewLine;

            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
            {
                ColCount++;
                if (ColCount < ATTRIBUTES.Count)
                {
                    m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.COLUMN_NAME + "," + Environment.NewLine;
                }
                else
                {
                    m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.COLUMN_NAME + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                }
            }
            m.SP_SOURCE_CODE_BODY += T3 + "FROM dbo." + OBJECT_NAME.ToUpper() + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            ///    _SQL_SELECT_ALL_STORED_PROCEDURE_SOURCE_CODE += "------------END OF STORED PROCEDURE " + Environment.NewLine + Environment.NewLine;
            if (SAVE_SOURCE_CODE_TO_FILE)
            {
                SAVE_FILE(
                    m.SP_FILE_FOLDER,
                    m.SP_FILE_NAME,
                    m.SP_SOURCE_CODE_TOP +   m.SP_SOURCE_CODE_BODY,
                    m.SP_NAME);
            }
            m.SP_EXISTS = UTL_DOES_STORED_PROCEDURE_EXIST( SERVER_NAME,  DATABASE_NAME, m.SP_NAME);
            LIST_MSSQL_SP_MODEL.Add(m);
            m = null;

        }
        private void TSQL_GENERATE_USP_INSERT(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES)
        {
            int ColCount = 0;
            int ParmCount = 0;
            int InsertColumnCount = 0;
            MSSQL_SP_MODEL m = new MSSQL_SP_MODEL
            {
                SP_NAME = UTL_GENERATE_STORED_PROCEDURE_NAME(OBJECT_NAME.ToUpper(), "INS"),
                SP_FILE_NAME = UTL_GENERATE_STORED_PROCEDURE_NAME(OBJECT_NAME.ToUpper(), "INS") + ".sql",
                SP_TYPE = MSSQL_SP_MODEL.SQL_STORED_PROCEDURE_TYPE.INSERT_A_ROW,
                SP_FILE_FOLDER = @"C:\Freddie\CS\MSSQL\" + SERVER_NAME + @"\" + DATABASE_NAME + @"\SQL\",
                SP_SOURCE_CODE_BODY = "na"
            };
            if (!Directory.Exists(m.SP_FILE_FOLDER))
            {
                Directory.CreateDirectory(m.SP_FILE_FOLDER);
            }

 



            m.SP_SOURCE_CODE_TOP = "USE [" + DATABASE_NAME + "]" + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "------------PROC CREATED BY Freddie ON " + DateTime.Now + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "SET ANSI_NULLS ON" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "SET QUOTED_IDENTIFIER ON" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;

            m.SP_SOURCE_CODE_TOP += "IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = '" + m.SP_NAME + "')" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "DROP PROCEDURE " + m.SP_NAME + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;




            m.SP_SOURCE_CODE_BODY = "CREATE PROC [dbo].[" + m.SP_NAME + "]" + Environment.NewLine + Environment.NewLine + Environment.NewLine;

            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
            {


                if (ATTRIBUTE.RESERVED_YN == "N" && ATTRIBUTE.MSSQL_IDENTITY_FIELD_YN == "N")
                {

                    ParmCount++;
                }


            }



            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
            {


                if (ATTRIBUTE.INSERT_YN == "Y")
                {

                    InsertColumnCount++;
                }


            }

            ColCount = 0;
            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
            {
                if (ATTRIBUTE.RESERVED_YN == "N" && ATTRIBUTE.MSSQL_IDENTITY_FIELD_YN == "N")
                {
                    ColCount++;
                    if (ColCount < ParmCount)
                    {
                        m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.MSSQL_PARM_STRING + " " + ATTRIBUTE.MSSQL_PROC_PARM_STRING + "," + Environment.NewLine;
                    }
                    else
                    {
                        m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.MSSQL_PARM_STRING + " " + ATTRIBUTE.MSSQL_PROC_PARM_STRING + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                    }
                }
            }
            ColCount = 0;
            m.SP_SOURCE_CODE_BODY += T3 + "AS" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_BODY += T3 + "INSERT INTO dbo." + OBJECT_NAME.ToUpper() + "(" + Environment.NewLine + Environment.NewLine + Environment.NewLine;

            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
            {
                if (ATTRIBUTE.INSERT_YN == "Y")
                {
                    ColCount++;
                    if (ColCount < InsertColumnCount)
                    {
                        m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.COLUMN_NAME + "," + Environment.NewLine;
                    }
                    else
                    {
                        m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.COLUMN_NAME + Environment.NewLine;
                    }
                }
            }
            m.SP_SOURCE_CODE_BODY += Environment.NewLine + Environment.NewLine + Environment.NewLine;
            ColCount = 0;

            m.SP_SOURCE_CODE_BODY += T3 + ")VALUES(" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
            {


                if (ATTRIBUTE.INSERT_YN == "Y")
                {
                    ColCount++;
                    if (ColCount < InsertColumnCount)
                    {
                        m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.MSSQL_PARM_STRING + "," + Environment.NewLine;
                    }
                    else
                    {
                        m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.MSSQL_PARM_STRING + Environment.NewLine + Environment.NewLine;
                    }
                }




            }
            ColCount = 0;

            m.SP_SOURCE_CODE_BODY += T3 + ")" + Environment.NewLine + Environment.NewLine;
          
            if(SAVE_SOURCE_CODE_TO_FILE)
            {
                SAVE_FILE(
                    m.SP_FILE_FOLDER, 
                    m.SP_FILE_NAME, 
                    m.SP_SOURCE_CODE_TOP+  m.SP_SOURCE_CODE_BODY, 
                    m.SP_NAME);
            }
            m.SP_EXISTS= UTL_DOES_STORED_PROCEDURE_EXIST( SERVER_NAME,  DATABASE_NAME, m.SP_NAME);
            LIST_MSSQL_SP_MODEL.Add(m);
            m = null;
        }
        private void TSQL_GENERATE_USP_INSERT_WHERE_NOT_EXISTS(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES)
        {
            int ColCount = 0;
            int ParmCount = 0;
            int InsertColumnCount = 0;
            int PKCount = 0;

            MSSQL_SP_MODEL m = new MSSQL_SP_MODEL
            {
                SP_NAME = UTL_GENERATE_STORED_PROCEDURE_NAME(OBJECT_NAME.ToUpper(), "INS_WHERE_NOT_EXISTS"),
                SP_FILE_NAME = UTL_GENERATE_STORED_PROCEDURE_NAME(OBJECT_NAME.ToUpper(), "INS_WHERE_NOT_EXISTS") + ".sql",
                SP_TYPE = MSSQL_SP_MODEL.SQL_STORED_PROCEDURE_TYPE.INSERT_WHERE_NOT_EXISTS,
                SP_FILE_FOLDER = @"C:\Freddie\CS\MSSQL\" + SERVER_NAME + @"\" + DATABASE_NAME + @"\SQL\",
                SP_SOURCE_CODE_BODY = "na"
            };
            if (!Directory.Exists(m.SP_FILE_FOLDER))
            {
                Directory.CreateDirectory(m.SP_FILE_FOLDER);
            }

            m.SP_SOURCE_CODE_TOP = "USE [" + DATABASE_NAME + "]" + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "------------PROC CREATED BY Freddie ON " + DateTime.Now + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "SET ANSI_NULLS ON" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "SET QUOTED_IDENTIFIER ON" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;

            m.SP_SOURCE_CODE_TOP += "IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = '" + m.SP_NAME + "')" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "DROP PROCEDURE " + m.SP_NAME + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;




            m.SP_SOURCE_CODE_BODY = "CREATE PROC [dbo].[" + m.SP_NAME + "]" + Environment.NewLine + Environment.NewLine + Environment.NewLine;


            //"IF NOT EXISTS (SELECT * FROM DBO.HRS_O_TRAININGSCHEDULED WHERE TRAINING_GUID=@TRAINING_GUID )" +
            //"BEGIN INSERT INTO DBO.HRS_O_TRAININGSCHEDULED(TRAINING_GUID,TRAININGROLE_ID,PERSON_GUID,STATUS,ONTHEBENCH,INSOPID,INSDT,
            ///UPDOPID,UPDDT) VALUES (@TRAINING_GUID,@TRAININGROLE_ID,@PERSON_GUID,@STATUS,@ONTHEBENCH,USER_NAME(),GetDate(),USER_NAME(),GetDate()) END ";



            ////get the number count for all parms being passed in to the INSERT proc
            ///The only types of fields that can't be passed in are IDENTITY fields and the following reserved field names
            ///INSDT, INOPID, UPDDT, UPDOPID
            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
            {
                if (ATTRIBUTE.RESERVED_YN == "N" && ATTRIBUTE.MSSQL_IDENTITY_FIELD_YN == "N")
                {
                    ParmCount++;
                }
            }

            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
            {
                if (ATTRIBUTE.PK_YN == "Y")
                {
                    PKCount++;

                }
            }


            ////GET THE NUMBER COUNT FOR THE COLUMNS THAT CAN BE INSERTED INTO
            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
            {
                if (ATTRIBUTE.INSERT_YN == "Y")
                {
                    InsertColumnCount++;
                }
            }

            ColCount = 0;
            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
            {
                if (ATTRIBUTE.RESERVED_YN == "N" && ATTRIBUTE.MSSQL_IDENTITY_FIELD_YN == "N")
                {
                    ColCount++;
                    if (ColCount < ParmCount)
                    {
                        m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.MSSQL_PARM_STRING + " " + ATTRIBUTE.MSSQL_PROC_PARM_STRING + "," + Environment.NewLine;
                    }
                    else
                    {
                        m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.MSSQL_PARM_STRING + " " + ATTRIBUTE.MSSQL_PROC_PARM_STRING + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                    }
                }
            }
            ColCount = 0;
            m.SP_SOURCE_CODE_BODY += T3 + "AS" + Environment.NewLine + Environment.NewLine + Environment.NewLine;


            //"IF NOT EXISTS (SELECT * FROM DBO.HRS_O_TRAININGSCHEDULED WHERE TRAINING_GUID=@TRAINING_GUID )" +

            m.SP_SOURCE_CODE_BODY += T3 + "IF NOT EXISTS (SELECT * FROM DBO." + OBJECT_NAME.ToUpper() + " WHERE " ;

            ColCount = 0;
            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
            {
                if (ATTRIBUTE.PK_YN == "Y")
                {
                    ColCount++;
                    if (ColCount < PKCount)
                    {
                        m.SP_SOURCE_CODE_BODY +=  ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.MSSQL_PARM_STRING + " AND" + Environment.NewLine;
                    }
                    else
                    {
                        m.SP_SOURCE_CODE_BODY +=  ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.MSSQL_PARM_STRING + ")" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                    }
                }
            }

            m.SP_SOURCE_CODE_BODY += T3 + "BEGIN" + Environment.NewLine + Environment.NewLine;



            m.SP_SOURCE_CODE_BODY += T4 + "INSERT INTO dbo." + OBJECT_NAME.ToUpper() + "(" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            ColCount = 0;
            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
            {
                if (ATTRIBUTE.INSERT_YN == "Y")
                {
                    ColCount++;
                    if (ColCount < InsertColumnCount)
                    {
                        m.SP_SOURCE_CODE_BODY += T5 + ATTRIBUTE.COLUMN_NAME + "," + Environment.NewLine;
                    }
                    else
                    {
                        m.SP_SOURCE_CODE_BODY += T5 + ATTRIBUTE.COLUMN_NAME + Environment.NewLine;
                    }
                }
            }
            m.SP_SOURCE_CODE_BODY += Environment.NewLine + Environment.NewLine + Environment.NewLine;
            

            m.SP_SOURCE_CODE_BODY += T5 + ")VALUES(" + Environment.NewLine + Environment.NewLine + Environment.NewLine;

            ColCount = 0;
            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
            {


                if (ATTRIBUTE.INSERT_YN == "Y")
                {
                    ColCount++;
                    if (ColCount < InsertColumnCount)
                    {
                        m.SP_SOURCE_CODE_BODY += T5 + ATTRIBUTE.MSSQL_PARM_STRING + "," + Environment.NewLine;
                    }
                    else
                    {
                        m.SP_SOURCE_CODE_BODY += T5 + ATTRIBUTE.MSSQL_PARM_STRING + Environment.NewLine + Environment.NewLine;
                    }
                }




            }
            ColCount = 0;

            m.SP_SOURCE_CODE_BODY += T5 + ")" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_BODY += T3 + "END" + Environment.NewLine + Environment.NewLine;

            if (SAVE_SOURCE_CODE_TO_FILE)

            {
                SAVE_FILE(
                    m.SP_FILE_FOLDER, 
                    m.SP_FILE_NAME, 
                    m.SP_SOURCE_CODE_TOP + m.SP_SOURCE_CODE_BODY, 
                    m.SP_NAME);
            }
            m.SP_EXISTS = UTL_DOES_STORED_PROCEDURE_EXIST(SERVER_NAME, DATABASE_NAME, m.SP_FILE_NAME);
            LIST_MSSQL_SP_MODEL.Add(m);
            m = null;


        }
        private void TSQL_GENERATE_USP_UPDATE_BY_PK(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES)
        {


            int ColCount = 0;
            int ParmCount = 0;
            int UpdateColumnCount = 0;
            int PKCount = 0;
            MSSQL_SP_MODEL m = new MSSQL_SP_MODEL
            {
                SP_NAME = UTL_GENERATE_STORED_PROCEDURE_NAME(OBJECT_NAME.ToUpper(), "UPD_BY_PK"),
                SP_FILE_NAME = UTL_GENERATE_STORED_PROCEDURE_NAME(OBJECT_NAME.ToUpper(), "UPD_BY_PK") + ".sql",
                SP_TYPE = MSSQL_SP_MODEL.SQL_STORED_PROCEDURE_TYPE.UPDATE_A_ROW_BY_PK,
                SP_FILE_FOLDER = @"C:\Freddie\CS\MSSQL\" + SERVER_NAME + @"\" + DATABASE_NAME + @"\SQL\",
                SP_SOURCE_CODE_BODY = "na"
            };
            if (!Directory.Exists(m.SP_FILE_FOLDER))
            {
                Directory.CreateDirectory(m.SP_FILE_FOLDER);
            }

 




            m.SP_SOURCE_CODE_TOP = "USE [" + DATABASE_NAME + "]" + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "------------PROC CREATED BY Freddie ON " + DateTime.Now + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "SET ANSI_NULLS ON" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "SET QUOTED_IDENTIFIER ON" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;




            m.SP_SOURCE_CODE_TOP += "IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = '" + m.SP_NAME + "')" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "DROP PROCEDURE " + m.SP_NAME + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;









            m.SP_SOURCE_CODE_BODY = "CREATE PROC [dbo].[" + m.SP_NAME + "]" + Environment.NewLine + Environment.NewLine + Environment.NewLine;


            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
            {
                if (ATTRIBUTE.PK_YN == "Y")
                {
                    PKCount++;
                }
            }

            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
            {

                if (ATTRIBUTE.RESERVED_YN == "N")
                {
                    ParmCount++;
                }


            }
            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
            {

                if (ATTRIBUTE.RESERVED_YN == "N")
                {
                    ColCount++;
                    if (ColCount < ParmCount)
                    {
                        m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.MSSQL_PARM_STRING + " " + ATTRIBUTE.MSSQL_PROC_PARM_STRING + "," + Environment.NewLine;
                    }
                    else
                    {
                        m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.MSSQL_PARM_STRING + " " + ATTRIBUTE.MSSQL_PROC_PARM_STRING + Environment.NewLine;
                    }
                }


            }
            m.SP_SOURCE_CODE_BODY += T3 + "AS" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_BODY += T3 + "UPDATE dbo." + OBJECT_NAME.ToUpper() + Environment.NewLine + T3 + "SET" + Environment.NewLine + Environment.NewLine;


            ColCount = 0;
            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
            {
                if (ATTRIBUTE.PK_YN == "N")
                {
                    if (ATTRIBUTE.UPDATE_YN == "Y")
                    {
                        UpdateColumnCount++;

                    }
                }
            }
            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
            {
                if (ATTRIBUTE.PK_YN == "N")
                {
                    if (ATTRIBUTE.UPDATE_YN == "Y")
                    {
                        ColCount++;
                        if (ColCount < UpdateColumnCount)
                        {
                            m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.MSSQL_PARM_STRING + "," + Environment.NewLine;
                        }
                        else
                        {
                            m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.MSSQL_PARM_STRING + Environment.NewLine + Environment.NewLine;
                        }
                    }
                }
            }
            m.SP_SOURCE_CODE_BODY += T3 + "WHERE" + Environment.NewLine + Environment.NewLine;


            ColCount = 0;
            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
            {
                if (ATTRIBUTE.PK_YN == "Y")
                {
                    ColCount++;
                    if (ColCount < PKCount)
                    {
                        m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.MSSQL_PARM_STRING + " AND" + Environment.NewLine;
                    }
                    else
                    {
                        m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.MSSQL_PARM_STRING + Environment.NewLine;
                    }
                }
            }

            ///   _SQL_UPDATE_BY_PK_STORED_PROCEDURE_SOURCE_CODE += "------------END OF STORED PROCEDURE " + Environment.NewLine + Environment.NewLine;

            if (SAVE_SOURCE_CODE_TO_FILE)
            {
                SAVE_FILE(
                    m.SP_FILE_FOLDER,
                    m.SP_FILE_NAME,
                    m.SP_SOURCE_CODE_TOP + m.SP_SOURCE_CODE_BODY,
                    m.SP_NAME);
            }

            m.SP_EXISTS = UTL_DOES_STORED_PROCEDURE_EXIST(SERVER_NAME, DATABASE_NAME, m.SP_NAME);
            LIST_MSSQL_SP_MODEL.Add(m);
            m = null;

        }
        private void TSQL_GENERATE_USP_DELETE_BY_PK(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES)
        {
            int ColCount = 0;
            int ParmCount = 0;



            MSSQL_SP_MODEL m = new MSSQL_SP_MODEL
            {
                SP_NAME = UTL_GENERATE_STORED_PROCEDURE_NAME(OBJECT_NAME.ToUpper(), "DEL_BY_PK"),
                SP_FILE_NAME = UTL_GENERATE_STORED_PROCEDURE_NAME(OBJECT_NAME.ToUpper(), "DEL_BY_PK") + ".sql",
                SP_TYPE = MSSQL_SP_MODEL.SQL_STORED_PROCEDURE_TYPE.DELETE_BY_PK,
                SP_FILE_FOLDER = @"C:\Freddie\CS\MSSQL\" + SERVER_NAME + @"\" + DATABASE_NAME + @"\SQL\",
                SP_SOURCE_CODE_BODY = "na"
            };
            if (!Directory.Exists(m.SP_FILE_FOLDER))
            {
                Directory.CreateDirectory(m.SP_FILE_FOLDER);
            }



            m.SP_SOURCE_CODE_TOP = "USE [" + DATABASE_NAME + "]" + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;

            m.SP_SOURCE_CODE_TOP += "------------PROC CREATED BY Freddie ON " + DateTime.Now + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "SET ANSI_NULLS ON" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "SET QUOTED_IDENTIFIER ON" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;

            m.SP_SOURCE_CODE_TOP += "IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = '" + m.SP_NAME + "')" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "DROP PROCEDURE " + m.SP_NAME + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;


           m.SP_SOURCE_CODE_BODY= "CREATE PROC [dbo].[" + m.SP_NAME + "]" + Environment.NewLine + Environment.NewLine + Environment.NewLine;


            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
            {
                if (ATTRIBUTE.PK_YN == "Y")
                {
                    ParmCount++;
                }
            }
            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
            {
                if (ATTRIBUTE.PK_YN == "Y")
                {
                    ColCount++;
                    if (ColCount < ParmCount)
                    {
                        m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.MSSQL_PARM_STRING + " " + ATTRIBUTE.MSSQL_PROC_PARM_STRING + "," + Environment.NewLine;
                    }
                    else
                    {
                        m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.MSSQL_PARM_STRING + " " + ATTRIBUTE.MSSQL_PROC_PARM_STRING + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                    }

                }
            }


            m.SP_SOURCE_CODE_BODY += T3 + "AS" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_BODY += T3 + "DELETE FROM dbo." + OBJECT_NAME.ToUpper() + Environment.NewLine + T3 + "WHERE" + Environment.NewLine + Environment.NewLine;




            ColCount = 0;




            foreach (MSSQL_CS_ATTRIBUTE ATTRIBUTE in ATTRIBUTES)
            {
                if (ATTRIBUTE.PK_YN == "Y")
                {
                    ColCount++;
                    if (ColCount < ParmCount)
                    {
                        m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.MSSQL_PARM_STRING + " AND " + Environment.NewLine;
                    }
                    else
                    {
                        m.SP_SOURCE_CODE_BODY += T3 + ATTRIBUTE.COLUMN_NAME + " = " + ATTRIBUTE.MSSQL_PARM_STRING + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                    }

                }
            }


            if (SAVE_SOURCE_CODE_TO_FILE)
            {
                SAVE_FILE(
                    m.SP_FILE_FOLDER,
                    m.SP_FILE_NAME,
                    m.SP_SOURCE_CODE_TOP + m.SP_SOURCE_CODE_BODY,
                    m.SP_NAME);
            }

            m.SP_EXISTS = UTL_DOES_STORED_PROCEDURE_EXIST(SERVER_NAME, DATABASE_NAME, m.SP_NAME);
            LIST_MSSQL_SP_MODEL.Add(m);
            m = null;





        }
        private void  TSQL_GENERATE_USP_DELETE_ALL(List<MSSQL_CS_ATTRIBUTE> ATTRIBUTES)
        {


            MSSQL_SP_MODEL m = new MSSQL_SP_MODEL
            {
                SP_NAME = UTL_GENERATE_STORED_PROCEDURE_NAME(OBJECT_NAME.ToUpper(), "DEL_ALL"),
                SP_FILE_NAME = UTL_GENERATE_STORED_PROCEDURE_NAME(OBJECT_NAME.ToUpper(), "DEL_ALL") + ".sql",
                SP_TYPE = MSSQL_SP_MODEL.SQL_STORED_PROCEDURE_TYPE.DELETE_ALL,
                SP_FILE_FOLDER = @"C:\Freddie\CS\MSSQL\" + SERVER_NAME + @"\" + DATABASE_NAME + @"\SQL\",
                SP_SOURCE_CODE_BODY = "na"
            };
            if (!Directory.Exists(m.SP_FILE_FOLDER))
            {
                Directory.CreateDirectory(m.SP_FILE_FOLDER);
            }

  


            m.SP_SOURCE_CODE_TOP = "USE [" + DATABASE_NAME + "]" + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;

            m.SP_SOURCE_CODE_TOP += "------------PROC CREATED BY Freddie ON " + DateTime.Now + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "SET ANSI_NULLS ON" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "SET QUOTED_IDENTIFIER ON" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;




            m.SP_SOURCE_CODE_TOP += "IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = '" + m.SP_NAME + "')" + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "DROP PROCEDURE " + m.SP_NAME + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_TOP += "GO" + Environment.NewLine + Environment.NewLine;










            m.SP_SOURCE_CODE_BODY += "CREATE PROC [dbo].[" + m.SP_NAME + "]" + Environment.NewLine + Environment.NewLine + Environment.NewLine;

            m.SP_SOURCE_CODE_BODY += T3 + "AS" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            m.SP_SOURCE_CODE_BODY += T3 + "DELETE " + Environment.NewLine + Environment.NewLine + Environment.NewLine;


            m.SP_SOURCE_CODE_BODY += T3 + "FROM dbo." + OBJECT_NAME.ToUpper() + Environment.NewLine + Environment.NewLine + Environment.NewLine;
            //  _SQL_DELETE_ALL_STORED_PROCEDURE_SOURCE_CODE += "------------END OF STORED PROCEDURE " + Environment.NewLine + Environment.NewLine;
            if (SAVE_SOURCE_CODE_TO_FILE)
            {
                SAVE_FILE(
                    m.SP_FILE_FOLDER,
                    m.SP_FILE_NAME,
                    m.SP_SOURCE_CODE_TOP + m.SP_SOURCE_CODE_BODY,
                    m.SP_NAME);
            }

            m.SP_EXISTS = UTL_DOES_STORED_PROCEDURE_EXIST(SERVER_NAME, DATABASE_NAME, m.SP_NAME);
            LIST_MSSQL_SP_MODEL.Add(m);
            m = null;
        }
        #endregion

        //#region GUI AREA

        ////1. GET DATA FROM GRID
        ////2. CLEAR TEXT BOXES
        ////3. UPDATE (INCLUDE INSERT, UPDATE , DELETE)
        ////4. PRIVATE VARIABLES
        ////5. LOAD DATAGRIDVIEW
        ////6. LOAD LISTBOX
        ////7. LOAD CHECKED LIST BOX
        ////8. LOAD COMBO BOX



        //#endregion



        private void SAVE_FILE(string FOLDER_NAME, string FILE_NAME, string FILE_CONTENT, string CLASS_NAME)
        {

            if (!Directory.Exists(FOLDER_NAME))
            {
                Directory.CreateDirectory(FOLDER_NAME);
            }
           
            FileStream FileStream = new FileStream(FOLDER_NAME + FILE_NAME, FileMode.Create, FileAccess.Write);
            StreamWriter StreamWriter = new StreamWriter(FileStream);
            StreamWriter.WriteLine(FILE_CONTENT);
            StreamWriter.Close();
            FileStream = null;
            StreamWriter = null;
        }


    }/////////////////////////END OF CLASS
 

