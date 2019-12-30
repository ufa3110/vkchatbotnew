using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NLog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using System.Collections.Generic;

namespace VkBot.Pages
{
    public class NLogModel : PageModel
    {
        public NLogModel()
        {
           logger  = LogManager.GetCurrentClassLogger();
        }

        public List<String> UserData { get; set; } = new List<string>();

        public void OnGet()
        {
            using (var fileStream = new FileStream("logs/logfile.txt", FileMode.Open))
            using (StreamReader reader = new StreamReader(fileStream))
            {
                while (!reader.EndOfStream)
                {
                    UserData.Add(reader.ReadLine());
                }
                UserData.Reverse();
            }
        }

        private Logger logger;

    }
}
