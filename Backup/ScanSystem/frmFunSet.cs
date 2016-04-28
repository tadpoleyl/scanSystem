using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using TwainDotNet;
using TwainDotNet.TwainNative;
using TwainDotNet.WinFroms;

namespace ScanSystem
{
    public partial class frmFunSet : Form
    {
        Twain _twain;
        public frmFunSet()
        {
            InitializeComponent();
        }

        private void frmFunSet_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;//去掉窗体边框

            try
            {
                string setValue = SetConfig.GetValue("ScanDrive");
                _twain = new Twain(new WinFormsWindowMessageHook(this));
                List<DictionaryEntry> listDS = _twain.GetAllSources().Select(x => new DictionaryEntry() { Key = x.SourceId.ProductName, Value = x.SourceId.ProductName + " " + x.SourceId.Version.Info }).ToList();
                int index = listDS.FindIndex(x => x.Key.ToString() == setValue);
                dgvCompany.DataSource = listDS;

                if ((dgvCompany.DataSource as List<DictionaryEntry>) != null && (dgvCompany.DataSource as List<DictionaryEntry>).Count > 0)
                {
                    dgvCompany.ClearSelection();
                }
                if (index >= 0)
                {
                    dgvCompany.Rows[index].Selected = true;
                    dgvCompany.CurrentCell = this.dgvCompany.Rows[index].Cells[1];
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("未检测到有扫描仪驱动！");
            }
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            try
            {
                if ((dgvCompany.DataSource as List<DictionaryEntry>) != null && (dgvCompany.DataSource as List<DictionaryEntry>).Count > 0)
                {
                    string cellvalue = dgvCompany.Rows[dgvCompany.CurrentRow.Index].Cells["keys"].Value.ToString();
                    if (cellvalue != null)
                    {
                        SetConfig.setValue("ScanDrive", cellvalue);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("未选择扫描仪器");
                    }
                }
                else
                {
                    MessageBox.Show("请选择扫描仪类型！！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
