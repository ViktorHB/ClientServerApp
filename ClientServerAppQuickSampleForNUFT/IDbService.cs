using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// An interface which provides contract to manage database files
    /// </summary>
    internal interface IDbService
    {
        /// <summary>
        /// Creates database file in the directory
        /// </summary>
        /// <param name="path">Database path</param>
        Task CreateDbFile(string path);
    }
}