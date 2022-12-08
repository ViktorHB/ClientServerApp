using Dapper;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace Server
{
    /// <summary>
    /// Class which provides logic to access to the message data
    /// </summary>
    internal class SqliteDataAccess : ISqliteDataAccess
    {
        private readonly string _dbFilePath;

        /// <summary>
        /// creates instance of <see cref="SqliteDataAccess"/>
        /// </summary>
        /// <param name="dbFilePath"></param>
        public SqliteDataAccess(string dbFilePath)
        {
            _dbFilePath = dbFilePath;
        }

        /// <summary>
        /// Gets data from the table
        /// </summary>
        /// <returns>Data</returns>
        public List<Message> GetMessages()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Message>("SELECT * FROM Messages", new DynamicParameters());
                return output.ToList();
            }
        }

        /// <summary>
        /// Inserts data into the table
        /// </summary>
        /// <param name="message">Data <see cref="Message"/></param>
        public void InsertMessage(Message message)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("INSERT INTO Messages (user, message) VALUES (@User, @Text)", message);
            }
        }

        /// <summary>
        /// Removes data from the table
        /// </summary>
        public void Delete()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<SqliteConnection>("DELETE FROM Messages", new DynamicParameters());
            }
        }

        private string LoadConnectionString() => $"Data Source={_dbFilePath};Version=3;";
    }
}
