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
                Console.WriteLine("Enter twice to exit.");
                Console.ReadLine();
                Console.ReadLine();
            }

            Console.WriteLine("Exiting...");
        }
    }
}