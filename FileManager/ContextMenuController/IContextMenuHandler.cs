using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.ContextMenuController
{
    public interface IContextMenuHandler
    {
        void Create();
        void Delete();
        void Rename();
    }

}
