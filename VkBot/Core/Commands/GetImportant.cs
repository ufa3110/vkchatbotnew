using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkBot.Core.Messages;
using VkBot.Core.Structures;
using VkBot.DB;
using VkNet.Abstractions;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.Keyboard;
using static VkBot.Core.Structures.Payload;

namespace VkBot.Core.Commands
{
    public class GetImportant : BaseServerCommand
    {

        public GetImportant()
        {
            KeyWord = "!всё важное";
            ShowInKeyboard = true;
            ButtonLabel = "Список важных сообщений";
        }

        public override void Calculate (Request request, Response response)
        {
            response.ResponseText = $"важные:";

            var keyboard = new MessageKeyboard();
            var keyboardButtons = new List<List<MessageKeyboardButton>>();
            using (var con = new Context())
            {
                foreach (var msg in con.History)
                {
                    var parameters = new List<PayloadParam>();
                    parameters.Add(new PayloadParam()
                    {
                        ParamName = "Id сообщения",
                        Value = msg.Id.ToString(),
                    });
                    var payload = new Payload()
                    {
                        Params = parameters,
                    };
                    var buttons = new List<MessageKeyboardButton>();
                    buttons.Add(new MessageKeyboardButton()
                    {
                        Action = new MessageKeyboardButtonAction() 
                        { 
                            Label = $"\"{msg.Comment} \" ({new ConcreteImportant().KeyWord})",
                            Type = KeyboardButtonActionType.Text,
                            Payload = payload.Serialize(),
                        },
                        Color = KeyboardButtonColor.Default
                    });
                    keyboardButtons.Add(buttons);
                }
                

            }
            keyboardButtons.AddBackButton();

            keyboard.Buttons = keyboardButtons;
            response.Keyboard = keyboard;
        }
    }
}
