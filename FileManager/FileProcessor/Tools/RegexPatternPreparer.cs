using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcessor.Tools;

internal static class RegexPatternPreparer
{
    private static Dictionary<string, string> mapTokens = new Dictionary<string, string>()
    {
        {".", @"\." },
        {"^", @"\^" },
        {"}", @"\}" },
        {"{", @"\{" },
        {"[", @"\[" },
        {"]", @"\]" },
        {"(", @"\(" },
        {")", @"\)" },
        {"+", @"\+" },
        {"*", ".*" },
        {"?", "." },
    };

    public static string PreparePattern(string rawString)
    {
        string pattern = rawString;

        foreach(KeyValuePair<string, string> item in mapTokens)
            pattern = pattern.Replace(item.Key, item.Value);

        return pattern;
    }
}
