using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailAFriend___Server
{
    public partial class FormServer : Form
    {
 
        public FormServer()
        {
            InitializeComponent();
            Server.StartServer();
        }

        public static void serverDislpay(string data)
        {
            FormServer form = new FormServer();
            
            form.tbServer.Text += data + "\r\n";
        }

    }

}
