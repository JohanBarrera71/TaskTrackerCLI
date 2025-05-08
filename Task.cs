using System.Text.Json;

namespace TaskTracker;

public class TaskEntity
{
    public int Id { get; set; } = Guid.NewGuid().GetHashCode();
    public string Description { get; set; } = "New Task";
    public string Status { get; set; } = "todo"; 
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}