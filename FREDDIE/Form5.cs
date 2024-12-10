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
    public partial class Form5 : Form
    {


        const string CS_PATH_MODEL_DEFAULT = @"C:\Freddie\CS\MSSQL\";
        const string CS_PATH_READ_DEFAULT = @"C:\Freddie\CS\MSSQL\";
        const string CS_PATH_IUD_DEFAULT = @"C:\Freddie\CS\MSSQL\";
        const string SQL_PATH_DEFAULT = @"C:\Freddie\CS\MSSQL\";

        const string SQL_DATABASES_THIS_SERVER = @"
                select 
                        name, 
                        dbid 
                from 
                        sysdatabases 
                where 
                        name not in('master','model','msdb','tempdb') and 
                        and name not in(SELECT DATABASE_NAME FROM MYDATABASE WHERE SERVER_ID = @SERVER_ID)
                order by 
                        name ";


        private Boolean bProcessing = false;
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            bProcessing = true;
            LoadAvailableServers();
            btnRemoveServer.Enabled = false;
            btnAddServer.Enabled = true;
            CheckFilePaths();
            bProcessing = false;
        }
        private void LoadAvailableServers() 
        {
            AvailableServer_READ DB = new AvailableServer_READ();
            lstServers.DataSource = DB.SEL_BY_INSOPID().Tables[0];
            lstServers.ValueMember = "SERVER_ID";
            lstServers.DisplayMember = "SERVER_NAME";


            cboServers.DataSource = DB.SEL_BY_INSOPID().Tables[0];
            cboServers.ValueMember = "SERVER_ID";
            cboServers.DisplayMember = "SERVER_NAME";
            cboServers.SelectedIndex = -1;
            DB = null;
        }
        private void btnAddServer_Click(object sender, EventArgs e)
        {
            AvailableServerModel m = new AvailableServerModel
            {
                SERVER_NAME = txtSERVER_NAME.Text
            };

            AvailableServer_IUD db = new AvailableServer_IUD();
            if(db.INSERT_A_ROW(m))
            {
                LoadAvailableServers();
                MessageBox.Show("SERVER ADDED TO DATABASE", "DATABASE UPDATED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSERVER_NAME.Clear();
                txtSERVER_ID.Clear();
                btnRemoveServer.Enabled = false;
                btnAddServer.Enabled = true;
            }
            else
            {
                MessageBox.Show("SERVER NOT ADDED TO DATABASE", "DATABASE ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            db = null;
            m = null;

        }

        private void lstServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!bProcessing)
            {
                txtSERVER_ID.Text = lstServers.SelectedValue.ToString();
                txtSERVER_NAME.Text = lstServers.Text;
                btnAddServer.Enabled = false;
                btnRemoveServer.Enabled = true;
            }
           
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSERVER_NAME.Clear();
            txtSERVER_ID.Clear();
            btnRemoveServer.Enabled = false;
            btnAddServer.Enabled = true;
        }

        private void btnRemoveServer_Click(object sender, EventArgs e)
        {


            DialogResult result =  MessageBox.Show("WARNING: BY DELETING THIS SERVER FROM THE LIST, YOU WILL ALSO DELETE YOUR MYDATABASES LIST AS WELL" + Environment.NewLine + "ARE YOU SURE YOU WANT TO DO THIS???", "ARE YOU SURE YOU WANT TO DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if(result ==   DialogResult.Yes)
            {
                AvailableServerModel m = new AvailableServerModel
                {
                    SERVER_NAME = txtSERVER_NAME.Text,
                    SERVER_ID = Convert.ToInt32(txtSERVER_ID.Text)

                };

                MydatabaseModel M = new MydatabaseModel
                {
                    SERVER_ID = Convert.ToInt32(txtSERVER_ID.Text)
                };

                AvailableServer_IUD db = new AvailableServer_IUD();
                if (db.DELETE_A_ROW_BY_PK(m))
                {
                    LoadAvailableServers();
                    MessageBox.Show("SERVER REMOVED FROM DATABASE", "DATABASE UPDATED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSERVER_NAME.Clear();
                    txtSERVER_ID.Clear();
                    btnRemoveServer.Enabled = false;
                    btnAddServer.Enabled = true;
                }
                else
                {
                    MessageBox.Show("SERVER NOT REMOVED FROM DATABASE", "DATABASE ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Mydatabase_IUD db2 = new Mydatabase_IUD();

                db2.DEL_MYDATABASE_BY_SERVER_ID(M);
                db2 = null;
                M = null;



                db = null;
                m = null;
                LoadAvailableServers();
                MessageBox.Show("DONE", "SERVER and any databases deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

          

        }

        private void GetMyDatabases()
        {
            lstMyDatabases.DataSource = null;
            MydatabaseModel m = new MydatabaseModel
            {
                SERVER_ID = Convert.ToInt32(cboServers.SelectedValue)
            };
            Mydatabase_READ db = new Mydatabase_READ();
            lstMyDatabases.DataSource = db.SEL_BY_SERVER_ID(m).Tables[0];
            lstMyDatabases.ValueMember = "DATABASE_NAME";
            lstMyDatabases.DisplayMember = "DATABASE_NAME";
            db = null;
            m = null;
        }
        private void GetDatabasesOnServerNotInMyDatabases()
        {

            lstDatabases.Items.Clear();
            MSSQL_CS_META_DATA db = new MSSQL_CS_META_DATA();
            DataSet DS = db.GET_DATABASES_THIS_SERVER_DS(cboServers.Text);
            db = null;
            foreach(DataRow r in DS.Tables[0].Rows)
            {
                MydatabaseModel M = new MydatabaseModel()
                {
                    SERVER_ID = Convert.ToInt32(cboServers.SelectedValue),
                    DATABASE_NAME = r["name"].ToString()

                };
                Mydatabase_READ db2 = new Mydatabase_READ();
                DataSet ds2 = db2.SEL_BY_PK(M);
                db2 = null;
                if(ds2.Tables[0].Rows.Count == 0)
                {
                    lstDatabases.Items.Add(M.DATABASE_NAME);
                }
                M = null;


            }

        }
        private void cboServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!bProcessing)
            {
                GetDatabasesOnServerNotInMyDatabases();
                GetMyDatabases();

            }
        }

        private void btnAddToMyDatabases_Click(object sender, EventArgs e)
        {
            bool errors = false;
            foreach (int indexChecked in this.lstDatabases.CheckedIndices)
            {
                Application.DoEvents();
                this.lstDatabases.SelectedIndex = indexChecked;
                MydatabaseModel m = new MydatabaseModel
                {
                    DATABASE_NAME = lstDatabases.Text,
                    SERVER_ID = Convert.ToInt32(cboServers.SelectedValue)
                };
                Mydatabase_IUD db = new Mydatabase_IUD();
                if (!db.INSERT_A_ROW(m))
                    {
                    errors = true;
                    }
                m = null;
                db = null;
            }
            if(errors)
            {
                MessageBox.Show("DATABASE(S) NOT ADDED TO YOUR LIST", "DATABASE ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                GetDatabasesOnServerNotInMyDatabases();
                GetMyDatabases();
            }
           
        }

        private void btnRemoveFromMyDatabases_Click(object sender, EventArgs e)
        {
            bool errors = false;
            foreach (int indexChecked in this.lstMyDatabases.CheckedIndices)
            {
                Application.DoEvents();
                this.lstMyDatabases.SelectedIndex = indexChecked;
                MydatabaseModel m = new MydatabaseModel
                {
                    DATABASE_NAME = lstMyDatabases.Text,
                    SERVER_ID = Convert.ToInt32(cboServers.SelectedValue)
                };
                Mydatabase_IUD db = new Mydatabase_IUD();
                if (!db.DELETE_A_ROW_BY_PK(m))
                {
                    errors = true;
                }
                m = null;
                db = null;
            }
            if (errors)
            {
                MessageBox.Show("DATABASE(S) NOT REMOVED FROM YOUR LIST", "DATABASE ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                GetDatabasesOnServerNotInMyDatabases();
                GetMyDatabases();
            }
        }

        private void CheckFilePaths()
        {
            Myfilepath_READ db = new Myfilepath_READ();
            DataSet ds = db.SEL_BY_PK();
            db = null;
            if(ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("SETTING DEFAULT FILE PATHS", "FIRST TIME SETUP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMODELPath.Text = CS_PATH_MODEL_DEFAULT;
                txtREADPath.Text = CS_PATH_READ_DEFAULT;
                txtIUDPath.Text = CS_PATH_IUD_DEFAULT;
                txtSQLPath.Text = SQL_PATH_DEFAULT;
                MyfilepathModel m = new MyfilepathModel
                {
                    CS_PATH_MODEL = txtMODELPath.Text,
                    CS_PATH_GUI = "not in use",
                    CS_PATH_IUD = txtIUDPath.Text,
                    CS_PATH_READ = txtREADPath.Text,
                    SQL_PATH = txtSQLPath.Text
                };
                Myfilepath_IUD dbInsert = new Myfilepath_IUD();
                if (dbInsert.INSERT_A_ROW(m))
                {
                    MessageBox.Show("SET UP COMPLETE", "DATABASE UPDATED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("UNABLE TO INSERT DEFAULT ROW IN TABLE", "DATABASE ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                foreach(DataRow r in ds.Tables[0].Rows)
                {
                    txtMODELPath.Text = r["CS_PATH_MODEL"].ToString();
                    txtREADPath.Text = r["CS_PATH_READ"].ToString();
                    txtIUDPath.Text = r["CS_PATH_IUD"].ToString();
                    txtSQLPath.Text = r["SQL_PATH"].ToString();
                }
            }
        }

        private void UpdateFilePaths()
        {
            MyfilepathModel m = new MyfilepathModel
            {
                CS_PATH_MODEL = txtMODELPath.Text,
                CS_PATH_GUI = "not in use",
                CS_PATH_IUD = txtIUDPath.Text,
                CS_PATH_READ = txtREADPath.Text,
                SQL_PATH = txtSQLPath.Text
            };
            Myfilepath_IUD db = new Myfilepath_IUD();
            if (db.UPDATE_A_ROW_BY_PK(m))
            {
                MessageBox.Show("PATH CHANGED", "DATABASE UPDATED", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("UNABLE TO UPDATE PATH", "DATABASE ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMODELPathChange_Click(object sender, EventArgs e)
        {
            UpdateFilePaths();
        }

        private void btnREADPathChange_Click(object sender, EventArgs e)
        {
            UpdateFilePaths();
        }

        private void btnSQLPathChange_Click(object sender, EventArgs e)
        {
            UpdateFilePaths();
        }

        private void btnIUDPathChange_Click(object sender, EventArgs e)
        {
            UpdateFilePaths();
        }

        private void btnMODELPathDefault_Click(object sender, EventArgs e)
        {
            txtMODELPath.Text = CS_PATH_MODEL_DEFAULT;
            UpdateFilePaths();

        }

        private void btnREADLPathDefault_Click(object sender, EventArgs e)
        {
            txtREADPath.Text = CS_PATH_READ_DEFAULT;
            UpdateFilePaths();
        }

        private void btnIUDPathDefault_Click(object sender, EventArgs e)
        {
            txtIUDPath.Text = CS_PATH_IUD_DEFAULT;
            UpdateFilePaths();
        }

        private void btnSQLPathDefault_Click(object sender, EventArgs e)
        {
            txtSQLPath.Text = SQL_PATH_DEFAULT;
            UpdateFilePaths();
        }
    }
}
