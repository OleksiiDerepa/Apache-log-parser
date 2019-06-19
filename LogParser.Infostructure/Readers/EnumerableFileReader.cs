using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

using LogParser.Infrastructure.Validation;

namespace LogParser.Infrastructure.Readers
{
    public class EnumerableFileReader : IEnumerable<string>
    {
        private readonly string _filePath;
        public EnumerableFileReader(string filePath)
        {
            Require.NotEmpty(filePath,()=>$"{filePath} should be specified");
            Require.IsTrue(File.Exists(_filePath),$"Specified file: {_filePath} does not exist");

            _filePath = filePath;
        }
        public IEnumerator<string> GetEnumerator()
        {
            using (var fileStream = new FileStream(_filePath, FileMode.Open, FileAccess.Read))
            {
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        yield return line;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}