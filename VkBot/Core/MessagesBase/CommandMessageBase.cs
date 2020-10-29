using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkBot.Core.Messages;

namespace VkBot.Core.MessagesBase
{
    public class CommandMessageBase : ICommandMessage
    {
        /// <summary>
        /// Версия - меняется руками, для отслеживания применения изменений
        /// </summary>
        public Int32 Version { get; set; }

        /// <summary>
        /// Id запроса <see cref="Request"/>
        /// </summary>
        public Guid MessageId { get; set; }

    }
}
