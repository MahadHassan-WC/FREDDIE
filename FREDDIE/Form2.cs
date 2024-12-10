using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SqlSvrMeta;
using System.Data.SqlClient;
using System.Configuration;


namespace FREDDIE
{
    public partial class Form2 : Form_BASE_1
    {
      

       

        private bool Processing = false;
        public Form2()
        {
            InitializeComponent();
        }
        //private void ClearListView(ListView lv)
        //{
        //    lv.Items.Clear();
        //    lv.Columns.Clear();
        //}
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
                if(PK)
                {
                    chkUPD_PK.Enabled = true;
                    chkDEL_PK.Enabled = true;
                    chkSEL_PK.Enabled = true;
                }
            }


        }
        
        private void Form2_Load(object sender, EventArgs e)
        {
            Processing = true;
        
            cboServers.Items.Add("WCSSQL2");
            cboServers.Items.Add(@"DEV\SQLTEST");
            optTABLES.Checked = true;
            Processing = false;

            SELECT_WCSSQL2();
            cboDatabases.Text = "DBA_CORP";
            optTABLES.Checked = true;
            cboTables.Text = "AMP";


          //  Load_lstDefaultOptions(optVIEWS.Checked);
          //  chkDELETE.Checked = true;
          //  chkDELETE_ALL.Checked = true;
          //  chkINSERT.Checked = true;
          //  chkINSERT_NOT_EXISTS.Checked = true;
          //  chkSELECT_ALL_DS.Checked = true;
          //  chkSELECT_PK_DS.Checked = true;
         //   chkUPDATE_A_ROW.Checked = true;
          //  GET_DB_METHODS();
        //    TAB_CONTROL_TABLE.SelectedIndex = 2;
        }
     

        private void cboServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!Processing)
            {
                Processing = true;
                SQLSVR_METADATA sqlsvr = new SQLSVR_METADATA();
                sqlsvr.SERVER_NAME = cboServers.Text;
                DataSet DS=  sqlsvr.GetDatabases_DS();
                this.cboDatabases.DataSource = DS.Tables[0];
                cboDatabases.DisplayMember = "NAME";
                cboDatabases.ValueMember = "NAME";
                cboDatabases.SelectedIndex = -1;
                sqlsvr = null;
                Processing = false;

            }
        }

        private void SELECT_WCSSQL2()
        {
            
                cboServers.Text = "WCSSQL2";
              
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
                txtCS.Clear();
                tv.Nodes.Clear();
                GUI_UTL.ClearCheckBoxControls(grpOPTIONS);
                lblOBJECT_ID.Text = lblOBJECT_ID.Tag.ToString();
                lblTABLE_SCHEMA.Text = lblTABLE_SCHEMA.Tag.ToString();
                chkPK.Checked = false;
                sqlsvr = null;
                Processing = false;
            }
        }

        private void HandleScreenForTablesOrViews()
        {
            lblTABLE_SCHEMA.Text = lblTABLE_SCHEMA.Tag.ToString();
            lblOBJECT_ID.Text = lblOBJECT_ID.Tag.ToString();
            tv.Nodes.Clear();
            txtCS.Clear();
            GUI_UTL.ClearCheckBoxControls(grpOPTIONS);
            GUI_UTL.EnableCheckBoxControls(grpOPTIONS);
            cboTables.Visible = optTABLES.Checked;
            cboViews.Visible = optVIEWS.Checked;
            chkPK.Visible = optTABLES.Checked;
            if(optVIEWS.Checked)
            {
                txtOBJ.BackColor = cboViews.BackColor;
                cboDatabases.BackColor = cboViews.BackColor;
                cboServers.BackColor = cboViews.BackColor;
                tv.BackColor = cboViews.BackColor;
                txtCS.BackColor= cboViews.BackColor;
                chkDEL_ALL.Enabled = false;
                chkDEL_PK.Enabled = false;
                chkINS.Enabled = false;
                chkUPD_PK.Enabled = false;
            }
           else
            {
                txtOBJ.BackColor = cboTables.BackColor;
                cboDatabases.BackColor = cboTables.BackColor;
                cboServers.BackColor = cboTables.BackColor;
                tv.BackColor = cboTables.BackColor;
                txtCS.BackColor = cboTables.BackColor;
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

        private void cboViews_SelectedIndexChanged(object sender, EventArgs e)
        {
            RESET_SCREEN_CHANGE_IN_TABLE_VIEW(true);
        }
        private void PopulateListView(ListView LV,string OBJECT_ID, string TABLE_NAME)
        {
            

            LV.Items.Clear();
            LV.Columns.Clear();


            LV.View = View.Details;
            LV.Columns.Add("COLUMN NAME");
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
            Processing =false;
            sqlsvr = null;
        }
      
        private void RESET_SCREEN_CHANGE_IN_TABLE_VIEW(bool View)
        {

            txtCS.Clear();
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
                
                FORMAT_CHECKBOX_OPTIONS(View, chkPK.Checked);
                
               
            }






        }
        private void cboTables_SelectedIndexChanged(object sender, EventArgs e)
        {

            /////anytime a user changes a table we are STARTING OVER
            /////this is the same if they change servers, views, or change from view to table or visa versa
            ////so the screen needs reset

            /// screen reset for table and view
            /// 1. Clear txtCS
            /// 2. chkPK.Checked = false
            /// 3. handle check boxes
            /// 4. Only in this case do we need to check for a PK
            RESET_SCREEN_CHANGE_IN_TABLE_VIEW(false);


        }

        private void button2_Click(object sender, EventArgs e)
        {
            SQLSVR_METADATA sqlsvr = new SQLSVR_METADATA();
            sqlsvr.SERVER_NAME = cboServers.Text;
            sqlsvr.DATABASE_NAME = cboDatabases.Text;
            sqlsvr.TABLE_NAME = cboTables.Text;
            sqlsvr.OBJECT_ID = cboTables.SelectedValue.ToString();
            List<CS_SQLSERVER_COLUMN_MATRIX> lst = new List<CS_SQLSERVER_COLUMN_MATRIX>();
            lst = sqlsvr.GET_CS_COLUMN_MATRIX_TABLE();
            sqlsvr = null;

        }
       


       
       

        private void btnOBJ_Click(object sender, EventArgs e)
        {
            if (!Processing)
            {
                CreateOBJ();
            }

        }
        private void CreateOBJ()
        {
            
        }

      

        private void PopulateTreeViewView()
        {
            tv.Nodes.Clear();
            SQLSVR_METADATA sqlsvr = new SQLSVR_METADATA();
            sqlsvr.SERVER_NAME = cboServers.Text;
            sqlsvr.DATABASE_NAME = cboDatabases.Text;
            sqlsvr.VIEW_NAME = cboViews.Text;
            sqlsvr.OBJECT_ID = cboViews.SelectedValue.ToString();
            DataSet DS = sqlsvr.GetColumnsInView_DS();
            int i = 0;
            foreach (DataRow r in DS.Tables[0].Rows)
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
        }  //// end of method
        private void PopulateTreeViewTable()
        {
            tv.Nodes.Clear();
            SQLSVR_METADATA sqlsvr = new SQLSVR_METADATA();
            sqlsvr.SERVER_NAME = cboServers.Text;
            sqlsvr.DATABASE_NAME = cboDatabases.Text;
            sqlsvr.TABLE_NAME = cboTables.Text;
            sqlsvr.OBJECT_ID = cboTables.SelectedValue.ToString();
            DataSet DS = sqlsvr.GetColumnsInTableAndPK_DS();
            int i = 0;
            foreach(DataRow r in DS.Tables[0].Rows)
            {
                lblTABLE_SCHEMA.Text = lblTABLE_SCHEMA.Tag + r["TABLE_SCHEMA"].ToString();
                lblOBJECT_ID.Text     = lblOBJECT_ID.Tag   + r["OBJECT_ID"].ToString();
                tv.Nodes.Add(r["COLUMN_NAME"].ToString());
                if (r["PK"].ToString() == "Y")
                {
                    tv.Nodes[i].Nodes.Add( "PK");
                }
                tv.Nodes[i].Nodes.Add(r["TYPE_NAME"].ToString());
                if (r["IS_IDENTITY"].ToString() == "1")
                {
                    tv.Nodes[i].Nodes.Add("IDENTITY:" + r["IDENT_SEED"].ToString() + "/" + r["IDENT_INCR"].ToString());
                }
                if ( (r["IS_NULLABLE"].ToString() == "0" ))
                {
                    tv.Nodes[i].Nodes.Add("NOT NULL"  );
                }
                tv.Nodes[i].Nodes.Add("LENGTH:" + r["MAX_LENGTH"].ToString());
                tv.Nodes[i].Nodes.Add("PRECISION:" + r["PRECISION"].ToString());
                tv.Nodes[i].Nodes.Add("SCALE:" + r["SCALE"].ToString());
                tv.Nodes[i].Nodes.Add("COLUMN ID:" + r["COLUMN_ID"].ToString());
                i++;
            } /// end of loop
            sqlsvr = null;
         }  //// end of method

        private void btnDB_METHOD_Click(object sender, EventArgs e)
        {

          //  GET_DB_METHODS();

        }
        
        private void btnDB_Click(object sender, EventArgs e)
        {

        }

        private void btnAddServer_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }


        private void GenerateCS()
        {
            txtCS.Clear();
            int OBJECT_ID = 0;
            string TABLE_OR_VIEW_NAME = null;
            if(optTABLES.Checked)
            {
                TABLE_OR_VIEW_NAME = cboTables.Text;
                OBJECT_ID = Convert.ToInt32(cboTables.SelectedValue);
            }
            else
            {
                TABLE_OR_VIEW_NAME = cboViews.Text;
                OBJECT_ID = Convert.ToInt32(cboViews.SelectedValue);
            }
            FREDDIE_UTL UTL = new FREDDIE_UTL(cboServers.Text,cboDatabases.Text, TABLE_OR_VIEW_NAME, OBJECT_ID,optVIEWS.Checked,chkPK.Checked,
                chkINS.Checked,chkUPD_PK.Checked,chkDEL_PK.Checked, chkSEL_PK.Checked, chkDEL_ALL.Checked, chkSEL_ALL.Checked,chkSEL_100.Checked);
         
            txtCS.Text = UTL.GENERATE_METHODS( );

            // LST = new List<FREDDIE_CS_METHOD>();
            //LST = UTL.LST_FREDDIE_CS_METHOD ;
            //vScrollBar1.Minimum =0;
            //vScrollBar1.Maximum = LST.Count-1;
            vScrollBar1.Refresh();
            UTL = null;


        }
        private void btnGenerateDefault_Click(object sender, EventArgs e)
        {
            GenerateCS();
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                //txtCS.Text = LST[Convert.ToInt32(vScrollBar1.Value)].CS.ToString();
                //lblMethodName.Text = lblMethodName.Tag + LST[Convert.ToInt32(vScrollBar1.Value )].METHOD_NAME.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
