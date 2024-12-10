namespace FREDDIE
{
    partial class frmPREFERANCES
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPREFERANCES));
            this.btnChangePathIUD = new System.Windows.Forms.Button();
            this.btnChangePathREAD = new System.Windows.Forms.Button();
            this.btnChangePathMODEL = new System.Windows.Forms.Button();
            this.txtCS_IUD_PATH = new System.Windows.Forms.TextBox();
            this.txtCS_READ_PATH = new System.Windows.Forms.TextBox();
            this.txtCS_MODEL_PATH = new System.Windows.Forms.TextBox();
            this.chkIUD_FOLDER = new System.Windows.Forms.CheckBox();
            this.chkMODEL_FOLDER = new System.Windows.Forms.CheckBox();
            this.chkREAD_FOLDER = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkIUD_DATABASE_FOLDER = new System.Windows.Forms.CheckBox();
            this.chkREAD_DATABASE_FOLDER = new System.Windows.Forms.CheckBox();
            this.chkMODEL_DATABASE_FOLDER = new System.Windows.Forms.CheckBox();
            this.chkIUD_SERVER_FOLDER = new System.Windows.Forms.CheckBox();
            this.chkREAD_SERVER_FOLDER = new System.Windows.Forms.CheckBox();
            this.chkMODEL_SERVER_FOLDER = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lstMyServers = new System.Windows.Forms.ListBox();
            this.btnAddServer = new System.Windows.Forms.Button();
            this.txtSERVER_NAME = new System.Windows.Forms.TextBox();
            this.btnSaveMyDatabases = new System.Windows.Forms.Button();
            this.lstMyDatabases = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.chkArchive = new System.Windows.Forms.CheckBox();
            this.btnUpdateServer = new System.Windows.Forms.Button();
            this.btnDeleteServer = new System.Windows.Forms.Button();
            this.txtSERVER_ID = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.grpMYSERVERS = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.grpMYSERVERS.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnChangePathIUD
            // 
            this.btnChangePathIUD.Location = new System.Drawing.Point(513, 95);
            this.btnChangePathIUD.Name = "btnChangePathIUD";
            this.btnChangePathIUD.Size = new System.Drawing.Size(96, 23);
            this.btnChangePathIUD.TabIndex = 110;
            this.btnChangePathIUD.Text = "CHANGE";
            this.btnChangePathIUD.UseVisualStyleBackColor = true;
            // 
            // btnChangePathREAD
            // 
            this.btnChangePathREAD.Location = new System.Drawing.Point(513, 63);
            this.btnChangePathREAD.Name = "btnChangePathREAD";
            this.btnChangePathREAD.Size = new System.Drawing.Size(96, 23);
            this.btnChangePathREAD.TabIndex = 109;
            this.btnChangePathREAD.Text = "CHANGE";
            this.btnChangePathREAD.UseVisualStyleBackColor = true;
            // 
            // btnChangePathMODEL
            // 
            this.btnChangePathMODEL.Location = new System.Drawing.Point(513, 31);
            this.btnChangePathMODEL.Name = "btnChangePathMODEL";
            this.btnChangePathMODEL.Size = new System.Drawing.Size(96, 23);
            this.btnChangePathMODEL.TabIndex = 108;
            this.btnChangePathMODEL.Text = "CHANGE";
            this.btnChangePathMODEL.UseVisualStyleBackColor = true;
            // 
            // txtCS_IUD_PATH
            // 
            this.txtCS_IUD_PATH.Location = new System.Drawing.Point(92, 98);
            this.txtCS_IUD_PATH.Name = "txtCS_IUD_PATH";
            this.txtCS_IUD_PATH.ReadOnly = true;
            this.txtCS_IUD_PATH.Size = new System.Drawing.Size(415, 20);
            this.txtCS_IUD_PATH.TabIndex = 107;
            this.txtCS_IUD_PATH.Text = "NA";
            // 
            // txtCS_READ_PATH
            // 
            this.txtCS_READ_PATH.Location = new System.Drawing.Point(92, 66);
            this.txtCS_READ_PATH.Name = "txtCS_READ_PATH";
            this.txtCS_READ_PATH.ReadOnly = true;
            this.txtCS_READ_PATH.Size = new System.Drawing.Size(415, 20);
            this.txtCS_READ_PATH.TabIndex = 106;
            this.txtCS_READ_PATH.Text = "NA";
            // 
            // txtCS_MODEL_PATH
            // 
            this.txtCS_MODEL_PATH.Location = new System.Drawing.Point(92, 34);
            this.txtCS_MODEL_PATH.Name = "txtCS_MODEL_PATH";
            this.txtCS_MODEL_PATH.ReadOnly = true;
            this.txtCS_MODEL_PATH.Size = new System.Drawing.Size(415, 20);
            this.txtCS_MODEL_PATH.TabIndex = 105;
            this.txtCS_MODEL_PATH.Text = "NA";
            // 
            // chkIUD_FOLDER
            // 
            this.chkIUD_FOLDER.AutoSize = true;
            this.chkIUD_FOLDER.Checked = true;
            this.chkIUD_FOLDER.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIUD_FOLDER.Location = new System.Drawing.Point(758, 99);
            this.chkIUD_FOLDER.Name = "chkIUD_FOLDER";
            this.chkIUD_FOLDER.Size = new System.Drawing.Size(55, 17);
            this.chkIUD_FOLDER.TabIndex = 102;
            this.chkIUD_FOLDER.Text = "/IUD/";
            this.chkIUD_FOLDER.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIUD_FOLDER.UseVisualStyleBackColor = true;
            // 
            // chkMODEL_FOLDER
            // 
            this.chkMODEL_FOLDER.AutoSize = true;
            this.chkMODEL_FOLDER.Checked = true;
            this.chkMODEL_FOLDER.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMODEL_FOLDER.Location = new System.Drawing.Point(758, 33);
            this.chkMODEL_FOLDER.Name = "chkMODEL_FOLDER";
            this.chkMODEL_FOLDER.Size = new System.Drawing.Size(77, 17);
            this.chkMODEL_FOLDER.TabIndex = 104;
            this.chkMODEL_FOLDER.Text = "/MODEL /";
            this.chkMODEL_FOLDER.UseVisualStyleBackColor = true;
            // 
            // chkREAD_FOLDER
            // 
            this.chkREAD_FOLDER.AutoSize = true;
            this.chkREAD_FOLDER.Checked = true;
            this.chkREAD_FOLDER.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkREAD_FOLDER.Location = new System.Drawing.Point(758, 66);
            this.chkREAD_FOLDER.Name = "chkREAD_FOLDER";
            this.chkREAD_FOLDER.Size = new System.Drawing.Size(66, 17);
            this.chkREAD_FOLDER.TabIndex = 103;
            this.chkREAD_FOLDER.Text = "/READ/";
            this.chkREAD_FOLDER.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkREAD_FOLDER.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkIUD_DATABASE_FOLDER);
            this.groupBox1.Controls.Add(this.chkREAD_DATABASE_FOLDER);
            this.groupBox1.Controls.Add(this.chkMODEL_DATABASE_FOLDER);
            this.groupBox1.Controls.Add(this.chkIUD_SERVER_FOLDER);
            this.groupBox1.Controls.Add(this.chkREAD_SERVER_FOLDER);
            this.groupBox1.Controls.Add(this.chkMODEL_SERVER_FOLDER);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtCS_IUD_PATH);
            this.groupBox1.Controls.Add(this.btnChangePathIUD);
            this.groupBox1.Controls.Add(this.chkREAD_FOLDER);
            this.groupBox1.Controls.Add(this.btnChangePathREAD);
            this.groupBox1.Controls.Add(this.chkMODEL_FOLDER);
            this.groupBox1.Controls.Add(this.btnChangePathMODEL);
            this.groupBox1.Controls.Add(this.chkIUD_FOLDER);
            this.groupBox1.Controls.Add(this.txtCS_MODEL_PATH);
            this.groupBox1.Controls.Add(this.txtCS_READ_PATH);
            this.groupBox1.Location = new System.Drawing.Point(12, 507);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(943, 134);
            this.groupBox1.TabIndex = 111;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FILE PATHS FOR C# OBJECTS";
            // 
            // chkIUD_DATABASE_FOLDER
            // 
            this.chkIUD_DATABASE_FOLDER.AutoSize = true;
            this.chkIUD_DATABASE_FOLDER.Checked = true;
            this.chkIUD_DATABASE_FOLDER.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIUD_DATABASE_FOLDER.Location = new System.Drawing.Point(701, 101);
            this.chkIUD_DATABASE_FOLDER.Name = "chkIUD_DATABASE_FOLDER";
            this.chkIUD_DATABASE_FOLDER.Size = new System.Drawing.Size(51, 17);
            this.chkIUD_DATABASE_FOLDER.TabIndex = 129;
            this.chkIUD_DATABASE_FOLDER.Text = "/DB/";
            this.chkIUD_DATABASE_FOLDER.UseVisualStyleBackColor = true;
            // 
            // chkREAD_DATABASE_FOLDER
            // 
            this.chkREAD_DATABASE_FOLDER.AutoSize = true;
            this.chkREAD_DATABASE_FOLDER.Checked = true;
            this.chkREAD_DATABASE_FOLDER.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkREAD_DATABASE_FOLDER.Location = new System.Drawing.Point(701, 66);
            this.chkREAD_DATABASE_FOLDER.Name = "chkREAD_DATABASE_FOLDER";
            this.chkREAD_DATABASE_FOLDER.Size = new System.Drawing.Size(51, 17);
            this.chkREAD_DATABASE_FOLDER.TabIndex = 128;
            this.chkREAD_DATABASE_FOLDER.Text = "/DB/";
            this.chkREAD_DATABASE_FOLDER.UseVisualStyleBackColor = true;
            // 
            // chkMODEL_DATABASE_FOLDER
            // 
            this.chkMODEL_DATABASE_FOLDER.AutoSize = true;
            this.chkMODEL_DATABASE_FOLDER.Checked = true;
            this.chkMODEL_DATABASE_FOLDER.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMODEL_DATABASE_FOLDER.Location = new System.Drawing.Point(701, 33);
            this.chkMODEL_DATABASE_FOLDER.Name = "chkMODEL_DATABASE_FOLDER";
            this.chkMODEL_DATABASE_FOLDER.Size = new System.Drawing.Size(51, 17);
            this.chkMODEL_DATABASE_FOLDER.TabIndex = 127;
            this.chkMODEL_DATABASE_FOLDER.Text = "/DB/";
            this.chkMODEL_DATABASE_FOLDER.UseVisualStyleBackColor = true;
            // 
            // chkIUD_SERVER_FOLDER
            // 
            this.chkIUD_SERVER_FOLDER.AutoSize = true;
            this.chkIUD_SERVER_FOLDER.Checked = true;
            this.chkIUD_SERVER_FOLDER.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIUD_SERVER_FOLDER.Location = new System.Drawing.Point(615, 101);
            this.chkIUD_SERVER_FOLDER.Name = "chkIUD_SERVER_FOLDER";
            this.chkIUD_SERVER_FOLDER.Size = new System.Drawing.Size(80, 17);
            this.chkIUD_SERVER_FOLDER.TabIndex = 126;
            this.chkIUD_SERVER_FOLDER.Text = "/SERVER/";
            this.chkIUD_SERVER_FOLDER.UseVisualStyleBackColor = true;
            // 
            // chkREAD_SERVER_FOLDER
            // 
            this.chkREAD_SERVER_FOLDER.AutoSize = true;
            this.chkREAD_SERVER_FOLDER.Checked = true;
            this.chkREAD_SERVER_FOLDER.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkREAD_SERVER_FOLDER.Location = new System.Drawing.Point(615, 66);
            this.chkREAD_SERVER_FOLDER.Name = "chkREAD_SERVER_FOLDER";
            this.chkREAD_SERVER_FOLDER.Size = new System.Drawing.Size(80, 17);
            this.chkREAD_SERVER_FOLDER.TabIndex = 125;
            this.chkREAD_SERVER_FOLDER.Text = "/SERVER/";
            this.chkREAD_SERVER_FOLDER.UseVisualStyleBackColor = true;
            // 
            // chkMODEL_SERVER_FOLDER
            // 
            this.chkMODEL_SERVER_FOLDER.AutoSize = true;
            this.chkMODEL_SERVER_FOLDER.Checked = true;
            this.chkMODEL_SERVER_FOLDER.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMODEL_SERVER_FOLDER.Location = new System.Drawing.Point(615, 33);
            this.chkMODEL_SERVER_FOLDER.Name = "chkMODEL_SERVER_FOLDER";
            this.chkMODEL_SERVER_FOLDER.Size = new System.Drawing.Size(80, 17);
            this.chkMODEL_SERVER_FOLDER.TabIndex = 124;
            this.chkMODEL_SERVER_FOLDER.Text = "/SERVER/";
            this.chkMODEL_SERVER_FOLDER.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 123;
            this.label5.Text = "IUD PATH:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 122;
            this.label4.Text = "READ PATH:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 121;
            this.label3.Text = "MODEL PATH:";
            // 
            // lstMyServers
            // 
            this.lstMyServers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lstMyServers.FormattingEnabled = true;
            this.lstMyServers.Location = new System.Drawing.Point(6, 19);
            this.lstMyServers.Name = "lstMyServers";
            this.lstMyServers.Size = new System.Drawing.Size(366, 316);
            this.lstMyServers.TabIndex = 112;
            this.lstMyServers.SelectedIndexChanged += new System.EventHandler(this.lstMyServers_SelectedIndexChanged);
            // 
            // btnAddServer
            // 
            this.btnAddServer.Location = new System.Drawing.Point(6, 348);
            this.btnAddServer.Name = "btnAddServer";
            this.btnAddServer.Size = new System.Drawing.Size(135, 37);
            this.btnAddServer.TabIndex = 114;
            this.btnAddServer.Text = "ADD SERVER TO LIST";
            this.btnAddServer.UseVisualStyleBackColor = true;
            this.btnAddServer.Click += new System.EventHandler(this.btnAddServer_Click);
            // 
            // txtSERVER_NAME
            // 
            this.txtSERVER_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.txtSERVER_NAME.Location = new System.Drawing.Point(81, 454);
            this.txtSERVER_NAME.Name = "txtSERVER_NAME";
            this.txtSERVER_NAME.Size = new System.Drawing.Size(291, 20);
            this.txtSERVER_NAME.TabIndex = 115;
            // 
            // btnSaveMyDatabases
            // 
            this.btnSaveMyDatabases.Location = new System.Drawing.Point(6, 449);
            this.btnSaveMyDatabases.Name = "btnSaveMyDatabases";
            this.btnSaveMyDatabases.Size = new System.Drawing.Size(545, 37);
            this.btnSaveMyDatabases.TabIndex = 116;
            this.btnSaveMyDatabases.Text = "SAVE\\UPDATE MY DATABASES LIST";
            this.btnSaveMyDatabases.UseVisualStyleBackColor = true;
            this.btnSaveMyDatabases.Click += new System.EventHandler(this.btnSaveMyDatabases_Click);
            // 
            // lstMyDatabases
            // 
            this.lstMyDatabases.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lstMyDatabases.CheckOnClick = true;
            this.lstMyDatabases.FormattingEnabled = true;
            this.lstMyDatabases.Location = new System.Drawing.Point(6, 19);
            this.lstMyDatabases.Name = "lstMyDatabases";
            this.lstMyDatabases.Size = new System.Drawing.Size(545, 424);
            this.lstMyDatabases.TabIndex = 117;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(81, 438);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 119;
            this.label2.Text = "SERVER NAME:";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(11, 683);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(944, 62);
            this.btnClose.TabIndex = 120;
            this.btnClose.Text = "CLOSE THIS SCREEN";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // chkArchive
            // 
            this.chkArchive.AutoSize = true;
            this.chkArchive.Location = new System.Drawing.Point(11, 647);
            this.chkArchive.Name = "chkArchive";
            this.chkArchive.Size = new System.Drawing.Size(151, 17);
            this.chkArchive.TabIndex = 130;
            this.chkArchive.Text = "ARCHIVE FILE IF EXISTS";
            this.chkArchive.UseVisualStyleBackColor = true;
            // 
            // btnUpdateServer
            // 
            this.btnUpdateServer.Location = new System.Drawing.Point(147, 348);
            this.btnUpdateServer.Name = "btnUpdateServer";
            this.btnUpdateServer.Size = new System.Drawing.Size(107, 37);
            this.btnUpdateServer.TabIndex = 131;
            this.btnUpdateServer.Text = "UPDATE SERVER";
            this.btnUpdateServer.UseVisualStyleBackColor = true;
            this.btnUpdateServer.Click += new System.EventHandler(this.btnUpdateServer_Click);
            // 
            // btnDeleteServer
            // 
            this.btnDeleteServer.Location = new System.Drawing.Point(260, 348);
            this.btnDeleteServer.Name = "btnDeleteServer";
            this.btnDeleteServer.Size = new System.Drawing.Size(114, 37);
            this.btnDeleteServer.TabIndex = 132;
            this.btnDeleteServer.Text = "DELETE SERVER";
            this.btnDeleteServer.UseVisualStyleBackColor = true;
            this.btnDeleteServer.Click += new System.EventHandler(this.btnDeleteServer_Click);
            // 
            // txtSERVER_ID
            // 
            this.txtSERVER_ID.Location = new System.Drawing.Point(10, 454);
            this.txtSERVER_ID.Name = "txtSERVER_ID";
            this.txtSERVER_ID.ReadOnly = true;
            this.txtSERVER_ID.Size = new System.Drawing.Size(65, 20);
            this.txtSERVER_ID.TabIndex = 133;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 438);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 134;
            this.label6.Text = "SERVER ID:";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(6, 391);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(368, 37);
            this.btnClear.TabIndex = 135;
            this.btnClear.Text = "CLEAR";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // grpMYSERVERS
            // 
            this.grpMYSERVERS.Controls.Add(this.lstMyServers);
            this.grpMYSERVERS.Controls.Add(this.btnClear);
            this.grpMYSERVERS.Controls.Add(this.btnAddServer);
            this.grpMYSERVERS.Controls.Add(this.label6);
            this.grpMYSERVERS.Controls.Add(this.txtSERVER_NAME);
            this.grpMYSERVERS.Controls.Add(this.txtSERVER_ID);
            this.grpMYSERVERS.Controls.Add(this.btnDeleteServer);
            this.grpMYSERVERS.Controls.Add(this.label2);
            this.grpMYSERVERS.Controls.Add(this.btnUpdateServer);
            this.grpMYSERVERS.Location = new System.Drawing.Point(11, 12);
            this.grpMYSERVERS.Name = "grpMYSERVERS";
            this.grpMYSERVERS.Size = new System.Drawing.Size(381, 489);
            this.grpMYSERVERS.TabIndex = 136;
            this.grpMYSERVERS.TabStop = false;
            this.grpMYSERVERS.Text = "MY SERVERS:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstMyDatabases);
            this.groupBox2.Controls.Add(this.btnSaveMyDatabases);
            this.groupBox2.Location = new System.Drawing.Point(398, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(557, 489);
            this.groupBox2.TabIndex = 137;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "MY DATABASES:";
            // 
            // frmPREFERANCES
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 757);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grpMYSERVERS);
            this.Controls.Add(this.chkArchive);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmPREFERANCES";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MSSQL SERVER TO C# - PREFERENCES";
            this.Load += new System.EventHandler(this.frmPREFERANCES_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpMYSERVERS.ResumeLayout(false);
            this.grpMYSERVERS.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnChangePathIUD;
        private System.Windows.Forms.Button btnChangePathREAD;
        private System.Windows.Forms.Button btnChangePathMODEL;
        private System.Windows.Forms.TextBox txtCS_IUD_PATH;
        private System.Windows.Forms.TextBox txtCS_READ_PATH;
        private System.Windows.Forms.TextBox txtCS_MODEL_PATH;
        private System.Windows.Forms.CheckBox chkIUD_FOLDER;
        private System.Windows.Forms.CheckBox chkMODEL_FOLDER;
        private System.Windows.Forms.CheckBox chkREAD_FOLDER;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstMyServers;
        private System.Windows.Forms.Button btnAddServer;
        private System.Windows.Forms.TextBox txtSERVER_NAME;
        private System.Windows.Forms.Button btnSaveMyDatabases;
        private System.Windows.Forms.CheckedListBox lstMyDatabases;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox chkMODEL_SERVER_FOLDER;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkIUD_DATABASE_FOLDER;
        private System.Windows.Forms.CheckBox chkREAD_DATABASE_FOLDER;
        private System.Windows.Forms.CheckBox chkMODEL_DATABASE_FOLDER;
        private System.Windows.Forms.CheckBox chkIUD_SERVER_FOLDER;
        private System.Windows.Forms.CheckBox chkREAD_SERVER_FOLDER;
        private System.Windows.Forms.CheckBox chkArchive;
        private System.Windows.Forms.Button btnUpdateServer;
        private System.Windows.Forms.Button btnDeleteServer;
        private System.Windows.Forms.TextBox txtSERVER_ID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.GroupBox grpMYSERVERS;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}