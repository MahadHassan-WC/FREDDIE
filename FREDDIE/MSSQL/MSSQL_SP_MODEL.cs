using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 
   public class MSSQL_SP_MODEL
    {
        private String _SP_NAME;
        private String _SP_FILE_NAME;
        private String _SP_FILE_FOLDER;
        private String _SP_SOURCE_CODE_TOP;
        private String _SP_SOURCE_CODE_BODY;
        private Boolean _SP_EXISTS;
        private SQL_STORED_PROCEDURE_TYPE _SP_TYPE;
    public enum SQL_STORED_PROCEDURE_TYPE
    {
        SELECT_ALL,
        SELECT_BY_PK,

        INSERT_A_ROW,
        INSERT_WHERE_NOT_EXISTS,

        UPDATE_A_ROW_BY_PK,
        DELETE_BY_PK,
        DELETE_ALL,


        INSERT_CUSTOM,
        DELETE_CUSTOM,
        SELECT_CUSTOM,
        SELECT_DISTINCT_CUSTOM,
        UPDATE_CUSTOM


    }

    public System.String SP_NAME
    {
        get { return _SP_NAME; }
        set { _SP_NAME = value; }
    }

    public System.String SP_FILE_NAME
    {
        get { return _SP_FILE_NAME; }
        set { _SP_FILE_NAME = value; }
    }

    public System.String SP_FILE_FOLDER
    {
        get { return _SP_FILE_FOLDER; }
        set { _SP_FILE_FOLDER = value; }
    }

    public System.String SP_SOURCE_CODE_TOP
    {
        get { return _SP_SOURCE_CODE_TOP; }
        set { _SP_SOURCE_CODE_TOP = value; }
    }

    public System.String SP_SOURCE_CODE_BODY
    {
        get { return _SP_SOURCE_CODE_BODY; }
        set { _SP_SOURCE_CODE_BODY = value; }
    }

    public System.Boolean SP_EXISTS
    {
        get { return _SP_EXISTS; }
        set { _SP_EXISTS = value; }
    }

    public SQL_STORED_PROCEDURE_TYPE SP_TYPE
    {
        get { return _SP_TYPE; }
        set { _SP_TYPE = value; }
    }

}

