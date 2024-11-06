using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcessor.Renaming.Generation;

internal class UuidRuleHandler : IRuleHandler
{
    public string GetValue(FileInfo? file = null)
    {
        return Guid.NewGuid().ToString();
    }
}
