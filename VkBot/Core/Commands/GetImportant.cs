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
            response.ResponseText = $"важные сообщения:";

            var keyboard = new MessageKeyboard();
            var keyboardButtons = new List<List<MessageKeyboardButton>>();
            using (var con = new Context())
            {
                var historyList =
                    from h in con.History
                    select h;

                var pages = GetMessagePages(historyList.ToList());

                var payload = Deserialize(request.Payload);
                int.TryParse(payload?.Params?.FirstOrDefault()?.Value?.ToString() ?? "", out var pageNumber);

                response.ResponseText += 
                    $"\n Страница: {pageNumber} из {pages.Count}," +
                    $"\n Кол-во записей всего: {historyList.Count()}";

                foreach (var msg in pages[pageNumber])
                {
                    if (msg != null)
                    {
                        keyboardButtons.Add(GetMessageButton(msg));
                    }
                }
                keyboardButtons.AddBackButton();
                keyboardButtons.Add(GetPagesNavigationButtons(pageNumber,pages.Count));
            }
            
            keyboard.Buttons = keyboardButtons;
            response.Keyboard = keyboard;
        }

        private List<MessageKeyboardButton> GetPagesNavigationButtons(int pageNumber, int pagesCount)
        {
            var keyboardButtons = new List<MessageKeyboardButton>();

            var parametersPrev = new List<PayloadParam>();
            parametersPrev.Add(new PayloadParam()
            {
                ParamName = "Номер страницы",
                Value = (pageNumber - 1).ToString(),
            });

            var parametersNext = new List<PayloadParam>();
            parametersNext.Add(new PayloadParam()
            {
                ParamName = "Номер страницы",
                Value = (pageNumber + 1).ToString(),
            });
            if (pageNumber > 0)
            {
                keyboardButtons.Add(new MessageKeyboardButton()
                {
                    Action = new MessageKeyboardButtonAction()
                    {
                        Label = "Предыдущая страница",
                        Type = KeyboardButtonActionType.Text,
                        Payload = new Payload()
                        {
                            Command = KeyWord,
                            Params = parametersPrev,
                        }.Serialize(),
                    },
                    Color = KeyboardButtonColor.Default
                });
            }
            if (pageNumber < pagesCount)
            {
                keyboardButtons.Add(new MessageKeyboardButton()
                {
                    Action = new MessageKeyboardButtonAction()
                    {
                        Label = "Следующая страница",
                        Type = KeyboardButtonActionType.Text,
                        Payload = new Payload()
                        {
                            Command = KeyWord,
                            Params = parametersNext,
                        }.Serialize(),
                    },
                    Color = KeyboardButtonColor.Default
                });
            }

            return keyboardButtons;
        }

        private List<List<DB.Messages>> GetMessagePages(List<DB.Messages> historyList)
        {
            var pages = new List<List<DB.Messages>>();
            var buttonsCounter = 0;
            var page = new List<DB.Messages>();
            foreach(var item in historyList)
            {
                buttonsCounter++;
                page.Add(item);
                if (buttonsCounter >= 5)
                {
                    buttonsCounter = 0;
                    pages.Add(page);
                    page = new List<DB.Messages>();
                }
            }
            if (buttonsCounter != 0)
            {
                pages.Add(page);
                page = new List<DB.Messages>();
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
