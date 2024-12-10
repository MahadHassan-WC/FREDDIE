using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FREDDIE.Properties;
using System.IO;
namespace FREDDIE
{
    public partial class Form14 : Form
    {
        private const string XML_FILE = @"c:\pete\MyMSSQL_SERVERS.xml";
        private Point original_label_location;
        private Point original_WHERE_listview_location;
        private Point original_COLUMNS_listview_location;
        private const int BIG_LIST_VIEW_HEIGHT = 478;
        private const int SMALL_LIST_VIEW_COLUMNS_HEIGHT = 210;
        private const int SMALL_LIST_VIEW_WHERE_HEIGHT = 243;
        private string CS_READ_PATH;
        private string CS_IUD_PATH;
        private string CS_SQL_PATH;
        private bool bPK = false;
        private bool bProcessing = false;
        private const String _DEFAULT_FOLDER_PATH = @"C:\_FREDDIE\CS\MSSQL\";
        public Form14()
        {
            InitializeComponent();
        }
        private void Form14_Load(object sender, EventArgs e)
        {
            bProcessing = true;
            if (!Directory.Exists(_DEFAULT_FOLDER_PATH))
            {
                Directory.CreateDirectory(_DEFAULT_FOLDER_PATH);
            }
            this.WindowState = FormWindowState.Maximized;
            cboDistinctColumns.Visible = false;
            optTables.Checked = true;
            LoadServers();
           // cboServers.Items.Add("LocalHost");
            cboServers.Text = "";
            SetFormProperties();
            lvColumns.GridLines = true;
            lvColumnsWhere.GridLines = true;
            lvColumns.View = View.Details;
            lvColumnsWhere.View = View.Details;
            lvColumnsWhere.Visible = false;
            lvColumns.Visible = false;
            lblWhereClause.Visible = false;
            lblColumnsToUpdateInsert.Visible = false;
            FormatListViews(lvColumnsWhere);
            FormatListViews(lvColumns);
            original_label_location = lblWhereClause.Location;
            original_WHERE_listview_location = lvColumnsWhere.Location;
            original_COLUMNS_listview_location = lvColumns.Location;
            DisableAllCustomCodeButtons();
            bProcessing = false;
        }
        private List<FREDS_MATRIX> MapListViewTOFreddieMaxtrix(ListView lv)
        {
            List<FREDS_MATRIX> MY_SELECTED_COLUMNS = new List<FREDS_MATRIX>();

            foreach(ListViewItem lvi in lv.Items)
            {
                if(lvi.Checked )
                {
                    FREDS_MATRIX m = new FREDS_MATRIX();



                    //1 SERVER_NAME 
                    //2 DATABASE_NAME 
                    //3 OBJECT_NAME 
                    //4 OBJECT_TYPE 
                    //5 OBJECT_ID 
                    m.SERVER_NAME = lvi.SubItems[1].Text;
                    m.DATABASE_NAME = lvi.SubItems[2].Text;
                    m.OBJECT_NAME = lvi.SubItems[3].Text;
                    m.OBJECT_TYPE = lvi.SubItems[4].Text;
                    m.OBJECT_ID = Convert.ToInt32(lvi.SubItems[5].Text);
                    //6 COLUMN_NAME
                    //7 COLUMN_ID
                    //8 PK_YN
                    //9 UPDATE_YN
                    //10 INSERT_YN
                    m.COLUMN_NAME = lvi.SubItems[6].Text;
                    m.COLUMN_ID = Convert.ToInt32(lvi.SubItems[7].Text);
                    m.PK_YN = lvi.SubItems[8].Text;
                    m.UPDATE_YN = lvi.SubItems[9].Text;
                    m.INSERT_YN = lvi.SubItems[10].Text;


                    //11 RESERVED_YN
                    //12 CS_PARM_STRING
                    //13 CS_DATA_TYPE
                    //14 CS_PRIVATE_VARIABLE
                    //15 CS_PUBLIC_PROPERTY
                    m.RESERVED_YN = lvi.SubItems[11].Text;
                    m.CS_PARM_STRING = lvi.SubItems[12].Text;
                    m.CS_DATA_TYPE = lvi.SubItems[13].Text;
                    m.CS_PRIVATE_VARIABLE = lvi.SubItems[14].Text;
                    m.CS_PUBLIC_PROPERTY = lvi.SubItems[15].Text;




                    //16 SQL_IDENTITY_FIELD_YN
                    //17 SQL_DATA_TYPE
                    //18 SQL_IS_NULLABLE
                    //19 SQL_MAX_LENGTH
                    //20 SQL_PRECISION
                    m.SQL_IDENTITY_FIELD_YN = lvi.SubItems[16].Text;
                    m.SQL_DATA_TYPE = lvi.SubItems[17].Text;
                    m.SQL_IS_NULLABLE = lvi.SubItems[18].Text;
                    m.SQL_MAX_LENGTH = Convert.ToInt32(lvi.SubItems[19].Text);
                    m.SQL_PRECISION = Convert.ToInt32(lvi.SubItems[20].Text);



                    //21 SQL_SCALE 
                    //22 SQL_TABLE_SCHEMA 
                    //23 SQL_IDENT_SEED 
                    //24 SQL_IDENT_INCR 
                    //25 SQL_VARIABLE_NAME 
                    //26 SQL_PROC_PARM_STRING 
                    m.SQL_SCALE = Convert.ToInt32(lvi.SubItems[21].Text);
                    m.SQL_TABLE_SCHEMA = lvi.SubItems[22].Text;
                    m.SQL_IDENT_SEED = Convert.ToInt32(lvi.SubItems[23].Text);
                    m.SQL_IDENT_INCR  = Convert.ToInt32(lvi.SubItems[24].Text);
                    m.SQL_VARIABLE_NAME = lvi.SubItems[25].Text;
                    m.SQL_PROC_PARM_STRING = lvi.SubItems[26].Text;
                    

                    MY_SELECTED_COLUMNS.Add(m);
                    m = null;
                }
            }
            return MY_SELECTED_COLUMNS;
        }
        #region CREATE CUSTOM CODE

