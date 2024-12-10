using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;



public delegate void PROGRAM_LOG_EVENT_HANDLER(object source, oPROGRAM_LOG e);


public class oPROGRAM_LOG : EventArgs
{


   
    //public MyEventArgs()
    //{
    //}
    //public MyEventArgs(string Text)
    //{
    //    EventInfo = Text;
    //}
    //public string GetInfo()
    //{
    //    return EventInfo;
    //}



    private String _LOG_FOLDER;
    private String _APPLICATION_NAME;
    private String _HOST_NAME;
    private String _USER_NAME;

    private String _DATABASE_SERVER_NAME;
    private String _DATABASE_NAME;
    private String _DATABASE_OBJECT_NAME;
    private String _DATABASE_OBJECT_TYPE;
    private String _DATABASE_CONNECTION_STRING;
    private String _DATABASE_CONNECTION_DETAILS;

    private String _DATABASE_COMMAND_SQL;
    private String _DATABASE_COMMAND_DETAILS;
    private String _DATABASE_MESSAGE;

    private String _DATABASE_DML_TYPE;
    private String _DATABASE_DDL_TYPE;
    private String _DATABASE_METHOD_NAME;
    private String _DATABASE_CLASS_NAME;

    private Int32 _DATABASE_ROWS_FOUND;
    private Int32 _DATABASE_ROWS_AFFECTED;
    private Int32 _DATABASE_MESSAGE_LEVEL;

    public oPROGRAM_LOG()
    {

        _DATABASE_SERVER_NAME = "NA";
        _DATABASE_NAME = "NA";
        _DATABASE_OBJECT_NAME = "NA";
        _DATABASE_OBJECT_TYPE = "NA";
        _DATABASE_CONNECTION_STRING = "NA";
        _DATABASE_CONNECTION_DETAILS = "NA";
        _DATABASE_COMMAND_SQL = "NA";
        _DATABASE_COMMAND_DETAILS = "NA";
        _DATABASE_MESSAGE = "NA";
        _DATABASE_ROWS_AFFECTED = -100;
        _DATABASE_ROWS_FOUND = 0;
        _DATABASE_MESSAGE_LEVEL = 0;
        _DATABASE_DML_TYPE = "NA";
        _DATABASE_DDL_TYPE = "NA";
        _DATABASE_METHOD_NAME = "NA";
        _DATABASE_CLASS_NAME = "NA";
    }


    public string LOG_DATABASE_ACTIVITY(string MSG, int LEVEL)
    {
        _DATABASE_MESSAGE_LEVEL = LEVEL;
        return DateTime.Now + " " + MSG;
    }
    
    public string LOG_DATABASE_COMMAND_DETAILS(SqlCommand cmd)
    {
        return DateTime.Now + " " + FORMAT_DATABASE_COMMAND_DETAILS(cmd);
    }
    public string LOG_DATABASE_CONNECTION_DETAILS(SqlConnection conn)
    {
        return DateTime.Now + " " + FORMAT_DATABASE_CONNECTION_DETAILS(conn);
    }
    private string FORMAT_DATABASE_COMMAND_DETAILS(SqlCommand cmd)
    {
        _DATABASE_COMMAND_DETAILS = "SqlCommand details: " + Environment.NewLine;
        if (cmd.Parameters.Count > 0)
        {
            if (cmd.Parameters.Count > 1)
            {
                _DATABASE_COMMAND_DETAILS += "There are " + cmd.Parameters.Count + " parameters " + Environment.NewLine;
            }
            else
            {
                _DATABASE_COMMAND_DETAILS += "There is 1 parameter" + Environment.NewLine;
            }
        }
        _DATABASE_COMMAND_DETAILS += cmd.Parameters.Count.ToString() + " SqlParameter details: " + Environment.NewLine;
        foreach (SqlParameter p in cmd.Parameters)
        {
            _DATABASE_COMMAND_DETAILS += "   ParameterName: " + p.ParameterName.ToString();
            _DATABASE_COMMAND_DETAILS += "   Value: " + p.Value.ToString() + Environment.NewLine;
            _DATABASE_COMMAND_DETAILS += "   Direction: " + p.Direction.ToString() + Environment.NewLine;
            _DATABASE_COMMAND_DETAILS += "   DbType: " + p.DbType.ToString() + Environment.NewLine;
            _DATABASE_COMMAND_DETAILS += "   SourceColumn: " + p.SourceColumn.ToString() + Environment.NewLine;
        }
        return _DATABASE_COMMAND_DETAILS;
    }
    public string FORMAT_DATABASE_CONNECTION_DETAILS(SqlConnection conn)
    {
        _DATABASE_CONNECTION_DETAILS= "SqlConnection details: " + Environment.NewLine +
                    "ConnectionString: " + conn.ConnectionString + Environment.NewLine +
                    "ConnectionTimeout: " + conn.ConnectionTimeout.ToString() + Environment.NewLine +
                    "State: " + conn.State + Environment.NewLine +
                    "DataSource: " + conn.DataSource + Environment.NewLine +
                    "Database: " + conn.Database + Environment.NewLine;
        return _DATABASE_CONNECTION_DETAILS;

    }











