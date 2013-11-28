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
        int lastTime = 0;

        public delegate void clientHandler(string data, Hashtable emails, object[] newEmails);

        public void clientDoneHandlerMessage(string data, Hashtable emails, object[] emailList)
        {
            try
            {
                if (data.IndexOf("Invalid user") > -1)
                {
                    btnConnect.Enabled = true;
                    btnSend.Enabled = false;
                    btnDisconnect.Enabled = false;
                    btnGetMail.Enabled = false;
                    tbName.Enabled = true;
                    tbPassword.Enabled = true;
                    tbDisplay.Text = "Incorrect Username/Password";
                }
                else
                {
                    data = data.Remove(data.Length - 11);
                    tbDisplay.Text = data;
                    if (emails.Count > 0 && emails.Count > lastTime)
                    {
                        lastTime = emails.Count;
                        listBox1.Items.Clear();
                        newEmails = emails;
                        listBox1.Items.AddRange(emailList);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
            btnConnect.Enabled = false;
            tbName.Enabled = false;
            tbPassword.Enabled = false;
            btnSend.Enabled = true;
            btnDisconnect.Enabled = true;
            btnGetMail.Enabled = true;
            tbDisplay.Text = "Connecting to Server...";
            client.startTheClient("");
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            tbDisplay.Text = "Disconnected";
            btnConnect.Enabled = true;
            btnSend.Enabled = false;
            btnDisconnect.Enabled = false;
            btnGetMail.Enabled = false;
            tbName.Enabled = true;
            tbPassword.Enabled = true;
            client.stopTheClient();
        }

        private void clientDoneHandler(string message, Hashtable emails, object[] newEmails)
        {
            this.BeginInvoke(new clientHandler(clientDoneHandlerMessage), new Object[] { message, emails, newEmails });
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
            tbFrom.Text = tbName.Text;
            Mail mail = new Mail(tbName.Text, tbTo.Text, TbSubject.Text, tbDisplay.Text);
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
