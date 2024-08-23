using system;
using system.windows.forms;


class Ship
{
    public string Name { get; set; }
    public int Cannons { get; set; }
    public int Crew { get; set; }
    public int MaxCrew { get; set; }
    public int MaxCannons { get; set; }
    public int Bank { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Items { get; set; }
    public int Chance { get; set; }
    public int EnemyCrew { get; set; }

    private static readonly Random random = new Random();
    
    public Ship(string name, int cannons, int crew, int bank, int maxhealth, int inventoryItems, int chance, int maxcrew, int maxcannons, int enemycrew)
    {
        Name = name;
        Cannons = cannons;
        Crew = crew;
        MaxCrew = maxcrew;
        MaxCannons = maxcannons;
        Bank = bank;
        Health = 100;
        MaxHealth = maxhealth;
        Items = inventoryItems;
        Chance = random.Next(1, 6);
        EnemyCrew = random.Next(10, 51);
    }

    public void Attack(Ship target)
    {
        int chance = random.Next(1, 11);
        if (chance >= 2)
        {
            int damage = random.Next(10, 21) + Cannons;
            target.Health -= damage;
            target.Health = Math.Max(target.Health, 0);
        }
        else
        {
            Console.WriteLine("You missed");
        }
    }

    public void Assault(Ship target)
    {
        int chance = random.Next(1, 11);
        if (chance >= 2)
        {
            int damage = random.Next(10, 21) + Cannons;
            target.Health -= damage;
            target.Health = Math.Max(target.Health, 0);
        }
        else
        {
            Console.WriteLine("The enemy ship missed");
        }
    }


    public void BoardChance(Ship target)
    {
        Chance = random.Next(1,2);
        if (target.Health < 31)
        {
            if (Crew > EnemyCrew)
            {
                if (Chance >= 2)
                {
                    Items += 1;
                    Crew = Math.Max(Crew - 5, 0);
                    Console.Clear();
                    Console.WriteLine("You won the board");
                    Console.WriteLine($"You now have {Items} captured ships");
                    Console.WriteLine($"Crew {Crew}/{MaxCrew}");
                    target.Health = 0;
                }
                else
                {
                    Crew = 25;
                    Health = 1;
                    Console.Clear();
                    Console.WriteLine("You lost the board");
                    Console.WriteLine($"Crew: {Crew}/{MaxCrew}");
                    Console.WriteLine($"Health: {Health}/{MaxHealth}");
                    return;
                }
            }
        }
    }

    public void Repair()
    {
        int repairAmount = random.Next(10, 21);
        Health += repairAmount;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        Console.WriteLine($"Health: {Health}/{MaxHealth}");
    }

    public void Treasure()
    {
        int treasureAmount = random.Next(5, 21);
        Bank += treasureAmount;
        Console.Clear();
        Console.WriteLine($"{treasureAmount} gold found");
        Console.WriteLine($"Gold: {Bank}");
    }

    public void Stolen()
    {
        int stolenAmount = random.Next(0, 251);
        Bank += stolenAmount;
        Console.WriteLine($"You sank the enemy ship and stole {stolenAmount} gold");
    }

    public void Inventory()
    {
        Items = 0;
    }

    public void EnemyRepair()
    {
        int chance = random.Next(1, 11);
        if (chance > 7)
        {
            int repairAmount = random.Next(10, 21);
            Health += repairAmount;
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
            Console.WriteLine($"The enemy ship repaired itself to {Health}/{MaxHealth}");
        }
    }
}

class PirateGame
{
    private Ship playerShip;
    private Ship enemyShip;
    private static ManualResetEvent resetEvent = new ManualResetEvent(true);

