using GTANetworkAPI;
using NeptuneEvo.Handles;
using NeptuneEvo.Accounts;
using NeptuneEvo.Players.Models;
using NeptuneEvo.Players;
using NeptuneEvo.Character.Models;
using NeptuneEvo.Character;
using NeptuneEvo.Chars;
using NeptuneEvo.Core;

using Redage.SDK;
using System;
using System.Linq;
using Localization;
using System.Collections.Generic;

namespace NeptuneEvo.World
{
    class Chat : Script
    {
        private static readonly nLog Log = new nLog("World.Chat");
        [ServerEvent(Event.ChatMessage)]
        public void API_onChatMessage(ExtPlayer player, string message)
        {
            try
            {
                if (!IsSafeForJavaScript(message))
                {
                    Console.WriteLine(player.Name + " " + player.SocialClubName + " " + message);
                    return;
                }
                var sessionData = player.GetSessionData();
                if (sessionData == null) return;
                var characterData = player.GetCharacterData();
                if (characterData == null) return;
                if (characterData.Unmute > 0)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.YouMutedMins, characterData.Unmute / 60), 3000);
                    return;
                }
                if (Main.IHaveDemorgan(player, true) || sessionData.DeathData.InDeath) return;
                message = Main.RainbowExploit(message);
                string testmsg = message.ToLower();
                if (Main.stringDefaultBlock.Any(c => testmsg.Contains(c))) return;
                int[] id = new int[] { player.Value };
                string text = "{name}: " + message;

                //Trigger.PlayAnimation(player, "amb@world_human_hang_out_street@male_a@idle_a", "idle_a", 48);

                var adminConfig = characterData.ConfigData.AdminOption;

                if (characterData.AdminLVL > 0 && adminConfig.RedName)
                    text = "(( Администратор {name}: " + message + " ))";

                foreach (ExtPlayer foreachPlayer in Main.GetPlayersInRadiusOfPosition(player.Position, 10f, UpdateData.GetPlayerDimension(player)))
                {
                    Trigger.ClientEvent(foreachPlayer, "sendRPMessage", "chat", text, id);
                    ChatHeadOverlay.SendOverlayMessage(foreachPlayer, player.Value, ChatHeadOverlay.MessageType.Message, message, false);
                }
                VoiceData phoneMeta = sessionData.VoiceData;
                if (phoneMeta.CallingState == "talk")
                {
                    ExtPlayer target = phoneMeta.Target;
                    var targetCharacterData = target.GetCharacterData();
                    if (targetCharacterData == null) return;
                    int pSim = characterData.Sim;
                    string contactName = (targetCharacterData.Contacts.ContainsKey(pSim)) ? targetCharacterData.Contacts[pSim] : pSim.ToString();
                    Trigger.SendChatMessage(target, $"[В телефоне] {contactName}: {message}");
                    GameLog.AddInfo($"(CChat) player({characterData.UUID}) {message} -> player({targetCharacterData.UUID})");
                }
                else GameLog.AddInfo($"(Chat) player({characterData.UUID}) {message}");
            }
            catch (Exception e)
            {
                Log.Write($"API_onChatMessage Exception: {e.ToString()}");
            }
        }
        public static bool IsSafeForJavaScript(string input)
        {
            input = input.ToLower();
            // Перечисляем ключевые слова, которые могут использоваться для запуска eval() в JavaScript
            string[] evalKeywords = new string[] { "eval", "settimeout", "setinterval", "function", "mp.invoke", "invoke", "command" };

            // Проверяем наличие ключевых слов в строке
            foreach (string keyword in evalKeywords)
            {
                if (input.Contains(keyword))
                {
                    // Если найдено хотя бы одно ключевое слово, возвращаем false
                    return false;
                }
            }

            // Если не найдено ни одного ключевого слова, возвращаем true
            return true;
        }
    }
}