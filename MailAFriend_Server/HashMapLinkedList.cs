using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MailAFriend_Server
{
    class HashMapLinkedList
    {

        Hashtable emailDatabase = new Hashtable();
        LinkedList emailList = new LinkedList();
        
        
        public void newEmail(string data)
        {
            LinkedList emailList = new LinkedList();
            String[] mailData;
            data = data.Remove(data.Length - 20);
            mailData = data.Split('|');
            Mail mail = new Mail(mailData[0], mailData[1], mailData[2], mailData[3]);

            if (emailDatabase.ContainsKey(mail.to))
            {
                emailList = (LinkedList)emailDatabase[mail.to];
                emailList.add(mail);
                emailDatabase.Add(mail.to, emailList);
            }
            else 
            {
                emailList.add(mail);
                emailDatabase.Add(mail.to, emailList);
            }
            
        }


        public Hashtable retrieveEmail(string userName)
        {

            Hashtable usersEmails = new Hashtable();
            LinkedList tempList = new LinkedList();
            userName = userName.Remove(userName.Length - 20);

            if (emailDatabase.ContainsKey(userName))
            {
                tempList = (LinkedList)emailDatabase[userName];
                usersEmails = tempList.DisplayAllNodes();
            }
            else
            {
                usersEmails = null;
            }
            return usersEmails;
        }

    }
}
