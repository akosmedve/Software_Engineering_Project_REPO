using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ProcrastiTaskPrototype.Domain;

namespace ProcrastiTaskPrototype.Persistence
{
    public class JsonTaskRepository : ITaskRepository
    {
        private readonly string filePath;

        public JsonTaskRepository(string filePath)
        {
            this.filePath = filePath;
        }

        public void Save(List<Task> tasks)
        {
            string json = JsonSerializer.Serialize(
                tasks,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText(filePath, json);
        }

        public List<Task> Load()
        {
            if (!File.Exists(filePath))
            {
                return new List<Task>();
            }

            string json = File.ReadAllText(filePath);

            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<Task>();
            }

            try
            {
                List<Task> tasks =
                    JsonSerializer.Deserialize<List<Task>>(json);

                return tasks ?? new List<Task>();
            }
            catch (JsonException)
            {
                return new List<Task>();
            }
        }
    }
}