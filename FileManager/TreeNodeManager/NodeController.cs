using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.nodeController
{
    public class NodeController
    {
        //private NodeCreator nodeCreator { get; set; }

        public NodeController(TreeView treeView)
        {
            TreeNode rootNode = new TreeNode("Root");
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (var drive in allDrives)
            {
                DirectoryInfo dir = new DirectoryInfo(drive.Name);
                rootNode.Nodes.Add(NodeCreator.CreateNode(dir));
            }
            treeView.Nodes.Add(rootNode);
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

        private void ExpandDirectoryNodes(DirectoryInfo dir, TreeNode node)
        {

            DirectoryInfo[] dirs = dir.GetDirectories();
            foreach (var d in dirs)
            {
                node.Nodes.Add(NodeCreator.CreateNode(d));
            }
        }








    }
}
