# TaskTrackerCLI

TaskTrackerCLI is a command-line interface (CLI) application for managing tasks. It allows users to add, update, delete, and list tasks, as well as update their statuses. Tasks are stored in a JSON file for persistence.

## Features

- Add new tasks with descriptions.
- Update task descriptions.
- Delete tasks by ID.
- Mark tasks as "in-progress" or "done."
- List all tasks or filter tasks by status (`todo`, `in-progress`, `done`).

## Requirements

- .NET 6.0 or later
- A terminal or command prompt

## Installation

1. Clone the repository:
-  ```bash
   git clone https://github.com/JohanBarrera71/TaskTrackerCLI.git
   cd TaskTrackerCLI
    ```
2. Build the project:
- ```bash
   dotnet build
   ```
3. Run the application:
 -  ```bash
    dotnet run <command>
    ```

## Usage
### Commands

- `help`: Displays a list of available commands and their descriptions.
- ```bash 
    dotnet run help
    ```
<br>

- `add <description>`: Add a new task with the given description.
- ```bash 
    dotnet run add "Task description"
    ```
<br>

- `update <id> <description>`: Update the task with the given ID and new description.
- ```bash 
    dotnet run update 1 "Updated task description"
    ```
  
<br>

- `delete <id>`: Delete the task with the given ID.
- ```bash 
    dotnet run delete 1
    ```
  
<br>

- `mark-in-progress <id>`: Mark the task with the given ID as "in-progress."
- ```bash 
    dotnet run mark-in-progress 1
    ```
  
<br>

- `mark-done <id>`: Mark the task with the given ID as "done."
- ```bash 
    dotnet run mark-done 1
    ```
  
<br>

- `list`: List all tasks with their IDs, descriptions, and statuses.
- ```bash 
    dotnet run list
    ```
  
<br>

- `list <status>`: List tasks filtered by status (`todo`, `in-progress`, `done`).
- ```bash 
    dotnet run list in-progress
    ```