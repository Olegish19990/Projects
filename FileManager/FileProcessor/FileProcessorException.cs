using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcessor;

public class FileProcessorException: Exception
{
    public FileProcessorException(string? message, Exception? innerException):
        base(message, innerException)
    { }
}
