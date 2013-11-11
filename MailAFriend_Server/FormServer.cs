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
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Program.startTheServer();
        }

        public void FormServer_Load(object sender, System.EventArgs e)
        {

        }
    }
}
