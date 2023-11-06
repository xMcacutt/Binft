using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binft
{
    public class Binf
    {
        private FileStream _stream;
        private bool _littleEndian;
        public long Position => _stream.Position;

        internal Binf(bool littleEndian, FileStream stream)
        {
            _littleEndian = littleEndian;
            _stream = stream;
        }

        ///<summary>Closes the binary file stream.</summary>
        public void Close()
        {
            _stream.Close();
        }

        ///<summary>Reads an unsigned integer from the file, advancing the stream by 4 bytes.</summary>
        ///<returns>The unsigned integer read from the binf stream.</returns>
        public uint ReadUInt()
        {
            var buffer = new byte[4];
            _stream.Read(buffer, 0, 4);
            return DataRead.ToUInt32(buffer, 0, _littleEndian);
        }

        ///<summary>Reads an integer from the file, advancing the stream by 4 bytes.</summary>
        ///<returns>The integer read from the binf stream.</returns>
        public int ReadInt()
        {
            var buffer = new byte[4];
            _stream.Read(buffer, 0, 4);
            return DataRead.ToInt32(buffer, 0, _littleEndian);
        }

        ///<summary>Reads an unsigned short from the file, advancing the stream by 2 bytes.</summary>
        ///<returns>The unsigned short read from the binf stream.</returns>
        public ushort ReadUShort()
        {
            var buffer = new byte[2];
            _stream.Read(buffer, 0, 2);
            return DataRead.ToUInt16(buffer, 0, _littleEndian);
        }

        ///<summary>Reads a short from the file, advancing the stream by 2 bytes.</summary>
        ///<returns>The short read from the binf stream.</returns>
        public short ReadShort()
        {
            var buffer = new byte[2];
            _stream.Read(buffer, 0, 2);
            return DataRead.ToInt16(buffer, 0, _littleEndian);
        }

        ///<summary>Reads the next byte from the file, advancing the stream by 1 bytes.</summary>
        ///<returns>The byte read from the binf stream.</returns>
        public byte ReadByte()
        {
            return (byte)_stream.ReadByte();
        }

        ///<summary>Reads an unsigned long from the file, advancing the stream by 8 bytes.</summary>
        ///<returns>The unsigned long read from the binf stream.</returns>
        public ulong ReadULong()
        {
            var buffer = new byte[8];
            _stream.Read(buffer, 0, 8);
            return DataRead.ToUInt64(buffer, 0, _littleEndian);
        }

        ///<summary>Reads a long from the file, advancing the stream by 8 bytes.</summary>
        ///<returns>The long read from the binf stream.</returns>
        public long ReadLong()
        {
            var buffer = new byte[8];
            _stream.Read(buffer, 0, 8);
            return DataRead.ToInt64(buffer, 0, _littleEndian);
        }

        ///<summary>Reads a float from the file, advancing the stream by 4 bytes.</summary>
        ///<returns>The float read from the binf stream.</returns>
        public float ReadFloat()
        {
            var buffer = new byte[4];
            _stream.Read(buffer, 0, 4);
            return DataRead.ToSingle(buffer, 0, _littleEndian);
        }

        ///<summary>Reads a double from the file, advancing the stream by 8 bytes.</summary>
        ///<returns>The double read from the binf stream.</returns>
        public double ReadDouble()
        {
            var buffer = new byte[8];
            _stream.Read(buffer, 0, 8);
            return DataRead.ToDouble(buffer, 0, _littleEndian);
        }

        ///<summary>Reads a byte array from the file, advancing the stream by as many bytes as the length specified.</summary>
        ///<param name="length">The number of bytes to read.</param>
        ///<returns>An array of size "length" containing the read bytes.</returns>
        public byte[] ReadBytes(int length)
        {
            var buffer = new byte[length];
            _stream.Read(buffer, 0, length);
            return buffer;
        }

        ///<summary>Reads a null terminated string from the file. The number of bytes advanced through the stream will match the length of the string found.</summary>
        ///<returns>A string read from the current position in the file up to the first null (0x0) byte.</returns>
        public string ReadString()
        {
            StringBuilder sb = new();
            byte b = ReadByte();
            while (b != 0x0)
            {
                sb.Append((char)b);
                b = ReadByte();
            }
            Skip(-1);
            return sb.ToString();
        }

        ///<summary>Reads a char terminated string from the file. The number of bytes advanced through the stream will match the length of the string found.</summary>
        ///<param name="x">The char used to terminate the string.</param>
        ///<returns>A string read from the current position in the file up to the first terminating byte.</returns>
        public string ReadString(char x)
        {
            StringBuilder sb = new();
            byte b = ReadByte();
            while ((char)b != x)
            {
                sb.Append((char)b);
                b = ReadByte();
            }
            Skip(-1);
            return sb.ToString();
        }

        ///<summary>Reads a null terminated string from the file. The number of bytes advanced through the stream is set by the length param.</summary>
        ///<param name="length">Sets the exact length to advance through the stream (the total space the string COULD use).</param>
        ///<returns>A fixedstring read from the current position in the file up to the first null (0x0) byte.</returns>
        public fixedstring ReadString(int length)
        {
            var stringData = ReadBytes(length);
            StringBuilder sb = new();
            int i = 0;
            byte b = stringData[i];
            i++;
            while (b != 0x0 && i < length)
            {
                sb.Append((char)b);
                b = stringData[i];
                i++;
            }
            return new fixedstring(sb.ToString(), length);
        }

        ///<summary>Reads a null terminated string from the file. The number of bytes advanced through the stream is set by the length param.</summary>
        ///<param name="length">Sets the exact length to advance through the stream (the total space the string COULD use).</param>
        ///<param name="x">The char used to terminate the string.</param>
        ///<returns>A fixedstring read from the current position in the file up to the first terminating byte.</returns>
        public fixedstring ReadString(int length, char x)
        {
            var stringData = ReadBytes(length);
            StringBuilder sb = new();
            int i = 0;
            byte b = stringData[i];
            i++;
            while ((char)b != x && i < length)
            {
                sb.Append((char)b);
                b = stringData[i];
                i++;
            }
            return new fixedstring(sb.ToString(), length);
        }

        ///<summary>Reads a float array from the file, advancing the stream by as many bytes as the count specified multiplied by 4.</summary>
        ///<param name="count">The number of floats to read.</param>
        ///<returns>A float array of size "count" containing the read floats.</returns>
        public float[] ReadFloatArray(int count)
        {
            float[] floatData = new float[count];
            for(int i = 0; i < count; i++)
            {
                floatData[i] = ReadFloat();
            }
            return floatData;
        }

        ///<summary>Reads an int array from the file, advancing the stream by as many bytes as the count specified multiplied by 4.</summary>
        ///<param name="count">The number of ints to read.</param>
        ///<returns>An int array of size "count" containing the read floats.</returns>
        public int[] ReadIntArray(int count)
        {
            int[] intData = new int[count];
            for(int i = 0; i < count; i++)
            {
                intData[i] = ReadInt();
            }
            return intData;
        }

        /// <summary>Writes byte representation of data to the binf file.</summary>
        /// <typeparam name="T">Interprets the type of data to be written.</typeparam>
        /// <param name="data">The data to be written.</param>
        public void Write<T>(T data)
        {
            _stream.Write(DataRead.GetBytes(data, _littleEndian));
        }

        /// <summary>Writes byte representation of data to the binf file at a specified position then returns to the current position in the stream.</summary>
        /// <typeparam name="T">Interprets the type of data to be written.</typeparam>
        /// <param name="data">The data to be written.</param>
        public void WriteToPosition<T>(T data, int address)
        {
            var pos = Position;
            GoTo(address);
            _stream.Write(DataRead.GetBytes(data, _littleEndian));
            GoTo(Position);
        }

        /// <summary>Seeks to a position in the binf stream from the start of the file.</summary>
        /// <param name="address">The position to seek to.</param>
        public void GoTo(int address)
        {
            _stream.Seek(address, SeekOrigin.Begin);
        }

        /// <summary>Seeks to a position in the binf stream from the start of the file.</summary>
        /// <param name="address">The position to seek to.</param>
        public void GoTo(long address)
        {
            _stream.Seek(address, SeekOrigin.Begin);
        }

        /// <summary>Seeks to a position in the binf stream from the current position in the file.</summary>
        /// <param name="count">The number of bytes to seek past.</param>
        public void Skip(int count)
        {
            _stream.Seek(count, SeekOrigin.Current);
        }
    }
}