        private void CreateCustomRead()
        {

            ClearAllSource();

            List<FREDS_MATRIX> COLUMNS_IN_TABLE = new List<FREDS_MATRIX>();
           
            UTL_CS_GENERATOR CS_UTL = new UTL_CS_GENERATOR();
            UTL_SQL_GENERATOR SQL_UTL = new UTL_SQL_GENERATOR();
            List<FREDS_MATRIX> COLUMNS_IN_WHERE = new List<FREDS_MATRIX>();


            if (optTables.Checked)
            {
                COLUMNS_IN_TABLE = CS_UTL.UTL_GET_ATTRIBUTES_TABLE(cboServers.Text, cboDatabases.Text, Convert.ToInt32(cboTables.SelectedValue), false);
            }
            else
            {
                COLUMNS_IN_TABLE = CS_UTL.UTL_GET_ATTRIBUTES_VIEW(cboServers.Text, cboDatabases.Text, Convert.ToInt32(cboTables.SelectedValue));
            }
         
          
            COLUMNS_IN_WHERE = MapListViewTOFreddieMaxtrix(lvColumnsWhere);
            txtCS.Text= CS_UTL.METHOD_SEL_CUSTOM(COLUMNS_IN_TABLE, COLUMNS_IN_WHERE,cboTables.Text);
            SQL_UTL.SQL_FOLDER = CS_SQL_PATH;
            txtSQL.Text = SQL_UTL.TSQL_SELECT_CUSTOM(COLUMNS_IN_TABLE, COLUMNS_IN_WHERE, cboTables.Text, cboDatabases.Text);


            CS_UTL = null;
            SQL_UTL = null;
            COLUMNS_IN_WHERE = null;

        }
       
