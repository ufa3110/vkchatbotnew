using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkBot.Core.Messages;
using VkNet.Abstractions;

namespace VkBot.Core.Commands
{
    public class BaseServerCommand : ICommand
    {
        public Response response { get; set; }

        public string KeyWord { get; set; }

        public bool AdminCommand { get; set; }

        public bool ShowInKeyboard { get; set; }
        public string ButtonLabel { get; set; } = "название кнопки не задано";

        public virtual void Calculate (Request request, Response response)
        {
        }

        public Response Execute(Request request)
        {
            var response = new Response();
            response.UserId = request.UserID;
            response.ForwardedMessages = request.ForwardedMessages;
            Calculate(request, response);
            return response;
        }

    }
}
