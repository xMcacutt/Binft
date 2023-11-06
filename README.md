![Binft](/binft.png)

# Binft - Binary File Tools

Binft was created to assist with reading and writing data to filestreams when working with binary files for reverse engineering and researching file formats. This library provides a very high level api to make repetitive coding patterns in reading and writing data significantly shorter. 

There are three main classes involved in the function of the API. 

## Binft

The <span style="color:mediumseagreen">Binft</span> class provides static methods for creating and opening filestreams wrapped in <span style="color:mediumseagreen">Binf</span> objects which can then be accessed through the public methods belonging to the <span style="color:mediumseagreen">Binf</span> object.

```csharp
//CREATE NEW FILE AT FILEPATH WITH BINF OBJECT FOR ACCESS
public static void Main(string[] args){
    bool isLittleEndian = true;
    if(!File.Exists(args[0])) return;
    string filePath = args[0];
    Binf binf = Binft.CreateBinf(filePath, isLittleEndian);
}


//OPEN EXISTING FILE FROM FILEPATH WITH BINF OBJECT FOR ACCESS
public static void Main(string[] args){
    bool isLittleEndian = false;
    string filePath = args[0];
    Binf binf = Binft.OpenBinf(filePath, isLittleEndian);
}
```

## DataRead

The <span style="color:mediumseagreen">DataRead</span> class is not visible to the end user. It provides a variable endian implementation of the <span style="color:mediumseagreen">BitConverter</span> class. The endianness of the binary file is specified in the <span style="color:khaki">OpenBinf</span> or <span style="color:khaki">CreateBinf</span> methods belonging to <span style="color:mediumseagreen">Binft</span>. These methods access the <span style="color:mediumseagreen">Binf</span> constructor which sets the endiannes of the file. The endianness boolean is passed to all of the <span style="color:mediumseagreen">DataRead</span> methods so after the create or open method, no attention needs to be given to the endianness when processing data. 

When writing data, the following data types can be provided to the <span style="color:khaki">Write</span> method:

```csharp
int
uint
short
ushort
long
ulong
string
fixedstring //custom type with a fixed length
byte
double
float
int[]
short[]
long[]
float[]
double[]
byte[]
```

## FixedString

The <span style="color:mediumturquoise">fixedstring</span> is a custom struct designed to deal with files which have a set capacity for strings such as in a name table. When defining a <span style="color:mediumturquoise">fixedstring</span>, the size of the string must be given along with the text. The fixed string exposes two methods to the user, the <span style="color:khaki">GetBytes</span> method provides the byte representation of the string while the <span style="color:khaki">ToString</span> override gives the string representation. The length can be set dynamically using the <span style="color:plum">Length</span> property and will resize the byte array for the byte representation. The text can be set dynamically using the <span style="color:plum">Text</span> property but can only be gotten with the <span style="color:khaki">ToString</span> override which is used implicitly if the <span style="color:mediumturquoise">fixedstring</span> is passed when a string is expected.

Example:

```csharp
//READ FIXED STRINGS FROM FILE
int stringLength = 0x20;
for(int i = 0; i < stringCount; i++){
    fixedstring fString = binf.ReadString(stringLength);
}

//CREATE FIXED STRING, EDIT PROPERTIES, PRINT, AND WRITE TO FILE
fixedstring fString = new fixedstring("Hello World", 0x18);
fString.Length = 0x20;
fString.Text = "I Changed My Mind";
Console.WriteLine(fString);
byte[] bytes = fString.GetBytes();
binf.Write(fString);
```

## Binf

The <span style="color:mediumseagreen">Binf</span> class is the general purpose binary file class. Its constructor is not exposed so the object must be created through the <span style="color:mediumseagreen">Binft</span> methods described above. The <span style="color:mediumseagreen">Binf</span> instance methods are exposed and can be used to shorten common coding patterns associated with reading and writing data to a binary file. When a <span style="color:mediumseagreen">Binf</span> is instantiated, a <span style="color:mediumseagreen">FileStream</span> is created and associated with the <span style="color:mediumseagreen">Binf</span> object. The <span style="color:mediumseagreen">FileStream</span> is not directly exposed but the following methods allow for manipulation of the stream:

