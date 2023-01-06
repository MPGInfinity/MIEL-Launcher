namespace MielLauncher.Classes.Net
{
    public class Versions
    {
        public string id { get; set; }
        public string type { get; set; }
        public string time { get; set; }
        public string url { get; set; }
        public Versions(string id, string type, string time, string url)
        {
            this.id = id;
            this.type = type;
            this.time = time;
            this.url = url;
        }
        public Versions() { }
    }
}
