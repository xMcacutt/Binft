using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
     Copyright [2023] [Matthew Cacutt]

       Licensed under the Apache License, Version 2.0 (the "License");
       you may not use this file except in compliance with the License.
       You may obtain a copy of the License at

         http://www.apache.org/licenses/LICENSE-2.0

       Unless required by applicable law or agreed to in writing, software
       distributed under the License is distributed on an "AS IS" BASIS,
       WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
       See the License for the specific language governing permissions and
       limitations under the License.
*/

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
