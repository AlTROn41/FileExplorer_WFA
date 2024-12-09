using System;
using System.IO;
using System.Windows.Forms;

namespace FileExplorer
{
    public partial class Form1 : Form
    {
        private DirectoryInfo currentDirectory;
        private DirectoryInfo previousDirectory;

        public Form1()
        {
            InitializeComponent();
            LoadDrives();
            treeView1.BeforeExpand += TreeView1_BeforeExpand;
            treeView1.AfterSelect += TreeView1_AfterSelect;
            button1.Click += Button1_Click; // ���������� ��� ������ "�����"
            button2.Click += Button2_Click; // ���������� ��� ������ "�����"

            // ��������� ���������� ������� �������� ����� �� �������� ListView
            listView1.ItemActivate += ListView1_ItemActivate;
        }

        private void LoadDrives()
        {
            treeView1.Nodes.Clear();
            foreach (var drive in DriveInfo.GetDrives())
            {
                TreeNode node = new TreeNode(drive.Name) { Tag = drive.RootDirectory, ImageKey = "folder" };
                node.Nodes.Add(""); // �������� ��� ������ "+"
                treeView1.Nodes.Add(node);
            }
        }

        private void TreeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode node = e.Node;

            // ���� ���� ��� �� ��������
            if (node.Nodes.Count == 1 && node.Nodes[0].Text == "")
            {
                node.Nodes.Clear(); // ������� ��������

                DirectoryInfo dir = node.Tag as DirectoryInfo;
                if (dir != null)
                {
                    try
                    {
                        foreach (var subDir in dir.GetDirectories())
                        {
                            // ��������� ����������� �����
                            if ((subDir.Attributes & FileAttributes.Hidden) == 0 &&
                                (subDir.Attributes & FileAttributes.System) == 0)
                            {
                                TreeNode subNode = new TreeNode(subDir.Name) { Tag = subDir, ImageKey = "folder" };
                                subNode.Nodes.Add(""); // ��������� �������� ��� "+"
                                node.Nodes.Add(subNode);
                            }
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {
                        MessageBox.Show("Access denied to: " + dir.FullName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selectedNode = e.Node;
            DirectoryInfo dirInfo = selectedNode.Tag as DirectoryInfo;

            if (dirInfo != null)
            {
                currentDirectory = dirInfo;
                PopulateListView(currentDirectory);
                textBox1.Text = currentDirectory.FullName;

                // ������� "�����" (������ 2) ��� �������� � ����� �����
                previousDirectory = null;
            }
        }

        private void PopulateListView(DirectoryInfo directory)
        {
            listView1.Items.Clear();

            try
            {
                // ��������� �����
                foreach (var dir in directory.GetDirectories())
                {
                    ListViewItem item = new ListViewItem(dir.Name, 0); // ���������� ������ 0 ��� �����
                    item.SubItems.Add("Folder");
                    item.SubItems.Add(dir.LastWriteTime.ToString());
                    item.Tag = dir; // ��������� ������ ����� � �������� ����
                    listView1.Items.Add(item);
                }

                // ��������� �����
                foreach (var file in directory.GetFiles())
                {
                    ListViewItem item = new ListViewItem(file.Name, 1); // ���������� ������ 1 ��� ������
                    item.SubItems.Add(file.Extension);
                    item.SubItems.Add(file.Length.ToString());
                    item.Tag = file; // ��������� ������ ����� � �������� ����
                    listView1.Items.Add(item);
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Access denied to: " + directory.FullName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (currentDirectory?.Parent != null)
            {
                previousDirectory = currentDirectory; // ��������� ������� ����� ��� ����������
                currentDirectory = currentDirectory.Parent; // ��������� �� ������� ����
                PopulateListView(currentDirectory);
                textBox1.Text = currentDirectory.FullName;
            }
            else
            {
                MessageBox.Show("No parent directory available.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (previousDirectory != null)
            {
                currentDirectory = previousDirectory; // ��������� �����
                PopulateListView(currentDirectory);
                textBox1.Text = currentDirectory.FullName;
            }
            else
            {
                MessageBox.Show("No previous directory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ���������� ������� �������� ����� �� �������� � ListView
        private void ListView1_ItemActivate(object sender, EventArgs e)
        {
            // �������� ��������� �������
            ListViewItem selectedItem = listView1.SelectedItems[0];

            // ���� ��� �����, ��������� � ����������
            if (selectedItem.Tag is DirectoryInfo dir)
            {
                previousDirectory = currentDirectory; // ��������� ������� ����� ��� ����������
                currentDirectory = dir; // ��������� � ��������� �����
                PopulateListView(currentDirectory); // ��������� ���������� �����
                textBox1.Text = currentDirectory.FullName; // ��������� ��������� ���� � �����
            }
        }
    }
}
