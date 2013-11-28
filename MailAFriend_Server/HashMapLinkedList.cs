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
        
        
        public string newEmail(string data)
        {
            LinkedList emailList = new LinkedList();
            String[] mailData;
            data = data.Remove(data.Length - 11);
            mailData = data.Split('|');
            Mail mail = new Mail(mailData[2], mailData[3], mailData[4], mailData[5]);

            if (emailDatabase.ContainsKey(mail.to))
            {
                emailList = (LinkedList)emailDatabase[mail.to];
                emailList.add(mail);
                emailDatabase[mail.to] = emailList;
            }
            else 
            {
                emailList.add(mail);
                emailDatabase.Add(mail.to, emailList);
            }
            return "message sent<SEND><EOF>";
            
        }


        public String retrieveEmail(string data)
        {
            String usersEmails = null;
            String user = null;
            LinkedList tempList = new LinkedList();
            String[] userPass;
            
            data = data.Remove(data.Length - 11);
            userPass = data.Split('|');
            user = userPass[0];

            if (emailDatabase.ContainsKey(user))
            {
                tempList = (LinkedList)emailDatabase[user];
                usersEmails = tempList.DisplayAllNodes() + "<RETR><EOF>";
            }
            else
            {
                usersEmails = "<EOF>";
            }
            return usersEmails;
        }

    }
}
