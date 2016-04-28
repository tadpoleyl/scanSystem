using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScanSystem
{
    public partial class frmTransportList : Form
    {
        frmScanMain parentFrm = null;
        public DataTable dtUpdateFile = null;
        public DataTable dtSuccess = null;
        int successNum = 0;
        public frmTransportList(DataTable dt1, DataTable dt2, frmScanMain frm)
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;//去掉窗体边框
            parentFrm = frm;
            dtUpdateFile = dt1;
            dtSuccess = dt2;
            successNum = dt2.Rows.Count;
            this.dgvNowUpdate.DataSource = dt1;
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                dgvNowUpdate.ClearSelection();
                this.label1.Text = "正在上传（" + dt1.Rows.Count.ToString() + "）";
                this.timer1.Enabled = true;
            }
            else
            {
                this.picbox_noBill.Visible = true;
                this.dgvNowUpdate.Visible = false;
            }
            this.dgvSuccess.DataSource = dt2;
            if (dt2 != null && dt2.Rows.Count > 0)
            {
                dgvSuccess.ClearSelection();
                this.label2.Text = "上传完成（" + dt2.Rows.Count.ToString() + "）";
                //parentFrm.label4.Text = dt2.Rows.Count.ToString() + "张";
            }
            #region
            dgvNowUpdate.AdvancedCellBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            dgvNowUpdate.AdvancedCellBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
            dgvNowUpdate.AdvancedCellBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
            dgvSuccess.AdvancedCellBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            dgvSuccess.AdvancedCellBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
            dgvSuccess.AdvancedCellBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
            dgvNowUpdate.RowTemplate.Height = 40;
            dgvSuccess.RowTemplate.Height = 40;
            #endregion
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #region 清除所有已完成记录
        private void picBox_clearAll_Click(object sender, EventArgs e)
        {
            if (this.dgvSuccess.Rows.Count > 0)
            {
                StringBuilder strUpdate = new StringBuilder();
                WirteTxt("", parentFrm.pathUpdateFile);

                dtSuccess.Rows.Clear();
                dgvSuccess.Update();

                this.label2.Text = "上传完成（0）";
            }
        }
        #endregion

        #region 正在上传
        private void label1_Click(object sender, EventArgs e)
        {
            this.pictureBox2.Visible = true;
            this.pictureBox3.Visible = false;
            this.label1.ForeColor = Color.FromArgb(61, 138, 226);
            this.label2.ForeColor = Color.FromArgb(51, 51, 51);
            this.picBox_clearAll.Visible = false;
            this.dgvSuccess.Visible = false;
            if ((this.dgvNowUpdate.DataSource as DataTable) != null && this.dgvNowUpdate.RowCount > 0)
            {
                this.dgvNowUpdate.Visible = true;
                this.picbox_noBill.Visible = false;
            }
            else
            {
                this.dgvNowUpdate.Visible = false;
                this.picbox_noBill.Visible = true;
            }
        }
        #endregion
        #region 上传完成
        private void label2_Click(object sender, EventArgs e)
        {
            this.pictureBox2.Visible = false;
            this.dgvNowUpdate.Visible = false;
            this.picbox_noBill.Visible = false;
            this.dgvNowUpdate.Visible = false;
            this.dgvSuccess.Visible = true;
            this.pictureBox3.Visible = true;
            this.label2.ForeColor = Color.FromArgb(61, 138, 226);
            this.label1.ForeColor = Color.FromArgb(51, 51, 51);
            this.picBox_clearAll.Visible = true;
        }
        #endregion

        private void frmTransportList_Load(object sender, EventArgs e)
        {
            this.dgvNowUpdate.AutoGenerateColumns = false;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            #region
            int len = dtUpdateFile.Rows.Count;
            if (len > 0)
            {
                for (int i = 0; i < len; i++)
                {
                    if (!File.Exists(dtUpdateFile.Rows[i]["fileAdd"].ToString()))//不存在表示上传成功
                    {
                        int num = Convert.ToInt32(parentFrm.label4.Text.Replace("张", ""));
                        num++;
                        parentFrm.label4.Text = num.ToString() + "张";
                        parentFrm.lblStatus.Text = parentFrm.tips[4] + "（" + num.ToString() + " / " + parentFrm.allNum.ToString() + "）";

                        DataRow dr1 = dtSuccess.NewRow();
                        var comName = dtUpdateFile.Rows[i]["CompanyName"];//公司名称
                        dr1["CompanyName"] = comName;
                        dr1["fileStatus"] = "传输完成";//文件上传状态
                        dr1["fileAdd"] = dtUpdateFile.Rows[i]["fileAdd"];
                        dtSuccess.Rows.Add(dr1);

                        StringBuilder strUpdate = new StringBuilder();
                        strUpdate.Append(ReadTxt(parentFrm.pathUpdateFile));
                        strUpdate.Append(comName + "$" + dtUpdateFile.Rows[i]["fileAdd"].ToString() + "|");
                        WirteTxt(strUpdate.ToString(), parentFrm.pathUpdateFile);



                        StringBuilder str = new StringBuilder();
                        str.Append(ReadTxt(parentFrm.pathTxt));
                        str.Replace(comName + "$" + dtUpdateFile.Rows[i]["fileAdd"].ToString() + "|", "");
                        WirteTxt(str.ToString(), parentFrm.pathTxt);

                        dtUpdateFile.Rows.RemoveAt(i);

                        len = dtUpdateFile.Rows.Count;
                        i--;
                        if (len == 0)
                        {
                            this.timer1.Enabled = false;
                            this.dgvNowUpdate.Visible = false;
                            this.picbox_noBill.Visible = true;
                            parentFrm.lblStatus.Text = parentFrm.tips[5];
                        }
                        this.label1.Text = "正在上传（" + len.ToString() + "）";
                        this.label2.Text = "上传完成（" + dtSuccess.Rows.Count.ToString() + "）";
                    }
                }
            }
            else
            {
                this.timer1.Enabled = false;
                this.dgvNowUpdate.Visible = false;
                this.picbox_noBill.Visible = true;
                parentFrm.lblStatus.Text = parentFrm.tips[5];
            }
            #endregion
        }
        private void WirteTxt(string json, string path)
        {

            StreamWriter sw = new StreamWriter(path, false);
            sw.WriteLine(json);
            sw.Close();
        }
        private string ReadTxt(string path)
        {
            string str = "";
            StreamReader sr = new StreamReader(path, false);
            str = sr.ReadLine();
            sr.Close();
            return str;
        }
    }
}
