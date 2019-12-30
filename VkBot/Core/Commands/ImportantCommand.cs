using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkBot.Core.Messages;
using VkBot.Core.Structures;
using VkBot.DB;
using VkBot.Keyboards;
using VkNet.Abstractions;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.Keyboard;
using VkNet.Model.RequestParams;

namespace VkBot.Core.Commands
{
    public class ImportantCommand : BaseServerCommand
    {
        public ImportantCommand()
        {
            KeyWord = "/важно";
        }

        public override void Calculate(Request request, Response response)
        {
            using (var con = new Context())
            using (var transaction = con.Database.BeginTransaction())
            {
                if (request.ReplyMessage != 0)
                {
                    try
                    {
                        var newMessage = new DB.Messages()
                        {
                            Id = (int)request.ReplyMessage,
                            Comment = request.MessageText.Replace(KeyWord,""),
                        };
                        if (!con.History.Any(_ => _.Id == newMessage.Id))
                        {
                            con.History.Add(newMessage);
                            con.SaveChanges();
                        }
                        else
                        {
                            con.Update(newMessage);
                        }

                    }
                    catch (Exception ex)
                    {
                        response.ResponseText = "ъеъ " + ex;
                    }
                    
                    transaction.Commit();

                    var reply = new List<long>();
                    reply.Add(request.Message.Id ?? 0);

                    request.vkApi.Messages.Send(new MessagesSendParams
                    {
                        RandomId = new DateTime().Millisecond,
                        PeerId = request.UserID,
                        Message = "Важно?",
                        ForwardMessages = reply,
                        UserId = response?.UserId ?? 0,
                        Keyboard = response.Keyboard.SetInlineVoteKeyboard(),
                    });

                }
                else
                {
                    response.ResponseText = "нужно послать ответ на сообщение)";
                }
            }
        }

    }
}

