namespace GPS.List
{
    partial class Owner
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
            this.dGrid1 = new System.Windows.Forms.DataGridView();
            this.ddlStatus = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ddlType = new System.Windows.Forms.ComboBox();
            this.dpDepto = new System.Windows.Forms.ComboBox();
            this.ddlCoverage = new System.Windows.Forms.ComboBox();
            this.ddlOrg = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.filtros1 = new GPS.List.Filtros();
            ((System.ComponentModel.ISupportInitialize)(this.dGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // dGrid1
            // 
            this.dGrid1.AllowUserToAddRows = false;
            this.dGrid1.AllowUserToDeleteRows = false;
            this.dGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGrid1.Location = new System.Drawing.Point(25, 255);
            this.dGrid1.Name = "dGrid1";
            this.dGrid1.Size = new System.Drawing.Size(1256, 367);
            this.dGrid1.TabIndex = 95;
            this.dGrid1.Visible = false;
            this.dGrid1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGrid1_CellContentClick);
            this.dGrid1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGrid1_CellDoubleClick);
            // 
            // ddlStatus
            // 
            this.ddlStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlStatus.FormattingEnabled = true;
            this.ddlStatus.Location = new System.Drawing.Point(192, 202);
            this.ddlStatus.Name = "ddlStatus";
            this.ddlStatus.Size = new System.Drawing.Size(280, 28);
            this.ddlStatus.TabIndex = 94;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(61, 205);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 20);
            this.label3.TabIndex = 93;
            this.label3.Text = "Owner";
            // 
            // ddlType
            // 
            this.ddlType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlType.FormattingEnabled = true;
            this.ddlType.Location = new System.Drawing.Point(738, 168);
            this.ddlType.Name = "ddlType";
            this.ddlType.Size = new System.Drawing.Size(178, 28);
            this.ddlType.TabIndex = 92;
            // 
            // dpDepto
            // 
            this.dpDepto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dpDepto.FormattingEnabled = true;
            this.dpDepto.Location = new System.Drawing.Point(153, 168);
            this.dpDepto.Name = "dpDepto";
            this.dpDepto.Size = new System.Drawing.Size(470, 28);
            this.dpDepto.TabIndex = 91;
            // 
            // ddlCoverage
            // 
            this.ddlCoverage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlCoverage.FormattingEnabled = true;
            this.ddlCoverage.Location = new System.Drawing.Point(738, 131);
            this.ddlCoverage.Name = "ddlCoverage";
            this.ddlCoverage.Size = new System.Drawing.Size(178, 28);
            this.ddlCoverage.TabIndex = 90;
            // 
            // ddlOrg
            // 
            this.ddlOrg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlOrg.FormattingEnabled = true;
            this.ddlOrg.Location = new System.Drawing.Point(153, 134);
            this.ddlOrg.Name = "ddlOrg";
            this.ddlOrg.Size = new System.Drawing.Size(470, 28);
            this.ddlOrg.TabIndex = 89;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(801, 44);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 40);
            this.button2.TabIndex = 88;
            this.button2.Text = "Exit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(661, 44);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 40);
            this.button1.TabIndex = 87;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(647, 168);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 20);
            this.label8.TabIndex = 86;
            this.label8.Text = "Doc. Type";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(21, 176);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 20);
            this.label7.TabIndex = 85;
            this.label7.Text = "Department";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(647, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 20);
            this.label6.TabIndex = 84;
            this.label6.Text = "Coverage";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(21, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 20);
            this.label5.TabIndex = 83;
            this.label5.Text = "Organization";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(403, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(230, 24);
            this.label4.TabIndex = 82;
            this.label4.Text = "SEARCH DOCUMENTS";
            // 
            // filtros1
            // 
            this.filtros1.coverage = "0";
            this.filtros1.dpto = "0";
            this.filtros1.Location = new System.Drawing.Point(2, 99);
            this.filtros1.Name = "filtros1";
            this.filtros1.orgs = "0";
            this.filtros1.Size = new System.Drawing.Size(1003, 97);
            this.filtros1.TabIndex = 96;
            this.filtros1.types = "0";
            // 
            // Owner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1360, 737);
            this.Controls.Add(this.filtros1);
            this.Controls.Add(this.dGrid1);
            this.Controls.Add(this.ddlStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ddlType);
            this.Controls.Add(this.dpDepto);
            this.Controls.Add(this.ddlCoverage);
            this.Controls.Add(this.ddlOrg);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Name = "Owner";
            this.Text = "DOCUMENT OWNER";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Owner_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dGrid1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dGrid1;
        private System.Windows.Forms.ComboBox ddlStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ddlType;
        private System.Windows.Forms.ComboBox dpDepto;
        private System.Windows.Forms.ComboBox ddlCoverage;
        private System.Windows.Forms.ComboBox ddlOrg;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private Filtros filtros1;
    }
}