using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using Microsoft.Extensions.Configuration;
using YoutubeInterfaceLayer;

namespace AspNetProject.Controllers
{
    [Route("Video")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        private readonly ILogger<VideosController> _logger;

        private readonly IConfiguration _configuration;

        public VideosController(ILogger<VideosController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet("{searchString}/{count}")]
        public string Get(string searchString, int count)
        {
            YoutubeLayer youtube = new YoutubeLayer(_configuration);
            return youtube.GetExplicitNumber(searchString, count);
        }
    }
}