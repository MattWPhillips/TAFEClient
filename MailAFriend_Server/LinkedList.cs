using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailAFriend_Server
{
    public class LinkedList
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

        public string DisplayAllNodes()
        {
            Node node = firstNode;
            String emails = null;
            while (node != null)
            {
                emails += node.data.sendFormat();
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

}
