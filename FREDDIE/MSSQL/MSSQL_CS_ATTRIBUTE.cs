using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace FREDDIE
//{
  public  class MSSQL_CS_ATTRIBUTE : CS_TABLE_ATTRIBUTE_SUPER
    {



      
        private System.String _MSSQL_IDENTITY_FIELD_YN;
        private System.String _MSSQL_DATA_TYPE;
        private System.String _MSSQL_IS_NULLABLE;
        private System.Int32 _MSSQL_MAX_LENGTH;
        private System.Int32 _MSSQL_PRECISION;
        private System.Int32 _MSSQL_SCALE;
        private System.String _MSSQL_TABLE_SCHEMA;
        private System.Int32 _MSSQL_IDENT_SEED;
        private System.Int32 _MSSQL_IDENT_INCR;
        private System.String _MSSQL_PARM_STRING;
        private System.String _MSSQL_PROC_PARM_STRING;








        public System.String MSSQL_IDENTITY_FIELD_YN
        {
            get { return _MSSQL_IDENTITY_FIELD_YN; }
            set { _MSSQL_IDENTITY_FIELD_YN = value; }
        }
        public System.String MSSQL_DATA_TYPE
        {
            get { return _MSSQL_DATA_TYPE; }
            set { _MSSQL_DATA_TYPE = value; }
        }
        public System.String MSSQL_IS_NULLABLE
        {
            get { return _MSSQL_IS_NULLABLE; }
            set { _MSSQL_IS_NULLABLE = value; }
        }
        public System.Int32 MSSQL_MAX_LENGTH
        {
            get { return _MSSQL_MAX_LENGTH; }
            set { _MSSQL_MAX_LENGTH = value; }
        }
        public System.Int32 MSSQL_PRECISION
        {
            get { return _MSSQL_PRECISION; }
            set { _MSSQL_PRECISION = value; }
        }
        public System.Int32 MSSQL_SCALE
        {
            get { return _MSSQL_SCALE; }
            set { _MSSQL_SCALE = value; }
        }
        public System.String MSSQL_TABLE_SCHEMA
        {
            get { return _MSSQL_TABLE_SCHEMA; }
            set { _MSSQL_TABLE_SCHEMA = value; }
        }
        public System.Int32 MSSQL_IDENT_SEED
        {
            get { return _MSSQL_IDENT_SEED; }
            set { _MSSQL_IDENT_SEED = value; }
        }
        public System.Int32 MSSQL_IDENT_INCR
        {
            get { return _MSSQL_IDENT_INCR; }
            set { _MSSQL_IDENT_INCR = value; }
        }
        public System.String MSSQL_PARM_STRING
        {
            get { return _MSSQL_PARM_STRING; }
            set { _MSSQL_PARM_STRING = value; }
        }
        public System.String MSSQL_PROC_PARM_STRING
        {
            get { return _MSSQL_PROC_PARM_STRING; }
            set { _MSSQL_PROC_PARM_STRING = value; }
        }
    }
//}
