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
            return ConnectToWebClient(searchString);
        }

        public string GetExplicitNumber(string searchString, int count)
        {
            return ConnectToWebClient(searchString, count);
        }

        private string ConnectToWebClient(string searchString, int count = 5)
        {
            string key = _configuration.GetSection("YoutubeApiKey").Value;
            string confDownloadString = _configuration.GetSection("DownloadString").Value;
            WebClient client = new WebClient();
            string downloadString = String.Format(confDownloadString, count, key, searchString);
            string reply = client.DownloadString(downloadString);
            return reply;
        }
    }
}
