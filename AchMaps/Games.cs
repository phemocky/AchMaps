using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace AchMaps
{
    public class Games
    {
        public string nazwa;
        public string ID;
        public Dictionary<string, int> podNazwa = new Dictionary<string, int>();
        public List<CategoryOfAchievement> categories;
        public string adres;

        public Games()
        {
        }
        public Games(string path)
        {
            List<CategoryOfAchievement> _categories = new List<CategoryOfAchievement>();
            categories = _categories;
            adres = path;
            var path2 = Path.Combine(Directory.GetCurrentDirectory() + @"\" + path);
            //if (!Directory.Exists(path2))
            //{
            //    Directory.CreateDirectory(path2);
            //    File.WriteAllText()
            //}
            DirectoryInfo d = new DirectoryInfo(path2);
            foreach (var file in d.GetFiles())
            {
                string text = File.ReadAllText(file.FullName);
                CategoryOfAchievement CoA = JsonConvert.DeserializeObject<CategoryOfAchievement>(text);
                string[] id = file.Name.Split('_');
                CoA.id = Convert.ToInt32(id[0]);
                categories.Add(CoA);

            }

        }
    }
}
