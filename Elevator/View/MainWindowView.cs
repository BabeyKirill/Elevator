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
        double time = 0;
        int NumberOfFloors;
        private readonly ApplicationContext _context;
        Button[] AddButtons;
        FlowLayoutPanel[] AwaitingPeopleContainer;
        FlowLayoutPanel[] ExitedPeopleContainer;
        CheckBox[] OuterActiveFloorButtons;
        CheckBox[] InnerActiveFloorButtons;

        public MainWindowView(ApplicationContext context)
        {
            _context = context;
            InitializeComponent();
        }

        public void SetNumberOfFloors(int n)
        {
            this.NumberOfFloors = n;
            AddButtons = new Button[n];
            AwaitingPeopleContainer = new FlowLayoutPanel[n];
            ExitedPeopleContainer = new FlowLayoutPanel[n];
            OuterActiveFloorButtons = new CheckBox[n];
            InnerActiveFloorButtons = new CheckBox[n];
            for (int i = 0; i < n; ++i)
            {
                ExitedPeopleContainer[i] = new FlowLayoutPanel();
                AwaitingPeopleContainer[i] = new FlowLayoutPanel();
                AddButtons[i] = new Button();
                OuterActiveFloorButtons[i] = new CheckBox();
                InnerActiveFloorButtons[i] = new CheckBox();

                ExitedPeopleContainer[i].BorderStyle = BorderStyle.FixedSingle;
                ExitedPeopleContainer[i].Size = new Size(70, 20);
                ExitedPeopleContainer[i].Location = new Point(10, 10 + 25 * (n - i - 1));
                ExitedPeopleContainer[i].BorderStyle = BorderStyle.FixedSingle;
                ExitedPeopleContainer[i].AutoSize = false;
                AwaitingPeopleContainer[i].Size = new Size(250, 20);
                AwaitingPeopleContainer[i].Location = new Point(204, 10 + 25 * (n - i - 1));
                AwaitingPeopleContainer[i].BorderStyle = BorderStyle.FixedSingle;
                AwaitingPeopleContainer[i].AutoSize = false;
                OuterActiveFloorButtons[i].Size = new Size(18, 17);
                OuterActiveFloorButtons[i].Location = new Point(187, 13 + 25 * (n - i - 1));
                OuterActiveFloorButtons[i].Enabled = false;
                InnerActiveFloorButtons[i].CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
                InnerActiveFloorButtons[i].Size = new Size(70, 17);
                InnerActiveFloorButtons[i].Location = new Point(80, 13 + 25 * (n - i - 1));
                InnerActiveFloorButtons[i].AutoSize = true;
                InnerActiveFloorButtons[i].Text = $"Floor №{i + 1}";
                InnerActiveFloorButtons[i].Enabled = false;
                AddButtons[i].BackColor = SystemColors.ButtonFace;
                AddButtons[i].BackgroundImage = Properties.Resources.Plus;
                AddButtons[i].BackgroundImageLayout = ImageLayout.Zoom;
                AddButtons[i].Location = new Point(455, 10 + 25 * (n - i - 1));
                AddButtons[i].Size = new Size(20, 20);
                AddButtons[i].Click += new System.EventHandler(this.AddButton_Click);
                AddButtons[i].Tag = i + 1;
                panel3.Location = new Point(155, 10 + 25 * (n - 1));
                this.panel2.Controls.Add(ExitedPeopleContainer[i]);
                this.panel2.Controls.Add(AwaitingPeopleContainer[i]);
                this.panel2.Controls.Add(OuterActiveFloorButtons[i]);
                this.panel2.Controls.Add(InnerActiveFloorButtons[i]);
                this.panel2.Controls.Add(AddButtons[i]);
            }
        }

        public event Action SetUp;
        public event Action<int> AddButtonClicked;
        public event Action StartSimulation;
        public event Action StopSimulation;

        public void MovePassengerInElevator(int NumberOfTheFloor)
        {
            if (AwaitingPeopleContainer[NumberOfTheFloor - 1].Controls.Count > 0)
            {
                AwaitingPeopleContainer[NumberOfTheFloor - 1].Controls.RemoveAt(AwaitingPeopleContainer[NumberOfTheFloor - 1].Controls.Count - 1);
                Console.WriteLine("Srabotalo");
            }
        }

        public void ActivateInnerActiveFloorCheckBox(int NumberOfTheFloor)
        {
            this.InnerActiveFloorButtons[NumberOfTheFloor - 1].Checked = true;
        }

        public void MoveElevator(int ElevatorNewFloor)
        {
            panel3.Location = new Point(155, 10 + 25 * (NumberOfFloors - ElevatorNewFloor));
            this.OuterActiveFloorButtons[ElevatorNewFloor - 1].Checked = false;
            this.InnerActiveFloorButtons[ElevatorNewFloor - 1].Checked = false;
        }

        public void ActivateOuterActiveFloorCheckBox(int NumberOfTheFloor)
        {
            this.OuterActiveFloorButtons[NumberOfTheFloor - 1].Checked = true;

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            int NumberOfTheFloor;
            Button btn = (Button)sender;
            NumberOfTheFloor = (int)btn.Tag;
            AddButtonClicked?.Invoke(NumberOfTheFloor);            
        }

        public void AddAPassenger(int NumberOfTheFloor)
        {
            PictureBox ManPicture = new PictureBox();
            AwaitingPeopleContainer[NumberOfTheFloor - 1].Controls.Add(ManPicture);
            ManPicture.BackgroundImage = global::Elevator.Properties.Resources.Man;
            ManPicture.BackgroundImageLayout = ImageLayout.Zoom;
            ManPicture.Size = new Size(16, 20);
            ManPicture.TabIndex = 1;
            ManPicture.TabStop = false;
        }

        public void SetActiveFloor(int floor)
        {
            panel3.Location = new Point(155, 10 + 25 * (NumberOfFloors - floor));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetUp?.Invoke();
        }
        
        //Start simulation button
        private void button2_Click(object sender, EventArgs e)
        {
            this.timer1.Start();           
            this.button3.Enabled = true;
            this.button2.Enabled = false;
            StartSimulation?.Invoke();
        }

        //Stop simulation button
        private void button3_Click(object sender, EventArgs e)
        {
            this.timer1.Stop();
            time = 0;
            this.toolStripStatusLabel1.Text = $"time: {time}";
            this.button2.Enabled = true;
            this.button3.Enabled = false;
            StopSimulation?.Invoke();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time++;
            this.toolStripStatusLabel1.Text = $"time: {time}";
        }

        public new void Show()
        {
            _context.MainForm = this;
            base.Show();
        }
    }
}
