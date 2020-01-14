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

        private readonly YoutubeService _youtube;

        public VideosController(ILogger<VideosController> logger, YoutubeService youtube)
        {
            _logger = logger;
            _youtube = youtube;
        }

        [HttpGet("{searchString}/{count}")]
        public string Get(string searchString, int count)
        {
            return _youtube.GetExplicitNumber(searchString, count);
        }
    }
}