using Microsoft.International.Converters.PinYinConverter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using TwainDotNet;
using TwainDotNet.TwainNative;
using TwainDotNet.WinFroms;

namespace ScanSystem
{
    public partial class frmScanMain : Form
    {
        #region 参数
        private static AreaSettings AreaSettings = new AreaSettings(Units.Centimeters, 0.1f, 5.7f, 0.1F + 2.6f, 5.7f + 2.6f);
        Twain _twain;
        ScanSettings _settings;//扫描配置
        int scanImgNum = 0;//票据的起始号
        public int allNum = 0;
        string companyId = string.Empty;//公司id
        string companyCode = string.Empty;//公司编码
        string companyNames = string.Empty;//公司名称
        string filePath = string.Empty;
        string companyYear = string.Empty;
        string companyMonth = string.Empty;
        DataTable dtUpdateFile =null;//需要上传文件的数据
        DataTable dtSuccess = null;
        string pathIni = Application.StartupPath + @"\config.ini";
        public string pathTxt = Application.StartupPath + @"\txtJson.txt";
        public string pathUpdateFile = Application.StartupPath + @"\txtupdate.txt";
        int CurrentIndex = -1;//当前行的索引 dgvCompany
        bool isScan = false;//是否安装了扫描仪驱动
        bool isStart = false;//是否正在扫描；false表示没有扫描，true：表示正在扫描中
        bool isClickUpdateBill = false;
        bool isScanNoUpdateBill = false;
        bool isScanBill = false;
        bool isFrmMian = false;
        bool isFrmNowUpdate = false;
        #endregion
        #region
        /// <summary>
        /// 公司信息
        /// </summary>
        public List<CompanyInfo> listCI = new List<CompanyInfo>();
        /// <summary>
        /// 手机号码
        /// </summary>
        public string telephone = string.Empty;
        /// <summary>
        /// 用户头像地址
        /// </summary>
        public string userPicUrl = string.Empty;
        /// <summary>
        /// 用户名称
        /// </summary>
        public string userName = string.Empty;
        /// <summary>
        /// 被退回的票据信息
        /// </summary>
        public List<ScanInfo> siList = new List<ScanInfo>();
        /// <summary>
        /// 重新扫描后 保存图片路径
        /// </summary>
        public string hzrdUploadsfilePath = Application.StartupPath.Split(':')[0] + ":\\hzrdUploads\\";

        public List<string> uploadfilePath = new List<string>();

