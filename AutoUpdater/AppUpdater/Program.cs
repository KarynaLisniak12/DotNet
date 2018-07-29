using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppUpdater
{
    static class Program
    {
        // Чтение версии программы с файла
        public static string[] readText;
        public static Version myVersion;

        public static Version serverVer;

        static string serverVersion = "http://127.0.0.1/edsa-up/VersionApp.txt"; // Адрес на сервере, где находится файл с версией программы
        public static string myAppName;
        public static string serverAppName;

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                readText = File.ReadAllLines("VersionApp.txt");
                myVersion = new Version(readText[0]);

                myAppName = Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                serverAppName = "new." + myAppName.ToString().Replace(".exe", ".zip"); 

                serverVer = getServerVersion();

                if (myVersion != serverVer)
                {
                    MessageBox.Show("Обнаружена новая версия " + serverVer, "APPlication Updater Launcher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Run(new Updater(myAppName, serverAppName));
                    Application.Run(new MainForm());
                }
                else Application.Run(new MainForm());
            }
            catch
            {
                Application.Run(new MainForm());
            }

        }

        // Метод, который считывает версию с сервера по указанному адресу
        static Version getServerVersion()
        {
            try
            {
                WebClient webClient = new WebClient();

                string textServerVersion = webClient.DownloadString(serverVersion).Trim();
                int endIndex = textServerVersion.IndexOf('\r');

                return new Version(textServerVersion.Substring(0, textServerVersion.Length - (textServerVersion.Length - endIndex)));
            }
            catch
            {
                return Program.myVersion;
            }
        }
    }
}
