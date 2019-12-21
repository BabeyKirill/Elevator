using Elevator.Model;
using Elevator.Presenter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Elevator.View
{
    public partial class PassengersInfoView : Form, IPassengersInfoView
    {
        public PassengersInfoView()
        {
            InitializeComponent();
        }

        public void ViewPassengersInfo(List<Passenger> Passengers)
        {
            flowLayoutPanel1.Controls.Clear();
            for (int i = 0; i < Passengers.Count; i++)
            {               
                FlowLayoutPanel pan = new FlowLayoutPanel();
                pan.Size = new System.Drawing.Size(600, 25);
                Label lab1 = new Label();
                Label lab2 = new Label();
                Label lab3 = new Label();
                Label lab4 = new Label();
                Label lab5 = new Label();

                lab1.BackColor = System.Drawing.Color.Moccasin;
                lab1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                lab1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                lab1.Size = new System.Drawing.Size(80, 25);
                lab1.Text = $"Passenger {i+1}";

                lab2.BackColor = System.Drawing.Color.Moccasin;
                lab2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                lab2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                lab2.Size = new System.Drawing.Size(80, 25);
                lab2.Text = $"Current floor: {Passengers[i].CurrentFloor}";

                lab3.BackColor = System.Drawing.Color.Moccasin;
                lab3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                lab3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                lab3.Size = new System.Drawing.Size(100, 25);
                lab3.Text = $"Wanted floor: {Passengers[i].WantedFloor}";

                lab4.BackColor = System.Drawing.Color.Moccasin;
                lab4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                lab4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                lab4.Size = new System.Drawing.Size(150, 25);
                lab4.Text = $"Status: {Passengers[i].Status}";

                lab5.BackColor = System.Drawing.Color.Moccasin;
                lab5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                lab5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                lab5.Size = new System.Drawing.Size(80, 25);
                lab5.Text = $"Weight: {Passengers[i].Weight}";

                pan.Controls.Add(lab1);
                pan.Controls.Add(lab2);
                pan.Controls.Add(lab3);
                pan.Controls.Add(lab4);
                pan.Controls.Add(lab5);
                flowLayoutPanel1.Controls.Add(pan);
            }
        }
    }
}
