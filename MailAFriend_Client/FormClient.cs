using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailAFriend_Client
{
    public partial class FormClient : Form
    {
        Client client = new Client();
        
        public delegate void serverHandler();

        public void clientDoneHandlerMessage()
        {
            tbDisplay.Text = "Server Stoped";
        }

        public FormClient()
        {
            InitializeComponent();
            client.clientThreadComplete += new Client.clientThreadHandler(this.clientDoneHandler);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            tbDisplay.Text = "Connecting to Server...";
            client.startClient();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            client.stopTheServer();
        }

        private void serverDoneHandler()
        {
            this.BeginInvoke(new serverHandler(clientDoneHandlerMessage), new Object[] { });
            tbDisplay.Text = "Server Stoped";
        }

    }
}
