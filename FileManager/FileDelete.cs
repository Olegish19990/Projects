using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    static class FileDelete
    {
        public static void Delete(List<FileInfo> files)
        {
           
            foreach(var item in files) 
            {
                try
                {
                    item.Delete();
                }
                catch
                {
                    continue;
                }
            }
        }
    }
}
