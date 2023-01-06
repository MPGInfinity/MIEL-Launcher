using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MielLauncher.Classes.Modules.CurrentSystem
{
    public static class Reg
    {
        public static string[] Access_Registry(string str, RegistryKey root = null)
        {
            if (root == null)
            {
                root = RegType.CurrentUser;
            }
            RegistryKey RegDir = root.OpenSubKey(str, false);
            try
            {
                string[] subkeyNames = RegDir.GetSubKeyNames();// All Subkey Name
                return subkeyNames;
            }
            catch
            {
                MessageBox.Show("读取时出现错误：REGISTRY_ACCESS_ERROR");
                return new string[0];
            }
        }
        public static object GetValue(string path, string paramName, RegistryKey root = null)
        {
            object value = null;
            if (root == null)
            {
                root = RegType.CurrentUser;
            }
            RegistryKey rk = root.OpenSubKey(path, false);
            if (rk != null)
            {
                value = rk.GetValue(paramName, null);
            }
            else
            {
                MessageBox.Show("获取信息时出现错误");
            }
            return value;
        }
        public static void SetValue(string path, string paramName, object value, RegistryKey root = null)
        {
            if (root == null)
            {
                root = Registry.CurrentUser;
            }
            RegistryKey rkey = root.OpenSubKey(path, true);

            rkey.SetValue(paramName, value);
        }
    }
}