        private void CreateCustomSelectDistinct( )
        {
            ClearAllSource();
            List<FREDS_MATRIX> COLUMNS_IN_TABLE = new List<FREDS_MATRIX>();
            List<FREDS_MATRIX> COLUMNS_IN_WHERE = new List<FREDS_MATRIX>();
            UTL_CS_GENERATOR CS_UTL = new UTL_CS_GENERATOR();
            UTL_SQL_GENERATOR SQL_UTL = new UTL_SQL_GENERATOR();
            if (optTables.Checked)
            {
                COLUMNS_IN_TABLE = CS_UTL.UTL_GET_ATTRIBUTES_TABLE(cboServers.Text, cboDatabases.Text, Convert.ToInt32(cboTables.SelectedValue), false);
            }
            else
            {
                COLUMNS_IN_TABLE = CS_UTL.UTL_GET_ATTRIBUTES_VIEW(cboServers.Text, cboDatabases.Text, Convert.ToInt32(cboTables.SelectedValue));
            }
          
         
            COLUMNS_IN_WHERE = MapListViewTOFreddieMaxtrix(lvColumnsWhere);
            txtCS.Text = CS_UTL.METHOD_SEL_DISTINCT_CUSTOM(cboDistinctColumns.Text, COLUMNS_IN_WHERE, cboTables.Text);

            SQL_UTL.SQL_FOLDER = CS_SQL_PATH;
            txtSQL.Text = SQL_UTL.TSQL_SELECT_DISTINCT_CUSTOM(cboDistinctColumns.Text, COLUMNS_IN_WHERE, cboTables.Text, cboDatabases.Text);


            CS_UTL = null;
            SQL_UTL = null;
            COLUMNS_IN_WHERE = null;
            COLUMNS_IN_TABLE = null;


        }
        private void CreateCustomDelete()
        {
            ClearAllSource();
            if (optTables.Checked)
            {
                List<FREDS_MATRIX> COLUMNS_IN_WHERE = new List<FREDS_MATRIX>();
                UTL_CS_GENERATOR CS_UTL = new UTL_CS_GENERATOR();
                UTL_SQL_GENERATOR SQL_UTL = new UTL_SQL_GENERATOR();
                SQL_UTL.SQL_FOLDER = CS_SQL_PATH;


                COLUMNS_IN_WHERE = MapListViewTOFreddieMaxtrix(lvColumnsWhere);
                txtCS.Text = CS_UTL.METHOD_DEL_CUSTOM(COLUMNS_IN_WHERE, cboTables.Text);
                txtSQL.Text = SQL_UTL.TSQL_DELETE_CUSTOM(COLUMNS_IN_WHERE, cboTables.Text, cboDatabases.Text);



                CS_UTL = null;
                SQL_UTL = null;
                COLUMNS_IN_WHERE = null;


            }
        }
        private void CreateCustomInsert()
        {
            ClearAllSource();

            if (optTables.Checked)
            {
                List<FREDS_MATRIX> COLUMNS_TO_INSERT = new List<FREDS_MATRIX>();
                UTL_CS_GENERATOR CS_UTL = new UTL_CS_GENERATOR();
                UTL_SQL_GENERATOR SQL_UTL = new UTL_SQL_GENERATOR();
                SQL_UTL.SQL_FOLDER = CS_SQL_PATH;


                COLUMNS_TO_INSERT = MapListViewTOFreddieMaxtrix(lvColumns);
                txtCS.Text = CS_UTL.METHOD_INS_CUSTOM(COLUMNS_TO_INSERT, cboTables.Text);
                txtSQL.Text = SQL_UTL.TSQL_INSERT_CUSTOM(COLUMNS_TO_INSERT, cboTables.Text, cboDatabases.Text);



                CS_UTL = null;
                SQL_UTL = null;
                COLUMNS_TO_INSERT = null;


            }
        }
        private void CreateCustomInsertWhereNotExists()
        {
            ClearAllSource();
            if (optTables.Checked)
            {
                List<FREDS_MATRIX> COLUMNS_TO_INSERT = new List<FREDS_MATRIX>();
                List<FREDS_MATRIX> COLUMNS_IN_WHERE_NOT_EXISTS = new List<FREDS_MATRIX>();
                UTL_CS_GENERATOR CS_UTL = new UTL_CS_GENERATOR();
                UTL_SQL_GENERATOR SQL_UTL = new UTL_SQL_GENERATOR();
                SQL_UTL.SQL_FOLDER = CS_SQL_PATH;


                COLUMNS_TO_INSERT = MapListViewTOFreddieMaxtrix(lvColumns );
                COLUMNS_IN_WHERE_NOT_EXISTS = MapListViewTOFreddieMaxtrix(lvColumnsWhere);
                txtCS.Text = CS_UTL.METHOD_INS_WHERE_NOT_EXISTS_CUSTOM(COLUMNS_TO_INSERT, COLUMNS_IN_WHERE_NOT_EXISTS, cboTables.Text);
                txtSQL.Text = SQL_UTL.TSQL_INSERT_WHERE_NOT_EXISTS_CUSTOM(COLUMNS_TO_INSERT, COLUMNS_IN_WHERE_NOT_EXISTS, cboTables.Text, cboDatabases.Text);



                CS_UTL = null;
                SQL_UTL = null;
                COLUMNS_TO_INSERT = null;
                COLUMNS_IN_WHERE_NOT_EXISTS = null;

            }
        }
        private void CreateCustomUpdate()
        {
            ClearAllSource();
            if (optTables.Checked)
            {
                List<FREDS_MATRIX> COLUMNS_TO_UPDATE = new List<FREDS_MATRIX>();
                List<FREDS_MATRIX> COLUMNS_IN_WHERE = new List<FREDS_MATRIX>();
                UTL_CS_GENERATOR CS_UTL = new UTL_CS_GENERATOR();
                UTL_SQL_GENERATOR SQL_UTL = new UTL_SQL_GENERATOR();
                SQL_UTL.SQL_FOLDER = CS_SQL_PATH;


                COLUMNS_TO_UPDATE = MapListViewTOFreddieMaxtrix(lvColumns);
                COLUMNS_IN_WHERE = MapListViewTOFreddieMaxtrix(lvColumnsWhere);
                txtCS.Text = CS_UTL.METHOD_UPD_CUSTOM(COLUMNS_TO_UPDATE, COLUMNS_IN_WHERE,cboTables.Text);
                txtSQL.Text = SQL_UTL.TSQL_UPDATE_CUSTOM(COLUMNS_TO_UPDATE, COLUMNS_IN_WHERE, cboTables.Text, cboDatabases.Text);




                CS_UTL = null;
                SQL_UTL = null;
                COLUMNS_TO_UPDATE = null;
                COLUMNS_IN_WHERE = null;

            }
        }

