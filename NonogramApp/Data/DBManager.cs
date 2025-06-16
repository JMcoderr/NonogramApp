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
        // Track the currently logged-in user (null if not logged in)
        public static User? CurrentUser { get; private set; }

        // Call this after a successful login
        public static void SetCurrentUser(User user)
        {
            CurrentUser = user;
        }

        // Log out the current user
        public static void Logout()
        {
            CurrentUser = null;
        }

        // Check if a user is logged in
        public static bool IsUserLoggedIn()
        {
            return CurrentUser != null;
        }

        // Add score to the current user and save to users.json
        public static void AddScoreToCurrentUser(int scoreToAdd)
        {
            if (CurrentUser == null)
                return;

            var users = DataManager.LoadUsers();
            var user = users.FirstOrDefault(u => u.Username == CurrentUser.Username);
            if (user != null)
            {
                user.Score += scoreToAdd;
                DataManager.SaveUsers(users);

                // Update CurrentUser reference to the new score
                CurrentUser.Score = user.Score;
            }
            else
            {
                // Optionally, handle the case where the user is not found
                System.Windows.Forms.MessageBox.Show(
                    "User not found in database. Score not saved.",
                    "Error",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error
                );
            }
        }
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
                return [];

            string json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<User>>(json) ?? [];
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

            if (user != null)
            {
                DBManager.SetCurrentUser(user);
                return true;
            }
            return false;
        }
    }
}
