using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MielLauncher.Classes.Game.JavaManage
{
    public class Java
    {
        public string name, path;
        public Java()
        {
            this.name = "";
            this.path = "";
        }
        public Java(string name, string path)
        {
            this.name = name;
            this.path = path;
        }
    }
}