    public PirateGame()
    {
        playerShip = new Ship("Player Ship", 5, 50, 0, 100, 0, 0, 50, 10, 0);
        enemyShip = new Ship("Enemy Ship", new Random().Next(1, 6), new Random().Next(10, 51), 0, new Random().Next(75, 151), 0, new Random().Next(1, 6), 50, 10, new Random().Next(10, 51));
    }
    private void ResumeGame()
{
    resetEvent.Set();
}

public void Start() // initiallization options for the game
{
    {
        Console.WriteLine("Press any key to continue. Press 'Esc' to exit.");

        while (true)
        {
            // Read a single key press
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true); // intercept: true prevents the key from being displayed

            // Check the key that was pressed
            if (keyInfo.Key == ConsoleKey.Escape)
            {
                Environment.Exit();
            }
            else
            {// Perform action based on the key press
            StartGame();
            break;
            }
        }
    }
}

public void StartGame() // Game Application itself
{
    Console.Clear();
    while (true)
    {
        string choice = Console.ReadLine();
        HandleChoice(choice);

        if (playerShip.Health < 1)
        {
            Console.Clear();
            Console.WriteLine("Your ship was destroyed. Game over!");
            Environment.Exit(0);
        }

        if (enemyShip.Health <= 0)
        {
            Console.clear();
            playerShip.Stolen();
            Console.WriteLine($"Health {playerShip.Health}/{playerShip.MaxHealth}");
            enemyShip.Health = new Random().Next(75, 151);
            enemyShip.MaxHealth = enemyShip.Health;
        }
    }
}

    private async void DisplayMenu()
    {
        Console.WriteLine(" \n              |    |    | \n             )_)  )_)  )_)   \n            )___))___))___)\\ \n           )____)____)_____)\\ \n         _____|____|____|____\\____\n---------\\                  /---------\n^^^^^ ^^^^^^^^^^^^^^^^^^^^^\n^^^^      ^^^^     ^^^    ^^\n      ^^^^      ^^^");
        Console.WriteLine(" \n \n \n ");
        Console.WriteLine("Options:\n1. Attack enemy ship\n2. Repair your ship\n3. Search for treasure\n4. Shop\n5. Quit\nEnter Choice: ");
    }

    private void HandleChoice(string choice)
    {
        switch (choice)
        {
            case "1":

                Console.clear();
                AttackSequence();
                break;
            case "2":
                console.clear();
                RepairShip();
                break;
            case "3":
                Console.Clear();
                SearchTreasure();
                break;
            case "4":
                console.clear();
                Shop();
                break;
            case "5":
                consoler.clear();
                Quit();
                break;
            default:
                console.clear();
                Console.WriteLine("Invalid choice. Try again.");
                break;
        }
    }

  private void AttackSequence()
{
    Console.Clear();
    enemyShip.Health = new Random().Next(75, 151);  // Reset enemy ship health
    enemyShip.MaxHealth = enemyShip.Health;
    Console.WriteLine($"Enemy ship health: {enemyShip.Health}");

    Console.WriteLine($"Your current health: {playerShip.Health}");
    Console.WriteLine("Do you want to fight this ship?");
    Console.WriteLine("1. Fight ship");
    Console.WriteLine("2. No");
    string choice = Console.ReadLine();

    if (choice == "1")
    {
        Console.Clear();
        while (enemyShip.Health > 0 && playerShip.Health > 1)
        {
            if (enemyShip.Health > 31)
            {
            Console.WriteLine($"Enemy ship health: {enemyShip.Health}/{enemyShip.MaxHealth}");
            Console.WriteLine($"Your current health: {playerShip.Health}/{playerShip.MaxHealth}");
            Console.WriteLine("Options:");
            Console.WriteLine("1. Shoot cannons");
            Console.WriteLine("2. Repair your ship");
            Console.WriteLine("3. Leave fight");
            }
            if (enemyShip.Health < 30 && enemyShip.Health > 0)
            { 
            Console.WriteLine($"Enemy ship health: {enemyShip.Health}/{enemyShip.MaxHealth}");
            Console.WriteLine($"Your current health: {playerShip.Health}/{playerShip.MaxHealth}");
            Console.WriteLine("Options:");
            Console.WriteLine("1. Shoot cannons");
            Console.WriteLine("2. Repair your ship");
            Console.WriteLine("3. Leave fight");
            Console.WriteLine("4. Board Ship");
            }

            choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    console.clear();
                    if (enemyShip.Health > 0)
                    {
                        playerShip.Attack(enemyShip);
                        if (enemyShip.Health <= 0)
                        {
                            Console.WriteLine("Enemy ship has been defeated!");
                            playerShip.Stolen();
                            Console.WriteLine($"Health {playerShip.Health}/{playerShip.MaxHealth}");
                            return;
                        }
                        else
                        {
                            enemyShip.Assault(playerShip);
                        }
                    }
                    break;

                case "2":
                    console.clear();
                    playerShip.Repair();
                    enemyShip.EnemyRepair();
                    break;

                case "3":
                    console.clear();
                    Console.WriteLine("You have left the fight");
                    return;

                case "4":
                    if (enemyShip.Health < 30 && enemyShip.Health > 0)
                    {
                        console.clear();
                        playerShip.BoardChance(enemyShip);
                        break;
                    }
                    else
                    {
                        console.clear();
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                    }

            }
        }
    }
    else if (choice == "2")
    {
        Console.Clear();
        Console.WriteLine("You didn't take the fight");
    }
}

