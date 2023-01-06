using MielLauncher.Classes.Game;
using MielLauncher.Classes.Game.JavaManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MielLauncher.Pages.Settings
{
    /// <summary>
    /// Settings.xaml 的交互逻辑
    /// </summary>
    public partial class Settings : Page
    {
        private static Settings settingsPage;
        public static Settings GetSettings()
        {
            return settingsPage;
        }
        public Settings()
        {
            settingsPage = this;
            InitializeComponent();
            InitializeJavaList();
            InitializeMaxMemory();
        }
        /// <summary>
        /// Java List
        /// </summary>
        #region
        List<Java> javas = FindJava.FindNow();
        private void InitializeJavaList()
        {
            if (javas.Count > 0)
            {
                javas.Sort((a, b) => a.name.CompareTo(b.name));
                List<string> javaNames = new List<string>();
                foreach (Java java in javas)
                {
                    javaNames.Add("版本 " + java.name + " (" + java.path + ")");
                }
                JavaList.ItemsSource = javaNames;
                JavaList.Text = javaNames[0];
            }
            else
            {
                JavaList.Text = "没有 Java，快去下载吧";
            }
        }
        public Java selectedJava()
        {
            Java selectedJava = javas[JavaList.SelectedIndex];
            return selectedJava;
        }
        #endregion
        /// <summary>
        /// Max Memory
        /// </summary>
        #region
        private void InitializeMaxMemory()
        {
            MaxMemory.Text = "1024";
        }
        public int getMaxMemory()
        {
            return int.Parse(MaxMemory.Text);
        }
        #endregion
    }
}
