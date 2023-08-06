namespace _Scripts.Data
{
    public class Stats
    {
        public int Health;
        public int Mana;
        public int AttackDamage;
        public float AttackSpeed;
        public int Armor;
        public int MagicResist;
        public int MovementSpeed;
        public int DodgeCooldown;

        public Stats(int health, int mana, int attackDamage, float attackSpeed, int armor, int magicResist,
            int movementSpeed, int dodgeCooldown)
        {
            Health = health;
            Mana = mana;
            AttackDamage = attackDamage;
            AttackSpeed = attackSpeed;
            Armor = armor;
            MagicResist = magicResist;
            MovementSpeed = movementSpeed;
            DodgeCooldown = dodgeCooldown;

            //  Log.Info("Stats are: health: " + health + " mana: " + mana + " attackDamage: " + attackDamage + " attackSpeed: " + attackSpeed + " armor: " + armor + " magicResist: " + magicResist + " movementSpeed: " + movementSpeed);
        }
    }
}