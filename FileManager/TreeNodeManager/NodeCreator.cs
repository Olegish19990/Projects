using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.nodeController
{
    public class NodeCreator
    {
        public TreeNode CreateNode(DirectoryInfo dir)
        {
            TreeNode newNode = new TreeNode(dir.Name);
            newNode.Tag = dir;
            AddFakeNode(newNode);
            return newNode;
        }

        public void AddFakeNode(TreeNode node)
        {
            node.Nodes.Add(new TreeNode("FakeNode"));
        }

    }
}
