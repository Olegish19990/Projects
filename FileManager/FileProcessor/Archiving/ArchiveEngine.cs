using FileProcessor.Archiving.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DirectoryNotFoundException = FileProcessor.Archiving.Exceptions.DirectoryNotFoundException;

namespace FileProcessor.Archiving;

public class ArchiveEngine
{
    private ICompressor compressor = null!;


    private void SetCompressor(ArchiveEngineMode mode)
    {
        compressor = mode switch
        {
            ArchiveEngineMode.Zip => new ZipComprressor(),

            _ => throw new ArchiveEngineException(ArchiveEngineException.UNSUPPORTED_MODE),
        };
    }

    public ArchiveEngine(ArchiveEngineMode mode = ArchiveEngineMode.Zip)
    {
        SetCompressor(mode);
    }

    public void SetMode(ArchiveEngineMode mode = ArchiveEngineMode.Zip)
    {
        SetCompressor(mode);
    }


    public string CompressDirectory(string path, string? receiveDir = "", string archiveFileName = "")
    {
        if (!Directory.Exists(path))
            throw new DirectoryNotFoundException($"Directory {path} not found");

        path = Path.TrimEndingDirectorySeparator(path);

        if (string.IsNullOrEmpty(receiveDir))
        {
            receiveDir = Directory.GetParent(path)?.FullName;

            if (receiveDir is null)
                throw new ReceiveDirectoryNotAvailableException($"Directory {receiveDir} is not available");
        }

        archiveFileName = string.IsNullOrEmpty(archiveFileName) ? Path.GetFileName(path) : archiveFileName;

        return compressor.CompressDir(path, receiveDir, archiveFileName);
    }
}
