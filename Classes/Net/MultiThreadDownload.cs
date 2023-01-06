using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MielLauncher.Classes.Net
{
    class DownloadingInformation
    {
        public int blocks { get; set; }
        public int finished { set; get; }
        public DownloadingInformation(int blocks, int finished)
        {
            this.blocks = blocks;
            this.finished = finished;
        }
    }

    class PartialInformation
    {
        public string url { get; set; }
        public long start { get; set; }
        public long end { set; get; }
        public long pid { get; set; }
        public MultiThreadDownload.ProgressCallBack progressCallBack { get; set; }
        public PartialInformation(string url, long start, long end, long pid, MultiThreadDownload.ProgressCallBack progressCallBack)
        {
            this.url = url;
            this.start = start;
            this.end = end;
            this.pid = pid;
            this.progressCallBack = progressCallBack;
        }

    }

    class DownloadInformation
    {
        public string link { get; set; }
        public string storeDir { get; set; }
        public string fileName { get; set; }
        public MultiThreadDownload.ProgressCallBack progressCallBack { get; set; }
        public MultiThreadDownload.ProgressEndCallBack progressEndCallBack { get; set; }
        public DownloadInformation(string link, string dir, MultiThreadDownload.ProgressCallBack progressCallBack, MultiThreadDownload.ProgressEndCallBack progressEndCallBack)
        {
            this.link = link;
            this.storeDir = dir;
            this.fileName = link.Split('/').Last<string>();
            this.progressCallBack = progressCallBack;
            this.progressEndCallBack = progressEndCallBack;
        }
        public DownloadInformation(string link, string dir, string fileName, MultiThreadDownload.ProgressCallBack progressCallBack, MultiThreadDownload.ProgressEndCallBack progressEndCallBack)
        {
            this.link = link;
            this.storeDir = dir;
            this.fileName = fileName;
            this.progressCallBack = progressCallBack;
            this.progressEndCallBack = progressEndCallBack;
        }


        public override string ToString()
        {
            return string.Format("link:{0} storeDir:{1} fileName:{2}", link, storeDir, fileName);
        }
    }
    class MultiThreadDownload
    {
        public delegate void ProgressCallBack(int now);
        public delegate void ProgressEndCallBack();
        private long DownloadSize;
        private DownloadInformation downloadInformation;
        private DownloadingInformation downloadingInformation;
        private ManualResetEvent manualResetEvent;

        private long getSize(string url)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest) HttpWebRequest.Create(url);
            httpWebRequest.AddRange(0, 1);
            HttpWebResponse response = (HttpWebResponse) httpWebRequest.GetResponse();
            string contentRange = response.Headers.Get("Content-Range");
            long res = long.Parse(contentRange.Split('/').Last<string>());
            response.Close();
            return res;
        }

        private void partialDownload(object param)
        {
            try
            {
                PartialInformation info = param as PartialInformation;
                HttpWebRequest httpWebRequest = (HttpWebRequest) HttpWebRequest.Create(info.url);
                httpWebRequest.AddRange(info.start, info.end);
                HttpWebResponse response = (HttpWebResponse) httpWebRequest.GetResponse();
                var stream = response.GetResponseStream();
                FileStream fstream = new FileStream(Path.Combine(downloadInformation.storeDir, downloadInformation.fileName), FileMode.Open, FileAccess.Write, FileShare.Write);
                fstream.Seek(info.start, SeekOrigin.Begin);
                byte[] content = new byte[65536];
                int len = 0;
                while ((len = stream.Read(content, 0, 65536)) > 0)
                {
                    fstream.Write(content, 0, len);
                    //                    info.func((int) (100.0 * downloadingInformation.finished + len / downloadingInformation.blocks));
                    //                    Console.Write("[Info][Downloader][{0}] {1}/{2}MB, len: {3}, ", info.pid , ++downloadingInformation.finished, downloadingInformation.blocks, len);
                    //                    Console.WriteLine((100.0 * downloadingInformation.finished / downloadingInformation.blocks).ToString("0.00") + "%");
                }
                stream.Close();
                fstream.Close();
                response.Close();
                Console.Write("[Info][Downloader] {0}/{1}MB, ", ++downloadingInformation.finished, downloadingInformation.blocks);
                Console.WriteLine((100.0 * downloadingInformation.finished / downloadingInformation.blocks).ToString("0.00") + "%");
                info.progressCallBack((int) (100.0 * downloadingInformation.finished / downloadingInformation.blocks));
                if (downloadingInformation.finished == downloadingInformation.blocks)
                {
                    manualResetEvent.Set();
                }
            }
            catch
            {
                Console.WriteLine("[Warn][Exception][Downloader] Thread Stop");
            }
        }

        public void DownloadThread(object InfoMation)
        {
            downloadInformation = InfoMation as DownloadInformation;

            long size = getSize(downloadInformation.link);
            DownloadSize = size;
            Console.WriteLine("[Info][Downloader] 总大小：{0}字节", size);
            ThreadPool.SetMinThreads(10, 10);
            ThreadPool.SetMaxThreads(4096, 4096);
            FileStream file = File.Create(downloadInformation.storeDir + "/" + downloadInformation.fileName);
            file.Close();
            long ThreadPartial = 1048576;
            downloadingInformation = new DownloadingInformation((int) Math.Ceiling(size / (double) ThreadPartial), 0);
            for (long i = 0; i < downloadingInformation.blocks; i++)
            {
                var ii = new PartialInformation(downloadInformation.link, ThreadPartial * i, ThreadPartial * (i + 1) - 1, i, downloadInformation.progressCallBack);
                ThreadPool.QueueUserWorkItem(partialDownload, ii);
            }
            manualResetEvent = new ManualResetEvent(false);
            // Wait Download Process
            manualResetEvent.WaitOne();
            Console.WriteLine("[Info][Downloader] 下完力（喜");
            downloadInformation.progressEndCallBack();
        }
        public void Download(string url, string directoryName, string fileName, ProgressCallBack progressCallBack, ProgressEndCallBack progressEndCallBack)
        {
            DownloadInformation downloadInformation = new DownloadInformation(url, directoryName, fileName, progressCallBack, progressEndCallBack);
            Thread thread = new Thread(new ParameterizedThreadStart(DownloadThread));
            thread.Start(downloadInformation);
        }
    }
}
