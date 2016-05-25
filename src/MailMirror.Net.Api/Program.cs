namespace MailMirror.Net.Api
{
    using System;
    using System.Threading;

    using MailMirror.Net.Common.Models;

    using Microsoft.Owin.Hosting;

    internal static class Program
    {
        private static void Main(string[] args)
        {
            var quitEvent = new ManualResetEvent(false);
            Console.CancelKeyPress += (sender, eArgs) =>
            {
                quitEvent.Set();
                eArgs.Cancel = true;
            };

            var baseAddress = $"http://*:{Constants.Port}";

            using (WebApp.Start<Startup>(baseAddress))
            {
                Console.WriteLine($"Running on {baseAddress}...");
                quitEvent.WaitOne();
            }

            Console.WriteLine("Exiting...");
        }
    }
}