#### Methods

| Method                                                      | Description                                                                                                                               |
| ----------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------- |
| <span style="color:khaki">Close</span>()                    | Closes the binary file stream.                                                                                                            |
| <span style="color:khaki">GoTo</span>(int)                  | Seeks to a position in the binary stream from the start of the file.                                                                      |
| <span style="color:khaki">GoTo</span>(long)                 | Seeks to a position in the binary stream from the start of the file.                                                                      |
| <span style="color:khaki">ReadByte</span>()                 | Reads the next byte from the file, advancing the stream by 1 byte.                                                                        |
| <span style="color:khaki">ReadBytes</span>(int)             | Reads a byte array from the file, advancing the stream by the specified length.                                                           |
| <span style="color:khaki">ReadDouble</span>()               | Reads a double from the file, advancing the stream by 8 bytes.                                                                            |
| <span style="color:khaki">ReadFloat</span>()                | Reads a float from the file, advancing the stream by 4 bytes.                                                                             |
| <span style="color:khaki">ReadFloatArray</span>(int)        | Reads a float array from the file, advancing the stream by the specified count multiplied by 4.                                           |
| <span style="color:khaki">ReadInt</span>()                  | Reads an integer from the file, advancing the stream by 4 bytes.                                                                          |
| <span style="color:khaki">ReadIntArray</span>(int)          | Reads an int array from the file, advancing the stream by the specified count multiplied by 4.                                            |
| <span style="color:khaki">ReadLong</span>()                 | Reads a long from the file, advancing the stream by 8 bytes.                                                                              |
| <span style="color:khaki">ReadShort</span>()                | Reads a short from the file, advancing the stream by 2 bytes.                                                                             |
| <span style="color:khaki">ReadString</span>()               | Reads a null-terminated string from the file. The number of bytes advanced through the stream matches the length of the string found.     |
| <span style="color:khaki">ReadString</span>(char)           | Reads a char-terminated string from the file. The number of bytes advanced through the stream matches the length of the string found.     |
| <span style="color:khaki">ReadString</span>(int)            | Reads a null-terminated string from the file. The number of bytes advanced through the stream is set by the length parameter.             |
| <span style="color:khaki">ReadString</span>(int, char)      | Reads a null-terminated string from the file. The number of bytes advanced through the stream is set by the length parameter.             |
| <span style="color:khaki">ReadUInt</span>()                 | Reads an unsigned integer from the file, advancing the stream by 4 bytes.                                                                 |
| <span style="color:khaki">ReadULong</span>()                | Reads an unsigned long from the file, advancing the stream by 8 bytes.                                                                    |
| <span style="color:khaki">ReadUShort</span>()               | Reads an unsigned short from the file, advancing the stream by 2 bytes.                                                                   |
| <span style="color:khaki">Skip</span>(int)                  | Seeks to a position in the binary stream from the current position in the file.                                                           |
| <span style="color:khaki">Write<T></span>(T)                | Writes the byte representation of data to the binary file.                                                                                |
| <span style="color:khaki">WriteToPosition<T></span>(T, int) | Writes the byte representation of data to the binary file at a specified position and then returns to the current position in the stream. |

#### Properties

The <span style="color:mediumseagreen">Binf</span> class only contains one property, <span style="color:plum">Position</span>, which can be used to get the current position of the stream in the file.

## Examples

For a comprehensive archive extraction example, the DIP Tools extraction class has been updated to use the binft package. 
[DIP Tools Extractor.cs]{https://github.com/xMcacutt/Yukes-DIP-Tools/blob/master/DIP%20Extractor/Extractor.cs}

## Future Plans

The goal with binf is to begin implementing common file format structures to aid in their reversal. The primary format goals are

- Archives

- Images

- 3D Models

- Audio

- Video

These will come as separate packages but will also all be pulled into the main binft nuget. For example, binft.arch (binary file tools - acrhive).

## Author

This package was created by Matthew Cacutt. 

You can find me on [Github](https://github.com/xMcacutt/) or join my [Discord](https://discord.gg/qkPHxEEczd "https://discord.gg/qkPHxEEczd").




