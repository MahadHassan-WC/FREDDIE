using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace FREDDIE
//{
 public   class CS_TABLE_ATTRIBUTE_SUPER
    {


        private System.String _SERVER_NAME;
        private System.String _DATABASE_NAME;
        private System.String _OBJECT_NAME;
        private System.String _OBJECT_TYPE;
        private System.Int32 _OBJECT_ID;
        private System.String _COLUMN_NAME;
        private System.Int32 _COLUMN_ID;
        private System.String _PK_YN;
        private System.String _UPDATE_YN;
        private System.String _INSERT_YN;
        private System.String _RESERVED_YN;



        private System.String _CS_PARM_STRING;
        private System.String _CS_DATA_TYPE;
        private System.String _CS_PRIVATE_VARIABLE_AND_TYPE;
        private System.String _CS_PUBLIC_PROPERTY;




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
        public System.Int32 OBJECT_ID
        {
            get { return _OBJECT_ID; }
            set { _OBJECT_ID = value; }
        }
        public System.String COLUMN_NAME
        {
            get { return _COLUMN_NAME; }
            set { _COLUMN_NAME = value; }
        }
        public System.Int32 COLUMN_ID
        {
            get { return _COLUMN_ID; }
            set { _COLUMN_ID = value; }
        }
        public System.String PK_YN
        {
            get { return _PK_YN; }
            set { _PK_YN = value; }
        }
        public System.String UPDATE_YN
        {
            get { return _UPDATE_YN; }
            set { _UPDATE_YN = value; }
        }
        public System.String INSERT_YN
        {
            get { return _INSERT_YN; }
            set { _INSERT_YN = value; }
        }
        public System.String RESERVED_YN
        {
            get { return _RESERVED_YN; }
            set { _RESERVED_YN = value; }
        }


        public System.String CS_PARM_STRING
        {
            get { return _CS_PARM_STRING; }
            set { _CS_PARM_STRING = value; }
        }

        public System.String CS_DATA_TYPE
        {
            get { return _CS_DATA_TYPE; }
            set { _CS_DATA_TYPE = value; }
        }


        public System.String CS_PRIVATE_VARIABLE_AND_TYPE
        {
            get { return _CS_PRIVATE_VARIABLE_AND_TYPE; }
            set { _CS_PRIVATE_VARIABLE_AND_TYPE = value; }
        }
        public System.String CS_PUBLIC_PROPERTY
        {
            get { return _CS_PUBLIC_PROPERTY; }
            set { _CS_PUBLIC_PROPERTY = value; }
        }



    }
//}
