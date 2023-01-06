using System;
using System.Collections.Generic;
using System.Data;
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
using static MielLauncher.Pages.Download.Download;
using NetModules = MielLauncher.Classes.Net;
using DateConvert = MielLauncher.Classes.Modules.DateConvert;

namespace MielLauncher.Pages.Download
{
    /// <summary>
    /// Download.xaml 的交互逻辑
    /// </summary>
    public partial class Download : Page
    {
        public Download()
        {
            InitializeComponent();
            ShowsNavigationUI = false;
            InitializeVersionList();
        }
        private void VersionList()
        {
            try
            {
                InitializeVersionList();
            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show("加载版本列表时，出现错误(" + ex.Message + ")，是否重试？", "提示", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    VersionList();
                }
                else
                {
                    return;
                }
            }
        }
        private List<NetModules.Versions> versions = new List<NetModules.Versions>();
        private async void InitializeVersionList()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("版本号", typeof(string));
            dataTable.Columns.Add("类型", typeof(string));
            dataTable.Columns.Add("发布时间", typeof(string));
            dataTable.Rows.Add("正在加载版本列表");
            MinecraftVersionList.ItemsSource = dataTable.DefaultView;

            versions = await NetModules.GetVersion.GetNow();
            dataTable.Rows.Clear();
            // Convert List<Versions> to DataTable

            for (int i = 0; i < versions.Count; i++)
            {
                string Type = versions[i].type;// Type of Version (release, snapshot etc.)
                switch (Type)
                {
                    case "release":
                        Type = "正式版本";
                        break;
                    case "snapshot":
                        Type = "快照版本";
                        break;
                    case "old_beta":
                        Type = "Beta 版本";
                        break;
                    case "old_alpha":
                        Type = "Alpha 版本";
                        break;
                }
                dataTable.Rows.Add(versions[i].id, Type, DateConvert.FormatDateString(versions[i].time));
            }
            MinecraftVersionList.ItemsSource = dataTable.DefaultView;
        }

        private void StartDownloadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView selectedItem = MinecraftVersionList.SelectedItem as DataRowView;
                if (selectedItem == null)
                {
                    MessageBox.Show("请先选择版本");
                    return;
                }
                string selectedVersionName = selectedItem[0] as string;
                if (selectedVersionName == null || selectedVersionName == "正在加载版本列表")
                {
                    return;
                }
                NetModules.Versions downloadVersion = null;
                for (int i = 0; i < versions.Count; i++)
                {
                    if (versions[i].id == selectedVersionName)
                    {
                        downloadVersion = versions[i];
                    }
                }
                if (downloadVersion == null)
                {
                    MessageBox.Show("找不到版本");
                }
                Navigation navigation = Navigation.GetNavigation();
                SelectVersion selectVersion = new SelectVersion();
                navigation.DownloadPage.Navigate(selectVersion, downloadVersion);
                navigation.DownloadPage.LoadCompleted += selectVersion.NavigationLoadCompleted;
            }
            catch
            {
            }
        }
    }
}