private void RepairShip()
    {
        console.clear();
        playerShip.Repair();
    }

private void SearchTreasure()
    {
        console.clear();
        playerShip.Treasure();
    }

private void Shop()
    {
        console.clear();
        while (true)
        {
            Console.WriteLine($"Health: {playerShip.Health}/{playerShip.MaxHealth}, Cannons: {playerShip.Cannons}/{playerShip.MaxCannons}, Crew: {playerShip.Crew}/{playerShip.MaxCrew}, Gold: {playerShip.Bank}, Captured ships: {playerShip.Items}");
            Console.WriteLine("");
            Console.WriteLine("     Welcome to the Shop    ");
            Console.WriteLine("");
            Console.WriteLine("Options:");
            Console.WriteLine("1. Buy cannons (1000 coins)");
            Console.WriteLine("2. Buy crew members (100 coins)");
            Console.WriteLine("3. Upgrade ship (5000 coins)");
            Console.WriteLine("4. Sell captured ship (1000 coins)");
            Console.WriteLine("5. Leave shop");

            string choice = Console.ReadLine();
            HandleShopChoice(choice);
            if (choice == "5") break;
        }
    }

private void HandleShopChoice(string choice)
    {
        switch (choice)
        {
            case "1":
                console.clear();
                if (playerShip.Bank < 1000)
                {
                    Console.WriteLine("You don't have enough coins");
                }
                else if (playerShip.Cannons < playerShip.MaxCannons)
                {
                    playerShip.Bank -= 1000;
                    playerShip.Cannons += 1;
                    Console.WriteLine($"You now have {playerShip.Cannons} cannons");
                }
                else
                {
                    Console.WriteLine("You have reached your max cannons amount (upgrade ship to increase)");
                }
                break;
            case "2":
                console.clear();
                if (playerShip.Bank < 100)
                {
                    Console.WriteLine("You don't have enough coins");
                }
                else if (playerShip.Crew < playerShip.MaxCrew)
                {
                    playerShip.Bank -= 100;
                    playerShip.Crew += 10;
                    if (playerShip.Crew > playerShip.MaxCrew)
                    {
                        playerShip.Crew = playerShip.MaxCrew;
                    }
                    Console.WriteLine($"You now have {playerShip.Crew} crew members");
                }
                else
                {
                    Console.WriteLine("You have reached your max crew amount (upgrade ship to increase)");
                }
                break;
            case "3":
                console.clear();
                if (playerShip.Bank < 5000)
                {
                    Console.WriteLine("You don't have enough coins");
                }
                else
                {
                    playerShip.Bank -= 5000;
                    playerShip.MaxHealth += 50;
                    playerShip.MaxCrew += 50;
                    playerShip.MaxCannons += 5;
                    Console.WriteLine($"Upgraded ship: Health to {playerShip.MaxHealth}, Crew to {playerShip.MaxCrew}, Cannons to {playerShip.MaxCannons}");
                }
                break;
            case "4":
                console.clear();
                if (playerShip.Items < 1)
                {
                    Console.WriteLine("You don't have any captured ships");
                }
                else
                {
                    playerShip.Items -= 1;
                    playerShip.Bank += 1000;
                    Console.WriteLine("You have sold a captured ship");
                }
                break;
            case "5":
                console.clear();
                break;
            default:
                console.clear();
                Console.WriteLine("Invalid choice. Try again.");
                break;
        }
    }

private void Quit()
    {
        console.clear();
        while (true)
        {
            Console.WriteLine("Are you sure you want to quit?");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");

            string choice = Console.ReadLine();
            if (choice == "1")
            {
                console.clear();
                Console.WriteLine("Thanks for playing!");
                return;
            }
            else if (choice == "2")
            {
                console.clear();
                break;
            }
            else
            {
                console.clear();
                Console.WriteLine("Invalid choice. Try again.");
            }
        }
    }

static void Main(string[] args)
    {
        PirateGame game = new PirateGame();
        game.Start();
    }
}