        #endregion
        #region 常量
        public string[] tips = {"未选择扫描仪","已选择扫描仪","正在扫描","扫描完成","正在上传","上传完成"};
        #endregion
        #region ftp
        private string ftpServerIP = SetConfig.GetValue("ftpServerIP");//服务器ip
        private string ftpUserID = SetConfig.GetValue("ftpUserID");//用户名FTPTEST
        private string ftpPassword = SetConfig.GetValue("ftpPassword");//密码
        AsynchronousFtpUpLoader ftp = new AsynchronousFtpUpLoader();
        #endregion
        #region 主程序
        public frmScanMain()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            Thread.Sleep(1000);
            #region 隔行变色 隐藏行头 图标
            dgvCompany.RowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dgvCompany.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dgvCompany.AdvancedCellBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            dgvCompany.AdvancedCellBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
            dgvCompany.AdvancedCellBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
            dgvCompany.AdvancedCellBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single;
            #endregion
            imgScanList.ImageSize = new Size(215, 127);
            imageListView.MultiSelect = false;
        }
        private void frmScanMain_Load(object sender, EventArgs e)
        {

            this.ShowInTaskbar = true;//在任务栏显示图标
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;//去掉窗体边框
            this.panelScanPic.HorizontalScroll.Visible = false;//横的
            this.panelScanPic.VerticalScroll.Visible = true;//竖的
            this.label1.Text = "欢迎您," + userName + "!";
            #region 公司信息

            #region 需要上传的文件
            dtUpdateFile = new DataTable();
            DataColumn dcCompanyName = dtUpdateFile.Columns.Add("CompanyName", Type.GetType("System.String"));
            DataColumn dcFileStatus = dtUpdateFile.Columns.Add("fileStatus", Type.GetType("System.String"));
            DataColumn dcFileAdd = dtUpdateFile.Columns.Add("fileAdd", Type.GetType("System.String"));
            dtSuccess = dtUpdateFile.Clone();
            #endregion

            #region 检查上传列表中是否有未上传的图片
            if (!File.Exists(pathTxt))
            {
                FileStream fs = File.Create(pathTxt);
                fs.Close();
            }

            if (!File.Exists(pathUpdateFile))
            {
                FileStream fs = File.Create(pathUpdateFile);
                fs.Close();
            }

            StringBuilder strUpdate = new StringBuilder();
            strUpdate.Append(ReadTxt(pathUpdateFile));


            this.label4.Text = SetConfig.GetValue("todayBillNum") + "张";
            
            StringBuilder str = new StringBuilder();
            string strReadtxt = ReadTxt(pathTxt);
            str.Append(strReadtxt);
            if (!string.IsNullOrEmpty(str.ToString()))
            {
                List<string> txtStr = str.ToString().TrimEnd('|').Split('|').ToList();
                DataRow dr = null;
                List<string> updateBillFilePath = new List<string>();
                txtStr.ForEach(x =>
                {
                    var txtArr = x.Split('$');
                    if (File.Exists(txtArr[1]))
                    {
                        dr = dtUpdateFile.NewRow();
                        dr["CompanyName"] = txtArr[0];
                        dr["fileStatus"] = "等待上传";//文件上传状态
                        dr["fileAdd"] = txtArr[1];
                        dtUpdateFile.Rows.Add(dr);
                        uploadfilePath.Add(txtArr[1]);
                        updateBillFilePath.Add(txtArr[1]);
                    }
                    else
                    {
                        allNum++;
                        strUpdate.Append(txtArr[0] + "$" + txtArr[1] + "|");
                        str.Replace(txtArr[0] + "$" + txtArr[1] + "|", "");
                    }
                });
                WirteTxt(str.ToString(),pathTxt);

                //isFrmNowUpdate = true;
                int txtNum = Convert.ToInt32(this.label4.Text.Replace("张", ""));
                successNum = txtNum;
                //int updateBullNums = updateBillFilePath.Count;

                allNum += txtNum + updateBillFilePath.Count;
                this.lblStatus.Text = tips[4] + "（ " + txtNum.ToString() + " / " + allNum.ToString() + "）";
                
                Thread th = new Thread(new ParameterizedThreadStart(ftp.UpLoad));
                th.Start(new object[] { ftpServerIP, ftpUserID, ftpPassword, updateBillFilePath });
                
                this.timer1.Enabled = true;
            }
            WirteTxt(strUpdate.ToString(), pathUpdateFile);
            if (!string.IsNullOrEmpty(strUpdate.ToString()))
            {
                List<string> txtStr = strUpdate.ToString().TrimEnd('|').Split('|').ToList();
                DataRow dr = null;
                txtStr.ForEach(x =>
                {
                    var txtArr = x.Split('$');
                    dr = dtSuccess.NewRow();
                    dr["CompanyName"] = txtArr[0];
                    dr["fileStatus"] = "传输完成";//文件上传状态
                    dr["fileAdd"] = txtArr[1];
                    dtSuccess.Rows.Add(dr);
                });
            }

            #endregion
            //创建ini文件
            if (!File.Exists(pathIni))
            {
                FileStream fs = File.Create(pathIni);
                fs.Dispose();
            }
            Ini ini = new Ini(pathIni);
            #region
            listCI.ForEach(x =>
            {
                x.Status = 0;
               string dYear = x.DtTime.Substring(0, 4);
               int dMonth = Convert.ToInt32(x.DtTime.Substring(4, 2));
               string paths = Application.StartupPath + @"\scanFile\" + Code(x.Code) + @"\" + dYear.ToString() + (dMonth < 10 ? ("0" + dMonth.ToString()) : dMonth.ToString()) + @"\";
               if (Directory.Exists(paths))
               {
                   string[] files = Directory.GetFiles(paths);
                   var len=files.Count();
                   if (len > 0) {
                       for (int i = 0; i < len; i++)
                       {
                           if (files[i].IndexOf("success") < 0 && uploadfilePath.IndexOf(files[i]) < 0)
                           {
                               x.Status = 1;
                               isScanNoUpdateBill = true;
                               return;
                           }
                       }
                   }
               }
                string key = x.Id;
                string value = x.Num.ToString();
                string num = ini.ReadString("setting", key, string.Empty);
                if (string.IsNullOrEmpty(num))//没有key的时候，写入
                {
                    ini.WriteString("setting", key, value);
                }
                else//如果有key
                {
                    if (!string.IsNullOrEmpty(x.Num))
                    {
                        if (Convert.ToInt32(num) <= Convert.ToInt32(x.Num))
                        {
                            ini.WriteString("setting", key, x.Num.ToString());
                        }
                    }
                    else
                    {
                        ini.WriteString("setting", key, "0");
                    }
                }
            });
            this.lbl_customerNum.Text = listCI.Count().ToString() + "家";
            this.dgvCompany.AutoGenerateColumns = false;
            dgvCompany.DataSource = listCI;
            #endregion
            #endregion
            #region 是否已选择扫描仪
            string nameScanSource = SetConfig.GetValue("ScanDrive");
            if (!string.IsNullOrEmpty(nameScanSource))
            {
                this.lblStatus.Text = tips[1];
            }
            else
            {
                this.lblStatus.Text = tips[0];
                isScan = true;
            }
            #endregion
        }

