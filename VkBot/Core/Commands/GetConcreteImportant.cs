using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkBot.Core.Messages;
using VkBot.Core.Structures;
using VkBot.DB;

namespace VkBot.Core.Commands
{
    public class GetConcreteImportant : BaseServerCommand
    {
        public GetConcreteImportant()
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
                    var messagesList = con.History.ToList();

                    var messageQ = from history in con.History
                                    .AsEnumerable()
                                    join p in payload.Params on history.Id.ToString() equals p.Value
                                    select history;
                    var message = messageQ.SingleOrDefault();

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
