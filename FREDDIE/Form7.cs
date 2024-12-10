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
    public partial class Form7 : Form
    {
        public Form7()
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



        private const String _DEFAULT_FOLDER_PATH = @"C:\_FREDDIE\CS\MSSQL\";
        private void Form7_Load(object sender, EventArgs e)
        {
            bProcessing = true;


           
            optTables.Checked = true;
          //  cboServers.Items.Add("LocalHost");
            LoadServers();
            cboServers.Text = "";
            SetFormProperties();
             DisableSPButtons();
            ClearAllSource();
 
            CS_PATH_MODEL = @"C:\Freddie\CS\MSSQL\";
            CS_PATH_READ = @"C:\Freddie\CS\MSSQL\";
            CS_PATH_IUD = @"C:\Freddie\CS\MSSQL\";
            CS_PATH_GUI = @"C:\Freddie\CS\MSSQL\";
            SQL_PATH = @"C:\Freddie\CS\MSSQL\";


            bProcessing = false;
        }
        private void SetFormProperties()
        {
            txtIUD.Size = txtMODEL.Size;
            txtREAD.Size = txtMODEL.Size;
            //txtGUI.Size = txtMODEL.Size;

            txtIUD.Location = txtMODEL.Location;
            txtREAD.Location = txtMODEL.Location;
            //txtGUI.Location = txtMODEL.Location;


            txtSQL_DELETE_ALL.Size = txtSQL_SELECT_ALL.Size;
            txtSQL_DELETE_BY_PK.Size = txtSQL_SELECT_ALL.Size;
            txtSQL_INSERT_A_ROW.Size = txtSQL_SELECT_ALL.Size;
            
            txtSQL_SELECT_BY_PK.Size = txtSQL_SELECT_ALL.Size;
            txtSQL_UPDATE_A_ROW.Size = txtSQL_SELECT_ALL.Size;


        }
        private void ClearAllSource()
        {
            txtIUD.Clear();
            txtMODEL.Clear();
            txtREAD.Clear();
           
            txtGetDataFromGrid.Clear();
            txtSQL_SELECT_BY_PK.Clear();
            txtSQL_SELECT_ALL.Clear();
            txtSQL_DELETE_ALL.Clear();
            txtSQL_DELETE_BY_PK.Clear();
            txtSQL_INSERT_A_ROW.Clear();
            
            txtSQL_UPDATE_A_ROW.Clear();

            txtUpdateARecord.Clear();
            txtDeleteARecord.Clear();
            txtGetDataFromGrid.Clear();
            txtLoadGrid.Clear();
            txtModuleLevelVariables.Clear();
            txtRecord_Is_Valid.Clear();
            txtResetScreen.Clear();
            txtTextChanged.Clear();
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
        private void LoadServers()
        {
            try
            {
                //cboServers.DataSource = null;
                //AvailableServer_READ db = new AvailableServer_READ();
                //cboServers.DataSource = db.SEL_ALL_ROWS().Tables[0];
                //cboServers.ValueMember = "SERVER_ID";
                //cboServers.DisplayMember = "SERVER_NAME";
                //db = null;

                cboServers.Items.Add("localhost");


                optTables.Enabled = false;
                optViews.Enabled = false;
                
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
            // UpdateCheckedTableCount();
            optTables.Enabled = true;
            optViews.Enabled = true;
         //   picKEY.Visible = false;
          //  picHomer.Visible = false;
            bProcessing = false;
        }

        private void cboServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bProcessing)
            {
                ClearAllSource();
                DisableSPButtons();
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
        private void DisableSPButtons()
        {
            //btnDeleteAll.Enabled = false;
            //btnDeleteByPK.Enabled = false;
            //btnInsertARow.Enabled = false;
            //btnInsertWhereNotExists.Enabled = false;
            //btnSelectALL.Enabled = false;
            //btnSelectByPK.Enabled = false;
            //btnUpdateByPK.Enabled = false;

            //btnDeleteAll.Text = "CREATE/RECREATE";
            //btnDeleteByPK.Text = "CREATE/RECREATE";
            //btnInsertARow.Text = "CREATE/RECREATE";
            //btnInsertWhereNotExists.Text = "CREATE/RECREATE";
            //btnSelectALL.Text = "CREATE/RECREATE";
            //btnSelectByPK.Text = "CREATE/RECREATE";
            //btnUpdateByPK.Text = "CREATE/RECREATE";

        }

        private void cboDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bProcessing)
            {
                ClearAllSource();
                DisableSPButtons();
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
           MyInt= db.TABLE_HAS_PK(ServerName, DatabaseName, ObjectID);

            if(MyInt ==0)
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


        private void ProcessTableOrView()
        {
            String ObjectName = null;
            Int32 ObjectID = 0;
            this.Cursor = Cursors.WaitCursor;
            ClearAllSource();
            DisableSPButtons();
           
                    ObjectName = cboTables.Text;
                    ObjectID = Convert.ToInt32(cboTables.SelectedValue);
                  
               
                UTL_CS_GENERATOR CS = new UTL_CS_GENERATOR();
                CS.CS_MODEL_FOLDER = CS_PATH_MODEL + cboServers.Text + @"\" + cboDatabases.Text + @"\MODEL\";
                CS.CS_READ_FOLDER = CS_PATH_READ + cboServers.Text + @"\" + cboDatabases.Text + @"\READ\";
                CS.CS_IUD_FOLDER = CS_PATH_IUD + cboServers.Text + @"\" + cboDatabases.Text + @"\IUD\";
                CS.GENERATE_DEFAULT(cboServers.Text, cboDatabases.Text, ObjectID, ObjectName, optViews.Checked);
                txtREAD.Text = CS.CS_READ_CLASS;
                txtMODEL.Text = CS.CS_MODEL_CLASS;
                txtIUD.Text = CS.CS_IUD_CLASS;
                CS = null;

                UTL_SQL_GENERATOR slq = new UTL_SQL_GENERATOR();
                slq.SQL_FOLDER = SQL_PATH + cboServers.Text + @"\" + cboDatabases.Text + @"\SQL\";
                slq.GENERATE_DEFAULT(cboServers.Text, cboDatabases.Text, ObjectID, ObjectName, optViews.Checked);
             
                txtSQL_SELECT_BY_PK.Text = slq.usp_SEL_BY_PK;
                txtSQL_SELECT_ALL.Text = slq.usp_SEL_ALL;
                txtSQL_INSERT_A_ROW.Text = slq.usp_INS_A_ROW;
                txtSQL_DELETE_BY_PK.Text = slq.usp_DEL_BY_PK;
             
                txtSQL_DELETE_ALL.Text = slq.usp_DEL_ALL_ROWS;
                txtSQL_UPDATE_A_ROW.Text = slq.usp_UPD_BY_PK;
                slq = null;



                UTL_CS_GUI_GENERATOR gui = new UTL_CS_GUI_GENERATOR();
                gui.GENERATE_GUI_FUNCTIONS(cboServers.Text, cboDatabases.Text, ObjectID, ObjectName, optViews.Checked);
                txtGetDataFromGrid.Text = gui.CS_GET_DATA_FROM_GRID;
                txtDeleteARecord.Text = gui.CS_DELETE_A_RECORD;
                txtUpdateARecord.Text = gui.CS_UPDATE_A_RECORD;
                txtResetScreen.Text = gui.CS_RESET_SCREEN;
                txtLoadGrid.Text = gui.CS_LOAD_GRID;
                txtRecord_Is_Valid.Text = gui.CS_RECORD_IS_VALID;
                txtModuleLevelVariables.Text = gui.CS_MODULE_LEVEL_VARIABLES;
                txtTextChanged.Text = gui.CS_TEXT_CHANGED;


                this.Cursor = Cursors.Default;
        }
        private void lvTables_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        
        
        private void button1_Click(object sender, EventArgs e)
        {
           // ProcessCheckedItems();
        }

        private void lvTables_Click(object sender, EventArgs e)
        {
            //if (!bProcessing)
            //{



            //    String ObjectName = null;
            //    Int32 ObjectID = 0;

            //    ListViewItem item = lvTables.sele
            //    if (optTables.Checked)
            //    {
            //        if (item != null)
            //        {
            //            this.Cursor = Cursors.WaitCursor;

            //            ObjectName = item.SubItems[1].Text;
            //            ObjectID = Convert.ToInt32(item.SubItems[2].Text);
            //            ClearAllSource();
            //            DisableSPButtons();
            //            CreateCSharpCode(cboServers.Text, cboDatabases.Text, ObjectName, ObjectID, optViews.Checked);
            //            CreateSQLCode(cboServers.Text, cboDatabases.Text, ObjectName, ObjectID, optViews.Checked);
            //            this.Cursor = Cursors.Default;
            //            lvTables.SelectedItems.Clear();
            //        }
            //    }
            //    else
            //    {
            //        if (item != null)
            //        {
            //            this.Cursor = Cursors.WaitCursor;

            //            ObjectName = item.SubItems[0].Text;
            //            ObjectID = Convert.ToInt32(item.SubItems[1].Text);
            //            ClearAllSource();
            //            DisableSPButtons();
            //            CreateCSharpCode(cboServers.Text, cboDatabases.Text, ObjectName, ObjectID, optViews.Checked);
            //            CreateSQLCode(cboServers.Text, cboDatabases.Text, ObjectName, ObjectID, optViews.Checked);
            //            this.Cursor = Cursors.Default;
            //            lvTables.SelectedItems.Clear();
            //        }
            //    }






            //}
        }

        private void cboTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(bProcessing == false)
            {
                this.ProcessTableOrView();
            }
        }
    }
}
