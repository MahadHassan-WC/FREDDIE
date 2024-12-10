using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SqlSvrMeta;
namespace FREDDIE
{
    public partial class Form3 : Form_BASE_1
    {
        private bool Processing = false;
        public Form3()
        {
            InitializeComponent();
        }
        private void FormatGrid()
        {
           dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.FromArgb(192, 255, 255);  
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(172, 225, 225);
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            Processing = true;
            cboServers.Items.Add("WCSSQL2");
            cboServers.Items.Add(@"DEV\SQLTEST");
            FormatGrid();
            btnAddDatabases.Enabled = false;
            Processing = false;
        }
        private void LoadMyDatabases()
        {
            FREDDIE_UTL U = new FREDDIE_UTL();
            dataGridView1.DataSource = U.GET_MYDATABASES_XML_DATASET(cboServers.Text, @"C:\_Freddie\MyDatabases\");
            dataGridView1.DataMember = "DB";
            dataGridView1.Sort(dataGridView1.Columns["DATABASE_NAME"],ListSortDirection.Ascending);
            U = null;
        }
        private void LoadDatabaseListBox()
        {
            try
            {
                if (!Processing)
                {
                    Processing = true;
                    this.lstDatabases.DataSource = null;
                    SQLSVR_METADATA sqlsvr = new SQLSVR_METADATA();
                    sqlsvr.SERVER_NAME = cboServers.Text;
                    DataSet DS = sqlsvr.GetDatabases_DS();
                    this.lstDatabases.DataSource = DS.Tables[0];
                    lstDatabases.DisplayMember = "NAME";
                    lstDatabases.ValueMember = "dbid";
                    sqlsvr = null;
                    LoadMyDatabases();
                    Processing = false;
                    btnAddDatabases.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cboServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDatabaseListBox();
        }
        private void CreateNewDatabaseList()
        {
            if (this.lstDatabases.CheckedItems.Count > 0)
            {
                string MyDatabases = null;
                string[] MyDatabasesArr = null;
                foreach (int indexChecked in this.lstDatabases.CheckedIndices)
                {
                    Application.DoEvents();
                    this.lstDatabases.SelectedIndex = indexChecked;
                    MyDatabases += cboServers.Text + "*" + lstDatabases.Text + "*" + lstDatabases.SelectedValue.ToString() + "|";
                }
                MyDatabasesArr = MyDatabases.Split('|');
                FREDDIE_UTL U = new FREDDIE_UTL();
                U.CREATE_MYDATABASES_XML(cboServers.Text, MyDatabasesArr, @"C:\_Freddie\MyDatabases\");
                U = null;
                LoadDatabaseListBox();
            }
            else
            {
                MessageBox.Show("Must selecte at least one database from list.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AppendDatabases()
        {
            if (this.lstDatabases.CheckedItems.Count > 0)
            {
                string MyDatabases = null;
                string[] MyDatabasesArr = null;
                foreach (int indexChecked in this.lstDatabases.CheckedIndices)
                {
                    Application.DoEvents();
                    this.lstDatabases.SelectedIndex = indexChecked;
                    MyDatabases += cboServers.Text + "*" + lstDatabases.Text + "*" + lstDatabases.SelectedValue.ToString() + "|";
                }
                MyDatabasesArr = MyDatabases.Split('|');
                FREDDIE_UTL U = new FREDDIE_UTL();
                U.ADD_MYDATABASES_XML(cboServers.Text, MyDatabasesArr, @"C:\_Freddie\MyDatabases\");
                U = null;
                LoadDatabaseListBox();
            }
            else
            {
                MessageBox.Show("Must selecte at least one database from list.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.CreateNewDatabaseList();
            LoadDatabaseListBox();
            this.Cursor = Cursors.Default;
        }

        private void btnUpdateDatabases_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            AppendDatabases();
            LoadDatabaseListBox();
            this.Cursor = Cursors.Default;
        }

        private void DeleteDatabaseFromList()
        {

        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteDatabaseFromList();
        }
    }
}
