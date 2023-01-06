﻿using System;
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

namespace MielLauncher.Pages.More
{
    /// <summary>
    /// More.xaml 的交互逻辑
    /// </summary>
    public partial class More : Page
    {
        public More()
        {
            InitializeComponent();
            VersionName.Text = "目前版本: " + MainWindow.VERSION;
            UpdateLog.Text = MainWindow.UPDATELOG;
        }
    }
}
