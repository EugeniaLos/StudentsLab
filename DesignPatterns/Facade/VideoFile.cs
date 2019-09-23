using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Facade
{
    public class VideoFile
    {
        public string FileName { get; set; }
        public VideoFile(string filename)
        {
            FileName = filename;
        }
    }
}
