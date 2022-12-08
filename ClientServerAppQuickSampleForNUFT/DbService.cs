using System;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// Class which provides logic to manage db file
    /// </summary>
    internal class DbService : IDbService
    {
        private readonly string _dirPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "___NUFT");

        /// <inheritdoc cref="IDbService"/>
        public async Task CreateDbFile(string path)
        {
            await Task.Run(async () =>
            {
                if (!Directory.Exists(_dirPath))
                {
                    Directory.CreateDirectory(_dirPath);
                }
                SQLiteConnection.CreateFile(path);
                await CreateTable(path);
            });
        }

        private static async Task CreateTable(string path)
        {
            try
            {
                await Task.Run(() =>
                {
                    using (SQLiteConnection dbConnection = new SQLiteConnection("Data Source=" + path + ";Version=3;"))
                    {
                        dbConnection.Open();
                        string sql = "CREATE TABLE Messages(" +
                                    "user VARCHAR(50)," +
                                    "message VARCHAR(1200)" +
                                    ")";
                        using (SQLiteCommand command = new SQLiteCommand(sql, dbConnection))
                        {
                            command.ExecuteNonQuery();
                        }
                        dbConnection.Close();
                    }
                });
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}