using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcessor.Archiving.Exceptions;

public class FileAlreadyExistsException : ArchiveEngineException
{
    public FileAlreadyExistsException(string? message, Exception? innerException = null) 
        : base(message, innerException)
    {}
}
