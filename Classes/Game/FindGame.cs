using System;
using System.Collections.Generic;
//using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MielLauncher.Classes.Game
{
    internal static class FindGame
    {
        public static List<Minecraft> Find(string MinecraftPath)
        {
            try
            {
                List<Minecraft> list = new List<Minecraft>();
                string VersionPath = Path.Combine(MinecraftPath, "versions");
                string[] Dirs = Directory.GetDirectories(VersionPath, "*", SearchOption.TopDirectoryOnly); // Find All Directories In the Minecraft Directory
                foreach (string Dir in Dirs) // Ergodic All Directory to check is it Minecraft Directory
                {
                    // Check is Version.jar and Version.json ALL exsist
                    string VersionName = Path.GetFileName(Dir);
                    if (File.Exists(Path.Combine(Dir, VersionName + ".jar")) && File.Exists(Path.Combine(Dir, VersionName + ".json")))
                    {
                        list.Add(new Minecraft(Dir, MinecraftPath));
//                        MessageBox.Show(Dir);
                    }
                }
                return list;
            }
            catch
            {
                return new List<Minecraft>();
            }
        }
        public static List<Minecraft> Find()
        {
            return Find(@".\.minecraft");
        }
    }
}
