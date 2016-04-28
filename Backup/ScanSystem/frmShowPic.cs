using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScanSystem
{
    public partial class frmShowPic : Form
    {
        public Image imgFiles = null;
        int width = 0;
        int height = 0;
        public string imgPaths = string.Empty;
        public frmShowPic(Bitmap btpImage, string imgPath)
        {
            InitializeComponent();
            try
            {
                imgFiles = Image.FromFile(imgPath);
                width = picShow.Width;
                height = picShow.Height;
                imgPaths = imgPath;
                if (btpImage != null)
                {
                    Graphics g = Graphics.FromImage(btpImage);
                    g.DrawImage(btpImage, new Rectangle(0, 0, btpImage.Width, btpImage.Height));//  btpImage.Width, btpImage.Height
                    this.picShow.Image = btpImage;
                    g.Dispose();
                }
                picShow.SizeMode = PictureBoxSizeMode.StretchImage;
                var str = imgPath.Replace("-success", "").Split('.')[0].ToString();
                this.label4.Text = Convert.ToInt32(str.Substring(str.Length - 4, 4)).ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmShowPic_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;//去掉窗体边框
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            imgFiles.Dispose();
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            imgFiles.Dispose();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        [DllImport("kernel32.dll")]
        public static extern IntPtr _lopen(string lpPathName, int iReadWrite);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);

        public const int OF_READWRITE = 2;
        public const int OF_SHARE_DENY_NONE = 0x40;
        public readonly IntPtr HFILE_ERROR = new IntPtr(-1);  
        private void btnStateScan_Click(object sender, EventArgs e)
        {
            try
            {
                IntPtr vHandle = _lopen(imgPaths, OF_READWRITE | OF_SHARE_DENY_NONE);
                if (vHandle == HFILE_ERROR)
                {
                    MessageBox.Show("文件正在上中,无法保存！");
                }
                else
                {
                    imgFiles.Save(imgPaths, ImageFormat.Jpeg);
                    imgFiles.Dispose();

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public int MyAngle = 0;
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            try
            {
                MyAngle -= 90;
                this.picShow.Image = RotateImg.RotateImgs(this.picShow.Image, MyAngle);
                imgFiles = RotateImg.RotateImgs(imgFiles, MyAngle);
                MyAngle = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            try
            {
                MyAngle += 90;
                this.picShow.Image = RotateImg.RotateImgs(this.picShow.Image, MyAngle);
                imgFiles = RotateImg.RotateImgs(imgFiles, MyAngle);
                MyAngle = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// 缩小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            try
            {
                if (picShow.Height > height && picShow.Width > width)
                {
                    picShow.Height = Convert.ToInt32(picShow.Height /2);
                    picShow.Width = Convert.ToInt32(picShow.Width /2);
                    panel3.VerticalScroll.Value = panel3.VerticalScroll.Maximum / 2 - panel3.VerticalScroll.LargeChange / 2;
                    if (panel3.VerticalScroll.Value != panel3.VerticalScroll.Maximum / 2 - panel3.VerticalScroll.LargeChange / 2)
                        panel3.VerticalScroll.Value = panel3.VerticalScroll.Maximum / 2 - panel3.VerticalScroll.LargeChange / 2;

                    panel3.HorizontalScroll.Value = panel3.HorizontalScroll.Maximum / 2 - panel3.HorizontalScroll.LargeChange / 2;
                    if (panel3.HorizontalScroll.Value != panel3.HorizontalScroll.Maximum / 2 - panel3.HorizontalScroll.LargeChange / 2)
                        panel3.HorizontalScroll.Value = panel3.HorizontalScroll.Maximum / 2 - panel3.HorizontalScroll.LargeChange / 2;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 放大
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            try
            {
                picShow.Height = Convert.ToInt32(picShow.Height * 2);
                picShow.Width = Convert.ToInt32(picShow.Width * 2);

                panel3.VerticalScroll.Value = panel3.VerticalScroll.Maximum / 2 - panel3.VerticalScroll.LargeChange / 2;
                if (panel3.VerticalScroll.Value != panel3.VerticalScroll.Maximum / 2 - panel3.VerticalScroll.LargeChange / 2)
                    panel3.VerticalScroll.Value = panel3.VerticalScroll.Maximum / 2 - panel3.VerticalScroll.LargeChange / 2;

                panel3.HorizontalScroll.Value = panel3.HorizontalScroll.Maximum / 2 - panel3.HorizontalScroll.LargeChange / 2;
                if (panel3.HorizontalScroll.Value != panel3.HorizontalScroll.Maximum / 2 - panel3.HorizontalScroll.LargeChange / 2)
                    panel3.HorizontalScroll.Value = panel3.HorizontalScroll.Maximum / 2 - panel3.HorizontalScroll.LargeChange / 2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
