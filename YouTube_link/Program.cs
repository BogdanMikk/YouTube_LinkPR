using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouTube_link_saver
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {

            using (Mutex mutex = new Mutex(false, @"Global\" + AppId))
            {
                if (!mutex.WaitOne(0, false))
                {
                    MessageBox.Show("Приложение уже запущено!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }

                GC.Collect();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                if (File.Exists(@"database.db"))
                {
                    using (SQLiteConnection c = new SQLiteConnection(@"Data Source=database.db;Version=3"))
                    {
                        try
                        {
                            c.Open();
                            if (c.State == ConnectionState.Open)
                            {
                                Application.Run(new Main());
                            }
                            else
                            {
                                MessageBox.Show("Отсутствует соединение с базой данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Application.Exit();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Отсутствует соединение с базой данных.\nПроверьте наличие БД в директории программы.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        public static Guid AppId { get; set; }
    }
}