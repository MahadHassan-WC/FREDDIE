namespace FREDDIE
{
    partial class Form4
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpOPTIONS = new System.Windows.Forms.GroupBox();
            this.chkSEL_ALL = new System.Windows.Forms.CheckBox();
            this.chkDEL_PK = new System.Windows.Forms.CheckBox();
            this.chkSEL_100 = new System.Windows.Forms.CheckBox();
            this.chkINS = new System.Windows.Forms.CheckBox();
            this.chkSEL_PK = new System.Windows.Forms.CheckBox();
            this.chkDEL_ALL = new System.Windows.Forms.CheckBox();
            this.chkUPD_PK = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkPK = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTABLE_SCHEMA = new System.Windows.Forms.Label();
            this.lblOBJECT_ID = new System.Windows.Forms.Label();
            this.tv = new System.Windows.Forms.TreeView();
            this.cboViews = new System.Windows.Forms.ComboBox();
            this.cboServers = new System.Windows.Forms.ComboBox();
            this.cboTables = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboDatabases = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.optVIEWS = new System.Windows.Forms.RadioButton();
            this.optTABLES = new System.Windows.Forms.RadioButton();
            this.cboCustomMethods = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCUSTOM_METHOD_NAME = new System.Windows.Forms.TextBox();
            this.lblCOLUMNS2 = new System.Windows.Forms.Label();
            this.lblCOLUMNS1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRemoveAllMethods = new System.Windows.Forms.Button();
            this.btnRemoveSelectedMethod = new System.Windows.Forms.Button();
            this.GRD_CUSTOM_METHODS = new System.Windows.Forms.DataGridView();
            this.btnAddCustomMethod = new System.Windows.Forms.Button();
            this.lvCOLUMNS2 = new System.Windows.Forms.ListView();
            this.lvCOLUMNS1 = new System.Windows.Forms.ListView();
            this.button3 = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnCreateCS_ALL = new System.Windows.Forms.Button();
            this.btnCreateCS_CUSTOM_METHODS = new System.Windows.Forms.Button();
            this.grpOPTIONS.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GRD_CUSTOM_METHODS)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpOPTIONS
            // 
            this.grpOPTIONS.Controls.Add(this.chkSEL_ALL);
            this.grpOPTIONS.Controls.Add(this.chkDEL_PK);
            this.grpOPTIONS.Controls.Add(this.chkSEL_100);
            this.grpOPTIONS.Controls.Add(this.chkINS);
            this.grpOPTIONS.Controls.Add(this.button3);
            this.grpOPTIONS.Controls.Add(this.chkSEL_PK);
            this.grpOPTIONS.Controls.Add(this.chkDEL_ALL);
            this.grpOPTIONS.Controls.Add(this.chkUPD_PK);
            this.grpOPTIONS.Location = new System.Drawing.Point(6, 522);
            this.grpOPTIONS.Name = "grpOPTIONS";
            this.grpOPTIONS.Size = new System.Drawing.Size(425, 137);
            this.grpOPTIONS.TabIndex = 48;
            this.grpOPTIONS.TabStop = false;
            this.grpOPTIONS.Text = "DEFAULT METHODS:";
            // 
            // chkSEL_ALL
            // 
            this.chkSEL_ALL.AutoSize = true;
            this.chkSEL_ALL.Location = new System.Drawing.Point(6, 73);
            this.chkSEL_ALL.Name = "chkSEL_ALL";
            this.chkSEL_ALL.Size = new System.Drawing.Size(99, 19);
            this.chkSEL_ALL.TabIndex = 30;
            this.chkSEL_ALL.Text = "SELECT ALL";
            this.chkSEL_ALL.UseVisualStyleBackColor = true;
            // 
            // chkDEL_PK
            // 
            this.chkDEL_PK.AutoSize = true;
            this.chkDEL_PK.Location = new System.Drawing.Point(290, 27);
            this.chkDEL_PK.Name = "chkDEL_PK";
            this.chkDEL_PK.Size = new System.Drawing.Size(113, 19);
            this.chkDEL_PK.TabIndex = 34;
            this.chkDEL_PK.Text = "DELETE BY PK";
            this.chkDEL_PK.UseVisualStyleBackColor = true;
            this.chkDEL_PK.CheckedChanged += new System.EventHandler(this.chkDEL_PK_CheckedChanged);
            // 
            // chkSEL_100
            // 
            this.chkSEL_100.AutoSize = true;
            this.chkSEL_100.Location = new System.Drawing.Point(6, 50);
            this.chkSEL_100.Name = "chkSEL_100";
            this.chkSEL_100.Size = new System.Drawing.Size(126, 19);
            this.chkSEL_100.TabIndex = 29;
            this.chkSEL_100.Text = "SELECT TOP 100";
            this.chkSEL_100.UseVisualStyleBackColor = true;
            // 
            // chkINS
            // 
            this.chkINS.AutoSize = true;
            this.chkINS.Location = new System.Drawing.Point(147, 27);
            this.chkINS.Name = "chkINS";
            this.chkINS.Size = new System.Drawing.Size(115, 19);
            this.chkINS.TabIndex = 28;
            this.chkINS.Text = "INSERT A ROW";
            this.chkINS.UseVisualStyleBackColor = true;
            // 
            // chkSEL_PK
            // 
            this.chkSEL_PK.AutoSize = true;
            this.chkSEL_PK.Location = new System.Drawing.Point(6, 27);
            this.chkSEL_PK.Name = "chkSEL_PK";
            this.chkSEL_PK.Size = new System.Drawing.Size(112, 19);
            this.chkSEL_PK.TabIndex = 31;
            this.chkSEL_PK.Text = "SELECT BY PK";
            this.chkSEL_PK.UseVisualStyleBackColor = true;
            // 
            // chkDEL_ALL
            // 
            this.chkDEL_ALL.AutoSize = true;
            this.chkDEL_ALL.Location = new System.Drawing.Point(290, 50);
            this.chkDEL_ALL.Name = "chkDEL_ALL";
            this.chkDEL_ALL.Size = new System.Drawing.Size(100, 19);
            this.chkDEL_ALL.TabIndex = 27;
            this.chkDEL_ALL.Text = "DELETE ALL";
            this.chkDEL_ALL.UseVisualStyleBackColor = true;
            // 
            // chkUPD_PK
            // 
            this.chkUPD_PK.AutoSize = true;
            this.chkUPD_PK.Location = new System.Drawing.Point(145, 50);
            this.chkUPD_PK.Name = "chkUPD_PK";
            this.chkUPD_PK.Size = new System.Drawing.Size(138, 19);
            this.chkUPD_PK.TabIndex = 26;
            this.chkUPD_PK.Text = "UPDATE A ROW PK";
            this.chkUPD_PK.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(204, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 15);
            this.label6.TabIndex = 47;
            this.label6.Text = "DATABASES:";
            // 
            // chkPK
            // 
            this.chkPK.AutoSize = true;
            this.chkPK.Location = new System.Drawing.Point(310, 159);
            this.chkPK.Name = "chkPK";
            this.chkPK.Size = new System.Drawing.Size(112, 19);
            this.chkPK.TabIndex = 46;
            this.chkPK.Text = "TABLE HAS PK";
            this.chkPK.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 15);
            this.label2.TabIndex = 45;
            this.label2.Tag = "COLUMN DATA:";
            this.label2.Text = "COLUMN DATA:";
            // 
            // lblTABLE_SCHEMA
            // 
            this.lblTABLE_SCHEMA.AutoSize = true;
            this.lblTABLE_SCHEMA.Location = new System.Drawing.Point(2, 145);
            this.lblTABLE_SCHEMA.Name = "lblTABLE_SCHEMA";
            this.lblTABLE_SCHEMA.Size = new System.Drawing.Size(105, 15);
            this.lblTABLE_SCHEMA.TabIndex = 44;
            this.lblTABLE_SCHEMA.Tag = "TABLE_SCHEMA:";
            this.lblTABLE_SCHEMA.Text = "TABLE_SCHEMA:";
            // 
            // lblOBJECT_ID
            // 
            this.lblOBJECT_ID.AutoSize = true;
            this.lblOBJECT_ID.Location = new System.Drawing.Point(2, 128);
            this.lblOBJECT_ID.Name = "lblOBJECT_ID";
            this.lblOBJECT_ID.Size = new System.Drawing.Size(75, 15);
            this.lblOBJECT_ID.TabIndex = 43;
            this.lblOBJECT_ID.Tag = "OBJECT_ID:";
            this.lblOBJECT_ID.Text = "OBJECT_ID:";
            // 
            // tv
            // 
            this.tv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tv.Location = new System.Drawing.Point(4, 194);
            this.tv.Name = "tv";
            this.tv.Size = new System.Drawing.Size(410, 302);
            this.tv.TabIndex = 42;
            // 
            // cboViews
            // 
            this.cboViews.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.cboViews.FormattingEnabled = true;
            this.cboViews.Location = new System.Drawing.Point(6, 105);
            this.cboViews.Name = "cboViews";
            this.cboViews.Size = new System.Drawing.Size(410, 21);
            this.cboViews.TabIndex = 41;
            this.cboViews.SelectedIndexChanged += new System.EventHandler(this.cboViews_SelectedIndexChanged);
            // 
            // cboServers
            // 
            this.cboServers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cboServers.FormattingEnabled = true;
            this.cboServers.Location = new System.Drawing.Point(6, 33);
            this.cboServers.Name = "cboServers";
            this.cboServers.Size = new System.Drawing.Size(200, 21);
            this.cboServers.TabIndex = 36;
            this.cboServers.SelectedIndexChanged += new System.EventHandler(this.cboServers_SelectedIndexChanged);
            // 
            // cboTables
            // 
            this.cboTables.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cboTables.FormattingEnabled = true;
            this.cboTables.Location = new System.Drawing.Point(6, 105);
            this.cboTables.Name = "cboTables";
            this.cboTables.Size = new System.Drawing.Size(408, 21);
            this.cboTables.TabIndex = 40;
            this.cboTables.SelectedIndexChanged += new System.EventHandler(this.cboTables_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 37;
            this.label1.Text = "SERVERS:";
            // 
            // cboDatabases
            // 
            this.cboDatabases.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cboDatabases.FormattingEnabled = true;
            this.cboDatabases.Location = new System.Drawing.Point(212, 33);
            this.cboDatabases.Name = "cboDatabases";
            this.cboDatabases.Size = new System.Drawing.Size(205, 21);
            this.cboDatabases.TabIndex = 38;
            this.cboDatabases.SelectedIndexChanged += new System.EventHandler(this.cboDatabases_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.optVIEWS);
            this.groupBox2.Controls.Add(this.optTABLES);
            this.groupBox2.Location = new System.Drawing.Point(6, 59);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(164, 31);
            this.groupBox2.TabIndex = 39;
            this.groupBox2.TabStop = false;
            // 
            // optVIEWS
            // 
            this.optVIEWS.AutoSize = true;
            this.optVIEWS.Location = new System.Drawing.Point(98, 8);
            this.optVIEWS.Name = "optVIEWS";
            this.optVIEWS.Size = new System.Drawing.Size(65, 19);
            this.optVIEWS.TabIndex = 7;
            this.optVIEWS.TabStop = true;
            this.optVIEWS.Text = "VIEWS";
            this.optVIEWS.UseVisualStyleBackColor = true;
            this.optVIEWS.CheckedChanged += new System.EventHandler(this.optVIEWS_CheckedChanged);
            // 
            // optTABLES
            // 
            this.optTABLES.AutoSize = true;
            this.optTABLES.Location = new System.Drawing.Point(7, 10);
            this.optTABLES.Name = "optTABLES";
            this.optTABLES.Size = new System.Drawing.Size(73, 19);
            this.optTABLES.TabIndex = 6;
            this.optTABLES.TabStop = true;
            this.optTABLES.Text = "TABLES";
            this.optTABLES.UseVisualStyleBackColor = true;
            this.optTABLES.CheckedChanged += new System.EventHandler(this.optTABLES_CheckedChanged);
            // 
            // cboCustomMethods
            // 
            this.cboCustomMethods.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cboCustomMethods.FormattingEnabled = true;
            this.cboCustomMethods.Location = new System.Drawing.Point(16, 47);
            this.cboCustomMethods.Name = "cboCustomMethods";
            this.cboCustomMethods.Size = new System.Drawing.Size(550, 21);
            this.cboCustomMethods.TabIndex = 49;
            this.cboCustomMethods.SelectedIndexChanged += new System.EventHandler(this.cboCustomMethods_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 15);
            this.label3.TabIndex = 50;
            this.label3.Text = "CUSTOM METHOD TYPES:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCreateCS_CUSTOM_METHODS);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtCUSTOM_METHOD_NAME);
            this.groupBox1.Controls.Add(this.lblCOLUMNS2);
            this.groupBox1.Controls.Add(this.lblCOLUMNS1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnRemoveAllMethods);
            this.groupBox1.Controls.Add(this.btnRemoveSelectedMethod);
            this.groupBox1.Controls.Add(this.GRD_CUSTOM_METHODS);
            this.groupBox1.Controls.Add(this.btnAddCustomMethod);
            this.groupBox1.Controls.Add(this.lvCOLUMNS2);
            this.groupBox1.Controls.Add(this.lvCOLUMNS1);
            this.groupBox1.Controls.Add(this.cboCustomMethods);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(441, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(835, 653);
            this.groupBox1.TabIndex = 51;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CUSTOM METHODS:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(156, 15);
            this.label5.TabIndex = 62;
            this.label5.Text = "CUSTOM METHOD NAME:";
            // 
            // txtCUSTOM_METHOD_NAME
            // 
            this.txtCUSTOM_METHOD_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtCUSTOM_METHOD_NAME.Location = new System.Drawing.Point(16, 84);
            this.txtCUSTOM_METHOD_NAME.Name = "txtCUSTOM_METHOD_NAME";
            this.txtCUSTOM_METHOD_NAME.Size = new System.Drawing.Size(550, 20);
            this.txtCUSTOM_METHOD_NAME.TabIndex = 61;
            // 
            // lblCOLUMNS2
            // 
            this.lblCOLUMNS2.AutoSize = true;
            this.lblCOLUMNS2.Location = new System.Drawing.Point(422, 108);
            this.lblCOLUMNS2.Name = "lblCOLUMNS2";
            this.lblCOLUMNS2.Size = new System.Drawing.Size(71, 15);
            this.lblCOLUMNS2.TabIndex = 60;
            this.lblCOLUMNS2.Tag = "COLUMNS TO ";
            this.lblCOLUMNS2.Text = "COLUMNS:";
            // 
            // lblCOLUMNS1
            // 
            this.lblCOLUMNS1.AutoSize = true;
            this.lblCOLUMNS1.Location = new System.Drawing.Point(14, 108);
            this.lblCOLUMNS1.Name = "lblCOLUMNS1";
            this.lblCOLUMNS1.Size = new System.Drawing.Size(71, 15);
            this.lblCOLUMNS1.TabIndex = 59;
            this.lblCOLUMNS1.Tag = "COLUMNS TO ";
            this.lblCOLUMNS1.Text = "COLUMNS:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 397);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 15);
            this.label4.TabIndex = 58;
            this.label4.Tag = "METHODS IN QUE:";
            this.label4.Text = "METHODS IN QUE:";
            // 
            // btnRemoveAllMethods
            // 
            this.btnRemoveAllMethods.Location = new System.Drawing.Point(221, 600);
            this.btnRemoveAllMethods.Name = "btnRemoveAllMethods";
            this.btnRemoveAllMethods.Size = new System.Drawing.Size(184, 40);
            this.btnRemoveAllMethods.TabIndex = 56;
            this.btnRemoveAllMethods.Text = "REMOVE ALL METHODS";
            this.btnRemoveAllMethods.UseVisualStyleBackColor = true;
            this.btnRemoveAllMethods.Click += new System.EventHandler(this.btnRemoveAllMethods_Click);
            // 
            // btnRemoveSelectedMethod
            // 
            this.btnRemoveSelectedMethod.Location = new System.Drawing.Point(16, 600);
            this.btnRemoveSelectedMethod.Name = "btnRemoveSelectedMethod";
            this.btnRemoveSelectedMethod.Size = new System.Drawing.Size(200, 40);
            this.btnRemoveSelectedMethod.TabIndex = 55;
            this.btnRemoveSelectedMethod.Text = "REMOVE SELECTED METHOD";
            this.btnRemoveSelectedMethod.UseVisualStyleBackColor = true;
            // 
            // GRD_CUSTOM_METHODS
            // 
            this.GRD_CUSTOM_METHODS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GRD_CUSTOM_METHODS.Location = new System.Drawing.Point(16, 415);
            this.GRD_CUSTOM_METHODS.Name = "GRD_CUSTOM_METHODS";
            this.GRD_CUSTOM_METHODS.Size = new System.Drawing.Size(813, 179);
            this.GRD_CUSTOM_METHODS.TabIndex = 54;
            // 
            // btnAddCustomMethod
            // 
            this.btnAddCustomMethod.Location = new System.Drawing.Point(17, 369);
            this.btnAddCustomMethod.Name = "btnAddCustomMethod";
            this.btnAddCustomMethod.Size = new System.Drawing.Size(810, 23);
            this.btnAddCustomMethod.TabIndex = 53;
            this.btnAddCustomMethod.Text = "ADD METHOD";
            this.btnAddCustomMethod.UseVisualStyleBackColor = true;
            this.btnAddCustomMethod.Click += new System.EventHandler(this.btnAddCustomMethod_Click);
            // 
            // lvCOLUMNS2
            // 
            this.lvCOLUMNS2.Location = new System.Drawing.Point(425, 124);
            this.lvCOLUMNS2.Name = "lvCOLUMNS2";
            this.lvCOLUMNS2.Size = new System.Drawing.Size(402, 239);
            this.lvCOLUMNS2.TabIndex = 52;
            this.lvCOLUMNS2.UseCompatibleStateImageBehavior = false;
            // 
            // lvCOLUMNS1
            // 
            this.lvCOLUMNS1.Location = new System.Drawing.Point(16, 124);
            this.lvCOLUMNS1.Name = "lvCOLUMNS1";
            this.lvCOLUMNS1.Size = new System.Drawing.Size(403, 239);
            this.lvCOLUMNS1.TabIndex = 51;
            this.lvCOLUMNS1.UseCompatibleStateImageBehavior = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 98);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(408, 33);
            this.button3.TabIndex = 57;
            this.button3.Text = "CREATE C# - DEFAULT METHODS ONLY";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(866, 665);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(404, 38);
            this.btnClose.TabIndex = 59;
            this.btnClose.Text = "CLOSE SCREEN";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tv);
            this.groupBox3.Controls.Add(this.cboViews);
            this.groupBox3.Controls.Add(this.cboTables);
            this.groupBox3.Controls.Add(this.lblOBJECT_ID);
            this.groupBox3.Controls.Add(this.lblTABLE_SCHEMA);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.chkPK);
            this.groupBox3.Controls.Add(this.cboServers);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.cboDatabases);
            this.groupBox3.Location = new System.Drawing.Point(6, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(425, 512);
            this.groupBox3.TabIndex = 60;
            this.groupBox3.TabStop = false;
            // 
            // btnCreateCS_ALL
            // 
            this.btnCreateCS_ALL.Location = new System.Drawing.Point(6, 668);
            this.btnCreateCS_ALL.Name = "btnCreateCS_ALL";
            this.btnCreateCS_ALL.Size = new System.Drawing.Size(854, 38);
            this.btnCreateCS_ALL.TabIndex = 61;
            this.btnCreateCS_ALL.Text = "CREATE C# - ALL OBJECTS AND METHODS";
            this.btnCreateCS_ALL.UseVisualStyleBackColor = true;
            this.btnCreateCS_ALL.Click += new System.EventHandler(this.btnCreateCS_ALL_Click);
            // 
            // btnCreateCS_CUSTOM_METHODS
            // 
            this.btnCreateCS_CUSTOM_METHODS.Location = new System.Drawing.Point(411, 601);
            this.btnCreateCS_CUSTOM_METHODS.Name = "btnCreateCS_CUSTOM_METHODS";
            this.btnCreateCS_CUSTOM_METHODS.Size = new System.Drawing.Size(418, 38);
            this.btnCreateCS_CUSTOM_METHODS.TabIndex = 62;
            this.btnCreateCS_CUSTOM_METHODS.Text = "CREATE C# - CUSTOM METHODS ONLY";
            this.btnCreateCS_CUSTOM_METHODS.UseVisualStyleBackColor = true;
            this.btnCreateCS_CUSTOM_METHODS.Click += new System.EventHandler(this.btnCreateCS_CUSTOM_METHODS_Click);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1288, 718);
            this.Controls.Add(this.btnCreateCS_ALL);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpOPTIONS);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form4";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CREATE C# - Microsoft SQL Server";
            this.Load += new System.EventHandler(this.Form4_Load);
            this.grpOPTIONS.ResumeLayout(false);
            this.grpOPTIONS.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GRD_CUSTOM_METHODS)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpOPTIONS;
        private System.Windows.Forms.CheckBox chkSEL_ALL;
        private System.Windows.Forms.CheckBox chkDEL_PK;
        private System.Windows.Forms.CheckBox chkSEL_100;
        private System.Windows.Forms.CheckBox chkINS;
        private System.Windows.Forms.CheckBox chkSEL_PK;
        private System.Windows.Forms.CheckBox chkDEL_ALL;
        private System.Windows.Forms.CheckBox chkUPD_PK;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkPK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTABLE_SCHEMA;
        private System.Windows.Forms.Label lblOBJECT_ID;
        private System.Windows.Forms.TreeView tv;
        private System.Windows.Forms.ComboBox cboViews;
        private System.Windows.Forms.ComboBox cboServers;
        private System.Windows.Forms.ComboBox cboTables;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboDatabases;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton optVIEWS;
        private System.Windows.Forms.RadioButton optTABLES;
        private System.Windows.Forms.ComboBox cboCustomMethods;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lvCOLUMNS2;
        private System.Windows.Forms.ListView lvCOLUMNS1;
        private System.Windows.Forms.Button btnRemoveAllMethods;
        private System.Windows.Forms.Button btnRemoveSelectedMethod;
        private System.Windows.Forms.DataGridView GRD_CUSTOM_METHODS;
        private System.Windows.Forms.Button btnAddCustomMethod;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblCOLUMNS1;
        private System.Windows.Forms.Label lblCOLUMNS2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCUSTOM_METHOD_NAME;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnCreateCS_ALL;
        private System.Windows.Forms.Button btnCreateCS_CUSTOM_METHODS;
    }
}