        #endregion
        private void SetFormProperties()
        {
            optTables.Enabled = false;
            optViews.Enabled = false;
            txtCS.Height = tabControl_Code.Height - 40;
            txtCS.Width = tabControl_Code.Width - 25;
            txtSQL.Location = txtCS.Location;
            txtSQL.Size = txtCS.Size;
            picKEY.Visible = false;
            btnClose.Width =  tabControl_Code.Width + lvColumns.Width;
            btnGenerate.Width = btnClose.Width;
        }
        private void FormatListViews(ListView lv)
        {
            lv.Columns.Add("COLUMN", 350);
            lv.Columns.Add("SERVER_NAME", 350);
            lv.Columns.Add("DATABASE_NAME", 350);
            lv.Columns.Add("OBJECT_NAME", 350);
            lv.Columns.Add("OBJECT_TYPE", 350);
            lv.Columns.Add("OBJECT_ID", 350);
            lv.Columns.Add("COLUMN_NAME", 350);
            lv.Columns.Add("COLUMN_ID", 350);
            lv.Columns.Add("PK_YN", 350);
            lv.Columns.Add("UPDATE_YN", 350);
            lv.Columns.Add("INSERT_YN", 350);
            lv.Columns.Add("RESERVED_YN", 350);
            lv.Columns.Add("CS_PARM_STRING", 350);
            lv.Columns.Add("CS_DATA_TYPE", 350);
            lv.Columns.Add("CS_PRIVATE_VARIABLE", 350);
            lv.Columns.Add("CS_PUBLIC_PROPERTY", 350);
            lv.Columns.Add("SQL_IDENTITY_FIELD", 350);
            lv.Columns.Add("SQL_DATA_TYPE", 350);
            lv.Columns.Add("SQL_IS_NULLABLE", 350);
            lv.Columns.Add("SQL_MAX_LENGTH", 350);
            lv.Columns.Add("SQL_PRECISION", 350);
            lv.Columns.Add("SQL_SCALE", 350);
            lv.Columns.Add("SQL_TABLE_SCHEMA", 350);
            lv.Columns.Add("SQL_IDENT_SEED", 350);
            lv.Columns.Add("SQL_IDENT_INCR", 350);
            lv.Columns.Add("SQL_VARIABLE_NAME", 350);
            lv.Columns.Add("SQL_PROC_PARM_STRING", 350);
        }
        private void AddColumnsToListView_WHERE_CLAUSE(ListView lv, bool TABLE, int OBJECT_ID )
        {
            bProcessing = true;
            List<FREDS_MATRIX> LST_FREDS_MATRIX = new List<FREDS_MATRIX>();
            UTL_CS_GENERATOR UTL = new UTL_CS_GENERATOR();
            if (TABLE)
            {
                LST_FREDS_MATRIX = UTL.UTL_GET_ATTRIBUTES_TABLE(cboServers.Text, cboDatabases.Text, OBJECT_ID, false);
            }
            else
            {
                LST_FREDS_MATRIX =  UTL.UTL_GET_ATTRIBUTES_VIEW(cboServers.Text, cboDatabases.Text, OBJECT_ID);
            }
            lv.Items.Clear();
            lv.Enabled = true;
            int i = 0;
            foreach (FREDS_MATRIX ATTRIBUTE in LST_FREDS_MATRIX)
            {
                i++;
                ListViewItem lvi = new ListViewItem();
                lvi.Text = ATTRIBUTE.COLUMN_NAME;
                lvi.SubItems.Add(ATTRIBUTE.SERVER_NAME);
                lvi.SubItems.Add(ATTRIBUTE.DATABASE_NAME);
                lvi.SubItems.Add(ATTRIBUTE.OBJECT_NAME);
                lvi.SubItems.Add(ATTRIBUTE.OBJECT_TYPE);
                lvi.SubItems.Add(ATTRIBUTE.OBJECT_ID.ToString());
                lvi.SubItems.Add(ATTRIBUTE.COLUMN_NAME);
                lvi.SubItems.Add(ATTRIBUTE.COLUMN_ID.ToString());
                lvi.SubItems.Add(ATTRIBUTE.PK_YN);
                lvi.SubItems.Add(ATTRIBUTE.UPDATE_YN);
                lvi.SubItems.Add(ATTRIBUTE.INSERT_YN);
                lvi.SubItems.Add(ATTRIBUTE.RESERVED_YN);
                lvi.SubItems.Add(ATTRIBUTE.CS_PARM_STRING);
                lvi.SubItems.Add(ATTRIBUTE.CS_DATA_TYPE);
                lvi.SubItems.Add(ATTRIBUTE.CS_PRIVATE_VARIABLE);
                lvi.SubItems.Add(ATTRIBUTE.CS_PUBLIC_PROPERTY);
                lvi.SubItems.Add(ATTRIBUTE.SQL_IDENTITY_FIELD_YN);
                lvi.SubItems.Add(ATTRIBUTE.SQL_DATA_TYPE);
                lvi.SubItems.Add(ATTRIBUTE.SQL_IS_NULLABLE);
                lvi.SubItems.Add(ATTRIBUTE.SQL_MAX_LENGTH.ToString());
                lvi.SubItems.Add(ATTRIBUTE.SQL_PRECISION.ToString());
                lvi.SubItems.Add(ATTRIBUTE.SQL_SCALE.ToString());
                lvi.SubItems.Add(ATTRIBUTE.SQL_TABLE_SCHEMA);
                lvi.SubItems.Add(ATTRIBUTE.SQL_IDENT_SEED.ToString());
                lvi.SubItems.Add(ATTRIBUTE.SQL_IDENT_INCR.ToString());
                lvi.SubItems.Add(ATTRIBUTE.SQL_VARIABLE_NAME);
                lvi.SubItems.Add(ATTRIBUTE.SQL_PROC_PARM_STRING);
                if (i % 2 == 0)
                {
                    lvi.BackColor = Color.LightSkyBlue;
                }
                else
                {
                    lvi.BackColor = Color.LightSteelBlue;
                }
                lv.Items.Add(lvi);
                lvi = null;
            }
            bProcessing = false;
        }

