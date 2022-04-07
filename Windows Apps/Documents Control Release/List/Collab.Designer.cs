namespace GPS.List
{
    partial class Collab
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
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.filtros1 = new GPS.List.Filtros();
            this.lbMSG = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // dGrid1
            // 
            this.dGrid1.AllowUserToAddRows = false;
            this.dGrid1.AllowUserToDeleteRows = false;
            this.dGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGrid1.Location = new System.Drawing.Point(31, 217);
            this.dGrid1.Name = "dGrid1";
            this.dGrid1.Size = new System.Drawing.Size(1117, 425);
            this.dGrid1.TabIndex = 81;
            this.dGrid1.Visible = false;
            this.dGrid1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGrid1_CellDoubleClick);
            // 
            // ddlStatus
            // 
            this.ddlStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlStatus.FormattingEnabled = true;
            this.ddlStatus.Location = new System.Drawing.Point(158, 155);
            this.ddlStatus.Name = "ddlStatus";
            this.ddlStatus.Size = new System.Drawing.Size(247, 24);
            this.ddlStatus.TabIndex = 80;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(27, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 16);
            this.label3.TabIndex = 79;
            this.label3.Text = "Collaborator";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(782, 147);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 40);
            this.button2.TabIndex = 74;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(649, 148);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 40);
            this.button1.TabIndex = 73;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(418, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(230, 24);
            this.label4.TabIndex = 68;
            this.label4.Text = "SEARCH DOCUMENTS";
            // 
            // filtros1
            // 
            this.filtros1.coverage = "0";
            this.filtros1.dpto = "0";
            this.filtros1.Location = new System.Drawing.Point(-28, 45);
            this.filtros1.Name = "filtros1";
            this.filtros1.orgs = "0";
            this.filtros1.Size = new System.Drawing.Size(1003, 96);
            this.filtros1.TabIndex = 82;
            this.filtros1.textoIni = null;
            this.filtros1.types = "0";
            // 
            // lbMSG
            // 
            this.lbMSG.AutoSize = true;
            this.lbMSG.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMSG.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbMSG.Location = new System.Drawing.Point(30, 190);
            this.lbMSG.Name = "lbMSG";
            this.lbMSG.Size = new System.Drawing.Size(365, 16);
            this.lbMSG.TabIndex = 150;
            this.lbMSG.Text = "Review all the information by double clicking a row";
            this.lbMSG.Visible = false;
            // 
            // Collab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1436, 874);
            this.Controls.Add(this.lbMSG);
            this.Controls.Add(this.filtros1);
            this.Controls.Add(this.dGrid1);
            this.Controls.Add(this.ddlStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Name = "Collab";
            this.Text = "COLLABORATORS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Collab_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dGrid1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dGrid1;
        private System.Windows.Forms.ComboBox ddlStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private Filtros filtros1;
        private System.Windows.Forms.Label lbMSG;
    }
}