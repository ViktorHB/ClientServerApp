using System;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var userGuid = Guid.NewGuid().ToString();
            var client = new Client();
            while (true)
            {
                client.SendMessage($"{userGuid}*{Console.ReadLine()}");
            }
        }
    }
}