    public String DATABASE_METHOD_NAME
    {
        get
        {
            return _DATABASE_METHOD_NAME;
        }
        set
        {
            _DATABASE_METHOD_NAME = value;
        }
    }
    public String DATABASE_CLASS_NAME
    {
        get
        {
            return _DATABASE_CLASS_NAME;
        }
        set
        {
            _DATABASE_CLASS_NAME = value;
        }
    }


    public String DATABASE_SERVER_NAME
    {
        get
        {
            return _DATABASE_SERVER_NAME;
        }
        set
        {
            _DATABASE_SERVER_NAME = value;
        }
    }
    public String DATABASE_NAME
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
    public String DATABASE_OBJECT_NAME
    {
        get
        {
            return _DATABASE_OBJECT_NAME;
        }
        set
        {
            _DATABASE_OBJECT_NAME = value;
        }
    }
    public String DATABASE_OBJECT_TYPE
    {
        get
        {
            return _DATABASE_OBJECT_TYPE;
        }
        set
        {
            _DATABASE_OBJECT_TYPE = value;
        }
    }
    public String DATABASE_CONNECTION_STRING
    {
        get
        {
            return _DATABASE_CONNECTION_STRING;
        }
        set
        {
            _DATABASE_CONNECTION_STRING = value;
        }
    }
    public String DATABASE_CONNECTION_DETAILS
    {
        get
        {
            return _DATABASE_CONNECTION_DETAILS;
        }
        set
        {
            _DATABASE_CONNECTION_DETAILS = value;
        }
    }
    public String DATABASE_COMMAND_SQL
    {
        get
        {
            return _DATABASE_COMMAND_SQL;
        }
        set
        {
            _DATABASE_COMMAND_SQL = value;
        }
    }
    public String DATABASE_COMMAND_DETAILS
    {
        get
        {
            return _DATABASE_COMMAND_DETAILS;
        }
        set
        {
            _DATABASE_COMMAND_DETAILS = value;
        }
    }
    public String DATABASE_MESSAGE
    {
        get
        {
            return _DATABASE_MESSAGE;
        }
        set
        {
            _DATABASE_MESSAGE = value;
        }
    }
    public Int32 DATABASE_ROWS_AFFECTED
    {
        get
        {
            return _DATABASE_ROWS_AFFECTED;
        }
        set
        {
            _DATABASE_ROWS_AFFECTED = value;
        }
    }
    public Int32 DATABASE_ROWS_FOUND
    {
        get
        {
            return _DATABASE_ROWS_FOUND;
        }
        set
        {
            _DATABASE_ROWS_FOUND = value;
        }
    }
    public Int32 DATABASE_MESSAGE_LEVEL
    {
        get
        {
            return _DATABASE_MESSAGE_LEVEL;
        }
        set
        {
            _DATABASE_MESSAGE_LEVEL = value;
        }
    }
    public String DATABASE_DML_TYPE
    {
        get
        {
            return _DATABASE_DML_TYPE;
        }
        set
        {
            _DATABASE_DML_TYPE = value;
        }
    }
    public String DATABASE_DDL_TYPE
    {
        get
        {
            return _DATABASE_DDL_TYPE;
        }
        set
        {
            _DATABASE_DDL_TYPE = value;
        }
    }

}

