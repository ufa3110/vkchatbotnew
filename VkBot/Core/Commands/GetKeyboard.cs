using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VkBot.Core.Messages;
using VkBot.DB;
using VkNet.Abstractions;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.Keyboard;

namespace VkBot.Core.Commands
{
    public class GetKeyboard : BaseServerCommand
    {

        public GetKeyboard ()
        {
            KeyWord = "/клавиатура";
            ShowInKeyboard = true;
            ButtonLabel = "Список команд";
        }

        public override void Calculate (Request request, Response response)
        {
            response.ResponseText = $"команды";
            var manager = new CommandsManager();
            manager.Init();
            var keyboard = new MessageKeyboard();
            var keyboardButtons = new List<List<MessageKeyboardButton>>();

            var commands = manager.CommandsList.Where(_ => _.ShowInKeyboard);
            foreach (var command in commands)
            {
                var buttons = new List<MessageKeyboardButton>();
                buttons.Add(new MessageKeyboardButton()
                {
                    Action = new MessageKeyboardButtonAction() {
                        Label = command.ButtonLabel,
                        Type = KeyboardButtonActionType.Text,
                        Payload = $"{{\"KeyWord\":\"{command.KeyWord}\"}}",
                    },
                    Color = (command.AdminCommand) ? KeyboardButtonColor.Negative :KeyboardButtonColor.Default
                });
                keyboardButtons.Add(buttons);
            }

            keyboard.Buttons = keyboardButtons;
            response.Keyboard = keyboard;
        }
    }
}
