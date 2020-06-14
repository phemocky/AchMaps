using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchMaps
{
    public class Key
    {
        private string key;
        public Key()
        {
            key = GetKeyFromFile(); 
        }
        public string GetKey()
        {
            return key;
        }
        public string GetKeyFromFile()
        {            
            var path = Path.Combine(Directory.GetCurrentDirectory() + @"\" + "key.txt");
            if (File.Exists(path))
            {
                string key = File.ReadAllText(path);
                return key;
            }
            else
            {
                return "";
            }
            
        }
    }
}
