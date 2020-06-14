using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchMaps
{
    public class CAchievements
    {
        public string apiname;
        public int achieved;
        public string unclockedtime;
    }
    public class AchievemntsContainer
    {
        public string steamID;
        public string gameName;
        public List<CAchievements> achievements;
        public bool success;
        public AchievemntsContainer()
        {
            List<CAchievements> achievements = new List<CAchievements>();
        }
    }
    public class JsonOutAchs
    {
        public AchievemntsContainer playerstats;
        public JsonOutAchs()
        {
            AchievemntsContainer playerstats = new AchievemntsContainer();
        }
    }
}
