using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace StRttmy.Common
{
    /// <summary>
    /// MultiSelectTreeView总结描述。
    ///System.Windows.Forms MultiSelectTreeView继承。树状视图,
    ///允许用户选择多个节点。
    ///底层comctl32 TreeView不支持多个选择。
    ///因此,MultiSelectTreeView BeforeSelect & & AfterSelect听
    ///事件动态更改个人treenodes的背景色
    ///表示选择。
    ///然后将TreeNode添加到当前的内部arraylist
    ///selectedNodes验证检查。
    ///
    ///MultiSelectTreeView支持
    ///1)选择+控制SelectedNodes的将当前节点添加到列表
    ///2)选择+ Shitft添加当前节点和节点之间的两个
    ///(如果开始节点和结束节点在同一级别)
    ///3)控制+当MultiSelectTreeView焦点将选择所有节点。
    ///	
    /// 
    /// </summary>
    public class MultiSelectTreeView : TreeView
    {
        public static bool IsUse = true;
        /// <summary>
        ///  treenode缓存用户点击
        /// </summary>
        private TreeNode lastNode;

        /// <summary>
        ///  SelectedNodes列表
        /// </summary>
        private	ArrayList selectedNodes;

        /// <summary>
        ///  缓存第一treenode用户点击
        /// </summary>
        private TreeNode firstNode;

        /// <summary>
        /// 构造函数初始化MultiSelectTreeView
        /// </summary>
        public MultiSelectTreeView()
        {
            selectedNodes = new ArrayList();
        }

        /// <summary>
        /// 构造函数初始化MultiSelectTreeView
        /// </summary>
        [Category("Selection"), Description("获取或设置选中节点的ArrayList")]
        public ArrayList SelectedNodes
        {
            get
            {
                return selectedNodes;
            }
            set
            {
                DeselectNodes();
                selectedNodes.Clear();
                selectedNodes = value;
                SelectNodes();
            }
        }

        #region overrides
        /// <summary>
        ///	用户按下“Ctrl+”键然后选择所有节点
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyDown (e);
            bool Pressed = (e.Control && ((e.KeyData & Keys.A) == Keys.A));
            if (Pressed)
            {
                SelectAllNodes(this.Nodes);
            }
        }
        /// <summary>
        ///	启动多个选择
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
        {
            base.OnBeforeSelect(e);

            //检查当前键按
            bool isControlPressed = (ModifierKeys == Keys.Control);
            bool isShiftPressed = (ModifierKeys == Keys.Shift);

            //如果按下“Ctrl”和selectedNodes包含当前节点
            //取消选择该节点…
            //删除从selectedNodes收集……
            if (isControlPressed && selectedNodes.Contains(e.Node))
            {
                DeselectNodes();
                selectedNodes.Remove(e.Node);
                SelectNodes();
                //MultiSelectTreeView处理这个事件....
                //Windows.Forms.TreeView应该支持这一事件
                e.Cancel = true;
                return;
            }

            //else (如果Shift键被按下)
            //启动多个选项…
            //因为转变按下我们将“选择”
            //所有节点从第一个节点到最后节点
            lastNode = e.Node;

            //如果转变不按下……
            //记住这个节点的迴旋节点。用户按下转向
            //选择多个节点。
            if (!isShiftPressed) firstNode = e.Node;
        }

        /// <summary>
        ///这个函数多选择结束。也添加和删除节点
        ///selectedNodes取决于按下的键
        /// </summary>
        /// <param name="e"></param>
        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            base.OnAfterSelect(e);

            //检查当前键按
            bool isControlPressed = (ModifierKeys == Keys.Control);
            bool isShiftPressed = (ModifierKeys == Keys.Shift);

            if (isControlPressed)
            {
                if (!selectedNodes.Contains(e.Node))
                {
                    //这是一个新节点,所以将其添加到列表中
                    selectedNodes.Add(e.Node);
                }
                else
                {
                    //如果按下“Ctrl”和selectedNodes包含当前节点
                    //取消选择该节点…
                    //删除从selectedNodes收集……
                    DeselectNodes();
                    selectedNodes.Remove(e.Node);
                }
                SelectNodes();
            }
            else
            {
                // Shift按下
                if (isShiftPressed)
                {
                    //开始寻找开始和结束节点选择的所有节点
                    TreeNode uppernode = firstNode;
                    TreeNode bottomnode = e.Node;
                    //检测父节点
                    bool bParent = CheckIfParent(uppernode, bottomnode);
                    if (!bParent)
                    {
                        //检测父节点
                        bParent = CheckIfParent(bottomnode, uppernode);
                        if (bParent)
                        {
                            TreeNode temp = uppernode;
                            uppernode = bottomnode;
                            bottomnode = temp;
                        }
                    }
                    if (bParent)
                    {
                        TreeNode n = bottomnode;
                        while (n != uppernode.Parent)
                        {
                            if (!selectedNodes.Contains(n))
                                selectedNodes.Add(n);
                            n = n.Parent;
                        }
                    }
                    // 未获取到父节点,检查节点是否在同一水平
                    else
                    {
                        if ((uppernode.Parent == null && bottomnode.Parent == null) || (uppernode.Parent != null && uppernode.Parent.Nodes.Contains(bottomnode))) //他们是兄弟吗?
                        {
                            int nIndexUpper = uppernode.Index;
                            int nIndexBottom = bottomnode.Index;
                            //需要交换如果订单是逆转…
                            if (nIndexBottom < nIndexUpper)
                            {
                                TreeNode temp = uppernode;
                                uppernode = bottomnode;
                                bottomnode = temp;
                                nIndexUpper = uppernode.Index;
                                nIndexBottom = bottomnode.Index;
                            }

                            TreeNode n = uppernode;
                            selectedNodes.Clear();
                            while (nIndexUpper < nIndexBottom)
                            {
                                //添加的所有节点,如果节点不存在于当前
                                //SelectedNodes列表……

                                if (!selectedNodes.Contains(n))
                                {
                                    selectedNodes.Add(n);
                                    SelectAllNodesInNode(n.Nodes, n);
                                }
                                n = n.NextNode;
                                nIndexUpper++;
                            }
                            //添加最后一个节点
                            selectedNodes.Add(n);
                        }
                        else
                        {
                            if (!selectedNodes.Contains(uppernode)) selectedNodes.Add(uppernode);
                            if (!selectedNodes.Contains(bottomnode)) selectedNodes.Add(bottomnode);
                        }
                    }
                    SelectNodes();
                    //irstNode计数器重置为后续“SHIFT”键。
                    firstNode = e.Node;
                }
                else
                {
                    //如果正常选择然后添加SelectedNodes集合
                    if (selectedNodes != null && selectedNodes.Count > 0)
                    {
                        if (IsUse)//删除多选节点时调用DeselectNodes()时TreeView获取不到报错,false不再调用DeselectNodes()方法,true恢复调用
                            DeselectNodes();
                        selectedNodes.Clear();
                    }
                    selectedNodes.Add(e.Node);
                }
            }
        }

        /// <summary>
        ///	重载OnLostFocus模仿TreeView取消节点的行为
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            DeselectNodes();
        }

        /// <summary>
        ///	重载OnGotFocus模仿TreeView的行为选择节点
        /// </summary>
        /// <param name="e"></param>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            SelectNodes();
        }

        #endregion overrides

        /// <summary>
        ///	检查节点的父节点
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="childNode"></param>
        /// <returns></returns>
        private bool CheckIfParent(TreeNode parentNode, TreeNode childNode)
        {
            if (parentNode == childNode)
                return true;
            TreeNode node = childNode;
            bool parentFound = false;
            while (!parentFound && node != null)
            {
                node = node.Parent;
                parentFound = (node == parentNode);
            }
            return parentFound;
        }

        /// <summary>
        ///这个函数提供了用户的反馈,该节点被选中
        ///基本背景色和字体颜色改变
        ///selectedNodes集合中的节点。
        /// </summary>
        private void SelectNodes()
        {
            foreach (TreeNode n in selectedNodes)
            {
                n.BackColor = SystemColors.Highlight;
                n.ForeColor = SystemColors.HighlightText;
            }
        }

        /// <summary>
        ///这个函数提供了用户反馈,取消选择的节点
        ///基本背景色和字体颜色改变
        ///selectedNodes集合中的节点。
        /// </summary>
        private void DeselectNodes()
        {
            if (selectedNodes.Count == 0) return;
            TreeNode node = (TreeNode)selectedNodes[0];
            if (node.TreeView != null) 
            {
                Color backColor = node.TreeView.BackColor;
                Color foreColor = node.TreeView.ForeColor;
                foreach (TreeNode n in selectedNodes)
                {
                    n.BackColor = backColor;
                    n.ForeColor = foreColor;
                }
            }
        }

        /// <summary>
        ///	选择中的所有节点MultiSelectTreeView
        /// </summary>
        private void SelectAllNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode n in this.Nodes)
            {
                selectedNodes.Add(n);
                if (n.Nodes.Count > 1)
                {
                    SelectAllNodesInNode(n.Nodes, n);
                }
            }
            SelectNodes();
        }

        /// <summary>
        ///	递归函数选择MultiSelectTreeView所有节点的节点
        /// </summary>
        private void SelectAllNodesInNode(TreeNodeCollection nodes, TreeNode node)
        {
            foreach (TreeNode n in node.Nodes)
            {
                selectedNodes.Add(n);
                if (n.Nodes.Count > 1)
                {
                    SelectAllNodesInNode(n.Nodes, n);
                }
            }
            SelectNodes();
        }
    }
}