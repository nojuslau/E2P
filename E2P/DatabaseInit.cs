using E2P.AppSettingsModels;
using Microsoft.Data.Sqlite;
using System;
using System.IO;

namespace E2P
{
    public class DatabaseInit
    {
        private string _initialDbFilePath = "../../../Files/Database/E2P.db";
        private readonly IWritableOptions<ApplicationSettings> _options;

        public DatabaseInit(IWritableOptions<ApplicationSettings> options)
        {
            _options = options;
        }

        public void EnsureDb(string? newDbFilePath = null)
        {
            if (string.IsNullOrEmpty(_options.Value.DatabaseFilePath) || newDbFilePath != null)
            {
                newDbFilePath = newDbFilePath ?? new DirectoryInfo(_initialDbFilePath).FullName;
                UpdateDbPath(newDbFilePath);
            }

            var directory = _options.Value.DatabaseDirectoryPath;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            InitializeDatabase();
        }

        public void UpdateDbPath(string? newDbFilePath = null)
        {
            if (newDbFilePath == null)
            {
                throw new Exception("Database file path is null");
            }

            _options.Update(opt =>
            {
                opt.DatabaseFilePath = newDbFilePath;
                opt.DatabaseDirectoryPath = Path.GetDirectoryName(newDbFilePath);
                opt.ConnectionStrings.DefaultConnection = "Data Source=" + newDbFilePath;
            });
        }

        private void InitializeDatabase()
        {
            string databaseFilePath = _options.Value.DatabaseFilePath;
            if (!File.Exists(databaseFilePath))
            {
                // The database file will be created automatically if it doesn't exist
                SqliteConnection sqliteConnection = new SqliteConnection($@"Data Source={databaseFilePath}");
                if (sqliteConnection.State != System.Data.ConnectionState.Open)
                {
                    sqliteConnection.Open();

                    sqliteConnection.Close();
                    Console.WriteLine("Database created and connection closed.");
                }
            }
        }
    }
}
