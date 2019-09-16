using System;
using System.Collections.Generic;
using System.Text;

namespace Facade
{
    class VideoFile
    {
        string FileName { get; set; }
        public VideoFile(string filename)
        {
            FileName = filename;
        }
    }
}
