using System.Text.Json;

namespace TaskTracker;

public static class TaskService
{
    private const string FilePath = "tasks.json";

    private static List<TaskEntity> LoadTasks()
    {
        if (!File.Exists(FilePath)) return new List<TaskEntity>();
        var json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<List<TaskEntity>>(json) ?? new List<TaskEntity>();
    }

    private static void SaveTasks(List<TaskEntity> tasks)
    {
        var json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, json);
    }

    public static void AddTask(string description)
    {
        var tasks = LoadTasks();
        tasks.Add(new TaskEntity { Description = description });
        SaveTasks(tasks);
        Console.WriteLine($"Task added: {description}");
    }

    public static void UpdateTask(int id, string description)
    {
        var tasks = LoadTasks();
        var task = tasks.FirstOrDefault(e => e.Id == id);
        if (task == null)
        {
            Console.WriteLine($"No task found with ID {id}");
            return;
        }

        task.Description = description;
        task.UpdatedAt = DateTime.Now;
        SaveTasks(tasks);
        Console.WriteLine($"Task {id} updated to: {description}");
    }

    public static void DeleteTask(int id)
    {
        var tasks = LoadTasks();
        var task = tasks.FirstOrDefault(e => e.Id == id);
        if (task == null)
        {
            Console.WriteLine($"No task found with ID {id}");
            return;
        }

        tasks.Remove(task);
        SaveTasks(tasks);
        Console.WriteLine($"Task {id} deleted.");
    }

    public static void UpdateStatus(int id, string status)
    {
        var tasks = LoadTasks();
        var task = tasks.FirstOrDefault(e => e.Id == id);
        if (task == null)
        {
            Console.WriteLine($"No task found with ID {id}");
            return;
        }

        task.Status = status;
        task.UpdatedAt = DateTime.Now;
        SaveTasks(tasks);
        Console.WriteLine($"Task {id} marked as {status}");
    }

    public static void ListAllTasks()
    {
        if (!File.Exists(FilePath))
        {
            Console.WriteLine("No tasks found.");
            return;
        }

        Console.WriteLine(File.ReadAllText(FilePath));
    }

    public static void ListTasksByStatus(string status)
    {
        var tasks = LoadTasks();
        var filteredTasks = tasks.Where(x => x.Status == status);
        Console.WriteLine(JsonSerializer.Serialize(filteredTasks, new JsonSerializerOptions { WriteIndented = true }));
    }
}