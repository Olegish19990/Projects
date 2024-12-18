using FileManager.FileOpen.FileTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.nodeController
{
    public class NodeController
    {

        public NodeController(TreeView treeView)
        {

            ImageList imageList = new ImageList();
            imageList.Images.Add("folder", Image.FromFile("folder.png"));
          
            treeView.ImageList = imageList;


            TreeNode rootNode = new TreeNode("Root",0,0);
            
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (var drive in allDrives)
            {
                
                DirectoryInfo dir = new DirectoryInfo(drive.Name);
                TreeNode node = new TreeNode();
               
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
