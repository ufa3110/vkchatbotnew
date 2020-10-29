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
            var keyboardButtons = new KeyboardButtonsFull();
            
            keyboardButtons.Add(GetInlineVoteKeyboardButtons());

            return new MessageKeyboard()
            {
                Inline = true,
                Buttons = keyboardButtons,
            };

        }

        private List<MessageKeyboardButton> GetInlineVoteKeyboardButtons()
        {
            var buttons = new List<MessageKeyboardButton>();

            buttons.Add(GetImportantButton());
            buttons.Add(GetNotImportantButton());

            return buttons;

        }



        private MessageKeyboardButton GetImportantButton()
        {
            return new MessageKeyboardButton()
            {
                Action = new MessageKeyboardButtonAction()
                {
                    Label = "Важно",
                    Type = KeyboardButtonActionType.Text,
                },
                Color = KeyboardButtonColor.Positive
            };
        }

        private MessageKeyboardButton GetNotImportantButton()
        {
            return new MessageKeyboardButton()
            {
                Action = new MessageKeyboardButtonAction()
                {
                    Label = "Не очень",
                    Type = KeyboardButtonActionType.Text,
                },
                Color = KeyboardButtonColor.Negative
            };
        }


    }
}
