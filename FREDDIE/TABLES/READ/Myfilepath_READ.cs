///////READ CLASS CREATED ON 09/26/2019 7:35:37 AM

public class Myfilepath_READ : dbUTL
{
	private const System.String DEFAULT_OBJECT_NAME ="MYFILEPATH";
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
	public System.Data.DataSet SEL_ALL_ROWS()
	{
		try
		{
		cmd = new System.Data.SqlClient.SqlCommand();
		cmd.CommandType = System.Data.CommandType.StoredProcedure;
		cmd.CommandText = "usp_SEL_ALL_MYFILEPATH";
			return GET_SP_DATASET(DEFAULT_DATABASE_NAME, cmd   );
		}
		catch (System.Data.SqlClient.SqlException SqlException)
		{
			LOG_ERROR_EVENT_LOCAL(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + SqlException.Message);
			return null;
		}
		finally
		{
			cmd = null;
			 LOG_INFORMATION_EVENT_VERBOSE_LOCAL(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
		}
	} /// end of SEL_ALL_ROWS
	public System.Data.DataSet SEL_BY_PK()
	{
		try
		{
			cmd = new System.Data.SqlClient.SqlCommand();
			cmd.CommandType = System.Data.CommandType.StoredProcedure;
			///// cmd.Parameters.AddWithValue("@INSOPID",o.INSOPID); RESERVED COLUMN NAME AUTO GENERATED BY SQL SERVER

		cmd.CommandText = "usp_SEL_BY_PK_MYFILEPATH";
			return GET_SP_DATASET(DEFAULT_DATABASE_NAME, cmd   );
		}
		catch (System.Data.SqlClient.SqlException SqlException)
		{
			LOG_ERROR_EVENT_LOCAL(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + SqlException.Message);
			return null;
		}
		finally
		{
			 cmd = null;
			 LOG_INFORMATION_EVENT_VERBOSE_LOCAL(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
		}
	} 
}   /////END OF CLASS


