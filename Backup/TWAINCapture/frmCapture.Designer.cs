namespace TWAINCapture
{
    partial class frmCapture
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCapture));
            this.Label8 = new System.Windows.Forms.Label();
            this.GroupBox8 = new System.Windows.Forms.GroupBox();
            this.btncapture = new System.Windows.Forms.Button();
            this.GroupBox7 = new System.Windows.Forms.GroupBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.GroupBox6 = new System.Windows.Forms.GroupBox();
            this.treealfresco = new System.Windows.Forms.TreeView();
            this.GroupBox5 = new System.Windows.Forms.GroupBox();
            this.chkSinglePage = new System.Windows.Forms.CheckBox();
            this.useAdfCheckBox = new System.Windows.Forms.CheckBox();
            this.useUICheckBox = new System.Windows.Forms.CheckBox();
            this.GroupBox4 = new System.Windows.Forms.GroupBox();
            this.btnselectdevice = new System.Windows.Forms.Button();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.rdtiff = new System.Windows.Forms.RadioButton();
            this.rdgif = new System.Windows.Forms.RadioButton();
            this.rdjpeg = new System.Windows.Forms.RadioButton();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.rdothers = new System.Windows.Forms.RadioButton();
            this.rdVideo = new System.Windows.Forms.RadioButton();
            this.rdCamera = new System.Windows.Forms.RadioButton();
            this.rdscanner = new System.Windows.Forms.RadioButton();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.lblDriver = new System.Windows.Forms.Label();
            this.lblMfg = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblWIA = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.devicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectADeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutCapturescoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blackAndWhiteCheckBox = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.GroupBox8.SuspendLayout();
            this.GroupBox7.SuspendLayout();
            this.GroupBox6.SuspendLayout();
            this.GroupBox5.SuspendLayout();
            this.GroupBox4.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label8
            // 
            resources.ApplyResources(this.Label8, "Label8");
            this.Label8.Name = "Label8";
            // 
            // GroupBox8
            // 
            this.GroupBox8.Controls.Add(this.btncapture);
            resources.ApplyResources(this.GroupBox8, "GroupBox8");
            this.GroupBox8.Name = "GroupBox8";
            this.GroupBox8.TabStop = false;
            // 
            // btncapture
            // 
            resources.ApplyResources(this.btncapture, "btncapture");
            this.btncapture.Name = "btncapture";
            this.btncapture.UseVisualStyleBackColor = true;
            this.btncapture.Click += new System.EventHandler(this.btncapture_Click);
            // 
            // GroupBox7
            // 
            this.GroupBox7.Controls.Add(this.blackAndWhiteCheckBox);
            this.GroupBox7.Controls.Add(this.Label6);
            this.GroupBox7.Controls.Add(this.txtPrefix);
            resources.ApplyResources(this.GroupBox7, "GroupBox7");
            this.GroupBox7.Name = "GroupBox7";
            this.GroupBox7.TabStop = false;
            // 
            // Label6
            // 
            resources.ApplyResources(this.Label6, "Label6");
            this.Label6.Name = "Label6";
            // 
            // txtPrefix
            // 
            resources.ApplyResources(this.txtPrefix, "txtPrefix");
            this.txtPrefix.Name = "txtPrefix";
            // 
            // GroupBox6
            // 
            this.GroupBox6.Controls.Add(this.treealfresco);
            resources.ApplyResources(this.GroupBox6, "GroupBox6");
            this.GroupBox6.Name = "GroupBox6";
            this.GroupBox6.TabStop = false;
            // 
            // treealfresco
            // 
            resources.ApplyResources(this.treealfresco, "treealfresco");
            this.treealfresco.Name = "treealfresco";
            this.treealfresco.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treealfresco_AfterSelect);
            // 
            // GroupBox5
            // 
            this.GroupBox5.Controls.Add(this.chkSinglePage);
            this.GroupBox5.Controls.Add(this.useAdfCheckBox);
            this.GroupBox5.Controls.Add(this.useUICheckBox);
            resources.ApplyResources(this.GroupBox5, "GroupBox5");
            this.GroupBox5.Name = "GroupBox5";
            this.GroupBox5.TabStop = false;
            // 
            // chkSinglePage
            // 
            resources.ApplyResources(this.chkSinglePage, "chkSinglePage");
            this.chkSinglePage.Name = "chkSinglePage";
            this.chkSinglePage.UseVisualStyleBackColor = true;
            this.chkSinglePage.CheckedChanged += new System.EventHandler(this.chkSinglePage_CheckedChanged);
            // 
            // useAdfCheckBox
            // 
            resources.ApplyResources(this.useAdfCheckBox, "useAdfCheckBox");
            this.useAdfCheckBox.Name = "useAdfCheckBox";
            this.useAdfCheckBox.UseVisualStyleBackColor = true;
            this.useAdfCheckBox.CheckedChanged += new System.EventHandler(this.useAdfCheckBox_CheckedChanged);
            // 
            // useUICheckBox
            // 
            resources.ApplyResources(this.useUICheckBox, "useUICheckBox");
            this.useUICheckBox.Name = "useUICheckBox";
            this.useUICheckBox.UseVisualStyleBackColor = true;
            // 
            // GroupBox4
            // 
            this.GroupBox4.Controls.Add(this.btnselectdevice);
            resources.ApplyResources(this.GroupBox4, "GroupBox4");
            this.GroupBox4.Name = "GroupBox4";
            this.GroupBox4.TabStop = false;
            // 
            // btnselectdevice
            // 
            resources.ApplyResources(this.btnselectdevice, "btnselectdevice");
            this.btnselectdevice.Name = "btnselectdevice";
            this.btnselectdevice.UseVisualStyleBackColor = true;
            this.btnselectdevice.Click += new System.EventHandler(this.btnselectdevice_Click);
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.rdtiff);
            this.GroupBox3.Controls.Add(this.rdgif);
            this.GroupBox3.Controls.Add(this.rdjpeg);
            resources.ApplyResources(this.GroupBox3, "GroupBox3");
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.TabStop = false;
            // 
            // rdtiff
            // 
            resources.ApplyResources(this.rdtiff, "rdtiff");
            this.rdtiff.Name = "rdtiff";
            this.rdtiff.UseVisualStyleBackColor = true;
            this.rdtiff.CheckedChanged += new System.EventHandler(this.rdtiff_CheckedChanged);
            // 
            // rdgif
            // 
            resources.ApplyResources(this.rdgif, "rdgif");
            this.rdgif.Name = "rdgif";
            this.rdgif.UseVisualStyleBackColor = true;
            this.rdgif.CheckedChanged += new System.EventHandler(this.rdgif_CheckedChanged);
            // 
            // rdjpeg
            // 
            resources.ApplyResources(this.rdjpeg, "rdjpeg");
            this.rdjpeg.Checked = true;
            this.rdjpeg.Name = "rdjpeg";
            this.rdjpeg.TabStop = true;
            this.rdjpeg.UseVisualStyleBackColor = true;
            this.rdjpeg.CheckedChanged += new System.EventHandler(this.rdjpeg_CheckedChanged);
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.rdothers);
            this.GroupBox2.Controls.Add(this.rdVideo);
            this.GroupBox2.Controls.Add(this.rdCamera);
            this.GroupBox2.Controls.Add(this.rdscanner);
            resources.ApplyResources(this.GroupBox2, "GroupBox2");
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.TabStop = false;
            // 
            // rdothers
            // 
            resources.ApplyResources(this.rdothers, "rdothers");
            this.rdothers.Name = "rdothers";
            this.rdothers.UseVisualStyleBackColor = true;
            this.rdothers.CheckedChanged += new System.EventHandler(this.rdothers_CheckedChanged);
            // 
            // rdVideo
            // 
            resources.ApplyResources(this.rdVideo, "rdVideo");
            this.rdVideo.Name = "rdVideo";
            this.rdVideo.UseVisualStyleBackColor = true;
            this.rdVideo.CheckedChanged += new System.EventHandler(this.rdVideo_CheckedChanged);
            // 
            // rdCamera
            // 
            resources.ApplyResources(this.rdCamera, "rdCamera");
            this.rdCamera.Name = "rdCamera";
            this.rdCamera.UseVisualStyleBackColor = true;
            this.rdCamera.CheckedChanged += new System.EventHandler(this.rdCamera_CheckedChanged);
            // 
            // rdscanner
            // 
            resources.ApplyResources(this.rdscanner, "rdscanner");
            this.rdscanner.Name = "rdscanner";
            this.rdscanner.UseVisualStyleBackColor = true;
            this.rdscanner.CheckedChanged += new System.EventHandler(this.rdscanner_CheckedChanged);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.lblDriver);
            this.GroupBox1.Controls.Add(this.lblMfg);
            this.GroupBox1.Controls.Add(this.lblDesc);
            this.GroupBox1.Controls.Add(this.lblName);
            this.GroupBox1.Controls.Add(this.lblWIA);
            this.GroupBox1.Controls.Add(this.Label5);
            this.GroupBox1.Controls.Add(this.Label2);
            this.GroupBox1.Controls.Add(this.Label4);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Controls.Add(this.Label3);
            resources.ApplyResources(this.GroupBox1, "GroupBox1");
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.TabStop = false;
            // 
            // lblDriver
            // 
            resources.ApplyResources(this.lblDriver, "lblDriver");
            this.lblDriver.Name = "lblDriver";
            // 
            // lblMfg
            // 
            resources.ApplyResources(this.lblMfg, "lblMfg");
            this.lblMfg.Name = "lblMfg";
            // 
            // lblDesc
            // 
            resources.ApplyResources(this.lblDesc, "lblDesc");
            this.lblDesc.Name = "lblDesc";
            // 
            // lblName
            // 
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Name = "lblName";
            // 
            // lblWIA
            // 
            resources.ApplyResources(this.lblWIA, "lblWIA");
            this.lblWIA.Name = "lblWIA";
            // 
            // Label5
            // 
            resources.ApplyResources(this.Label5, "Label5");
            this.Label5.Name = "Label5";
            // 
            // Label2
            // 
            resources.ApplyResources(this.Label2, "Label2");
            this.Label2.Name = "Label2";
            // 
            // Label4
            // 
            resources.ApplyResources(this.Label4, "Label4");
            this.Label4.Name = "Label4";
            // 
            // Label1
            // 
            resources.ApplyResources(this.Label1, "Label1");
            this.Label1.Name = "Label1";
            // 
            // Label3
            // 
            resources.ApplyResources(this.Label3, "Label3");
            this.Label3.Name = "Label3";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.devicesToolStripMenuItem,
            this.helpToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // devicesToolStripMenuItem
            // 
            this.devicesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectADeviceToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.devicesToolStripMenuItem.Name = "devicesToolStripMenuItem";
            resources.ApplyResources(this.devicesToolStripMenuItem, "devicesToolStripMenuItem");
            // 
            // selectADeviceToolStripMenuItem
            // 
            resources.ApplyResources(this.selectADeviceToolStripMenuItem, "selectADeviceToolStripMenuItem");
            this.selectADeviceToolStripMenuItem.Name = "selectADeviceToolStripMenuItem";
            this.selectADeviceToolStripMenuItem.Click += new System.EventHandler(this.selectADeviceToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1,
            this.aboutCapturescoToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            resources.ApplyResources(this.helpToolStripMenuItem1, "helpToolStripMenuItem1");
            // 
            // aboutCapturescoToolStripMenuItem
            // 
            this.aboutCapturescoToolStripMenuItem.Name = "aboutCapturescoToolStripMenuItem";
            resources.ApplyResources(this.aboutCapturescoToolStripMenuItem, "aboutCapturescoToolStripMenuItem");
            this.aboutCapturescoToolStripMenuItem.Click += new System.EventHandler(this.aboutCapturescoToolStripMenuItem_Click);
            // 
            // blackAndWhiteCheckBox
            // 
            resources.ApplyResources(this.blackAndWhiteCheckBox, "blackAndWhiteCheckBox");
            this.blackAndWhiteCheckBox.Name = "blackAndWhiteCheckBox";
            this.blackAndWhiteCheckBox.UseVisualStyleBackColor = true;
            this.blackAndWhiteCheckBox.CheckedChanged += new System.EventHandler(this.blackAndWhiteCheckBox_CheckedChanged);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // frmCapture
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.GroupBox8);
            this.Controls.Add(this.GroupBox7);
            this.Controls.Add(this.GroupBox6);
            this.Controls.Add(this.GroupBox5);
            this.Controls.Add(this.GroupBox4);
            this.Controls.Add(this.GroupBox3);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmCapture";
            this.Load += new System.EventHandler(this.frmCapture_Load);
            this.GroupBox8.ResumeLayout(false);
            this.GroupBox7.ResumeLayout(false);
            this.GroupBox7.PerformLayout();
            this.GroupBox6.ResumeLayout(false);
            this.GroupBox5.ResumeLayout(false);
            this.GroupBox5.PerformLayout();
            this.GroupBox4.ResumeLayout(false);
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox3.PerformLayout();
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.GroupBox GroupBox8;
        internal System.Windows.Forms.Button btncapture;
        internal System.Windows.Forms.GroupBox GroupBox7;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.TextBox txtPrefix;
        internal System.Windows.Forms.GroupBox GroupBox6;
        internal System.Windows.Forms.TreeView treealfresco;
        internal System.Windows.Forms.GroupBox GroupBox5;
        internal System.Windows.Forms.CheckBox useAdfCheckBox;
        internal System.Windows.Forms.CheckBox useUICheckBox;
        internal System.Windows.Forms.GroupBox GroupBox4;
        internal System.Windows.Forms.Button btnselectdevice;
        internal System.Windows.Forms.GroupBox GroupBox3;
        internal System.Windows.Forms.RadioButton rdtiff;
        internal System.Windows.Forms.RadioButton rdgif;
        internal System.Windows.Forms.RadioButton rdjpeg;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.RadioButton rdothers;
        internal System.Windows.Forms.RadioButton rdVideo;
        internal System.Windows.Forms.RadioButton rdCamera;
        internal System.Windows.Forms.RadioButton rdscanner;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.Label lblDriver;
        internal System.Windows.Forms.Label lblMfg;
        internal System.Windows.Forms.Label lblDesc;
        internal System.Windows.Forms.Label lblName;
        internal System.Windows.Forms.Label lblWIA;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem devicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutCapturescoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectADeviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.CheckBox chkSinglePage;
        internal System.Windows.Forms.CheckBox blackAndWhiteCheckBox;
        private System.Windows.Forms.Label label7;
    }
}