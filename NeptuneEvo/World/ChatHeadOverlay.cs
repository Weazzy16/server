using GTANetworkAPI;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace NeptuneEvo.World
{
    public static class ChatHeadOverlay
    {
        static readonly List<string> bwords = new List<string>{
                    "https://",
                    "http://",
                    ".ru",
                    ".com"
        };
        private const string SendOverlayMessageEvent = "SRV::CL::FuckingChatMSGEvent";

        public static void SendOverlayMessage(Player player, int senderId, MessageType type, string message, bool result = false)
        {
            foreach (string joke in bwords)
            {
                if (message.Contains(joke)) return;
            }
            var msgInfo = new
            {
                sender = senderId,
                type = (int)type,
                text = message,
                result = result
            };
            
            player.TriggerEvent(SendOverlayMessageEvent, JsonConvert.SerializeObject(msgInfo));
        }
        

        public enum MessageType
        {
            Message = 1,
            Me = 2,
            Do = 3,
            Try = 4
        }
    }
}