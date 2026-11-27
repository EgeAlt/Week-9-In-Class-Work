using ConsoleApp1;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

string[] names = { "Ahmet", "Mehmet", "Ali", "Veli", "Hüseyin" };
int[] namesTaken = { 1, 1, 1, 1, 1 };

Hero[] heroes = new Hero[5];
for (int i = 0; i < heroes.Length; i++)
{
    heroes[i] = new Hero();
    heroes[i].SetupHero(SetName(), Random.Shared.Next(80, 121), Random.Shared.Next(10, 21), 0);
}

bool running = true;
while (running)
{
    Console.WriteLine();
    Console.WriteLine("1) List 2) Duel 3) Heal 4) Champion");
    Console.WriteLine("0) Exit");
    Console.Write("> ");

    if (!int.TryParse(Console.ReadLine(), out int choice))
    {
        Console.WriteLine("Please enter a number.");
        continue;
    }

    switch (choice)
    {
        case 1:
            ListHeroes();
            break;
        case 2:
            Duel();
            break;
        case 3:
            HealAll();
            break;
        case 4:
            ShowChampion();
            break;
        case 0:
            running = false;
            Console.WriteLine("Bye!");
            break;
        default:
            Console.WriteLine("Unknown option.");
            break;
    }
}


string SetName()
{
    int selected;
    do
        selected = Random.Shared.Next(0, names.Length);
    while (namesTaken[selected] == 0);

    namesTaken[selected] = 0;
    return names[selected];
}

int ReadHeroIndex(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < heroes.Length)
            return index;

        Console.WriteLine($"Enter a number from 0 to {heroes.Length - 1}.");
    }
}

void ListHeroes()
{
    for (int i = 0; i < heroes.Length; i++)
    {
        Console.Write($"{i}) ");
        heroes[i].ShowInfo();
    }
}

void Duel()
{
    int a = ReadHeroIndex("Hero 1 (0-4)? ");
    int b = ReadHeroIndex("Hero 2? ");

    if (a == b)
    {
        Console.WriteLine("A hero can't fight themselves.");
        return;
    }

    if (!heroes[a].IsAlive() || !heroes[b].IsAlive())
    {
        Console.WriteLine("Both heroes must be alive. Use 3) Heal first.");
        return;
    }

    while (heroes[a].IsAlive() && heroes[b].IsAlive())
    {
        heroes[a].Attack(heroes[b]);

        if (heroes[b].IsAlive())
            heroes[b].Attack(heroes[a]);
    }

    Hero winner;
    if (heroes[a].IsAlive())
        winner = heroes[a];
    else
        winner = heroes[b];

    winner.wins++;
    Console.WriteLine($"🏆 {winner.name} wins! (wins: {winner.wins})");
}

void HealAll()
{
    for (int i = 0; i < heroes.Length; i++)
    {
        heroes[i].Heal();
    }
    Console.WriteLine("All heroes are back to full HP.");
}

void ShowChampion()
{
    int best = 0;
    for (int i = 1; i < heroes.Length; i++)
    {
        if (heroes[i].wins > heroes[best].wins)
            best = i;
    }

    if (heroes[best].wins == 0)
    {
        Console.WriteLine("Nobody has won a duel yet.");
        return;
    }

    Console.Write("👑 Champion: ");
    heroes[best].ShowInfo();
}