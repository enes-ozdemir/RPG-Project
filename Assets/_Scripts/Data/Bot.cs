using System;

namespace _Scripts.Data
{
    [Serializable]
    public class Bot
    {
        public string name;
        public int level;
        public Stats stats;

        public Bot(int level,string name, Stats stats)
        {
            this.level = level;
            this.name = name;
            this.stats = stats;
        }
       
     
    }
}