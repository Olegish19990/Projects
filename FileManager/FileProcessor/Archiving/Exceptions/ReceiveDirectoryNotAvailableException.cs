using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcessor.Archiving.Exceptions;

public class ReceiveDirectoryNotAvailableException : ArchiveEngineException
{
    public ReceiveDirectoryNotAvailableException(string? message, Exception? innerException = null) 
        : base(message, innerException)
    {
    }
}
