namespace Server
{
    /// <summary>
    /// An interface which provides contract for server.
    /// Receives messages from the clients and sent message beck to the client.
    /// </summary>
    internal interface IServer
    {
        /// <summary>
        /// Runs server
        /// </summary>
        void Run();
    }
}