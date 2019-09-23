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

            if (format == "mp4")
            {
                Codec futureCodec = new MPEG4Codec();
                VideoConverter.Convert(sourceFile, futureCodec);
            }
            else
            {
                Codec futureCodec = new OGGCodec();
                VideoConverter.Convert(sourceFile, futureCodec);
            }
        }
    }
}
