namespace MailMirror.Net.Api.Tests
{
    using System.IO;

    using FluentAssertions;

    using MailMirror.Net.Api.Data;
    using MailMirror.Net.Common.Models;

    using Xunit;

    public class ParsingFacts
    {
        private const string EmlDumpFile = "Assets/dump";
        private readonly string _eml;

        public ParsingFacts()
        {
            _eml = File.ReadAllText(EmlDumpFile);
        }

        [Fact]
        public void Can_parse_messageId()
        {
            var parser = new EmlParser();

            var message = new Message
            {
                Eml = _eml
            };

            // Act
            message = parser.PopulateEml(message);

            // Assert
            message.MessageId.Should().Be("CB607111-84FE-48DE-B3C0-3C86EC8A68A9");
        }

        [Fact]
        public void Can_parse_from()
        {
            var parser = new EmlParser();

            var message = new Message
            {
                Eml = _eml
            };

            // Act
            message = parser.PopulateEml(message);

            // Assert
            message.FromAddress.Should().Be("from-test@silvenga.com");
            message.FromDisplayName.Should().Be("Name from");
        }

        [Fact]
        public void Can_parse_subject()
        {
            var parser = new EmlParser();

            var message = new Message
            {
                Eml = _eml
            };

            // Act
            message = parser.PopulateEml(message);

            // Assert
            message.Subject.Should().Be("subject");
        }

        [Fact]
        public void Can_parse_postfix_queue()
        {
            var parser = new EmlParser();

            var message = new Message
            {
                Eml = _eml
            };

            // Act
            message = parser.PopulateEml(message);

            // Assert
            message.PostfixQueueId.Should().Be("B7FC041B58");
        }
    }
}