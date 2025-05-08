using System.Text;
using TaskTracker;

class Program
{
    private static readonly TaskService _taskService = new TaskService();

    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine(
                "Hello, you can use task tracker through 'task-cli' command. And try 'help' command for information.");
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
        help.AppendLine("Comandos disponibles:");
        help.AppendLine("  help                - Muestra esta ayuda.");
        help.AppendLine("  add <descripcion>   - Agrega una nueva tarea con la descripción proporcionada.");
        help.AppendLine("  update <id> <desc>  - Actualiza la descripción de una tarea existente.");
        help.AppendLine("  delete <id>         - Elimina una tarea por su ID.");
        help.AppendLine("  mark-in-progress <id> - Marca una tarea como 'in-progress' por su ID.");
        help.AppendLine("  mark-done <id>      - Marca una tarea como 'done' por su ID.");
        help.AppendLine(
            "  list [status]       - Lista todas las tareas o filtra por estado ('todo', 'in-progress', 'done').");
        Console.WriteLine(help.ToString());
    }

    private static void ListTask(string[] args)
    {
        if (args.Length < 2)
        {
            _taskService.ListAllTasks();
            return;
        }

        string status = args[1].ToLower();
        if (status is "done" or "todo" or "in-progress")
        {
            _taskService.ListTasksByStatus(status);
        }
        else
        {
            Console.WriteLine("Uso: task-cli <done || todo || in-progress>");
        }
    }

    private static void MarkDone(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Uso: task-cli mark-done <taskId>");
            return;
        }

        if (!int.TryParse(args[1], out var taskId))
        {
            Console.WriteLine("Error: ID de tarea invalido.");
            return;
        }

        _taskService.UpdateStatus(taskId, "done");
    }

    private static void MarkInProgress(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Uso: task-cli mark-in-progress <taskId>");
            return;
        }

        if (!int.TryParse(args[1], out var taskId))
        {
            Console.WriteLine("Error: ID de tarea invalido.");
            return;
        }

        _taskService.UpdateStatus(taskId, "in-progress");
    }

    private static void DeleteTask(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Uso: task-cli delete <taskId>");
            return;
        }

        if (!int.TryParse(args[1], out var taskId))
        {
            Console.WriteLine("Error: ID de tarea invalido.");
            return;
        }

        _taskService.DeleteTask(taskId);
    }

    private static void UpdateTask(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Uso: task-cli update <taskId> \"nueva descripcion\"");
            return;
        }

        if (!int.TryParse(args[1], out int taskId))
        {
            Console.WriteLine("Error: ID de tarea inválido.");
            return;
        }

        string newDescription = string.Join(" ", args[2..]);

        _taskService.UpdateTask(taskId, newDescription);
    }

    private static void AddTask(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Uso: task-cli add \"nombre de la tarea\"");
            return;
        }

        string taskDescription = string.Join(" ", args[1..]);

        _taskService.AddTask(taskDescription);
    }
}