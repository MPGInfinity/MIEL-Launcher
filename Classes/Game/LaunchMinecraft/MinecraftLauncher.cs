using KMCCC.Authentication;
using KMCCC.Launcher;
using MielLauncher.Classes.Game.LaunchMinecraft.Tools;
using MielLauncher.Classes.Game.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MielLauncher.Classes.Game.LaunchMinecraft
{
    internal class MinecraftLauncher
    {
        MinecraftLaunchConfig launchConfig;
        public MinecraftLauncher(MinecraftLaunchConfig launchConfig)
        {
            this.launchConfig = launchConfig;
        }
        public LaunchResult Launch()
        {/*
            string arguments = Arguments.getArg(launchConfig);
            Clipboard.SetText(arguments);
            MessageBox.Show("参数已复制到剪贴板");*/
            LauncherCore launcherCore = LauncherCore.Create(
                new LauncherCoreCreationOption(
                    javaPath: launchConfig.selectedJava.path + "bin\\javaw.exe",
                    gameRootPath: launchConfig.minecraft.getMinecraftRootPath()
                ));
            KMCCC.Launcher.Version version = launcherCore.GetVersion(launchConfig.minecraft.getName());
            Reporter.SetClientName("MIEL");
            LaunchResult result = launcherCore.Launch(new LaunchOptions
            {
                Version = version,
                Authenticator = new OfflineAuthenticator((launchConfig.auth as AuthClass).playerName),
                MaxMemory = Pages.Settings.Settings.GetSettings().getMaxMemory(),
                Mode = LaunchMode.BmclMode,
                VersionType = "MIEL"
            });
            return result;
        }
    }
}
