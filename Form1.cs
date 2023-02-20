using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.Devices;
using System.Diagnostics.Metrics;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace WinFormsCursor
{
    public partial class Form1 : Form
    {
        private readonly Action _moveCursor;
        private bool isMoving = false;
        private bool needEscape = false;
        private bool hesitating = false;
        private MouseOperations mouseOps = new MouseOperations();

        public Form1()
        {
            _moveCursor = async () => {
                if (!hesitating)
                {
                    Rectangle res = Screen.PrimaryScreen.Bounds;
                    var pos = new Point((Size.Width) / 2, (Size.Height) / 2);
                    var random = new Random();
                    var last = DateTime.Now;
                    var x = random.Next(0, 10) % 2 == 0 ? pos.X + random.Next(0, (Size.Width) / 3) : pos.X - random.Next(0, (Size.Width) / 3);
                    var y = random.Next(0, 10) % 2 == 0 ? pos.Y + random.Next(0, (Size.Height) / 3) : pos.Y - random.Next(0, (Size.Height) / 3);
                    Cursor.Position = new Point(x,y);
                    last = DateTime.Now;
                    button2.PerformClick();
                    mouseOps.DoMouseClick();
                }

                if (!needEscape)
                {
                    await Task.Delay(500);
                    _moveCursor();
                }
                else
                { 
                    isMoving= false;
                }
            };
            InitializeComponent();
            button2.BackColor = Color.Transparent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!isMoving)
            {
                needEscape = false;
                isMoving = true;
                _moveCursor();
                button1.Text = "durdurmak için fareyi double clickle...";
            }
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            if (isMoving)
            {
                hesitating = true;
                needEscape = MessageBox.Show("Durayım mı?", "Sigara içildi mi?",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question) == DialogResult.Yes;
                if (needEscape)
                {
                    button1.Text = "Tekrar başla :)";
                }
                hesitating = false;
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = $"click time => {DateTime.Now.ToString("HH:mm:ss")}";
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right) { MessageBox.Show("left click"); }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void Form1_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("click");
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}