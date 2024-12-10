namespace FREDDIE
{
    partial class Form5
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
            this.btnClose = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnIUDPathDefault = new System.Windows.Forms.Button();
            this.btnIUDPathChange = new System.Windows.Forms.Button();
            this.txtIUDPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnREADLPathDefault = new System.Windows.Forms.Button();
            this.btnREADPathChange = new System.Windows.Forms.Button();
            this.txtREADPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnMODELPathDefault = new System.Windows.Forms.Button();
            this.btnMODELPathChange = new System.Windows.Forms.Button();
            this.txtMODELPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtSERVER_ID = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnRemoveServer = new System.Windows.Forms.Button();
            this.btnAddServer = new System.Windows.Forms.Button();
            this.txtSERVER_NAME = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lstServers = new System.Windows.Forms.ListBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btnRemoveFromMyDatabases = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.lstMyDatabases = new System.Windows.Forms.CheckedListBox();
            this.btnAddToMyDatabases = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.lstDatabases = new System.Windows.Forms.CheckedListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboServers = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSQLPathDefault = new System.Windows.Forms.Button();
            this.btnSQLPathChange = new System.Windows.Forms.Button();
            this.txtSQLPath = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(12, 591);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(1014, 74);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "CLOSE";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1018, 573);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1010, 547);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "C#";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnIUDPathDefault);
            this.groupBox1.Controls.Add(this.btnIUDPathChange);
            this.groupBox1.Controls.Add(this.txtIUDPath);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnREADLPathDefault);
            this.groupBox1.Controls.Add(this.btnREADPathChange);
            this.groupBox1.Controls.Add(this.txtREADPath);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnMODELPathDefault);
            this.groupBox1.Controls.Add(this.btnMODELPathChange);
            this.groupBox1.Controls.Add(this.txtMODELPath);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(998, 166);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PATHS TO C# OBJECTS";
            // 
            // btnIUDPathDefault
            // 
            this.btnIUDPathDefault.Location = new System.Drawing.Point(678, 117);
            this.btnIUDPathDefault.Name = "btnIUDPathDefault";
            this.btnIUDPathDefault.Size = new System.Drawing.Size(75, 23);
            this.btnIUDPathDefault.TabIndex = 15;
            this.btnIUDPathDefault.Text = "DEFAULT";
            this.btnIUDPathDefault.UseVisualStyleBackColor = true;
            this.btnIUDPathDefault.Click += new System.EventHandler(this.btnIUDPathDefault_Click);
            // 
            // btnIUDPathChange
            // 
            this.btnIUDPathChange.Location = new System.Drawing.Point(597, 117);
            this.btnIUDPathChange.Name = "btnIUDPathChange";
            this.btnIUDPathChange.Size = new System.Drawing.Size(75, 23);
            this.btnIUDPathChange.TabIndex = 14;
            this.btnIUDPathChange.Text = "CHANGE";
            this.btnIUDPathChange.UseVisualStyleBackColor = true;
            this.btnIUDPathChange.Click += new System.EventHandler(this.btnIUDPathChange_Click);
            // 
            // txtIUDPath
            // 
            this.txtIUDPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtIUDPath.Location = new System.Drawing.Point(16, 120);
            this.txtIUDPath.Name = "txtIUDPath";
            this.txtIUDPath.ReadOnly = true;
            this.txtIUDPath.Size = new System.Drawing.Size(575, 20);
            this.txtIUDPath.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "IUD";
            // 
            // btnREADLPathDefault
            // 
            this.btnREADLPathDefault.Location = new System.Drawing.Point(678, 78);
            this.btnREADLPathDefault.Name = "btnREADLPathDefault";
            this.btnREADLPathDefault.Size = new System.Drawing.Size(75, 23);
            this.btnREADLPathDefault.TabIndex = 11;
            this.btnREADLPathDefault.Text = "DEFAULT";
            this.btnREADLPathDefault.UseVisualStyleBackColor = true;
            this.btnREADLPathDefault.Click += new System.EventHandler(this.btnREADLPathDefault_Click);
            // 
            // btnREADPathChange
            // 
            this.btnREADPathChange.Location = new System.Drawing.Point(597, 78);
            this.btnREADPathChange.Name = "btnREADPathChange";
            this.btnREADPathChange.Size = new System.Drawing.Size(75, 23);
            this.btnREADPathChange.TabIndex = 10;
            this.btnREADPathChange.Text = "CHANGE";
            this.btnREADPathChange.UseVisualStyleBackColor = true;
            this.btnREADPathChange.Click += new System.EventHandler(this.btnREADPathChange_Click);
            // 
            // txtREADPath
            // 
            this.txtREADPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtREADPath.Location = new System.Drawing.Point(16, 81);
            this.txtREADPath.Name = "txtREADPath";
            this.txtREADPath.ReadOnly = true;
            this.txtREADPath.Size = new System.Drawing.Size(575, 20);
            this.txtREADPath.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "READ";
            // 
            // btnMODELPathDefault
            // 
            this.btnMODELPathDefault.Location = new System.Drawing.Point(678, 39);
            this.btnMODELPathDefault.Name = "btnMODELPathDefault";
            this.btnMODELPathDefault.Size = new System.Drawing.Size(75, 23);
            this.btnMODELPathDefault.TabIndex = 7;
            this.btnMODELPathDefault.Text = "DEFAULT";
            this.btnMODELPathDefault.UseVisualStyleBackColor = true;
            this.btnMODELPathDefault.Click += new System.EventHandler(this.btnMODELPathDefault_Click);
            // 
            // btnMODELPathChange
            // 
            this.btnMODELPathChange.Location = new System.Drawing.Point(597, 39);
            this.btnMODELPathChange.Name = "btnMODELPathChange";
            this.btnMODELPathChange.Size = new System.Drawing.Size(75, 23);
            this.btnMODELPathChange.TabIndex = 6;
            this.btnMODELPathChange.Text = "CHANGE";
            this.btnMODELPathChange.UseVisualStyleBackColor = true;
            this.btnMODELPathChange.Click += new System.EventHandler(this.btnMODELPathChange_Click);
            // 
            // txtMODELPath
            // 
            this.txtMODELPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtMODELPath.Location = new System.Drawing.Point(16, 42);
            this.txtMODELPath.Name = "txtMODELPath";
            this.txtMODELPath.ReadOnly = true;
            this.txtMODELPath.Size = new System.Drawing.Size(575, 20);
            this.txtMODELPath.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "MODEL";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1010, 547);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "SQL";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Controls.Add(this.lstServers);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1010, 547);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "AVAILABLE SERVERS";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClear);
            this.groupBox2.Controls.Add(this.txtSERVER_ID);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.btnRemoveServer);
            this.groupBox2.Controls.Add(this.btnAddServer);
            this.groupBox2.Controls.Add(this.txtSERVER_NAME);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(359, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(645, 196);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(230, 86);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(130, 27);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "CLEAR";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtSERVER_ID
            // 
            this.txtSERVER_ID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtSERVER_ID.Location = new System.Drawing.Point(94, 30);
            this.txtSERVER_ID.Name = "txtSERVER_ID";
            this.txtSERVER_ID.ReadOnly = true;
            this.txtSERVER_ID.Size = new System.Drawing.Size(64, 20);
            this.txtSERVER_ID.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Server ID:";
            // 
            // btnRemoveServer
            // 
            this.btnRemoveServer.Location = new System.Drawing.Point(471, 86);
            this.btnRemoveServer.Name = "btnRemoveServer";
            this.btnRemoveServer.Size = new System.Drawing.Size(168, 27);
            this.btnRemoveServer.TabIndex = 4;
            this.btnRemoveServer.Text = "REMOVE SERVER";
            this.btnRemoveServer.UseVisualStyleBackColor = true;
            this.btnRemoveServer.Click += new System.EventHandler(this.btnRemoveServer_Click);
            // 
            // btnAddServer
            // 
            this.btnAddServer.Location = new System.Drawing.Point(94, 86);
            this.btnAddServer.Name = "btnAddServer";
            this.btnAddServer.Size = new System.Drawing.Size(130, 27);
            this.btnAddServer.TabIndex = 3;
            this.btnAddServer.Text = "ADD SERVER";
            this.btnAddServer.UseVisualStyleBackColor = true;
            this.btnAddServer.Click += new System.EventHandler(this.btnAddServer_Click);
            // 
            // txtSERVER_NAME
            // 
            this.txtSERVER_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtSERVER_NAME.Location = new System.Drawing.Point(94, 60);
            this.txtSERVER_NAME.Name = "txtSERVER_NAME";
            this.txtSERVER_NAME.Size = new System.Drawing.Size(545, 20);
            this.txtSERVER_NAME.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Server Name:";
            // 
            // lstServers
            // 
            this.lstServers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lstServers.FormattingEnabled = true;
            this.lstServers.Location = new System.Drawing.Point(6, 6);
            this.lstServers.Name = "lstServers";
            this.lstServers.Size = new System.Drawing.Size(347, 199);
            this.lstServers.TabIndex = 0;
            this.lstServers.SelectedIndexChanged += new System.EventHandler(this.lstServers_SelectedIndexChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btnRemoveFromMyDatabases);
            this.tabPage4.Controls.Add(this.label7);
            this.tabPage4.Controls.Add(this.lstMyDatabases);
            this.tabPage4.Controls.Add(this.btnAddToMyDatabases);
            this.tabPage4.Controls.Add(this.label6);
            this.tabPage4.Controls.Add(this.lstDatabases);
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Controls.Add(this.cboServers);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1010, 547);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "My Databases";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnRemoveFromMyDatabases
            // 
            this.btnRemoveFromMyDatabases.Location = new System.Drawing.Point(545, 509);
            this.btnRemoveFromMyDatabases.Name = "btnRemoveFromMyDatabases";
            this.btnRemoveFromMyDatabases.Size = new System.Drawing.Size(459, 32);
            this.btnRemoveFromMyDatabases.TabIndex = 7;
            this.btnRemoveFromMyDatabases.Text = "REMOVE SELECTED DATABASES";
            this.btnRemoveFromMyDatabases.UseVisualStyleBackColor = true;
            this.btnRemoveFromMyDatabases.Click += new System.EventHandler(this.btnRemoveFromMyDatabases_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(542, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "MY DATABASES:";
            // 
            // lstMyDatabases
            // 
            this.lstMyDatabases.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lstMyDatabases.CheckOnClick = true;
            this.lstMyDatabases.FormattingEnabled = true;
            this.lstMyDatabases.Location = new System.Drawing.Point(545, 94);
            this.lstMyDatabases.Name = "lstMyDatabases";
            this.lstMyDatabases.Size = new System.Drawing.Size(459, 409);
            this.lstMyDatabases.TabIndex = 5;
            // 
            // btnAddToMyDatabases
            // 
            this.btnAddToMyDatabases.Location = new System.Drawing.Point(9, 509);
            this.btnAddToMyDatabases.Name = "btnAddToMyDatabases";
            this.btnAddToMyDatabases.Size = new System.Drawing.Size(459, 32);
            this.btnAddToMyDatabases.TabIndex = 4;
            this.btnAddToMyDatabases.Text = "ADD SELECTED DATABASES TO MY DATABASES >>>";
            this.btnAddToMyDatabases.UseVisualStyleBackColor = true;
            this.btnAddToMyDatabases.Click += new System.EventHandler(this.btnAddToMyDatabases_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "AVAILABLE DATABASES:";
            // 
            // lstDatabases
            // 
            this.lstDatabases.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lstDatabases.CheckOnClick = true;
            this.lstDatabases.FormattingEnabled = true;
            this.lstDatabases.Location = new System.Drawing.Point(9, 94);
            this.lstDatabases.Name = "lstDatabases";
            this.lstDatabases.Size = new System.Drawing.Size(459, 409);
            this.lstDatabases.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "AVAILABLE SERVERS:";
            // 
            // cboServers
            // 
            this.cboServers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cboServers.FormattingEnabled = true;
            this.cboServers.Location = new System.Drawing.Point(6, 54);
            this.cboServers.Name = "cboServers";
            this.cboServers.Size = new System.Drawing.Size(462, 21);
            this.cboServers.TabIndex = 0;
            this.cboServers.SelectedIndexChanged += new System.EventHandler(this.cboServers_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnSQLPathDefault);
            this.groupBox3.Controls.Add(this.btnSQLPathChange);
            this.groupBox3.Controls.Add(this.txtSQLPath);
            this.groupBox3.Location = new System.Drawing.Point(9, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(998, 166);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "PATHS TO SQL OBJECTS";
            // 
            // btnSQLPathDefault
            // 
            this.btnSQLPathDefault.Location = new System.Drawing.Point(678, 39);
            this.btnSQLPathDefault.Name = "btnSQLPathDefault";
            this.btnSQLPathDefault.Size = new System.Drawing.Size(75, 23);
            this.btnSQLPathDefault.TabIndex = 7;
            this.btnSQLPathDefault.Text = "DEFAULT";
            this.btnSQLPathDefault.UseVisualStyleBackColor = true;
            this.btnSQLPathDefault.Click += new System.EventHandler(this.btnSQLPathDefault_Click);
            // 
            // btnSQLPathChange
            // 
            this.btnSQLPathChange.Location = new System.Drawing.Point(597, 39);
            this.btnSQLPathChange.Name = "btnSQLPathChange";
            this.btnSQLPathChange.Size = new System.Drawing.Size(75, 23);
            this.btnSQLPathChange.TabIndex = 6;
            this.btnSQLPathChange.Text = "CHANGE";
            this.btnSQLPathChange.UseVisualStyleBackColor = true;
            this.btnSQLPathChange.Click += new System.EventHandler(this.btnSQLPathChange_Click);
            // 
            // txtSQLPath
            // 
            this.txtSQLPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtSQLPath.Location = new System.Drawing.Point(16, 42);
            this.txtSQLPath.Name = "txtSQLPath";
            this.txtSQLPath.ReadOnly = true;
            this.txtSQLPath.Size = new System.Drawing.Size(575, 20);
            this.txtSQLPath.TabIndex = 0;
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 677);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form5";
            this.Text = "FREDDIE\'S SETTINGS";
            this.Load += new System.EventHandler(this.Form5_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnIUDPathDefault;
        private System.Windows.Forms.Button btnIUDPathChange;
        private System.Windows.Forms.TextBox txtIUDPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnREADLPathDefault;
        private System.Windows.Forms.Button btnREADPathChange;
        private System.Windows.Forms.TextBox txtREADPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnMODELPathDefault;
        private System.Windows.Forms.Button btnMODELPathChange;
        private System.Windows.Forms.TextBox txtMODELPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnRemoveServer;
        private System.Windows.Forms.Button btnAddServer;
        private System.Windows.Forms.TextBox txtSERVER_NAME;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lstServers;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboServers;
        private System.Windows.Forms.Button btnRemoveFromMyDatabases;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckedListBox lstMyDatabases;
        private System.Windows.Forms.Button btnAddToMyDatabases;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckedListBox lstDatabases;
        private System.Windows.Forms.TextBox txtSERVER_ID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSQLPathDefault;
        private System.Windows.Forms.Button btnSQLPathChange;
        private System.Windows.Forms.TextBox txtSQLPath;
    }
}