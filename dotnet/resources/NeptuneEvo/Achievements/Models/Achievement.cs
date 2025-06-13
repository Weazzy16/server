using System.Collections.Generic;
using NeptuneEvo.Chars.Models;


namespace NeptuneEvo.Achievements.Models
{
    public class Achievement
    {
        // Уникальный ID достижения (тип)
        public int Id { get; set; }

        // Название и описание
        public string Name { get; set; }
        public string Description { get; set; }

        // Сколько нужно выполнить
        public int RequiredCount { get; set; }

        // Возможные награды
        public List<ItemId> RewardPool { get; set; }

        // --- ↓ Прогресс игрока ↓ ---
        public int CharId { get; set; }                  // ID персонажа
        public int AchievementsId { get; set; }          // ID ачивки (дублирует Id, можно объединить)
        public sbyte AchievementsCount { get; set; }     // Прогресс (например, 7/10)

        public Achievement() { }

        public Achievement(int id, string name, string description, int requiredCount, List<ItemId> rewardPool)
        {
            Id = id;
            Name = name;
            Description = description;
            RequiredCount = requiredCount;
            RewardPool = rewardPool;

            AchievementsId = id; // можно сопоставить
        }
    }
}
