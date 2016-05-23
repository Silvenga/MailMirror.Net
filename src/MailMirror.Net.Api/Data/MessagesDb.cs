namespace MailMirror.Net.Api.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MailMirror.Net.Common.Models;

    public interface IMessagesDb
    {
        HashSet<Message> Messages { get; }
        bool Import(Message message);
        IEnumerable<Message> ListAll();
        Message Find(string messageId);
    }

    public class MessagesDb : IMessagesDb
    {
        private static readonly string[] NewLineSeparator = {"\r\n", "\n"};

        public HashSet<Message> Messages { get; } = new HashSet<Message>();

        public bool Import(Message message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (message.Eml == null)
            {
                throw new ArgumentNullException(nameof(message.Eml));
            }

            var messageIdHeader = message.Eml
                                         .Split(NewLineSeparator, StringSplitOptions.None)
                                         .FirstOrDefault(x => x.StartsWith(Constants.MessageIdHeader));
            var messageId = messageIdHeader
                ?.Remove(0, Constants.MessageIdHeader.Length)
                 .Trim();

            message.MessageId = messageId;
            message.CreatedOn = DateTime.Now;
            message.ExpiresOn = message.CreatedOn.AddHours(1);

            var result = Messages.Add(message);

            var status = result ? "success" : "failure";
            Console.WriteLine($"Save of message {messageId ?? "No Id"} completed with {status}.");

            return result;
        }

        public IEnumerable<Message> ListAll()
        {
            return Messages.ToList();
        }

        public Message Find(string messageId)
        {
            return ListAll()
                .FirstOrDefault(x => x.MessageId == messageId);
        }
    }
}