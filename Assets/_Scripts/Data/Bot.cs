using System;
using _Scripts.Utils;
using Enca.Extensions;
using Random = Enca.Extensions.Random;

namespace _Scripts.Data
{
    [Serializable]
    public class Bot
    {
        public string name;
        public int level;
        public Stats stats;

        public Bot()
        {
            level = Random.GetRandomNumber(99).ToInt();
            name = RpgUtil.GetRandomRpgName();
            stats = RpgUtil.GetRandomStats(level);
        }
     
    }
}