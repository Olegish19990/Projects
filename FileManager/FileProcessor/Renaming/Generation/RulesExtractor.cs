using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileProcessor.Renaming.Generation;

internal class RulesExtractor
{
    private const string RULE_PATTERN = @"\<([^>]*)\>";
    private const string SHORTCUT_PATTERN = @"^[a-z_]*";
    private const string OPTIONS_PATTERN = @"\((.+)\)";

    private Regex ruleRegex;
    private Regex shortcutRegex;
    private Regex optionsRegex;

    public RulesExtractor()
    {
        ruleRegex = new Regex(RULE_PATTERN);
        shortcutRegex = new Regex(SHORTCUT_PATTERN);
        optionsRegex = new Regex(OPTIONS_PATTERN);
    }

    public List<RuleItem> Extract(string replacePattern)
    {
        List<RuleItem> ruleItems = new List<RuleItem>();

        foreach(Match match in ruleRegex.Matches(replacePattern))
        {
            string signature = match.Groups[1].Value;

            ruleItems.Add(new RuleItem()
            {
                Signature = signature,
                Shortcut = shortcutRegex.Match(signature).Value,
                Options = optionsRegex.Match(signature).Groups[1].Value,
            });
        }

        return ruleItems;
    }
}
