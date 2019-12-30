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
    public class ServiceTest : BaseServerCommand
    {

        public ServiceTest ()
        {
            KeyWord = "тест";
        }

        public override void Calculate (Request request, Response response)
        {
            response.ResponseText = $"Сервис тест";
            response.ReplyMessage = request.ReplyMessage;
        }
    }
}
