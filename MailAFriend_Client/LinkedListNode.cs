using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailAFriend_Server
{
    public class Node
    {
        public Mail data;
        public Node next;

        public Node()
        {
            data = null;
            next = null;
        }

        public Node(Mail data)
        {
            this.data = data;
            next = null;
        }

        public Node(Mail data, Node next)
        {
            this.data = data;
            this.next = next;
        }

    }
}

