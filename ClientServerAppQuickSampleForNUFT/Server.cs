using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Server;

namespace ClientServerAppQuickSampleForNUFT
{
    /// <summary>
    /// Class which provides logic as the server.
    /// Receives messages from the clients and sent message beck to the client.
    /// </summary>
    internal class Server : IServer
    {
        private const int PortNo = 5000;
        private const string ServerIp = "127.0.0.1";
        private readonly SqliteDataAccess _dataAccess;

        /// <summary>
        /// File path for Messages.db"
        /// </summary>
        private static string DbFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "___NUFT", "Messages.db");

        /// <summary>
        /// Creates instance of <see cref="Server"/>
        /// </summary>
        public Server()
        {
            _dataAccess = new SqliteDataAccess(DbFilePath);

            if (!File.Exists(DbFilePath))
            {
                new DbService().CreateDbFile(DbFilePath);
            }
        }

        /// <summary>
        /// Runs server
        /// </summary>
        public void Run()
        {
            //---listen at the specified IP and port no.---
            IPAddress localAdd = IPAddress.Parse(ServerIp);
            TcpListener listener = new TcpListener(localAdd, PortNo);
            Console.WriteLine("Listening...");
            listener.Start();

            TcpClient client;
            while (true)
            {
                //---incoming client connected---
                client = listener.AcceptTcpClient();

                //---get the incoming data through a network stream---
                NetworkStream nwStream = client.GetStream();
                byte[] buffer = new byte[client.ReceiveBufferSize];


                //---read incoming stream---
                int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

                //---convert the data received into a string---
                string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                var data = dataReceived.Split('*');
                _dataAccess.InsertMessage(new Message
                {
                    User = data.First(),
                    Text = data.Last()
                });
                ConsoleReceived($"{data.First()} : {data.Last()}");

                //---write back the text to the client---
                ConsoleSending("Sending back : " + data.Last());
                nwStream.Write(buffer, 0, bytesRead);
            }

            client.Close();
            listener.Stop();
            Console.ReadLine();
        }

        private static void ConsoleReceived(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Received from the client: " + message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void ConsoleSending(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Sending : " + message.Split('*').Last());
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}