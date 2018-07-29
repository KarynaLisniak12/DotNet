using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppUpdater
{
    public partial class Updater : Form
    {
        private string url_program = "http://127.0.0.1/edsa-up/ArchiveUpdater.zip"; // Адрес на сервере, где находиться архив с установочными файлами 

        private string myAppName;
        private string serverAppName;

        public Updater(string myAppName, string serverAppName)
        {
            InitializeComponent();
            
            this.myAppName = myAppName;
            this.serverAppName = serverAppName;
            download_file();
        }

        private void download_file()
        {
            try
            {
                WebClient webClient = new WebClient();
                // Создаём обработчики событий продвижения прогресса и его окончания
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(download_Completed);

                // Начинаем скачивание
                webClient.DownloadFileAsync(new Uri(url_program), serverAppName);
            }
            catch
            {
                MessageBox.Show("Нет связи с сервером!");
            }
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            label1.Text = String.Format("Загружено: {0} Кбайт / {1} Кбайт", e.BytesReceived - 1024, e.TotalBytesToReceive - 1024); // Выводим в лейбл информацию о процессе загрузки
            progress_download.Value = e.ProgressPercentage;
        }

        private void download_Completed(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                Process.Start("UpdaterLauncher.exe", serverAppName + " " + myAppName + " " + Program.serverVer);
                this.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Нет связи с сервером!");
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
