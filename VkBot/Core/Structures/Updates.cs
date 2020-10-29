using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VkBot.Core.Structures
{
	[Serializable]
	public class Updates
	{
        /// <summary>
        /// Тип события
        /// </summary>
        [JsonProperty("type")]
		public string Type { get; set; }

		/// <summary>
		/// Объект, инициировавший событие
		/// Структура объекта зависит от типа уведомления
		/// </summary>
		[JsonProperty("object")]
		public Vk103Obj Object { get; set; }

		/// <summary>
		/// ID сообщества, в котором произошло событие
		/// </summary>
		[JsonProperty("group_id")]
		public long GroupId { get; set; }
	}

    [Serializable]
    public class Vk103Obj
    {
        [JsonProperty("message")]
        public JObject Message { get; set; }
        [JsonProperty("client_info")]
        public JObject ClientInfo { get; set; }
    }
}