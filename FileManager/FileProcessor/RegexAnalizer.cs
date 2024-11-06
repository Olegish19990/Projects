using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileProcessor;

internal class RegexAnalizer
{
    private Dictionary<string, string> map = new Dictionary<string, string>()
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

    public List<T> Filter<T>(T[] items, string[] masks)
        where T: FileSystemInfo
    {
        List<T> result = new List<T>();

        string pattern = PreparePattern(masks);

        Regex regex = new Regex(pattern);

        foreach(T item in items)
        {
            if (regex.IsMatch(item.Name))
                result.Add(item);
        }

        return result;
    }



    private string PreparePattern(string[] masks)
    {
        List<string> patterns = new List<string>();

        foreach (string mask in masks)
            patterns.Add(ConvertMaskToPattern(mask));

        return $"{String.Join("|", patterns)}";
    }

    private string ConvertMaskToPattern(string mask)
    {
        foreach(KeyValuePair<string, string> item in map)
            mask = mask.Replace(item.Key, item.Value);

        return $"^{mask}$";
    }
}
