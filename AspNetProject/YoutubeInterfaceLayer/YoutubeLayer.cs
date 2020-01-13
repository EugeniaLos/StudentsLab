using System;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace YoutubeInterfaceLayer
{
    public class YoutubeLayer
    {
        private readonly IConfiguration _configuration;

        public YoutubeLayer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Get(string searchString)
        {
            string key = _configuration.GetSection("YoutubeApiKey").Value;
            string DownloadString = _configuration.GetSection("DownloadString").Value;
            WebClient client = new WebClient();
            int count = 5;
            string downloadString = String.Format(DownloadString, count, key, searchString);
            string reply = client.DownloadString(downloadString);
            return reply;
        }

        public string GetExplicitNumber(string searchString, int count)
        {
            string key = _configuration.GetSection("YoutubeApiKey").Value;
            string DownloadString = _configuration.GetSection("DownloadString").Value;
            WebClient client = new WebClient();
            string downloadString = String.Format(DownloadString, count, key, searchString);
            string reply = client.DownloadString(downloadString);
            return reply;
        }
    }
}
