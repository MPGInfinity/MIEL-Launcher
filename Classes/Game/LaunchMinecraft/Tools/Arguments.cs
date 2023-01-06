using ControlzEx.Standard;
using MielLauncher.Classes.Game.User;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MielLauncher.Classes.Game.LaunchMinecraft.Tools
{
    internal static class Arguments
    {
        private static List<string> JsonTokenToArgs(JToken token) // Get string args from JSON text (1.13+)
        {
            List<string> result = new List<string>();
            if (token.Type == JTokenType.String)
            {
                result.Add(token.ToString());
            }
            else
            {
                foreach (var array in token)
                {
                    result.Add(array.ToString());
                }
            }
            return result;
        }
        public static string getArg(MinecraftLaunchConfig launchConfig)
        {
            // Get Minecraft Launch Arguments
            string arg = string.Empty;
            StreamReader argumentJsonFile = File.OpenText(Path.Combine(launchConfig.minecraft.getGamePath(), launchConfig.minecraft.getName() + ".json"));
            JsonTextReader reader = new JsonTextReader(argumentJsonFile);
            JObject argumentRoot = (JObject) JToken.ReadFrom(reader);// Read Json
            // For 1.12-, Read {minecraftArguments string}
            // In 1.12.2, minecraftArguments is this: --username ${auth_player_name} --version ${version_name} --gameDir ${game_directory} --assetsDir ${assets_root} --assetIndex ${assets_index_name} --uuid ${auth_uuid} --accessToken ${auth_access_token} --userType ${user_type} --versionType ${version_type}
            arg = (string) argumentRoot["minecraftArguments"];
            string AssetsIndex = (string) argumentRoot["assetIndex"]["id"];
            arg = arg.
            Replace("${auth_player_name}", "\"" + (launchConfig.auth as AuthClass).playerName + "\"").
            Replace("${version_name}", "\"" + launchConfig.minecraft.getName() + "\"").
            Replace("${game_directory}", "\"" + launchConfig.minecraft.getMinecraftRootPath() + "\"").
            Replace("${assets_root}", "\"" + Path.Combine(launchConfig.minecraft.getMinecraftRootPath(), "assets") + "\"").
            Replace("${assets_index_name}", "\"" + AssetsIndex + "\"").
            Replace("${auth_uuid}", "\"" + (launchConfig.auth as AuthClass).userUuid.ToString("N") + "\"").
            Replace("${auth_access_token}", "\"" + (launchConfig.auth as AuthClass).accessToken.ToString("N") + "\"").
            Replace("${user_type}", "\"" + (launchConfig.auth as AuthClass).userType + "\"").
            Replace("${version_type}", "\"" + "MIEL Launcher" + "\"");
            return arg;
        }
    }
}
