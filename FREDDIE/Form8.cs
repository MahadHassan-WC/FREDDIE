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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }
        private const string XML_FILE = @"c:\pete\MyMSSQL_SERVERS.xml";

        //private bool bPK = false;
        //private bool bPKContainsIdentityField = false;
        private bool bProcessing = false;

        private string CS_PATH_MODEL = null;
        private string CS_PATH_READ = null;
        private string CS_PATH_IUD = null;
        private string CS_PATH_GUI = null;
        private string SQL_PATH = null;
        private void Form8_Load(object sender, EventArgs e)
        {
            bProcessing = true;

          //  cboServers.Items.Add("LocalHost");

            optTables.Checked = true;
            LoadServers();
            cboServers.Text = "";
           
           
            ClearAllSource();

            CS_PATH_MODEL = @"C:\Freddie\CS\MSSQL\";
            CS_PATH_READ = @"C:\Freddie\CS\MSSQL\";
            CS_PATH_IUD = @"C:\Freddie\CS\MSSQL\";
            CS_PATH_GUI = @"C:\Freddie\CS\MSSQL\";
            SQL_PATH = @"C:\Freddie\CS\MSSQL\";


            bProcessing = false;
        }
        private void ClearAllSource()
        {
            //txtIUD.Clear();
            txtMODEL.Clear();
            //txtREAD.Clear();
            //txtSQL_SELECT_BY_PK.Clear();
            //txtSQL_SELECT_ALL.Clear();
            //txtSQL_DELETE_ALL.Clear();
            //txtSQL_DELETE_BY_PK.Clear();
            //txtSQL_INSERT_A_ROW.Clear();

            //txtSQL_UPDATE_A_ROW.Clear();
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
                //DisableSPButtons();
                this.Cursor = Cursors.WaitCursor;
                //if (chkMyDatabases.Checked)
                //{
                //    LoadMyDatabases();
                //}
                //else
                //{
                    LoadDatabasesToComboBox(cboDatabases);
                //}

                this.Cursor = Cursors.Default;
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
            // UpdateCheckedTableCount();
            optTables.Enabled = true;
            optViews.Enabled = true;
            //   picKEY.Visible = false;
            //  picHomer.Visible = false;
            bProcessing = false;
        }

        private void cboDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bProcessing)
            {
                ClearAllSource();
              //  DisableSPButtons();
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

        private void cboTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bProcessing == false)
            {
                this.ProcessTableOrView();
            }
        }
        private void ProcessTableOrView()
        {
            String ObjectName = null;
            Int32 ObjectID = 0;
            this.Cursor = Cursors.WaitCursor;
            ClearAllSource();
           

            ObjectName = cboTables.Text;
            ObjectID = Convert.ToInt32(cboTables.SelectedValue);


            UTL_CS_GENERATOR CS = new UTL_CS_GENERATOR();
            CS.CS_MODEL_FOLDER = CS_PATH_MODEL + cboServers.Text + @"\" + cboDatabases.Text + @"\MODEL\";
            
            CS.GENERATE_MODEL_IGNORE_CASE(cboServers.Text, cboDatabases.Text, ObjectID, ObjectName, optViews.Checked);
           
            txtMODEL.Text = CS.CS_MODEL_CLASS;
          
            CS = null;

           
            this.Cursor = Cursors.Default;
        }

        private void optTables_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
