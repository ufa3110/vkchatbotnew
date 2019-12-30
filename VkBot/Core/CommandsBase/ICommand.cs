using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkBot.Core.Messages;
using VkNet.Abstractions;

namespace VkBot.Core.Commands
{
    public interface ICommand
    {
        /// <summary>
        /// Имя команды
        /// </summary>
        String KeyWord { get; }

        /// <summary>
        /// Является ли команда админской
        /// </summary>
        bool AdminCommand { get; set; }

        /// <summary>
        /// Показывать ли команду в клавиатуре
        /// </summary>
        bool ShowInKeyboard { get; set; }

        /// <summary>
        /// Название кнопки в клавиатуре
        /// </summary>
        string ButtonLabel { get; set; }

        /// <summary>
        /// Выполнение команды
        /// </summary>
        /// <returns></returns>
        void Calculate (Request request, Response response);
        Response Execute (Request request);
    }
}
