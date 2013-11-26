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

namespace MailAFriend_Server
{
    public partial class FormServer : Form
    {
        Server server = new Server();

        public delegate void serverHandler();

        public void serverDoneHandlerMessage()
        {
            tbServer.Text = "Server Stoped";
        }


        public FormServer()
        {
            InitializeComponent();
            server.serverThreadComplete += new Server.serverThreadHandler(this.serverDoneHandler);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            tbServer.Text = "Server Started";
            server.startTheServer();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            server.stopTheServer();
        }

        private void serverDoneHandler()
        {
            this.BeginInvoke(new serverHandler(serverDoneHandlerMessage), new Object[] {});
            tbServer.Text = "Server Stoped";
        }

    }
}
