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
using VkBot.Controllers.Messages;

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



        public CallbackController(IVkApi vkApi, IConfiguration configuration)
        {
            _configuration = configuration;
            _vkApi = vkApi;
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
                        JsonSerializer serializer = new JsonSerializer();
                        // Десериализация
                        var msg = Message.FromJson(new VkResponse(updates.Object.Message));
                        try
                        {
                            var parser = new NewMessageParser(msg, _vkApi);
                            parser.Parse();
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
