namespace FREDDIE
{
    partial class Form8
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
            this.chkMyDatabases = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.optViews = new System.Windows.Forms.RadioButton();
            this.optTables = new System.Windows.Forms.RadioButton();
            this.cboDatabases = new System.Windows.Forms.ComboBox();
            this.cboServers = new System.Windows.Forms.ComboBox();
            this.txtMODEL = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cboTables
            // 
            this.cboTables.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cboTables.FormattingEnabled = true;
            this.cboTables.Location = new System.Drawing.Point(549, 44);
            this.cboTables.Name = "cboTables";
            this.cboTables.Size = new System.Drawing.Size(462, 21);
            this.cboTables.TabIndex = 33;
            this.cboTables.SelectedIndexChanged += new System.EventHandler(this.cboTables_SelectedIndexChanged);
            // 
            // chkMyDatabases
            // 
            this.chkMyDatabases.AutoSize = true;
            this.chkMyDatabases.Location = new System.Drawing.Point(205, 23);
            this.chkMyDatabases.Name = "chkMyDatabases";
            this.chkMyDatabases.Size = new System.Drawing.Size(93, 17);
            this.chkMyDatabases.TabIndex = 32;
            this.chkMyDatabases.Text = "DATABASES:";
            this.chkMyDatabases.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "SERVERS:";
            // 
            // optViews
            // 
            this.optViews.AutoSize = true;
            this.optViews.Location = new System.Drawing.Point(622, 22);
            this.optViews.Margin = new System.Windows.Forms.Padding(2);
            this.optViews.Name = "optViews";
            this.optViews.Size = new System.Drawing.Size(60, 17);
            this.optViews.TabIndex = 30;
            this.optViews.TabStop = true;
            this.optViews.Text = "VIEWS";
            this.optViews.UseVisualStyleBackColor = true;
            // 
            // optTables
            // 
            this.optTables.AutoSize = true;
            this.optTables.Location = new System.Drawing.Point(552, 22);
            this.optTables.Margin = new System.Windows.Forms.Padding(2);
            this.optTables.Name = "optTables";
            this.optTables.Size = new System.Drawing.Size(66, 17);
            this.optTables.TabIndex = 29;
            this.optTables.TabStop = true;
            this.optTables.Text = "TABLES";
            this.optTables.UseVisualStyleBackColor = true;
            this.optTables.CheckedChanged += new System.EventHandler(this.optTables_CheckedChanged);
            // 
            // cboDatabases
            // 
            this.cboDatabases.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cboDatabases.FormattingEnabled = true;
            this.cboDatabases.Location = new System.Drawing.Point(205, 44);
            this.cboDatabases.Margin = new System.Windows.Forms.Padding(2);
            this.cboDatabases.Name = "cboDatabases";
            this.cboDatabases.Size = new System.Drawing.Size(340, 21);
            this.cboDatabases.TabIndex = 28;
            this.cboDatabases.SelectedIndexChanged += new System.EventHandler(this.cboDatabases_SelectedIndexChanged);
            // 
            // cboServers
            // 
            this.cboServers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cboServers.FormattingEnabled = true;
            this.cboServers.Location = new System.Drawing.Point(11, 44);
            this.cboServers.Margin = new System.Windows.Forms.Padding(2);
            this.cboServers.Name = "cboServers";
            this.cboServers.Size = new System.Drawing.Size(190, 21);
            this.cboServers.TabIndex = 27;
            this.cboServers.SelectedIndexChanged += new System.EventHandler(this.cboServers_SelectedIndexChanged);
            // 
            // txtMODEL
            // 
            this.txtMODEL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMODEL.Location = new System.Drawing.Point(11, 82);
            this.txtMODEL.Margin = new System.Windows.Forms.Padding(2);
            this.txtMODEL.Multiline = true;
            this.txtMODEL.Name = "txtMODEL";
            this.txtMODEL.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMODEL.Size = new System.Drawing.Size(1000, 617);
            this.txtMODEL.TabIndex = 34;
            // 
            // Form8
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 710);
            this.Controls.Add(this.txtMODEL);
            this.Controls.Add(this.cboTables);
            this.Controls.Add(this.chkMyDatabases);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.optViews);
            this.Controls.Add(this.optTables);
            this.Controls.Add(this.cboDatabases);
            this.Controls.Add(this.cboServers);
            this.Name = "Form8";
            this.Text = "Create C# Model Class - CUSTOM";
            this.Load += new System.EventHandler(this.Form8_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboTables;
        private System.Windows.Forms.CheckBox chkMyDatabases;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton optViews;
        private System.Windows.Forms.RadioButton optTables;
        private System.Windows.Forms.ComboBox cboDatabases;
        private System.Windows.Forms.ComboBox cboServers;
        private System.Windows.Forms.TextBox txtMODEL;
    }
}