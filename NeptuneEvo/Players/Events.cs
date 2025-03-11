using GTANetworkAPI;
using NeptuneEvo.Handles;
using NeptuneEvo.Chars;
using NeptuneEvo.Core;
using NeptuneEvo.Functions;
using NeptuneEvo.Players.Models;
using Newtonsoft.Json;
using Redage.SDK;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NeptuneEvo.Players
{
    class Events
    {
        [ServerEvent(Event.PlayerConnected)]
        public void OnPlayerConnected(Player player)
        {
            NAPI.Task.Run(() =>
            {
                player.TriggerEvent("UpdateInventoryHealth", player.Health);
            }, delayTime: 2000); // Задержка, чтобы CEF успел загрузиться
        }
        [ServerEvent(Event.PlayerDamage)]
        public void OnPlayerDamage(Player player, float healthLoss, float armorLoss)
        {
            int newHealth = Math.Max(0, (int)(player.Health - healthLoss)); // Приведение float к int
            player.Health = newHealth;
            player.TriggerEvent("UpdateInventoryHealth", newHealth);
        }


    }
}
