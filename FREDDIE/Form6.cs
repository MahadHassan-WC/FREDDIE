using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FREDDIE
{
    public partial class Form6 : Form
    {



        private const string XML_FILE = @"c:\pete\MyMSSQL_SERVERS.xml";

        //private bool bPK = false;
        //private bool bPKContainsIdentityField = false;
        private bool bProcessing = false;

        private string CS_PATH_MODEL = null;
        private string CS_PATH_READ = null;
        private string CS_PATH_IUD = null;
        private string CS_PATH_GUI = null;
        private string SQL_PATH = null;



        private const String _DEFAULT_FOLDER_PATH = @"C:\_FREDDIE\MVC\MSSQL\";
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            bProcessing = true;



            optTables.Checked = true;
            LoadServers();
           // cboServers.Items.Add("LocalHost");
            cboServers.Text = "";
          //  SetFormProperties();
           
            ClearAllSource();

            //Myfilepath_READ db = new Myfilepath_READ();
            //DataSet ds = db.SEL_BY_PK();
            //db = null;
            //foreach (DataRow r in ds.Tables[0].Rows)
            //{
            //    CS_PATH_MODEL = r["CS_PATH_MODEL"].ToString();
            //    CS_PATH_READ = r["CS_PATH_READ"].ToString();
            //    CS_PATH_IUD = r["CS_PATH_IUD"].ToString();
            //    CS_PATH_GUI = r["CS_PATH_GUI"].ToString();
            //    SQL_PATH = r["SQL_PATH"].ToString();

            //}

            CS_PATH_MODEL = @"C:\Freddie\CS\MSSQL\";
            CS_PATH_READ = @"C:\Freddie\CS\MSSQL\";
            CS_PATH_IUD = @"C:\Freddie\CS\MSSQL\";
            CS_PATH_GUI = @"C:\Freddie\CS\MSSQL\";
            SQL_PATH = @"C:\Freddie\CS\MSSQL\";
           
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
                // picKEY.Visible = false;
                //  picHomer.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bProcessing)
            {
                ClearAllSource();
                
                this.Cursor = Cursors.WaitCursor;
                if (chkMyDatabases.Checked)
                {
                    LoadMyDatabases();
                }
                else
                {
                    LoadDatabasesToComboBox(cboDatabases);
                }

                this.Cursor = Cursors.Default;
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
            // UpdateCheckedTableCount();
            optTables.Enabled = true;
            optViews.Enabled = true;
            //   picKEY.Visible = false;
            //  picHomer.Visible = false;
            bProcessing = false;
        }

        private void ClearAllSource()
        {
            txtCONTROLLER.Clear();
            txtMODEL.Clear();
            txtVIEW.Clear();
        }

        private void cboDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bProcessing)
            {
                ClearAllSource();
             //   DisableSPButtons();
                this.Cursor = Cursors.WaitCursor;
                if (optTables.Checked)
                {
                    LoadTables();
                }
                else
                {
                    LoadViews();
                }

                this.Cursor = Cursors.Default;
            }
        }
        private void LoadTables()
        {
            if (!bProcessing)
            {



                bProcessing = true;
                ClearAllSource();
                cboTables.DataSource = null;
                MSSQL_CS_META_DATA db = new MSSQL_CS_META_DATA();
                DataSet DS = db.GET_TABLES_THIS_DATABASE_DS(cboServers.Text, cboDatabases.Text);
                cboTables.DataSource = DS.Tables[0];
                cboTables.ValueMember = "OBJECT_ID";
                cboTables.DisplayMember = "NAME";
                cboTables.Text = "";
                cboTables.BackColor = Color.FromArgb(192, 255, 192);
                DS = null;
                db = null;

                bProcessing = false;



            }
        }
        private String CheckForPK(String ServerName, String DatabaseName, int ObjectID)
        {
            int MyInt = 0;
            MSSQL_CS_META_DATA db = new MSSQL_CS_META_DATA();
            MyInt = db.TABLE_HAS_PK(ServerName, DatabaseName, ObjectID);

            if (MyInt == 0)
            {
                return "N";
            }
            else
            {
                return "Y";
            }

        }
        private void LoadViews()
        {
            if (!bProcessing)
            {



                bProcessing = true;
                ClearAllSource();
                cboTables.DataSource = null;
                MSSQL_CS_META_DATA db = new MSSQL_CS_META_DATA();
                // DataSet DS = db.GET_TABLES_THIS_DATABASE_DS(cboServers.Text, cboDatabases.Text);
                DataSet DS = db.GET_VIEWS_THIS_DATABASE_DS(cboServers.Text, cboDatabases.Text);
                cboTables.DataSource = DS.Tables[0];
                cboTables.ValueMember = "OBJECT_ID";
                cboTables.DisplayMember = "NAME";
                cboTables.Text = "";
                cboTables.BackColor = Color.FromArgb(192, 255, 255);
                DS = null;
                db = null;
                bProcessing = false;


            }
        }

        private void optTables_CheckedChanged(object sender, EventArgs e)
        {
            if (!bProcessing)
            {

                LoadTables();
            }
        }

        private void optViews_CheckedChanged(object sender, EventArgs e)
        {
            if (!bProcessing)
            {
                LoadViews();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!bProcessing)
            {
                if (optTables.Checked)
                {
                    ProcessTable();
                }
            }
           
        }
        private void ProcessTable()
        {
            String ObjectName = null;
            Int32 ObjectID = 0;
            this.Cursor = Cursors.WaitCursor;
            ClearAllSource();
           

            ObjectName = cboTables.Text;
            ObjectID = Convert.ToInt32(cboTables.SelectedValue);


            UTL_MVC_MODEL_GENERATOR MVC = new UTL_MVC_MODEL_GENERATOR();
            MVC.CS_MODEL_FOLDER = CS_PATH_MODEL + cboServers.Text + @"\" + cboDatabases.Text + @"\MODEL\";
            
            MVC.GENERATE_MODEL_TABLE(cboServers.Text, cboDatabases.Text, cboTables.Text, ObjectID );
            
            txtMODEL.Text = MVC.CS_MODEL_CLASS;
           
            MVC = null;




            UTL_MVC_DML_READ MVC_READ = new UTL_MVC_DML_READ();
            MVC_READ.CS_READ_FOLDER= CS_PATH_READ + cboServers.Text + @"\" + cboDatabases.Text + @"\READ\";
            MVC_READ.GENERATE_READ_TABLE(cboServers.Text, cboDatabases.Text, cboTables.Text, ObjectID);
            txtREAD.Text = MVC_READ.CS_READ_CLASS;
            MVC_READ = null;


            UTL_MVC_VIEW_GENERATOR MVC_VIEW = new UTL_MVC_VIEW_GENERATOR();
            MVC_VIEW.GENERATE_ALL_VIEWS(cboServers.Text, cboDatabases.Text, cboTables.Text,   ObjectID);
            txtVIEW.Text = MVC_VIEW.INDEX_CSHTML;
            MVC_VIEW = null;


            UTL_MVC_CONTROLLER_GENERATOR MVC_CONTROLLER = new UTL_MVC_CONTROLLER_GENERATOR();
            MVC_CONTROLLER.GENERATE_INDEX_ACTION(cboTables.Text);
            txtCONTROLLER.Text = MVC_CONTROLLER.CS_CONTROLLER;
            MVC_CONTROLLER = null;

           this.Cursor = Cursors.Default;
        }
    }
}
