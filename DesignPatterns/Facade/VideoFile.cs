using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Facade
{
    public class VideoFile
    {
        string FileName { get; set; }
        public VideoFile(string filename)
        {
            FileName = filename;
        }
    }
}
