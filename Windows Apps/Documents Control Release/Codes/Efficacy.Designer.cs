namespace GPS.Codes
{
    partial class Efficacy
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
            this.components = new System.ComponentModel.Container();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.efficacyOwnerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tsEfficacyOwnerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dtEffOwner = new GPS.Codes.dtEffOwner();
            this.tsEfficacyOwnerTableAdapter = new GPS.Codes.dtEffOwnerTableAdapters.tsEfficacyOwnerTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tsEfficacyOwnerBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEffOwner)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(632, 207);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 48);
            this.button2.TabIndex = 8;
            this.button2.Text = "Update";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(632, 90);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 48);
            this.button1.TabIndex = 7;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.efficacyOwnerDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.tsEfficacyOwnerBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(184, 61);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(262, 457);
            this.dataGridView1.TabIndex = 6;
            // 
            // efficacyOwnerDataGridViewTextBoxColumn
            // 
            this.efficacyOwnerDataGridViewTextBoxColumn.DataPropertyName = "EfficacyOwner";
            this.efficacyOwnerDataGridViewTextBoxColumn.HeaderText = "Efficacy Owner";
            this.efficacyOwnerDataGridViewTextBoxColumn.Name = "efficacyOwnerDataGridViewTextBoxColumn";
            this.efficacyOwnerDataGridViewTextBoxColumn.Width = 150;
            // 
            // tsEfficacyOwnerBindingSource
            // 
            this.tsEfficacyOwnerBindingSource.DataMember = "tsEfficacyOwner";
            this.tsEfficacyOwnerBindingSource.DataSource = this.dtEffOwner;
            // 
            // dtEffOwner
            // 
            this.dtEffOwner.DataSetName = "dtEffOwner";
            this.dtEffOwner.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tsEfficacyOwnerTableAdapter
            // 
            this.tsEfficacyOwnerTableAdapter.ClearBeforeFill = true;
            // 
            // Efficacy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1444, 882);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Efficacy";
            this.Text = "EFFICACY OWNER";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Efficacy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tsEfficacyOwnerBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEffOwner)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private dtEffOwner dtEffOwner;
        private System.Windows.Forms.BindingSource tsEfficacyOwnerBindingSource;
        private dtEffOwnerTableAdapters.tsEfficacyOwnerTableAdapter tsEfficacyOwnerTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn efficacyOwnerDataGridViewTextBoxColumn;
    }
}