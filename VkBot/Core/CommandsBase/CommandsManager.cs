using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using VkBot.Core.Messages;

namespace VkBot.Core.Commands
{
    public class CommandsManager : ICommandsManager
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// ID пользователя
        /// </summary>
        public long UserID { get ; set; }
        /// <summary>
        /// Список команд, зарегистрированных в менеджере
        /// </summary>
        public List<ICommand> CommandsList { get; set; } = new List<ICommand>();

        private void RegisterNewCommand (ICommand command)
        {
            CommandsList.Add(command);
        }

        public void Init ()
        {
            GetCommands();
        }

        public void GetCommands ()
        {
            var commands =  Assembly.GetExecutingAssembly().GetTypes().Where(t => t.BaseType == typeof(BaseServerCommand)).ToArray();
            foreach (var command in commands)
            {
                object obj = Activator.CreateInstance(command);

                RegisterNewCommand(obj as ICommand);
            }
        }

    }
}
