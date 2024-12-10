using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 
   public class FREDS_MATRIX
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


    #region C# strings
    private System.String _CS_PARM_STRING;
    private System.String _CS_DATA_TYPE;
    private System.String _CS_PRIVATE_VARIABLE;
    private System.String _CS_PUBLIC_PROPERTY;
    private System.String _CS_READER_STRING ;
    #endregion


    #region SQL SERVER  variable names

    private System.String _SQL_IDENTITY_FIELD_YN;
    private System.String _SQL_DATA_TYPE;
    private System.String _SQL_IS_NULLABLE;
    private System.Int32 _SQL_MAX_LENGTH;
    private System.Int32 _SQL_PRECISION;
    private System.Int32 _SQL_SCALE;
    private System.String _SQL_TABLE_SCHEMA;
    private System.Int32 _SQL_IDENT_SEED;
    private System.Int32 _SQL_IDENT_INCR;
    private System.String _SQL_VARIABLE_NAME;
    private System.String _SQL_PROC_PARM_STRING;
    private System.String _SQL_PROC_PARM_YN;

    #endregion


  


    public System.String SQL_IDENTITY_FIELD_YN
    {
        get { return _SQL_IDENTITY_FIELD_YN; }
        set { _SQL_IDENTITY_FIELD_YN = value; }
    }
    public System.String SQL_DATA_TYPE
    {
        get { return _SQL_DATA_TYPE; }
        set { _SQL_DATA_TYPE = value; }
    }
    public System.String SQL_IS_NULLABLE
    {
        get { return _SQL_IS_NULLABLE; }
        set { _SQL_IS_NULLABLE = value; }
    }
    public System.Int32 SQL_MAX_LENGTH
    {
        get { return _SQL_MAX_LENGTH; }
        set { _SQL_MAX_LENGTH = value; }
    }
    public System.Int32 SQL_PRECISION
    {
        get { return _SQL_PRECISION; }
        set { _SQL_PRECISION = value; }
    }
    public System.Int32 SQL_SCALE
    {
        get { return _SQL_SCALE; }
        set { _SQL_SCALE = value; }
    }
    public System.String SQL_TABLE_SCHEMA
    {
        get { return _SQL_TABLE_SCHEMA; }
        set { _SQL_TABLE_SCHEMA = value; }
    }
    public System.Int32 SQL_IDENT_SEED
    {
        get { return _SQL_IDENT_SEED; }
        set { _SQL_IDENT_SEED = value; }
    }
    public System.Int32 SQL_IDENT_INCR
    {
        get { return _SQL_IDENT_INCR; }
        set { _SQL_IDENT_INCR = value; }
    }
    public System.String SQL_VARIABLE_NAME
    {
        get { return _SQL_VARIABLE_NAME; }
        set { _SQL_VARIABLE_NAME = value; }
    }
    public System.String SQL_PROC_PARM_STRING
    {
        get { return _SQL_PROC_PARM_STRING; }
        set { _SQL_PROC_PARM_STRING = value; }
    }

    public System.String SQL_PROC_PARM_YN
    {
        get { return _SQL_PROC_PARM_YN; }
        set { _SQL_PROC_PARM_YN = value; }
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


    public System.String CS_PRIVATE_VARIABLE
    {
        get { return _CS_PRIVATE_VARIABLE; }
        set { _CS_PRIVATE_VARIABLE = value; }
    }
    public System.String CS_PUBLIC_PROPERTY
    {
        get { return _CS_PUBLIC_PROPERTY; }
        set { _CS_PUBLIC_PROPERTY = value; }
    }
    public System.String CS_READER_STRING
    {
        get { return _CS_READER_STRING; }
        set { _CS_READER_STRING = value; }
    }
}

