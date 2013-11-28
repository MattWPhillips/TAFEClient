using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailAFriend_Client
{
    public class Mail
    {

        public String from { get; set; }
        public String to { get; set; }
        public String subject { get; set; }
        public String content { get; set; }
        public String received { get; set; }
        public String read { get; set; }

        public Mail(String data) 
        {
            String[] mailData;
            mailData = data.Split('|');

            this.from = mailData[0];
            this.to = mailData[1];
            this.subject = mailData[2];
            this.content = mailData[3];
            this.received = mailData[4];
            this.read = mailData[5];
        }

        public Mail(String from, String to, String subject, String content)
        {

            this.from = from;
            this.to = to;
            this.subject = subject;
            this.content = content;
        }

        public String sendFormat()
        {
            return from + "|" + to + "|" + subject + "|" + content;
        }


    }
}
