using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FREDDIE
{
    public partial class frmPREFERANCES : Form
    {
        public frmPREFERANCES()
        {
            InitializeComponent();
        }
        private const string XML_FILE = @"c:\pete\MyMSSQL_SERVERS.xml";
        XDocument xmldoc;
        private bool bFormLoading = false;
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void LoadDatabases()
        {
            bFormLoading = true;
            MSSQL_CS_META_DATA db = new MSSQL_CS_META_DATA();
            DataSet DS = db.GET_DATABASES_THIS_SERVER_DS(lstMyServers.Text);


            lstMyDatabases.Items.Clear();

            foreach (DataRow r in DS.Tables[0].Rows)
            {
                lstMyDatabases.Items.Add(r["NAME"]);
            }
 
            DS = null;
            db = null;
            bFormLoading = false;
        }
        private void frmPREFERANCES_Load(object sender, EventArgs e)
        {
            bFormLoading = true;
            BindGrid();
            bFormLoading = false;
        }
        private void Reset()
        {
            txtSERVER_NAME.Clear();
            txtSERVER_ID.Clear();
            txtSERVER_NAME.Focus();


            btnDeleteServer.Enabled = false;
            btnUpdateServer.Enabled = false;
            
            lstMyDatabases.Items.Clear();




        }
        private void BindGrid()
        {
            try
            {

                //GRD_XML.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                //GRD_XML.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                //GRD_XML.ReadOnly = true;
                //GRD_XML.AllowUserToAddRows = false;
                //GRD_XML.AllowUserToDeleteRows = false;
                //GRD_XML.RowsDefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
                //GRD_XML.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.LightGoldenrodYellow;
                DataSet dataSet = new DataSet();
                dataSet.ReadXml(XML_FILE);

                //  GRD_XML.DataSource = dataSet.Tables[0];

                lstMyServers.DisplayMember = "SERVER_NAME";
                lstMyServers.ValueMember = "SERVER_ID";
                lstMyServers.DataSource = dataSet.Tables[0];


                //lst2XML.Items.Clear();

                //foreach (DataRow r in dataSet.Tables[0].Rows)
                //{
                //    lst2XML.Items.Add(r["SERVER_NAME"]);
                //}

                //cboXML.DisplayMember = "SERVER_NAME";
                //cboXML.DataSource = dataSet.Tables[0];

                xmldoc = XDocument.Load(XML_FILE);   //add xml document  

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstMyServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!bFormLoading )
            {
                txtSERVER_NAME.Text = lstMyServers.Text;
                txtSERVER_ID.Text = lstMyServers.SelectedValue.ToString();
                btnDeleteServer.Enabled = true;
                btnUpdateServer.Enabled = true;
                LoadDatabases();

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnAddServer_Click(object sender, EventArgs e)
        {
            try
            {

                XDocument xmlDoc = XDocument.Load(XML_FILE);
                int xmlValue = (from prod in xmlDoc.Descendants("SERVER") select (int.Parse(prod.Element("SERVER_ID").Value))).Max() + 1;
                txtSERVER_ID.Text = xmlValue.ToString();
                XElement SVR = new XElement
                    (
                        "SERVER",
                         new XElement("SERVER_NAME", txtSERVER_NAME.Text),
                         new XElement("SERVER_ID", txtSERVER_ID.Text)
                    );

                xmldoc.Root.Add(SVR);
                xmldoc.Save(XML_FILE);
                BindGrid();
                Reset(); // For clear textbox  

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteServer_Click(object sender, EventArgs e)
        {
            try
            {
                XElement SVR = xmldoc.Descendants("SERVER").FirstOrDefault(p => p.Element("SERVER_ID").Value == txtSERVER_ID.Text);
                if (SVR != null)
                {
                    SVR.Remove();
                    xmldoc.Save(XML_FILE);
                    BindGrid();
                    Reset();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateServer_Click(object sender, EventArgs e)
        {
            try
            {
                XElement SVR = xmldoc.Descendants("SERVER").FirstOrDefault(p => p.Element("SERVER_ID").Value == txtSERVER_ID.Text);
                if (SVR != null)
                {
                    SVR.Element("SERVER_NAME").Value = txtSERVER_NAME.Text;
                    xmldoc.Save(XML_FILE);
                    BindGrid();
                    Reset();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveMyDatabases_Click(object sender, EventArgs e)
        {

        }
    }
}
