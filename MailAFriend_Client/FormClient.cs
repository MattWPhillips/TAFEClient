using System;
using System.Collections;
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
        Hashtable newEmails = new Hashtable();
        int indx = 0;
        
        public delegate void clientHandler(string data, Hashtable emails);

        public void clientDoneHandlerMessage(string data, Hashtable emails)
        {
            tbDisplay.Text = data;
            newEmails = emails;
            createEmailList();
        }

        private void createEmailList()
        {
            string[] emailList = null;

            foreach (string value in newEmails.Values)
            {
                emailList[indx] = value;
                indx++;
            }
            listBox1.Items.AddRange(emailList);
        }

        public FormClient()
        {
            InitializeComponent();
            client.clientThreadComplete += new Client.clientThreadHandler(clientDoneHandler);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            client.username = tbName.Text;
            client.password = tbPassword.Text;
            tbDisplay.Text = "Connecting to Server...";
            client.startClient();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {

        }

        private void clientDoneHandler(string message, Hashtable emails)
        {
            this.BeginInvoke(new clientHandler(clientDoneHandlerMessage), new Object[] { message, emails });
        }

    }
}
