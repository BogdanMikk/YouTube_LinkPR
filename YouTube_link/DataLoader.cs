using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouTube_link_saver
{
    class DataLoader
    {
        private List<Links> links;

        string cs = @"Data Source=database.db;Version=3";

        public DataLoader()
        {
            links = new List<Links>();
        }

        public void doQuery(string q)
        {
            using (SQLiteConnection c = new SQLiteConnection(cs))
            {
                try
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.CommandText = q;
                    cmd.Connection = c;
                    c.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void fillLinks()
        {
            links.Clear();
            using (SQLiteConnection c = new SQLiteConnection(cs))
            {
                try
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.CommandText = @"SELECT * FROM 'links'";
                    cmd.Connection = c;
                    c.Open();
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Links d = new Links(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), rdr.GetInt32(3), rdr.GetString(4));
                            links.Add(d);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void addLink(Links ln)
        {
            links.Add(ln);
        }
        public List<Links> getLinks()
        {
            return links;
        } 
    }
}
