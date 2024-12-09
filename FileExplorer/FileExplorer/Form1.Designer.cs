namespace FileExplorer
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private Button button1;
        private Button button2;
        private Label label1;
        private TextBox textBox1;
        private ListView listView1;
        private TreeView treeView1;
        private ImageList imageList1; // ImageList для иконок

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.button1 = new Button();
            this.button2 = new Button();
            this.label1 = new Label();
            this.textBox1 = new TextBox();
            this.listView1 = new ListView();
            this.treeView1 = new TreeView();
            this.imageList1 = new ImageList(); // Инициализация ImageList
            this.SuspendLayout();

            // ImageList1
            this.imageList1.ColorDepth = ColorDepth.Depth32Bit; // Настроить глубину цвета
            this.imageList1.ImageSize = new Size(16, 16); // Размер иконок
            this.imageList1.TransparentColor = Color.Transparent; // Прозрачный фон для изображений

            // Добавляем стандартные иконки для файлов и папок
            this.imageList1.Images.Add("folder", Image.FromFile("C:\\Users\\altron41\\Pictures\\ava4.png")); // Иконка для папки
            this.imageList1.Images.Add("file", Image.FromFile("C:\\Users\\altron41\\Pictures\\ava3.png")); // Иконка для файла

            // Button1
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 29);
            this.button1.Text = "<<";
            this.button1.UseVisualStyleBackColor = true;

            // Button2
            this.button2.Location = new System.Drawing.Point(68, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(50, 29);
            this.button2.Text = ">>";
            this.button2.UseVisualStyleBackColor = true;

            // Label1
            this.label1.Location = new System.Drawing.Point(124, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 20);
            this.label1.Text = "Path:";

            // TextBox1
            this.textBox1.Location = new System.Drawing.Point(170, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(618, 27);

            // ListView1
            this.listView1.Location = new System.Drawing.Point(170, 47);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(618, 391);
            this.listView1.View = View.Details;
            this.listView1.Columns.Add("Name", 250);
            this.listView1.Columns.Add("Type", 100);
            this.listView1.Columns.Add("Size/Date", 150);
            this.listView1.SmallImageList = this.imageList1; // Присваиваем ImageList

            // TreeView1
            this.treeView1.Location = new System.Drawing.Point(12, 47);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(152, 391);
            this.treeView1.ImageList = this.imageList1; // Присваиваем ImageList

            // Form1
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "File Explorer";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}