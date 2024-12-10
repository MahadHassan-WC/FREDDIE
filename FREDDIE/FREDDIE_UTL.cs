using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Data;
using System.Xml.Linq;
using SqlSvrMeta;
namespace FREDDIE
{
   public class FREDDIE_UTL
    {
        #region CONSTANTS
        private const String Q = @"""";
        private const String T1 = "\t";
        private const String T2 = "\t\t";
        private const String T3 = "\t\t\t";
        private const String T4 = "\t\t\t\t";
        private const String NL = "\n";
        private const String AFTER_METHOD = "////END OF METHOD ////////////////////////////////////////////////////////////" + "\n" + "\n";
        #endregion
        #region PRIVATE VARIABLES
      //  private List<FREDDIE_CS_METHOD> _LST_FREDDIE_CS_METHOD;
        private List<CS_SQLSERVER_COLUMN_MATRIX> lst;
        private bool _TABLE_HAS_PK = false;
        private bool _IS_VIEW = false;

        private bool _INS_A_ROW_WHERE_NOT_EXISTS = false;
        private bool _INS_A_ROW = false;

        private bool _UPD_A_ROW_PK = false;

        private bool _DEL_A_ROW_PK = false;
        private bool _DEL_ALL_ROWS = false;

        private bool _SEL_A_ROW_PK = false;
        private bool _SEL_ALL_ROWS = false;
        private bool _SEL_TOP_100_ROWS = false;

        private string _SERVER_NAME;
        private string _DATABASE_NAME;
      
        private string _CS_DB_OBJECT_NAME;
        private string _CS_OBJECT_NAME;

        private int _OBJECT_ID;


       
        private string _TABLE_VIEW_CLASS_FILE_PATH;
        private string _TABLE_VIEW_CLASS_CS;


     
        private string _DATABASE_CLASS_FILE_PATH;
        private string _DATABASE_CLASS_CS;

        #endregion
        #region PUBLIC PROPERTIES



        public string DATABASE_CLASS_CS
        {
            get { return _DATABASE_CLASS_CS; }
           
        }
        public string TABLE_VIEW_CLASS_CS
        {
            get { return _TABLE_VIEW_CLASS_CS; }
      
        }

        public string DATABASE_CLASS_FILE_PATH
        {
            get
            {
                return _DATABASE_CLASS_FILE_PATH;
            }
            set
            {
                _DATABASE_CLASS_FILE_PATH = value;
            }
        }

        public string TABLE_VIEW_CLASS_FILE_PATH
        {
            get
            {
                return _TABLE_VIEW_CLASS_FILE_PATH;
            }
            set
            {
                _TABLE_VIEW_CLASS_FILE_PATH = value;
            }
        }

        //public List<FREDDIE_CS_METHOD> LST_FREDDIE_CS_METHOD
        //{
        //    get
        //    {
        //        return _LST_FREDDIE_CS_METHOD;
        //    }
        //}

        
        public bool TABLE_HAS_PK
        {
            get
            {
                return _TABLE_HAS_PK;
            }
            set
            {
                _TABLE_HAS_PK = value;
            }
        }
        public bool IS_VIEW
        {
            get
            {
                return _IS_VIEW;
            }
            set
            {
                _IS_VIEW = value;
            }
        }
        public bool DEL_ALL_ROWS
        {
            get
            {
                return _DEL_ALL_ROWS;
            }
            set
            {
                _DEL_ALL_ROWS = value;
            }
        }
        public bool SEL_ALL_ROWS
        {
            get
            {
                return _SEL_ALL_ROWS;
            }
            set
            {
                _SEL_ALL_ROWS = value;
            }
        }
        public bool SEL_TOP_100_ROWS
        {
            get
            {
                return _SEL_TOP_100_ROWS;
            }
            set
            {
                _SEL_TOP_100_ROWS = value;
            }
        }
        public bool SEL_A_ROW_PK
        {
            get
            {
                return _SEL_A_ROW_PK;
            }
            set
            {
                _SEL_A_ROW_PK = value;
            }
        }

        public bool DEL_A_ROW_PK
        {
            get
            {
                return _DEL_A_ROW_PK;
            }
            set
            {
                _DEL_A_ROW_PK = value;
            }
        }


        public bool UPD_A_ROW_PK
        {
            get
            {
                return _UPD_A_ROW_PK;
            }
            set
            {
                _UPD_A_ROW_PK = value;
            }
        }

        public bool INS_A_ROW
        {
            get
            {
                return _INS_A_ROW;
            }
            set
            {
                _INS_A_ROW = value;
            }
        }
        public bool INS_A_ROW_WHERE_NOT_EXISTS
        {
            get
            {
                return _INS_A_ROW_WHERE_NOT_EXISTS;
            }
            set
            {
                _INS_A_ROW_WHERE_NOT_EXISTS = value;
            }
        }

        public string SERVER_NAME
        {
            get
            {
                return _SERVER_NAME;
            }
            set
            {
                _SERVER_NAME = value;
            }
        }



        public string DATABASE_NAME
        {
            get
            {
                return _DATABASE_NAME;
            }
            set
            {
                _DATABASE_NAME = value;
            }
        }
        public int OBJECT_ID
        {
            get
            {
                return _OBJECT_ID;
            }
            set
            {
                _OBJECT_ID = value;
            }
        }
        #endregion
        #region CONTRUCTORS
        public FREDDIE_UTL()
        {

        }
        public FREDDIE_UTL(
           string SERVER,
           string DATABASE,
           string TABLE_OR_VIEW_NAME,
           int OBJECT_ID,
           bool VIEW,
           bool PK,
           bool INS_A_ROW,
           bool UPD_A_ROW_PK,
           bool DEL_A_ROW_PK,
           bool SEL_A_ROW_PK,
           bool DEL_ALL_ROWS,
           bool SEL_ALL_ROWS,
           bool SEL_TOP_100_ROWS
           )
        {
            _SERVER_NAME = SERVER;
            _DATABASE_NAME = DATABASE;
            _CS_OBJECT_NAME = TABLE_OR_VIEW_NAME;
            _OBJECT_ID = OBJECT_ID;
            _IS_VIEW = VIEW;
            _TABLE_HAS_PK = PK;
            _INS_A_ROW = INS_A_ROW;
            _UPD_A_ROW_PK = UPD_A_ROW_PK;
            _DEL_A_ROW_PK = DEL_A_ROW_PK;
            _DEL_ALL_ROWS = DEL_ALL_ROWS;
            _SEL_A_ROW_PK = SEL_A_ROW_PK;
            _SEL_ALL_ROWS = SEL_ALL_ROWS;
            _SEL_TOP_100_ROWS = SEL_TOP_100_ROWS;



        }
        #endregion
        #region GENERATE SQL
        private string SQL_INS_A_ROW()
        {
            string SQL = "string SQL_COMMAND = @" + Q + "INSERT INTO dbo." + lst[0].TABLE_OR_VIEW_NAME + Environment.NewLine + T3 + "(" + Environment.NewLine;

            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].INSERT_YN == "Y")
                {
                    if (i < lst.Count - 1)
                    {
                        SQL += T3 + lst[i].COLUMN_NAME + "," + Environment.NewLine;
                    }
                    else
                    {
                        SQL += T3 + lst[i].COLUMN_NAME + Environment.NewLine;
                    }
                }

            }
            SQL += T3 + ")" + Environment.NewLine;
            SQL += T3 + " VALUES " + Environment.NewLine;
            SQL += T3 + "(" + Environment.NewLine;


            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].INSERT_YN == "Y")
                {
                    if (i < lst.Count - 1)
                    {
                        SQL += T3 + lst[i].DB_PARM_VALUE + "," + Environment.NewLine;
                    }
                    else
                    {
                        SQL += T3 + lst[i].DB_PARM_VALUE + Environment.NewLine;
                    }
                }

            }

            return SQL + T3 + ")" + Q + ";" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
        }
        private string SQL_INS_A_ROW_WHERE_NOT_EXISTS()
        {




            string WHERE_CLAUSE = null;
            int PK_COUNT = 0;
            int PK_COUNT2 = 0;
            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].PK_YN == "Y")
                {
                    PK_COUNT++;
                    WHERE_CLAUSE = Environment.NewLine + T4 + " WHERE " + Environment.NewLine;
                }

            }
            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].PK_YN == "Y")
                {
                    PK_COUNT2++;
                    if (PK_COUNT2 < PK_COUNT)
                    {
                        WHERE_CLAUSE += T4 + lst[i].COLUMN_NAME + " = " + lst[i].DB_PARM_VALUE + " AND " + Environment.NewLine;
                    }
                    else
                    {
                        WHERE_CLAUSE += T4 + lst[i].COLUMN_NAME + " = " + lst[i].DB_PARM_VALUE + ")" + Environment.NewLine;
                    }
                }
            }



            string SQL = "string SQL_COMMAND = @" + Q + "IF NOT EXISTS (" + Environment.NewLine + T3 + "SELECT * FROM DBO." + lst[0].TABLE_OR_VIEW_NAME + WHERE_CLAUSE


                + Environment.NewLine
               + Environment.NewLine + T3 + "BEGIN " + Environment.NewLine + T4 + "INSERT INTO dbo." + lst[0].TABLE_OR_VIEW_NAME + Environment.NewLine + T4 + "(" + Environment.NewLine;

            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].INSERT_YN == "Y")
                {
                    if (i < lst.Count - 1)
                    {
                        SQL += T4 + lst[i].COLUMN_NAME + "," + Environment.NewLine;
                    }
                    else
                    {
                        SQL += T4 + lst[i].COLUMN_NAME + Environment.NewLine;
                    }
                }

            }
            SQL += T4 + ")" + Environment.NewLine;
            SQL += T4 + " VALUES " + Environment.NewLine;
            SQL += T4 + "(" + Environment.NewLine;


            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].INSERT_YN == "Y")
                {
                    if (i < lst.Count - 1)
                    {
                        SQL += T4 + lst[i].DB_PARM_VALUE + "," + Environment.NewLine;
                    }
                    else
                    {
                        SQL += T4 + lst[i].DB_PARM_VALUE + Environment.NewLine;
                    }
                }

            }

            return SQL + T4 + ")" + Environment.NewLine + T3 + " END " + Q + ";" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
        }
        private string SQL_UPD_A_ROW_PK()
        {
            string SQL = "const string SQL_COMMAND = @" + Q + "UPDATE dbo." + lst[0].TABLE_OR_VIEW_NAME + " SET " + Environment.NewLine;

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
                if (lst[i].UPDATE_YN == "Y")
                {

                    if (i < lst.Count - 1)
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
                        SQL += T3 + lst[i].COLUMN_NAME + " = " + lst[i].DB_PARM_VALUE + Environment.NewLine;
                    }
                }
            }

            return SQL + T3 + Q + ";" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
        }
        private string SQL_DEL_ALL_ROWS()
        {
            string SQL = "string SQL_COMMAND = @" + Q + "DELETE FROM dbo." + lst[0].TABLE_OR_VIEW_NAME + ";" + Environment.NewLine;
            return SQL + Environment.NewLine + Environment.NewLine + Environment.NewLine;
        }
        private string SQL_SEL_ALL_ROWS()
        {
            string SQL = "string SQL_COMMAND = @" + Q + "SELECT " + Environment.NewLine;
            for (int i = 0; i < lst.Count; i++)
            {


                if (i < lst.Count - 1)
                {
                    SQL += T3 + lst[i].COLUMN_NAME + " , " + Environment.NewLine;
                }
                else
                {
                    SQL += T3 + lst[i].COLUMN_NAME + Environment.NewLine;
                }

            }
            SQL += T3 + " FROM dbo." + lst[0].TABLE_OR_VIEW_NAME;
            return SQL + T3 + Q + ";" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
        }
        private string SQL_SEL_TOP_100_ROWS()
        {
            string SQL = "string SQL_COMMAND = @" + Q + "SELECT TOP 100 " + Environment.NewLine;
            for (int i = 0; i < lst.Count; i++)
            {


                if (i < lst.Count - 1)
                {
                    SQL += T3 + lst[i].COLUMN_NAME + " , " + Environment.NewLine;
                }
                else
                {
                    SQL += T3 + lst[i].COLUMN_NAME + Environment.NewLine;
                }

            }
            SQL += T3 + " FROM dbo." + lst[0].TABLE_OR_VIEW_NAME;
            return SQL + T3 + Q + ";" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
        }
        private string SQL_SEL_A_ROW_PK()
        {
            string SQL = "string SQL_COMMAND = @" + Q + "SELECT " + Environment.NewLine;
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

                if (i < lst.Count - 1)
                {
                    SQL += T3 + lst[i].COLUMN_NAME + " , " + Environment.NewLine;
                }
                else
                {

                    SQL += T3 + lst[i].COLUMN_NAME + Environment.NewLine + T3 + " FROM " + lst[0].TABLE_OR_VIEW_NAME + Environment.NewLine;
                    if (PK_COUNT > 0)
                    {
                        SQL += T3 + " WHERE " + Environment.NewLine;
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
                        SQL += T3 + lst[i].COLUMN_NAME + " = " + lst[i].DB_PARM_VALUE + Environment.NewLine;
                    }
                }
            }

            return SQL + T3 + Q + ";" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
        }
        private string SQL_DEL_A_ROW_PK()
        {
            string SQL = "const string SQL_COMMAND = @" + Q + "DELETE dbo." + lst[0].TABLE_OR_VIEW_NAME + " WHERE " + Environment.NewLine;

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
                if (lst[i].PK_YN == "Y")
                {
                    PK_COUNT2++;
                    if (PK_COUNT2 < PK_COUNT)
                    {
                        SQL += T3 + lst[i].COLUMN_NAME + " = " + lst[i].DB_PARM_VALUE + " AND " + Environment.NewLine;
                    }
                    else
                    {
                        SQL += T3 + lst[i].COLUMN_NAME + " = " + lst[i].DB_PARM_VALUE + Environment.NewLine;
                    }
                }
            }

            return SQL + T3 + Q + ";" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
        }

        #endregion
        #region GENERATE C#

        
        public string GENERATE_TABLE_OBJECT(  bool bWriteFile)
        {
            SQLSVR_METADATA sqlsvr = new SQLSVR_METADATA();
            sqlsvr.SERVER_NAME = _SERVER_NAME;
            sqlsvr.DATABASE_NAME = _DATABASE_NAME;
            sqlsvr.TABLE_NAME = _CS_OBJECT_NAME;
            sqlsvr.OBJECT_ID = _OBJECT_ID.ToString();
            List<CS_SQLSERVER_COLUMN_MATRIX> lst = new List<CS_SQLSERVER_COLUMN_MATRIX>();
            lst = sqlsvr.GET_CS_COLUMN_MATRIX_TABLE();
            sqlsvr = null;
            _TABLE_VIEW_CLASS_CS = null;

            _TABLE_VIEW_CLASS_CS = "public  class " + lst[0].CS_OBJECT_NAME.ToString() + Environment.NewLine + "{" + Environment.NewLine;
            string FileName = lst[0].CS_OBJECT_NAME.ToString() + ".cs";
            foreach (CS_SQLSERVER_COLUMN_MATRIX X in lst)
            {
                _TABLE_VIEW_CLASS_CS += T1 + X.CS_OBJECT_PRIVATE_VARIABLE_NAME_WITH_TYPE;
            }
            _TABLE_VIEW_CLASS_CS += Environment.NewLine + Environment.NewLine;
            foreach (CS_SQLSERVER_COLUMN_MATRIX X in lst)
            {
                _TABLE_VIEW_CLASS_CS += T1 + X.CS_PUBLIC_PROPERTY_CODE;
            }

            _TABLE_VIEW_CLASS_CS += CREATE_OBJECT_CONTRUCTOR_1(_SERVER_NAME, _DATABASE_NAME, _CS_OBJECT_NAME, _OBJECT_ID);
            _TABLE_VIEW_CLASS_CS += CREATE_OBJECT_CONTRUCTOR_2(_SERVER_NAME, _DATABASE_NAME, _CS_OBJECT_NAME, _OBJECT_ID);

            _TABLE_VIEW_CLASS_CS += "}   /// END OF CLASS" + Environment.NewLine;
            if (bWriteFile)
            { WriteFile(@"C:\_Freddie\" + SERVER_NAME + @"\" + DATABASE_NAME + @"\TABLES\", FileName, _TABLE_VIEW_CLASS_CS); }
            return _TABLE_VIEW_CLASS_CS;
        }
        public string CREATE_OBJECT_CONTRUCTOR_1(string SERVER_NAME, string DATABASE_NAME, string TABLE_NAME, int OBJECT_ID)
        {


            SQLSVR_METADATA sqlsvr = new SQLSVR_METADATA();
            sqlsvr.SERVER_NAME = _SERVER_NAME;
            sqlsvr.DATABASE_NAME = _DATABASE_NAME;
            sqlsvr.TABLE_NAME = _CS_OBJECT_NAME;
            sqlsvr.OBJECT_ID = _OBJECT_ID.ToString();
            List<CS_SQLSERVER_COLUMN_MATRIX> lst = new List<CS_SQLSERVER_COLUMN_MATRIX>();
            lst = sqlsvr.GET_CS_COLUMN_MATRIX_TABLE();
            sqlsvr = null;
            string CONTRUCTOR_1 = T1 + "public " + lst[0].CS_OBJECT_NAME.ToString() + "()" +
              T1 + Environment.NewLine +
              T1 + "{ " + Environment.NewLine;
            foreach (CS_SQLSERVER_COLUMN_MATRIX X in lst)
            {
                CONTRUCTOR_1 += T2 + X.CS_OBJECT_PRIVATE_VARIABLE_NAME + " = " + X.CS_OBJECT_PRIVATE_VARIABLE_CONSTRUCTOR_VALUE + ";" + Environment.NewLine;
            }
            CONTRUCTOR_1 += T1 + "} /// END OF FIRST CONSTRUCTOR" + Environment.NewLine + Environment.NewLine;
            return CONTRUCTOR_1;
        }
        public string CREATE_OBJECT_CONTRUCTOR_2(string SERVER_NAME, string DATABASE_NAME, string TABLE_NAME, int OBJECT_ID)
        {
            SQLSVR_METADATA sqlsvr = new SQLSVR_METADATA();
            sqlsvr.SERVER_NAME = SERVER_NAME;
            sqlsvr.DATABASE_NAME = DATABASE_NAME;
            sqlsvr.TABLE_NAME = TABLE_NAME;
            sqlsvr.OBJECT_ID = OBJECT_ID.ToString();
            List<CS_SQLSERVER_COLUMN_MATRIX> lst = new List<CS_SQLSERVER_COLUMN_MATRIX>();
            lst = sqlsvr.GET_CS_COLUMN_MATRIX_TABLE();
            sqlsvr = null;
            int i = 0;


            string CONTRUCTOR_2 = T1 + "public " + lst[0].CS_OBJECT_NAME.ToString() + "(" + Environment.NewLine;
            foreach (CS_SQLSERVER_COLUMN_MATRIX X in lst)
            {
                i++;
                if (i < lst.Count)
                { CONTRUCTOR_2 += T2 + X.CS_DATA_TYPE + " " + X.COLUMN_NAME.ToLower() + "," + Environment.NewLine; }
                else
                { CONTRUCTOR_2 += T2 + X.CS_DATA_TYPE + " " + X.COLUMN_NAME.ToLower() + Environment.NewLine; }
            }
            CONTRUCTOR_2 += T1 + ")" + Environment.NewLine;


            CONTRUCTOR_2 += T1 + "{" + Environment.NewLine;
            foreach (CS_SQLSERVER_COLUMN_MATRIX X in lst)
            {
                CONTRUCTOR_2 += T2 + "this." + X.COLUMN_NAME + " = " + X.COLUMN_NAME.ToLower() + ";" + Environment.NewLine;
            }
            CONTRUCTOR_2 += T1 + "} /// END OF SECOND CONSTRUCTOR" + Environment.NewLine + Environment.NewLine;
            return CONTRUCTOR_2;
        }
        public string CREATE_OBJECT_CONTRUCTOR_PK()
        {
            string CONTRUCTOR_PK = null;
            return CONTRUCTOR_PK;
        }


        public string GENERATE_VIEW_OBJECT( bool bWriteFile)
        {
            SQLSVR_METADATA sqlsvr = new SQLSVR_METADATA();
            sqlsvr.SERVER_NAME = _SERVER_NAME;
            sqlsvr.DATABASE_NAME = _DATABASE_NAME;
            sqlsvr.TABLE_NAME = _CS_OBJECT_NAME;
            sqlsvr.OBJECT_ID = _OBJECT_ID.ToString();
            List<CS_SQLSERVER_COLUMN_MATRIX> lst = new List<CS_SQLSERVER_COLUMN_MATRIX>();
            lst = sqlsvr.GET_CS_COLUMN_MATRIX_TABLE();
            sqlsvr = null;
            _TABLE_VIEW_CLASS_CS = null;

            _TABLE_VIEW_CLASS_CS = "public  class " + lst[0].CS_OBJECT_NAME.ToString() + Environment.NewLine + "{" + Environment.NewLine;
            string FileName = lst[0].CS_OBJECT_NAME.ToString() + ".cs";
            foreach (CS_SQLSERVER_COLUMN_MATRIX X in lst)
            {
                _TABLE_VIEW_CLASS_CS += T1 + X.CS_OBJECT_PRIVATE_VARIABLE_NAME_WITH_TYPE;
            }
            _TABLE_VIEW_CLASS_CS += Environment.NewLine + Environment.NewLine;
            foreach (CS_SQLSERVER_COLUMN_MATRIX X in lst)
            {
                _TABLE_VIEW_CLASS_CS += T1 + X.CS_PUBLIC_PROPERTY_CODE;
            }
            _TABLE_VIEW_CLASS_CS += "}" + Environment.NewLine;
            if (bWriteFile)
            { WriteFile(@"C:\_Freddie\" + SERVER_NAME + @"\" + DATABASE_NAME + @"\VIEWS\", FileName, _TABLE_VIEW_CLASS_CS); }
            return _TABLE_VIEW_CLASS_CS;
        }

         
        public String GENERATE_SQLSERVER_DB_CLASS()
        {
           
            _DATABASE_CLASS_CS= "public class db" + _CS_OBJECT_NAME + " : dbUTL" + Environment.NewLine + "{" + Environment.NewLine + GENERATE_METHODS() + "}" + Environment.NewLine + "/////////END OF CLASS";
            return _DATABASE_CLASS_CS;
        }



        //public String GENERATE_CUSTOM_METHODS(List<FREDDIE_CS_METHOD> lst)
        //{
        //    int i = 0;

        //    foreach (FREDDIE_CS_METHOD Freddie in lst)
        //    {
        //        switch(lst[i].METHOD_TYPE.ToString())
        //        {

        //        }
        //        i++;
        //    }

        //    return "";
        //}
        public String GENERATE_METHODS()
        {
            //SQLSVR_METADATA sqlsvr = new SQLSVR_METADATA();
            //sqlsvr.SERVER_NAME = SERVER_NAME;
            //sqlsvr.DATABASE_NAME = DATABASE_NAME;
            //sqlsvr.TABLE_NAME = _CS_OBJECT_NAME;
            //sqlsvr.OBJECT_ID = _OBJECT_ID.ToString();
            //lst = sqlsvr.GET_CS_COLUMN_MATRIX_TABLE();

            //_LST_FREDDIE_CS_METHOD = new List<FREDDIE_CS_METHOD>();

            //if (lst != null)
            //{
            //    if (lst.Count > 0)
            //    {
            //        _CS_DB_OBJECT_NAME = lst[0].CS_DB_OBJECT_NAME.ToString();
            //        _CS_OBJECT_NAME = lst[0].CS_OBJECT_NAME.ToString();
            //    }
            //}
            //_TABLE_HAS_PK = sqlsvr.TableHasPK();
            //String s = null;

            //if (INS_A_ROW)
            //{
            //    FREDDIE_CS_METHOD o = new FREDDIE_CS_METHOD();
            //    o.CS = CS_INS_A_ROW() + AFTER_METHOD;
            //    o.METHOD_NAME = "INS_A_ROW";
            //    s += o.CS;
            //    _LST_FREDDIE_CS_METHOD.Add(o);
            //    o = null;
            //}
            //if (INS_A_ROW_WHERE_NOT_EXISTS)
            //{
            //    if (_TABLE_HAS_PK)
            //    {
            //        s += CS_INS_A_ROW_WHERE_NOT_EXISTS() + AFTER_METHOD;
            //    }
            //    else
            //    {
            //        s += "///TABLE HAS NO PRIMARY KEY" + Environment.NewLine + Environment.NewLine + AFTER_METHOD;
            //    }

            //}
            //if (UPD_A_ROW_PK)
            //{

            //    if (_TABLE_HAS_PK)
            //    {
            //        FREDDIE_CS_METHOD o = new FREDDIE_CS_METHOD();
            //        o.CS = CS_UPD_A_ROW_PK() + AFTER_METHOD;
            //        o.METHOD_NAME = "UPD_A_ROW_PK";
            //        s += o.CS;
            //        _LST_FREDDIE_CS_METHOD.Add(o);
            //        o = null;
            //    }
            //    else
            //    {
            //        s += "///TABLE HAS NO PRIMARY KEY" + Environment.NewLine + Environment.NewLine + AFTER_METHOD;
            //    }


            //}

            //if (SEL_A_ROW_PK)
            //{
            //    if (_TABLE_HAS_PK)
            //    {
            //        FREDDIE_CS_METHOD o = new FREDDIE_CS_METHOD();
            //        o.CS = CS_SEL_A_ROW_PK() + AFTER_METHOD;
            //        o.METHOD_NAME = "SEL_A_ROW_PK";
            //        s += o.CS;
            //        _LST_FREDDIE_CS_METHOD.Add(o);
            //        o = null;
            //    }
            //    else
            //    {
            //        s += "///TABLE HAS NO PRIMARY KEY" + AFTER_METHOD;
            //    }

            //}

            //if (SEL_ALL_ROWS)
            //{
            //    FREDDIE_CS_METHOD o = new FREDDIE_CS_METHOD();
            //    o.CS = CS_SEL_ALL_ROWS() + AFTER_METHOD;
            //    o.METHOD_NAME = "SEL_ALL_ROWS";
            //    s += o.CS;
            //    _LST_FREDDIE_CS_METHOD.Add(o);
            //    o = null;
            //}
            //if (SEL_TOP_100_ROWS)
            //{
            //    FREDDIE_CS_METHOD o = new FREDDIE_CS_METHOD();
            //    o.CS = CS_SEL_TOP_100_ROWS() + AFTER_METHOD;
            //    o.METHOD_NAME = "SEL_TOP_100_ROWS";
            //    s += o.CS;
            //    _LST_FREDDIE_CS_METHOD.Add(o);
            //    o = null;
            //}
            //if (DEL_A_ROW_PK)
            //{
            //    if (_TABLE_HAS_PK)
            //    {
            //        FREDDIE_CS_METHOD o = new FREDDIE_CS_METHOD();
            //        o.CS = CS_DEL_A_ROW_PK() + AFTER_METHOD;
            //        o.METHOD_NAME = "DEL_A_ROW_PK";
            //        s += o.CS;
            //        _LST_FREDDIE_CS_METHOD.Add(o);
            //        o = null;
            //    }
            //    else
            //    {
            //        s += "///TABLE HAS NO PRIMARY KEY" + AFTER_METHOD;
            //    }

            //}
            //if (DEL_ALL_ROWS)
            //{
            //    FREDDIE_CS_METHOD o = new FREDDIE_CS_METHOD();
            //    o.CS = CS_DEL_ALL_ROWS() + AFTER_METHOD;
            //    o.METHOD_NAME = "DEL_ALL_ROWS";
            //    s += o.CS;
            //    _LST_FREDDIE_CS_METHOD.Add(o);
            //    o = null;
            //}

            return "";
        }


        
        public String CS_INS_A_ROW_WHERE_NOT_EXISTS()
        {


            string CS_INSERT_PARAMETERS = null;
            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].INSERT_YN == "Y")
                {
                    if (lst[i].CS_PARM_VALUE.ToUpper() != "NA")
                    {
                        CS_INSERT_PARAMETERS += T2 + lst[i].CS_PARM_VALUE + Environment.NewLine;
                    }
                }


            }

            return

               @"
                    public System.Boolean INS_A_ROW_WHERE_NOT_EXISTS(" + _CS_OBJECT_NAME + " o)" + Environment.NewLine +
                @"    {

                        try
                        {"
                         + SQL_INS_A_ROW_WHERE_NOT_EXISTS() +
                            @"
                            cmd = new System.Data.SqlClient.SqlCommand();
                            cmd.CommandText = SQL_COMMAND;"
                              + Environment.NewLine + CS_INSERT_PARAMETERS +
                           @"
                            return RUN_SQL_COMMAND(o.DEFAULT_DATABASE_NAME, o.DEFAULT_OBJECT_NAME );
                        } ////end of try
                        catch (System.Data.SqlClient.SqlException SqlException)
                         {
                            return false;
                         } ////end of catch
                        finally
                        {
                        } ////end of finally
                    }
                ";
        }
        public String CS_INS_A_ROW()
        {
            string CS_INSERT_PARAMETERS = null;
            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].INSERT_YN == "Y")
                {
                    if (lst[i].CS_PARM_VALUE.ToUpper() != "NA")
                    {
                        CS_INSERT_PARAMETERS += T2 + lst[i].CS_PARM_VALUE + Environment.NewLine;
                    }
                }


            }

            return

               @"
                    public System.Boolean INS_A_ROW(" + _CS_OBJECT_NAME + " o)" + Environment.NewLine +
                  @"  {

                        try
                        {"
                        + SQL_INS_A_ROW() +


                           @"
                            cmd = new System.Data.SqlClient.SqlCommand();
                            cmd.CommandText = SQL_COMMAND;"
                              + Environment.NewLine + CS_INSERT_PARAMETERS +
                           @"
                            return RUN_SQL_COMMAND(o.DEFAULT_DATABASE_NAME, o.DEFAULT_OBJECT_NAME );
                             
                        } ////end of try
                        catch (System.Data.SqlClient.SqlException SqlException)
                         {
                            return false;
                          } ////end of catch
                        finally
                        {
                               cmd = null;
                        } ////end of finally
                    }
                    /////END OF INS_A_ROW



                ";
        }
        public String CS_UPD_A_ROW_PK()
        {


            string CS_UPDATE_PARAMETERS = null;
            for (int i = 0; i < lst.Count; i++)
            {

                if (lst[i].CS_PARM_VALUE.ToUpper() != "NA")
                {
                    CS_UPDATE_PARAMETERS += T2 + lst[i].CS_PARM_VALUE + Environment.NewLine;
                }
            }


            return

               @"
                    public System.Boolean UPD_A_ROW_PK(" + _CS_OBJECT_NAME + " o)" + Environment.NewLine +
                   @" {

                        try
                        {"
                        + SQL_UPD_A_ROW_PK() +
                           @" cmd = new System.Data.SqlClient.SqlCommand();
                            cmd.CommandText = SQL_COMMAND;"
                              + Environment.NewLine + CS_UPDATE_PARAMETERS +
                           @"
                            return RUN_SQL_COMMAND(o.DEFAULT_DATABASE_NAME, o.DEFAULT_OBJECT_NAME );
                             
                        } ////end of try
                        catch (System.Data.SqlClient.SqlException SqlException)
                         {
                            return false;
                         } ////end of catch
                        finally
                        {
                               cmd = null;
                        }
                    }

                    /////END OF UPD_A_ROW_PK



                ";
        }
        public String CS_SEL_A_ROW_PK()
        {


            string CS_SELECT_PARAMETERS = null;
            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].PK_YN == "Y")
                {
                    if (lst[i].CS_PARM_VALUE.ToUpper() != "NA")
                    {
                        CS_SELECT_PARAMETERS += T2 + lst[i].CS_PARM_VALUE + Environment.NewLine;
                    }
                }


            }



            return

               @"
                    public System.Data.DataSet SEL_A_ROW_PK(" + _CS_OBJECT_NAME + " o)" + Environment.NewLine +
                   @" {

                         try
                        {"
                        + SQL_SEL_A_ROW_PK() + Environment.NewLine + CS_SELECT_PARAMETERS +
                           @"  
                                      
                                    da = new System.Data.SqlClient.SqlDataAdapter();
                                    return GET_DATASET(o.DEFAULT_DATABASE_NAME, o.DEFAULT_OBJECT_NAME, SQL_COMMAND);
                        } ////end of try
                        catch (System.Data.SqlClient.SqlException SqlException)
                         {
                            return null;
                         } ////end of catch
                        finally
                        {
                        } ////end of finally
                    }
                ";
        }
        public String CS_SEL_ALL_ROWS()
        {
            return
               @"
                    public System.Data.DataSet SEL_ALL_ROWS(" + _CS_OBJECT_NAME + " o)" + Environment.NewLine +
                   @" {
                         try
                        {"
                        + SQL_SEL_ALL_ROWS() +
                           @"  
                              da = new System.Data.SqlClient.SqlDataAdapter();
                              return GET_DATASET(o.DEFAULT_DATABASE_NAME, o.DEFAULT_OBJECT_NAME, SQL_COMMAND);
                         } ////end of try
                        catch (System.Data.SqlClient.SqlException SqlException)
                         {
                            return null;
                         } ////end of catch
                        finally
                        {
                               cmd = null;
                         }
                        }
                    /////END OF SEL_ALL_ROWS
                ";
        }
        public String CS_SEL_TOP_100_ROWS()
        {
            return
               @"
                    public System.Data.DataSet SEL_TOP_100_ROWS(" + _CS_OBJECT_NAME + " o)" + Environment.NewLine +
                   @" {
                         try
                        {"
                        + SQL_SEL_TOP_100_ROWS() +
                           @"  
                              da = new System.Data.SqlClient.SqlDataAdapter();
                              return GET_DATASET(o.DEFAULT_DATABASE_NAME, o.DEFAULT_OBJECT_NAME, SQL_COMMAND);
                         } ////end of try
                        catch (System.Data.SqlClient.SqlException SqlException)
                         {
                            return null;
                         } ////end of catch
                        finally
                        {
                               cmd = null;
                         }
                        }
                    /////END OF SEL_ALL_ROWS
                ";
        }
        public String CS_DEL_A_ROW_PK()
        {
            string CS_DELETE_PARAMETERS = null;
            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].PK_YN == "Y")
                {
                    if (lst[i].CS_PARM_VALUE.ToUpper() != "NA")
                    {
                        CS_DELETE_PARAMETERS += T2 + lst[i].CS_PARM_VALUE + Environment.NewLine;
                    }
                }


            }
            return

               @"
                    public System.Boolean DEL_A_ROW_PK(" + _CS_OBJECT_NAME + " o)" + Environment.NewLine +

                    @"{

                        try
                        {"
                        + SQL_DEL_A_ROW_PK() +
                           @" cmd = new System.Data.SqlClient.SqlCommand();
                            cmd.CommandText = SQL_COMMAND;"
                              + Environment.NewLine + CS_DELETE_PARAMETERS +
                           @"
                            return RUN_SQL_COMMAND(o.DEFAULT_DATABASE_NAME, o.DEFAULT_OBJECT_NAME );
                             
                        } ////end of try
                        catch (System.Data.SqlClient.SqlException SqlException)
                         {
                            return false;
                         } ////end of catch
                        finally
                        {
                               cmd = null;
                         }
                        }




                    /////END OF DEL_A_ROW_PK
                ";
        }
        public String CS_DEL_ALL_ROWS()
        {

            return

                @"
                    public System.Boolean DEL_ALL_ROWS()
                    {

                        try
                        {"
                        + SQL_DEL_ALL_ROWS() +
                           @" cmd = new System.Data.SqlClient.SqlCommand();
                            cmd.CommandText = SQL_COMMAND; 
                          
                          
                            return RUN_SQL_COMMAND(o.DEFAULT_DATABASE_NAME, o.DEFAULT_OBJECT_NAME );
                             
                        } ////end of try
                        catch (System.Data.SqlClient.SqlException SqlException)
                         {
                            return false;
                         } ////end of catch
                        finally
                        {
                               cmd = null;
                         }
                        }




                    /////END OF DEL_A_ROW_PK
                ";
        }
        #endregion
        #region FILE IO
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



       



        #endregion
        #region NOT IN USE
        public DataSet GET_MYDATABASES_XML_DATASET(string MyServer, string MyPath)
        {
            ////STEP 1 CREATE A DataSet and a DataTable object
            //DataSet DS = new DataSet();
            //DataTable DT = new DataTable();
            ////STEP 2 CREATE the columns
            //DataColumn SERVER_NAME = new DataColumn("SERVER_NAME");
            //DataColumn DATABASE_NAME = new DataColumn("DATABASE_NAME");
            ////STEP 3 Set the DataType for each column
            //SERVER_NAME.DataType = System.Type.GetType("System.String");
            //DATABASE_NAME.DataType = System.Type.GetType("System.String");
            ////STEP 4 Add each column to the DataTable
            //DT.Columns.Add(SERVER_NAME);
            //DT.Columns.Add(DATABASE_NAME);

            string filePath = @"C:\Users\mennettp\Desktop\Development\2017\ReadXML\ReadXML\authors.xml";
            string FileName = "MYDATABASES_" + MyServer + ".XML";
            DataSet DS = new DataSet();

            ///MYDATABASES_WCSSQL2.XML
            ///

            DS.ReadXml(MyPath + FileName);


            return DS;
        }

        public bool CREATE_MYDATABASES_JSON(string MyServer, string[] MyDatabases, string MyPath)
        {
            string MyFileText = "{" + Q + "MYDATABASES" + Q + ":[" + Environment.NewLine;
            for (int x = 0; x < MyDatabases.Length - 1; x++)
            {
                if (x < MyDatabases.Length - 2)
                {
                    MyFileText += T1 + "{" + Environment.NewLine + T1
                        + Q + "DB" + Q + ":" + Q + MyDatabases[x] + Q + Environment.NewLine + T1 + "}," + Environment.NewLine;
                }
                else
                {
                    MyFileText +=
                          T1 + "{" + Environment.NewLine
                        + T1 + Q + "DB" + Q + ":" + Q + MyDatabases[x] + Q + Environment.NewLine
                        + T1 + "}" + Environment.NewLine;

                }
            }
            MyFileText += "]}" + Environment.NewLine;
            WriteFile(MyPath, "MYDATABASES_" + MyServer + ".JSON", MyFileText);
            return true;
        }
        public bool CREATE_MYDATABASES_XML(string MyServer, string[] MyDatabases, string MyPath)
        {
            string[] MyArray = null;
            string MyFileText =
                "<MYDATABASES>" + Environment.NewLine;
            for (int x = 0; x < MyDatabases.Length - 1; x++)
            {
                MyArray = MyDatabases[x].Split('*');
                MyFileText += T1 + "<DB>" + Environment.NewLine;
                MyFileText += T2 + "<SERVER_NAME>" + MyArray[0] + "</SERVER_NAME>" + Environment.NewLine;
                MyFileText += T2 + "<DATABASE_NAME>" + MyArray[1] + "</DATABASE_NAME>" + Environment.NewLine;
                MyFileText += T2 + "<DATABASE_ID>" + MyArray[2] + "</DATABASE_ID>" + Environment.NewLine;
                MyFileText += T1 + "</DB>" + Environment.NewLine;



            }
            MyFileText += "</MYDATABASES>" + Environment.NewLine;
            WriteFile(MyPath, "MYDATABASES_" + MyServer + ".XML", MyFileText);
            return true;
        }


        public bool ADD_MYDATABASES_XML(string MyServer, string[] MyDatabases, string MyPath)
        {
            string[] MyArray = null;
            XDocument document = XDocument.Load(MyPath + "MYDATABASES_" + MyServer + ".XML");
            for (int x = 0; x < MyDatabases.Length - 1; x++)
            {
                MyArray = MyDatabases[x].Split('*');
                var newElement = new XElement("DB",
                   new XElement("SERVER_NAME", MyArray[0]),
                   new XElement("DATABASE_NAME", MyArray[1]),
                   new XElement("DATABASE_ID", MyArray[2]));

                XElement XE = (from xml2 in document.Descendants("DB")
                               where xml2.Element("DATABASE_NAME").Value == MyArray[1]
                               select xml2).FirstOrDefault();
                if (XE == null)
                {
                    document.Element("MYDATABASES").Add(newElement);
                }
            }
            document.Save(MyPath + "MYDATABASES_" + MyServer + ".XML");
            return true;
        }
        public bool DELETE_MYDATABASES_XML(string MyServer, string[] MyDatabases, string MyPath)
        {
            //string[] MyArray = null;
            //XDocument document = XDocument.Load(MyPath + "MYDATABASES_" + MyServer + ".XML");
            //IEnumerable<XElement> Person = document.Element("Person");


            //for (int x = 0; x < MyDatabases.Length - 1; x++)
            //{
            //    MyArray = MyDatabases[x].Split('*');
            //    var newElement = new XElement("DB",
            //       new XElement("SERVER_NAME", MyArray[0]),
            //       new XElement("DATABASE_NAME", MyArray[1]),
            //       new XElement("DATABASE_ID", MyArray[2]));

            //    XElement XE = (from xml2 in document.Descendants("DB")
            //                   where xml2.Element("DATABASE_NAME").Value == MyArray[1]
            //                   select xml2).FirstOrDefault();
            //    //if (XE != null)
            //    //{
            //    //    document.Element("MYDATABASES").Remove(XE);
            //    //}
            //}
            //  document.Save(MyPath + "MYDATABASES_" + MyServer + ".XML");
            return true;
        }
        #endregion


        #region CUSTOM AREA
        /////CUSTOM INSERT
        /////CUSTOM UPDATE
        /////CUSTOM DELETE
        /////CUSTOM INSERT WHERE NOT EXISTS
        /////CUSTOM SELECT
        /////CUSTOM SELECT BETWEEN TWO DATES
        /////CUSTOM SELECT COUNT (*)
        /////CUSTOM SELECT SUM()
        /////CUSTOM SELECT MIN()
        /////CUSTOM SELECT MAX()
        /////CUSTOM SELECT TOP 1()
        #endregion
    }
}
