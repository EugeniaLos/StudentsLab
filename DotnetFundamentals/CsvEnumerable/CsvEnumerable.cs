using CsvHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace CsvEnumerable
{
    public class CsvEnumerable: IEnumerable
    {
        private List<string> records = new List<string>();
        public CsvEnumerable(string fileName)
        {
            using (StreamReader sr = new StreamReader(Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, fileName)))
            {
                String line;

                while ((line = sr.ReadLine()) != null)
                {
                    records.Add(line);
                }
            }
        }

        public IEnumerator GetEnumerator()
        {
            return new CsvEnumerator(records);
        }

        private class CsvEnumerator: IEnumerator
        {
            private string[] records;
            private int position = -1;

            public CsvEnumerator(List<string> records)
            {
                this.records = new string[records.Count];
                records.CopyTo(this.records);
            }

            public object Current
            {
                get
                {
                    if (position != -1 || position < records.Length)
                        return records[position];
                    return "No record";
                }
            }

            public bool MoveNext()
            {
                if (position < records.Length - 1)
                {
                    position++;
                    return true;
                }
                else
                    return false;
            }

            public void Reset()
            {
                position = -1;
            }
        }
    }
}
