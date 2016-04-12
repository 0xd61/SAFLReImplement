using FlightRadar.DataAccess;
using FlightRadar.Model;
using FlightRadar.Service.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlightRadar.UI
{
    public partial class MainWindow : Form
    {
        private MessageViewModel viewModel = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            viewModel = new MessageViewModel();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            viewModel?.Dispose();
        }

        private void timerPlanes_Tick(object sender, EventArgs e)
        {
            viewModel.Update();

            //listBox1.Items.Clear();

            foreach (KeyValuePair<string, Plane> entry in viewModel.Planes)
            {
                if(!listBox1.Items.Contains(entry.Key))
                    listBox1.Items.Add(entry.Key);
            }
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
                return;

            string icao = listBox1.SelectedItem.ToString();

            ADSBPositionMessage positionMessage = viewModel.GetPositionMessage(icao);
            if (positionMessage != null)
                textBoxAltitude.Text = positionMessage.Altitude.ToString();
            else
                textBoxAltitude.Text = "No message available";

            ADSBVelocityMessage velocityMessage = viewModel.GetVelocityMessage(icao);
            if (velocityMessage != null)
                textBoxSpeed.Text = velocityMessage.Speed.ToString();
            else
                textBoxSpeed.Text = "No message available";

           ADSBIdentificationMessage idMessage = viewModel.GetIdentificationMessage(icao);
            if (idMessage != null)
                textBoxAircraftID.Text = idMessage.AircraftID.ToString();
            else
                textBoxAircraftID.Text = "No message available";
        }

        private void textBoxAltitude_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
