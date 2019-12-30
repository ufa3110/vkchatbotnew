using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkNet.Model.Keyboard;

namespace VkBot.Core.Structures
{
    public static class KeyboardExtenstions
    {
        /// <summary>
        /// Добавляет кнопку "Назад ко всем командам/ в главное меню"
        /// </summary>
        /// <param name="keyboardButtons"></param>
        public static void AddBackButton(this List<List<MessageKeyboardButton>> keyboardButtons)
        {
            var backButton = new List<MessageKeyboardButton>();

            backButton.Add(new KeyboardBackButton());

            keyboardButtons.Add(backButton);
        }
    }
}
