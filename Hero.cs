using System;

namespace ConsoleApp1
{
    public class Hero
    {
        public string name;
        public int hp;
        public int power;
        public int wins;
        private int maxHp;

        public void SetupHero(string valName, int valHp, int valPwr, int valWins)
        {
            name = valName;
            hp = valHp;
            maxHp = valHp;
            power = valPwr;
            wins = valWins;
        }

        public void ShowInfo()
        {
            string status = "";
            if (!IsAlive())
                status = " (down)";

            Console.WriteLine($"{name} | ❤ {hp}/{maxHp} | Power: {power} | Wins: {wins}{status}");
        }

        public void TakeDamage(int amount)
        {
            hp -= amount;
            if (hp < 0)
                hp = 0;
        }

        public void Heal()
        {
            hp = maxHp;
        }

        public bool IsAlive()
        {
            return hp > 0;
        }

        public void Attack(Hero target)
        {
            int damage = power + Random.Shared.Next(-5, 6);
            if (damage < 1)
                damage = 1;

            target.TakeDamage(damage);
            Console.WriteLine($"{name} hits {target.name}: -{damage} ({target.name} ❤ {target.hp})");
        }
    }
}