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
        public FormServer()
        {
            InitializeComponent();
            textBox1.Text = "work";
        }
        
        private void btnStart_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            Server.StartServer();
        }

        private void SomeEventHanler (object sender, EventArgs args)
        {
            Task.Factory.StartNew(() => Server.serverDisplay())
    .ContinueWith(t => textBox1.AppendText(t.Result)
        , CancellationToken.None
        , TaskContinuationOptions.None
        , TaskScheduler.FromCurrentSynchronizationContext());

        }
        private void tbDisplay_TextChanged(object sender, EventArgs e)
        {
            MessageBox.Show("This can work");
        }
    }
}
