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
    public partial class SetUpView : Form, ISetUpView
    {
        private readonly ApplicationContext _context;

        public SetUpView(ApplicationContext context)
        {
            _context = context;
            InitializeComponent();
        }

        public decimal NumberOfFloors => numericUpDown1.Value;

        public event Action SetUp;

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