        private void ScanBill()
        {
            #region 扫描
            try
            {
                isScanBill = true;
                isScan = true;
                #region 扫描
                _twain = new Twain(new WinFormsWindowMessageHook(this));
                _twain.TransferImage += delegate(Object senders, TransferImageEventArgs args)
                {
                    if (args.Image != null)
                    {
                        if (!string.IsNullOrEmpty(reScanImgName))//重新扫描票据
                        {
                            //保存到指定的文件夹中
                            args.Image.Save(reScanImgName, System.Drawing.Imaging.ImageFormat.Jpeg);
                            args.Image.Dispose();
                        }
                        else
                        {
                            var lblText = UpdateFile(args.Image);//保存到本地
                            setScanImage(lblText, args.Image, scanImgNum);
                            scanImgNum++;
                        }
                    }
                };
                _twain.ScanningComplete += delegate
                {
                    isStart = false;
                    this.lblStatus.Text = tips[3];
                    setData();
                };
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #endregion
        }

        private void setData()
        {
            Ini ini = new Ini(pathIni);
            if (!string.IsNullOrEmpty(reScanImgName))//重新扫描
            {
                if (!string.IsNullOrEmpty(imgPath))
                {
                    #region
                    Image imgFiles = Image.FromFile(imgPath);
                    Bitmap bgBMPs = new Bitmap(215,127);
                    if (bgBMPs != null)
                    {
                        Graphics gs = Graphics.FromImage(bgBMPs);
                        gs.DrawImage(imgFiles, new Rectangle(0, 0, 215, 127));
                        imgFiles.Dispose();
                        if (bgBMPs != null && imgScanList != null && imgScanList.Images != null)
                        {
                            imgScanList.Images[imageIndex] = bgBMPs;
                            bgBMPs.Dispose();
                            imageListView.Items[index].ImageIndex = imageIndex;
                            imageListView.Update();
                        }
                    }
                    #endregion
                }
                reScanImgName = string.Empty;
            }
            setStatus(1);
            ini.WriteString("setting", companyId, scanImgNum.ToString());
            Enabled = true;
            isScanNoUpdateBill = true;
            if (isClickUpdateBill && dtUpdateFile.Rows.Count > 0)
            {
                this.timer2.Enabled = true;
            }
        }
        /// <summary>
        /// status 0:表示已上传；1:表示扫描未上传
        /// </summary>
        /// <param name="status"></param>
        private void setStatus(int status)
        {
            if (CurrentIndex != -1)
            {
                dgvCompany.Rows[CurrentIndex].Cells["status"].Value = status;
                listCI.ForEach(x =>
                {
                    if (x.Id == companyId)
                    {
                        x.Status = status;
                    }
                });
            }
        }

        #endregion
        #region 票据扫描与保存
        public void setScanImage(string lblText, Bitmap btpImg, int num)
        {
            #region
            imgScanList.Images.Add(btpImg);
            ListViewItem li = new ListViewItem();
            li.Text = "第" + Convert.ToInt32(lblText.Substring(lblText.Length - 4, 4)).ToString() + "张";
            li.ImageIndex = imgScanList.Images.Count - 1;
            li.Name = filePath + lblText + ".jpg";
            imageListView.Items.Add(li);
            #endregion
        }
        /// <summary>
        /// 扫描票据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScan_Click(object sender, EventArgs e)
        {
            if (!isStart)
            {
                if (!isChange)
                {
                    if (!string.IsNullOrEmpty(lblNowCompany.Text))
                    {
                        OpenFrmScan();
                    }
                    else
                    {
                        MessageBox.Show("请选择需要扫描的公司!");
                    }
                }
                else
                {
                    MessageBox.Show("加载图片中！！");
                }
            }
            else
            {
                MessageBox.Show("正在扫描中");
            }
        }
        /// <summary>
        /// 扫描
        /// </summary>
        public void OpenFrmScan()
        {
            if (!string.IsNullOrEmpty(companyCode))
            {
                #region frmScan ShowDialog
                frmScan mediacapture = new frmScan();
                mediacapture.num = scanImgNum == 0 ? "1" : scanImgNum.ToString();
                mediacapture.ShowDialog();
                #endregion
                #region 开始扫描
                if (mediacapture.DialogResult == DialogResult.OK)
                {
                    string nameScanSource = SetConfig.GetValue("ScanDrive");
                    if (!string.IsNullOrEmpty(nameScanSource))
                    {
                        if (!isScanBill)
                        {
                            ScanBill();
                        }
                        #region
                        try
                        {
                            _twain.SetScanSource(nameScanSource);//设置默认的扫描仪驱动
                        }
                        catch (Exception ex)
                        {
                            isStart = false;
                            var meg = ex.Message;
                            Enabled = true;
                            MessageBox.Show("请查看是否连接扫描仪！！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        Enabled = false;
                        _settings = new ScanSettings();
                        _settings.UseDocumentFeeder = false;
                        _settings.ShowTwainUI = false;
                        _settings.ShowProgressIndicatorUI = false;
                        _settings.UseDuplex = false;
                        _settings.Resolution = false ? ResolutionSettings.Fax : ResolutionSettings.ColourPhotocopier;
                        _settings.Area = !false ? null : AreaSettings;
                        _settings.ShouldTransferAllPages = true;
                        _settings.Rotation = new RotationSettings()
                        {
                            AutomaticRotate = false,
                            AutomaticBorderDetection = false
                        };
                        try
                        {
                            _twain.StartScanning(_settings);
                            #region 创建相应的文件
                            if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
                            #endregion

                            this.lblStatus.Text = tips[2];
                            scanImgNum = Convert.ToInt32(mediacapture.txtNum.Text);
                            isStart = true;


                        }
                        catch (TwainException ex)
                        {
                            MessageBox.Show(ex.Message);
                            Enabled = true;
                            isStart = false;
                            return;
                        }
                        #endregion
                    }
                    else
                    {
                        MessageBox.Show("请选择扫描仪");
                    }
                }
                #endregion
            }
            else
            {
                MessageBox.Show("请选择公司信息");
            }
        }
        /// <summary>
        /// 票据上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!isStart)
            {
                updateBillList();
            }
            else {
                MessageBox.Show("正在扫描中");
            }
        }
        /// <summary>
        /// 票据添加到上传列表中
        /// </summary>
        private void updateBillList()
        {
            this.timer1.Enabled = false;
            isScanNoUpdateBill = false;
            #region
            if (Directory.Exists(filePath))
            {
                string[] files = Directory.GetFiles(filePath);
                DataRow dr = null;
                int len = files.Count();
                StringBuilder str = new StringBuilder();
                if (len > 0)
                {
                    isClickUpdateBill = true;
                    str.Append(ReadTxt(pathTxt));
                    List<string> needUpdateFile = new List<string>();
                    for (int i = 0; i < len; i++)
                    {
                        if (files[i].IndexOf("success") < 0)
                        {
                            FileInfo fi = new FileInfo(files[i]);
                            if (uploadfilePath.IndexOf(filePath + fi.Name) < 0)
                            {
                                dr = dtUpdateFile.NewRow();
                                var dName = fi.Name.Split('.')[0];
                                var comName = lblNowCompany.Text + "第" + Convert.ToInt32(dName.Substring(dName.Length - 4, 4)).ToString() + "张";//公司名称
                                dr["CompanyName"] = comName;
                                dr["fileStatus"] = "等待上传";//文件上传状态
                                dr["fileAdd"] = files[i];
                                if (str.ToString().IndexOf(comName + "$" + files[i] + "|") < 0)
                                {
                                    str.Append(comName + "$" + files[i] + "|");
                                }
                                dtUpdateFile.Rows.Add(dr);
                                uploadfilePath.Add(filePath + fi.Name);
                                needUpdateFile.Add(filePath + fi.Name);
                            }
                        }
                    }
                    WirteTxt(str.ToString(), pathTxt);
                    setStatus(0);
                    frmTips tips = new frmTips();
                    tips.ShowDialog();
                    int txtNum = Convert.ToInt32(this.label4.Text.Replace("张", ""));
                    allNum = allNum + needUpdateFile.Count;

                    this.lblStatus.Text = this.tips[4] + "（" + txtNum + " / " + allNum.ToString() + "）";
                    if (needUpdateFile.Count() > 0)
                    {
                        Thread th = new Thread(new ParameterizedThreadStart(ftp.UpLoad));
                        th.Start(new object[] { ftpServerIP, ftpUserID, ftpPassword, needUpdateFile });
                    }
                    this.timer1.Enabled = true;
                }
                else
                {
                    MessageBox.Show("没有需要传输的文件！");
                }
            }
            else
            {
                MessageBox.Show("没有需要传输的文件！");
            }
            #endregion
        }

        public void WirteTxt(string json,string path) {
            StreamWriter sw = new StreamWriter(path, false);
            sw.WriteLine(json);
            sw.Close();
        }

        public string ReadTxt(string path)
        {
            string str = "";
            StreamReader sr = new StreamReader(path, false);
            str = sr.ReadLine();
            sr.Close();
            return str;
        }
        #endregion
        #region 扫描后 保存到指定目录中
        /// <summary>
        /// 上传
        /// </summary>
        public string UpdateFile(Bitmap btp)
        {
            string textNum = Code(companyCode) + companyYear + companyMonth + (scanImgNum < 10 ? "000" + scanImgNum.ToString() : scanImgNum < 100 ? "00" + scanImgNum.ToString() : scanImgNum < 1000 ? "0" + scanImgNum.ToString() : scanImgNum.ToString());
            btp.Save(filePath + textNum + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            return textNum;
        }
        #endregion
        #region 退出整个程序
        /// <summary>
        /// 退出整个应用程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picClose_Click(object sender, EventArgs e)
        {
            if (!isStart)
            {
                if (dtUpdateFile.Rows.Count > 0)
                {
                    CloseFrm();
                }
                else if (isScanNoUpdateBill)
                {
                    DialogResult dr = MessageBox.Show("您有扫描的数据未上传！您确认退出登录吗？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    this.Dispose();
                    if (dr == DialogResult.OK)
                    {
                        System.Environment.Exit(0);
                    }
                }
                else
                {
                    System.Environment.Exit(0);
                }
            }
            else
            {
                MessageBox.Show("正在扫描中");
            }
        }

        private void CloseFrm()
        {
            frmExitLogin frmex = new frmExitLogin();
            frmex.ShowDialog();
            if (frmex.DialogResult == DialogResult.Yes)//后台上传
            {
                isFrmMian = true;
                this.Hide();
            }
        }

        #endregion
        #region 公司列表 行点击事件
        Thread th1 = null;
        Mutex mUnique = new Mutex();
        bool isChange = false;
        string currentCompanyCode = string.Empty;
        /// <summary>
        /// 公司列表 行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCompany_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!isStart)
            {
                #region
                if (currentCompanyCode != dgvCompany.Rows[e.RowIndex].Cells[2].Value.ToString())
                {
                    if (!isChange)
                    {
                        if (CurrentIndex == -1)
                        {
                            dgvCompanyCellClick(e);
                        }
                        else if (Convert.ToInt32(dgvCompany.Rows[CurrentIndex].Cells["status"].Value) == 0)//表示扫描的文件都已经上传或者已经在上传列表中
                        {
                            dgvCompanyCellClick(e);
                        }
                        else//表示有文件未添加到上传列表中
                        {
                            frmTipUpBill tub = new frmTipUpBill();
                            tub.ShowDialog();
                            if (tub.DialogResult == DialogResult.OK)//确认上传
                            {
                                updateBillList();
                                dgvCompanyCellClick(e);
                            }
                            else if (tub.DialogResult == DialogResult.Cancel)
                            {
                                dgvCompany.Rows[CurrentIndex].Selected = true;
                                dgvCompany.CurrentCell = this.dgvCompany.Rows[CurrentIndex].Cells[0];
                            }
                            else {
                                dgvCompany.Rows[CurrentIndex].Selected = true;
                                dgvCompany.CurrentCell = this.dgvCompany.Rows[CurrentIndex].Cells[0];
                            }
                        }
                    }
                    else
                    {
                        dgvCompany.Rows[CurrentIndex].Selected = true;
                        dgvCompany.CurrentCell = this.dgvCompany.Rows[CurrentIndex].Cells[0];
                        MessageBox.Show("加载图片中！！");
                    }
                }
                #endregion
            }
            else
            {
                MessageBox.Show("正在扫描中");
            }
        }

        private void dgvCompanyCellClick(DataGridViewCellEventArgs e)
        {
            CurrentIndex = e.RowIndex;
            isChange = true;
            #region
            imageListView.Clear();
            lblNowCompany.Text = dgvCompany.Rows[e.RowIndex].Cells[0].Value.ToString();//公司名称
            companyNames = lblNowCompany.Text;
            companyId = dgvCompany.Rows[e.RowIndex].Cells[1].Value.ToString();//公司id
            companyCode = dgvCompany.Rows[e.RowIndex].Cells[2].Value.ToString();//公司编码
            currentCompanyCode = companyCode;
            string deDt = dgvCompany.Rows[e.RowIndex].Cells[3].Value.ToString();
            companyYear = deDt.Substring(0, 4);
            companyMonth = deDt.Substring(4, 2);
            int years = Convert.ToInt32(companyYear);
            int months = Convert.ToInt32(companyMonth);
            this.lbl_Account_period.Text = years.ToString() + "第" + months.ToString() + "期";
            //得到票据的起始号
            Ini ini = new Ini(pathIni);
            scanImgNum = ini.ReadInt("setting", companyId, 0);

            filePath = Application.StartupPath + @"\scanFile\" + Code(companyCode) + @"\" + years.ToString() + (months < 10 ? ("0" + months.ToString()) : months.ToString()) + @"\";
            if (Directory.Exists(filePath))
            {
                #region
                lock (this)
                {
                    th1 = new Thread(delegate(object param)
                    {
                        mUnique.WaitOne();
                        mUnique.ReleaseMutex();
                        setImageListView(filePath);
                    });
                    th1.Start();
                }
                #endregion
            }
            else
            {
                isChange = false;
            }
            #endregion
        }

        private void setImageListView(string filePath)
        {
            try
            {
                imgScanList.Images.Clear();
                imageListView.Items.Clear();
                string[] files = Directory.GetFiles(filePath);
                if (scanImgNum == 0)
                {
                    scanImgNum = files.Count() + 1;
                }
                for (int i = 0, len = files.Count(); i < len; i++)
                {
                    string[] strText = files[i].Substring(files[i].LastIndexOf(@"\") + 1, files[i].Length - files[i].LastIndexOf(@"\") - 1).Split('.');
                    string lblText = strText[0];
                    Image imgFile = Image.FromFile(files[i]);
                    Bitmap bgBMP = new Bitmap(215, 127);//imgFile.Width, imgFile.Height
                    if (bgBMP != null)
                    {
                        Graphics g = Graphics.FromImage(bgBMP);
                        g.DrawImage(imgFile, new Rectangle(0, 0, 215, 127));// imgFile.Width, imgFile.Height
                        g.Dispose();
                        imgFile.Dispose();
                        if (bgBMP != null && imgScanList != null && imgScanList.Images != null)
                        {
                            imgScanList.Images.Add(bgBMP);
                            bgBMP.Dispose();
                        }
                        ListViewItem li = new ListViewItem();
                        if (lblText.IndexOf("success") > -1)
                        {
                            lblText = lblText.Replace("-success", "");
                            li.Text = "第" + Convert.ToInt32(lblText.Substring(lblText.Length - 4, 4)).ToString() + "张  ✔";
                        }
                        else
                        {
                            li.Text = "第" + Convert.ToInt32(lblText.Substring(lblText.Length - 4, 4)).ToString() + "张";
                        }
                        li.ImageIndex = i;
                        li.Name = files[i];
                        li.ToolTipText = strText[1];
                        imageListView.BeginUpdate();
                        imageListView.Items.Add(li);
                        imageListView.EndUpdate();
                    }
                }
                isChange = false;
            }
            catch (Exception)
            {
                isChange = false;
                th1.Suspend();
                th1.Resume();
            }
        }

        public string Code(string code)
        {
            if (code.Length < 10)
            {
                for (int i = 0, len = code.Length; i < 10 - len; i++)
                {
                    code += "A";
                }
            }
            return code;
        }

        #endregion
        #region 搜索
        private void txtKey_TextChanged(object sender, EventArgs e)
        {
            if (!isStart)
            {
                if (!isChange)
                {
                    #region
                    if (txtKey.Focused)
                    {
                        imgScanList.Images.Clear();
                        imageListView.Items.Clear();
                        lblNowCompany.Text = "";
                        CurrentIndex = -1;
                        currentCompanyCode = string.Empty;
                        var txtName = txtKey.Text;
                        if (!string.IsNullOrEmpty(txtName))
                        {
                            dgvCompany.DataSource = listCI.Where(x => x.Name.Contains(txtName) || x.Code.Contains(txtName)).ToList();
                        }
                        else
                        {
                            dgvCompany.DataSource = listCI;
                        }
                        if ((dgvCompany.DataSource as List<CompanyInfo>).Count > 0)
                        {
                            dgvCompany.ClearSelection();
                        }
                    }
                    #endregion
                }
                else
                {
                    MessageBox.Show("加载图片中！！");
                }
            }
            else
            {
                MessageBox.Show("正在扫描中");
            }
        }
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (!isStart)
            {
                if (!isChange)
                {
                    #region
                    imgScanList.Images.Clear();
                    imageListView.Items.Clear();
                    lblNowCompany.Text = "";
                    currentCompanyCode = string.Empty;
                    CurrentIndex = -1;
                    var txtName = txtKey.Text;
                    if (!string.IsNullOrEmpty(txtName))
                    {
                        dgvCompany.DataSource = listCI.Where(x => x.Name.Contains(txtName) || x.Code.Contains(txtName)).ToList();
                    }
                    else
                    {
                        dgvCompany.DataSource = listCI;
                    }
                    if ((dgvCompany.DataSource as List<CompanyInfo>).Count > 0)
                    {
                        dgvCompany.ClearSelection();
                    }
                    #endregion
                }
                else
                {
                    MessageBox.Show("加载图片中！！");
                }
            }
            else
            {
                MessageBox.Show("正在扫描中");
            }
        }

        #endregion
        #region 窗体拖拽
        Point mouseOff;//鼠标移动位置变量
        bool leftFlag;//标签是否为左键
        private void panelHeader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y); //得到变量的值
                leftFlag = true;                  //点击左键按下时标注为true;
            }
        }

        private void panelHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }

