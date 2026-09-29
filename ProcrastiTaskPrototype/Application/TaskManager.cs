using System;
using System.Collections.Generic;
using System.Linq;
using ProcrastiTaskPrototype.Domain;
using ProcrastiTaskPrototype.Persistence;

namespace ProcrastiTaskPrototype.Application
{
    public class TaskManager
    {
        private readonly List<Task> tasks;
        private readonly ITaskRepository repository;

        //interface load:
        public TaskManager(ITaskRepository repository)
        {
            this.repository = repository;
            tasks = repository.Load();
        }

        public List<Task> GetTasks()
        {
            return tasks;
        }

        public Task CreateTask(
            string title,
            string description,
            TaskStatus status,
            TaskPriority priority)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Task title cannot be empty.");
            }

            Task task = new Task
            {
                TaskTitle = title,
                TaskDescription = description,
                TaskStatus = status,
                TaskPriority = priority,
                DevContext = new ProcrastiTaskPrototype.Domain.DevContext()
            };

            tasks.Add(task);

            return task;
        }

        public void UpdateTask(Task task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            if (string.IsNullOrWhiteSpace(task.TaskTitle))
            {
                throw new ArgumentException("Task title cannot be empty.");
            }
        }

        public void DeleteTask(Task task)
        {
            if (task == null)
            {
                return;
            }

            tasks.Remove(task);
        }

        public void ChangeStatus(Task task, TaskStatus status)
        {
            if (task == null)
            {
                return;
            }

            task.TaskStatus = status;
        }

        public void SetPriority(Task task, TaskPriority priority)
        {
            if (task == null)
            {
                return;
            }

            task.TaskPriority = priority;
        }

        //if time is enough -> tasksearch
        public List<Task> SearchTasks(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return new List<Task>(tasks);
            }

            return tasks
                .Where(t =>
                    t.TaskTitle.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    t.TaskDescription.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
        }

        //if time is enough search->filter
        public List<Task> FilterTasks(
            TaskStatus? status = null,
            TaskPriority? priority = null)
        {
            IEnumerable<Task> result = tasks;

            if (status.HasValue)
            {
                result = result.Where(t => t.TaskStatus == status.Value);
            }

            if (priority.HasValue)
            {
                result = result.Where(t => t.TaskPriority == priority.Value);
            }

            return result.ToList();
        }
        public void SaveTasks()
        {
            repository.Save(tasks);
        }

        public void LoadTasks()
        {
            tasks.Clear();
            tasks.AddRange(repository.Load());
        }
    }
}
