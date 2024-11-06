using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcessor.Renaming.Generation;

internal class RuleItem
{
    public string Signature { get; set; } = null!;
    public string Shortcut { get; set; } = null!;
    public string Options { get; set; } = null!;
}
