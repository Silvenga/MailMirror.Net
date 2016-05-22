namespace MailMirror.Net.Api.Middleware
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    public class LoggerMiddleware
    {
        private readonly Func<IDictionary<string, object>, Task> _next;

        public LoggerMiddleware(Func<IDictionary<string, object>, Task> next)
        {
            _next = next;
        }

        private static int _requestIndex;

        // ReSharper disable once ConsiderUsingAsyncSuffix
        public async Task Invoke(IDictionary<string, object> env)
        {
            var currentIndex = _requestIndex++;

            Console.WriteLine($"{DateTime.Now.ToLongTimeString()} {currentIndex} {env["owin.RequestMethod"]} {env["owin.RequestPath"]}");

            var timer = new Stopwatch();
            timer.Start();
            await _next.Invoke(env);
            timer.Stop();

            var responseHeaders = (IDictionary<string, string[]>) env["owin.ResponseHeaders"];
            var contentLength = responseHeaders["Content-Length"].First();
            Console.WriteLine(
                $"{DateTime.Now.ToLongTimeString()} {currentIndex} {timer.ElapsedMilliseconds}ms {env["owin.ResponseStatusCode"]} {env["owin.ResponseReasonPhrase"]} {contentLength} bytes");
        }
    }
}