namespace GPS.Documents
{
    partial class CreateDoc
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txTitle = new System.Windows.Forms.TextBox();
            this.txDescr = new System.Windows.Forms.TextBox();
            this.txShare = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.filtros1 = new GPS.List.Filtros();
            this.ddlEffOwner = new System.Windows.Forms.ComboBox();
            this.lblEfficacyOwner = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(156, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Title";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(156, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Description";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(492, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(290, 24);
            this.label4.TabIndex = 3;
            this.label4.Text = "CREATE A NEW DOCUMENT";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(309, 492);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(179, 40);
            this.button1.TabIndex = 8;
            this.button1.Text = "Add Document";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(744, 492);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(179, 40);
            this.button2.TabIndex = 9;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txTitle
            // 
            this.txTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txTitle.Location = new System.Drawing.Point(288, 91);
            this.txTitle.Name = "txTitle";
            this.txTitle.Size = new System.Drawing.Size(579, 26);
            this.txTitle.TabIndex = 10;
            // 
            // txDescr
            // 
            this.txDescr.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txDescr.Location = new System.Drawing.Point(288, 133);
            this.txDescr.Multiline = true;
            this.txDescr.Name = "txDescr";
            this.txDescr.Size = new System.Drawing.Size(579, 74);
            this.txDescr.TabIndex = 11;
            // 
            // txShare
            // 
            this.txShare.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txShare.Location = new System.Drawing.Point(288, 392);
            this.txShare.Name = "txShare";
            this.txShare.Size = new System.Drawing.Size(763, 26);
            this.txShare.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(156, 392);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 16);
            this.label9.TabIndex = 17;
            this.label9.Text = "Share Point";
            // 
            // filtros1
            // 
            this.filtros1.coverage = null;
            this.filtros1.dpto = null;
            this.filtros1.Location = new System.Drawing.Point(103, 247);
            this.filtros1.Name = "filtros1";
            this.filtros1.orgs = null;
            this.filtros1.Size = new System.Drawing.Size(1003, 89);
            this.filtros1.TabIndex = 19;
            this.filtros1.textoIni = null;
            this.filtros1.types = null;
            // 
            // ddlEffOwner
            // 
            this.ddlEffOwner.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlEffOwner.FormattingEnabled = true;
            this.ddlEffOwner.Location = new System.Drawing.Point(288, 349);
            this.ddlEffOwner.Name = "ddlEffOwner";
            this.ddlEffOwner.Size = new System.Drawing.Size(170, 24);
            this.ddlEffOwner.TabIndex = 42;
            // 
            // lblEfficacyOwner
            // 
            this.lblEfficacyOwner.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEfficacyOwner.Location = new System.Drawing.Point(156, 349);
            this.lblEfficacyOwner.Name = "lblEfficacyOwner";
            this.lblEfficacyOwner.Size = new System.Drawing.Size(126, 21);
            this.lblEfficacyOwner.TabIndex = 43;
            this.lblEfficacyOwner.Text = "Efficacy Owner";
            // 
            // CreateDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1354, 728);
            this.Controls.Add(this.ddlEffOwner);
            this.Controls.Add(this.lblEfficacyOwner);
            this.Controls.Add(this.filtros1);
            this.Controls.Add(this.txShare);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txDescr);
            this.Controls.Add(this.txTitle);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "CreateDoc";
            this.Text = "CREATE DOCUMENTS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CreateDoc_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txTitle;
        private System.Windows.Forms.TextBox txDescr;
        private System.Windows.Forms.TextBox txShare;
        private System.Windows.Forms.Label label9;
        private List.Filtros filtros1;
        private System.Windows.Forms.ComboBox ddlEffOwner;
        private System.Windows.Forms.Label lblEfficacyOwner;
    }
}