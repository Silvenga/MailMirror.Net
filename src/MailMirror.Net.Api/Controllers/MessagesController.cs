namespace MailMirror.Net.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using MailMirror.Net.Api.Data;

    [RoutePrefix("api/messages")]
    public class MessagesController : ApiController
    {
        private readonly IMessagesDb _messagesDb;

        public MessagesController(IMessagesDb messagesDb)
        {
            _messagesDb = messagesDb;
        }

        [Route, HttpPost]
        public IHttpActionResult Import(Message message)
        {
            _messagesDb.Messages.Add(message);

            return Ok();
        }

        [Route, HttpGet]
        public IHttpActionResult List()
        {
            var messages = _messagesDb
                .Messages
                .ToList();

            return Ok(messages);
        }

        [Route("{messageId}"), HttpGet]
        public IHttpActionResult Get(string messageId)
        {
            var messages = _messagesDb
                .Messages
                .ToList()
                .FirstOrDefault(x => x.MessageId == messageId);

            if (messages == null)
            {
                return NotFound();
            }

            return Ok(messages);
        }
    }
}