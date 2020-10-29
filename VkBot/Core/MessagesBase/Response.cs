using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkBot.Core.MessagesBase;
using VkNet.Model.Keyboard;

namespace VkBot.Core.Messages
{
    /// <summary>
    /// Ответ выполнения команды
    /// </summary>
    public class Response : CommandMessageBase
    {

        public String ResponseText { get; set; }

        /// <summary>
        /// Ответ на сообщение
        /// </summary>
        public long ReplyMessage { get; set; }

        /// <summary>
        /// массив пересланных сообщений (если есть)
        /// </summary>
        public IEnumerable<long> ForwardedMessages { get; set; }

        /// <summary>
        /// Идентификатор пользователя, которому отправляется сообщение.
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// Отправляемая в ответ клавиатура
        /// </summary>
        public MessageKeyboard Keyboard { get; set; }


        public bool IsSuccess { get; set; }
        /// <summary>
        /// Инициализирует <see cref="MessageId"/> значением <see cref="Guid.Empty"/>
        /// </summary>
        public Response ()
            : this(null)
        {
        }

        /// <summary>
        /// <paramref name="requestId"/> должно быть либо null либо не должно быть <see cref="Guid.Empty"/>
        /// </summary>
        public Response (Guid? requestId)
        {
            if (requestId != null && requestId == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException("requestId", "RequestId shouldn't be Guid.Empty");
            }

            MessageId = requestId ?? Guid.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString ()
        {
            return $"{GetType().FullName}(RequestId={MessageId}, Version={Version})";
        }
    }
}
