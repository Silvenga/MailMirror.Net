namespace MailMirror.Net.Api.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
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

        [Route("list/from"), HttpGet]
        public IHttpActionResult ListFrom()
        {
            var messages = _messagesDb
                .ListAll()
                .Select(x => x.FromAddress);

            return Ok(messages);
        }

        [Route("messageId/{messageId}"), HttpGet]
        public IHttpActionResult GetByMessageId(string messageId)
        {
            var message = _messagesDb
                .FindByMessageId(messageId);

            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }

        [Route("id/{id}"), HttpGet]
        public IHttpActionResult GetById(string id)
        {
            var message = _messagesDb
                .FindById(id);

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

        [Route("id/{id}/eml"), HttpGet]
        public HttpResponseMessage GetByIdEml(string id)
        {
            var message = _messagesDb
                .FindById(id);

            if (message == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(message.Eml ?? ""));
            response.Content = new StreamContent(stream);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("message/rfc822");

            return response;
        }
    }
}