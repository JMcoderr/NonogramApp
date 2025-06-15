using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using NonogramApp.Models;

namespace NonogramApp.Data
{
    internal class DBManager
    {

    }

    public static class DataManager
    {
        // get users.json
        private static readonly string path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data\users.json"));


        //get data from the json
        public static List<User> LoadUsers()
        {
            if (!File.Exists(path)) 
                return new List<User>();

            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }

        //save users
        public static void SaveUsers(List<User> users)
        {
            string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
        }

        // save a new user
        public static void AddUser(User user)
        {
            List<User> users = LoadUsers();

            int maxId = users.Count > 0 ? users.Max(u => u.Id) : 0;
            user.Id = maxId + 1;

            users.Add(user);
            SaveUsers(users);
        }

        //check if username is available
        public static User? GetUserByUsername(string username)
        {
            return LoadUsers().FirstOrDefault(u => u.Username == username);
        }
    }
}
