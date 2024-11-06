using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcessor.Archiving.Exceptions;

public class ArchiveEngineException : FileProcessorException
{
    internal const string UNSUPPORTED_MODE = "Unsupported mode";
    public ArchiveEngineException(string? message, Exception? innerException = null) 
        : base(message, innerException)
    {}
}
