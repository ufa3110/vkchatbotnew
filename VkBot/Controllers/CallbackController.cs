using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using VkNet.Abstractions;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Utils;
using Newtonsoft.Json;
using VkBot.Core.Commands;
using System.Linq;
using VkBot.Core.Messages;
using System.Diagnostics;
using VkBot.Core.Users;
using VkBot.Core.Structures;

namespace VkBot.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CallbackController : ControllerBase
    {
        /// <summary>
        /// Конфигурация приложения
        /// </summary>
        private readonly IConfiguration _configuration;

        private readonly IVkApi _vkApi;

        private readonly CommandsManager manager = new CommandsManager();

        public List<String> Messages { get; set; } = new List<string>();


        public CallbackController(IVkApi vkApi, IConfiguration configuration)
        {
            _configuration = configuration;
            _vkApi = vkApi;
            manager.Init();
        }


        [HttpPost]
        public IActionResult Callback([FromBody] Updates updates)
        {
            // Тип события
            switch (updates.Type)
            {
                // Ключ-подтверждение
                case "confirmation":
                    {
                        Debug.WriteLine("confirmation recieved");
                        return Ok(_configuration["Config:Confirmation"]);
                    }

                // Новое сообщение
                case "message_new":
                    {
                        Debug.WriteLine("message_new recieved");
                        JsonSerializer serializer = new JsonSerializer();
                        // Десериализация
                        var msg = Message.FromJson(new VkResponse(updates.Object.Message));
                        Trace.WriteLine($"message_new deserialized, msg.text = {msg.Text}");
                        try
                        {
                            var payload = new Payload().Deserialize(msg.Payload ?? "");

                            var receivedCommand = manager.CommandsList.FirstOrDefault(_ => msg.Text.Contains(_.KeyWord)) 
                                ?? manager.CommandsList.FirstOrDefault(_ => payload?.Command.Contains(_.KeyWord) ?? false);

                            var response = new Response();
                            var request = new Request()
                            {
                                UserName = $"@{msg.FromId ?? 0}",
                                ForwardedMessages = msg.ForwardedMessages?.Select(_ => _.Id ?? 0).AsEnumerable<long>(),
                                ReplyMessage = msg.ReplyMessage?.Id ?? 0,
                                MessageText = msg.Text,
                                Payload = msg.Payload,
                                vkApi = _vkApi,
                                UserID = msg.FromId ?? 0,
                            };

                            if (receivedCommand != null)
                            {
                                if (receivedCommand.AdminCommand)
                                {
                                    if (Admins.AdminsList.Contains(msg.FromId.ToString()))
                                    {
                                        response = receivedCommand.Execute(request);
                                    }
                                    else
                                    {
                                        response.ResponseText = "Недостаточно прав для выполнения данной команды";
                                        response.UserId = msg.FromId;
                                        response.ForwardedMessages = request.ForwardedMessages;
                                    }
                                }
                                else
                                {
                                    response = receivedCommand.Execute(request);
                                }

                                if (response.ResponseText != string.Empty)
                                {
                                    _vkApi.Messages.Send(new MessagesSendParams
                                    {
                                        RandomId = new DateTime().Millisecond,
                                        PeerId = msg.PeerId.Value,
                                        Message = response.ResponseText,
                                        ForwardMessages = response?.ForwardedMessages,
                                        UserId = response?.UserId ?? 0,
                                        Keyboard = response.Keyboard,
                                    });
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            _vkApi.Messages.Send(new MessagesSendParams
                            {
                                RandomId = new DateTime().Millisecond,
                                PeerId = msg.PeerId.Value,
                                Message = ex +$"{msg}",
                            });
                        }
                        
                    }
                    break;
            }
            
            return Ok("ok");
        }
    }
}
