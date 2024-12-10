namespace FREDDIE
{
    partial class Form7
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
            this.chkMyDatabases = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.optViews = new System.Windows.Forms.RadioButton();
            this.optTables = new System.Windows.Forms.RadioButton();
            this.cboDatabases = new System.Windows.Forms.ComboBox();
            this.cboServers = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtMODEL = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtREAD = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtIUD = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.txtSQL_SELECT_ALL = new System.Windows.Forms.TextBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.txtSQL_SELECT_BY_PK = new System.Windows.Forms.TextBox();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.txtSQL_INSERT_A_ROW = new System.Windows.Forms.TextBox();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.txtSQL_UPDATE_A_ROW = new System.Windows.Forms.TextBox();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.txtSQL_DELETE_BY_PK = new System.Windows.Forms.TextBox();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.txtSQL_DELETE_ALL = new System.Windows.Forms.TextBox();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage12 = new System.Windows.Forms.TabPage();
            this.txtUpdateARecord = new System.Windows.Forms.TextBox();
            this.tabPage13 = new System.Windows.Forms.TabPage();
            this.txtDeleteARecord = new System.Windows.Forms.TextBox();
            this.tabPage14 = new System.Windows.Forms.TabPage();
            this.txtLoadGrid = new System.Windows.Forms.TextBox();
            this.tabPage15 = new System.Windows.Forms.TabPage();
            this.txtGetDataFromGrid = new System.Windows.Forms.TextBox();
            this.tabPage16 = new System.Windows.Forms.TabPage();
            this.txtResetScreen = new System.Windows.Forms.TextBox();
            this.tabPage17 = new System.Windows.Forms.TabPage();
            this.txtRecord_Is_Valid = new System.Windows.Forms.TextBox();
            this.tabPage18 = new System.Windows.Forms.TabPage();
            this.txtModuleLevelVariables = new System.Windows.Forms.TextBox();
            this.tabPage19 = new System.Windows.Forms.TabPage();
            this.txtTextChanged = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.cboTables = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tabPage9.SuspendLayout();
            this.tabPage10.SuspendLayout();
            this.tabPage11.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage12.SuspendLayout();
            this.tabPage13.SuspendLayout();
            this.tabPage14.SuspendLayout();
            this.tabPage15.SuspendLayout();
            this.tabPage16.SuspendLayout();
            this.tabPage17.SuspendLayout();
            this.tabPage18.SuspendLayout();
            this.tabPage19.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkMyDatabases
            // 
            this.chkMyDatabases.AutoSize = true;
            this.chkMyDatabases.Location = new System.Drawing.Point(196, 19);
            this.chkMyDatabases.Name = "chkMyDatabases";
            this.chkMyDatabases.Size = new System.Drawing.Size(93, 17);
            this.chkMyDatabases.TabIndex = 21;
            this.chkMyDatabases.Text = "DATABASES:";
            this.chkMyDatabases.UseVisualStyleBackColor = true;
            this.chkMyDatabases.CheckedChanged += new System.EventHandler(this.chkMyDatabases_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "SERVERS:";
            // 
            // optViews
            // 
            this.optViews.AutoSize = true;
            this.optViews.Location = new System.Drawing.Point(613, 18);
            this.optViews.Margin = new System.Windows.Forms.Padding(2);
            this.optViews.Name = "optViews";
            this.optViews.Size = new System.Drawing.Size(60, 17);
            this.optViews.TabIndex = 17;
            this.optViews.TabStop = true;
            this.optViews.Text = "VIEWS";
            this.optViews.UseVisualStyleBackColor = true;
            this.optViews.CheckedChanged += new System.EventHandler(this.optViews_CheckedChanged);
            // 
            // optTables
            // 
            this.optTables.AutoSize = true;
            this.optTables.Location = new System.Drawing.Point(543, 18);
            this.optTables.Margin = new System.Windows.Forms.Padding(2);
            this.optTables.Name = "optTables";
            this.optTables.Size = new System.Drawing.Size(66, 17);
            this.optTables.TabIndex = 16;
            this.optTables.TabStop = true;
            this.optTables.Text = "TABLES";
            this.optTables.UseVisualStyleBackColor = true;
            this.optTables.CheckedChanged += new System.EventHandler(this.optTables_CheckedChanged);
            // 
            // cboDatabases
            // 
            this.cboDatabases.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cboDatabases.FormattingEnabled = true;
            this.cboDatabases.Location = new System.Drawing.Point(196, 40);
            this.cboDatabases.Margin = new System.Windows.Forms.Padding(2);
            this.cboDatabases.Name = "cboDatabases";
            this.cboDatabases.Size = new System.Drawing.Size(340, 21);
            this.cboDatabases.TabIndex = 15;
            this.cboDatabases.SelectedIndexChanged += new System.EventHandler(this.cboDatabases_SelectedIndexChanged);
            // 
            // cboServers
            // 
            this.cboServers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cboServers.FormattingEnabled = true;
            this.cboServers.Location = new System.Drawing.Point(2, 40);
            this.cboServers.Margin = new System.Windows.Forms.Padding(2);
            this.cboServers.Name = "cboServers";
            this.cboServers.Size = new System.Drawing.Size(190, 21);
            this.cboServers.TabIndex = 14;
            this.cboServers.SelectedIndexChanged += new System.EventHandler(this.cboServers_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage8);
            this.tabControl1.Location = new System.Drawing.Point(5, 66);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1304, 514);
            this.tabControl1.TabIndex = 22;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtMODEL);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(1296, 488);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "MODEL";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtMODEL
            // 
            this.txtMODEL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMODEL.Location = new System.Drawing.Point(4, 5);
            this.txtMODEL.Margin = new System.Windows.Forms.Padding(2);
            this.txtMODEL.Multiline = true;
            this.txtMODEL.Name = "txtMODEL";
            this.txtMODEL.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMODEL.Size = new System.Drawing.Size(1288, 482);
            this.txtMODEL.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtREAD);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(1296, 488);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "READ";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtREAD
            // 
            this.txtREAD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtREAD.Location = new System.Drawing.Point(4, 5);
            this.txtREAD.Margin = new System.Windows.Forms.Padding(2);
            this.txtREAD.Multiline = true;
            this.txtREAD.Name = "txtREAD";
            this.txtREAD.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtREAD.Size = new System.Drawing.Size(739, 482);
            this.txtREAD.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtIUD);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage3.Size = new System.Drawing.Size(1296, 488);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "IUD";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtIUD
            // 
            this.txtIUD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIUD.Location = new System.Drawing.Point(4, 5);
            this.txtIUD.Margin = new System.Windows.Forms.Padding(2);
            this.txtIUD.Multiline = true;
            this.txtIUD.Name = "txtIUD";
            this.txtIUD.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtIUD.Size = new System.Drawing.Size(739, 482);
            this.txtIUD.TabIndex = 1;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.tabControl2);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1296, 488);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "SQL";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Controls.Add(this.tabPage6);
            this.tabControl2.Controls.Add(this.tabPage7);
            this.tabControl2.Controls.Add(this.tabPage9);
            this.tabControl2.Controls.Add(this.tabPage10);
            this.tabControl2.Controls.Add(this.tabPage11);
            this.tabControl2.Location = new System.Drawing.Point(2, 2);
            this.tabControl2.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(1290, 486);
            this.tabControl2.TabIndex = 0;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.txtSQL_SELECT_ALL);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage5.Size = new System.Drawing.Size(1282, 460);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "SELECT ALL";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // txtSQL_SELECT_ALL
            // 
            this.txtSQL_SELECT_ALL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSQL_SELECT_ALL.Location = new System.Drawing.Point(6, 5);
            this.txtSQL_SELECT_ALL.Margin = new System.Windows.Forms.Padding(2);
            this.txtSQL_SELECT_ALL.Multiline = true;
            this.txtSQL_SELECT_ALL.Name = "txtSQL_SELECT_ALL";
            this.txtSQL_SELECT_ALL.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSQL_SELECT_ALL.Size = new System.Drawing.Size(1265, 451);
            this.txtSQL_SELECT_ALL.TabIndex = 0;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.txtSQL_SELECT_BY_PK);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage6.Size = new System.Drawing.Size(1282, 460);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "SELECT BY PK";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // txtSQL_SELECT_BY_PK
            // 
            this.txtSQL_SELECT_BY_PK.Location = new System.Drawing.Point(4, 5);
            this.txtSQL_SELECT_BY_PK.Margin = new System.Windows.Forms.Padding(2);
            this.txtSQL_SELECT_BY_PK.Multiline = true;
            this.txtSQL_SELECT_BY_PK.Name = "txtSQL_SELECT_BY_PK";
            this.txtSQL_SELECT_BY_PK.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSQL_SELECT_BY_PK.Size = new System.Drawing.Size(602, 453);
            this.txtSQL_SELECT_BY_PK.TabIndex = 1;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.txtSQL_INSERT_A_ROW);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage7.Size = new System.Drawing.Size(1282, 460);
            this.tabPage7.TabIndex = 2;
            this.tabPage7.Text = "INSERT";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // txtSQL_INSERT_A_ROW
            // 
            this.txtSQL_INSERT_A_ROW.Location = new System.Drawing.Point(4, 5);
            this.txtSQL_INSERT_A_ROW.Margin = new System.Windows.Forms.Padding(2);
            this.txtSQL_INSERT_A_ROW.Multiline = true;
            this.txtSQL_INSERT_A_ROW.Name = "txtSQL_INSERT_A_ROW";
            this.txtSQL_INSERT_A_ROW.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSQL_INSERT_A_ROW.Size = new System.Drawing.Size(602, 453);
            this.txtSQL_INSERT_A_ROW.TabIndex = 1;
            // 
            // tabPage9
            // 
            this.tabPage9.Controls.Add(this.txtSQL_UPDATE_A_ROW);
            this.tabPage9.Location = new System.Drawing.Point(4, 22);
            this.tabPage9.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Size = new System.Drawing.Size(1282, 460);
            this.tabPage9.TabIndex = 4;
            this.tabPage9.Text = "UPDATE BY PK";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // txtSQL_UPDATE_A_ROW
            // 
            this.txtSQL_UPDATE_A_ROW.Location = new System.Drawing.Point(4, 5);
            this.txtSQL_UPDATE_A_ROW.Margin = new System.Windows.Forms.Padding(2);
            this.txtSQL_UPDATE_A_ROW.Multiline = true;
            this.txtSQL_UPDATE_A_ROW.Name = "txtSQL_UPDATE_A_ROW";
            this.txtSQL_UPDATE_A_ROW.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSQL_UPDATE_A_ROW.Size = new System.Drawing.Size(605, 456);
            this.txtSQL_UPDATE_A_ROW.TabIndex = 1;
            // 
            // tabPage10
            // 
            this.tabPage10.Controls.Add(this.txtSQL_DELETE_BY_PK);
            this.tabPage10.Location = new System.Drawing.Point(4, 22);
            this.tabPage10.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Size = new System.Drawing.Size(1282, 460);
            this.tabPage10.TabIndex = 5;
            this.tabPage10.Text = "DELETE BY PK";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // txtSQL_DELETE_BY_PK
            // 
            this.txtSQL_DELETE_BY_PK.Location = new System.Drawing.Point(4, 5);
            this.txtSQL_DELETE_BY_PK.Margin = new System.Windows.Forms.Padding(2);
            this.txtSQL_DELETE_BY_PK.Multiline = true;
            this.txtSQL_DELETE_BY_PK.Name = "txtSQL_DELETE_BY_PK";
            this.txtSQL_DELETE_BY_PK.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSQL_DELETE_BY_PK.Size = new System.Drawing.Size(605, 456);
            this.txtSQL_DELETE_BY_PK.TabIndex = 1;
            // 
            // tabPage11
            // 
            this.tabPage11.Controls.Add(this.txtSQL_DELETE_ALL);
            this.tabPage11.Location = new System.Drawing.Point(4, 22);
            this.tabPage11.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Size = new System.Drawing.Size(1282, 460);
            this.tabPage11.TabIndex = 6;
            this.tabPage11.Text = "DELETE ALL";
            this.tabPage11.UseVisualStyleBackColor = true;
            // 
            // txtSQL_DELETE_ALL
            // 
            this.txtSQL_DELETE_ALL.Location = new System.Drawing.Point(4, 5);
            this.txtSQL_DELETE_ALL.Margin = new System.Windows.Forms.Padding(2);
            this.txtSQL_DELETE_ALL.Multiline = true;
            this.txtSQL_DELETE_ALL.Name = "txtSQL_DELETE_ALL";
            this.txtSQL_DELETE_ALL.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSQL_DELETE_ALL.Size = new System.Drawing.Size(605, 456);
            this.txtSQL_DELETE_ALL.TabIndex = 1;
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.tabControl3);
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(1296, 488);
            this.tabPage8.TabIndex = 4;
            this.tabPage8.Text = "GUI";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPage12);
            this.tabControl3.Controls.Add(this.tabPage13);
            this.tabControl3.Controls.Add(this.tabPage14);
            this.tabControl3.Controls.Add(this.tabPage15);
            this.tabControl3.Controls.Add(this.tabPage16);
            this.tabControl3.Controls.Add(this.tabPage17);
            this.tabControl3.Controls.Add(this.tabPage18);
            this.tabControl3.Controls.Add(this.tabPage19);
            this.tabControl3.Location = new System.Drawing.Point(6, 6);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(1284, 476);
            this.tabControl3.TabIndex = 0;
            // 
            // tabPage12
            // 
            this.tabPage12.Controls.Add(this.txtUpdateARecord);
            this.tabPage12.Location = new System.Drawing.Point(4, 22);
            this.tabPage12.Name = "tabPage12";
            this.tabPage12.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage12.Size = new System.Drawing.Size(1276, 450);
            this.tabPage12.TabIndex = 0;
            this.tabPage12.Text = "Update_A_Record";
            this.tabPage12.UseVisualStyleBackColor = true;
            // 
            // txtUpdateARecord
            // 
            this.txtUpdateARecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUpdateARecord.Location = new System.Drawing.Point(5, 5);
            this.txtUpdateARecord.Margin = new System.Windows.Forms.Padding(2);
            this.txtUpdateARecord.Multiline = true;
            this.txtUpdateARecord.Name = "txtUpdateARecord";
            this.txtUpdateARecord.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtUpdateARecord.Size = new System.Drawing.Size(1266, 440);
            this.txtUpdateARecord.TabIndex = 3;
            // 
            // tabPage13
            // 
            this.tabPage13.Controls.Add(this.txtDeleteARecord);
            this.tabPage13.Location = new System.Drawing.Point(4, 22);
            this.tabPage13.Name = "tabPage13";
            this.tabPage13.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage13.Size = new System.Drawing.Size(1276, 450);
            this.tabPage13.TabIndex = 1;
            this.tabPage13.Text = "Delete_A_Record";
            this.tabPage13.UseVisualStyleBackColor = true;
            // 
            // txtDeleteARecord
            // 
            this.txtDeleteARecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeleteARecord.Location = new System.Drawing.Point(5, 5);
            this.txtDeleteARecord.Margin = new System.Windows.Forms.Padding(2);
            this.txtDeleteARecord.Multiline = true;
            this.txtDeleteARecord.Name = "txtDeleteARecord";
            this.txtDeleteARecord.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDeleteARecord.Size = new System.Drawing.Size(1266, 440);
            this.txtDeleteARecord.TabIndex = 5;
            // 
            // tabPage14
            // 
            this.tabPage14.Controls.Add(this.txtLoadGrid);
            this.tabPage14.Location = new System.Drawing.Point(4, 22);
            this.tabPage14.Name = "tabPage14";
            this.tabPage14.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage14.Size = new System.Drawing.Size(1276, 450);
            this.tabPage14.TabIndex = 2;
            this.tabPage14.Text = "LoadGrid";
            this.tabPage14.UseVisualStyleBackColor = true;
            // 
            // txtLoadGrid
            // 
            this.txtLoadGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoadGrid.Location = new System.Drawing.Point(5, 5);
            this.txtLoadGrid.Margin = new System.Windows.Forms.Padding(2);
            this.txtLoadGrid.Multiline = true;
            this.txtLoadGrid.Name = "txtLoadGrid";
            this.txtLoadGrid.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLoadGrid.Size = new System.Drawing.Size(1266, 440);
            this.txtLoadGrid.TabIndex = 3;
            // 
            // tabPage15
            // 
            this.tabPage15.Controls.Add(this.txtGetDataFromGrid);
            this.tabPage15.Location = new System.Drawing.Point(4, 22);
            this.tabPage15.Name = "tabPage15";
            this.tabPage15.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage15.Size = new System.Drawing.Size(1276, 450);
            this.tabPage15.TabIndex = 3;
            this.tabPage15.Text = "GetDataFromGrid";
            this.tabPage15.UseVisualStyleBackColor = true;
            // 
            // txtGetDataFromGrid
            // 
            this.txtGetDataFromGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGetDataFromGrid.Location = new System.Drawing.Point(5, 5);
            this.txtGetDataFromGrid.Margin = new System.Windows.Forms.Padding(2);
            this.txtGetDataFromGrid.Multiline = true;
            this.txtGetDataFromGrid.Name = "txtGetDataFromGrid";
            this.txtGetDataFromGrid.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtGetDataFromGrid.Size = new System.Drawing.Size(1266, 440);
            this.txtGetDataFromGrid.TabIndex = 2;
            // 
            // tabPage16
            // 
            this.tabPage16.Controls.Add(this.txtResetScreen);
            this.tabPage16.Location = new System.Drawing.Point(4, 22);
            this.tabPage16.Name = "tabPage16";
            this.tabPage16.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage16.Size = new System.Drawing.Size(1276, 450);
            this.tabPage16.TabIndex = 4;
            this.tabPage16.Text = "ResetScreen";
            this.tabPage16.UseVisualStyleBackColor = true;
            // 
            // txtResetScreen
            // 
            this.txtResetScreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResetScreen.Location = new System.Drawing.Point(5, 5);
            this.txtResetScreen.Margin = new System.Windows.Forms.Padding(2);
            this.txtResetScreen.Multiline = true;
            this.txtResetScreen.Name = "txtResetScreen";
            this.txtResetScreen.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResetScreen.Size = new System.Drawing.Size(1266, 440);
            this.txtResetScreen.TabIndex = 3;
            // 
            // tabPage17
            // 
            this.tabPage17.Controls.Add(this.txtRecord_Is_Valid);
            this.tabPage17.Location = new System.Drawing.Point(4, 22);
            this.tabPage17.Name = "tabPage17";
            this.tabPage17.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage17.Size = new System.Drawing.Size(1276, 450);
            this.tabPage17.TabIndex = 5;
            this.tabPage17.Text = "Record_Is_Valid";
            this.tabPage17.UseVisualStyleBackColor = true;
            // 
            // txtRecord_Is_Valid
            // 
            this.txtRecord_Is_Valid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRecord_Is_Valid.Location = new System.Drawing.Point(5, 5);
            this.txtRecord_Is_Valid.Margin = new System.Windows.Forms.Padding(2);
            this.txtRecord_Is_Valid.Multiline = true;
            this.txtRecord_Is_Valid.Name = "txtRecord_Is_Valid";
            this.txtRecord_Is_Valid.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRecord_Is_Valid.Size = new System.Drawing.Size(1266, 440);
            this.txtRecord_Is_Valid.TabIndex = 4;
            // 
            // tabPage18
            // 
            this.tabPage18.Controls.Add(this.txtModuleLevelVariables);
            this.tabPage18.Location = new System.Drawing.Point(4, 22);
            this.tabPage18.Name = "tabPage18";
            this.tabPage18.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage18.Size = new System.Drawing.Size(1276, 450);
            this.tabPage18.TabIndex = 6;
            this.tabPage18.Text = "Module Level Variables";
            this.tabPage18.UseVisualStyleBackColor = true;
            // 
            // txtModuleLevelVariables
            // 
            this.txtModuleLevelVariables.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtModuleLevelVariables.Location = new System.Drawing.Point(5, 5);
            this.txtModuleLevelVariables.Margin = new System.Windows.Forms.Padding(2);
            this.txtModuleLevelVariables.Multiline = true;
            this.txtModuleLevelVariables.Name = "txtModuleLevelVariables";
            this.txtModuleLevelVariables.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtModuleLevelVariables.Size = new System.Drawing.Size(1266, 440);
            this.txtModuleLevelVariables.TabIndex = 4;
            // 
            // tabPage19
            // 
            this.tabPage19.Controls.Add(this.txtTextChanged);
            this.tabPage19.Location = new System.Drawing.Point(4, 22);
            this.tabPage19.Name = "tabPage19";
            this.tabPage19.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage19.Size = new System.Drawing.Size(1276, 450);
            this.tabPage19.TabIndex = 7;
            this.tabPage19.Text = "Text Changed Event";
            this.tabPage19.UseVisualStyleBackColor = true;
            // 
            // txtTextChanged
            // 
            this.txtTextChanged.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTextChanged.Location = new System.Drawing.Point(5, 5);
            this.txtTextChanged.Margin = new System.Windows.Forms.Padding(2);
            this.txtTextChanged.Multiline = true;
            this.txtTextChanged.Name = "txtTextChanged";
            this.txtTextChanged.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtTextChanged.Size = new System.Drawing.Size(1266, 440);
            this.txtTextChanged.TabIndex = 5;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(5, 584);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(1296, 37);
            this.btnClose.TabIndex = 24;
            this.btnClose.Text = "CLOSE";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cboTables
            // 
            this.cboTables.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cboTables.FormattingEnabled = true;
            this.cboTables.Location = new System.Drawing.Point(540, 40);
            this.cboTables.Name = "cboTables";
            this.cboTables.Size = new System.Drawing.Size(462, 21);
            this.cboTables.TabIndex = 26;
            this.cboTables.SelectedIndexChanged += new System.EventHandler(this.cboTables_SelectedIndexChanged);
            // 
            // Form7
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1316, 628);
            this.Controls.Add(this.cboTables);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.chkMyDatabases);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.optViews);
            this.Controls.Add(this.optTables);
            this.Controls.Add(this.cboDatabases);
            this.Controls.Add(this.cboServers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form7";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CREATE C#\\SQL SERVER OBJECTS - DEFAULT";
            this.Load += new System.EventHandler(this.Form7_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.tabPage7.PerformLayout();
            this.tabPage9.ResumeLayout(false);
            this.tabPage9.PerformLayout();
            this.tabPage10.ResumeLayout(false);
            this.tabPage10.PerformLayout();
            this.tabPage11.ResumeLayout(false);
            this.tabPage11.PerformLayout();
            this.tabPage8.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tabPage12.ResumeLayout(false);
            this.tabPage12.PerformLayout();
            this.tabPage13.ResumeLayout(false);
            this.tabPage13.PerformLayout();
            this.tabPage14.ResumeLayout(false);
            this.tabPage14.PerformLayout();
            this.tabPage15.ResumeLayout(false);
            this.tabPage15.PerformLayout();
            this.tabPage16.ResumeLayout(false);
            this.tabPage16.PerformLayout();
            this.tabPage17.ResumeLayout(false);
            this.tabPage17.PerformLayout();
            this.tabPage18.ResumeLayout(false);
            this.tabPage18.PerformLayout();
            this.tabPage19.ResumeLayout(false);
            this.tabPage19.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkMyDatabases;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton optViews;
        private System.Windows.Forms.RadioButton optTables;
        private System.Windows.Forms.ComboBox cboDatabases;
        private System.Windows.Forms.ComboBox cboServers;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox txtMODEL;
        private System.Windows.Forms.TextBox txtREAD;
        private System.Windows.Forms.TextBox txtIUD;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TextBox txtSQL_SELECT_ALL;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TabPage tabPage9;
        private System.Windows.Forms.TabPage tabPage10;
        private System.Windows.Forms.TabPage tabPage11;
        private System.Windows.Forms.TextBox txtSQL_SELECT_BY_PK;
        private System.Windows.Forms.TextBox txtSQL_INSERT_A_ROW;
        private System.Windows.Forms.TextBox txtSQL_UPDATE_A_ROW;
        private System.Windows.Forms.TextBox txtSQL_DELETE_BY_PK;
        private System.Windows.Forms.TextBox txtSQL_DELETE_ALL;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cboTables;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPage12;
        private System.Windows.Forms.TabPage tabPage13;
        private System.Windows.Forms.TabPage tabPage14;
        private System.Windows.Forms.TabPage tabPage15;
        private System.Windows.Forms.TextBox txtGetDataFromGrid;
        private System.Windows.Forms.TextBox txtUpdateARecord;
        private System.Windows.Forms.TextBox txtLoadGrid;
        private System.Windows.Forms.TabPage tabPage16;
        private System.Windows.Forms.TextBox txtResetScreen;
        private System.Windows.Forms.TabPage tabPage17;
        private System.Windows.Forms.TextBox txtRecord_Is_Valid;
        private System.Windows.Forms.TabPage tabPage18;
        private System.Windows.Forms.TextBox txtDeleteARecord;
        private System.Windows.Forms.TextBox txtModuleLevelVariables;
        private System.Windows.Forms.TabPage tabPage19;
        private System.Windows.Forms.TextBox txtTextChanged;
    }
}