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
            button1.Click += Button1_Click; // Обработчик для кнопки "Назад"
            button2.Click += Button2_Click; // Обработчик для кнопки "Вперёд"

            // Добавляем обработчик события двойного клика по элементу ListView
            listView1.ItemActivate += ListView1_ItemActivate;
        }

        private void LoadDrives()
        {
            treeView1.Nodes.Clear();
            foreach (var drive in DriveInfo.GetDrives())
            {
                TreeNode node = new TreeNode(drive.Name) { Tag = drive.RootDirectory, ImageKey = "folder" };
                node.Nodes.Add(""); // Заглушка для кнопки "+"
                treeView1.Nodes.Add(node);
            }
        }

        private void TreeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode node = e.Node;

            // Если узел ещё не загружен
            if (node.Nodes.Count == 1 && node.Nodes[0].Text == "")
            {
                node.Nodes.Clear(); // Убираем заглушку

                DirectoryInfo dir = node.Tag as DirectoryInfo;
                if (dir != null)
                {
                    try
                    {
                        foreach (var subDir in dir.GetDirectories())
                        {
                            // Проверяем доступность папки
                            if ((subDir.Attributes & FileAttributes.Hidden) == 0 &&
                                (subDir.Attributes & FileAttributes.System) == 0)
                            {
                                TreeNode subNode = new TreeNode(subDir.Name) { Tag = subDir, ImageKey = "folder" };
                                subNode.Nodes.Add(""); // Добавляем заглушку для "+"
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

                // Очистка "Вперёд" (кнопка 2) при переходе в новую папку
                previousDirectory = null;
            }
        }

        private void PopulateListView(DirectoryInfo directory)
        {
            listView1.Items.Clear();

            try
            {
                // Добавляем папки
                foreach (var dir in directory.GetDirectories())
                {
                    ListViewItem item = new ListViewItem(dir.Name, 0); // Используем индекс 0 для папки
                    item.SubItems.Add("Folder");
                    item.SubItems.Add(dir.LastWriteTime.ToString());
                    item.Tag = dir; // Сохраняем объект папки в качестве тега
                    listView1.Items.Add(item);
                }

                // Добавляем файлы
                foreach (var file in directory.GetFiles())
                {
                    ListViewItem item = new ListViewItem(file.Name, 1); // Используем индекс 1 для файлов
                    item.SubItems.Add(file.Extension);
                    item.SubItems.Add(file.Length.ToString());
                    item.Tag = file; // Сохраняем объект файла в качестве тега
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
                previousDirectory = currentDirectory; // Сохраняем текущую папку как предыдущую
                currentDirectory = currentDirectory.Parent; // Переходим на уровень выше
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
                currentDirectory = previousDirectory; // Переходим назад
                PopulateListView(currentDirectory);
                textBox1.Text = currentDirectory.FullName;
            }
            else
            {
                MessageBox.Show("No previous directory.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Обработчик события двойного клика по элементу в ListView
        private void ListView1_ItemActivate(object sender, EventArgs e)
        {
            // Получаем выбранный элемент
            ListViewItem selectedItem = listView1.SelectedItems[0];

            // Если это папка, открываем её содержимое
            if (selectedItem.Tag is DirectoryInfo dir)
            {
                previousDirectory = currentDirectory; // Сохраняем текущую папку как предыдущую
                currentDirectory = dir; // Переходим в выбранную папку
                PopulateListView(currentDirectory); // Загружаем содержимое папки
                textBox1.Text = currentDirectory.FullName; // Обновляем текстовое поле с путем
            }
        }
    }
}
