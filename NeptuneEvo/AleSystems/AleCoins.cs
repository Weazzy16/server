using System;
using GTANetworkAPI;
using Redage.SDK;
using NeptuneEvo.Players;
using NeptuneEvo.Character;
using NeptuneEvo.Handles;
using GTANetworkMethods;
using NeptuneEvo.Chars;
using NeptuneEvo.Accounts;

namespace NeptuneEvo.AleSystems
{
    class AleCoins : Script
    {
        private static readonly nLog Log = new nLog(nameof(AleCoins));

        [ServerEvent(Event.ResourceStart)]
        public void onResourceStart()
        {
            try
            {
                Log.Write("Система коинов загружена " + "\u001B[33m" + "[by Ale]" + "\u001B[0m");
            }
            catch (Exception e)
            {
                Log.Write($"onResourceStart Exception: {e.ToString()}");
            }
        }
        public static void AleCoinsStart(ExtPlayer player)
        {
            var characterData = player.GetCharacterData();
            if (characterData == null) return;
            var sessionData = player.GetSessionData();
            if (sessionData == null) return;

            if (characterData.ReceivedCoins == 1)
            {
                Trigger.ClientEvent(player, "everyday.coins.start", "500", "00:00", 1);
                return;
            }

            if (characterData.CoinsTime < 0) characterData.CoinsTime = 18000;
            sessionData.TimersData.CoinsTimer = Timers.Start(1000, () => AleCoins.CoinsTimer(player));


            var timeHour = characterData.CoinsTime / 3600;
            var remainingSeconds = characterData.CoinsTime % 3600;
            var timeMinute = remainingSeconds / 60;
            var timeSecond = remainingSeconds % 60;

            var coins = "500";
            var time = $"0{timeHour}:{timeMinute}";
            var received = 0;
            Trigger.ClientEvent(player, "everyday.coins.start", coins, time, received);
        }
        public static void AleCoinsStop(ExtPlayer player)
        {
            var accountData = player.GetAccountData();
            if (accountData == null) return;
            var characterData = player.GetCharacterData();
            if (characterData == null) return;

            var amount = 500;
            var time = "00:00";
            var received = 1;
            characterData.ReceivedCoins = 1;
            Trigger.ClientEvent(player, "everyday.coins.stop", time, received);
            if (accountData.RedBucks + amount < 0) amount = 0;
            UpdateData.RedBucks(player, amount, msg: "Отыгрыш 5 часов.");
            Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, "Вы получили 500 Maestro Coins, за то что отыграли 5 часов.", 3000);
        }
        public static void CoinsTimer(ExtPlayer player)
        {
            try
            {
                var characterData = player.GetCharacterData();
                if (characterData == null) return;

                var timeHour = characterData.CoinsTime / 3600;
                var remainingSeconds = characterData.CoinsTime % 3600;
                var timeMinute = remainingSeconds / 60;
                var timeSecond = remainingSeconds % 60;

                string time;

                if (characterData.CoinsTime < 600)
                {
                    time = $"0{timeHour}:0{timeMinute}";
                }
                else if (characterData.CoinsTime > 3600 && characterData.CoinsTime < 4200)
                {
                    time = $"0{timeHour}:0{timeMinute}";
                }
                else if (characterData.CoinsTime > 7200 && characterData.CoinsTime < 7800)
                {
                    time = $"0{timeHour}:0{timeMinute}";
                }
                else if (characterData.CoinsTime > 10800 && characterData.CoinsTime < 11400)
                {
                    time = $"0{timeHour}:0{timeMinute}";
                }
                else if (characterData.CoinsTime > 14400 && characterData.CoinsTime < 15000)
                {
                    time = $"0{timeHour}:0{timeMinute}";
                }
                else
                {
                    time = $"0{timeHour}:{timeMinute}";
                }

                Trigger.ClientEvent(player, "client::everyday.coins.update", time);
                if (characterData.CoinsTime-- <= 0) CoinsTimerEnd(player);
            }
            catch (Exception e)
            {
                Log.Write($"CoinsTimer Exception: {e.ToString()}");
            }
        }
        public static void CoinsTimerEnd(ExtPlayer player)
        {
            try
            {
                var sessionData = player.GetSessionData();
                if (sessionData == null) return;
                var characterData = player.GetCharacterData();
                if (characterData == null) return;
                if (sessionData.TimersData.CoinsTimer != null)
                {
                    Timers.Stop(sessionData.TimersData.CoinsTimer);
                    sessionData.TimersData.CoinsTimer = null;
                }
                characterData.CoinsTime = 0;
                AleCoinsStop(player);
            }
            catch (Exception e)
            {
                Log.Write($"CoinsTimerEnd Exception: {e.ToString()}");
            }
        }
        [Command("cisr")]
        public static void CMD_cisr(ExtPlayer player)
        {
            try
            {
                var characterData = player.GetCharacterData();
                if (characterData == null) return;
                if (characterData.AdminLVL != 9)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "У вас нет доступа.", 3000);
                    return;
                }
                foreach (ExtPlayer foreachPlayer in Character.Repository.GetPlayers())
                {
                    if (!foreachPlayer.IsCharacterData()) return;

                    var foreachCharacterData = foreachPlayer.GetCharacterData();
                    if (foreachCharacterData == null) return;

                    foreachCharacterData.ReceivedCoins = 0;
                    foreachCharacterData.CoinsTime = 18000;
                    NAPI.Task.Run(() =>
                    {
                        AleCoinsStart(foreachPlayer);
                    }, 1100);
                }
            }
            catch (Exception e)
            {
                Log.Write($"CMD_cisr Exception: {e.ToString()}");
            }
        }
    }
}
