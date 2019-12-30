using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkNet.Model.Keyboard;

namespace VkBot.Keyboards
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

        public static MessageKeyboard SetInlineVoteKeyboard (this MessageKeyboard keyboardButtons)
        {
            var voteKeyboard = new InlineVoteKeyboard();
            keyboardButtons = voteKeyboard.GetInlineVoteKeyboard();
            return keyboardButtons;
        }

    }
}
