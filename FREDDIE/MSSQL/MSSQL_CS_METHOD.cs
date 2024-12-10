using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace FREDDIE.MSSQL
//{
  public  class MSSQL_CS_METHOD

    {
        private System.Boolean _CUSTOM_METHOD;
        private System.String _SERVER_NAME;
        private System.String _DATABASE_NAME;
        private System.String _OBJECT_NAME;
        private System.String _OBJECT_TYPE;
        private System.String _CLASS_NAME;
        private System.String _MSSQL_CS_METHOD_NAME;
        private System.String _MSSQL_CS_METHOD_TYPE;
        private System.String _CS;

        private List<MSSQL_CS_ATTRIBUTE> _ATTRIBUTE_LIST_1;
        private List<MSSQL_CS_ATTRIBUTE> _ATTRIBUTE_LIST_2;

        //private CS_METHOD_OPTION_DEFAULT _OPTION_DEFAULT;
        //private CS_METHOD_OPTION_CUSTOM _CS_METHOD_OPTION_CUSTOM;


        //public CS_METHOD_OPTION_DEFAULT OPTION_DEFAULT
        //{
        //    get { return _OPTION_DEFAULT; }
        //    set { _OPTION_DEFAULT = value; }
        //}
        //public CS_METHOD_OPTION_CUSTOM CS_METHOD_OPTION_CUSTOM
        //{
        //    get { return _CS_METHOD_OPTION_CUSTOM; }
        //    set { _CS_METHOD_OPTION_CUSTOM = value; }
        //}

        public System.Boolean CUSTOM_METHOD
        {
            get { return _CUSTOM_METHOD; }
            set { _CUSTOM_METHOD = value; }
        }

        public List<MSSQL_CS_ATTRIBUTE> ATTRIBUTE_LIST_1
        {
            get { return _ATTRIBUTE_LIST_1; }
            set { _ATTRIBUTE_LIST_1 = value; }
        }
        public List<MSSQL_CS_ATTRIBUTE> ATTRIBUTE_LIST_2
        {
            get { return _ATTRIBUTE_LIST_2; }
            set { _ATTRIBUTE_LIST_2 = value; }
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
        public System.String OBJECT_TYPE
        {
            get { return _OBJECT_TYPE; }
            set { _OBJECT_TYPE = value; }
        }

        public System.String CLASS_NAME
        {
            get { return _CLASS_NAME; }
            set { _CLASS_NAME = value; }
        }



        public System.String MSSQL_CS_METHOD_NAME
        {
            get { return _MSSQL_CS_METHOD_NAME; }
            set { _MSSQL_CS_METHOD_NAME = value; }
        }

        public System.String MSSQL_CS_METHOD_TYPE
        {
            get { return _MSSQL_CS_METHOD_TYPE; }
            set { _MSSQL_CS_METHOD_TYPE = value; }
        }

        public System.String CS
        {
            get { return _CS; }
            set { _CS = value; }
        }
    }
//}
