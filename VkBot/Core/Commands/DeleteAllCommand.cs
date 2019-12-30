using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkBot.Core.Messages;
using VkBot.DB;

namespace VkBot.Core.Commands
{
    public class DeleteAllCommand : BaseServerCommand
    {
        public DeleteAllCommand ()
        {
            KeyWord = "!почистить БД";
            AdminCommand = true;
            ShowInKeyboard = true;
            ButtonLabel = "Удалить все записи из БД";
        }
        public override void Calculate(Request request, Response response)
        {
            using (var con = new Context())
            using (var transaction = con.Database.BeginTransaction())
            {
                    try
                    {
                        foreach (var msg in con.History)
                        {
                            con.History.Remove(msg);
                        }
                        con.SaveChanges();
                        response.ResponseText = "удолил";
                    }
                    catch (Exception ex)
                    {
                        response.ResponseText = "ъеъ " + ex;
                    }

                    transaction.Commit();
            }
        }
    }
}
