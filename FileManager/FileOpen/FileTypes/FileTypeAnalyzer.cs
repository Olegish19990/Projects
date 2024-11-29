using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileManager.FileOpen.FileTypes
{
    public class FileTypeAnalyzer
    {
        Dictionary<string, IFileViewer> ValidTypes = new Dictionary<string, IFileViewer>()
        {
            {".txt", new TextFile()},
            {".png", new PictureFile()},
            {".jpg", new PictureFile()},
           

        };
        public IFileViewer Analyze(FileInfo file)
        {

            IFileViewer type;
            if(ValidTypes.TryGetValue(file.Extension, out type))
            {
                return type;
            }
            return new UnknowableFileType();
        }
    }
}
