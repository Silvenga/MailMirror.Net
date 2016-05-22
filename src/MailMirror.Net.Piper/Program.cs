namespace MailMirror.Net.Piper
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("eml: stdin");
                Console.WriteLine("<queueId> <recipient> <sender>");
                throw new ArgumentException();
            }

            Task.Run(async () => await MainAsync(args)).Wait();
        }

        private static async Task MainAsync(IReadOnlyList<string> args)
        {
            var queueId = args[0];
            var recipient = args[1];
            var sender = args[2];
            var eml = await Console.In.ReadToEndAsync();

            Console.WriteLine("Sending post.");

            var client = new Client();
            await client.SendAsync(eml, queueId, recipient, sender);
        }
    }
}