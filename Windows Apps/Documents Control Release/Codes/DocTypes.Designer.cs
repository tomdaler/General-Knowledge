namespace GPS.Codes
{
    partial class DocTypes
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
            this.dtDocType = new GPS.Codes.dtDocType();
            this.tsDocumentTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tsDocumentTypeTableAdapter = new GPS.Codes.dtDocTypeTableAdapters.tsDocumentTypeTableAdapter();
            this.docCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.docTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDocType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tsDocumentTypeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(646, 206);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 48);
            this.button2.TabIndex = 11;
            this.button2.Text = "Update";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(646, 89);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 48);
            this.button1.TabIndex = 10;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.docCodeDataGridViewTextBoxColumn,
            this.docTypeDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.tsDocumentTypeBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(198, 60);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(262, 457);
            this.dataGridView1.TabIndex = 9;
            // 
            // dtDocType
            // 
            this.dtDocType.DataSetName = "dtDocType";
            this.dtDocType.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tsDocumentTypeBindingSource
            // 
            this.tsDocumentTypeBindingSource.DataMember = "tsDocumentType";
            this.tsDocumentTypeBindingSource.DataSource = this.dtDocType;
            // 
            // tsDocumentTypeTableAdapter
            // 
            this.tsDocumentTypeTableAdapter.ClearBeforeFill = true;
            // 
            // docCodeDataGridViewTextBoxColumn
            // 
            this.docCodeDataGridViewTextBoxColumn.DataPropertyName = "DocCode";
            this.docCodeDataGridViewTextBoxColumn.HeaderText = "Code";
            this.docCodeDataGridViewTextBoxColumn.Name = "docCodeDataGridViewTextBoxColumn";
            // 
            // docTypeDataGridViewTextBoxColumn
            // 
            this.docTypeDataGridViewTextBoxColumn.DataPropertyName = "DocType";
            this.docTypeDataGridViewTextBoxColumn.HeaderText = "Type";
            this.docTypeDataGridViewTextBoxColumn.Name = "docTypeDataGridViewTextBoxColumn";
            // 
            // DocTypes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1444, 882);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "DocTypes";
            this.Text = "DOCUMENT TYPE";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DocTypes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDocType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tsDocumentTypeBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private dtDocType dtDocType;
        private System.Windows.Forms.BindingSource tsDocumentTypeBindingSource;
        private dtDocTypeTableAdapters.tsDocumentTypeTableAdapter tsDocumentTypeTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn docCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn docTypeDataGridViewTextBoxColumn;
    }
}