using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog;
using Npgsql;
using VkBot.Core.Structures;
using VkBot.DB;

namespace VkBot.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel()
        {
        }

        public string LocalTime { get; private set; } = "";
        public string LastMessage { get; set; } = "";

        public string Exceptions { get; set; } = "";

        public float Counter { get; set; }

        public float Load { get; set; }

        public string Logs { get; set; } = "";


        public void OnGet()
        {
            LocalTime = $"{DateTime.Now}";
            try
            {
                //Logs = $"{{\"KeyWord\":\"123\"}}\n";
                //var payload = new Payload();
                //payload.Param = "parameter";
                //payload.Value.Add("1");
                //payload.Value.Add("2");
                //var serializedPayload = payload.Serialize();
                //Logs += serializedPayload + "\n";
                //var deserialized = payload.Deserialize(serializedPayload);
                //Logs += deserialized.ToString();

                using (var con = new Context())
                {
                    var messagesQ =
                        from history in con.History
                        select history;

                    var messages = messagesQ.ToList();

                    var lastMessage = messages.LastOrDefault();
                    LastMessage = ("id = " + lastMessage.Id + " \n");
                    LastMessage += ("comment = " + lastMessage.Comment);

                    Counter = messages.Count();
                    Load = Counter / 10000 * 100;

                }
            }
            catch (Exception ex)
            {
                Exceptions += ex;
            }
        }


    }
}
