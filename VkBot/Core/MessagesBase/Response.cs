using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkNet.Model.Keyboard;

namespace VkBot.Core.Messages
{
    /// <summary>
    /// Ответ выполнения команды
    /// </summary>
    public class Response : ICommandMessage
    {
        /// <summary>
        /// Версия - меняется руками, для отслеживания применения изменений
        /// </summary>
        public Int32 Version { get; set; }

        /// <summary>
        /// Id запроса <see cref="Request"/>
        /// </summary>
        public Guid RequestId { get; set; }

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
        /// Инициализирует <see cref="RequestId"/> значением <see cref="Guid.Empty"/>
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

            RequestId = requestId ?? Guid.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString ()
        {
            return $"{GetType().FullName}(RequestId={RequestId}, Version={Version})";
        }
    }
}
