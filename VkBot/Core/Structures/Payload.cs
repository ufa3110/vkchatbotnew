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
        /// Параметры
        /// </summary>
        [JsonProperty("Params")]
        public List<PayloadParam> Params { get; set; } = new List<PayloadParam>();


        public class PayloadParam
        {
            /// <summary>
            /// Имя параметра
            /// </summary>
            public string ParamName { get; set; }
            /// <summary>
            /// Значение
            /// </summary>
            public string Value { get; set; }
        }


        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
        public static Payload Deserialize(string SerializedJSONString)
        {
            try
            {
                var payload = (Payload)JsonConvert.DeserializeObject(SerializedJSONString, typeof(Payload));
                return payload;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

}
