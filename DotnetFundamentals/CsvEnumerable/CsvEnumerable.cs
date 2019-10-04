using CsvHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace CsvEnumerable
{
    public class CsvEnumerable<T>: IEnumerable<T>
    {
        private string fileName;
        public CsvEnumerable(string fileName)
        {
            this.fileName = fileName;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new CsvEnumerator<T>(fileName);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }



        private class CsvEnumerator<T>: IEnumerator<T>
        {
            private bool firstRecord;
            private string fileName;
            private StreamReader sr;
            private CsvReader csvReader;

            public CsvEnumerator(string fileName)
            {
                this.fileName = fileName;
                firstRecord = true;
            }

            public bool MoveNext()
            {
                if (firstRecord)
                {
                    sr = new StreamReader(Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, fileName));
                    csvReader = new CsvReader(sr);
                    firstRecord = false;
                }
                return csvReader.Read();
            }


            public T Current
            {
                get
                {
                    return csvReader.GetRecord<T>();
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return csvReader.GetRecord<T>();
                }
            }

            public void Reset()
            {
                csvReader.Dispose();
                sr.Dispose();
            }

            public void Dispose()
            {
                Reset();
            }
        }
    }
}
