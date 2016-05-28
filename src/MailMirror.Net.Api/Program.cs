namespace MailMirror.Net.Api
{
    using System;

    using MailMirror.Net.Common.Models;

    using Microsoft.Owin.Hosting;

    internal static class Program
    {
        private static void Main(string[] args)
        {
            var baseAddress = $"http://*:{Constants.Port}";

            using (WebApp.Start<Startup>(baseAddress))
            {
                Console.WriteLine($"Running on {baseAddress}...");
                Console.WriteLine("Type `exit` to shutdown.");
                while (Console.ReadLine() != "exit")
                {
                }
            }

            Console.WriteLine("Exiting...");
        }
    }
}