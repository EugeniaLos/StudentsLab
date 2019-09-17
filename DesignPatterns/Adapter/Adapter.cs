using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace DesignPatterns.Adapter
{
    public class Adapter
    {
        private void ConvertXMLtoJson()
        {

        }
        public void GetOldestBook()
        {
            Library.GetBoooksXml();
            ConvertXMLtoJson();
            BooksAnalyzer.GetOldestBook();
        }

    }
}