        private void AddColumnsToInsertToListView(ListView lv, int OBJECT_ID)
        {
            bProcessing = true;
            Boolean addfield = true;
            List<FREDS_MATRIX> LST_FREDS_MATRIX = new List<FREDS_MATRIX>();
            UTL_CS_GENERATOR UTL = new UTL_CS_GENERATOR();
            LST_FREDS_MATRIX = UTL.UTL_GET_ATTRIBUTES_TABLE(cboServers.Text, cboDatabases.Text, OBJECT_ID, false);
            lv.Items.Clear();
            lv.Enabled = true;
            int i = 0;
            foreach (FREDS_MATRIX ATTRIBUTE in LST_FREDS_MATRIX)
            {
                addfield = true;
                i++;
                ListViewItem lvi = new ListViewItem();
                lvi.Text = ATTRIBUTE.COLUMN_NAME;
                lvi.SubItems.Add(ATTRIBUTE.SERVER_NAME);
                lvi.SubItems.Add(ATTRIBUTE.DATABASE_NAME);
                lvi.SubItems.Add(ATTRIBUTE.OBJECT_NAME);
                lvi.SubItems.Add(ATTRIBUTE.OBJECT_TYPE);
                lvi.SubItems.Add(ATTRIBUTE.OBJECT_ID.ToString());
                lvi.SubItems.Add(ATTRIBUTE.COLUMN_NAME);
                lvi.SubItems.Add(ATTRIBUTE.COLUMN_ID.ToString());
                lvi.SubItems.Add(ATTRIBUTE.PK_YN);
                lvi.SubItems.Add(ATTRIBUTE.UPDATE_YN);
                lvi.SubItems.Add(ATTRIBUTE.INSERT_YN);
                lvi.SubItems.Add(ATTRIBUTE.RESERVED_YN);
                lvi.SubItems.Add(ATTRIBUTE.CS_PARM_STRING);
                lvi.SubItems.Add(ATTRIBUTE.CS_DATA_TYPE);
                lvi.SubItems.Add(ATTRIBUTE.CS_PRIVATE_VARIABLE);
                lvi.SubItems.Add(ATTRIBUTE.CS_PUBLIC_PROPERTY);
                lvi.SubItems.Add(ATTRIBUTE.SQL_IDENTITY_FIELD_YN);
                lvi.SubItems.Add(ATTRIBUTE.SQL_DATA_TYPE);
                lvi.SubItems.Add(ATTRIBUTE.SQL_IS_NULLABLE);
                lvi.SubItems.Add(ATTRIBUTE.SQL_MAX_LENGTH.ToString());
                lvi.SubItems.Add(ATTRIBUTE.SQL_PRECISION.ToString());
                lvi.SubItems.Add(ATTRIBUTE.SQL_SCALE.ToString());
                lvi.SubItems.Add(ATTRIBUTE.SQL_TABLE_SCHEMA);
                lvi.SubItems.Add(ATTRIBUTE.SQL_IDENT_SEED.ToString());
                lvi.SubItems.Add(ATTRIBUTE.SQL_IDENT_INCR.ToString());
                lvi.SubItems.Add(ATTRIBUTE.SQL_VARIABLE_NAME);
                lvi.SubItems.Add(ATTRIBUTE.SQL_PROC_PARM_STRING);
                if (i % 2 == 0)
                {
                    lvi.BackColor = Color.LightSkyBlue;
                }
                else
                {
                    lvi.BackColor = Color.LightSteelBlue;
                }

                if (ATTRIBUTE.SQL_IDENTITY_FIELD_YN == "Y")
                {
                    addfield = false;
                }
                if (ATTRIBUTE.PK_YN == "Y")
                {
                    addfield = false;
                }
                if (ATTRIBUTE.COLUMN_NAME == "UPDDT" || ATTRIBUTE.COLUMN_NAME == "UPDOPID")
                {
                    addfield = false;
                }

                if (addfield)
                {
                    lv.Items.Add(lvi);
                }

                lvi = null;

            }
            bProcessing = false;
        }
        private void AddColumnsToUpdateToListView(ListView lv, int OBJECT_ID)
        {
            bProcessing = true;
            Boolean addfield = true;
            List<FREDS_MATRIX> LST_FREDS_MATRIX = new List<FREDS_MATRIX>();
            UTL_CS_GENERATOR UTL = new UTL_CS_GENERATOR();
            LST_FREDS_MATRIX = UTL.UTL_GET_ATTRIBUTES_TABLE(cboServers.Text, cboDatabases.Text, OBJECT_ID, false);
            lv.Items.Clear();
            lv.Enabled = true;
            int i = 0;
            foreach (FREDS_MATRIX ATTRIBUTE in LST_FREDS_MATRIX)
            {
                addfield = true;
                i++;
                ListViewItem lvi = new ListViewItem();
                lvi.Text = ATTRIBUTE.COLUMN_NAME;
                lvi.SubItems.Add(ATTRIBUTE.SERVER_NAME);
                lvi.SubItems.Add(ATTRIBUTE.DATABASE_NAME);
                lvi.SubItems.Add(ATTRIBUTE.OBJECT_NAME);
                lvi.SubItems.Add(ATTRIBUTE.OBJECT_TYPE);
                lvi.SubItems.Add(ATTRIBUTE.OBJECT_ID.ToString());
                lvi.SubItems.Add(ATTRIBUTE.COLUMN_NAME);
                lvi.SubItems.Add(ATTRIBUTE.COLUMN_ID.ToString());
                lvi.SubItems.Add(ATTRIBUTE.PK_YN);
                lvi.SubItems.Add(ATTRIBUTE.UPDATE_YN);
                lvi.SubItems.Add(ATTRIBUTE.INSERT_YN);
                lvi.SubItems.Add(ATTRIBUTE.RESERVED_YN);
                lvi.SubItems.Add(ATTRIBUTE.CS_PARM_STRING);
                lvi.SubItems.Add(ATTRIBUTE.CS_DATA_TYPE);
                lvi.SubItems.Add(ATTRIBUTE.CS_PRIVATE_VARIABLE);
                lvi.SubItems.Add(ATTRIBUTE.CS_PUBLIC_PROPERTY);
                lvi.SubItems.Add(ATTRIBUTE.SQL_IDENTITY_FIELD_YN);
                lvi.SubItems.Add(ATTRIBUTE.SQL_DATA_TYPE);
                lvi.SubItems.Add(ATTRIBUTE.SQL_IS_NULLABLE);
                lvi.SubItems.Add(ATTRIBUTE.SQL_MAX_LENGTH.ToString());
                lvi.SubItems.Add(ATTRIBUTE.SQL_PRECISION.ToString());
                lvi.SubItems.Add(ATTRIBUTE.SQL_SCALE.ToString());
                lvi.SubItems.Add(ATTRIBUTE.SQL_TABLE_SCHEMA);
                lvi.SubItems.Add(ATTRIBUTE.SQL_IDENT_SEED.ToString());
                lvi.SubItems.Add(ATTRIBUTE.SQL_IDENT_INCR.ToString());
                lvi.SubItems.Add(ATTRIBUTE.SQL_VARIABLE_NAME);
                lvi.SubItems.Add(ATTRIBUTE.SQL_PROC_PARM_STRING);
                if (i % 2 == 0)
                {
                    lvi.BackColor = Color.LightSkyBlue;
                }
                else
                {
                    lvi.BackColor = Color.LightSteelBlue;
                }

                if (ATTRIBUTE.SQL_IDENTITY_FIELD_YN == "Y")
                {
                    addfield = false;
                }
                if (ATTRIBUTE.PK_YN == "Y")
                {
                    addfield = false;
                }
                if (ATTRIBUTE.COLUMN_NAME == "INSDT" || ATTRIBUTE.COLUMN_NAME == "INSOPID")
                {
                    addfield = false;
                }

                if (addfield)
                {
                    lv.Items.Add(lvi);
                }

                lvi = null;

            }
            bProcessing = false;
        }








