using Microsoft.Win32;
using MielLauncher.Classes.Modules.CurrentSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MielLauncher.Classes.Game.JavaManage
{
    internal class FindJava
    {
        public static List<Java> FindNow()
        {
            List<Java> result = new List<Java>();
            List<Java> NewJava = FindByPath("SOFTWARE\\JavaSoft\\JDK");
            List<Java> OldJava = FindByPath("SOFTWARE\\JavaSoft\\Java Development Kit");
            foreach (Java Java in NewJava)
            {
                result.Add(Java);
            }
            foreach (Java Java in OldJava)
            {
                result.Add(Java);
            }
            return result;
        }
        public static List<Java> FindByPath(string str)
        {
            List<Java> result = new List<Java>();
            string[] regs = Classes.Modules.CurrentSystem.Reg.Access_Registry(str, RegType.LocalMachine);
            foreach (string reg in regs)
            {
                result.Add(new Java(reg, Classes.Modules.CurrentSystem.Reg.GetValue(str + "\\" + reg, "JavaHome", RegType.LocalMachine) + "\\"));
            }
            result = result.Where((x, i) => result.FindIndex(z => z.path == x.path) == i).ToList();
            return result;
        }
    }
}
