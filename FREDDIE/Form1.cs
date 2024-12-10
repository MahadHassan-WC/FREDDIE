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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void cREATECOBJECTSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FREDDIE.Form4 frm = new FREDDIE.Form4();
            //frm.MdiParent = this;
            //frm.Show();
        }

        private void mAINTAINMYDATABASESToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tABLESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FREDDIE.Form7 frm = new FREDDIE.Form7();
            frm.MdiParent = this;
            frm.Show();
        }

        private void vIEWSToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tABLESToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void vIEWSToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void tABLESVIEWToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void mODELOBJECTSToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void mODELOBJECTSToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void rEADOBJECTSToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void iUDOBJECTSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void gUIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void rEADSTOREDPROCEDURESToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cUSTOMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FREDDIE.Form14 frm = new FREDDIE.Form14();
            frm.MdiParent = this;
            frm.Show();
        }

        private void cREATESQLSERVEROBJECTSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void sETTINGSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FREDDIE.Form5 frm = new FREDDIE.Form5();
            frm.MdiParent = this;
            frm.Show();
        }

        private void mVCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FREDDIE.Form6 frm = new FREDDIE.Form6();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
