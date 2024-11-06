using FileProcessor.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileProcessor.Renaming.Generation;

internal class StringGenerator
{

    private string replacePattern = string.Empty;
    private RulesExtractor extractor;
    private List<RuleItem> rules = null!;

    private Dictionary<string, IRuleHandler> handlers = new Dictionary<string, IRuleHandler>()
    {
        { "uuid", new UuidRuleHandler() },
        { "increment", new IncrementRuleHandler() },
        { "old_name", new OldNameRuleHandler() },
    };

    public StringGenerator()
    {
        extractor = new RulesExtractor();
    }

    public void AddRuleHandler(string shortcut, IRuleHandler handler)
    {
        if (handlers.Keys.Contains(shortcut))
            throw new RenamerException(RenamerException.DUPLICATE_SHORTCUT);

        handlers.Add(shortcut, handler);
    }

    public void SetReplacePattern(string replacePattern)
    {
        this.replacePattern = replacePattern;

        rules = extractor.Extract(replacePattern);

        ConfigureHandlers();
    }

    private void ConfigureHandlers()
    {
        rules.ForEach(rule =>
        {
            IRuleHandler handler = handlers[rule.Shortcut];

            if (!string.IsNullOrEmpty(rule.Options))
                if (handler is IWithOptions h)
                    h.OptionsString = rule.Options;

        });
    }

    public string GetNext(FileInfo file)
    {
        string result = replacePattern;

        rules.ForEach(rule =>
        {
            IRuleHandler handler = handlers[rule.Shortcut];

            string value = handler.GetValue(file);
            string pattern = RegexPatternPreparer.PreparePattern(rule.Signature);

            Regex replaceRegex = new Regex($@"\<{pattern}\>");

            result = replaceRegex.Replace(result, value);

        });

        return result;
    }

}
