using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MailAFriend_Client
{
    class HashMapLinkedList
    {
        LinkedList emailList = new LinkedList();
        
        
        public void newEmail(string data)
        {
            LinkedList emailList = new LinkedList();
            String[] mailData;
            data = data.Remove(data.Length - 7);
            mailData = data.Split('|');
            Mail mail = new Mail(mailData[0], mailData[1], mailData[2], mailData[3]);
            emailList.add(mail);
        }


        public Hashtable retrieveEmail()
        {

            Hashtable usersEmails = new Hashtable();

                usersEmails = emailList.DisplayAllNodes();

            return usersEmails;
        }

    }
}
