using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkBot.Core.Structures
{
    [Serializable]
    public class Payload
    {
        /// <summary>
        /// Ссылка на команду
        /// </summary>
        [JsonProperty("Command")]
        public string Command { get; set; }

        /// <summary>
        /// Параметр
        /// </summary>
        [JsonProperty("Param")]
        public string Param { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        [JsonProperty("Value")]
        public List<string> Values { get; set; } = new List<string>();

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
        public Payload Deserialize(string SerializedJSONString)
        {
            var stuff = (Payload)JsonConvert.DeserializeObject(SerializedJSONString, typeof(Payload));
            return stuff;
        }
    }

}
