using FileManager.FileHandling;
using FileManager.ItemCreate;
using FileManager.ItemRenamer;
using FileProcessor.Removing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.ContextMenuController
{
    public class FileContextMenuHandler : IContextMenuHandler
    {
        private FileDisplayer fileDisplayer;

        public FileContextMenuHandler(FileDisplayer fileDisplayer)
        {
            this.fileDisplayer = fileDisplayer;
        }

        public void Create()
        {
            FileCreate fileCreate = new FileCreate();
            fileCreate.ShowDialog();
            fileDisplayer.DisplayFiles(CurrentDirectory.CurrentDir.GetFiles());


        }

        public void Delete()
        {

            DeleteItem deleteItem = new DeleteItem();
            var files = fileDisplayer.GetSelectedItems();

            if (deleteItem.ConfirmDelete())
            {

                deleteItem.Delete(RemoverMode.Files, files);
                fileDisplayer.DisplayFiles(CurrentDirectory.CurrentDir.GetFiles());


            }
        }

        public void Rename()
        {

            FileRenamer fileRenamer = new FileRenamer(fileDisplayer.GetSelectedItems());
            fileRenamer.ShowDialog();
            fileDisplayer.DisplayFiles(CurrentDirectory.CurrentDir.GetFiles());

        }
    }
}
