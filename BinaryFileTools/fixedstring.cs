﻿using System;
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
    public struct fixedstring
    {
        private byte[] _byteRep;
        private string _stringRep;

        public int Length { get; set; }

        public string Text
        {
            private get { return _stringRep; }
            set
            {
                _stringRep = value;
                byte[] byteRep = Encoding.ASCII.GetBytes(value);
                Array.Resize(ref byteRep, Length);
            }
        }

        public fixedstring(string text, int length)
        {
            _byteRep = new byte[length];
            _stringRep = text;
            Length = length;
        }

        public byte[] GetBytes()
        {
            return _byteRep;
        }

        public override string ToString()
        {
            return _stringRep;
        }
    }
}
