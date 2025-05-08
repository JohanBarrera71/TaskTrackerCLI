using System.Text.Json;

namespace TaskTracker;

public class TaskService
{
    private readonly String _filePath = "tasks.json";
    public Task AddTask(string description)
    {
        var task = new TaskEntity
        {
            Description = description
        };
        
        List<TaskEntity> tasks;

        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);
            tasks = JsonSerializer.Deserialize<List<TaskEntity>>(json) ?? new List<TaskEntity>();
        }
        else
        {
            tasks = new List<TaskEntity>();
        }
        
        tasks.Add(task);

        var updateJson = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, updateJson);
        Console.WriteLine($"Tarea agregada: {task.Description}");
        return Task.CompletedTask;
    }

    public Task UpdateTask(int id, string description)
    {
        if (!File.Exists(_filePath))
        {
            Console.WriteLine("No hay tareas para actualizar");
            return Task.CompletedTask;
        }

        var json = File.ReadAllText(_filePath);
        var tasks = JsonSerializer.Deserialize<List<TaskEntity>>(json) ?? new List<TaskEntity>();

        var task = tasks.FirstOrDefault(e => e.Id == id);
        if(task is null) 
        {
            Console.WriteLine($"No se encontró la tarea con ID {id}");
            return Task.CompletedTask;
        }
        
        task.Description = description;
        task.UpdatedAt = DateTime.Now;
        

        var updateJson = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, updateJson);

        Console.WriteLine($"Tarea {task.Id} actualizada a: {task.Description}");
        return Task.CompletedTask;
    }

    public Task DeleteTask(int id)
    {
        if (!File.Exists(_filePath))
        {
            Console.WriteLine("No hay tareas para eliminar.");
            return Task.CompletedTask;
        }

        var json = File.ReadAllText(_filePath);
        var tasks = JsonSerializer.Deserialize<List<TaskEntity>>(json) ?? [];

        var task = tasks.FirstOrDefault(e => e.Id == id);
        if (task is null)
        {
            Console.WriteLine($"No se encontro la tarea con ID {id}");
            return Task.CompletedTask;
        }

        tasks.Remove(task);
        
        var updateJson = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, updateJson);
        
        
        Console.WriteLine($"Tarea {task.Id} eliminada.");
        return Task.CompletedTask;
    }

    public Task UpdateStatus(int id, string status)
    {
        if (!File.Exists(_filePath))
        {
            Console.WriteLine("No hay tareas para actualizar.");
            return Task.CompletedTask;
        }

        var json = File.ReadAllText(_filePath);
        var tasks = JsonSerializer.Deserialize<List<TaskEntity>>(json) ?? [];

        var task = tasks.FirstOrDefault(x => x.Id == id);
        if (task is null)
        {
            Console.WriteLine($"No se encontro la taarea con ID {id}");
            return Task.CompletedTask;
        }

        task.Status = status;
        task.UpdatedAt = DateTime.Now;
        
        var updateJson = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, updateJson);
        Console.WriteLine($"Tarea {task.Id} marcada como  {task.Status}");
        return Task.CompletedTask;
    }

    public Task ListAllTasks()
    {
        if (!File.Exists(_filePath))
        {
            Console.WriteLine("No hay tareas para mostrar.");
            return Task.CompletedTask;
        }
        
        // List all tasks
        var json = File.ReadAllText(_filePath);
        Console.WriteLine(json);
        return Task.CompletedTask;
    }

    public Task ListTasksByStatus(string status)
    {
        if (!File.Exists(_filePath))
        {
            Console.WriteLine("No hay tareas para mostrar.");
            return Task.CompletedTask;
        }
        var json = File.ReadAllText(_filePath);
        var tasks = JsonSerializer.Deserialize<List<TaskEntity>>(json) ?? [];

        var query = tasks.Where(x => x.Status == status);
        Console.WriteLine(JsonSerializer.Serialize(query, new JsonSerializerOptions { WriteIndented = true }));
        return Task.CompletedTask;
    }
}