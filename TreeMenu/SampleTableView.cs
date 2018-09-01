using System;
using UIKit;
using Xamarin.SideMenu;
using Foundation;
using System.Linq;
using System.Collections.Generic;
using CoreGraphics;

namespace TreeMenu
{
    public class SampleTableView : UITableViewController
    {
        public SampleTableView()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //TreeViewの設定
            this.TableView.RegisterClassForCellReuse(typeof(FolderCell), "FolderCell");
            TableView.RowHeight = FolderCell.Height;
            TableView.SeparatorColor = UIColor.Clear;
            TableView.Source = new TreeViewSource(CreateNodes(), this);

        }

        private TreeNode CreateNodes()
        {
            var tokyo = new TreeNode { Level = 0, Name = "東京都" };

            var sub1 = new TreeNode { Level = 1, Name = "中央区" };
            var sub2 = new TreeNode { Level = 1, Name = "渋谷区" };
            var sub3 = new TreeNode { Level = 1, Name = "港区" };

            tokyo.Children.Add(sub1);
            tokyo.Children.Add(sub2);
            tokyo.Children.Add(sub3);

            var sub4 = new TreeNode { Level = 2, Name = "築地" };
            sub1.Children.Add(sub4);

            return tokyo;
        }

        private class TreeViewSource : UITableViewSource
        {
            private List<TreeNode> Nodes = new List<TreeNode>();
            private SampleTableView view;

            public TreeViewSource(TreeNode root, SampleTableView view)
            {
                Nodes.Add(root);
                this.view = view;
            }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var cell = tableView.DequeueReusableCell("FolderCell") as FolderCell;
                cell.SetCellContents(Nodes[indexPath.Row]);
                return cell;
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return Nodes.Count;
            }

            public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            {
                var selectedNode = Nodes[indexPath.Row];
                var selectedIndex = indexPath.Row;


                if (!selectedNode.IsExpanded)
                {
                    var children = selectedNode.Children;

                    // 開く
                    if (!children.Any())
                    {
                        //最終子要素の場合は画面遷移する
                        tableView.DeselectRow(indexPath, true);

                        var rnd = new Random(Guid.NewGuid().GetHashCode());

                        var vc = new UIViewController() { };
                        vc.View.BackgroundColor = UIColor.FromRGB(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));

                        this.view.ShowViewController(vc, this);

                        return;
                    }

                    selectedNode.IsExpanded = true;

                    var indexPaths = new List<NSIndexPath>();
                    foreach (var node in children.Select((Value, Index) => new { Value, Index }))
                    {
                        node.Value.IsExpanded = false;
                        indexPaths.Add(NSIndexPath.FromRowSection(selectedIndex + node.Index + 1, 0));
                    }
                    Nodes.InsertRange(selectedIndex + 1, children);
                    tableView.InsertRows(indexPaths.ToArray(), UITableViewRowAnimation.Automatic);
                }
                else
                {
                    // 閉じる
                    selectedNode.IsExpanded = false;

                    var node = Nodes.Skip(selectedIndex + 1).FirstOrDefault(i => i.Level <= selectedNode.Level);
                    var deleteCount = (node != null) ?
                        Nodes.IndexOf(node) - selectedIndex - 1 :
                        Nodes.Count - selectedIndex - 1;

                    var indexPaths = new List<NSIndexPath>();
                    for (int i = 0; i < deleteCount; i++)
                    {
                        Nodes.RemoveAt(selectedIndex + 1);
                        tableView.DeleteRows(new NSIndexPath[] { NSIndexPath.FromRowSection(selectedIndex + 1, 0) }, UITableViewRowAnimation.Top);
                    }
                }
            }
        }
    }


}
