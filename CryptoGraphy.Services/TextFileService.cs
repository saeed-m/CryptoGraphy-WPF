using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoGraphy.Services
{
    /// <summary>
    /// Contains methods for working with text files
    /// </summary>
    public static class TextFileService
    {
        /// <summary>
        /// Open the text file and find the Count of lines in it
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns>int Count of lines in file </returns>
        public static async Task<int> GetLinesCount(string filepath)
        {
            int lineCount = 0;
             using (StreamReader sr = new StreamReader(filepath))
            {
                string line;
                while ((line = await sr.ReadLineAsync()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        lineCount++;
                    }
                }
            }
            return lineCount;
        }
    }
}
