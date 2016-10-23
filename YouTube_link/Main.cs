using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouTube_link_saver
{
    public partial class Main : Form
    {
        DataLoader dl = new DataLoader();
        private Links ln;

        Thread t2;

        public Main()//LOAD
        {
            InitializeComponent();

            dl.fillLinks();
            displayLinks(dl.getLinks());
            displayLinks(dl.getLinks());

            button3.Visible = false;
        }

        public static Image GetImageFromUrl(string url)
        {
            try {
                HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);

                using (HttpWebResponse httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    using (Stream stream = httpWebReponse.GetResponseStream())
                    {
                        return Image.FromStream(stream);
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        private void imageLoad()
        {
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                t2 = new Thread(imageLoadThread);
                t2.Start(r);
            }
        }

        private void imageLoadThread(object r)
        {          
            DataGridViewRow row = (DataGridViewRow)r;
            row.Cells[0].Value = GetImageFromUrl(row.Cells[5].Value.ToString());
        }

        private string getPreview(string l)
        {
            bool k = false;
            string r = "", x = "", result = "";
            string template = @"https://img.youtube.com/vi/***********/default.jpg";
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

            k = false;
            r = "";

            return result;
        }

        private void linksFilter(int f)//filter
        {
            if (f == 1)
            {
                var selectedLinks = from x in dl.getLinks()
                                    where x.Link.IndexOf(textBox3.Text, StringComparison.OrdinalIgnoreCase) != -1
                                    where x.Descr.IndexOf(textBox4.Text, StringComparison.OrdinalIgnoreCase) != -1
                                    where x.Rating == Convert.ToInt32(comboBox2.Text)
                                    select x;
                displayLinks(selectedLinks.ToList());
            }
            else
            {
                var selectedLinks = from x in dl.getLinks()
                                    where x.Link.IndexOf(textBox3.Text, StringComparison.OrdinalIgnoreCase) != -1
                                    where x.Descr.IndexOf(textBox4.Text, StringComparison.OrdinalIgnoreCase) != -1
                                    select x;
                displayLinks(selectedLinks.ToList());
            }
            imageLoad();
        }

        private void displayLinks(List<Links> l)//DISPLAY LINKS
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = l;
            tableStyle();
        }

        private void tableStyle()//STYLES
        {
            if (dataGridView1.ColumnCount == 5)
            {
                var img = new DataGridViewImageColumn();
                img.ImageLayout = DataGridViewImageCellLayout.Normal;
                dataGridView1.Columns.Add(img);


                dataGridView1.Columns[0].Visible = false;//id
                dataGridView1.Columns[1].HeaderText = "Ссылка";
                dataGridView1.Columns[1].ReadOnly = true;
                dataGridView1.Columns[2].HeaderText = "Хэштег";
                dataGridView1.Columns[3].HeaderText = "Рейтинг";
                dataGridView1.Columns[4].Visible = false;//preview
            }
            else {

                dataGridView1.Columns[0].Width = 60;
                dataGridView1.Columns[1].Visible = false;//id
                dataGridView1.Columns[2].HeaderText = "Ссылка";
                dataGridView1.Columns[2].ReadOnly = true;
                dataGridView1.Columns[3].HeaderText = "Хэштег";
                dataGridView1.Columns[4].HeaderText = "Рейтинг";
                dataGridView1.Columns[4].Width = 30;
                dataGridView1.Columns[5].HeaderText = "img";
                dataGridView1.Columns[5].Visible = false;//preview
            }
        }

        private void button1_Click(object sender, EventArgs e)//ADD
        {
            if (textBox1.Text != "" && textBox2.Text != "" && comboBox1.Text != "")
            {
                if (Convert.ToInt32(comboBox1.Text) >= 0 && Convert.ToInt32(comboBox1.Text) <= 10)
                {
                    /////
                    string preview = getPreview(textBox1.Text);
                    /////
                    dl.doQuery(@"INSERT INTO 'links'(link, descr, rating, preview) VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + Convert.ToInt32(comboBox1.Text) + "', '" + preview + "' )");
                    ln = new Links(0, textBox1.Text, textBox2.Text, Convert.ToInt32(comboBox1.Text), preview);
                    dl.addLink(ln);
                    displayLinks(dl.getLinks());//refresh datagrid

                    imageLoad();

                    textBox1.Text = textBox2.Text = comboBox1.Text = "";
                }
                else
                {
                    MessageBox.Show("Рейтинг должен быть от 0 до 10!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    comboBox1.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)//CB HANDLER
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            { 
                e.Handled = true;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            linksFilter(0);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            linksFilter(0);
        }      

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            {
                linksFilter(1);
            }
            else
            {
                linksFilter(0);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Text = textBox4.Text = comboBox2.Text = "";
        }

        private void Main_Load(object sender, EventArgs e)
        {
            imageLoad();
        }
    }
}
