using System;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace AppNote
{
    public static class Database
    {
        private const string DbFile = "notes.db";

        public static void Initialize()
        {
            if (!File.Exists(DbFile))
            {
                SQLiteConnection.CreateFile(DbFile);
            }

            using (var conn = new SQLiteConnection($"Data Source={DbFile};Version=3;"))
            {
                conn.Open();
                string createUsersTable = @"CREATE TABLE IF NOT EXISTS Users (
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            Username TEXT UNIQUE NOT NULL,
                                            PasswordHash TEXT NOT NULL
                                        );";

                string createNotesTable = @"CREATE TABLE IF NOT EXISTS Notes (
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            UserId INTEGER,
                                            Content TEXT,
                                            CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
                                            FOREIGN KEY (UserId) REFERENCES Users(Id)
                                        );";

                using (var cmd = new SQLiteCommand(createUsersTable, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SQLiteCommand(createNotesTable, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static bool Login(string username, string password)
        {
            using (var conn = new SQLiteConnection($"Data Source={DbFile};Version=3;"))
            {
                conn.Open();
                string query = "SELECT PasswordHash FROM Users WHERE Username = @username";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    var result = cmd.ExecuteScalar();
                    return result != null && VerifyPassword(password, result.ToString());
                }
            }
        }

        public static bool Register(string username, string password)
        {
            using (var conn = new SQLiteConnection($"Data Source={DbFile};Version=3;"))
            {
                conn.Open();
                string checkUserQuery = "SELECT COUNT(*) FROM Users WHERE Username = @username";
                using (var cmd = new SQLiteCommand(checkUserQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    var count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        return false; // Użytkownik już istnieje
                    }
                }

                string insertUserQuery = "INSERT INTO Users (Username, PasswordHash) VALUES (@username, @passwordHash)";
                using (var cmd = new SQLiteCommand(insertUserQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@passwordHash", HashPassword(password));
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
        }

        private static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        private static bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
    }
}
