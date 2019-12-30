using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkBot.Core.Messages;
using VkBot.DB;
using VkNet.Abstractions;

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
                            response.ResponseText = "запомнил";
                        }
                        else
                        {
                            con.Update(newMessage);
                            response.ResponseText = "обновил";
                        }

                    }
                    catch (Exception ex)
                    {
                        response.ResponseText = "ъеъ " + ex;
                    }
                    
                    transaction.Commit();
                }
                else
                {
                    response.ResponseText = "нужно послать ответ на сообщение)";
                }
            }
        }
    }
}

