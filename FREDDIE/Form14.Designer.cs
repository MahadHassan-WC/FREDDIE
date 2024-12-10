namespace FREDDIE
{
    partial class Form14
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
            this.cboTables = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.optViews = new System.Windows.Forms.RadioButton();
            this.optTables = new System.Windows.Forms.RadioButton();
            this.cboDatabases = new System.Windows.Forms.ComboBox();
            this.cboServers = new System.Windows.Forms.ComboBox();
            this.txtCS = new System.Windows.Forms.TextBox();
            this.tabControl_Code = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.txtSQL = new System.Windows.Forms.TextBox();
            this.picKEY = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.chkMyDatabases = new System.Windows.Forms.CheckBox();
            this.lvColumnsWhere = new System.Windows.Forms.ListView();
            this.lvColumns = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.optUpdate = new System.Windows.Forms.RadioButton();
            this.optDelete = new System.Windows.Forms.RadioButton();
            this.optInsert = new System.Windows.Forms.RadioButton();
            this.optInsertWhereNotExists = new System.Windows.Forms.RadioButton();
            this.optSelectDistinct = new System.Windows.Forms.RadioButton();
            this.optSelect = new System.Windows.Forms.RadioButton();
            this.lblWhereClause = new System.Windows.Forms.Label();
            this.lblColumnsToUpdateInsert = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.cboDistinctColumns = new System.Windows.Forms.ComboBox();
            this.tabControl_Code.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picKEY)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboTables
            // 
            this.cboTables.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cboTables.FormattingEnabled = true;
            this.cboTables.Location = new System.Drawing.Point(1065, 68);
            this.cboTables.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboTables.Name = "cboTables";
            this.cboTables.Size = new System.Drawing.Size(601, 24);
            this.cboTables.TabIndex = 19;
            this.cboTables.SelectedIndexChanged += new System.EventHandler(this.cboTables_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 17);
            this.label1.TabIndex = 16;
            this.label1.Text = "SERVERS:";
            // 
            // optViews
            // 
            this.optViews.AutoSize = true;
            this.optViews.Location = new System.Drawing.Point(1177, 44);
            this.optViews.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.optViews.Name = "optViews";
            this.optViews.Size = new System.Drawing.Size(72, 21);
            this.optViews.TabIndex = 15;
            this.optViews.TabStop = true;
            this.optViews.Text = "VIEWS";
            this.optViews.UseVisualStyleBackColor = true;
            this.optViews.CheckedChanged += new System.EventHandler(this.optViews_CheckedChanged);
            // 
            // optTables
            // 
            this.optTables.AutoSize = true;
            this.optTables.Checked = true;
            this.optTables.Location = new System.Drawing.Point(1065, 44);
            this.optTables.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.optTables.Name = "optTables";
            this.optTables.Size = new System.Drawing.Size(82, 21);
            this.optTables.TabIndex = 14;
            this.optTables.TabStop = true;
            this.optTables.Text = "TABLES";
            this.optTables.UseVisualStyleBackColor = true;
            this.optTables.CheckedChanged += new System.EventHandler(this.optTables_CheckedChanged);
            // 
            // cboDatabases
            // 
            this.cboDatabases.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cboDatabases.FormattingEnabled = true;
            this.cboDatabases.Location = new System.Drawing.Point(396, 68);
            this.cboDatabases.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboDatabases.Name = "cboDatabases";
            this.cboDatabases.Size = new System.Drawing.Size(663, 24);
            this.cboDatabases.TabIndex = 13;
            this.cboDatabases.SelectedIndexChanged += new System.EventHandler(this.cboDatabases_SelectedIndexChanged);
            // 
            // cboServers
            // 
            this.cboServers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cboServers.FormattingEnabled = true;
            this.cboServers.Location = new System.Drawing.Point(19, 68);
            this.cboServers.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboServers.Name = "cboServers";
            this.cboServers.Size = new System.Drawing.Size(371, 24);
            this.cboServers.TabIndex = 12;
            this.cboServers.SelectedIndexChanged += new System.EventHandler(this.cboServers_SelectedIndexChanged);
            // 
            // txtCS
            // 
            this.txtCS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtCS.Location = new System.Drawing.Point(7, 7);
            this.txtCS.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCS.Multiline = true;
            this.txtCS.Name = "txtCS";
            this.txtCS.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCS.Size = new System.Drawing.Size(107, 173);
            this.txtCS.TabIndex = 22;
            // 
            // tabControl_Code
            // 
            this.tabControl_Code.Controls.Add(this.tabPage5);
            this.tabControl_Code.Controls.Add(this.tabPage6);
            this.tabControl_Code.Location = new System.Drawing.Point(727, 124);
            this.tabControl_Code.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl_Code.Name = "tabControl_Code";
            this.tabControl_Code.SelectedIndex = 0;
            this.tabControl_Code.Size = new System.Drawing.Size(947, 710);
            this.tabControl_Code.TabIndex = 23;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.txtCS);
            this.tabPage5.Location = new System.Drawing.Point(4, 25);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage5.Size = new System.Drawing.Size(939, 681);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "C#";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.txtSQL);
            this.tabPage6.Location = new System.Drawing.Point(4, 25);
            this.tabPage6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage6.Size = new System.Drawing.Size(939, 681);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "TSQL";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // txtSQL
            // 
            this.txtSQL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.txtSQL.Location = new System.Drawing.Point(624, 178);
            this.txtSQL.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSQL.Multiline = true;
            this.txtSQL.Name = "txtSQL";
            this.txtSQL.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSQL.Size = new System.Drawing.Size(107, 173);
            this.txtSQL.TabIndex = 23;
            // 
            // picKEY
            // 
            this.picKEY.Image = global::FREDDIE.Properties.Resources.key;
            this.picKEY.Location = new System.Drawing.Point(1633, 41);
            this.picKEY.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picKEY.Name = "picKEY";
            this.picKEY.Size = new System.Drawing.Size(35, 20);
            this.picKEY.TabIndex = 20;
            this.picKEY.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(21, 901);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(2073, 70);
            this.btnClose.TabIndex = 24;
            this.btnClose.Text = "CLOSE";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // chkMyDatabases
            // 
            this.chkMyDatabases.AutoSize = true;
            this.chkMyDatabases.Location = new System.Drawing.Point(396, 44);
            this.chkMyDatabases.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkMyDatabases.Name = "chkMyDatabases";
            this.chkMyDatabases.Size = new System.Drawing.Size(116, 21);
            this.chkMyDatabases.TabIndex = 25;
            this.chkMyDatabases.Text = "DATABASES:";
            this.chkMyDatabases.UseVisualStyleBackColor = true;
            this.chkMyDatabases.CheckedChanged += new System.EventHandler(this.chkMyDatabases_CheckedChanged);
            // 
            // lvColumnsWhere
            // 
            this.lvColumnsWhere.CheckBoxes = true;
            this.lvColumnsWhere.FullRowSelect = true;
            this.lvColumnsWhere.GridLines = true;
            this.lvColumnsWhere.Location = new System.Drawing.Point(21, 534);
            this.lvColumnsWhere.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lvColumnsWhere.MultiSelect = false;
            this.lvColumnsWhere.Name = "lvColumnsWhere";
            this.lvColumnsWhere.Size = new System.Drawing.Size(701, 295);
            this.lvColumnsWhere.TabIndex = 26;
            this.lvColumnsWhere.UseCompatibleStateImageBehavior = false;
            this.lvColumnsWhere.View = System.Windows.Forms.View.Details;
            // 
            // lvColumns
            // 
            this.lvColumns.CheckBoxes = true;
            this.lvColumns.GridLines = true;
            this.lvColumns.Location = new System.Drawing.Point(21, 245);
            this.lvColumns.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lvColumns.Name = "lvColumns";
            this.lvColumns.Size = new System.Drawing.Size(701, 260);
            this.lvColumns.TabIndex = 27;
            this.lvColumns.UseCompatibleStateImageBehavior = false;
            this.lvColumns.View = System.Windows.Forms.View.Details;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.optUpdate);
            this.groupBox1.Controls.Add(this.optDelete);
            this.groupBox1.Controls.Add(this.optInsert);
            this.groupBox1.Controls.Add(this.optInsertWhereNotExists);
            this.groupBox1.Controls.Add(this.optSelectDistinct);
            this.groupBox1.Controls.Add(this.optSelect);
            this.groupBox1.Location = new System.Drawing.Point(19, 113);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(700, 97);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CUSTOM C# METHODS";
            // 
            // optUpdate
            // 
            this.optUpdate.AutoSize = true;
            this.optUpdate.Location = new System.Drawing.Point(540, 31);
            this.optUpdate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.optUpdate.Name = "optUpdate";
            this.optUpdate.Size = new System.Drawing.Size(85, 21);
            this.optUpdate.TabIndex = 5;
            this.optUpdate.TabStop = true;
            this.optUpdate.Text = "UPDATE";
            this.optUpdate.UseVisualStyleBackColor = true;
            this.optUpdate.CheckedChanged += new System.EventHandler(this.optUpdate_CheckedChanged);
            // 
            // optDelete
            // 
            this.optDelete.AutoSize = true;
            this.optDelete.Location = new System.Drawing.Point(540, 59);
            this.optDelete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.optDelete.Name = "optDelete";
            this.optDelete.Size = new System.Drawing.Size(83, 21);
            this.optDelete.TabIndex = 4;
            this.optDelete.TabStop = true;
            this.optDelete.Text = "DELETE";
            this.optDelete.UseVisualStyleBackColor = true;
            this.optDelete.CheckedChanged += new System.EventHandler(this.optDelete_CheckedChanged);
            // 
            // optInsert
            // 
            this.optInsert.AutoSize = true;
            this.optInsert.Location = new System.Drawing.Point(247, 31);
            this.optInsert.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.optInsert.Name = "optInsert";
            this.optInsert.Size = new System.Drawing.Size(79, 21);
            this.optInsert.TabIndex = 3;
            this.optInsert.TabStop = true;
            this.optInsert.Text = "INSERT";
            this.optInsert.UseVisualStyleBackColor = true;
            this.optInsert.CheckedChanged += new System.EventHandler(this.optInsert_CheckedChanged);
            // 
            // optInsertWhereNotExists
            // 
            this.optInsertWhereNotExists.AutoSize = true;
            this.optInsertWhereNotExists.Location = new System.Drawing.Point(247, 59);
            this.optInsertWhereNotExists.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.optInsertWhereNotExists.Name = "optInsertWhereNotExists";
            this.optInsertWhereNotExists.Size = new System.Drawing.Size(220, 21);
            this.optInsertWhereNotExists.TabIndex = 2;
            this.optInsertWhereNotExists.TabStop = true;
            this.optInsertWhereNotExists.Text = "INSERT WHERE NOT EXISTS";
            this.optInsertWhereNotExists.UseVisualStyleBackColor = true;
            this.optInsertWhereNotExists.CheckedChanged += new System.EventHandler(this.optInsertWhereNotExists_CheckedChanged);
            // 
            // optSelectDistinct
            // 
            this.optSelectDistinct.AutoSize = true;
            this.optSelectDistinct.Location = new System.Drawing.Point(17, 59);
            this.optSelectDistinct.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.optSelectDistinct.Name = "optSelectDistinct";
            this.optSelectDistinct.Size = new System.Drawing.Size(148, 21);
            this.optSelectDistinct.TabIndex = 1;
            this.optSelectDistinct.TabStop = true;
            this.optSelectDistinct.Text = "SELECT DISTINCT";
            this.optSelectDistinct.UseVisualStyleBackColor = true;
            this.optSelectDistinct.CheckedChanged += new System.EventHandler(this.optSelectDistinct_CheckedChanged);
            // 
            // optSelect
            // 
            this.optSelect.AutoSize = true;
            this.optSelect.Location = new System.Drawing.Point(17, 31);
            this.optSelect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.optSelect.Name = "optSelect";
            this.optSelect.Size = new System.Drawing.Size(82, 21);
            this.optSelect.TabIndex = 0;
            this.optSelect.TabStop = true;
            this.optSelect.Text = "SELECT";
            this.optSelect.UseVisualStyleBackColor = true;
            this.optSelect.CheckedChanged += new System.EventHandler(this.optSelect_CheckedChanged);
            // 
            // lblWhereClause
            // 
            this.lblWhereClause.AutoSize = true;
            this.lblWhereClause.Location = new System.Drawing.Point(20, 513);
            this.lblWhereClause.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWhereClause.Name = "lblWhereClause";
            this.lblWhereClause.Size = new System.Drawing.Size(206, 17);
            this.lblWhereClause.TabIndex = 29;
            this.lblWhereClause.Text = "COLUMNS IN WHERE CLAUSE";
            // 
            // lblColumnsToUpdateInsert
            // 
            this.lblColumnsToUpdateInsert.AutoSize = true;
            this.lblColumnsToUpdateInsert.Location = new System.Drawing.Point(17, 225);
            this.lblColumnsToUpdateInsert.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblColumnsToUpdateInsert.Name = "lblColumnsToUpdateInsert";
            this.lblColumnsToUpdateInsert.Size = new System.Drawing.Size(218, 17);
            this.lblColumnsToUpdateInsert.TabIndex = 30;
            this.lblColumnsToUpdateInsert.Text = "COLUMNS TO INSERT, UPDATE";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(21, 837);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(2073, 57);
            this.btnGenerate.TabIndex = 31;
            this.btnGenerate.Text = "c#\\sql";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // cboDistinctColumns
            // 
            this.cboDistinctColumns.FormattingEnabled = true;
            this.cboDistinctColumns.Location = new System.Drawing.Point(21, 245);
            this.cboDistinctColumns.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboDistinctColumns.Name = "cboDistinctColumns";
            this.cboDistinctColumns.Size = new System.Drawing.Size(701, 24);
            this.cboDistinctColumns.TabIndex = 32;
            // 
            // Form14
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2167, 972);
            this.Controls.Add(this.cboDistinctColumns);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.lblColumnsToUpdateInsert);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblWhereClause);
            this.Controls.Add(this.lvColumns);
            this.Controls.Add(this.lvColumnsWhere);
            this.Controls.Add(this.chkMyDatabases);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tabControl_Code);
            this.Controls.Add(this.picKEY);
            this.Controls.Add(this.cboTables);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.optViews);
            this.Controls.Add(this.optTables);
            this.Controls.Add(this.cboDatabases);
            this.Controls.Add(this.cboServers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form14";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CREATE C#\\SQL SERVER OBJECTS - CUSTOM";
            this.Load += new System.EventHandler(this.Form14_Load);
            this.tabControl_Code.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picKEY)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picKEY;
        private System.Windows.Forms.ComboBox cboTables;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton optViews;
        private System.Windows.Forms.RadioButton optTables;
        private System.Windows.Forms.ComboBox cboDatabases;
        private System.Windows.Forms.ComboBox cboServers;
        private System.Windows.Forms.TextBox txtCS;
        private System.Windows.Forms.TabControl tabControl_Code;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TextBox txtSQL;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox chkMyDatabases;
        private System.Windows.Forms.ListView lvColumnsWhere;
        private System.Windows.Forms.ListView lvColumns;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton optUpdate;
        private System.Windows.Forms.RadioButton optDelete;
        private System.Windows.Forms.RadioButton optInsert;
        private System.Windows.Forms.RadioButton optInsertWhereNotExists;
        private System.Windows.Forms.RadioButton optSelectDistinct;
        private System.Windows.Forms.RadioButton optSelect;
        private System.Windows.Forms.Label lblWhereClause;
        private System.Windows.Forms.Label lblColumnsToUpdateInsert;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.ComboBox cboDistinctColumns;
    }
}