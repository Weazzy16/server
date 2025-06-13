using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using GTANetworkAPI;
using NeptuneEvo.Character.Models;
using NeptuneEvo.Chars.Models;
using NeptuneEvo.Players;
using NeptuneEvo.Handles;
using Redage.SDK;
using LinqToDB;
using NeptuneEvo.Character;

namespace NeptuneEvo.Achievements
{
    class AchievementsMain : Script
    {
        private static readonly nLog Log = new nLog("Achievements.Main");

        /// <summary>
        /// Клиент вызывает при инициализации: загрузить список ачивок для HUD
        /// </summary>
        /// <param name="player"></param>
        [RemoteEvent("server.achievements.init")]
        public static async Task InitAchievements(ExtPlayer player)
        {
            try
            {
                if (!player.IsCharacterData()) return;
                var characterData = player.GetCharacterData();
                // Загрузить из БД
                var list = await LoadAchievements(player);
                characterData.Achievements = list;
                // Отправить клиенту JSON
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(list);
                Trigger.ClientEvent(player, "client.achievements.init", json);
            }
            catch (Exception e)
            {
                Log.Write($"InitAchievements Exception: {e}");
            }
        }

        /// <summary>
        /// Клиент вызывает при получении награды за ачивку
        /// </summary>
        [RemoteEvent("server.achievements.claim")]
        public static async Task ClaimAchievement(ExtPlayer player, int achievementId)
        {
            try
            {
                if (!player.IsCharacterData()) return;
                var characterData = player.GetCharacterData();
                var ach = characterData.Achievements.FirstOrDefault(a => a.AutoId == achievementId);
                if (ach == null || ach.Completed)
                    return;
                // Отметить выполненной
                ach.Completed = true;
                ach.LastUpdated = DateTime.Now;

                // Сохранить в БД
                await SaveSingleAchievement(player, ach);
                // Выдать награду
                Chars.Repository.AddNewItem(player, $"char_{characterData.UUID}", "inventory", ach.ItemId, ach.Count);

                // Сообщить клиенту, что эта ачивка выполнена
                Trigger.ClientEvent(player, "client.achievements.update", achievementId, true);
            }
            catch (Exception e)
            {
                Log.Write($"ClaimAchievement Exception: {e}");
            }
        }

        /// <summary>
        /// Загрузить все ачивки из БД
        /// </summary>
        private static async Task<List<AchievementData>> LoadAchievements(ExtPlayer player)
        {
            var result = new List<AchievementData>();
            try
            {
                var uuid = player.GetCharacterData().UUID;
                await using var db = new ServerBD("MainDB");
                var dbList = await db.AchievementsChars
                    .Where(a => a.CharId == uuid)
                    .ToListAsync();

                foreach (var r in dbList)
                {
                    // Попробуем спарсить ваше string r.ItemId в enum ItemId
                    ItemId parsed;
                    if (!Enum.TryParse<NeptuneEvo.Chars.Models.ItemId>(r.ItemId, out parsed))
                        parsed = NeptuneEvo.Chars.Models.ItemId.Debug; // дефолтное значение

                    result.Add(new AchievementData
                    {
                        AutoId = r.AchAutoId,
                        AchievementId = r.AchId,
                        Completed = r.Completed,
                        LastUpdated = r.LastUpdated,
                        ItemId = parsed,
                        Count = r.Count
                    });
                }
            }
            catch (Exception e)
            {
                Log.Write($"LoadAchievements Exception: {e}");
            }
            return result;
        }

        /// <summary>
        /// Сохранить одну ачивку (обновить статус)
        /// </summary>
        private static async Task SaveSingleAchievement(ExtPlayer player, AchievementData ach)
        {
            try
            {
                var uuid = player.GetCharacterData().UUID;
                await using var db = new ServerBD("MainDB");
                await db.AchievementsChars
                    .Where(a => a.AchAutoId == ach.AutoId && a.CharId == uuid)
                    .Set(a => a.Completed, ach.Completed)
                    .Set(a => a.LastUpdated, ach.LastUpdated)
                    .UpdateAsync();
            }
            catch (Exception e)
            {
                Log.Write($"SaveSingleAchievement Exception: {e}");
            }
        }

        /// <summary>
        /// Сохраняет все изменённые ачивки (если необходимо вызвать из другого места)
        /// </summary>
        public static async Task SaveAllAchievements(ExtPlayer player)
        {
            try
            {
                var list = player.GetCharacterData().Achievements;
                foreach (var ach in list)
                {
                    await SaveSingleAchievement(player, ach);
                }
            }
            catch (Exception e)
            {
                Log.Write($"SaveAllAchievements Exception: {e}");
            }
        }
    }

    /// <summary>
    /// Модель данных ачивки у игрока
    /// </summary>
    public class AchievementData
    {
        [Newtonsoft.Json.JsonIgnore]
        public int AutoId { get; set; }
        public int AchievementId { get; set; }
        public bool Completed { get; set; }
        public DateTime LastUpdated { get; set; }
        public ItemId ItemId { get; set; }   // ← enum
        public int Count { get; set; }
    }
}
