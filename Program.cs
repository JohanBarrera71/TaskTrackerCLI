using System.Text;

namespace TaskTracker;

internal static class Program
{
    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine(
                "Hello, you can use task tracker through 'task-cli' command. And try 'help' command for more information.");
            return;
        }

        // Command dictionary
        var commands = new Dictionary<string, Action<string[]>>
        {
            { "help", Help },
            { "add", AddTask },
            { "update", UpdateTask },
            { "delete", DeleteTask },
            { "mark-in-progress", MarkInProgress },
            { "mark-done", MarkDone },
            { "list", ListTask },
        };

        string command = args[0].ToLower();

        // Check if the command exists in the dictionary
        if (commands.ContainsKey(command))
        {
            commands[command](args);
        }
        else
        {
            Console.WriteLine("Command not recognized. Try 'help' for more information.");
        }
    }

    private static void Help(string[] args)
    {
        var help = new StringBuilder();
        help.AppendLine("Available commands:");
        help.AppendLine("  help                - Shows this help.");
        help.AppendLine("  add <descripcion>   - Adds a new task with the given description.");
        help.AppendLine("  update <id> <desc>  - Updates description of an existing task. ");
        help.AppendLine("  delete <id>         - Deletes a task with the given id.");
        help.AppendLine("  mark-in-progress <id> - Marks a task as 'in-progress' by its ID.");
        help.AppendLine("  mark-done <id>      - Marks a task as 'done' by its ID.");
        help.AppendLine(
            "  list [status]       - Lists all tasks or filter by status ('todo', 'in-progress', 'done').");
        Console.WriteLine(help.ToString());
    }

    private static void ListTask(string[] args)
    {
        if (args.Length < 2)
        {
            TaskService.ListAllTasks();
            return;
        }

        string status = args[1].ToLower();
        if (status is "done" or "todo" or "in-progress")
        {
            TaskService.ListTasksByStatus(status);
        }
        else
        {
            Console.WriteLine("Use: task-cli <done || todo || in-progress>");
        }
    }

    private static void MarkDone(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Use: task-cli mark-done <taskId>");
            return;
        }

        if (!int.TryParse(args[1], out var taskId))
        {
            Console.WriteLine("Error: Invalid task ID.");
            return;
        }

        TaskService.UpdateStatus(taskId, "done");
    }

    private static void MarkInProgress(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Use: task-cli mark-in-progress <taskId>");
            return;
        }

        if (!int.TryParse(args[1], out var taskId))
        {
            Console.WriteLine("Error: Invalid task ID.");
            return;
        }

        TaskService.UpdateStatus(taskId, "in-progress");
    }

    private static void DeleteTask(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Use: task-cli delete <taskId>");
            return;
        }

        if (!int.TryParse(args[1], out var taskId))
        {
            Console.WriteLine("Error: Invalid task ID.");
            return;
        }

        TaskService.DeleteTask(taskId);
    }

    private static void UpdateTask(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Use: task-cli update <taskId> \"new description\"");
            return;
        }

        if (!int.TryParse(args[1], out int taskId))
        {
            Console.WriteLine("Error: Invalid task ID.");
            return;
        }

        string newDescription = string.Join(" ", args[2..]);

        TaskService.UpdateTask(taskId, newDescription);
    }

    private static void AddTask(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Use: task-cli add \"task description\"");
            return;
        }

        string taskDescription = string.Join(" ", args[1..]);

        TaskService.AddTask(taskDescription);
    }
}