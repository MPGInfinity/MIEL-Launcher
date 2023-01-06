using MielLauncher.Classes.Game.JavaManage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MielLauncher.Classes.Game.LaunchMinecraft
{
    internal class MinecraftLaunchConfig
    {
        public Minecraft minecraft;
        public object auth;
        public string assetsRoot;
        public string assetsIndex;
        public Java selectedJava;
        public MinecraftLaunchConfig(Minecraft minecraft, object auth, string assetsIndex, Java selectedJava)
        {
            this.minecraft = minecraft;
            this.auth = auth;
            this.assetsIndex = assetsIndex;
            this.selectedJava = selectedJava;
            assetsRoot = Path.Combine(minecraft.getMinecraftRootPath(), "assets");
        }
    }
}
