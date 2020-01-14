using System;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace YoutubeInterfaceLayer
{
    public class YoutubeService
    {
        private readonly IConfiguration _configuration;

        public YoutubeService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Get(string searchString)
        {
            return GetYoutubeVideoResponse(searchString);
        }

        public string GetExplicitNumber(string searchString, int count)
        {
            return GetYoutubeVideoResponse(searchString, count);
        }

        private string GetYoutubeVideoResponse(string searchString, int count = 5)
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
