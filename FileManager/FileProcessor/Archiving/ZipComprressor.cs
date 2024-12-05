using FileProcessor.Archiving.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DirectoryNotFoundException = FileProcessor.Archiving.Exceptions.DirectoryNotFoundException;

namespace FileProcessor.Archiving;

internal class ZipComprressor : ICompressor
{
    private const string EXTENSION = ".zip";

    public CompressionLevel CompressionLevel { get; set; } = CompressionLevel.SmallestSize;
    public bool IncludeParentDirectory { get; set; } = false;

    public string CompressDir(string target, string receiveDir, string archiveFileName)
    {
        if (!Directory.Exists(target))
            throw new DirectoryNotFoundException($"Directory {target} not found");

        string outFile = Path.Combine(receiveDir, $"{archiveFileName}{EXTENSION}");

        if (File.Exists(outFile))
            throw new FileAlreadyExistsException($"File {outFile} already exists");

        ZipFile.CreateFromDirectory(target, outFile, CompressionLevel, IncludeParentDirectory);
       
        return outFile;
    }

    public void DecompressDir()
    {
        throw new NotImplementedException();
    }
}
