using System;
using System.Collections.Generic;
using System.Text;

namespace session_repeat_1.Entities
{
    public class Session
    {
        public string Date { get; set; }
        public List<SessionItem> SessionItems { get; set; }
    }
}
