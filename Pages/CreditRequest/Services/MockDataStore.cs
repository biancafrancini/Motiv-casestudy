using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace MotivWebApp.Pages.CreditRequest.Services
{
    // Represents a mock data store.
    public class MockDataStore
    {
        private Dictionary<string, object> data;
        private string filePath;

        public MockDataStore()
        {
            data = new Dictionary<string, object>();
            string currentDirectory = Directory.GetCurrentDirectory();
            filePath = Path.Combine(currentDirectory, "data.json");
            LoadFromFile(); // Load existing data from file
            SaveToFile();
        }

        // Creates new data and returns the generated key.
        public string CreateData()
        {
            string key = GenerateRandomKey();
            data[key] = new Dictionary<string, object>();
            SaveToFile();
            return key;
        }

        // Saves the specified value with the given key in the data store.
        public void SaveData(string obj_key, string key, object value)
        {
            if (data.ContainsKey(obj_key) && data[obj_key] is Dictionary<string, object> innerDict)
            {
                innerDict[key] = value;
                SaveToFile();
            }
        }

        // Gets the data associated with the specified key.
        public Dictionary<string, object> GetData(string obj_key)
        {   
            if (data.ContainsKey(obj_key))
            {
                var jsonElement = (JsonElement)data[obj_key];
                var json = jsonElement.GetRawText();
                var user_data = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
                return user_data;
            }
            return null;
        }

        private void SaveToFile()
        {
            string json = JsonSerializer.Serialize(data);
            File.WriteAllText(filePath, json);
        }

        private void LoadFromFile()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                data = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
            }
            else
            {
                Console.WriteLine("Data file not found: " + filePath); // Debug statement
            }
        }

        private string GenerateRandomKey()
        {
            Random random = new Random();
            string key = random.Next().ToString();
            return key;
        }
    }
}
