using Affdex;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HRUTWeb
{
    public partial class Form : System.Windows.Forms.Form
    {
        PhotoHandler phandler = new PhotoHandler();

        public Form()
        {
            InitializeComponent();
            ClientSize = new Size(612, 512 + 100 + 64 + 30);
            phandler.OnFinished += (s, e) => pictureBox1.Image = e.Image;
        }

        private void selectBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = BitmapExtensions.LoadImageFitSize(ofd.FileName);
                phandler.Detect(ofd.FileName);
            }
        }
    }
}