        private void panelHeader_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;
            }
        }
        #endregion
        #region 最大化 最小化
        private void picMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void picMax_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }
        #endregion
        #region 清楚列表数据源
        public void clear()
        {
            this.dtUpdateFile = new DataTable();
        }
        #endregion
        #region 去掉默认选中
        private void ClearSelection(object sender, EventArgs e)
        {
            if ((dgvCompany.DataSource as List<CompanyInfo>) != null && (dgvCompany.DataSource as List<CompanyInfo>).Count > 0)
            {
                dgvCompany.ClearSelection();
            }
        }
        #endregion
        #region 右键菜单
        string imgPath = string.Empty;
        int imageIndex = -1;
        int index = -1;
        /// <summary>
        /// 右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageListView_MouseClick(object sender, MouseEventArgs e)
        {
            if (!isStart)
            {
                if (!isChange)
                {
                    #region
                    //禁止多选
                    imageListView.MultiSelect = false;
                    //鼠标右键
                    if (e.Button == MouseButtons.Right)
                    {
                        imgPath = ((System.Windows.Forms.ListView)(sender)).FocusedItem.Name;
                        if (imgPath.IndexOf("-success") > -1)//!File.Exists(imgPath) || 
                        {
                            re_scanPic.Visible = false;
                        }
                        else if (uploadfilePath.IndexOf(imgPath) > -1)
                        {
                            re_scanPic.Visible = false;
                            delPic.Visible = false;
                        }
                        else
                        {
                            re_scanPic.Visible = true;
                            delPic.Visible = true;
                        }
                        imageIndex = ((System.Windows.Forms.ListView)(sender)).FocusedItem.Index;
                        index = ((System.Windows.Forms.ListView)(sender)).FocusedItem.ImageIndex;
                        Point p = new Point(e.X, e.Y);
                        contextMenuStrip1.Show(imageListView, p);
                    }
                    #endregion
                }
                else {
                    MessageBox.Show("加载图片中！！");
                }
            }
            else
            {
                MessageBox.Show("正在扫描中");
            }
        }
        #endregion
        #region 查看图片

        [DllImport("kernel32.dll")]
        public static extern IntPtr _lopen(string lpPathName, int iReadWrite);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);

        public const int OF_READWRITE = 2;
        public const int OF_SHARE_DENY_NONE = 0x40;
        public readonly IntPtr HFILE_ERROR = new IntPtr(-1);  

        private void seePic_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(imgPath))
            {
                try
                {

                    IntPtr vHandle = _lopen(imgPath, OF_READWRITE | OF_SHARE_DENY_NONE);
                    if (vHandle == HFILE_ERROR)
                    {
                        MessageBox.Show("文件被占用,正在上传中");
                    }
                    else
                    {
                        if (!File.Exists(imgPath))
                        {
                            var paths = imgPath.Split('.');
                            imgPath = paths[0] + "-success." + paths[1];
                        }
                        Image imgFile = Image.FromFile(imgPath);
                        Bitmap bgBMP = new Bitmap(imgFile.Width, imgFile.Height);//797, 479 imgFile.Width, imgFile.Height
                        Graphics g = Graphics.FromImage(bgBMP);
                        g.DrawImage(imgFile, new Rectangle(0, 0, imgFile.Width, imgFile.Height));//imgFile.Width, imgFile.Height
                        g.Dispose();
                        imgFile.Dispose();

                        frmShowPic fsp = new frmShowPic(bgBMP, imgPath);
                        fsp.ShowDialog();
                        if (fsp.DialogResult == DialogResult.OK)//保存图片
                        {
                            if (!string.IsNullOrEmpty(imgPath))
                            {
                                bgBMP.Dispose();
                                Image imgFiles = Image.FromFile(imgPath);

                                Bitmap bgBMPs = new Bitmap(215, 127);//imgFiles.Width, imgFiles.Height
                                if (bgBMPs != null)
                                {
                                    Graphics gs = Graphics.FromImage(bgBMPs);
                                    gs.DrawImage(imgFiles, new Rectangle(0, 0, 215, 127));
                                    gs.Dispose();
                                    imgFiles.Dispose();
                                    if (bgBMPs != null && imgScanList != null && imgScanList.Images != null)
                                    {
                                        imgScanList.Images[imageIndex] = bgBMPs;
                                        bgBMPs.Dispose();
                                        imageListView.Items[index].ImageIndex = imageIndex;
                                        imageListView.Update();
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        /// <summary>
        /// 双击查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageListView_DoubleClick(object sender, EventArgs e)
        {
            if (!isStart)
            {
                if (!isChange)
                {
                    #region
                    imgPath = ((System.Windows.Forms.ListView)(sender)).FocusedItem.Name;
                    imageIndex = ((System.Windows.Forms.ListView)(sender)).FocusedItem.Index;
                    index = ((System.Windows.Forms.ListView)(sender)).FocusedItem.ImageIndex;
                    seePic_Click(sender, e);
                    #endregion
                }
                else
                {
                    MessageBox.Show("加载图片中！！");
                }
            }
            else
            {
                MessageBox.Show("正在扫描中");
            }
        }
        #endregion
        #region 删除
        private void delPic_Click(object sender, EventArgs e)
        {
            del frmdel = new del();
            frmdel.ShowDialog();
            if (frmdel.DialogResult == DialogResult.OK)//确认删除
            {
                if (!string.IsNullOrEmpty(imgPath))
                {
                    File.Delete(imgPath);
                    imageListView.Items.RemoveAt(imageIndex);
                    imgScanList.Images.RemoveAt(imageIndex);
                    for (int i = 0; i < imgScanList.Images.Count; i++)
                    {
                        imageListView.Items[i].ImageIndex = i;
                    }
                    imageListView.Update();
                    string[] files = Directory.GetFiles(filePath);
                    if (files.Count() == 0)
                    {
                        setStatus(0);
                        isScanNoUpdateBill = false;
                    }
                }
            }
        }
        #endregion
        #region 重新扫描
        string reScanImgName = string.Empty;
        /// <summary>
        /// 重新扫描
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void re_scanPic_Click(object sender, EventArgs e)
        {
            frmReScanPic frmrsp = new frmReScanPic();
            frmrsp.ShowDialog();
            if (frmrsp.DialogResult == DialogResult.OK)//确认
            {
                reScanImgName = imgPath;
                #region
                string nameScanSource = SetConfig.GetValue("ScanDrive");
                if (!string.IsNullOrEmpty(nameScanSource))
                {
                    #region
                    try
                    {
                        if (!isScanBill)
                        {
                            ScanBill();
                        }
                        _twain.SetScanSource(nameScanSource);//设置默认的扫描仪驱动
                    }
                    catch (Exception ex)
                    {
                        isStart = false;
                        var meg = ex.Message;
                        Enabled = true;
                        MessageBox.Show("请查看是否连接扫描仪！！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    Enabled = false;
                    _settings = new ScanSettings();
                    _settings.UseDocumentFeeder = false;
                    _settings.ShowTwainUI = false;
                    _settings.ShowProgressIndicatorUI = false;
                    _settings.UseDuplex = false;
                    _settings.Resolution = false ? ResolutionSettings.Fax : ResolutionSettings.ColourPhotocopier;
                    _settings.Area = !false ? null : AreaSettings;
                    _settings.ShouldTransferAllPages = true;
                    _settings.Rotation = new RotationSettings()
                    {
                        AutomaticRotate = false,
                        AutomaticBorderDetection = false
                    };
                    try
                    {
                        _twain.StartScanning(_settings);

                        this.lblStatus.Text = tips[2];
                        isStart = true;
                    }
                    catch (TwainException ex)
                    {
                        MessageBox.Show(ex.Message);
                        Enabled = true;
                        isStart = false;
                    }
                    #endregion
                }
                else
                {
                    MessageBox.Show("未选择扫描仪！");
                }
                #endregion
            }
        }
        #endregion
        #region 本地上传
        private void pictureBox13_Click(object sender, EventArgs e)
        {
            if (!isStart)
            {
                if (!isChange)
                {
                    #region
                    if (!string.IsNullOrEmpty(lblNowCompany.Text))
                    {
                        OpenFileDialog fileDialog = new OpenFileDialog();
                        fileDialog.Multiselect = true;
                        fileDialog.Title = "请选择文件";
                        fileDialog.Filter = "所有文件(*.*)|*.*";
                        if (fileDialog.ShowDialog() == DialogResult.OK)
                        {
                            string file = fileDialog.FileName;
                            if (!string.IsNullOrEmpty(file))
                            {
                                Image img = Image.FromFile(file);
                                if (scanImgNum == 0)
                                {
                                    scanImgNum++;
                                }
                                string textNum = Code(companyCode) + companyYear + companyMonth + (scanImgNum < 10 ? "000" + scanImgNum.ToString() : scanImgNum < 100 ? "00" + scanImgNum.ToString() : scanImgNum < 1000 ? "0" + scanImgNum.ToString() : scanImgNum.ToString());
                                #region 创建相应的文件
                                if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
                                
                                #endregion
                                img.Save(filePath + textNum + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                                imgScanList.Images.Add(img);
                                ListViewItem li = new ListViewItem();
                                li.Text = "第" + scanImgNum.ToString() + "张";
                                li.ImageIndex = imgScanList.Images.Count - 1;
                                li.Name = filePath + textNum + ".jpg";
                                imageListView.Items.Add(li);
                                imageListView.Update();
                                Ini ini = new Ini(pathIni);
                                scanImgNum++;
                                ini.WriteString("setting", companyId, scanImgNum.ToString());
                                setStatus(1);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("请选择公司!");
                    }
                    #endregion
                }
                else
                {
                    MessageBox.Show("加载图片中！！");
                }
            }
            else
            {
                MessageBox.Show("正在扫描中");
            }
        }
        #endregion
        #region 扫描仪设置
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (!isStart)
            {
                frmFunSet ffs = new frmFunSet();
                ffs.ShowDialog();
                if (ffs.DialogResult == DialogResult.OK)
                {
                    this.lblStatus.Text = tips[1];
                }
            }
            else {
                MessageBox.Show("正在扫描中");
            }
        }
        #endregion
        #region 传输列表
        int successNum = 0;
        /// <summary>
        /// 传输列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox14_Click(object sender, EventArgs e)
        {
            if (!isStart)
            {
                this.timer1.Enabled = false;
                successNum = 0;
                frmTransportList ftl = new frmTransportList(dtUpdateFile, dtSuccess, this);
                ftl.ShowDialog();
                if (ftl.DialogResult == DialogResult.OK)
                {
                    dtUpdateFile = ftl.dtUpdateFile;
                    dtSuccess = ftl.dtSuccess;
                    if (dtUpdateFile != null && dtUpdateFile.Rows.Count > 0)
                    {
                        successNum = Convert.ToInt32(this.label4.Text.Replace("张", ""));
                        this.lblStatus.Text = tips[4] + "（" + successNum.ToString() + " / " + allNum.ToString() + "）";
                        this.timer1.Enabled = true;
                    }
                }
            }
            else {
                MessageBox.Show("正在扫描中");
            }
        }
        #endregion
        #region 监测上传
        private void timer1_Tick(object sender, EventArgs e)
        {
            #region
            int len = dtUpdateFile.Rows.Count;
            if (len > 0)
            {
                for (int i = 0; i < len; i++)
                {
                    #region
                    if (!File.Exists(dtUpdateFile.Rows[i]["fileAdd"].ToString()))//不存在表示上传成功
                    {
                        DataRow dr1 = dtSuccess.NewRow();
                        var comName = dtUpdateFile.Rows[i]["CompanyName"];//公司名称
                        dr1["CompanyName"] = comName;
                        dr1["fileStatus"] = "传输完成";//文件上传状态
                        dr1["fileAdd"] = dtUpdateFile.Rows[i]["fileAdd"];
                        dtSuccess.Rows.Add(dr1);

                        StringBuilder strUpdate = new StringBuilder();
                        strUpdate.Append(ReadTxt(pathUpdateFile));
                        strUpdate.Append(comName + "$" + dtUpdateFile.Rows[i]["fileAdd"].ToString() + "|");
                        WirteTxt(strUpdate.ToString(), pathUpdateFile);


                        StringBuilder str = new StringBuilder();
                        str.Append(ReadTxt(pathTxt));
                        str.Replace(comName + "$" + dtUpdateFile.Rows[i]["fileAdd"].ToString() + "|", "");
                        WirteTxt(str.ToString(),pathTxt);

                        dtUpdateFile.Rows.RemoveAt(i);
                        len = dtUpdateFile.Rows.Count;
                        i--;
                        successNum++;
                        this.label4.Text = successNum.ToString() + "张";
                        SetConfig.setValue("todayBillNum", successNum.ToString());
                        if (!isStart)
                        {
                            if (isFrmNowUpdate)
                            {
                                isFrmNowUpdate = false;
                                allNum = successNum + uploadfilePath.Count;
                            }
                            this.lblStatus.Text = tips[4] + "（" + successNum.ToString() + " / " + allNum.ToString() + "）";
                        }
                    }
                    #endregion
                }
            }
            else
            {
                if (isFrmMian)
                {
                    this.Dispose();
                    System.Environment.Exit(0);
                }
                this.timer1.Enabled = false;
                if (!isStart)
                {
                    this.lblStatus.Text = tips[5];
                }
            }
            #endregion
        }
        #endregion
        #region timer2
        private void timer2_Tick(object sender, EventArgs e)
        {
            this.timer2.Enabled = false;
            if (dtUpdateFile.Rows.Count > 0)//表示上传列表中还在上传
            {
                this.lblStatus.Text = tips[4] + "（" + (uploadfilePath.Count - dtUpdateFile.Rows.Count).ToString() + " / " + uploadfilePath.Count.ToString() + "）";
            }
            else {
                this.lblStatus.Text = tips[4];
            }
        }
        #endregion
        #region 最小化
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion
    }
}
