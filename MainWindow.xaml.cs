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
using MahApps.Metro.Controls;
using Microsoft.Win32;
using MielLauncher.Classes.Modules.CurrentSystem;

namespace MielLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            NavigationLink.Navigate(new Uri(@"Pages/Navigation.xaml", UriKind.Relative));
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            Classes.Modules.CurrentSystem.Reg.GetValue("SOFTWARE\\JavaSoft\\JDK\\18.0.2.1", "JavaHome", RegType.LocalMachine);
        }
        public static string NAME = "MIEL 启动器";
        public static string VERSION = "Beta 1.2.1";
        public static string UPDATELOG = "" +
        "Beta 1.2.1 (2023.1.1)\n" +
        "\t元旦快乐！\n" +
        "\t1. 支持在设置里修改最大内存\n" +
        "Beta 1.2 (2022.12.31)\n" +
        "\t1. 支持自定义游戏名称\n" +
        "\t2. 在下载版本后会刷新版本列表\n" +
        "Beta 1.1 (2022.12.30)\n" +
        "\t1. 添加启动功能，使用 KMCCC 内核\n" +
        "\t2. 启动时会在主页顶部展示启动状态\n" +
        "\t3. Java 版本会优先选择 Java 8\n" +
        "\t（注意：目前需要其他启动器补全资源）\n" +
        "Beta 1.0.2 (2022.12.26)\n" +
        "\t1. 改进参数生成方法\n" +
        "Beta 1.0.1 (2022.12.25)\n" +
        "\t1. 可以生成 Minecraft 1.12.2 及以下的 Minecraft 启动参数\n" +
        "\t\t点击启动即可看到\n" +
        "\t2. 统一 UI\n" +
        "Beta 1.0 (2022.12.23)\n" +
        "\t1. 增加查找 Java 功能\n" +
        "\t2. 优化下载功能\n" +
        "\t3. 关于页面可以查看更新日志\n" +
        "Old Dev 2022.12.10:\n" +
        "\t1. 修复在 Windows 7 中无法检查版本的 Bug\n" +
        "Old Dev 2022.11.22:\n" +
        "\t1. 增加多线程下载模块，默认使用此模块\n" +
        "\t2. 重绘 UI\n" +
        "\t3. 增加关于页面\n" +
        "\t修复 1 个 Bug:\n" +
        "\t\t[严重] Lbug-002: 无法下载 1.13 以上的版本 - _Windows11_\n" +
        "Old Dev 2022.11.21 [不稳定]\n" +
        "\t1. 下载显示进度条\n" +
        "Old Dev 2022.11.20 [不稳定][修复版本]\n" +
        "\t1. 增加下载功能\n" +
        "Old Dev 2022.11.19\n" +
        "\t1. 增加版本列表查找功能，暂时无法下载\n" +
        "\t2. [大改动] 将框架从 .NET Core 迁移至 .NET Framework 以更好适应 Windows 7\n" +
        "\t修复 1 个 Bug:\n" +
        "\t\t[严重] Lbug-001: 版本目录不存在会闪退(System.IO.IOException) - _Windows11_\n" +
        "Old Dev 2022.11.18 [版本丢失]\n" +
        "\t1. 增加扫描本地版本列表功能\n" +
        "Old Dev 2022.11.17 [首版][版本丢失]\n" +
        "\t1. 增加 UI\n";
    }
}
