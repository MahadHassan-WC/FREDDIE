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
    public partial class Form4 : Form
    {
        private bool Processing = false;
        private string _OBJECT_ID;
        private DataSet DS_COLUMNS;
        public Form4()
        {
            InitializeComponent();
        }
        private void FORMAT_GRD_CUSTOM_METHODS()
        {
            GRD_CUSTOM_METHODS.Columns.Clear();
            GRD_CUSTOM_METHODS.Rows.Clear();



            GRD_CUSTOM_METHODS.ColumnCount =4;
           
           
            GRD_CUSTOM_METHODS.RowsDefaultCellStyle.BackColor = System.Drawing.Color.LightBlue;
            GRD_CUSTOM_METHODS.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightSeaGreen;
            GRD_CUSTOM_METHODS.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            GRD_CUSTOM_METHODS.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            GRD_CUSTOM_METHODS.ReadOnly = true;
            GRD_CUSTOM_METHODS.AllowUserToAddRows = false;
            GRD_CUSTOM_METHODS.AllowUserToDeleteRows = false;
            GRD_CUSTOM_METHODS.Columns[0].Name = "METHOD NAME";
            GRD_CUSTOM_METHODS.Columns[1].Name = "COLUMNS";
            GRD_CUSTOM_METHODS.Columns[2].Name = "WHERE..";
            GRD_CUSTOM_METHODS.Columns[3].Name = "TYPE";




        }
        private bool VALIDATE_ADD_CUSTOM(string METHOD_TO_ADD, string COL1, string COL2, string TYPE)
        {
           
            GRD_CUSTOM_METHODS.Rows.Add(METHOD_TO_ADD, COL1, COL2, TYPE);
            return true;
        }
        private void Form4_Load(object sender, EventArgs e)
        {
            Processing = true;
            cboServers.Items.Add("WCSSQL2");
            cboServers.Items.Add(@"DEV\SQLTEST");

            lblCOLUMNS1.Visible = false;
            lblCOLUMNS2.Visible = false;
            lvCOLUMNS1.Visible = false;
            lvCOLUMNS2.Visible = false;
            btnAddCustomMethod.Enabled = false;
            FORMAT_GRD_CUSTOM_METHODS();


            optTABLES.Checked = true;
            Processing = false;
        }
        private void LOAD_cboCustomMethods(bool View)
        {
            btnAddCustomMethod.Enabled = false;
            lblCOLUMNS1.Visible = false;
            lblCOLUMNS2.Visible = false;
            lvCOLUMNS1.Visible = false;
            lvCOLUMNS2.Visible = false;
            cboCustomMethods.Items.Clear();
            cboCustomMethods.Text = "";
            
            bool ADD_SUM_METHOD = false;
            bool ADD_DATE_METHOD = false;
            foreach (DataRow R in this.DS_COLUMNS.Tables[0].Rows)
            {
                if (R["TYPE_NAME"].ToString().ToUpper() == "INT")
                {
                    ADD_SUM_METHOD = true;
                }
                if (R["TYPE_NAME"].ToString().ToUpper() == "DECIMAL")
                {
                    ADD_SUM_METHOD = true;
                }
                if (R["TYPE_NAME"].ToString().ToUpper() == "DATETIME")
                {
                    ADD_DATE_METHOD = true;
                }
            }
            cboCustomMethods.Items.Add("SELECT");
            cboCustomMethods.Items.Add("SELECT COUNT (*)");
            cboCustomMethods.Items.Add("SELECT TOP 1");
            cboCustomMethods.Items.Add("SELECT MIN()");
            cboCustomMethods.Items.Add("SELECT MAX()");
            if (ADD_SUM_METHOD)
            {
                cboCustomMethods.Items.Add("SELECT SUM()");
            }
            if (ADD_DATE_METHOD)
            {
                cboCustomMethods.Items.Add("SELECT BETWEEN DATES");
            }
            if (!View)
            {
                cboCustomMethods.Items.Add("INSERT");
                cboCustomMethods.Items.Add("INSERT WHERE NOT EXISTS");
                cboCustomMethods.Items.Add("UPDATE");
                cboCustomMethods.Items.Add("DELETE");
            }
            if(cboCustomMethods.Items.Count==0)
            {
                btnAddCustomMethod.Enabled = false;
            }
            else
            {
                btnAddCustomMethod.Enabled = true;
            }
        }
        private void FORMAT_CHECKBOX_OPTIONS(bool View, bool PK)
        {
            GUI_UTL.ClearCheckBoxControls(grpOPTIONS);
            GUI_UTL.DisableCheckBoxControls(grpOPTIONS);

            chkSEL_100.Enabled = true;
            chkSEL_ALL.Enabled = true;
            if (!View)

            {
                chkINS.Enabled = true;
                chkDEL_ALL.Enabled = true;
                if (PK)
                {
                    chkUPD_PK.Enabled = true;
                    chkDEL_PK.Enabled = true;
                    chkSEL_PK.Enabled = true;
                }
            }


        }

        private void cboServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Processing)
            {
                Processing = true;

                CS_MSSQL_META_DATA o = new CS_MSSQL_META_DATA();
                DataSet DS = o.GET_DATABASES_THIS_SERVER_DS(cboServers.Text);
              
                            
                
                this.cboDatabases.DataSource = DS.Tables[0];
                cboDatabases.DisplayMember = "NAME";
                cboDatabases.ValueMember = "NAME";
                cboDatabases.SelectedIndex = -1;
                DS = null;
                o = null;






                lblCOLUMNS1.Visible = false;
                lblCOLUMNS2.Visible = false;
                lvCOLUMNS1.Visible = false;
                lvCOLUMNS2.Visible = false;
                cboCustomMethods.Items.Clear();
                cboCustomMethods.Text = "";
                Processing = false;

            }
        }

        private void cboDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Processing)
            {
                Processing = true;
                SQLSVR_METADATA sqlsvr = new SQLSVR_METADATA();
                sqlsvr.SERVER_NAME = cboServers.Text;
                sqlsvr.DATABASE_NAME = cboDatabases.Text;
                DataSet DSTables = sqlsvr.GetTablesInDatabase_DS();
                DataSet DSViews = sqlsvr.GetViewsInDatabase_DS();
                this.cboTables.DataSource = DSTables.Tables[0];
                this.cboViews.DataSource = DSViews.Tables[0];
                cboTables.ValueMember = "object_id";
                cboTables.DisplayMember = "TABLE_NAME";
                cboTables.SelectedIndex = -1;
                cboViews.ValueMember = "object_id";
                cboViews.DisplayMember = "VIEW_NAME";
                cboViews.SelectedIndex = -1;
                cboViews.Text = "";
                cboTables.Text = "";
             //   txtCS.Clear();
                tv.Nodes.Clear();
                GUI_UTL.ClearCheckBoxControls(grpOPTIONS);
                lblOBJECT_ID.Text = lblOBJECT_ID.Tag.ToString();
                lblTABLE_SCHEMA.Text = lblTABLE_SCHEMA.Tag.ToString();
                chkPK.Checked = false;
                sqlsvr = null;
                lblCOLUMNS1.Visible = false;
                lblCOLUMNS2.Visible = false;
                lvCOLUMNS1.Visible = false;
                lvCOLUMNS2.Visible = false;
                cboCustomMethods.Items.Clear();
                cboCustomMethods.Text = "";
                Processing = false;
            }
        }



        private void HandleScreenForTablesOrViews()
        {
            lblCOLUMNS1.Visible = false;
            lblCOLUMNS2.Visible = false;
            lvCOLUMNS1.Visible = false;
            lvCOLUMNS2.Visible = false;
            cboCustomMethods.Items.Clear();
            cboCustomMethods.Text = "";
            lblTABLE_SCHEMA.Text = lblTABLE_SCHEMA.Tag.ToString();
            lblOBJECT_ID.Text = lblOBJECT_ID.Tag.ToString();
            tv.Nodes.Clear();
         //   txtCS.Clear();
            GUI_UTL.ClearCheckBoxControls(grpOPTIONS);
            GUI_UTL.EnableCheckBoxControls(grpOPTIONS);
            cboTables.Visible = optTABLES.Checked;
            cboViews.Visible = optVIEWS.Checked;
            chkPK.Visible = optTABLES.Checked;
            if (optVIEWS.Checked)
            {
              //  txtOBJ.BackColor = cboViews.BackColor;
                cboDatabases.BackColor = cboViews.BackColor;
                cboServers.BackColor = cboViews.BackColor;
                tv.BackColor = cboViews.BackColor;
              //  txtCS.BackColor = cboViews.BackColor;
                chkDEL_ALL.Enabled = false;
                chkDEL_PK.Enabled = false;
                chkINS.Enabled = false;
                chkUPD_PK.Enabled = false;
            }
            else
            {
              //  txtOBJ.BackColor = cboTables.BackColor;
                cboDatabases.BackColor = cboTables.BackColor;
                cboServers.BackColor = cboTables.BackColor;
                tv.BackColor = cboTables.BackColor;
             //   txtCS.BackColor = cboTables.BackColor;
            }
        }

        private void optTABLES_CheckedChanged(object sender, EventArgs e)
        {
            HandleScreenForTablesOrViews();
        }

        private void optVIEWS_CheckedChanged(object sender, EventArgs e)
        {
            HandleScreenForTablesOrViews();
        }
        private void RESET_SCREEN_CHANGE_IN_TABLE_VIEW(bool View)
        {

           
            chkPK.Checked = false;
            if (!Processing)
            {

                if (!View)
                {
                    SQLSVR_METADATA sqlsvr = new SQLSVR_METADATA();
                    sqlsvr.SERVER_NAME = cboServers.Text;
                    sqlsvr.DATABASE_NAME = cboDatabases.Text;
                    sqlsvr.TABLE_NAME = cboTables.Text;
                    sqlsvr.OBJECT_ID = cboTables.SelectedValue.ToString();
                    _OBJECT_ID = sqlsvr.OBJECT_ID;
                    if (sqlsvr.TableHasPK())
                    {
                        chkPK.Checked = true;
                    }
                    sqlsvr = null;
                    PopulateTreeViewTable();
                }
                else
                {
                    PopulateTreeViewView();
                }
                LOAD_cboCustomMethods(View);
                FORMAT_CHECKBOX_OPTIONS(View, chkPK.Checked);


            }






        }
        private void PopulateTreeViewTable()
        {
            tv.Nodes.Clear();
            SQLSVR_METADATA sqlsvr = new SQLSVR_METADATA();
            sqlsvr.SERVER_NAME = cboServers.Text;
            sqlsvr.DATABASE_NAME = cboDatabases.Text;
            sqlsvr.TABLE_NAME = cboTables.Text;
            sqlsvr.OBJECT_ID = cboTables.SelectedValue.ToString();
            this.DS_COLUMNS = sqlsvr.GetColumnsInTableAndPK_DS();
            int i = 0;
            foreach (DataRow r in this.DS_COLUMNS.Tables[0].Rows)
            {
                lblTABLE_SCHEMA.Text = lblTABLE_SCHEMA.Tag + r["TABLE_SCHEMA"].ToString();
                lblOBJECT_ID.Text = lblOBJECT_ID.Tag + r["OBJECT_ID"].ToString();
                tv.Nodes.Add(r["COLUMN_NAME"].ToString());
                if (r["PK"].ToString() == "Y")
                {
                    tv.Nodes[i].Nodes.Add("PK");
                }
                tv.Nodes[i].Nodes.Add(r["TYPE_NAME"].ToString());
                if (r["IS_IDENTITY"].ToString() == "1")
                {
                    tv.Nodes[i].Nodes.Add("IDENTITY:" + r["IDENT_SEED"].ToString() + "/" + r["IDENT_INCR"].ToString());
                }
                if ((r["IS_NULLABLE"].ToString() == "0"))
                {
                    tv.Nodes[i].Nodes.Add("NOT NULL");
                }
                tv.Nodes[i].Nodes.Add("LENGTH:" + r["MAX_LENGTH"].ToString());
                tv.Nodes[i].Nodes.Add("PRECISION:" + r["PRECISION"].ToString());
                tv.Nodes[i].Nodes.Add("SCALE:" + r["SCALE"].ToString());
                tv.Nodes[i].Nodes.Add("COLUMN ID:" + r["COLUMN_ID"].ToString());
                i++;
            } /// end of loop
            sqlsvr = null;
            PopulateListView(lvCOLUMNS1, cboTables.SelectedValue.ToString(), cboTables.Text);
            PopulateListView(lvCOLUMNS2, cboTables.SelectedValue.ToString(), cboTables.Text);
        }  //// end of method
        private void PopulateTreeViewView()
        {
            tv.Nodes.Clear();
            SQLSVR_METADATA sqlsvr = new SQLSVR_METADATA();
            sqlsvr.SERVER_NAME = cboServers.Text;
            sqlsvr.DATABASE_NAME = cboDatabases.Text;
            sqlsvr.VIEW_NAME = cboViews.Text;
            sqlsvr.OBJECT_ID = cboViews.SelectedValue.ToString();
            _OBJECT_ID = sqlsvr.OBJECT_ID;
            this.DS_COLUMNS = sqlsvr.GetColumnsInView_DS();
            int i = 0;
            foreach (DataRow r in this.DS_COLUMNS.Tables[0].Rows)
            {
                lblTABLE_SCHEMA.Text = lblTABLE_SCHEMA.Tag + r["TABLE_SCHEMA"].ToString();
                lblOBJECT_ID.Text = lblOBJECT_ID.Tag + r["OBJECT_ID"].ToString();
                tv.Nodes.Add(r["COLUMN_NAME"].ToString());
                //if (r["PK"].ToString() == "Y")
                //{
                //    tv.Nodes[i].Nodes.Add("PK");
                //}
                tv.Nodes[i].Nodes.Add(r["TYPE_NAME"].ToString());
                if (r["IS_IDENTITY"].ToString() == "1")
                {
                    tv.Nodes[i].Nodes.Add("IDENTITY:" + r["IDENT_SEED"].ToString() + "/" + r["IDENT_INCR"].ToString());
                }
                if ((r["IS_NULLABLE"].ToString() == "0"))
                {
                    tv.Nodes[i].Nodes.Add("NOT NULL");
                }
                tv.Nodes[i].Nodes.Add("LENGTH:" + r["MAX_LENGTH"].ToString());
                tv.Nodes[i].Nodes.Add("PRECISION:" + r["PRECISION"].ToString());
                tv.Nodes[i].Nodes.Add("SCALE:" + r["SCALE"].ToString());
                tv.Nodes[i].Nodes.Add("COLUMN ID:" + r["COLUMN_ID"].ToString());
                i++;
            } /// end of loop
            sqlsvr = null;
            PopulateListView(lvCOLUMNS1, cboViews.SelectedValue.ToString(), cboViews.Text);
            PopulateListView(lvCOLUMNS2, cboViews.SelectedValue.ToString(), cboViews.Text);
        }  //// end of method

        private void cboViews_SelectedIndexChanged(object sender, EventArgs e)
        {
            RESET_SCREEN_CHANGE_IN_TABLE_VIEW(true);
        }

        private void cboTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            RESET_SCREEN_CHANGE_IN_TABLE_VIEW(false);
        }
        private void SHOW_BOTH()
        {
            lblCOLUMNS1.Visible = true;
            lblCOLUMNS2.Visible = true;
            lvCOLUMNS1.Visible = true;
            lvCOLUMNS2.Visible = true;
        }
        private void SHOW_WHERE_ONLY()
        {
            
            lblCOLUMNS2.Visible = true;
            lvCOLUMNS2.Visible = true;
            lvCOLUMNS1.Visible = false;
            lblCOLUMNS1.Visible = false;
        }
        private void SHOW_COLUMNS_ONLY()
        {
            lblCOLUMNS1.Visible = true;
            lvCOLUMNS1.Visible = true;

            lblCOLUMNS2.Visible = false;
            lvCOLUMNS2.Visible = false;



        }
        private void cboCustomMethods_SelectedIndexChanged(object sender, EventArgs e)
        {

          

            switch (cboCustomMethods.Text)
            {
                case "SELECT":  //show both
                    lblCOLUMNS1.Text = "COLUMNS TO SELECT:";
                    lblCOLUMNS2.Text = "COLUMNS IN WHERE CLAUSE:";
                    txtCUSTOM_METHOD_NAME.Text = "SELECT_CUSTOM";
                    SHOW_BOTH();
                    break;
                case "SELECT COUNT (*)":
                    lblCOLUMNS1.Text = "";
                    lblCOLUMNS2.Text = "COLUMNS IN WHERE CLAUSE:";
                    txtCUSTOM_METHOD_NAME.Text = "SELECT_COUNT_CUSTOM";
                    SHOW_WHERE_ONLY();
                    break;
                case "SELECT TOP 1": //show both
                    lblCOLUMNS1.Text = "COLUMNS TO SELECT:";
                    lblCOLUMNS2.Text = "COLUMNS IN WHERE CLAUSE:";
                    txtCUSTOM_METHOD_NAME.Text = "SELECT_TOP_1_CUSTOM";
                    SHOW_BOTH();
                    break;
                case "SELECT MIN()": //show both
                    lblCOLUMNS1.Text = "COLUMN TO SELECT:";
                    lblCOLUMNS2.Text = "COLUMNS IN WHERE CLAUSE:";
                    txtCUSTOM_METHOD_NAME.Text = "SELECT_MIN_CUSTOM";
                    SHOW_BOTH();
                    break;
                case "SELECT MAX()":
                    lblCOLUMNS1.Text = "COLUMN TO SELECT:";
                    lblCOLUMNS2.Text = "COLUMNS IN WHERE CLAUSE:";
                    txtCUSTOM_METHOD_NAME.Text = "SELECT_MAX_CUSTOM";
                    SHOW_BOTH();
                    break;
                case "SELECT BETWEEN DATES": //show both
                    lblCOLUMNS1.Text = "COLUMNS TO SELECT:";
                    lblCOLUMNS2.Text = "COLUMNS IN BETWEEN CLAUSE:";
                    txtCUSTOM_METHOD_NAME.Text = "SELECT_BETWEEN_DATES_CUSTOM";
                    SHOW_BOTH();
                    break;
                case "SELECT SUM()": //show both
                    lblCOLUMNS1.Text = "COLUMNS TO SUM:";
                    lblCOLUMNS2.Text = "COLUMNS IN WHERE CLAUSE:";
                    txtCUSTOM_METHOD_NAME.Text = "SELECT_SUM_CUSTOM";
                    SHOW_BOTH();
                    break;
                case "INSERT":
                    lblCOLUMNS1.Text = "COLUMNS TO INSERT";
                    lblCOLUMNS2.Text = "";
                    txtCUSTOM_METHOD_NAME.Text = "INSERT_CUSTOM";
                    SHOW_COLUMNS_ONLY();
                    break;
                case "INSERT WHERE NOT EXISTS": //show both
                    lblCOLUMNS1.Text = "COLUMNS TO INSERT";
                    lblCOLUMNS2.Text = "COLUMNS IN WHERE NOT EXISTS";
                    txtCUSTOM_METHOD_NAME.Text = "INSERT_WHERE_NOT_EXISTS_CUSTOM";
                    SHOW_BOTH();
                    break;
                case "UPDATE": //show both
                    lblCOLUMNS1.Text = "COLUMNS TO UPDATE";
                    lblCOLUMNS2.Text = "COLUMNS IN WHERE CLAUSE:";
                    txtCUSTOM_METHOD_NAME.Text = "UPDATE_CUSTOM";
                    SHOW_BOTH();
                    break;
                case "DELETE":
                    lblCOLUMNS1.Text = "";
                    lblCOLUMNS2.Text = "COLUMNS IN WHERE CLAUSE:";
                    txtCUSTOM_METHOD_NAME.Text = "DELETE_CUSTOM";
                    SHOW_WHERE_ONLY();
                    break;
            }

        }
        private void PopulateListView(ListView LV, string OBJECT_ID, string TABLE_NAME)
        {


            LV.Items.Clear();
            LV.Columns.Clear();


            LV.View = View.Details;
            LV.Columns.Add("COLUMN NAME");
            LV.Columns.Add("DATA TYPE");
            LV.CheckBoxes = true;
            LV.GridLines = true;


            int i = 0;
            Processing = true;
            SQLSVR_METADATA sqlsvr = new SQLSVR_METADATA();
            sqlsvr.SERVER_NAME = cboServers.Text;
            sqlsvr.DATABASE_NAME = cboDatabases.Text;
            sqlsvr.TABLE_NAME = TABLE_NAME;
            sqlsvr.OBJECT_ID = OBJECT_ID;

            DataSet DS = null;
            if (optTABLES.Checked)
            {
                DS = sqlsvr.GetColumnsInTable_DS();
            }
            else
            {
                DS = sqlsvr.GetColumnsInView_DS();
            }


            foreach (DataRow r in DS.Tables[0].Rows)
            {
                i++;
                ListViewItem listitem = new ListViewItem(r["COLUMN_NAME"].ToString());
                listitem.SubItems.Add(r["TYPE_NAME"].ToString());
            //    listitem.SubItems[0].Text = r["TYPE_NAME"].ToString();
                LV.Items.Add(listitem);
                if (i % 2 == 0)
                {
                    if (optTABLES.Checked)
                    {
                        listitem.BackColor = Color.LightBlue;
                    }
                    else
                    {
                        listitem.BackColor = Color.LightGray;
                    }
                }
                else
                {
                    if (optTABLES.Checked)
                    {
                        listitem.BackColor = Color.Beige;
                    }
                    else
                    {
                        listitem.BackColor = Color.LightSkyBlue;
                    }
                }

            }
            LV.Columns[0].Width = 250;
            LV.Columns[1].Width = 200;
            Processing = false;
            sqlsvr = null;
        }

        private void btnAddCustomMethod_Click(object sender, EventArgs e)
        {

            string COL1 = null;

            foreach(ListViewItem lvi1 in this.lvCOLUMNS1.Items)
            {
                if(lvi1.Checked)
                {
                    COL1 += lvi1.Text + "|";
                }
            }


            string COL2 = null;
            foreach (ListViewItem lvi2 in this.lvCOLUMNS2.Items)
            {
                if (lvi2.Checked)
                {
                    COL2 += lvi2.Text + "|";
                }
            }
            if(COL1 == null)
            {
                COL1 = "NA";
            }
            if (COL2 == null)
            {
                COL2 = "NA";
            }
            VALIDATE_ADD_CUSTOM(txtCUSTOM_METHOD_NAME.Text,COL1,COL2, cboCustomMethods.Text);
            foreach (ListViewItem lvi1 in this.lvCOLUMNS1.Items)
            {
                if (lvi1.Checked)
                {
                    lvi1.Checked=false;
                }
            }
            foreach (ListViewItem lvi2 in this.lvCOLUMNS2.Items)
            {
                if (lvi2.Checked)
                {
                    lvi2.Checked = false;
                }
            }

        }

        private void btnRemoveAllMethods_Click(object sender, EventArgs e)
        {
            GRD_CUSTOM_METHODS.Rows.Clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkDEL_PK_CheckedChanged(object sender, EventArgs e)
        {

        }
        private string CreateCSCodeTable(bool Methods)
        {
            FREDDIE_UTL UTL = new FREDDIE_UTL(cboServers.Text, cboDatabases.Text, cboTables.Text, Convert.ToInt32(cboTables.SelectedValue), false, chkPK.Checked,
                 chkINS.Checked, chkUPD_PK.Checked, chkDEL_PK.Checked, chkSEL_PK.Checked, chkDEL_ALL.Checked, chkSEL_ALL.Checked, chkSEL_100.Checked);
            if (Methods)
            {
                return UTL.GENERATE_METHODS();
            }
            else
            {
             
                return UTL.GENERATE_SQLSERVER_DB_CLASS();
            }
        }
        
        private void button3_Click(object sender, EventArgs e)
        {

            GENERATE(true);


        }

        private void GENERATE(bool Methods)
        {
            Screen[] sc;
            sc = Screen.AllScreens;
            Form5 f = new Form5();
            //  f.FormBorderStyle = FormBorderStyle.None;
            f.Left = sc[1].Bounds.Width;
            f.Top = sc[1].Bounds.Height;

            string FORM_TEXT = null;
            if (Methods)
            {
                FORM_TEXT = "C# CODE - METHODS ONLY FOR: ";
            }
            else
            {
                FORM_TEXT = "C# CODE - ENTIRE SQL SERVER DATABASE CLASS FOR: ";
            }

            if (optVIEWS.Checked)

            {
                FREDDIE_UTL UTL = new FREDDIE_UTL(
                    cboServers.Text, 
                    cboDatabases.Text, 

                    cboViews.Text, 
                    Convert.ToInt32(cboViews.SelectedValue),


                    optVIEWS.Checked, 
                    chkPK.Checked,
                    chkINS.Checked, 
                    chkUPD_PK.Checked, 
                    chkDEL_PK.Checked, 
                    chkSEL_PK.Checked, 
                    chkDEL_ALL.Checked, 
                    chkSEL_ALL.Checked, 
                    chkSEL_100.Checked
                    );

                UTL.GENERATE_VIEW_OBJECT(false);
                if (Methods)
                {
                   
                    UTL.GENERATE_METHODS();
                }
                else
                {
                    UTL.GENERATE_SQLSERVER_DB_CLASS();
                }

                f.txtCS.Text = UTL.TABLE_VIEW_CLASS_CS;
                f.txtCSDB.Text = UTL.DATABASE_CLASS_CS;
                f.Text = FORM_TEXT + cboViews.Text;
                f.txtCS.BackColor = cboViews.BackColor;
                f.txtCSDB.BackColor = cboViews.BackColor;






            }
            else
            {
             
                FREDDIE_UTL UTL = new FREDDIE_UTL(
                    cboServers.Text,
                    cboDatabases.Text,

                    cboTables.Text,
                    Convert.ToInt32(cboTables.SelectedValue),


                    optVIEWS.Checked,
                    chkPK.Checked,
                    chkINS.Checked,
                    chkUPD_PK.Checked,
                    chkDEL_PK.Checked,
                    chkSEL_PK.Checked,
                    chkDEL_ALL.Checked,
                    chkSEL_ALL.Checked,
                    chkSEL_100.Checked
                    );

                UTL.GENERATE_TABLE_OBJECT(false);

                if (Methods)
                {

                    UTL.GENERATE_METHODS();
                }
                else
                {
                    UTL.GENERATE_SQLSERVER_DB_CLASS();
                }

                f.txtCS.Text = UTL.TABLE_VIEW_CLASS_CS;
                f.txtCSDB.Text = UTL.DATABASE_CLASS_CS;
                f.Text = FORM_TEXT + cboTables.Text;
                f.txtCS.BackColor = cboTables.BackColor;
                f.txtCSDB.BackColor = cboTables.BackColor;








            }
     
         


          
            f.Show();
        }


        private void btnCreateCS_ALL_Click(object sender, EventArgs e)
        {
            GENERATE(false);
        }

        private void btnCreateCS_CUSTOM_METHODS_Click(object sender, EventArgs e)
        {
        //    List<FREDDIE_CS_METHOD> lst = new List<FREDDIE_CS_METHOD>();
            string[] cols;
            string[] where;
            string  Cols=null;
            string  Where=null;
            foreach (DataGridViewRow row in GRD_CUSTOM_METHODS.Rows)
            {
                cols = row.Cells[1].Value.ToString().Split('|');
                for(int i = 0; i < cols.Length-1; i++)
                {
                    if (i < cols.Length - 2)
                    {
                        Cols += cols[i].ToString() + ", " + Environment.NewLine;
                    }
                    else
                    {
                        Cols += cols[i].ToString() +   Environment.NewLine;
                    }
                }
                where = row.Cells[2].Value.ToString().Split('|');
                for (int i = 0; i < where.Length - 1; i++)
                {
                    if (i < where.Length - 2)
                    {
                        Where += where[i].ToString() + " AND " + Environment.NewLine;
                    }
                    else
                    {
                        Where += where[i].ToString() + Environment.NewLine;
                    }
                }
               //FREDDIE_CS_METHOD o = new FREDDIE_CS_METHOD
               // {
               //     CS = "",
               //     METHOD_NAME = row.Cells[0].Value.ToString(),
               //     COLUMNS= Cols,
               //      WHERE = Where,
               //       METHOD_TYPE = row.Cells[3].Value.ToString()
               //};
               // lst.Add(o);
            }
        }
    }
}
