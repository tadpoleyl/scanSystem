namespace ScanSystem
{
    partial class frmTransportList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTransportList));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.picBox_clearAll = new System.Windows.Forms.PictureBox();
            this.picbox_noBill = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.picClose = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dgvNowUpdate = new System.Windows.Forms.DataGridView();
            this.dgvSuccess = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.companyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileAdd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_clearAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbox_noBill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNowUpdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSuccess)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("SimSun", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(138)))), ((int)(((byte)(226)))));
            this.label1.Location = new System.Drawing.Point(60, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 24);
            this.label1.TabIndex = 14;
            this.label1.Text = "正在上传（0）";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label2.Font = new System.Drawing.Font("SimSun", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.label2.Location = new System.Drawing.Point(352, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 24);
            this.label2.TabIndex = 15;
            this.label2.Text = "上传完成（0）";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // picBox_clearAll
            // 
            this.picBox_clearAll.BackColor = System.Drawing.Color.White;
            this.picBox_clearAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBox_clearAll.Image = global::ScanSystem.Properties.Resources.y_scan_clearAll;
            this.picBox_clearAll.Location = new System.Drawing.Point(442, 453);
            this.picBox_clearAll.Name = "picBox_clearAll";
            this.picBox_clearAll.Size = new System.Drawing.Size(160, 32);
            this.picBox_clearAll.TabIndex = 17;
            this.picBox_clearAll.TabStop = false;
            this.picBox_clearAll.Visible = false;
            this.picBox_clearAll.Click += new System.EventHandler(this.picBox_clearAll_Click);
            // 
            // picbox_noBill
            // 
            this.picbox_noBill.BackColor = System.Drawing.Color.White;
            this.picbox_noBill.Image = global::ScanSystem.Properties.Resources.y_scan_noBill;
            this.picbox_noBill.Location = new System.Drawing.Point(191, 216);
            this.picbox_noBill.Name = "picbox_noBill";
            this.picbox_noBill.Size = new System.Drawing.Size(194, 108);
            this.picbox_noBill.TabIndex = 16;
            this.picbox_noBill.TabStop = false;
            this.picbox_noBill.Visible = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::ScanSystem.Properties.Resources.y_scan_transport_list_line;
            this.pictureBox3.Location = new System.Drawing.Point(307, 114);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(298, 4);
            this.pictureBox3.TabIndex = 13;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::ScanSystem.Properties.Resources.y_scan_transport_list_line;
            this.pictureBox2.Location = new System.Drawing.Point(9, 114);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(298, 4);
            this.pictureBox2.TabIndex = 12;
            this.pictureBox2.TabStop = false;
            // 
            // picClose
            // 
            this.picClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(138)))), ((int)(((byte)(226)))));
            this.picClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picClose.Image = global::ScanSystem.Properties.Resources.y_scanClose;
            this.picClose.Location = new System.Drawing.Point(573, 22);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(16, 16);
            this.picClose.TabIndex = 11;
            this.picClose.TabStop = false;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ScanSystem.Properties.Resources.y_scan_transport_lists;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(614, 499);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // dgvNowUpdate
            // 
            this.dgvNowUpdate.AllowUserToAddRows = false;
            this.dgvNowUpdate.AllowUserToDeleteRows = false;
            this.dgvNowUpdate.AllowUserToResizeColumns = false;
            this.dgvNowUpdate.AllowUserToResizeRows = false;
            this.dgvNowUpdate.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNowUpdate.BackgroundColor = System.Drawing.Color.White;
            this.dgvNowUpdate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNowUpdate.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvNowUpdate.ColumnHeadersHeight = 40;
            this.dgvNowUpdate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvNowUpdate.ColumnHeadersVisible = false;
            this.dgvNowUpdate.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fileName,
            this.companyName,
            this.fileAdd});
            this.dgvNowUpdate.Location = new System.Drawing.Point(12, 118);
            this.dgvNowUpdate.MultiSelect = false;
            this.dgvNowUpdate.Name = "dgvNowUpdate";
            this.dgvNowUpdate.ReadOnly = true;
            this.dgvNowUpdate.RowHeadersVisible = false;
            this.dgvNowUpdate.RowTemplate.Height = 23;
            this.dgvNowUpdate.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNowUpdate.Size = new System.Drawing.Size(591, 314);
            this.dgvNowUpdate.TabIndex = 18;
            // 
            // dgvSuccess
            // 
            this.dgvSuccess.AllowUserToAddRows = false;
            this.dgvSuccess.AllowUserToDeleteRows = false;
            this.dgvSuccess.AllowUserToResizeColumns = false;
            this.dgvSuccess.AllowUserToResizeRows = false;
            this.dgvSuccess.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSuccess.BackgroundColor = System.Drawing.Color.White;
            this.dgvSuccess.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSuccess.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvSuccess.ColumnHeadersHeight = 40;
            this.dgvSuccess.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvSuccess.ColumnHeadersVisible = false;
            this.dgvSuccess.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dgvSuccess.Location = new System.Drawing.Point(11, 119);
            this.dgvSuccess.MultiSelect = false;
            this.dgvSuccess.Name = "dgvSuccess";
            this.dgvSuccess.ReadOnly = true;
            this.dgvSuccess.RowHeadersVisible = false;
            this.dgvSuccess.RowTemplate.Height = 23;
            this.dgvSuccess.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSuccess.Size = new System.Drawing.Size(592, 314);
            this.dgvSuccess.TabIndex = 19;
            this.dgvSuccess.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "CompanyName";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn1.FillWeight = 239.7897F;
            this.dataGridViewTextBoxColumn1.HeaderText = "文件名称";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "fileStatus";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn2.FillWeight = 75.13412F;
            this.dataGridViewTextBoxColumn2.HeaderText = "状态";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "fileAdd";
            this.dataGridViewTextBoxColumn3.HeaderText = "文件地址";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Visible = false;
            // 
            // fileName
            // 
            this.fileName.DataPropertyName = "CompanyName";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.fileName.DefaultCellStyle = dataGridViewCellStyle2;
            this.fileName.FillWeight = 187.6498F;
            this.fileName.HeaderText = "文件名称";
            this.fileName.Name = "fileName";
            this.fileName.ReadOnly = true;
            this.fileName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // companyName
            // 
            this.companyName.DataPropertyName = "fileStatus";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.companyName.DefaultCellStyle = dataGridViewCellStyle3;
            this.companyName.FillWeight = 58.79694F;
            this.companyName.HeaderText = "状态";
            this.companyName.Name = "companyName";
            this.companyName.ReadOnly = true;
            this.companyName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // fileAdd
            // 
            this.fileAdd.DataPropertyName = "fileAdd";
            this.fileAdd.HeaderText = "文件地址";
            this.fileAdd.Name = "fileAdd";
            this.fileAdd.ReadOnly = true;
            this.fileAdd.Visible = false;
            // 
            // frmTransportList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 499);
            this.Controls.Add(this.dgvSuccess);
            this.Controls.Add(this.dgvNowUpdate);
            this.Controls.Add(this.picBox_clearAll);
            this.Controls.Add(this.picbox_noBill);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.picClose);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTransportList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmTransportList";
            this.Load += new System.EventHandler(this.frmTransportList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBox_clearAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbox_noBill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNowUpdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSuccess)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox picClose;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox picbox_noBill;
        private System.Windows.Forms.PictureBox picBox_clearAll;
        public System.Windows.Forms.DataGridView dgvNowUpdate;
        public System.Windows.Forms.DataGridView dgvSuccess;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn companyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileAdd;
    }
}