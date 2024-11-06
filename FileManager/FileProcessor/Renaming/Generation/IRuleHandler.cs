using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcessor.Renaming.Generation;

public interface IRuleHandler
{
    public string GetValue(FileInfo? item = null);

    // TODO: maybe???
    //public string GetValue<T>(T? item = null) where T : class;
}
