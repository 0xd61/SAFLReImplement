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

using FlightRadar.Service;
using FlightRadar.DataAccess;
using FlightRadar.Model;

namespace FlightRadar.UI
{
    public partial class Form1 : Form
    {
        private MessageViewModel msgViewModel = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            msgViewModel = new MessageViewModel(new LocalMessageRepository());
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            
            msgViewModel.Update();

            listView1.Items.Clear();
            foreach(ADSBMessageBase msg in msgViewModel.MessageList)
            {
                listView1.Items.Add(msg.Name);
            }
        }
    }
}
