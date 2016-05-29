namespace MailMirror.Net.Api.Controllers
{
    using System.Web.Http;

    using MailMirror.Net.Api.Data;
    using MailMirror.Net.Common.Models;

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
            var result = _messagesDb.Import(message);

            return result
                ? (IHttpActionResult) Ok()
                : BadRequest();
        }

        [Route, HttpGet]
        public IHttpActionResult List()
        {
            var messages = _messagesDb
                .ListAll();

            return Ok(messages);
        }

        [Route("id/{messageId}"), HttpGet]
        public IHttpActionResult Get(string messageId)
        {
            var message = _messagesDb
                .FindByMessageId(messageId);

            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }

        [Route("from/{fromRecipient}"), HttpGet]
        public IHttpActionResult GetByFrom(string fromRecipient)
        {
            var message = _messagesDb
                .FindByFrom(fromRecipient);

            return Ok(message);
        }
    }
}