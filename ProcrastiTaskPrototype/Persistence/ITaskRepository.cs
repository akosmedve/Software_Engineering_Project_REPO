using System.Collections.Generic;
using ProcrastiTaskPrototype.Domain;

namespace ProcrastiTaskPrototype.Persistence
{
    public interface ITaskRepository
    {
        void Save(List<Task> tasks);

        List<Task> Load();
    }
}
