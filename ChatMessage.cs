using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_NW_Airbnb
{
   public class ChatMessage
    {
        public string Message { get; set; }

        public bool IsUser { get; set; } //true = user, false = bot message
    }
}
