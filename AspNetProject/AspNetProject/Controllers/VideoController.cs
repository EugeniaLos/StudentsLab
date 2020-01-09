using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace AspNetProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideoController : ControllerBase
    {

        private readonly ILogger<VideoController> _logger;

        private readonly IConfiguration _configuration;

        public VideoController(ILogger<VideoController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public string Get()
        {
            return "There is no search filters";
        }

        [HttpGet("{searchString}")]
        public string Get(string searchString)
        {
            string KEY = _configuration.GetSection("YoutubeApiKey").Value;
            WebClient client = new WebClient();
            string reply = client.DownloadString($"https://www.googleapis.com/youtube/v3/search?part=snippet&key={KEY}&q={searchString}&type=video");
            return reply;
        }

        [HttpGet("{searchString}/{count}")]
        public string Get(string searchString,int count)
        {
            string KEY = _configuration.GetSection("YoutubeApiKey").Value;
            WebClient client = new WebClient();
            string reply = client.DownloadString($"https://www.googleapis.com/youtube/v3/search?part=snippet&maxResults={count}&key={KEY}&q={searchString}&type=video");
            return reply;
        }
    }
}
