using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcessor.Renaming.Generation;

internal class OldNameRuleHandler : IRuleHandler
{
    public string GetValue(FileInfo? file = null)
    {
        return file is not null ? Path.GetFileNameWithoutExtension(file.Name) : string.Empty;
    }
}
