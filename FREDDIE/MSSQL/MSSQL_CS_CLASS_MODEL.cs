using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 
   public class MSSQL_CS_CLASS_MODEL
    {
    private String _CS_CLASS_NAME;
    private String _CS_FILE_NAME;
    private String _CS_FILE_FOLDER;
    private String _CS_SOURCE_CODE;
    private List<MSSQL_CS_METHOD> _LIST_MSSQL_CS_METHOD;
    private CS_CLASS_TYPE _CLASS_TYPE;
    public enum CS_CLASS_TYPE
    {
        MODEL,
        READ,
        IUD,
        GUI,
    }

    public CS_CLASS_TYPE CLASS_TYPE
    {
        get { return _CLASS_TYPE; }
        set { _CLASS_TYPE = value; }
    }
    public System.String CS_CLASS_NAME
    {
        get { return _CS_CLASS_NAME; }
        set { _CS_CLASS_NAME = value; }
    }



    public System.String CS_FILE_FOLDER
    {
        get { return _CS_FILE_FOLDER; }
        set { _CS_FILE_FOLDER = value; }
    }

    public System.String CS_FILE_NAME
    {
        get { return _CS_FILE_NAME; }
        set { _CS_FILE_NAME = value; }
    }
    public System.String CS_SOURCE_CODE
    {
        get { return _CS_SOURCE_CODE; }
        set { _CS_SOURCE_CODE = value; }
    }

    public List<MSSQL_CS_METHOD> LIST_MSSQL_CS_METHOD
    {
        get { return _LIST_MSSQL_CS_METHOD; }
        set { _LIST_MSSQL_CS_METHOD = value; }
    }



}

