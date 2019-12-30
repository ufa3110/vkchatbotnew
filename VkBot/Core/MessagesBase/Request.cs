using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkNet.Abstractions;
using VkNet.Model;

namespace VkBot.Core.Messages
{
    /// <summary>
    /// Запрос на выполнение команды
    /// </summary>
    public class Request : ICommandMessage
    {
        /// <summary>
        /// Версия - меняется руками, для отслеживания применения изменений
        /// </summary>
        public Int32 Version { get; set; }

        /// <summary>
        /// Id сообщения, используется для трассировки обработки сообщения
        /// </summary>
        public Guid MessageId { get; set; }

        /// <summary>
        /// Имя пользователя который послал запрос
        /// </summary>
        public String UserName { get; set; }

        /// <summary>
        /// ID пользователя
        /// </summary>
        public Int64 UserID { get; set; }

        /// <summary>
        /// массив пересланных сообщений (если есть)
        /// </summary>
        public IEnumerable<long> ForwardedMessages { get; set; }

        /// <summary>
        /// Ответ на сообщение
        /// </summary>
        public long ReplyMessage { get; set; }

        /// <summary>
        /// Само сообщение
        /// </summary>
        public string MessageText { get; set; }

        /// <summary>
        /// Полезная нагрузка
        /// </summary>
        public string Payload { get; set; }

        /// <summary>
        /// Api Vk
        /// </summary>
        public IVkApi vkApi { get; set; }

        /// <summary>
        /// Само сообщение
        /// </summary>
        public Message Message { get; set; }

        /// <summary>
        /// Инициализирует <see cref="MessageId"/> значением <see cref="Guid.Empty"/>
        /// </summary>
        public Request ()
            : this(null)
        {
        }

        public Request (Message msg, IVkApi _vkApi)
        {
            UserName = $"@{msg.FromId ?? 0}";
            ForwardedMessages = msg.ForwardedMessages?.Select(_ => _.Id ?? 0).AsEnumerable<long>();
            ReplyMessage = msg.ReplyMessage?.Id ?? 0;
            MessageText = msg.Text;
            Payload = msg.Payload;
            vkApi = _vkApi;
            UserID = msg.FromId ?? 0;
            Message = msg;

            if (msg.ForwardedMessages.Any() && msg.ReplyMessage?.Id != 0 )
            {
                var reply = new List<long>();
                reply.Add(msg.ReplyMessage.Id ?? 0);
                ForwardedMessages = reply.AsEnumerable();
            }

        }


        /// <summary>
        /// <paramref name="messageId"/> должно быть либо null либо не должно быть <see cref="Guid.Empty"/>
        /// </summary>
        /// <param name="messageId"></param>
        public Request (Guid? messageId)
        {
            if (messageId != null && messageId == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException("messageId", "MessageId shouldn't be Guid.Empty");
            }

            MessageId = messageId ?? Guid.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString ()
        {
            return $"{GetType().FullName}(MessageId={MessageId}, Version={Version})";
        }
    }
}