        private void LoadServers()
        {
            try
            {

                cboServers.DataSource = null;
                AvailableServer_READ db = new AvailableServer_READ();
                cboServers.DataSource = db.SEL_ALL_ROWS().Tables[0];
                cboServers.ValueMember = "SERVER_ID";
                cboServers.DisplayMember = "SERVER_NAME";
                db = null;
                optTables.Enabled = false;
                optViews.Enabled = false;
                picKEY.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadDatabasesToComboBox(ComboBox cbo)
        {
            bProcessing = true;
            MSSQL_CS_META_DATA db = new MSSQL_CS_META_DATA();
            DataSet DS = db.GET_DATABASES_THIS_SERVER_DS(cboServers.Text);
            cbo.DataSource = DS.Tables[0];
            cbo.DisplayMember = "NAME";
            cbo.ValueMember = "NAME";
            cbo.SelectedIndex = -1;
            DS = null;
            db = null;
           
            optTables.Enabled = true;
            optViews.Enabled = true;
            picKEY.Visible = false;
            bProcessing = false;
        }
        private void LoadTables()
        {
            if (!bProcessing)
            {
                bProcessing = true;
                MSSQL_CS_META_DATA db = new MSSQL_CS_META_DATA();
                DataSet DS = db.GET_TABLES_THIS_DATABASE_DS(cboServers.Text, cboDatabases.Text);
                this.cboTables.DataSource = DS.Tables[0];
                this.cboTables.DisplayMember = "NAME";
                this.cboTables.ValueMember = "OBJECT_ID";
                DS = null;
                db = null;
                optTables.Enabled = true;
                optViews.Enabled = true;
                cboTables.Text = "";
                picKEY.Visible = false;
                bProcessing = false;
            }
        }
        private void LoadViews()
        {
            if (!bProcessing)
            {
                this.Cursor = Cursors.WaitCursor;
                bProcessing = true;
                MSSQL_CS_META_DATA db = new MSSQL_CS_META_DATA();
                DataSet DS = db.GET_VIEWS_THIS_DATABASE_DS(cboServers.Text, cboDatabases.Text);
                this.cboTables.DataSource = DS.Tables[0];
                this.cboTables.DisplayMember = "NAME";
                this.cboTables.ValueMember = "OBJECT_ID";
                cboTables.Text = "";
                picKEY.Visible = false;
                DS = null;
                db = null;
                bProcessing = false;
                this.Cursor = Cursors.Default;
            }
        }
        private void ClearAllSource()
        {
            txtCS.Clear();
            txtSQL.Clear();
        }
        private void ClearAllListViews()
        {
            lvColumnsWhere.Items.Clear();
            lvColumns.Items.Clear();
            cboDistinctColumns.DataSource = null;
            cboDistinctColumns.Text = "";
        }
        private void cboServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bProcessing)
            {
                ClearAllSource();
                ClearAllListViews();
                DisableAllCustomCodeButtons();
                //  DisableSPButtons();
                this.Cursor = Cursors.WaitCursor;
                if (chkMyDatabases.Checked)
                {
                    LoadMyDatabases();
                }
                else
                {
                    LoadDatabasesToComboBox(cboDatabases);
                }
                    optTables.Enabled = false;
                    optViews.Enabled = false;
                bProcessing = false;
                this.Cursor = Cursors.Default;
            }
        }
        private void cboDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bProcessing)
            {
                this.Cursor = Cursors.WaitCursor;
                DisableAllCustomCodeButtons();
                ClearAllSource();
                ClearAllListViews();
                if (optTables.Checked)
                {
                    LoadTables();
                }
                else
                {
                    LoadViews();
                }

                CS_READ_PATH = Settings.Default["DEFAULT_FOLDER_PATH"] + cboServers.Text + @"\" + cboDatabases.Text + @"\READ\";
                CS_IUD_PATH = Settings.Default["DEFAULT_FOLDER_PATH"] + cboServers.Text + @"\" + cboDatabases.Text + @"\IUD\";
                this.Cursor = Cursors.Default;
            }
        }
        private void optViews_CheckedChanged(object sender, EventArgs e)
        {
            if (!bProcessing)
            {
                ClearAllSource();
                ClearAllListViews();
                LoadViews();
            }
        }
        private void optTables_CheckedChanged(object sender, EventArgs e)
        {
            if (!bProcessing)
            {
                ClearAllSource();
                ClearAllListViews();
                LoadTables();
            }
        }
        private void GetPKData()
        {
            if (!bProcessing)
            {
                bPK = false;
                MSSQL_CS_META_DATA db = new MSSQL_CS_META_DATA();
                if (db.TABLE_HAS_PK(cboServers.Text, cboDatabases.Text, Convert.ToInt32(cboTables.SelectedValue)) > 0)
                {
                    bPK = true;
                }
                else
                {
                    bPK = false;
                }
            }
            picKEY.Visible = bPK;

        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void DisableAllCustomCodeButtons()
        {
            btnGenerate.Enabled = false;
        }
        private void FormatComboBoxWithColumnsForSelDistinct()
        {
            bProcessing = true;
            cboDistinctColumns.Items.Clear();
            List<FREDS_MATRIX> COLUMNS_IN_TABLE = new List<FREDS_MATRIX>();
            UTL_CS_GENERATOR UTL = new UTL_CS_GENERATOR();
            if(optTables.Checked)
            {
                COLUMNS_IN_TABLE = UTL.UTL_GET_ATTRIBUTES_TABLE(cboServers.Text, cboDatabases.Text, Convert.ToInt32(cboTables.SelectedValue), false);
            }
            else
            {
                COLUMNS_IN_TABLE = UTL.UTL_GET_ATTRIBUTES_VIEW(cboServers.Text, cboDatabases.Text, Convert.ToInt32(cboTables.SelectedValue) );
            }
            foreach(FREDS_MATRIX f in COLUMNS_IN_TABLE)
            {
                cboDistinctColumns.Items.Add(f.COLUMN_NAME);
            }
            bProcessing = false;
        }
        private void cboTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (!bProcessing)
            {
                ClearAllSource();
                ClearAllListViews();
                DisableAllCustomCodeButtons();
               
                FormatComboBoxWithColumnsForSelDistinct( );

                if(optSelectDistinct.Checked)
                {
                    SetScreen_SELECT_DISTINCT();
                }
                if (optSelect.Checked)
                {
                    SetScreen_SELECT();
                }
                if (optTables.Checked)
                {
                    GetPKData();
                    if(optInsert.Checked)
                    {
                       SetScreen_INSERT();
                    }
                    if(optUpdate.Checked)
                    {
                        SetScreen_UPDATE();
                    }
                    if(optInsertWhereNotExists.Checked)
                    {
                        SetScreen_INSERT_WHERE_NOT_EXISTS();
                    }
                    if(optDelete.Checked)
                    {
                        SetScreen_DELETE();
                    }
                }
             
                CS_READ_PATH = Settings.Default["DEFAULT_FOLDER_PATH"] + cboServers.Text + @"\" + cboDatabases.Text + @"\READ\";
                CS_IUD_PATH = Settings.Default["DEFAULT_FOLDER_PATH"] + cboServers.Text + @"\" + cboDatabases.Text + @"\IUD\";
                CS_SQL_PATH = Settings.Default["DEFAULT_FOLDER_PATH"] + cboServers.Text + @"\" + cboDatabases.Text + @"\SQL\";
             }
            this.Cursor = Cursors.Default;
        }

        private void btnCS_Clipboard_Click(object sender, EventArgs e)
        {
            switch (tabControl_Code.SelectedTab.Text)
            {
                case "C#":
                    Clipboard.SetText(txtCS.Text);
                    break;
                case "TSQL":
                    break;

          }
           
        }
        private void LoadMyDatabases()
        {
            bProcessing = true;
            MydatabaseModel m = new MydatabaseModel
            {
                SERVER_ID = Convert.ToInt32(cboServers.SelectedValue)
            };
            cboDatabases.DataSource = null;
            Mydatabase_READ db = new Mydatabase_READ();
            cboDatabases.DataSource = db.SEL_BY_SERVER_ID(m).Tables[0];
            cboDatabases.DisplayMember = "DATABASE_NAME";
            cboDatabases.ValueMember = "DATABASE_NAME";
            cboDatabases.SelectedIndex = -1;
            db = null;
            optTables.Enabled = true;
            optViews.Enabled = true;
            bProcessing = false;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void chkMyDatabases_CheckedChanged(object sender, EventArgs e)
        {
            if (cboServers.Text != "")
            {
                if (chkMyDatabases.Checked)
                {
                    LoadMyDatabases();
                }
                else
                {
                    LoadDatabasesToComboBox(cboDatabases);
                }
            }
        }
        private void optSelect_CheckedChanged(object sender, EventArgs e)
        {
            SetScreen_SELECT();
        }
        private void optInsert_CheckedChanged(object sender, EventArgs e)
        {
            SetScreen_INSERT();
        }
        private void optUpdate_CheckedChanged(object sender, EventArgs e)
        {
            SetScreen_UPDATE();
        }
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if(optSelect.Checked)
            {
                CreateCustomRead();
            }
            if(optSelectDistinct.Checked)
            {
                CreateCustomSelectDistinct();
            }
            if(optInsert.Checked)
            {
                CreateCustomInsert();
            }
            if(optInsertWhereNotExists.Checked)
            {
                CreateCustomInsertWhereNotExists();
            }
            if(optUpdate.Checked)
            {
                CreateCustomUpdate();
            }
            if(optDelete.Checked)
            {
                CreateCustomDelete();
            }
        }

        private void optSelectDistinct_CheckedChanged(object sender, EventArgs e)
        {
            SetScreen_SELECT_DISTINCT();
        }

        private void optDelete_CheckedChanged(object sender, EventArgs e)
        {
            SetScreen_DELETE();
        }

        private void SetScreen_INSERT()
        {
            if (cboTables.Text != "")
            {
                cboDistinctColumns.Visible = false;
                AddColumnsToInsertToListView(lvColumns, Convert.ToInt32(cboTables.SelectedValue));
                lvColumns.Visible = true;
                lblColumnsToUpdateInsert.Text = "COLUMNS TO INSERT";
                lblColumnsToUpdateInsert.Visible = true;
                lvColumns.Height = BIG_LIST_VIEW_HEIGHT;
                lblWhereClause.Visible = false;
                lvColumnsWhere.Visible = false;
                btnGenerate.Text = @"C#\SQL - CUSTOM INSERT";
                btnGenerate.Enabled = true;
            }
        }
        private void SetScreen_INSERT_WHERE_NOT_EXISTS()
        {
            if (cboTables.Text != "")
            {
                AddColumnsToListView_WHERE_CLAUSE(lvColumnsWhere, optTables.Checked, Convert.ToInt32(cboTables.SelectedValue));
                AddColumnsToInsertToListView(lvColumns, Convert.ToInt32(cboTables.SelectedValue));
                cboDistinctColumns.Visible = false;
                lblColumnsToUpdateInsert.Visible = true;
                lblColumnsToUpdateInsert.Text = "COLUMNS TO INSERT";
                lvColumns.Visible = true;
                lvColumns.Height = SMALL_LIST_VIEW_COLUMNS_HEIGHT;
                lvColumnsWhere.Height = SMALL_LIST_VIEW_WHERE_HEIGHT;
                lvColumnsWhere.Location = original_WHERE_listview_location;
                lblWhereClause.Visible = true;
                lvColumnsWhere.Visible = true;
                lblWhereClause.Location = original_label_location;
                btnGenerate.Text = @"C#\SQL - CUSTOM INSERT WHERE NOT EXISTS";
                btnGenerate.Enabled = true;
            }
        }
        private void SetScreen_UPDATE()
        {
            if (cboTables.Text != "")
            {
                AddColumnsToListView_WHERE_CLAUSE(lvColumnsWhere, optTables.Checked, Convert.ToInt32(cboTables.SelectedValue));
                cboDistinctColumns.Visible = false;
                AddColumnsToUpdateToListView(lvColumns, Convert.ToInt32(cboTables.SelectedValue));
                lvColumns.Visible = true;
                lvColumns.Height = SMALL_LIST_VIEW_COLUMNS_HEIGHT;
                lblColumnsToUpdateInsert.Text = "COLUMNS TO UPDATE";
                lblColumnsToUpdateInsert.Visible = true;
                lblWhereClause.Visible = true;
                lblWhereClause.Location = original_label_location;
                lvColumnsWhere.Visible = true;
                lvColumnsWhere.Height = SMALL_LIST_VIEW_WHERE_HEIGHT;
                lvColumnsWhere.Location = original_WHERE_listview_location;
                btnGenerate.Text = @"C#\SQL - CUSTOM UPDATE";
                btnGenerate.Enabled = true;
            }
        }
        private void SetScreen_DELETE()
        {
            if (cboTables.Text != "")
            {
                AddColumnsToListView_WHERE_CLAUSE(lvColumnsWhere, optTables.Checked, Convert.ToInt32(cboTables.SelectedValue));
                cboDistinctColumns.Visible = false;
                lblColumnsToUpdateInsert.Visible = false;
                lvColumns.Visible = false;
                lvColumnsWhere.Location = original_COLUMNS_listview_location;
                lblWhereClause.Location = lblColumnsToUpdateInsert.Location;
                lblWhereClause.Visible = true;
                lvColumnsWhere.Visible = true;
                lvColumnsWhere.Height = BIG_LIST_VIEW_HEIGHT;
                btnGenerate.Text = @"C#\SQL - CUSTOM DELETE";
                btnGenerate.Enabled = true;
            }
        }
        private void SetScreen_SELECT()
        {
            if (cboTables.Text != "")
            {
                AddColumnsToListView_WHERE_CLAUSE(lvColumnsWhere, optTables.Checked, Convert.ToInt32(cboTables.SelectedValue));
                cboDistinctColumns.Visible = false;
                lblColumnsToUpdateInsert.Visible = false;
                lvColumns.Visible = false;
                lblWhereClause.Visible = true;
                lblWhereClause.Location = lblColumnsToUpdateInsert.Location;
                lvColumnsWhere.Visible = true;
                lvColumnsWhere.Height = BIG_LIST_VIEW_HEIGHT;
                lvColumnsWhere.Location = original_COLUMNS_listview_location;
                btnGenerate.Text = @"C#\SQL - CUSTOM SELECT";
                btnGenerate.Enabled = true;
            }
        }
        private void SetScreen_SELECT_DISTINCT()
        {
            if (cboTables.Text != "")
            {
                AddColumnsToListView_WHERE_CLAUSE(lvColumnsWhere, optTables.Checked, Convert.ToInt32(cboTables.SelectedValue));
                cboDistinctColumns.Visible = true;
                lblColumnsToUpdateInsert.Text = "DISTINCT COLUMN TO SELECT";
                lblColumnsToUpdateInsert.Visible = true;
                lblWhereClause.Visible = true;
                lvColumnsWhere.Visible = true;
                lblWhereClause.Location = original_label_location;
                lvColumns.Visible = false;
                btnGenerate.Text = @"C#\SQL - CUSTOM SELECT DISTINCT";
                btnGenerate.Enabled = true;
                lvColumnsWhere.Height = SMALL_LIST_VIEW_WHERE_HEIGHT;
                lvColumnsWhere.Location = original_WHERE_listview_location;
            }
        }
        private void optInsertWhereNotExists_CheckedChanged(object sender, EventArgs e)
        {
            SetScreen_INSERT_WHERE_NOT_EXISTS();
        }
    }
}
