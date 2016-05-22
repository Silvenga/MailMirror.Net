namespace MailMirror.Net.Api.Controllers
{
    using System;

    public class Message
    {
        // http://www.postfix.org/pipe.8.html

        public DateTime CreatedOn { get; set; }

        public DateTime ExpiresOn { get; set; }

        public string MessageId { get; set; }

        public string PostfixQueueId { get; set; }

        public string Sender { get; set; }

        public string Recipient { get; set; }

        public string Eml { get; set; }
    }
}