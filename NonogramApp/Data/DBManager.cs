using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using NonogramApp.Models;

namespace NonogramApp.Data
{
    internal class DBManager
    {

    }

    public static class DataManager
    {
        private static string _path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\users.json"));

        public static string FilePath
        {
            get => _path;
            set => _path = value;
        }

        // Get data from the JSON
        public static List<User> LoadUsers()
        {
            if (!File.Exists(FilePath))
                return new List<User>();

            string json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }

        // Save users
        public static void SaveUsers(List<User> users)
        {
            string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }

        // Save a new user
        public static void AddUser(User user)
        {
            List<User> users = LoadUsers();

            int maxId = users.Count > 0 ? users.Max(u => u.Id) : 0;
            user.Id = maxId + 1;

            users.Add(user);
            SaveUsers(users);
        }

        // Check if username is available
        public static User? GetUserByUsername(string username)
        {
            return LoadUsers().FirstOrDefault(u => u.Username == username);
        }

        public static bool Login(string username, string password, out User? user)
        {
            user = LoadUsers().FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password // Replace with Hash(password) if using hashes
            );

            return user != null;
        }
    }
}
