namespace GPS.List
{
    partial class Filtros
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ddlType = new System.Windows.Forms.ComboBox();
            this.dpDepto = new System.Windows.Forms.ComboBox();
            this.ddlCoverage = new System.Windows.Forms.ComboBox();
            this.ddlOrg = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ddlType
            // 
            this.ddlType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlType.FormattingEnabled = true;
            this.ddlType.Location = new System.Drawing.Point(737, 52);
            this.ddlType.Name = "ddlType";
            this.ddlType.Size = new System.Drawing.Size(150, 24);
            this.ddlType.TabIndex = 100;
            this.ddlType.SelectedIndexChanged += new System.EventHandler(this.ddlType_SelectedIndexChanged);
            // 
            // dpDepto
            // 
            this.dpDepto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dpDepto.FormattingEnabled = true;
            this.dpDepto.Location = new System.Drawing.Point(186, 52);
            this.dpDepto.Name = "dpDepto";
            this.dpDepto.Size = new System.Drawing.Size(405, 24);
            this.dpDepto.TabIndex = 99;
            this.dpDepto.SelectedIndexChanged += new System.EventHandler(this.dpDepto_SelectedIndexChanged);
            // 
            // ddlCoverage
            // 
            this.ddlCoverage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlCoverage.FormattingEnabled = true;
            this.ddlCoverage.Location = new System.Drawing.Point(737, 15);
            this.ddlCoverage.Name = "ddlCoverage";
            this.ddlCoverage.Size = new System.Drawing.Size(150, 24);
            this.ddlCoverage.TabIndex = 98;
            this.ddlCoverage.SelectedIndexChanged += new System.EventHandler(this.ddlCoverage_SelectedIndexChanged);
            // 
            // ddlOrg
            // 
            this.ddlOrg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlOrg.FormattingEnabled = true;
            this.ddlOrg.Location = new System.Drawing.Point(186, 18);
            this.ddlOrg.Name = "ddlOrg";
            this.ddlOrg.Size = new System.Drawing.Size(405, 24);
            this.ddlOrg.TabIndex = 97;
            this.ddlOrg.SelectedIndexChanged += new System.EventHandler(this.ddlOrg_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(646, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 16);
            this.label8.TabIndex = 96;
            this.label8.Text = "Doc. Type";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(54, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 16);
            this.label7.TabIndex = 95;
            this.label7.Text = "Department";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(646, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 16);
            this.label6.TabIndex = 94;
            this.label6.Text = "Coverage";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(54, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 16);
            this.label5.TabIndex = 93;
            this.label5.Text = "Organization";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // Filtros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ddlType);
            this.Controls.Add(this.dpDepto);
            this.Controls.Add(this.ddlCoverage);
            this.Controls.Add(this.ddlOrg);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Name = "Filtros";
            this.Size = new System.Drawing.Size(904, 89);
            this.Load += new System.EventHandler(this.Filtros_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ddlType;
        private System.Windows.Forms.ComboBox dpDepto;
        private System.Windows.Forms.ComboBox ddlCoverage;
        private System.Windows.Forms.ComboBox ddlOrg;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
    }
}
