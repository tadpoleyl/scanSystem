using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwainDotNet;
using TwainDotNet.TwainNative;
using TwainDotNet.WinFroms;

namespace ScanSystem
{
    public partial class frmScan : Form
    {
        public string num = string.Empty;
        private string pattern = @"^[0-9]*$";
        private string param1 = null;

        public frmScan()
        {
            InitializeComponent();
        }

        private void frmScan_Load(object sender, EventArgs e)
        {
            this.txtNum.Text = num;
            param1 = num;
            this.ControlBox = false;//去掉右上角关闭按钮
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;//去掉窗体边框
        }
        #region 开始扫描
        private void btnStateScan_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtNum.Text))
            {
                if (Convert.ToInt32(num) > Convert.ToInt32(this.txtNum.Text))
                {
                    MessageBox.Show("起始号只能比现在的大，不能比现在的小！！");
                    this.txtNum.Text = num;
                    // 将光标定位到文本框的最后
                    this.txtNum.SelectionStart = this.txtNum.Text.Length;
                }
                else
                {
                    this.txtNum.Text = num;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }
        #endregion
        #region 关闭
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        #region 只能输入数字
        private void txtNum_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtNum.Text))
            {
                Match m = Regex.Match(this.txtNum.Text, pattern); // 匹配正则表达式

                if (!m.Success) // 输入的不是数字
                {
                    this.txtNum.Text = param1; // textBox内容不变

                    // 将光标定位到文本框的最后
                    this.txtNum.SelectionStart = this.txtNum.Text.Length;
                }
                else // 输入的是数字
                {
                    param1 = this.txtNum.Text; // 将现在textBox的值保存下来
                }
            }
            else
            {
                this.txtNum.Text = num;
                // 将光标定位到文本框的最后
                this.txtNum.SelectionStart = this.txtNum.Text.Length;
            }
        }
        #endregion
    }
}
