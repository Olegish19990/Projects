using FileProcessor.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcessor.Removing;

public class Remover
{
    public FSObjectContainer Container { get; set; }

    public Remover(FSObjectContainer container)
    {
        Container = container;
    }

    public Report<FileSystemInfo> Execute(RemoverMode mode) => mode switch
    {
        RemoverMode.Files => RemoveItems<FileInfo>(),
        RemoverMode.Directories => RemoveItems<DirectoryInfo>(),
        RemoverMode.All => RemoveAll(),

        _ => throw new RemoverException(RemoverException.UNSUPPORTED_MODE),
    };

    private Report<FileSystemInfo> RemoveItems<T>()
        where T : FileSystemInfo
    {
        Report<FileSystemInfo> report = new Report<FileSystemInfo>();

        List<FileSystemInfo> items
            = typeof(T) == typeof(FileInfo)
            ? new List<FileSystemInfo>(Container.Files)
            : new List<FileSystemInfo>(Container.Dirs);

        foreach (T item in items)
        {
            try
            {
                if (item is DirectoryInfo d)
                    d.Delete(true);
                else
                    item.Delete();

                report.pushSuccess(item);
            }
            catch (Exception ex)
            {
                report.pushError(item, ex);
            }
        }

        return report;
    }

    //private Report<FileSystemInfo> RemoveFiles()
    //{
    //    Report<FileSystemInfo> report = new Report<FileSystemInfo>();

    //    foreach(FileInfo file in Container.Files)
    //    {
    //        try
    //        {
    //            file.Delete();

    //            report.pushSuccess(file);
    //        }
    //        catch (Exception ex)
    //        {
    //            report.pushError(file, ex);
    //        }
    //    }

    //    return report;
    //}

    //private Report<FileSystemInfo> RemoveDirectories()
    //{
    //    Report<FileSystemInfo> report = new Report<FileSystemInfo>();

    //    foreach (DirectoryInfo dir in Container.Dirs)
    //    {
    //        try
    //        {
    //            dir.Delete();

    //            report.pushSuccess(dir);
    //        }
    //        catch (Exception ex)
    //        {
    //            report.pushError(dir, ex);
    //        }
    //    }

    //    return report;
    //}

    private Report<FileSystemInfo> RemoveAll()
    {
        Report<FileSystemInfo> filesReport = RemoveItems<FileInfo>();
        Report<FileSystemInfo> dirsReport = RemoveItems<DirectoryInfo>();

        return filesReport.Merge(dirsReport);
    }
}
