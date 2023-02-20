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

        public Form1()
        {
            _moveCursor = async () => {
                var mouseOps = new MouseOperations();
                Rectangle res = Screen.PrimaryScreen.Bounds;
                var pos = new Point((Size.Width) / 2, (Size.Height) / 2);
                var random = new Random();
                var last = DateTime.Now;
                var x = random.Next(0, 10) % 2 == 0 ? pos.X + random.Next(0, (Size.Width) / 3) : pos.X - random.Next(0, (Size.Width) / 3);
                var y = random.Next(0, 10) % 2 == 0 ? pos.Y + random.Next(0, (Size.Height) / 3) : pos.Y - random.Next(0, (Size.Height) / 3);
                Cursor.Position = new Point(x,y);
                last = DateTime.Now;
                await Task.Delay(500);

                //MouseEventArgs eventArgs = new MouseEventArgs(MouseButtons.Right, 1, x, y, 0);
                button2.PerformClick();
                mouseOps.DoMouseClick();
                
                if (!needEscape)
                {
                    _moveCursor();
                }
                else
                { 
                    isMoving= false;
                }
            };
            InitializeComponent();
            button1.Size = new Size(300, 50);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!isMoving)
            {
                needEscape = false;
                isMoving = true;
                _moveCursor();
                button1.Text = "durdurmak için fareyi clickle...";
            }
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            if (isMoving)
            {
                needEscape = MessageBox.Show("Durayım mı?", "Mola?",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question) == DialogResult.Yes;
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("form click");
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
    }
}