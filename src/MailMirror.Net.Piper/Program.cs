namespace MailMirror.Net.Piper
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal static class Program
    {
        private static void Main(string[] args)
        {
            Task.Run(async () => await MainAsync(args)).Wait();
        }

        private static async Task MainAsync(IReadOnlyList<string> args)
        {
            var eml = await Console.In.ReadToEndAsync();

            Console.WriteLine("Sending post.");

            var client = new Client();
            await client.SendAsync(eml);
        }
    }
}