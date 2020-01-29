using System.Collections.Generic;
using System.Linq;
using VkBot.Core.Messages;
using VkBot.Core.Structures;
using VkBot.DB;
using VkBot.Keyboards;
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
                var historyList =
                    from h in con.History
                    select h;

                var pages = GetMessagePages(historyList.ToList());

                var payload = Deserialize(request.Payload);
                int.TryParse(payload?.Params?.FirstOrDefault()?.ToString() ?? "", out var pageNumber);

                pageNumber = (pageNumber == 0) ? 1 : pageNumber;

                foreach (var msg in pages[pageNumber - 1])
                {
                    if (msg != null)
                    {
                        keyboardButtons.Add(GetMessageButton(msg));
                    }
                }
            }
            keyboardButtons.AddBackButton();

            keyboard.Buttons = keyboardButtons;
            response.Keyboard = keyboard;
        }

        private List<List<DB.Messages>> GetMessagePages(List<DB.Messages> historyList)
        {
            var pages = new List<List<DB.Messages>>();
            var pageNumber = 0;
            var buttonsCounter = 0;
            var page = new List<DB.Messages>();
            foreach(var item in historyList)
            {
                buttonsCounter++;
                page.Add(item);
                if (buttonsCounter >= 10)
                {
                    buttonsCounter = 0;
                    pages.Add(page);
                    page.Clear();
                }
            }
            if (buttonsCounter != 0)
            {
                pages.Add(page);
                page.Clear();
            }

            return pages;
        }

        private List<MessageKeyboardButton> GetMessageButton(DB.Messages msg)
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
                    Label = $"\"{msg.Comment} \" ({new GetConcreteImportant().KeyWord})",
                    Type = KeyboardButtonActionType.Text,
                    Payload = payload.Serialize(),
                },
                Color = KeyboardButtonColor.Default
            });
            return buttons;
        }
    }
}
