using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VkBot.Core.Messages
{
    /// <summary>
    /// Сообщение к серверу (<see cref="Request"/>, <see cref="Response"/>)
    /// </summary>
    public interface ICommandMessage
    {
        /// <summary>
        /// Версия
        /// </summary>
        Int32 Version { get; }
    }
}
