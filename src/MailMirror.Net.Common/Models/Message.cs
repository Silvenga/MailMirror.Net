namespace MailMirror.Net.Common.Models
{
    using System;

    public class Message
    {
        // http://www.postfix.org/pipe.8.html

        public DateTime CreatedOn { get; set; }

        public DateTime ExpiresOn { get; set; }

        public string MessageId { get; set; }

        public string Eml { get; set; }
        public string FromAddress { get; set; }
        public string FromDisplayName { get; set; }

        public string Subject { get; set; }

        public string PostfixQueueId { get; set; }
    }
}