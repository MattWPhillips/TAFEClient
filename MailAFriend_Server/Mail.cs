using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailAFriend_Server
{
    class Mail
    {
    
        public String from { get; set; }
        public String to { get; set; }
        public String subject { get; set; }
        public String content { get; set; }
        public String received { get; set; }
        public String read { get; set; }

        public Mail(String from, String to, String subject, String content) 
        {
            
            this.from = from;
            this.to = to;
            this.subject = subject;
            this.content = content;
            this.received = "no";
            this.read = "no";
        } 

        public String sendFormat()
        {
            return from + "|" + to + "|" + subject + "|" + content + "|" + received + "|" + read + "<EOF>";
        }


    }
}
