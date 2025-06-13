using System.Collections.Generic;

namespace NeptuneEvo.Achievements.Models
{
    public class AchievementData
    {
        public HashSet<int> Completed = new HashSet<int>();
        public Dictionary<int, int> Progress = new Dictionary<int, int>();
    }
}
