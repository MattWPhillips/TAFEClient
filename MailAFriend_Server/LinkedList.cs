using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailAFriend_Server
{
    class LinkedList
    {
        private Node firstNode;
        private Node lastNode;

        int numOfNodes;

        public bool Empty
        {
            get { return this.numOfNodes == 0; }
        }

        public int Count
        {
            get { return this.numOfNodes; }
        }

        public void add(Mail newData)
        {
            Node node = new Node(newData);

            if (firstNode == null)
            {
                firstNode = node;
            }
            if (lastNode != null)
            {
                lastNode.next = node;
            }
            lastNode = node;
            numOfNodes++;
        }

        public void remove(Mail dataRemove)
        {
            Node temp = firstNode;
            Node prev = firstNode;

            while (temp != null)
            {
                if (temp.data == dataRemove)
                {
                    prev.next = temp.next;
                    return;
                }
                prev = temp;
                temp = temp.next;
            }
        }

        public Hashtable DisplayAllNodes()
        {
            Node node = firstNode;
            Hashtable emails = new Hashtable();
            int indx = 0;

            while (node != null)
            {
                emails.Add(indx, node.data.sendFormat());
                indx++;
                node = node.next;
            }
            return emails;
        }
        
        public void receivedMail(Mail dataRemove)
        {
            Node temp = firstNode;
            Node prev = firstNode;

            while (temp != null)
            {
                temp.data.received = "yes";
                prev = temp;
                temp = temp.next;
            }
        }
    }

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
