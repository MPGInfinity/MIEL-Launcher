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

namespace MielLauncher.Pages
{
    /// <summary>
    /// Navigation.xaml 的交互逻辑
    /// </summary>
    public partial class Navigation : Page
    {
        private static Navigation _navigation;
        internal static Navigation GetNavigation()
        {
            return _navigation;
        }
        internal void NavigationTo(Frame frame, string uri)
        {
            frame.Navigate(new Uri(uri, UriKind.Relative));
        }
        public Navigation()
        {
            InitializeComponent();
            _navigation = this;
            HomePage.Navigate(new Uri(@"Pages/Home/Home.xaml", UriKind.Relative));
            DownloadPage.Navigate(new Uri(@"Pages/Download/Download.xaml", UriKind.Relative));
            AccountPage.Navigate(new Uri(@"Pages/User/User.xaml", UriKind.Relative));
            SettingsPage.Navigate(new Uri(@"Pages/Settings/Settings.xaml", UriKind.Relative));
            MorePage.Navigate(new Uri(@"Pages/More/More.xaml", UriKind.Relative));
        }
    }
}
