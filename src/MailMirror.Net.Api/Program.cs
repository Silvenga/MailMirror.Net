namespace MailMirror.Net.Api
{
    using System;

    using Microsoft.Owin.Hosting;

    internal static class Program
    {
        private static void Main(string[] args)
        {
            var baseAddress = "http://localhost:9000/";
            
            using (WebApp.Start<Startup>(baseAddress))
            {
                Console.WriteLine("Running...");
                Console.ReadLine();
            }
        }
    }
}