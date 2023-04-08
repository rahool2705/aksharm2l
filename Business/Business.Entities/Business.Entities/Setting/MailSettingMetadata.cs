using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Setting
{
    public class MailSettingMetadata
    {
        public string Mail { get; set; } = "sun260880@gmail.com";
        public string DisplayName { get; set; } = "Sandip Patel.";
        public string Password { get; set; } = "xuei xcsg jtjv iczs";
        public string Host { get; set; } = "smtp.gmail.com";
        public int Port { get; set; } = 587;
    }
}
