using System.Collections.Generic;

namespace Server
{
    /// <summary>
    /// An interface which provides contract to access to the message data
    /// </summary>
    internal interface ISqliteDataAccess
    {
        /// <summary>
        /// Gets data from the table
        /// </summary>
        /// <returns>Data</returns>
        List<Message> GetMessages();

        /// <summary>
        /// Inserts data into the table
        /// </summary>
        /// <param name="message">Data <see cref="Message"/></param>
        void InsertMessage(Message message);

        /// <summary>
        /// Removes data from the table
        /// </summary>
        void Delete();
    }
}