using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MailAFriend_Server
{
    class LoginCheck
    {
        public string user;
        public string password;
        
        public LoginCheck(string data)
        {
            String[] userPass;
            data = data.Remove(data.Length - 5);
            userPass = data.Split('|');
            user = userPass[0];
            password = userPass[1];
        }

        public bool checkID()
        {
            bool validUser = false;
            Hashtable users = new Hashtable();
            users = getNames();

            if (users.ContainsKey(user))
            {
                if (password == (String)users[user])
                {
                    validUser = true;
                }
            }

            return validUser;
        }

        public Hashtable getNames()
        {

            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Matthew\Documents\MailAFriend-Server\MailAFriend_Server\Resource\Users.txt");
            string[] parts;
            Hashtable users = new Hashtable();
            foreach (string line in lines)
            {
                parts = line.Split('|');
                users.Add(parts[0],parts[1]);
            }
            return users;
        }
    }
}
