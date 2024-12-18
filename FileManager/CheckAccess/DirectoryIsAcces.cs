
using FileManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    public static class DirectoryIsAcces
    {
        public static bool CheckAccess(DirectoryInfo dir)
        {
            bool result = true;
            try
            {
                dir.GetDirectories();
                var information = dir.GetAccessControl();
                Console.WriteLine("Get information");
            }
            catch
            {
                result = false;

            }
            return result;
        }
   
    }
}



