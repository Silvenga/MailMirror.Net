namespace MailMirror.Net.Api.Data
{
    using System.Collections.Generic;

    using MailMirror.Net.Api.Controllers;

    public interface IMessagesDb
    {
        HashSet<Message> Messages { get; set; }
    }

    public class MessagesDb : IMessagesDb
    {
        public HashSet<Message> Messages { get; set; } = new HashSet<Message>();
    }
}