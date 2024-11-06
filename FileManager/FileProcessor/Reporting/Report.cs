using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcessor.Reporting;

public class Report<T>
    where T: FileSystemInfo
{
    public List<ReportItem<T>> Items { get; set; } = new List<ReportItem<T>>();

    public void pushSuccess(T fSInfo)
    {
        Items.Add(new ReportItem<T>(fSInfo));
    }

    public void pushError(T fSInfo, Exception exception)
    {
        Items.Add(new ReportItem<T>(fSInfo, true, exception));
    }

    public Report<T> Merge(Report<T> report)
    {
        Items.AddRange(report.Items);

        return this;
    }
}
