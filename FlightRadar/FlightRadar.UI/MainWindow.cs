using FlightRadar.DataAccess;
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

        private void button1_Click(object sender, EventArgs e)
        {
            viewModel.Update();

            listBox1.Items.Clear();

            foreach(string s in viewModel.RawMessages)
            {
                listBox1.Items.Add(s);
            }

        }
    }
}
