using System.Net.Sockets;

namespace Client
{
    /// <summary>
    /// Class which implements logic to sending simple text
    /// messages to the server via tcp connection <see cref="TcpClient"/>
    /// and receive message back to the client
    /// </summary>
    internal interface IClient
    {

        /// <summary>
        /// Sends text messages to the server and receive message back to the client
        /// </summary>
        /// <param name="message">Message</param>
        void SendMessage(string message);
    }
}