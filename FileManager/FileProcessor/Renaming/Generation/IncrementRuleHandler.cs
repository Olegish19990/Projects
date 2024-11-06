using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcessor.Renaming.Generation;

internal class IncrementRuleHandler : IRuleHandler, IWithOptions
{
    private int from;
    private int step;
    private int current;

    private string optionsString = string.Empty;
    public string OptionsString
    {
        get
        { 
            return optionsString;
        }
        set
        { 
            optionsString = value;
            ParseOptions();
        }
    }

    private void ParseOptions()
    {
        string[] items = optionsString.Split(',');

        from = Convert.ToInt32(items[0]);
        step = Convert.ToInt32(items[1]);

        current = from;
    }

    public string GetValue(FileInfo? file = null)
    {
        string result = current.ToString();

        current += step;

        return result;
    }

    
}
