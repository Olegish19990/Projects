using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcessor.Renaming;

public class RenamerException : FileProcessorException
{
    internal const string DUPLICATE_SHORTCUT = "Duplicate shortcut";
    public RenamerException(string? message, Exception? innerException = null):
        base(message, innerException)
    { }
}
