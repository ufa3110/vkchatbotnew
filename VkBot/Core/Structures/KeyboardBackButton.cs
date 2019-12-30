using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkBot.Core.Commands;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.Keyboard;

namespace VkBot.Core.Structures
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
