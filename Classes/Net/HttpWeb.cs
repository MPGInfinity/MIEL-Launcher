using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;

namespace MielLauncher.Classes.Net
{
    internal static class HttpWeb
    {
        public delegate void Func(int now);
        public static async Task<string> GetWebClient(string url)
        {
            using var client = new HttpClient();
            var content = await client.GetStringAsync(url);
            return content;
        }

        public static async Task DownloadFile(string url, string directoryName, string fileName)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync(url);
                using (Stream stream = await response.Content.ReadAsStreamAsync())
                {
                    //获取文件后缀
                    using (FileStream fileStream = new FileStream($"{directoryName}/{fileName}", FileMode.CreateNew))
                    {
                        int BufferSize = 100;
                        byte[] buffer = new byte[BufferSize];
                        int readLength = 0;
                        int length;
                        while ((length = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                        {
                            readLength += length;
                            // 写入到文件
                            fileStream.Write(buffer, 0, length);
                        }
                    }
                }
            }
            catch
            {
            }
        }
        public static async Task DownloadFile(string url, string directoryName, string fileName, Func func, int Total)
        {
            try
            {
                //                func(0);
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync(url);
                using (Stream stream = await response.Content.ReadAsStreamAsync())
                {
                    using (FileStream fileStream = new FileStream($"{directoryName}/{fileName}", FileMode.CreateNew))
                    {
                        int BufferSize = 100;
                        byte[] buffer = new byte[BufferSize];
                        int readLength = 0;
                        int length;
                        //                        MessageBox.Show("准备完成，即将开始下载");
                        while ((length = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                        {
                            MielLauncher.Classes.DispatcherHelper.DoEvents();

                            readLength += length;
                            //                            MessageBox.Show(((int)(10000.0 * readLength / Total)).ToString() + "%%, " + (readLength).ToString() + "/" + Total.ToString());
                            func((int) (10000.0 * readLength / Total));
                            //                            System.Threading.Thread.Sleep(1);
                            // 写入到文件
                            fileStream.Write(buffer, 0, length);
                        }
                    }
                }
                MessageBox.Show("下载成功！");
            }
            catch
            {
                MessageBox.Show("下载失败: MC官方服务器连接超时（屑Mojang）");
            }
        }
    }
}

