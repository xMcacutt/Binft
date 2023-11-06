using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
