using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    /// <summary>
    /// Class which implements logic to sending simple text
    /// messages to the server via tcp connection <see cref="TcpClient"/>
    /// and receive message back to the client
    /// </summary>
    internal class Client
    {
        private const int PortNo = 5000;
        private const string ServerIp = "127.0.0.1";

        /// <summary>
        /// Sends text messages to the server and receive message back to the client
        /// port <see cref="PortNo"/>
        /// server ip <see cref="ServerIp"/>
        /// </summary>
        /// <param name="message">Message</param>
        public void SendMessage(string message)
        {
            //---create a TCPClient object at the IP and port no.---
            var client = new TcpClient(ServerIp, PortNo);
            var nwStream = client.GetStream();
            byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(message);

            //---send the text---
            ConsoleSending(message);
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);

            //---read back the text---
            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);

            var receivedData = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);
            ConsoleReceived(receivedData);
            client.Close();
        }

        private static void ConsoleReceived(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Received from the server: " + message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void ConsoleSending(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Sending : " + message.Split('*').Last());
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}