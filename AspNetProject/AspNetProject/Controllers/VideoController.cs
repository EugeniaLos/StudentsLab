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
    [ApiController]
    [Route("[controller]")]
    public class VideoController : ControllerBase
    {

        private readonly ILogger<VideoController> _logger;

        private readonly YoutubeService _youtube;

        public VideoController(ILogger<VideoController> logger, YoutubeService youtube ) {
            _logger = logger;
            _youtube = youtube;
        }

        [HttpGet]
        public string Get()
        {
            return "There is no search filters";
        }

        [HttpGet("{searchString}")]
        public string Get(string searchString)
        {
            return _youtube.Get(searchString);
        }
    }
}
