namespace PirateGame
{
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
    public void Attack(Ship target) // player ship attack function
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
            Console.WriteLine("-------------\n You missed\n-------------");
        }
    }
    public void Assault(Ship target) // enemy ship attack fucntion
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
            Console.WriteLine("-------------------------\n The enemy ship missed\n-------------------------");
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
                    Console.WriteLine($"----------------------------------------------\nYou won the board\n----------------------------------------------\nYou now have {Items} captured ships\n----------------------------------------------\nCrew {Crew}/{MaxCrew}\n----------------------------------------------");
                    target.Health = 0;
                }
                else
                {
                    Crew = 25;
                    Health = 1;
                    Console.Clear();
                    Console.WriteLine($"----------------------------------------------\nYou lost the board\n----------------------------------------------\nCrew: {Crew}/{MaxCrew}\n----------------------------------------------\nHealth: {Health}/{MaxHealth}\n----------------------------------------------");
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
        Console.WriteLine($"-------------------------------------------------------\nHealth: {Health}/{MaxHealth}\n-------------------------------------------------------");
    }
    public void Treasure()
    {
        int treasureAmount = random.Next(5, 21);
        Bank += treasureAmount;
        Console.Clear();
        Console.WriteLine($"----------------------------------------------\n{treasureAmount} gold found\n----------------------------------------------\nGold: {Bank}\n----------------------------------------------");
    }
    public void Stolen() // defines how much gold is given after a ship has been sunk
    {
        int stolenAmount = random.Next(0, 250);
        Console.WriteLine($"----------------------------------------------\n Enemy ship has been defeated! \n----------------------------------------------\n You sank the enemy ship and stole {stolenAmount} gold \n----------------------------------------------\n Health {Health}/{MaxHealth}\n----------------------------------------------");
        Bank += stolenAmount;
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
            Console.WriteLine($"------------------------------------------------\nThe enemy ship repaired itself to {Health}/{MaxHealth}\n------------------------------------------------");
        }
    }
}
class PirateGame
{
    bool menuanimationcancel = false;
    private Ship playerShip;
    private Ship enemyShip;
    public PirateGame()
    {
        playerShip = new Ship("Player Ship", 5, 50, 0, 100, 0, 0, 50, 10, 0); // 5 = cannons, 50 = crew, 100 = maxhealth, 0 = inventory.
        enemyShip = new Ship("Enemy Ship", new Random().Next(1, 6), new Random().Next(10, 51), 0, new Random().Next(75, 151), 0, new Random().Next(1, 6), 50, 10, new Random().Next(10, 51));
    }
public async void StartGameAnimation() // animation at the start of the game
{
    while (!menuanimationcancel) // token system to toggle start game animation
    {
        // menu animation frames here
        Console.Clear();
        Console.WriteLine("                  |> \n             |    |    | \n            )_)  )_)  )_)   \n           )___))___))___)\\ \n          )____)____)_____)\\ \n        _____|____|____|____\\____ \n--------\\                  /---------\n^^^^^ ^^^^^^^  ^^^^^^^^^     ^^^^ \n^^^^      ^^^^     ^^    ^^\n      ^^^      ^^^ \n \n \n \n    Press any key to continue\n       Press 'esc' to exit");
        await Task.Delay(1500);  // Delay for animation effect
        if (menuanimationcancel) // check if animation cancel token
        {
            break;
        }
        Console.Clear();
        Console.WriteLine("                   |>\n              |    |    | \n             )_)  )_)  )_)   \n            )___))___))___)\\ \n           )____)____)_____)\\ \n         _____|____|____|____\\____\n---------\\                  /---------\n^^^^^ ^^^^^  ^^^^^^   ^^^^^^^\n^^^^      ^^^    ^^^    ^^\n      ^^^^   ^^   ^^^ \n \n \n \n    Press any key to continue\n       Press 'esc' to exit");
        await Task.Delay(1500); // Delay for animation effect
        if (menuanimationcancel) // check if animtion cancel token
        {
            break;
        }
        Console.Clear();
        Console.WriteLine("                    |> \n               |    |    | \n              )_)  )_)  )_)   \n             )___))___))___)\\ \n            )____)____)_____)\\ \n          _____|____|____|____\\____\n----------\\                  /---------\n^^^^^ ^^^^^   ^^^^^^ ^^^^   ^^^^^^\n^^^^      ^^^     ^^^    ^^\n      ^^^      ^^^  \n \n \n \n    Press any key to continue\n       Press 'esc' to exit");
        await Task.Delay(1500); // Delay for animation effect
        if (menuanimationcancel) // check if animtion cancel token
        {
            break;
        }
        Console.Clear();
        Console.WriteLine("                   |>\n              |    |    | \n             )_)  )_)  )_)   \n            )___))___))___)\\ \n           )____)____)_____)\\ \n         _____|____|____|____\\____\n---------\\                  /---------\n^^^^^ ^^^^^  ^^^^^^^^        ^^^^^\n^^^^      ^^^    ^^^    ^^\n      ^^^^   ^^   ^^^^^^^^  ^^^ \n \n \n \n    Press any key to continue\n       Press 'esc' to exit");
        await Task.Delay(1500); // Delay for animation effect
        if (menuanimationcancel) // check if animation cancel toekn
        {
            break;
        }
        Console.Clear();
        Console.WriteLine("                  |> \n             |    |    | \n            )_)  )_)  )_)   \n           )___))___))___)\\ \n          )____)____)_____)\\ \n        _____|____|____|____\\____ \n--------\\                  /--------- \n^^^^^ ^^^^^^^  ^^^^^^^^^     ^^^^ \n^^^^      ^^^^     ^^    ^^ \n      ^^^      ^^^ \n \n \n \n    Press any key to continue \n       Press 'esc' to exit ");
        await Task.Delay(1500);  // Delay for animation effect
        if (menuanimationcancel) // check if animation cancel token
        {
            break;
        }     
        Console.Clear();
        Console.WriteLine("                 |> \n            |    |    | \n           )_)  )_)  )_)   \n          )___))___))___)\\ \n         )____)____)_____)\\ \n       _____|____|____|____\\____ \n-------\\                  /---------\n^      ^^^^ ^^^^^^^  ^^^^^^^^^     ^^^^ \n^^^^      ^   ^^^     ^^    ^^ \n      ^^^         ^^^ \n \n \n \n    Press any key to continue \n       Press 'esc' to exit ");
        await Task.Delay(1500);  // Delay for animation effect
        if (menuanimationcancel) // check if animation cancel token
        {
            break;
        }       
    
    }
}
    public void Start() // initiallization options for the game
    {   
        StartGameAnimation();
        while (true)
        {   
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true); // intercept: true prevents the key from being displayed
            if (keyInfo.Key == ConsoleKey.Escape) // Check the key that was pressed
            {
                Environment.Exit(0);
            }
            else
            {
            menuanimationcancel = true;
            StartGame(); // Perform action based on the key press
            }
        }
    }
    public void StartGame() // Game Application itself
    {
        Console.Clear();
        while (menuanimationcancel == true)
        {
            Task.Delay(3000);
            StartMenu();
            string choice = Console.ReadLine();
            if (playerShip.Health <= 0)
            {
                Console.Clear();
                Console.WriteLine("----------------------------------------------\n    Your ship was destroyed. Game over!\n----------------------------------------------");
                Environment.Exit(0);
            }
            if (enemyShip.Health <= 0)
            {
                Console.Clear();
                playerShip.Stolen();
                enemyShip.Health = new Random().Next(75, 151);
                enemyShip.MaxHealth = enemyShip.Health;
            }   
        }

    void StartMenu()
    {
        Console.Clear();
        Console.WriteLine(" \n              |    |    | \n             )_)  )_)  )_)   \n            )___))___))___)\\ \n           )____)____)_____)\\ \n         _____|____|____|____\\____\n---------\\                  /---------------------------\n^^^^^ ^^^^^^^^^         ^^^^^^^^^^^^^     ^^^^^^^\n^^^^      ^^^^     ^^^           ^^^^^^^^^^^^^^^^  ^^\n      ^^^^   ^^^^^^^^^^^^^^^^^^^   ^^^ \n \n \n \n-------------------------------------------------------\n1. Leave Outpost \n2. Shop \n3. Quit\n-------------------------------------------------------");
        while (true)
        {
            // Read key without displaying it
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

            // Pass the key character to the HandleChoice method
            HandleChoice(keyInfo.KeyChar);
        }
    }
void HandleChoice(char choice)
{
    switch (choice)
    {
        case '1':  // Use single quotes for char literals
            Console.Clear();
            OutofPortMenu();
            break;
        case '2':  // Use single quotes for char literals
            Console.Clear();
            ShopMenu();
            break;
        case '3':  // Use single quotes for char literals
            Console.Clear();
            QuitMenu();
            break;
        default:
            Console.Clear();
            Console.WriteLine("-----------------------------\n  Invalid choice. Try again.\n-----------------------------");
            StartMenu();
            break;
    }
}
    void OutofPortMenu()
    {
        while (true)
        {
        Console.WriteLine("\n              |    |    | \n             )_)  )_)  )_)   \n            )___))___))___)\\ \n           )____)____)_____)\\ \n         _____|____|____|____\\____\n---------\\                  /---------------------------\n^^^^^ ^^^^^^^^^^^ ^^^^^^^^^^^^  ^^^^^^^^^^\n^^^^      ^^^^  ^^^^^^^^^^^^^^^^^^^^^^   ^^^    ^^\n      ^^^^ ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^     ^^^ \n \n \n \n-------------------------------------------------------\n1. Attack Ship \n2. Repair Ship \n3. Search for treausre\n4. Go back to outpost\n-------------------------------------------------------");
        while (true)
        {
            // Read key without displaying it
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

            // Pass the key character to the HandleChoice method
            OutofPortChoice(keyInfo.KeyChar);
        }

        }
    }
   void OutofPortChoice(char choice)
    {
        switch (choice)
        {
            case '1':
                Console.Clear();
                AttackSequence();
                break;
            case '2':
                Console.Clear();
                RepairShip();
                break;
            case '3':
                Console.Clear();
                SearchTreasure();
                break;
            case '4':
                enemyShip.Health = new Random().Next(75, 151);  // Reset enemy ship health so the you don't sink it twice
                StartMenu();
                Console.Clear();
                break;
            default:
                Console.Clear();
                Console.WriteLine("-----------------------------\n  Invalid choice. Try again.\n-----------------------------");
                OutofPortMenu();
                break;
        }
    }
void AttackSequence()
{
    Console.Clear();
    enemyShip.Health = new Random().Next(75, 151);  // Reset enemy ship health
    enemyShip.MaxHealth = enemyShip.Health;
    Console.WriteLine($"-------------------------------------------------------\nEnemy ship health: {enemyShip.Health}\nYour current health: {playerShip.Health}\nDo you want to fight this ship?\n-------------------------------------------------------\n1. Fight ship\n2. No\n-------------------------------------------------------");
    string choice = Console.ReadLine();

    if (choice == "1")
    {
        Console.Clear();
        while (enemyShip.Health > 0 && playerShip.Health > 0)  // Make sure playerShip.Health is > 0
        {
            if (playerShip.Health <= 0)
            {
                Console.Clear();
                Console.WriteLine("-------------------------------------------------------\nYour ship was destroyed. Game Over.\n-------------------------------------------------------");
                Environment.Exit(0);  // Exit the program if player's health is 0 or less
            }
            if (enemyShip.Health > 31)
            {
                Console.WriteLine($"-------------------------------------------------------\nEnemy ship health: {enemyShip.Health}/{enemyShip.MaxHealth}\n-------------------------------------------------------\nYour current health: {playerShip.Health}/{playerShip.MaxHealth}\n-------------------------------------------------------\n1. Shoot cannons\n2. Repair your ship\n3. Leave fight\n-------------------------------------------------------");
            }
            if (enemyShip.Health < 30 && enemyShip.Health > 0)
            {
                Console.WriteLine($"Enemy ship health: {enemyShip.Health}/{enemyShip.MaxHealth}\n-------------------------------------------------------\nYour current health: {playerShip.Health}/{playerShip.MaxHealth}\n-------------------------------------------------------\n1. Shoot cannons\n2. Repair your ship\n3. Leave fight\n4. Board Ship\n-------------------------------------------------------");
            }

            choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Clear();
                    if (enemyShip.Health > 0)
                    {
                        playerShip.Attack(enemyShip);
                        if (enemyShip.Health <= 0)
                        {
                            playerShip.Stolen();
                            break;
                        }
                        else
                        {
                            enemyShip.Assault(playerShip);
                        }
                    }
                    break;

                case "2":
                    Console.Clear();
                    playerShip.Repair();
                    enemyShip.EnemyRepair();
                    break;

                case "3":
                    Console.Clear();
                    Console.WriteLine("---------------------------\n You have left the fight \n---------------------------");
                    return;

                case "4":
                    if (enemyShip.Health < 30 && enemyShip.Health > 0)
                    {
                        Console.Clear();
                        playerShip.BoardChance(enemyShip);
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("------------------------------\n Invalid choice. Try again. \n------------------------------");
                        break;
                    }
            }

            // Final check for player's health after each action
            if (playerShip.Health <= 0)
            {
                Console.Clear();
                Console.WriteLine("-------------------------------------------------------\nYour ship was destroyed. Game Over.\n-------------------------------------------------------");
                Environment.Exit(0);  // Exit the game if player's health is 0 or less
            }
        }
    }
    else if (choice == "2")
    {
        Console.Clear();
        Console.WriteLine("-----------------------------\n You didn't take the fight \n-----------------------------");
    }
}
    void RepairShip()
    {
        Console.Clear();
        playerShip.Repair();
        OutofPortMenu();
    }
    void SearchTreasure()
    {
        Console.Clear();
        playerShip.Treasure();
        OutofPortMenu();
    }
    void ShopMenu()
    {
        while (true)
        {
            Console.WriteLine($"--------------------------------------------------------------------------------------------------------------------------------------------------\n Stats: Health: {playerShip.Health}/{playerShip.MaxHealth} Cannons:{playerShip.Cannons}/{playerShip.MaxCannons} Crew: {playerShip.Crew}/{playerShip.MaxCrew} Gold: {playerShip.Bank} Captured ships: {playerShip.Items} \n--------------------------------------------------------------------------------------------------------------------------------------------------\n------------------------------------------------------\nI                  Welcome to the Shop               I\nI       1. Buy cannons (1000 coins)                  I\nI       2. Hire crew members (100 coins)             I\nI       3. Upgrade ship (5000 coins)                 I\nI       4. Sell captured ship (1000 coins)           I\nI       5. Leave shop                                I\n------------------------------------------------------");
        while (true)
        {
            // Read key without displaying it
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

            // Pass the key character to the HandleChoice method
            HandleShopChoice(keyInfo.KeyChar);
        }
        }
    }
    void HandleShopChoice(char choice)
    {
        switch (choice)
        {
            case '1':
                Console.Clear();
                if (playerShip.Bank < 1000)
                {
                    Console.WriteLine("-------------------------------\n you don't have enough coins \n-------------------------------");
                }
                else if (playerShip.Cannons < playerShip.MaxCannons)
                {
                    playerShip.Bank -= 1000;
                    playerShip.Cannons += 1;         
                    Console.WriteLine($"---------------------------------------------\n You now have {playerShip.Cannons} cannons \n---------------------------------------------");
                }
                else
                {
                    Console.WriteLine("----------------------------------------------------------------------\n You have reached your max cannons amount (upgrade ship to increase) \n----------------------------------------------------------------------");
                }
                ShopMenu();
                break;
            case '2':
                Console.Clear();
                if (playerShip.Bank < 100)
                {                                   
                    Console.WriteLine("-------------------------------\n You don't have enough coins \n-------------------------------");
                }
                else if (playerShip.Crew < playerShip.MaxCrew)
                {
                    playerShip.Bank -= 100;
                    playerShip.Crew += 10;
                    if (playerShip.Crew > playerShip.MaxCrew)
                    {
                        playerShip.Crew = playerShip.MaxCrew;
                    }                                
                    Console.WriteLine($"-----------------------------------------------\n You now have {playerShip.Crew} crew members \n-----------------------------------------------");
                }
                else
                {                                   
                    Console.WriteLine("--------------------------------------------------------------------\n  You have reached your max crew amount (upgrade ship to increase) \n--------------------------------------------------------------------");
                }
                ShopMenu();
                break;
            case '3':
                Console.Clear();
                if (playerShip.Bank < 5000)
                {                                   
                    Console.WriteLine("-------------------------------\n You don't have enough coins \n-------------------------------");
                }
                else
                {
                    playerShip.Bank -= 5000;
                    playerShip.MaxHealth += 50;
                    playerShip.MaxCrew += 50;
                    playerShip.MaxCannons += 5;      
                    Console.WriteLine($"---------------------------------------------------------------------------------------------------------------------\n  Upgraded ship: Health to {playerShip.MaxHealth}, Crew to {playerShip.MaxCrew}, Cannons to {playerShip.MaxCannons} \n---------------------------------------------------------------------------------------------------------------------");
                }
                ShopMenu();
                break;
            case '4':
                Console.Clear();
                if (playerShip.Items < 1)
                {                                                                          
                    Console.WriteLine("------------------------------------- You don't have any captured ships \n-------------------------------------");
                }
                else
                {
                    playerShip.Items -= 1;
                    playerShip.Bank += 1000;        
                    Console.WriteLine("---------------------------------\n You have sold a captured ship \n---------------------------------");
                }
                ShopMenu();
                break;
            case '5':
                Console.Clear();
                StartMenu();
                break;
            default:
                Console.Clear();                
                Console.WriteLine("------------------------------\n Invalid choice. Try again. \n------------------------------");
                ShopMenu();
                break;
        }
    }
    void QuitMenu()
    {
        Console.WriteLine("-------------------------------------------------------\nAre you sure you want to quit?\n-------------------------------------------------------\n1. Yes\n2. No\n-------------------------------------------------------");
        while (true)
        {
            // Read key without displaying it
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

            // Pass the key character to the HandleChoice method
            QuitChoiceHandler(keyInfo.KeyChar);
        }
    }
    void QuitChoiceHandler(char choice)
    {
        switch (choice)
        {
            case '1':
                Environment.Exit(1);
                break;
            case '2':
                Console.Clear();
                StartMenu();
                break;
            default:
                Console.Clear();                
                Console.WriteLine("------------------------------\n Invalid choice. Try again. \n------------------------------");
                QuitMenu();
                break;    
        }
    }

    static void Main(string[] args)
    {
        PirateGame game = new PirateGame();
        game.Start();
    }
}
}
}