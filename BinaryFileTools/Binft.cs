using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binft
{
    public class Binft
    {
        /// <summary>
        /// Create a new binf file and associated stream.
        /// </summary>
        /// <param name="path">The file path for the newly created file.</param>
        /// <param name="littleEndian">Whether the data written to the file should be written as little or big endian.</param>
        /// <returns>A binf file object used to write data to a private file stream.</returns>
        public static Binf CreateBinf(string path, bool littleEndian = true)
        {
            var fs = File.Create(path);
            return new Binf(littleEndian, fs);
        }

        /// <summary>
        /// Open a file for the binf object and associated stream.
        /// </summary>
        /// <param name="path">The file path for the file to open in the stream.</param>
        /// <param name="littleEndian">Whether the data read from the file is little or big endian.</param>
        /// <returns>A binf file object used to read data from a private file stream.</returns>
        public static Binf OpenBinf(string path, bool littleEndian = true)
        {
            var fs = File.OpenRead(path);
            return new Binf(littleEndian, fs);
        }
    }
}
