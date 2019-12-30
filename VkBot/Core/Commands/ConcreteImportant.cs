using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkBot.Core.Messages;
using VkBot.DB;

namespace VkBot.Core.Commands
{
    public class ConcreteImportant : BaseServerCommand
    {
        public ConcreteImportant()
        {
            KeyWord = "показать";
        }

        public override void Calculate(Request request, Response response)
        {
            if (request.Payload == null || request.Payload == "")
            {
                return;
            }

            using (var con = new Context())
            {
                try
                {
                    var message =  con.History.FirstOrDefault(_ => _.Id == int.Parse(request.Payload ?? "0"));
                    var replyMessage = new List<long>();
                    replyMessage.Add(message.Id);
                    response.ForwardedMessages = replyMessage;
                    
                    response.ResponseText = $"Cообщение(id = {message.Id}, комментарий [{message.Comment}])";
                    
                }
                catch (Exception ex)
                {
                    response.ResponseText = "ъеъ " + ex;
                }

            }
        }
    }
}
