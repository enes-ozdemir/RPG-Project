using Enca.Debug;

namespace _Scripts
{
    public class Stats
    {
        public int health;
        public int mana;
        public int attackDamage;
        public float attackSpeed;
        public int armor;
        public int magicResist;
        public int movementSpeed;

        public Stats(int health, int mana, int attackDamage, float attackSpeed, int armor, int magicResist, int movementSpeed)
        {
            this.health = health;
            this.mana = mana;
            this.attackDamage = attackDamage;
            this.attackSpeed = attackSpeed;
            this.armor = armor;
            this.magicResist = magicResist;
            this.movementSpeed = movementSpeed;
            
            Log.Info("Stats are: health: " + health + " mana: " + mana + " attackDamage: " + attackDamage + " attackSpeed: " + attackSpeed + " armor: " + armor + " magicResist: " + magicResist + " movementSpeed: " + movementSpeed);
            
        }
    }
}