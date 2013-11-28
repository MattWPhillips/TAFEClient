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
            MessageBox.Show("Job Done");
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
            client.startTheClient("");
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            tbDisplay.Text = "Disconnected";
            client.stopTheClient();
        }

        private void clientDoneHandler(string message, Hashtable emails)
        {
            this.BeginInvoke(new clientHandler(clientDoneHandlerMessage), new Object[] { message, emails });
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            String data;
            int selection = (int)listBox1.SelectedItem;
            data = (String)newEmails[selection];
            Mail mail = new Mail(data);
            tbFrom.Text = mail.from;
            tbTo.Text = mail.to;
            TbSubject.Text = mail.subject;
            tbDisplay.Text = mail.content;
            mail.read = "yes";

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            String tempmessge = null;
            Mail mail = new Mail(tbFrom.Text, tbTo.Text, TbSubject.Text, tbDisplay.Text);
            tempmessge = mail.sendFormat() + "<SEND>";

            client.startTheClient(tempmessge);   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            client.username = tbName.Text;
            client.password = tbPassword.Text;
            tbDisplay.Text = "Retieving mail";
            client.startTheClient("<RETR>");
        }
    }
}
