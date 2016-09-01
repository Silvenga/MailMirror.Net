namespace MailMirror.Net.Api.Data
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    using MailMirror.Net.Common.Models;

    using MimeKit;

    public class EmlParser
    {
        public Message PopulateEml(Message message)
        {
            var emlStream = GenerateStreamFromString(message.Eml);
            var mimeMessage = MimeMessage.Load(emlStream);

            message.MessageId = mimeMessage.Headers.FirstOrDefault(x => x.Field == Constants.MessageIdHeader)?.Value;

            var address = mimeMessage.From.FirstOrDefault() as MailboxAddress;
            message.FromAddress = address?.Address;
            message.FromDisplayName = address?.Name;

            message.Subject = mimeMessage.Subject;

            SetQueueId(mimeMessage, message);

            message.Id = Guid.NewGuid();
            message.CreatedOn = DateTime.Now;
            message.ExpiresOn = message.CreatedOn.AddHours(1);

            return message;
        }

        private MemoryStream GenerateStreamFromString(string value)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
        }

        private static void SetQueueId(MimeMessage eml, Message message)
        {
            const string regex = "id (?<id>\\w{10})";

            var match = Regex.Match(eml.Headers.FirstOrDefault(x => x.Field == "Received")?.Value ?? "", regex, RegexOptions.Multiline);
            var value = match.Groups["id"].Value;

            message.PostfixQueueId = value;
        }
    }
}