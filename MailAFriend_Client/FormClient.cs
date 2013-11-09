﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailAFriend_Client
{
    public partial class FormClient : Form
    {
        public FormClient()
        {
            InitializeComponent();
        }

        public void clientDislpay(string data)
        {
            tbDisplay.Text = data;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Client.startClient();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            Client.stopClient();
        }

    }
}