using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcessor.Reporting;

public class ReportItem<T>
    where T : FileSystemInfo
{
    public T FSInfo { get; set; }
    public bool Failed { get; set; } = false;
    public Exception? Exception { get; set; } = null;
    public ReportItem(T fSInfo, bool failed = false, Exception? exception = null)
    {
        FSInfo = fSInfo;
        Failed = failed;
        Exception = exception;
    }

}
