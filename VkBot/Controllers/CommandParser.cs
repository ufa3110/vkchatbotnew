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

namespace VkBot.Controllers
{
    public class CommandParser
    {
        public CommandParser(Message msg, IVkApi vkApi)
        {
            _msg = msg;
            manager.Init();
            _vkApi = vkApi;
        }

        public void Parse()
        {
            var payload = new Payload().Deserialize(_msg.Payload ?? "");

            var receivedCommand = manager.CommandsList.FirstOrDefault(_ => _msg.Text.Contains(_.KeyWord))
                ?? manager.CommandsList.FirstOrDefault(_ => payload?.Command.Contains(_.KeyWord) ?? false);

            var response = new Response();
            var request = new Request(_msg, _vkApi);

            if (receivedCommand != null)
            {
                if (receivedCommand.AdminCommand)
                {
                    if (Admins.AdminsList.Contains(_msg.FromId.ToString()))
                    {
                        response = receivedCommand.Execute(request);
                    }
                    else
                    {
                        response.ResponseText = "Недостаточно прав для выполнения данной команды";
                        response.UserId = _msg.FromId;
                        response.ForwardedMessages = request.ForwardedMessages;
                    }
                }
                else
                {
                    response = receivedCommand.Execute(request);
                }

                if (response.ResponseText != null && response.ResponseText != "")
                {
                    _vkApi.Messages.Send(new MessagesSendParams
                    {
                        RandomId = new DateTime().Millisecond,
                        PeerId = _msg.PeerId.Value,
                        Message = response.ResponseText,
                        ForwardMessages = response?.ForwardedMessages,
                        UserId = response?.UserId ?? 0,
                        Keyboard = response.Keyboard,
                    });
                }
            }
        }


        private Message _msg;
        private readonly CommandsManager manager = new CommandsManager();
        private readonly IVkApi _vkApi;
    }
}
