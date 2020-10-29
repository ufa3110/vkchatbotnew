using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VkBot.DB.Entity
{
    public class Log
    {
        public int Id { get; set; }
        public DateTimeOffset dateTime { get; set; }
        public string log { get; set; }
    }
}
