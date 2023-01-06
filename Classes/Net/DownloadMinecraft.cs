using System;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Json = Newtonsoft.Json.Linq;

namespace MielLauncher.Classes.Net
{
    internal static class DownloadMinecraft
    {
        public delegate void ProgressCallBack(int now);
        public delegate void ProgressEndCallBack();
        // string path: .minecraft Directory
        public async static Task DownloadCore(Versions version, string path, string name, ProgressCallBack progressCallBack, ProgressEndCallBack progressEndCallBack)
        {
            string newPath = System.IO.Path.Combine(path, "versions/" + name);
            System.IO.Directory.CreateDirectory(newPath);

            // Download Version.json and request it to collect Version.jar Download Link
            await HttpWeb.DownloadFile(version.url, newPath, name + ".json");
            string versionArg = await HttpWeb.GetWebClient(version.url);
            Json.JObject obj = (Json.JObject) Json.JToken.Parse(versionArg);
            string jarLink = (string) obj["downloads"]["client"]["url"];

            // Download Version.jar
            MultiThreadDownload multiThreadDownload = new MultiThreadDownload();
            multiThreadDownload.Download(jarLink, newPath, name + ".jar", new MultiThreadDownload.ProgressCallBack(progressCallBack), new MultiThreadDownload.ProgressEndCallBack(progressEndCallBack));
        }
        public async static void Download(Versions version, string path, string name, ProgressCallBack progressCallBack, ProgressEndCallBack progressEndCallBack)
        {
            await DownloadCore(version, path, name, progressCallBack, progressEndCallBack);
        }
    }
}
