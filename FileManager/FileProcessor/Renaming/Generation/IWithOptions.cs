using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcessor.Renaming.Generation;

public interface IWithOptions
{
    public string OptionsString { get; set; }
}
