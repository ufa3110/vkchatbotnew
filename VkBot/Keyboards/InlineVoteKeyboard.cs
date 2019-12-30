using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.Keyboard;

namespace VkBot.Keyboards
{
    public class InlineVoteKeyboard
    {
        public MessageKeyboard GetInlineVoteKeyboard()
        {
            var keyboardButtons = new List<List<MessageKeyboardButton>>();
            var buttons = new List<MessageKeyboardButton>();
            buttons.Add(new MessageKeyboardButton()
            {
                Action = new MessageKeyboardButtonAction()
                {
                    Label = "Важно",
                    Type = KeyboardButtonActionType.Text,
                },
                Color = KeyboardButtonColor.Positive
            });
            buttons.Add(new MessageKeyboardButton()
            {
                Action = new MessageKeyboardButtonAction()
                {
                    Label = "Не очень",
                    Type = KeyboardButtonActionType.Text,
                },
                Color = KeyboardButtonColor.Negative
            });
            keyboardButtons.Add(buttons);

            return new MessageKeyboard()
            {
                Inline = true,
                Buttons = keyboardButtons,
            };

        }

    }
}
