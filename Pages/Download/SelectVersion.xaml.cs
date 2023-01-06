using MielLauncher.Classes.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using NetModules = MielLauncher.Classes.Net;

namespace MielLauncher.Pages.Download
{
    /// <summary>
    /// SelectVersion.xaml 的交互逻辑
    /// </summary>
    public partial class SelectVersion : Page
    {
        public void ProgressUpdate(int value)
        {
            Dispatcher.Invoke(
                new Action(
                        delegate
                        {
                            //            MessageBox.Show(value.ToString());
                            Progress.Value = value;
                            ProgressText.Text = ((double) value).ToString() + "%";
                            Progress.IsIndeterminate = false;
                        }));
        }
        public void ProgressEnd()
        {
            Dispatcher.Invoke(
                new Action(
                        delegate
                        {
                            ProgressText.Text = "下载完成";
                            GoBack.IsEnabled = true;
                            Home.Home.GetHome().InitializeVersionList();
                        }));
        }
        public NetModules.Versions version = new NetModules.Versions();

        public SelectVersion()
        {
            InitializeComponent();
            //            version = versionArg as NetModules.Versions;
            ShowsNavigationUI = false;
        }
        public void NavigationLoadCompleted(object sender, NavigationEventArgs e)
        {
            version = e.ExtraData as NetModules.Versions;
            if (version != null)
            {
                MinecraftName.Text = "下载 Minecraft " + version.id;
            }
            // TODO: Check if the directory exists
        }
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }

        private void Download_Click(object sender, RoutedEventArgs e)
        {
            Download.IsEnabled = false;
            GoBack.IsEnabled = false;
            // Download Minecraft
            if (version != null)
            {
                ProgressText.Text = "准备下载";
                Progress.IsIndeterminate = true;
                NetModules.DownloadMinecraft.Download(version, @".\.minecraft", version.id, new NetModules.DownloadMinecraft.ProgressCallBack(ProgressUpdate), new NetModules.DownloadMinecraft.ProgressEndCallBack(ProgressEnd));
            }
        }
    }
}
