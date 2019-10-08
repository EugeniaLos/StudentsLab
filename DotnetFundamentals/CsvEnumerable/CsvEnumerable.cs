using CsvHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace CsvEnumerable
{
    public class CsvEnumerable<T>: IEnumerable<T>, IEnumerator<T>
    {
        private string filePath;
        private StreamReader streamReader;
        private CsvReader csvReader;
        private bool firstRecord;

        public CsvEnumerable(string filePath)
        {
            this.filePath = filePath;
            firstRecord = true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public bool MoveNext()
        {
            if (firstRecord)
            {
                streamReader = new StreamReader(filePath);
                csvReader = new CsvReader(streamReader);
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
            streamReader.Dispose();
            firstRecord = true;
         }

         public void Dispose()
         {
            Reset();
         }
    }
}
