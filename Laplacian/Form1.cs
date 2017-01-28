using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laplacian
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Bitmap bm = null;

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                bm = (Bitmap)Bitmap.FromFile(openFileDialog1.FileName);
                pictureBox1.Image = bm;
                laplacinToolStripMenuItem.Enabled = true;
                saveToolStripMenuItem.Enabled = false;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.Title = "Save file";
                saveFileDialog1.OverwritePrompt = true;
                saveFileDialog1.DefaultExt = ".jpg";
                saveFileDialog1.InitialDirectory = @"C:\Users\Asus\Pictures\Transformed_Pictures";


                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    pictureBox2.Image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }

            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void laplacinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                saveToolStripMenuItem.Enabled = true;

                Bitmap bm1 = new Bitmap(bm.Width, bm.Height);
                int width = bm.Width - 1;
                int height = bm.Height - 1;

                for (int i = 1; i < width; i++)
                {
                    for (int j = 1; j < height; j++)
                    {
                        Color color2, color4, color5, color6, color8;
                        color2 = bm.GetPixel(i, j - 1);
                        color4 = bm.GetPixel(i - 1, j);
                        color5 = bm.GetPixel(i, j);
                        color6 = bm.GetPixel(i + 1, j);
                        color8 = bm.GetPixel(i, j + 1);

                        int colorRed = color2.R + color4.R + color5.R * (-4) + color6.R + color8.R;
                        int colorGreen = color2.G + color4.G + color5.G * (-4) + color6.G + color8.G;
                        int colorBlue = color2.B + color4.B + color5.B * (-4) + color6.B + color8.B;
                        int avg = (colorRed + colorGreen + colorBlue) / 3;

                        if (avg > 255)
                        {
                            avg = 255;
                        }

                        if (avg < 0)
                        {
                            avg = 0;
                        }

                        bm1.SetPixel(i, j, Color.FromArgb(avg, avg, avg));
                    }
                }

                pictureBox2.Image = bm1;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
    }
}
