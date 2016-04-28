using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Extended;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ScanSystem
{
    public partial class frmLogin : Form
    {
        string ServiceUrl = SetConfig.GetValue("ServiceUrl");
        public frmLogin()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体初始化
        /// </summary>       
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;//在任务栏显示图标
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;//去掉窗体边框
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picLogin_Click(object sender, EventArgs e)
        {
            login();
        }
        public void login()
        {
            if (!string.IsNullOrEmpty(txtName.Text.Replace("\r", "").Replace("\n", "")) && !string.IsNullOrEmpty(txtPwd.Text.Replace("\r", "").Replace("\n", "")))
            {
                string str = getXmlStr(txtName.Text.Replace("\r", "").Replace("\n", ""), txtPwd.Text.Replace("\r", "").Replace("\n", "")).Replace("\r", "").Replace("\n", "");
                if (str != "0" && !string.IsNullOrEmpty(str))
                {
                    TestData jobj = JsonConvert.DeserializeObject<TestData>(str);
                    string times = SetConfig.GetValue("times");
                    if (!string.IsNullOrEmpty(times)) {
                        if (Convert.ToInt32(times) < Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")))
                        {
                            SetConfig.setValue("times", DateTime.Now.ToString("yyyyMMdd"));
                            SetConfig.setValue("todayBillNum", "0");
                        }
                    }
                    else
                    {
                        SetConfig.setValue("times", DateTime.Now.ToString("yyyyMMdd"));
                        SetConfig.setValue("todayBillNum", "0");
                    }
                    string todayBillNum = SetConfig.GetValue("todayBillNum");
                    frmScanMain fsm = new frmScanMain();
                    fsm.telephone = jobj.telephone;
                    fsm.userPicUrl = jobj.userPicUrl;
                    fsm.listCI = jobj.comInfoList;
                    fsm.userName = jobj.userName;
                    fsm.label4.Text = todayBillNum + "张";
                    this.Hide();
                    fsm.Show();
                }
                else
                {
                    this.txtPwd.Text = string.Empty;
                    MessageBox.Show("用户名或密码错误");
                }
            }
            else
            {
                MessageBox.Show("请输入用户名或密码");
            }
        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picClose_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
        #region 拖拽
        Point mouseOff;//鼠标移动位置变量
        bool leftFlag;//标签是否为左键
        private void frmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y); //得到变量的值
                leftFlag = true;                  //点击左键按下时标注为true;
            }
        }

        private void frmLogin_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }

        private void frmLogin_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;
            }
        }
        #endregion

        #region 请求java webservice
        public string getXmlStr(string name,string pwd)
        {
            string strBack = string.Empty;
            try
            {
                string Url = ServiceUrl + "LoginS";
                string userName = name;
                string userPwd = pwd;
                Encoding encoding = Encoding.GetEncoding("GB2312");
                string postData = "username=" + userName + "&password=" + userPwd;
                byte[] data = encoding.GetBytes(postData);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                //发送数据
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();
                //接收返回值
                HttpWebResponse res = (HttpWebResponse)request.GetResponse();
                StreamReader sReader = new StreamReader(res.GetResponseStream(), System.Text.Encoding.UTF8);
                strBack = sReader.ReadToEnd();
                sReader.Close();
                res.Close();
            }
            catch (Exception)
            {
                //MessageBox.Show("服务器报错！");
            }           
            return strBack;
        }
        public class TestData
        {
            public TestData()
            {
            }
            /// <summary>
            /// 登录账号名称
            /// </summary>
            public string userName { get; set; }
            /// <summary>
            /// 电话号码
            /// </summary>
            public string telephone { get; set; }
            /// <summary>
            /// 邮箱
            /// </summary>
            public string email { get; set; }
            /// <summary>
            /// 用户头像地址
            /// </summary>
            public string userPicUrl { get; set; }
            /// <summary>
            /// 公司信息列表
            /// </summary>
            public List<CompanyInfo> comInfoList { get; set; }
        }
        #endregion

        #region xxxx
        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    login();
                    break;
            }
        }

        private void txtPwd_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    login();
                    break;
            }
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    login();
                    break;
            }
        }
        #endregion

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
