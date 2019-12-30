using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VkBot.Core.Structures
{
    [Serializable]
    public class ButtonActions
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }
        
        [JsonProperty("open_link")]
        public string OpenLink { get; set; }


    }
}
