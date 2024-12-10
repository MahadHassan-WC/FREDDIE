using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FREDDIE
{
 public   class FREDDIE_COLUMN_MATRIX
    {
        private string _SERVER_NAME;
        private string _DATABASE_NAME;
        private string _OBJECT_NAME;
        private int _OBJECT_ID;
        private int _COLUMN_ID;
        private string _OBJECT_TYPE;
        private string _COLUMN_NAME;
        private string _DB_DATA_TYPE;
        private string _CS_DATA_TYPE;
        private string _DB_PARM_VALUE;
        private string _CS_PARM_VALUE;
        private string _UPDATE_YN;
        private string _INSERT_YN;
        private string _PK_YN;
        private int _IDENTITY_FIELD_YN;



        ////THESE ARE PRIVATE DATE TIME VARIABLE NAMES THAT CAN BE USED IN A 'BETWEEN' STATEMENT IN SQL (i.e BETWEEN DATENAME1 AND DATENAME2) 


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
        public string OBJECT_NAME
        {
            get
            {
                return _OBJECT_NAME;
            }
            set
            {
                _OBJECT_NAME = value;
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
        public int COLUMN_ID
        {
            get
            {
                return _COLUMN_ID;
            }
            set
            {
                _COLUMN_ID = value;
            }
        }
        public string OBJECT_TYPE
        {
            get
            {
                return _OBJECT_TYPE;
            }
            set
            {
                _OBJECT_TYPE = value;
            }
        }
        public string COLUMN_NAME
        {
            get
            {
                return _COLUMN_NAME;
            }
            set
            {
                _COLUMN_NAME = value;
            }
        }
        public string DB_DATA_TYPE
        {
            get
            {
                return _DB_DATA_TYPE;
            }
            set
            {
                _DB_DATA_TYPE = value;
            }
        }
        public string CS_DATA_TYPE
        {
            get
            {
                return _CS_DATA_TYPE;
            }
            set
            {
                _CS_DATA_TYPE = value;
            }
        }
        public string DB_PARM_VALUE
        {
            get
            {
                return _DB_PARM_VALUE;
            }
            set
            {
                _DB_PARM_VALUE = value;
            }
        }
        public string CS_PARM_VALUE
        {
            get
            {
                return _CS_PARM_VALUE;
            }
            set
            {
                _CS_PARM_VALUE = value;
            }
        }
        public string UPDATE_YN
        {
            get
            {
                return _UPDATE_YN;
            }
            set
            {
                _UPDATE_YN = value;
            }
        }
        public string INSERT_YN
        {
            get
            {
                return _INSERT_YN;
            }
            set
            {
                _INSERT_YN = value;
            }
        }
        public string PK_YN
        {
            get
            {
                return _PK_YN;
            }
            set
            {
                _PK_YN = value;
            }
        }
        public int IDENTITY_FIELD_YN
        {
            get
            {
                return _IDENTITY_FIELD_YN;
            }
            set
            {
                _IDENTITY_FIELD_YN = value;
            }
        }
        ////THESE ARE PUBLIC DATE TIME PROPERTIES THAT CAN BE USED IN A 'BETWEEN' STATEMENT IN SQL (i.e BETWEEN DATENAME1 AND DATENAME2) 

    }
}
