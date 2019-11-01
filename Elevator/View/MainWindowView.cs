using Elevator.Presenter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Elevator.View
{
    public partial class MainWindowView : Form, IMainWindowView
    {
        int time = 0;
        int NumberOfFloors;
        private readonly ApplicationContext _context;

        public MainWindowView(ApplicationContext context)
        {
            _context = context;
            InitializeComponent();
        }

        public void SetNumberOfFloors(decimal n)
        {
            NumberOfFloors = (int)n;
            for (int i = 0; i < n; ++i)
            {
                FlowLayoutPanel flowLayPnl1 = new FlowLayoutPanel();
                FlowLayoutPanel flowLayPnl2 = new FlowLayoutPanel();
                Button btn = new Button();
                CheckBox chBox1 = new CheckBox();
                CheckBox chBox2 = new CheckBox();

                flowLayPnl1.BorderStyle = BorderStyle.FixedSingle;
                flowLayPnl1.Size = new Size(70, 20);
                flowLayPnl1.Location = new Point(10, 10 + 25 * i);
                flowLayPnl1.BorderStyle = BorderStyle.FixedSingle;
                flowLayPnl1.AutoSize = false;
                flowLayPnl2.Size = new Size(250, 20);
                flowLayPnl2.Location = new Point(204, 10 + 25 * i);
                flowLayPnl2.BorderStyle = BorderStyle.FixedSingle;
                flowLayPnl2.AutoSize = false;
                chBox1.Size = new Size(18, 17);
                chBox1.Location = new Point(187, 13 + 25 * i);
                chBox2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
                chBox2.Size = new Size(70, 17);
                chBox2.Location = new Point(80, 13 + 25 * i);
                chBox2.AutoSize = true;
                chBox2.Text = $"Floor №{n - i}";                
                btn.BackColor = SystemColors.ButtonFace;
                btn.BackgroundImage = Properties.Resources.Plus;
                btn.BackgroundImageLayout = ImageLayout.Zoom;
                btn.Location = new Point(455, 10 + 25 * i);
                btn.Size = new Size(20, 20);
                panel3.Location = new Point(155, 10 + 25 * ((int)n - 1));

                this.panel2.Controls.Add(flowLayPnl1);
                this.panel2.Controls.Add(flowLayPnl2);
                this.panel2.Controls.Add(chBox1);
                this.panel2.Controls.Add(chBox2);
                this.panel2.Controls.Add(btn);
            }
        }

        public void SetActiveFloor(int floor)
        {
            panel3.Location = new Point(155, 10 + 25 * (NumberOfFloors - floor));
        }

        public event Action SetUp;

        private void button1_Click(object sender, EventArgs e)
        {
            SetUp?.Invoke();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.timer1.Start();
            panel3.Location = new Point(155, 10);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.timer1.Stop();
            time = 0;
            this.toolStripStatusLabel1.Text = $"time: {time}";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time++;
            this.toolStripStatusLabel1.Text = $"time: {time}";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        public new void Show()
        {
            _context.MainForm = this;
            base.Show();
        }
    }
}
