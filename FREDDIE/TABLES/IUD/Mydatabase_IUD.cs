///////IUD CLASS CREATED ON 09/25/2019 9:39:54 AM

public class Mydatabase_IUD : dbUTL
{
	private const System.String DEFAULT_OBJECT_NAME ="MYDATABASE";
	private const System.String DEFAULT_DATABASE_NAME ="FREDDIE";
	public event EVENT_HANDLER INFORMATION_EVENT_VERBOSE_LOCAL;
	public event EVENT_HANDLER INFORMATION_EVENT_LOCAL;
	public event EVENT_HANDLER WARNING_EVENT_LOCAL;
	public event EVENT_HANDLER ERROR_EVENT_LOCAL;
	private void LOG_INFORMATION_EVENT_VERBOSE_LOCAL(string MSG)
	{
	if (INFORMATION_EVENT_VERBOSE_LOCAL != null)
	{
	INFORMATION_EVENT_VERBOSE_LOCAL(this, new EVENT_HANDLER_SUPER(MSG));
	}
	}
	private void LOG_INFORMATION_EVENT_LOCAL(string MSG)
	{
	if (INFORMATION_EVENT_LOCAL != null)
	{
	INFORMATION_EVENT_LOCAL(this, new EVENT_HANDLER_SUPER(MSG));
	}
	}
	private void LOG_WARNING_EVENT_LOCAL(string MSG)
	{
	if (WARNING_EVENT_LOCAL != null)
	{
	WARNING_EVENT_LOCAL(this, new EVENT_HANDLER_SUPER(MSG));
	}
	}
	private void LOG_ERROR_EVENT_LOCAL(string MSG)
	{
	if (ERROR_EVENT_LOCAL != null)
	{
	ERROR_EVENT_LOCAL(this, new EVENT_HANDLER_SUPER(MSG));
	}
	}
    public System.Boolean DEL_MYDATABASE_BY_SERVER_ID(MydatabaseModel o)
    {
        try
        {
            cmd = new System.Data.SqlClient.SqlCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "usp_DEL_MYDATABASE_BY_SERVER_ID";
            /////TABLE COLUMNS TO DELETE BY////
            cmd.Parameters.AddWithValue("@SERVER_ID", o.SERVER_ID);
            LOG_INFORMATION_EVENT_VERBOSE_LOCAL(GET_SQLSERVER_COMMAND_VALUES(cmd));
            return RUN_SQL_COMMAND(DEFAULT_DATABASE_NAME, DEFAULT_OBJECT_NAME);
        }
        catch (System.Data.SqlClient.SqlException SqlException)
        {
            LOG_ERROR_EVENT_LOCAL(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + SqlException.Message);
            return false;
        }
        finally
        {
            cmd = null;
            LOG_INFORMATION_EVENT_VERBOSE_LOCAL(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
        }
    } /// end of CUSTOM DELETE

    public System.Boolean DELETE_A_ROW_BY_PK(MydatabaseModel o)
    {
        try
        {
            cmd = new System.Data.SqlClient.SqlCommand("usp_DEL_BY_PK_MYDATABASE");
            /////PRIMARY KEY////
            cmd.Parameters.AddWithValue("@SERVER_ID", o.SERVER_ID);
            cmd.Parameters.AddWithValue("@DATABASE_NAME", o.DATABASE_NAME);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            LOG_INFORMATION_EVENT_VERBOSE_LOCAL(GET_SQLSERVER_COMMAND_VALUES(cmd));
            return RUN_SQL_COMMAND(DEFAULT_DATABASE_NAME, DEFAULT_OBJECT_NAME);
        }
        catch (System.Data.SqlClient.SqlException SqlException)
        {
            LOG_ERROR_EVENT_LOCAL(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + SqlException.Message);
            return false;
        }
        finally
        {
            LOG_INFORMATION_EVENT_VERBOSE_LOCAL(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            cmd = null;
        }
    }
    public System.Boolean DELETE_ALL_ROWS()
	{
		try
		{
			cmd = new System.Data.SqlClient.SqlCommand("usp_DEL_ALL_MYDATABASE");
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			LOG_INFORMATION_EVENT_VERBOSE_LOCAL(GET_SQLSERVER_COMMAND_VALUES(cmd));
			return RUN_SQL_COMMAND(DEFAULT_DATABASE_NAME, DEFAULT_OBJECT_NAME);
		}
		catch (System.Data.SqlClient.SqlException SqlException)
		{
			LOG_ERROR_EVENT_LOCAL(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + SqlException.Message);
			return false;
		}
		finally
		{
			LOG_INFORMATION_EVENT_VERBOSE_LOCAL(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
			cmd = null;
		}
	}

	public System.Boolean INSERT_A_ROW(MydatabaseModel o)
	{
		try
		{
			cmd = new System.Data.SqlClient.SqlCommand("usp_INS_MYDATABASE");
			/////TABLE COLUMNS////
			cmd.Parameters.AddWithValue("@SERVER_ID",o.SERVER_ID);
			cmd.Parameters.AddWithValue("@DATABASE_NAME",o.DATABASE_NAME);
			///// cmd.Parameters.AddWithValue("@INSDT",o.INSDT); RESERVED COLUMN NAME AUTO GENERATED BY SQL SERVER
			///// cmd.Parameters.AddWithValue("@INSOPID",o.INSOPID); RESERVED COLUMN NAME AUTO GENERATED BY SQL SERVER

			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			LOG_INFORMATION_EVENT_VERBOSE_LOCAL(GET_SQLSERVER_COMMAND_VALUES(cmd));
			return RUN_SQL_COMMAND(DEFAULT_DATABASE_NAME, DEFAULT_OBJECT_NAME);
		}
		catch (System.Data.SqlClient.SqlException SqlException)
		{
			LOG_ERROR_EVENT_LOCAL(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + SqlException.Message);
			return false;
		}
		finally
		{
			 LOG_INFORMATION_EVENT_VERBOSE_LOCAL(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
			 cmd = null;
		}
	}
}   /////END OF IUD CLASS MydatabaseModel


