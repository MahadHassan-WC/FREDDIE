using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

public class dbUTL : EVENT_HANDLER_SUPER
{
    private const String T1 = "\t";
    private const String T2 = "\t\t";
    private const String T3 = "\t\t\t";
    private const String T4 = "\t\t\t\t";
    public event EVENT_HANDLER INFORMATION_EVENT;
    public event EVENT_HANDLER INFORMATION_EVENT_VERBOSE;
    public event EVENT_HANDLER WARNING_EVENT;
    public event EVENT_HANDLER ERROR_EVENT;
    public event EVENT_HANDLER ROWS_AFFECTED;


    protected SqlCommand cmd;
    protected SqlDataAdapter da;



    private string GET_SERVER()
    {
        return @"localhost";
    }
    #region LOG AREA





    //private void LOG_ROWS_AFFECTED(Int32 COUNT)
    //{
    //    if (ROWS_AFFECTED != null)
    //    {
    //        ROWS_AFFECTED(this, new EVENT_HANDLER_SUPER(COUNT));
    //    }
    //    //if (INFORMATION_EVENT_VERBOSE != null)
    //    //{
    //    //    INFORMATION_EVENT_VERBOSE(this, new EVENT_HANDLER_SUPER(MSG));
    //    //}
    //}




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
    private void LOG_INFO_EVENT_VERBOSE(string MSG)
    {

        if (INFORMATION_EVENT_VERBOSE != null)
        {
            INFORMATION_EVENT_VERBOSE(this, new EVENT_HANDLER_SUPER(MSG));
        }
    }
    #endregion

