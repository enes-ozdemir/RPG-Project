using UnityEngine;

namespace _Scripts.Utils
{
    public class RpgUtil
    {
        private static string[] RpgNames = new string[]
        {
            "Enca","Caen","Numunik","selmanbaba67","SirAiakos","Sauron666","ilkerix","Requashe","Albastı","Phantaso"
        };

        public static string GetRandomRpgName()
        {
            int index = Random.Range(0, RpgNames.Length);
            return RpgNames[index];
        }

        public static Stats GetRandomStats(int level)
        {
            var rand = new System.Random();
            
            int health = rand.Next(50, 100) * level;
            int mana = rand.Next(20, 50) * level;
            int attackDamage = rand.Next(10, 20) * level;
            float attackSpeed = (float) (rand.Next(1, 3) + rand.NextDouble()); 
            int armor = rand.Next(5, 10) * level;
            int magicResist = rand.Next(5, 10) * level;
            int movementSpeed = rand.Next(5, 10) * level;

            return new Stats(health, mana, attackDamage, attackSpeed, armor, magicResist, movementSpeed);
        }
    }
}