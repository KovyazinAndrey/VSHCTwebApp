using VSHCTwebApp.Components.Models;
using System.Diagnostics;

namespace VSHCTwebApp.Components.Services
{
    public class CommandService
    {
        private readonly List<Command> _commands;

        public CommandService()
        {
            _commands = new List<Command>
            {
                new Command 
                { 
                    Name = "help", 
                    Description = "Показать список доступных команд"
                },
                new Command 
                { 
                    Name = "start", 
                    Description = "Начать новую сессию"
                }
            };
        }

        public List<Command> GetAllCommands()
        {
            Debug.WriteLine($"Получение списка команд. Количество: {_commands.Count}");
            return _commands.ToList();
        }

        public void AddCommand(Command command)
        {
            Debug.WriteLine($"Попытка добавить команду: {command?.Name} - {command?.Description}");

            if (command == null)
                throw new ArgumentNullException(nameof(command), "Команда не может быть null");

            if (string.IsNullOrWhiteSpace(command.Name))
                throw new ArgumentException("Имя команды не может быть пустым");

            if (string.IsNullOrWhiteSpace(command.Description))
                throw new ArgumentException("Описание команды не может быть пустым");

            if (_commands.Any(c => c.Name.Equals(command.Name, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("Команда с таким именем уже существует");

            _commands.Add(command);
            Debug.WriteLine($"Команда добавлена успешно. Текущее количество команд: {_commands.Count}");
        }

        public void DeleteCommand(string name)
        {
            Debug.WriteLine($"Попытка удалить команду: {name}");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Имя команды не может быть пустым");

            var command = _commands.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (command != null)
            {
                _commands.Remove(command);
                Debug.WriteLine($"Команда удалена успешно. Текущее количество команд: {_commands.Count}");
            }
            else
            {
                Debug.WriteLine($"Команда не найдена: {name}");
                throw new InvalidOperationException("Команда не найдена");
            }
        }
    }
} 