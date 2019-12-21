using Elevator.Presenter;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Elevator.View
{
    public partial class MainWindowView : Form, IMainWindowView
    {
        private int NumberOfFloors;
        private int NumberOfCreatedPeople;
        private int MovedMass;
        private int Rides;
        private int IddleRides;
        private int NumberOfPassengersInElevator;
        private readonly ApplicationContext _context;
        private Button[] AddButtons;
        private FlowLayoutPanel[] AwaitingPeopleContainer;
        private FlowLayoutPanel[] ExitedPeopleContainer;
        private CheckBox[] OuterActiveFloorButtons;
        private CheckBox[] InnerActiveFloorButtons;

        public event Action SetUp;
        public event Action<int> AddButtonClicked;
        public event Action StartSimulation;
        public event Action StopSimulation;
        public event Action PassengersInfoShown;

        public MainWindowView(ApplicationContext context)
        {
            _context = context;
            InitializeComponent();
            NumberOfPassengersInElevator = 0;
            NumberOfCreatedPeople = 0;
            MovedMass = 0;
            Rides = 0;
            IddleRides = 0;
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

        public void DeletePassenger(int NumberOfTheFloor)
        {
            if (ExitedPeopleContainer[NumberOfTheFloor - 1].Controls.Count > 0)
            {
                ExitedPeopleContainer[NumberOfTheFloor - 1].Controls.RemoveAt(ExitedPeopleContainer[NumberOfTheFloor - 1].Controls.Count - 1);
            }
        }

        public void MovePassengerFromElevator(int NumberOfTheFloor)
        {
            PictureBox ManPicture = new PictureBox();
            ExitedPeopleContainer[NumberOfTheFloor - 1].Controls.Add(ManPicture);
            ManPicture.BackgroundImage = global::Elevator.Properties.Resources.Man;
            ManPicture.BackgroundImageLayout = ImageLayout.Zoom;
            ManPicture.Size = new Size(16, 20);
            ManPicture.TabIndex = 1;
            ManPicture.TabStop = false;
            
            NumberOfPassengersInElevator--;
            if (NumberOfPassengersInElevator == 0)
            {
                pictureBox1.Visible = false;
            }
        }

        public void MovePassengerInElevator(int NumberOfTheFloor)
        {
            if (AwaitingPeopleContainer[NumberOfTheFloor - 1].Controls.Count > 0)
            {
                AwaitingPeopleContainer[NumberOfTheFloor - 1].Controls.RemoveAt(AwaitingPeopleContainer[NumberOfTheFloor - 1].Controls.Count - 1);
                NumberOfPassengersInElevator++;
                pictureBox1.Visible = true;
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
            NumberOfCreatedPeople++;
            label4.Text = $"Passengers count = {NumberOfCreatedPeople}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetUp?.Invoke();
        }
        
        //Start simulation button
        private void button2_Click(object sender, EventArgs e)
        {          
            this.button3.Enabled = true;
            this.button2.Enabled = false;
            StartSimulation?.Invoke();
        }

        //Stop simulation button
        private void button3_Click(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = $"time: {0}";
            foreach (FlowLayoutPanel pan in AwaitingPeopleContainer)
            {
                pan.Controls.Clear();
            }
            foreach (FlowLayoutPanel pan in ExitedPeopleContainer)
            {
                pan.Controls.Clear();
            }
            foreach (CheckBox chb in OuterActiveFloorButtons)
            {
                chb.Checked = false;
            }
            foreach (CheckBox chb in InnerActiveFloorButtons)
            {
                chb.Checked = false;
            }
            panel3.Location = new Point(155, 10 + 25 * (NumberOfFloors - 1));
            this.button2.Enabled = true;
            this.button3.Enabled = false;
            pictureBox1.Visible = false;
            radioButton1.Checked = false;
            NumberOfPassengersInElevator = 0;
            Rides = 0;
            IddleRides = 0;
            MovedMass = 0;
            NumberOfCreatedPeople = 0;
            label1.Text = "Rides = 0";
            label2.Text = "Iddle rides = 0";
            label3.Text = "Transported mass = 0";
            label4.Text = "Passengers count = 0";
            StopSimulation?.Invoke();
        }

        public void UpdateMovedMass(int MassIncrease)
        {
            MovedMass = MovedMass + MassIncrease;
            this.label3.Text = $"Transported mass = {MovedMass}";
        }

        public void IncreaseRides()
        {
            this.Rides++;
            this.label1.Text = $"Rides = {Rides}";
        }

        public void IncreaseIddleRides()
        {
            this.IddleRides++;
            this.label2.Text = $"Iddle rides = {IddleRides}";
        }

        public void ActivateOverWeight()
        {
            this.radioButton1.Checked = true;
        }

        public void DeactivateOverWeight()
        {
            this.radioButton1.Checked = false;
        }

        public void UpdateTime(double NewTime)
        {
            this.toolStripStatusLabel1.Text = $"time: {NewTime}";
        }

        public new void Show()
        {
            _context.MainForm = this;
            base.Show();
        }

        //View passengers info
        private void button4_Click(object sender, EventArgs e)
        {
            PassengersInfoShown?.Invoke();
        }
    }
}
