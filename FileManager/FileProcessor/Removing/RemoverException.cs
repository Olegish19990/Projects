using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcessor.Removing;

internal class RemoverException: FileProcessorException
{
    internal const string UNSUPPORTED_MODE = "Unsupported mode";
    public RemoverException(string? message, Exception? innerException = null):
        base(message, innerException)
    { }
}
