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

namespace YouTube_link_saver
{
    public partial class Form1 : Form
    {
        bool k = false;
        string r = "", x = "", result = "";
        string template = @"https://img.youtube.com/vi/***********/default.jpg";
        public Form1()
        {
            InitializeComponent();
            //pictureBox1.ImageLocation = @"https://img.youtube.com/vi/JMJXvsCLu6s/default.jpg";
            //https://www.youtube.com/watch?v=pfFqbWeTWUE

            //https://youtu.be/MCW_eJA2FeY?t=4s

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool k1 = false, k2 = false;
            int cx = 0;
            if (textBox1.Text.Contains("youtube.com"))
            {
                foreach (char c in textBox1.Text)
                {
                    if (c == '=')
                    {
                        k = true;
                    }
                    if (k)
                    {
                        r += c;
                    }
                }
                x = r.Replace("=", "");
            }
            else
            {
                foreach (char c in textBox1.Text)
                {
                    if (c == 'e')
                    {
                        k1 = true;
                    }
                    if (k2 && cx <= 11)
                    {
                        r += c;
                        cx++;
                    }
                    if (k1)
                    {
                        k2 = true;
                    }
                }
                x = r.Replace("/", "");
            }
            
            result = template.Replace("***********", x);
            textBox2.Text = result;
            pictureBox1.ImageLocation = result;


            if (dataGridView1.ColumnCount == 0)
            {
                var img = new DataGridViewImageColumn();
                img.ImageLayout = DataGridViewImageCellLayout.Normal;
                dataGridView1.Columns.Add(img);
            }


            dataGridView1.Rows.Add(pictureBox1.Image);

            k = false;
            r = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
