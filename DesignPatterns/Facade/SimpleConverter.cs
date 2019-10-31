using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Facade
{
    public class SimpleConverter
    {
        public void Convert(string filename, string format)
        {
            VideoFile sourceFile = new VideoFile(filename);
            Codec futureCodec;
            if (format == "mp4")
            {
                futureCodec = new MPEG4Codec();
            }
            else
            {
                futureCodec = new OGGCodec();
            }
            VideoConverter.Convert(sourceFile, futureCodec);
        }
    }
}

