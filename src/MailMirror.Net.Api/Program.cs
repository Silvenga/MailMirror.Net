namespace MailMirror.Net.Api
{
    using System;

    using MailMirror.Net.Common.Models;

    using Microsoft.Owin.Hosting;

    internal static class Program
    {
        private static void Main(string[] args)
        {
            var baseAddress = $"http://localhost:{Constants.Port}";

            using (WebApp.Start<Startup>(baseAddress))
            {
                Console.WriteLine("Running...");
                Console.ReadLine();
            }
        }
    }
}