    #region READ AREA
    protected DataSet GET_DATASET(string DATABASE_NAME, string OBJECT_NAME, string SQL_STATEMENT)
    {
        try
        {
            string SERVER_NAME = GET_SERVER();
            if (!OBJECT_EXISTS(SERVER_NAME, DATABASE_NAME, OBJECT_NAME))
            {
                return null;
            }
            SqlConnectionStringBuilder bldr = new SqlConnectionStringBuilder();
            {
                bldr.DataSource = SERVER_NAME;
                bldr.InitialCatalog = DATABASE_NAME;
            }
            bldr.IntegratedSecurity = true;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = bldr.ConnectionString;
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(SQL_STATEMENT, conn);
            DataSet ds = new DataSet(DATABASE_NAME);
            da.Fill(ds, OBJECT_NAME);
            da.Dispose();
            return ds;
        }
        catch (SqlException SqlException)
        {
            LOG_ERROR_EVENT("DATABASE SUPER CLASS|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + SqlException.Message);
            return null;
        }
        finally
        {
            LOG_INFO_EVENT_VERBOSE("DATABASE SUPER CLASS|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
        }

    }
    protected DataSet GET_SP_DATASET(string DATABASE_NAME,   SqlCommand cmd)
    {
        try
        {
            SqlConnectionStringBuilder bldr = new SqlConnectionStringBuilder();
            {
                bldr.DataSource = GET_SERVER();
                bldr.InitialCatalog = DATABASE_NAME;
            }
            
            bldr.IntegratedSecurity = true;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = bldr.ConnectionString;
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter( );
            cmd.Connection = conn;
            da.SelectCommand = cmd;
            DataSet ds = new DataSet(DATABASE_NAME);
            da.Fill(ds);
            da.Dispose();
            conn.Close();
            cmd = null;
            return ds;
        }
        catch (SqlException SqlException)
        {
            LOG_ERROR_EVENT("DATABASE SUPER CLASS|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + SqlException.Message);
            return null;
        }
        finally
        {
            LOG_INFO_EVENT_VERBOSE("DATABASE SUPER CLASS|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
        }

    }
    protected DataSet GET_SP_DATASET(string DATABASE_NAME, SqlCommand cmd, String ServerName)
    {
        try
        {
            SqlConnectionStringBuilder bldr = new SqlConnectionStringBuilder();
            {
                bldr.DataSource = ServerName;
                bldr.InitialCatalog = DATABASE_NAME;
            }

            bldr.IntegratedSecurity = true;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = bldr.ConnectionString;
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.Connection = conn;
            da.SelectCommand = cmd;
            DataSet ds = new DataSet(DATABASE_NAME);
            da.Fill(ds);
            da.Dispose();
            conn.Close();
            cmd = null;
            return ds;
        }
        catch (SqlException SqlException)
        {
            LOG_ERROR_EVENT("DATABASE SUPER CLASS|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + SqlException.Message);
            return null;
        }
        finally
        {
            LOG_INFO_EVENT_VERBOSE("DATABASE SUPER CLASS|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
        }

    }
    protected DataSet GET_DATASET_WITH_PARMS(string DATABASE_NAME, string OBJECT_NAME, string SQL_STATEMENT  )
    {
        da.SelectCommand.CommandText= SQL_STATEMENT;
        cmd.CommandText = SQL_STATEMENT;
        SqlConnectionStringBuilder bldr = new SqlConnectionStringBuilder();
        {

            bldr.DataSource = GET_SERVER(); ;
            bldr.InitialCatalog = DATABASE_NAME;
        }
        bldr.IntegratedSecurity = true;
        SqlConnection conn = new SqlConnection( );
        
        conn.ConnectionString = bldr.ConnectionString;
        
        conn.Open();
        cmd.Connection = conn;
        
        
       


        DataSet ds = new DataSet(DATABASE_NAME);
        da.Fill(ds, OBJECT_NAME);
        da.Dispose();
        return ds;


    }

    #endregion



    private bool OBJECT_EXISTS(string SERVER_NAME, string DATABASE_NAME,   string OBJECT_NAME)
    {
        string SQL = "SELECT * FROM SYSOBJECTS WHERE NAME ='" + OBJECT_NAME + "'";
        SqlConnectionStringBuilder bldr = new SqlConnectionStringBuilder();
        {

            bldr.DataSource = SERVER_NAME;
            bldr.InitialCatalog = DATABASE_NAME;
        }
        bldr.IntegratedSecurity = true;
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = bldr.ConnectionString;
        SqlDataAdapter da = new SqlDataAdapter(SQL, conn);
        DataSet ds = new DataSet(DATABASE_NAME);
        da.Fill(ds, OBJECT_NAME);
        da.Dispose();
        conn.Open();
        conn.Close();
        conn = null;
        bldr = null;
        if(ds.Tables[0].Rows.Count ==0)
        {
            return false;
        }
        else
        {
            return true;
        }
      
    }
    protected Boolean RUN_SQL_COMMAND(string DATABASE_NAME,  string OBJECT_NAME )
    {
        try
        {
            Int32 rows = 0;
            string SERVER_NAME = GET_SERVER();
            if (!OBJECT_EXISTS(SERVER_NAME,DATABASE_NAME,OBJECT_NAME))
            {
                return false;
            }
            SqlConnectionStringBuilder bldr = new SqlConnectionStringBuilder();
            {
                bldr.DataSource = SERVER_NAME;
                bldr.InitialCatalog = DATABASE_NAME;
            }
            bldr.IntegratedSecurity = true;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = bldr.ConnectionString;
            cmd.Connection = conn;
            cmd.CommandTimeout = 900000;
          //  cmd.CommandText = SQL_STATEMENT;
            
            conn.Open();
            rows = cmd.ExecuteNonQuery();
            conn.Close();
            conn = null;
          
            bldr = null;
         
            return true;
        }
        catch (SqlException SqlException)
        {
            LOG_ERROR_EVENT("DATABASE SUPER CLASS|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + SqlException.Message);
            return false;
        }
        finally
        {
            LOG_INFO_EVENT_VERBOSE("DATABASE SUPER CLASS|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
        }
    }



    protected String GET_SQLSERVER_COMMAND_VALUES(SqlCommand cmd)
    {
        String MyString = cmd.CommandText + Environment.NewLine;
        if (cmd.Parameters.Count == 0)
        {
            MyString += "NO SqlCommand parameters were found!!!" + Environment.NewLine;
        }
        else
        {
            MyString += cmd.Parameters.Count +  ") SqlCommand parameters were found" + Environment.NewLine + "NAME =  VALUE:" + Environment.NewLine;
        }
        foreach (SqlParameter Para in cmd.Parameters)
        {
            MyString += T2 +    ((string)Para.ParameterName)  + " = " +((string)Para.Value.ToString()) + Environment.NewLine;
        }

        return MyString;
    }




    










}

