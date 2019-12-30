using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkBot.Core.Messages;

namespace VkBot.Core.Commands
{
    /// <summary>
    /// Менеджер команд
    /// </summary>
    public interface ICommandsManager
    {

        /// <summary>
        /// Имя пользователя
        /// </summary>
        String UserName { get; set; }

        /// <summary>
        /// ID пользователя
        /// </summary>
        Int64 UserID { get; set; }

        /// <summary>
        /// Список команд, зарегистрированных в менеджере
        /// </summary>
        List<ICommand> CommandsList { get; set; }
    }
}
