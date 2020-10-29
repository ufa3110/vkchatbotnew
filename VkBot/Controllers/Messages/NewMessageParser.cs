using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkBot.Core.Commands;
using VkBot.Core.Messages;
using VkBot.Core.Structures;
using VkBot.Core.Users;
using VkNet.Abstractions;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace VkBot.Controllers.Messages
{
    public static class NewMessageParser
    {
        public static void Parse(Message msg, IVkApi vkApi)
        {
            var manager = new CommandsManager();

            manager.Init();

            var payload = Payload.Deserialize(msg.Payload ?? "");

            var receivedCommand = manager.CommandsList.FirstOrDefault(_ => msg.Text.Contains(_.KeyWord))
                ?? manager.CommandsList.FirstOrDefault(_ => payload?.Command.Contains(_.KeyWord) ?? false);

            var response = new Response();
            var request = new Request(msg, vkApi);

            if (receivedCommand != null)
            {
                if (receivedCommand.AdminCommand)
                {
                    if (Admins.AdminsList.Contains(msg.FromId.ToString()))
                    {
                        response = receivedCommand.Execute(request);
                    }
                    else
                    {
                        response.ResponseText = "Недостаточно прав для выполнения данной команды";
                        response.UserId = msg.FromId;
                        response.ForwardedMessages = request.ForwardedMessages;
                    }
                }
                else
                {
                    response = receivedCommand.Execute(request);
                }

                if (response.ResponseText != null && response.ResponseText != "")
                {
                    vkApi.Messages.Send(new MessagesSendParams
                    {
                        RandomId = new DateTime().Millisecond,
                        PeerId = msg.PeerId.Value,
                        Message = response.ResponseText,
                        ForwardMessages = response?.ForwardedMessages,
                        UserId = response?.UserId ?? 0,
                        Keyboard = response.Keyboard,
                    });
                }
            }
        }

    }
}
