namespace GPS.Codes
{
    partial class Organizations
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
            this.orgcodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orgdescrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tsOrgListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dtOrgs = new GPS.Codes.dtOrgs();
            this.tsOrgListTableAdapter = new GPS.Codes.dtOrgsTableAdapters.tsOrgListTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tsOrgListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtOrgs)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(678, 199);
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
            this.button1.Location = new System.Drawing.Point(678, 82);
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
            this.orgcodeDataGridViewTextBoxColumn,
            this.orgdescrDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.tsOrgListBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(164, 63);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(373, 249);
            this.dataGridView1.TabIndex = 6;
            // 
            // orgcodeDataGridViewTextBoxColumn
            // 
            this.orgcodeDataGridViewTextBoxColumn.DataPropertyName = "orgcode";
            this.orgcodeDataGridViewTextBoxColumn.HeaderText = "Code";
            this.orgcodeDataGridViewTextBoxColumn.Name = "orgcodeDataGridViewTextBoxColumn";
            this.orgcodeDataGridViewTextBoxColumn.Width = 50;
            // 
            // orgdescrDataGridViewTextBoxColumn
            // 
            this.orgdescrDataGridViewTextBoxColumn.DataPropertyName = "org_descr";
            this.orgdescrDataGridViewTextBoxColumn.HeaderText = "Description";
            this.orgdescrDataGridViewTextBoxColumn.Name = "orgdescrDataGridViewTextBoxColumn";
            this.orgdescrDataGridViewTextBoxColumn.Width = 250;
            // 
            // tsOrgListBindingSource
            // 
            this.tsOrgListBindingSource.DataMember = "tsOrgList";
            this.tsOrgListBindingSource.DataSource = this.dtOrgs;
            // 
            // dtOrgs
            // 
            this.dtOrgs.DataSetName = "dtOrgs";
            this.dtOrgs.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tsOrgListTableAdapter
            // 
            this.tsOrgListTableAdapter.ClearBeforeFill = true;
            // 
            // Organizations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1374, 735);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Organizations";
            this.Text = "ORGANIZATIONS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Organizations_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tsOrgListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtOrgs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private dtOrgs dtOrgs;
        private System.Windows.Forms.BindingSource tsOrgListBindingSource;
        private dtOrgsTableAdapters.tsOrgListTableAdapter tsOrgListTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn orgcodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn orgdescrDataGridViewTextBoxColumn;
    }
}