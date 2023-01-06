using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using Json = Newtonsoft.Json.Linq;
using Net = MielLauncher.Classes.Net;

namespace MielLauncher.Classes.Net
{
    internal static class GetVersion
    {
        public async static Task<List<Versions>> GetNow()
        {
            try
            {
                List<Versions> res = new List<Versions>();
                string jsonText = await Net.HttpWeb.GetWebClient("https://piston-meta.mojang.com/mc/game/version_manifest.json");
                Json.JObject obj = (Json.JObject) Json.JToken.Parse(jsonText);
                foreach (var v in obj["versions"])
                {
                    Versions version = new Versions((string) v["id"], (string) v["type"], (string) v["releaseTime"], (string) v["url"]);
                    res.Add(version);
                }
                return res;
            }
            catch (Exception ex)
            {
                MessageBox.Show("版本列表获取失败: " + ex.Message + ", ex.ToString = " + ex.ToString());
                return new List<Versions>();
            }
        }
    }
}
