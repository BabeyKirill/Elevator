using Elevator.Presenter;
using System;
using System.Windows.Forms;

namespace Elevator.View
{
    public partial class SetUpView : Form, ISetUpView
    {
        private readonly ApplicationContext _context;
        public int NumberOfFloors => (int)numericUpDown1.Value;

        public event Action SetUp;

        public SetUpView(ApplicationContext context)
        {
            _context = context;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetUp?.Invoke();
        }

        public new void Show()
        {
            _context.MainForm = this;
            base.Show();
        }
    }
}
