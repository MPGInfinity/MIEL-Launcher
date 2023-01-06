using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MielLauncher.Classes.Game
{
    internal class Minecraft
    {
        string GamePath;// .minecraft/versions/<name> Directory
        string Name;
        string MinecraftRootPath;// .minecraft Directory
        public Minecraft(string gamePath, string name, string minecraftRootPath)
        {
            GamePath = gamePath;
            Name = name;
            MinecraftRootPath = minecraftRootPath;
        }
        public Minecraft(string gamePath, string minecraftRootPath)
        {
            MinecraftRootPath = minecraftRootPath;
            GamePath = gamePath;
            Name = Path.GetFileName(gamePath);
        }
        public string getGamePath()
        {
            return GamePath;
        }
        public string getName()
        {
            return Name;
        }
        public string getMinecraftRootPath()
        {
            return MinecraftRootPath;
        }
        public bool IsVaild()
        {
            if (File.Exists(Path.Combine(GamePath, Name + ".jar")) && File.Exists(Path.Combine(GamePath, Name + ".json")))
            {
                return true;
            }
            return false;
        }
    }
}
