using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchMaps
{
    public class CMaps
    {
        public List<string> id;
        public string name;        
        public TypeMap type;
        public CMaps()
        {
            List<string> _id = new List<string>();
            id = _id;
        }
    }
    public enum TypeMap
    {
        Normal = 0,
        Long = 1,
        Objective = 2,
        Survival = 3,
        Bain = 4,
        Classic = 5,
        Event = 6,
        Hector = 7,
        Jimmy = 8,
        Locky = 9,
        Buther = 10,
        Continental = 11,
        Dentist = 12,
        Elephant = 13,
        Vlad = 14,
        Holdout = 15,
    }
    public class CategoryOfAchievement
    {
        public List<string> upName;
        public List<CMaps> maps;
        public string name;
        public int id;
        public CategoryOfAchievement()
        {
            List<CMaps> _maps = new List<CMaps>();
            maps = _maps;
            List<string> _Upname = new List<string>();
            upName = _Upname;
        }
    }
}
