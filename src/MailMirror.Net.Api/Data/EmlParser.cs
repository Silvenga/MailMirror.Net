namespace MailMirror.Net.Api.Data
{
    using System;
    using System.Linq;
    using System.Net.Mail;
    using System.Text.RegularExpressions;

    using MailMirror.Net.Common.Models;

    public class EmlParser
    {
        private static readonly string[] NewLineSeparator = {"\r\n", "\n"};

        public Message PopulateEml(Message message)
        {
            SetMessageId(message.Eml, message);
            SetFrom(message.Eml, message);
            SetSubject(message.Eml, message);
            SetQueueId(message.Eml, message);

            message.CreatedOn = DateTime.Now;
            message.ExpiresOn = message.CreatedOn.AddHours(1);

            return message;
        }

        private static void SetMessageId(string eml, Message message)
        {
            var messageIdHeader = eml
                .Split(NewLineSeparator, StringSplitOptions.None)
                .FirstOrDefault(x => x.StartsWith(Constants.MessageIdHeader));
            var messageId = messageIdHeader
                ?.Remove(0, Constants.MessageIdHeader.Length)
                 .Trim();

            message.MessageId = messageId;
        }

        private static void SetFrom(string eml, Message message)
        {
            const string header = "From:";
            var messageIdHeader = eml
                .Split(NewLineSeparator, StringSplitOptions.None)
                .FirstOrDefault(x => x.StartsWith(header));
            var value = messageIdHeader
                ?.Remove(0, header.Length)
                 .Trim();

            if (value != null)
            {
                var address = new MailAddress(value);
                message.FromAddress = address.Address;
                message.FromDisplayName = address.DisplayName;
            }
        }

        private static void SetSubject(string eml, Message message)
        {
            const string header = "Subject:";
            var messageIdHeader = eml
                .Split(NewLineSeparator, StringSplitOptions.None)
                .FirstOrDefault(x => x.StartsWith(header));
            var value = messageIdHeader
                ?.Remove(0, header.Length)
                 .Trim();

            message.Subject = value;
        }

        private static void SetQueueId(string eml, Message message)
        {
            const string regex = "with ESMTP id (?<id>\\w{10})$";

            var match = Regex.Match(eml, regex, RegexOptions.Multiline);
            var value = match.Groups["id"].Value;

            message.PostfixQueueId = value;
        }
    }
}