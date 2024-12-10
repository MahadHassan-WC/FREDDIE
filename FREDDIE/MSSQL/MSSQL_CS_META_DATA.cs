using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

 
   
    public  class MSSQL_CS_META_DATA : EVENT_HANDLER_SUPER
    {

        public event EVENT_HANDLER INFORMATION_EVENT;
        public event EVENT_HANDLER INFORMATION_EVENT_VERBOSE;
        public event EVENT_HANDLER WARNING_EVENT;
        public event EVENT_HANDLER ERROR_EVENT;


      
        const string SQL_DATABASES_THIS_SERVER = @"
                select 
                        name, 
                        dbid 
                from 
                        sysdatabases 
                where 
                        name not in('master','model','msdb','tempdb')
                order by 
                        name ";



        const string SQL_TABLES_THIS_DATABASE = @"
                select
                    object_id,
                     name as 'NAME',
                     name as 'TABLE_NAME',
                    TYPE,  
                    TYPE        AS 'TABLE_TYPE'  
                from 
                    sys.tables
                ORDER BY
                    NAME";





        const string SQL_VIEWS_THIS_DATABASE = @"
                select
                    object_id,
                    name as 'NAME',
                    name as 'VIEW_NAME',
                    TYPE,  
                    TYPE        AS 'VIEW_TYPE'  
                from 
                    sys.views
                ORDER BY
                    NAME";






        const string SQL_COLUMNS_IN_TABLE_BY_OBJECT_ID = @"
            select
                b.column_id  AS COLUMN_ID, 
                a.object_id  AS OBJECT_ID, 
                 a.name as 'TABLE_NAME', 
                a.TYPE AS 'TABLE_TYPE' , 
                b.Name as 'COLUMN_NAME',
                convert( int,b.is_identity) as 'IS_IDENTITY', 
                c.Name   as 'TYPE_NAME' ,
                b.is_nullable   as IS_NULLABLE,
                b.max_length    AS MAX_LENGTH,
                b.precision     AS PRECISION,
                b.scale         AS SCALE,
                s.TABLE_SCHEMA,
                    ISNULL(  IDENT_SEED(s.TABLE_SCHEMA + '.' + s.TABLE_NAME) ,0) as IDENT_SEED,
                    ISNULL(  IDENT_INCR(s.TABLE_SCHEMA + '.' + s.TABLE_NAME),0) as IDENT_INCR
            from  
                sys.tables a,
                sys.columns b,
                sys.systypes C ,
		        INFORMATION_SCHEMA.TABLES s
            where   
                a.object_id = b.Object_id and 
                B.system_type_id = c.xtype and 
                s.TABLE_NAME = a.name and
                a.object_id = @OBJECT_ID
                AND c.Name <> 'sysname'
            order by b.column_id";

        const string SQL_COLUMNS_IN_VIEW_BY_OBJECT_ID = @"
              select
                  b.column_id, 
                  a.object_id, 
                   a.name as 'VIEW_NAME', 
                  a.TYPE AS 'VIEW_TYPE' , 
                  b.Name as 'COLUMN_NAME',
                  convert( int,b.is_identity) as 'IS_IDENTITY', 
                  c.Name as 'TYPE_NAME' ,
				    b.is_nullable   as IS_NULLABLE,
					b.max_length    AS MAX_LENGTH,
					b.precision     AS PRECISION,
					b.scale         AS SCALE ,
					s.TABLE_SCHEMA
            from
				sys.views a,
				sys.columns b,
				sys.systypes C  ,
				INFORMATION_SCHEMA.VIEWS s
            where   
                  a.object_id      = b.Object_id and
                  B.system_type_id = c.xtype and 
				  s.TABLE_NAME = a.name  and
	              a.object_id = @OBJECT_ID
                  order by b.column_id";
        
        




        private const string SQL_PRIMARY_KEYS_IN_TABLE_BY_OBJECT_ID = @"
                    SELECT 
                        Col.Column_Name as 'PK_COLUMN_NAME',
                        Col.Column_Name as 'COLUMN_NAME',
						t.object_id,
						col.TABLE_NAME
						 
                    from 
                        INFORMATION_SCHEMA.TABLE_CONSTRAINTS Tab,
                        INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE Col ,
						sys.tables t
                    WHERE 
                        Col.Constraint_Name = Tab.Constraint_Name AND 
                        Col.Table_Name = Tab.Table_Name AND 
                        Constraint_Type = 'PRIMARY KEY'  AND 
                        Col.Table_Name = t.name and
						t.object_id = @OBJECT_ID";












        private   SqlConnection GET_CONNECTION_TO_DATABASE(string SERVER_NAME, string DATABASE_NAME)
        {
            try
            {
                SqlConnectionStringBuilder bldr = new SqlConnectionStringBuilder();
                {
                  
                    bldr.DataSource = SERVER_NAME;
                    bldr.InitialCatalog = DATABASE_NAME;
                }
                bldr.IntegratedSecurity = true;
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = bldr.ConnectionString;
                bldr = null;
                return conn;
            }
            catch (Exception ex)
            {
                
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return null;
            }
            finally
            {
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }
        }
       
        private DataSet GET_DATASET(string SERVER_NAME, string DATABASE_NAME,  string SQL )
        {
            try
            { 
                SqlConnection DATABASE_CONNECTION =  GET_CONNECTION_TO_DATABASE(SERVER_NAME, DATABASE_NAME);
                SqlDataAdapter DATA_ADAPTER = new SqlDataAdapter(SQL, DATABASE_CONNECTION);
                DATA_ADAPTER.SelectCommand.CommandTimeout = 60000;
                DataSet DATA_SET = new DataSet(DATABASE_NAME);
                DATA_ADAPTER.Fill(DATA_SET);
                DATA_ADAPTER.Dispose();
                return DATA_SET;
            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message + Environment.NewLine + "DATABASE NAME: " + DATABASE_NAME + Environment.NewLine + "SQL: " + SQL);
                return null;
            }
            finally
            {
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }
        }
        private DataSet GET_DATASET(string SERVER_NAME, string DATABASE_NAME, string SQL, int OBJECT_ID)
        {
            try
            {
                SqlConnection DATABASE_CONNECTION = GET_CONNECTION_TO_DATABASE(SERVER_NAME, DATABASE_NAME);
                SqlDataAdapter DATA_ADAPTER = new SqlDataAdapter(SQL, DATABASE_CONNECTION);
                DATA_ADAPTER.SelectCommand.CommandTimeout = 60000;
                DATA_ADAPTER.SelectCommand.Parameters.AddWithValue("@OBJECT_ID", OBJECT_ID);
                DataSet DATA_SET = new DataSet(DATABASE_NAME);
                DATA_ADAPTER.Fill(DATA_SET);
                DATA_ADAPTER.Dispose();
                return DATA_SET;
            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message + Environment.NewLine + "DATABASE NAME: " + DATABASE_NAME   +Environment.NewLine + "SQL: " + SQL);
                return null;
            }
            finally
            {
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }
        }



        private DataTable GET_SCHEMA_TABLE(string SERVER_NAME, string DATABASE_NAME, string SQL)
        {
            try
            {
                SqlConnection DATABASE_CONNECTION = GET_CONNECTION_TO_DATABASE(SERVER_NAME, DATABASE_NAME);
                DATABASE_CONNECTION.Open();
                SqlCommand DATABASE_COMMAND = new SqlCommand(SQL, DATABASE_CONNECTION);
                SqlDataReader DATA_READER = DATABASE_COMMAND.ExecuteReader(CommandBehavior.SchemaOnly);
                DataTable DATA_TABLE = DATA_READER.GetSchemaTable();
                return DATA_TABLE;
            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message + Environment.NewLine + "DATABASE NAME: " + DATABASE_NAME);
                return null;
            }
            finally
            {
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }

        }

        public Boolean CREATE_STORED_PROCEDURE_ON_SERVER(string SERVER_NAME, string DATABASE_NAME, string SQL)
        {
            try
            {
                SqlConnection DATABASE_CONNECTION = GET_CONNECTION_TO_DATABASE(SERVER_NAME, DATABASE_NAME);
                DATABASE_CONNECTION.Open();
                SqlCommand DATABASE_COMMAND = new SqlCommand(SQL, DATABASE_CONNECTION);
                DATABASE_COMMAND.ExecuteNonQuery();
               
                return true;
            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message + Environment.NewLine + "DATABASE NAME: " + DATABASE_NAME);
                return false;
            }
            finally
            {
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }

        }




        public DataSet GET_DATABASES_THIS_SERVER_DS(string SERVER_NAME)
        {
            try
            {
                return GET_DATASET(SERVER_NAME, "master",  SQL_DATABASES_THIS_SERVER );
            }
            catch(Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return null;
            }
            finally
            {
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }
        }


        

        public DataSet GET_TABLES_THIS_DATABASE_DS(string SERVER_NAME, string DATABASE_NAME)
        {
            try
            {
                return GET_DATASET(SERVER_NAME, DATABASE_NAME, SQL_TABLES_THIS_DATABASE);
            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return null;
            }
            finally
            {
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }
        }
        public DataSet GET_VIEWS_THIS_DATABASE_DS(string SERVER_NAME, string DATABASE_NAME)
        {
            try
            {
                return GET_DATASET(SERVER_NAME, DATABASE_NAME, SQL_VIEWS_THIS_DATABASE);
            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return null;
            }
            finally
            {
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }
        }
        public DataSet GET_COLUMNS_IN_TABLE_DS(string SERVER_NAME, string DATABASE_NAME, int OBJECT_ID)
        {
            try
            {
                return GET_DATASET(SERVER_NAME, DATABASE_NAME, SQL_COLUMNS_IN_TABLE_BY_OBJECT_ID, OBJECT_ID);
            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return null;
            }
            finally
            {
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }
        }
        public DataSet GET_COLUMNS_IN_VIEW_DS(string SERVER_NAME, string DATABASE_NAME, int OBJECT_ID)
        {
            try
            {
                return GET_DATASET(SERVER_NAME, DATABASE_NAME, SQL_COLUMNS_IN_VIEW_BY_OBJECT_ID, OBJECT_ID);
            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return null;
            }
            finally
            {
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }
        }
        public DataSet GET_PRIMARY_KEYS_IN_TABLE_DS(string SERVER_NAME, string DATABASE_NAME, int OBJECT_ID)
        {
            try
            {
                return GET_DATASET(SERVER_NAME, DATABASE_NAME, SQL_PRIMARY_KEYS_IN_TABLE_BY_OBJECT_ID, OBJECT_ID);
            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return null;
            }
            finally
            {
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }
        }

        public int TABLE_HAS_PK(string SERVER_NAME, string DATABASE_NAME, int OBJECT_ID)
        {
            try
            {
                return GET_DATASET(SERVER_NAME, DATABASE_NAME, SQL_PRIMARY_KEYS_IN_TABLE_BY_OBJECT_ID, OBJECT_ID).Tables[0].Rows.Count;
            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return -1;
            }
            finally
            {
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }
        }

        public Boolean STORED_PROCEDURE_EXISTS(string SERVER_NAME, string DATABASE_NAME, string OBJECT_NAME)
        {
            try
            {
                

                String SQL = "SELECT * FROM sys.objects WHERE type = 'P' AND name = '" + OBJECT_NAME + "'";
                DataSet DS = GET_DATASET(SERVER_NAME, DATABASE_NAME, SQL);
                if(DS.Tables[0].Rows.Count > 0 )
                {
                    return true;
                }
                else
                {
                    return false;
                }
              
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public int TABLE_HAS_INDENTITY_FIELD_IN_PK(string SERVER_NAME, string DATABASE_NAME, int OBJECT_ID)
        {
            try
            {
                int IDENTITY_COUNT = 0;
                DataSet PK_DS = GET_DATASET(SERVER_NAME, DATABASE_NAME, SQL_PRIMARY_KEYS_IN_TABLE_BY_OBJECT_ID, OBJECT_ID);
                DataSet COLUMNS_DS = GET_DATASET(SERVER_NAME, DATABASE_NAME, SQL_COLUMNS_IN_TABLE_BY_OBJECT_ID, OBJECT_ID);

                foreach(DataRow PK_ROW in PK_DS.Tables[0].Rows)
                {
                    foreach(DataRow COL_ROW in COLUMNS_DS.Tables[0].Rows)
                    if(PK_ROW["PK_COLUMN_NAME"].ToString () == COL_ROW["COLUMN_NAME"].ToString())

                    {
                            if(COL_ROW["IS_IDENTITY"].ToString()== "1")
                            {
                                IDENTITY_COUNT++;
                            }
                           
                    }
                }



                return IDENTITY_COUNT;
            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message);
                return -1;
            }
            finally
            {
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }
        }


        public DataTable GET_SCHEMA_TABLE_DT(string SERVER_NAME, string DATABASE_NAME, string OBJECT_NAME)
        {
            try
            {
                return GET_SCHEMA_TABLE(SERVER_NAME, DATABASE_NAME, "SELECT * FROM " + OBJECT_NAME);
            }
            catch (Exception ex)
            {
                LOG_ERROR_EVENT(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|" + ex.Message + Environment.NewLine + "DATABASE NAME:" + DATABASE_NAME);
                return null;
            }
            finally
            {
                LOG_INFO_EVENT_VERBOSE(this.GetType().Name + "|" + System.Reflection.MethodBase.GetCurrentMethod().Name + "|finally");
            }
        }



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
        private void LOG_INFO_EVENT_VERBOSE(string MSG)
        {
             
            if (INFORMATION_EVENT_VERBOSE != null)
            {
                INFORMATION_EVENT_VERBOSE(this, new EVENT_HANDLER_SUPER(MSG));
            }
        }

    }
 
