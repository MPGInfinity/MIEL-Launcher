using Game = MielLauncher.Classes.Game;
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
using MielLauncher.Classes.Game.JavaManage;
using MielLauncher.Classes.Game.LaunchMinecraft;
using MielLauncher.Classes.Game.User.AuthClasses;
using System.Security.Permissions;
using ControlzEx.Standard;

namespace MielLauncher.Pages.Home
{
    /// <summary>
    /// Home.xaml 的交互逻辑
    /// </summary>
    public partial class Home : Page
    {
        private static Home HomePage;
        public static Home GetHome()
        {
            return HomePage;
        }
        public Home()
        {
            HomePage = this;
            InitializeComponent();
            InitializeVersionList();
        }
        private List<Game.Minecraft> minecrafts = new List<Game.Minecraft>();
        public void InitializeVersionList()
        {
            minecrafts = Game.FindGame.Find();
            if (minecrafts.Count > 0)
            {
                List<string> minecraftNames = new List<string>();
                foreach (Game.Minecraft minecraft in minecrafts)
                {
                    minecraftNames.Add(minecraft.getName());
                }
                VersionList.ItemsSource = minecraftNames;
                VersionList.Text = minecraftNames[0];
            }
            else
            {
                VersionList.Text = "没有游戏版本，快去下载吧";
            }
        }

        private void LaunchGame_Click(object sender, RoutedEventArgs e)
        {
            LaunchStatus.Text = "正在启动 " + minecrafts[VersionList.SelectedIndex].getName();
            AuthLegacy auth = new AuthLegacy(UserName.Text);
            MinecraftLaunchConfig launchConfig = new MinecraftLaunchConfig(minecrafts[VersionList.SelectedIndex], auth, "MIEL Launcher Test", Settings.Settings.GetSettings().selectedJava());
            MinecraftLauncher minecraftLauncher = new MinecraftLauncher(launchConfig);
            KMCCC.Launcher.LaunchResult result = minecraftLauncher.Launch();
            LaunchStatus.Text = "启动完成";
            if (!result.Success)
            {
                LaunchStatus.Text = "启动失败";
                MessageBox.Show("启动失败, 原因：" + result.ErrorMessage);
            }
        }
    }
}
