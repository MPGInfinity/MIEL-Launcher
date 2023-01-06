using ControlzEx.Standard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MielLauncher.Classes.Game.User.AuthClasses
{
    internal class AuthLegacy : AuthClass
    {
        public AuthLegacy(string playerName)
        {
            this.userType = "Legacy";
            this.playerName = playerName;
            // Calculate UUID
            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(Encoding.Default.GetBytes(playerName));
            this.userUuid = this.accessToken = new Guid(hash);
        }
    }
}
