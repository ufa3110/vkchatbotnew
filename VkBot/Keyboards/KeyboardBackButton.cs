using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkBot.Core.Commands;
using VkBot.Core.Structures;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.Keyboard;

namespace VkBot.Keyboards
{
    public class KeyboardBackButton : MessageKeyboardButton
    {
        public KeyboardBackButton()
        {
            var newKeyBoard = new GetKeyboard();
            Action = new MessageKeyboardButtonAction()
            {
                Label = newKeyBoard.ButtonLabel,
                Type = KeyboardButtonActionType.Text,
                Payload = new Payload()
                {
                    Command = newKeyBoard.KeyWord
                }.Serialize(),
            };
            Color = KeyboardButtonColor.Default;
        }

    }
}
