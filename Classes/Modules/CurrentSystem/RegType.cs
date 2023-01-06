using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows;

namespace MielLauncher.Classes.Modules.CurrentSystem
{
    internal static class RegType
    {
        public static string Wow64 = Path.Combine(Environment.GetEnvironmentVariable("windir"), "SysWOW64");
        public static bool Bit64 = Directory.Exists(Wow64);
        public static RegistryKey LocalMachine
        {
            get
            {
                RegistryKey key = Registry.LocalMachine;
                string Wow64 = Environment.GetEnvironmentVariable("windir");
                if (Bit64)
                {
                    key = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
                }
                return key;
            }
        }
        public static RegistryKey CurrentUser
        {
            get
            {
                RegistryKey key = Registry.CurrentUser;
                if (Bit64)
                {
                    key = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.CurrentUser, RegistryView.Registry64);
                }
                return key;
            }
        }
    }
}
