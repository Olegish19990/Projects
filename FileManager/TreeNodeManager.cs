using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    public class TreeNodeManager
    {
        public TreeNodeManager(TreeView treeView)
        {
            TreeNode rootNode = new TreeNode("Root");
            
            
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (var drive in allDrives)
            {
                DirectoryInfo dir = new DirectoryInfo(drive.Name);
                rootNode.Nodes.Add(CreateNode(dir));
            }
            treeView.Nodes.Add(rootNode);
        }

        private TreeNode CreateNode(DirectoryInfo dir)
        {
            TreeNode newNode = new TreeNode(dir.Name);
            newNode.Tag = dir;
            AddFakeNode(newNode);
            return newNode;
        }


        public void NodeOpen(TreeNode selectedNode)
        {
            TreeNode currentNode = selectedNode;

            if (currentNode.Nodes[0].Text == "FakeNode")
            {
                currentNode.Nodes.Clear();
                DirectoryInfo dirInfo = (DirectoryInfo)currentNode.Tag;
                ExpandDirectoryNodes(dirInfo, currentNode);
            }

        }

        private void AddFakeNode(TreeNode node)
        {
            node.Nodes.Add(new TreeNode("FakeNode"));
        }

        private void ExpandDirectoryNodes(DirectoryInfo dir, TreeNode node)
        {   
           
            DirectoryInfo[] dirs = dir.GetDirectories();
            foreach (var d in dirs)
            {
                node.Nodes.Add(CreateNode(d));
            }
        }

       
       


      
    }
}
