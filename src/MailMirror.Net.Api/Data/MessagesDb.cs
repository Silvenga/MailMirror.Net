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
        Message FindByMessageId(string messageId);
        IEnumerable<Message> FindByFrom(string @from);
        Message FindById(string messageId);
    }

    public class MessagesDb : IMessagesDb
    {
        private readonly EmlParser _parser = new EmlParser();

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

            _parser.PopulateEml(message);

            var result = Messages.Add(message);
            PurgeExpired();

            var status = result ? "success" : "failure";
            Console.WriteLine($"Save of message {message.Id} from {message.FromAddress} completed with {status}.");

            return result;
        }

        private int PurgeExpired()
        {
            return Messages.RemoveWhere(x => x.ExpiresOn < DateTime.Now);
        }

        public IEnumerable<Message> ListAll()
        {
            return Messages.ToList();
        }

        public Message FindByMessageId(string messageId)
        {
            return ListAll()
                .FirstOrDefault(x => x.MessageId == messageId);
        }

        public Message FindById(string messageId)
        {
            return ListAll()
                .FirstOrDefault(x => x.MessageId == messageId);
        }

        public IEnumerable<Message> FindByFrom(string @from)
        {
            return ListAll().Where(x => x.FromAddress == @from);
        }
    }
}