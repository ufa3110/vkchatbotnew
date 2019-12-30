using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkBot.Core.Messages;
using VkBot.Core.Structures;
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
                    var payload = new Payload().Deserialize(request.Payload);
                    var message =  con.History.FirstOrDefault(_ => payload.Values.Any(p => Int16.Parse(p) == _.Id ));
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
