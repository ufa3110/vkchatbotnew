using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkBot.Core.Messages;
using VkNet.Abstractions;

namespace VkBot.Core.Commands
{
    public class HelpCommand : BaseServerCommand
    {

        public HelpCommand()
        {
            KeyWord = "!помощь";
            ShowInKeyboard = true;
            ButtonLabel = "Помощь";
        }

        public override void Calculate (Request request, Response response)
        {
            response.ResponseText = "Чтобы пометить важным сообщение, необходимо ответом на данное сообщение написать /важно (неплохо бы кроме этого ещё и комментарий понятный оставить).  \n" +
                "Комментарий к важному пишется сразу после /важно ;)" +
                "Чтобы вызвать клавиатуру, необходимо написать /клавиатура.\n" +
                "В случае возникновения вопросов - писать в личку. \n" +
                "https://vk.com/id512071793";
        }
    }
}
