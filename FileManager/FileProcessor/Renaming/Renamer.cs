using FileProcessor.Renaming.Generation;
using FileProcessor.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcessor.Renaming;

public class Renamer
{
    public FSObjectContainer Container { get; private set; }
    private StringGenerator Generator { get; set; }

    public Renamer(FSObjectContainer container)
    {
        Container = container;
        Generator = new StringGenerator();
    }

    public void AddRuleHanndler(string shortcut, IRuleHandler handler)
    {
        Generator.AddRuleHandler(shortcut, handler);
    }

    public Report<FileInfo> RenameFiles(string generatePattern)
    {
        Report<FileInfo> report = new Report<FileInfo>();
        
        Generator.SetReplacePattern(generatePattern);

        Container.Files.ForEach(f =>
        {
            try
            {
                string generatedName = Generator.GetNext(f);

                string fileName = $"{generatedName}{f.Extension}";

                string path = Path.Combine(f.Directory?.FullName ?? "", fileName);

                f.MoveTo(path);

                report.pushSuccess(f);
            }
            catch (Exception ex)
            {
                report.pushError(f, ex);
            }
        });

        return report;

    }

}
