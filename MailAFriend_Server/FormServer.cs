﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailAFriend_Server
{
    public partial class FormServer : Form
    {

        public string displayData;

        public FormServer()
        {
            InitializeComponent();
            tbServer.Text = "Server Started";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Server.StartServer();
        }
    }
}
