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
            Console.WriteLine("You missed");
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
    public void Stolen() // defines how much gold is given after a ship has been sunk
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
    bool menuanimationcancel = false;
    bool attackanimationcancel = true;
    bool shopanimationcancel = true;
    bool LeavePortAnimationCancel = true;
    bool ReturnPortAnimationCanel = true;
    private Ship playerShip;
    private Ship enemyShip;
    public PirateGame()
    {
        playerShip = new Ship("Player Ship", 5, 50, 0, 100, 0, 0, 50, 10, 0); // 5 = cannons, 50 = crew, 100 = maxhealth, 0 = inventory.
        enemyShip = new Ship("Enemy Ship", new Random().Next(1, 6), new Random().Next(10, 51), 0, new Random().Next(75, 151), 0, new Random().Next(1, 6), 50, 10, new Random().Next(10, 51));
    }
    public async void StartGameAnimation()
    {
        while (menuanimationcancel == false) // animation at the start of the game
        {    
            Console.Clear();
            Console.WriteLine($"Stop Req: {menuanimationcancel}"); // don't remove, this makes the animation cancel work properly, if removed code completely breaks.
            Console.Clear();
            Console.WriteLine("                  |> \n             |    |    | \n            )_)  )_)  )_)   \n           )___))___))___)\\ \n          )____)____)_____)\\ \n        _____|____|____|____\\____\n--------\\                  /---------\n^^^^^ ^^^^^^^^^^^^^^^^^^^^\n^^^^      ^^^^     ^^    ^^\n      ^^^      ^^^ \n \n \n \n Press any key to continue\nPress 'esc' to exit ");
            await Task.Delay(1500);
            if (menuanimationcancel == true)
            {
                break;
            }
            Console.Clear();
            Console.WriteLine("                   |> \n              |    |    | \n             )_)  )_)  )_)   \n            )___))___))___)\\ \n           )____)____)_____)\\ \n         _____|____|____|____\\____\n---------\\                  /---------\n^^^^^ ^^^^^  ^^^^^^^^^^^^^\n^^^^      ^^^    ^^^    ^^\n      ^^^^   ^^   ^^^ \n \n \n \n Press any key to continue\nPress 'esc' to exit");
            await Task.Delay(1500);
            if (menuanimationcancel == true)
            {
                break;
            }
            Console.Clear();
            Console.WriteLine("                   |> \n              |    |    | \n             )_)  )_)  )_)   \n            )___))___))___)\\ \n           )____)____)_____)\\ \n         _____|____|____|____\\____\n---------\\                  /---------\n^^^^^ ^^^^^^^^^^^^^^^^^^^^^\n^^^^      ^^^     ^^^    ^^\n      ^^^      ^^^  \n \n \n \n Press any key to continue\n  Press 'esc' to exit");
            await Task.Delay(1500);
            if (menuanimationcancel == true)
            {
                break;
            }
        }
    }
    public void attackanimation()
    {
        while (attackanimationcancel == false)
        {
            Console.WriteLine($"{attackanimationcancel}");
            Console.WriteLine("             |>            \n       |    |    |             		   \n       )_)  )_)  )_)                                       |> \n      )___))___))___)\\                               |    |    | \n     )____)____)_____)\\                             (_(  (_(  (_(  \n    _____|____|____|____\\____                     //(___((___((___(  \n---\\                 /------------------------- //(____(____(_____( \n^^^^^ ^^^^^^^^^^^^^^^^^^^^  ^^^^ ^^^^^^^^^^ ____//____|____|____|_____ \n^^^^^      ^^^^     ^^   ^^^^^^^ ^        ^^^^^^^^\\                 /^^\n ^^^^^ ^^^^^^^^                 ^^^^^^^^^^^^               ^^^^ ^^^^ \n ^^      ^^^      ^^^  ^^^^^^^^^^^^^^^^^^^^^^^^^ ^^^^^         ^^^^^^^^ ");
             Task.Delay(1500);
            Console.Clear();
            Console.WriteLine("             |>            \n       |    |    |             		   \n       )_)  )_)  )_)                                       |> \n      )___))___))___)\\                               |    |    | \n     )____)____)_____)\\                             (_(  (_(  (_(  \n    _____|____|____|____\\____                     //(___((___((___(  \n---\\                 /------------------------- //(____(____(_____( \n^^^^^ ^^^^^^^^^^^^^^^^^^^^  ^^^^ ^^^^^^^^^^ ____//____|____|____|_____ \n^^^^^      ^^^^     ^^   ^^^^^^^ ^        ^^^^^^^^\\                 /^^\n ^^^^^ ^^^^^^^^                 ^^^^^^^^^^^^               ^^^^ ^^^^ \n ^^      ^^^      ^^^  ^^^^^^^^^^^^^^^^^^^^^^^^^ ^^^^^         ^^^^^^^^ "); 
             Task.Delay(1500);
            Console.Clear();
            Console.WriteLine("             |>            \n       |    |    |             		   \n       )_)  )_)  )_)                                       |> \n      )___))___))___)\\                               |    |    | \n     )____)____)_____)\\                             (_(  (_(  (_(  \n    _____|____|____|____\\____                     //(___((___((___(  \n---\\                 /------------------------- //(____(____(_____( \n^^^^^ ^^^^^^^^^^^^^^^^^^^^  ^^^^ ^^^^^^^^^^ ____//____|____|____|_____ \n^^^^^      ^^^^     ^^   ^^^^^^^ ^        ^^^^^^^^\\                 /^^\n ^^^^^ ^^^^^^^^                 ^^^^^^^^^^^^               ^^^^ ^^^^ \n ^^      ^^^      ^^^  ^^^^^^^^^^^^^^^^^^^^^^^^^ ^^^^^         ^^^^^^^^ "); 
             Task.Delay(1500);
            attackanimationcancel = true;
            break;
        }
    }
    public void shopanimation()
    {
        while (shopanimationcancel == false)
        {
            Console.WriteLine($"{shopanimationcancel}");
            Console.WriteLine($"             |>                    / \n        |    |    |               /    \n       )_)  )_)  )_)             /      /- \n      )___))___))___)\\          |      | |        /-------------- \n     )____)____)_____)\\        /       | |        |  |_____________| \n   _____|____|____|____\\____          | |      \n---                  /----------|     /___      \n^^^^^ ^^^^^^^^^^^^^^^^^^^       /                /--- \n^^^^^      ^^^^     ^^         |     /-         |___|      \n ^^      ^^^      ^^^          /     | | \n                               ");
        }
    }   
    public void LeavePortAnimation()
    {
        while (LeavePortAnimationCancel == false)
        {
            Console.WriteLine($"{LeavePortAnimationCancel}");
            Console.WriteLine("             |>                    / \n        |    |    |               /    \n       (_(  (_(  (_(             /      /- \n    //(___((___((___(            |      | |        /-------------- \n   //(____(____(_____(          /       | |        |  |_____________| \n__//____|____|____|____               | |      \n---                  /----------|     /___      \n^^^^^ ^^^^^^^^^^^^^^^^^^^       /                /---) \n^^^^^      ^^^^     ^^         |     /–)         |___|      \n ^^      ^^^      ^^^          /     | | \n                              ");
            LeavePortAnimationCancel = true;
        }
    }
    public void ReturnPortAnimation()
    {
        while (ReturnPortAnimationCanel == false)
        {
            Console.WriteLine($"{ReturnPortAnimationCanel}");
        }
    }
    public async void animation()
{
Console.WriteLine("             |>  ");
Console.WriteLine("        |    |    |");
Console.WriteLine("       )_)  )_)  )_)                                       |>");
Console.WriteLine("      )___))___))___)\\                               |    |    |");
Console.WriteLine("     )____)____)_____)\\                             (_(  (_(  (_(");
Console.WriteLine("    ____|____|____|____\\_____                     /(___((___((___( ");
Console.WriteLine("----\\                /                           /(____(____(_____(");
Console.WriteLine("^^^^^^^^^^^^^^^^^^^^  ^^^^ ^^^^^^^^^^       ____//___|____|____|_____ ");
Console.WriteLine("^^^^^^^ ^        ^^^^^^^^                          \\                /^^ ^^^^^ ^^^^^^^^");
Console.WriteLine("^^^^^^^^^^^^               ^^^^ ^^^^    ^^      ^^^      ^^^  ^^^^^^^^^^^^^^^^^^^^^^^^^ ^^^^^ ");
Console.WriteLine("^^^^^^^^            ^^^^^^^^^^^^                          ^^^^^^^^^^^^^            ^^^^^^^^^^^^^ ");
await Task.Delay(500);
Console.Clear();
Console.WriteLine("             |>  ");
Console.WriteLine("        |    |    |");
Console.WriteLine("       )_)  )_)  )_)                                    |>");
Console.WriteLine("      )___))___))___)\\                            |    |    |");
Console.WriteLine("     )____)____)_____)\\                          (_(  (_(  (_(");
Console.WriteLine("    ____|____|____|____\\_____                  /(___((___((___( ");
Console.WriteLine("----\\                /                        /(____(____(_____(");
Console.WriteLine("^^^^^^   ^^^^^^^^^^^  ^^^^ ^^^  ^^^^^    ____//___|____|____|_____ ");
Console.WriteLine("^^^^^^^ ^        ^^^^^^^^                       \\                /^^ ^^^^^ ^   ^^^^^");
Console.WriteLine("^^^^^^^  ^^^^^           ^^^  ^ ^^^^    ^^      ^^^      ^^^  ^^^^^^^^^^^^^   ^^^^^^^^^^^^ ^^^^^ ");
Console.WriteLine("^^^^^^^^            ^^^^^^^^^^^^                          ^^^^^^^^^            ^^^^^^^   ^^^^^^ ");
await Task.Delay(500);
Console.Clear();
Console.WriteLine("             |>  ");
Console.WriteLine("        |    |    |");
Console.WriteLine("       )_)  )_)  )_)                                  |>");
Console.WriteLine("      )___))___))___)\\                          |    |    |");
Console.WriteLine("     )____)____)_____)\\                        (_(  (_(  (_(");
Console.WriteLine("    ____|____|____|____\\____                 /(___((___((___( ");
Console.WriteLine("----\\                /                      /(____(____(_____(");
Console.WriteLine("^^^^^^        ^^^^^^  ^^^^ ^^   ^^     ____//___|____|____|_____ ");
Console.WriteLine("^^^^^^^ ^        ^^^^^^^^                     \\                /^^ ^^^^^ ^^^^^^^^");
Console.WriteLine("^^^^^^^^^^^^               ^^^^ ^^^^    ^^      ^^^      ^^^  ^^^^^^^^^^^^^^^ ^^^^^ ");
Console.WriteLine("^^^^^^^^           ^^^        ^^^^^^^            ^^^^^^^^^^^^^                   ^^^^^^^ ");
await Task.Delay(500);
Console.Clear();
Console.WriteLine("             |>  ");
Console.WriteLine("        |    |    |");
Console.WriteLine("       )_)  )_)  )_)                               |>");
Console.WriteLine("      )___))___))___)\\                       |    |    |");
Console.WriteLine("     )____)____)_____)\\                     (_(  (_(  (_(");
Console.WriteLine("    ____|____|____|____\\____              /(___((___((___( ");
Console.WriteLine("----\\                /                   /(____(____(_____(");
Console.WriteLine("^^^^^^        ^^^^^^  ^^^^ ^^       ____//___|____|____|_____ ");
Console.WriteLine("^^^^^^^ ^        ^^^^^^^^                 \\                 /^^ ^^^^^ ^^  ^^^^");
Console.WriteLine("^^^^^^                 ^^^^ ^^^^          ^^      ^^^    ^^^^ ^^^^^ ");
Console.WriteLine("^^^^^^^^           ^^^                   ^^^^^^^^^^^^^^^^^^^^^^^                   ^^^^^^^ ");
await Task.Delay(500);
Console.Clear();
Console.WriteLine("             |>  ");
Console.WriteLine("        |    |    |");
Console.WriteLine("       )_)  )_)  )_)                              |>");
Console.WriteLine("      )___))___))___)\\                      |    |    |");
Console.WriteLine("     )____)____)_____)\\                    (_(  (_(  (_(");
Console.WriteLine("    ____|____|____|____\\____        o o o/(___((___((___( ");
Console.WriteLine("----\\                /                  /(____(____(_____(");
Console.WriteLine("^^^^^^        ^^^^^^  ^^^^ ^^      ____//___|____|____|_____ ");
Console.WriteLine("^^^  ^^ ^        ^^^^^^^^                 \\                /^^ ^^^^^ ^^  ^^^^");
Console.WriteLine("^^^^^^                 ^^^^ ^^^^          ^^      ^^^    ^^^^ ^^^^^ ");
Console.WriteLine("^^^  ^^^           ^^^                   ^^^^^^^^^   ^^^^^  ^^^^                   ^^^^^^^ ");
await Task.Delay(500);
Console.Clear();
Console.WriteLine("             |>  ");
Console.WriteLine("        |    |    |");
Console.WriteLine("       )_)  )_)  )_)                              |>");
Console.WriteLine("      )___))___))___)\\                      |    |    |");
Console.WriteLine("     )____)____)_____)\\            o o o   (_(  (_(  (_(");
Console.WriteLine("    ____|____|____|____\\____             /(___((___((___( ");
Console.WriteLine("----\\                /                  /(____(____(_____(");
Console.WriteLine("^^^^^^        ^^^^^^  ^^^^ ^^      ____//___|____|____|_____ ");
Console.WriteLine("^^^  ^^ ^        ^^^^^^^^                 \\                /^^ ^^^^^ ^^  ^^^^");
Console.WriteLine("^^^^^^                 ^^^^ ^^^^          ^^      ^^^    ^^^^ ^^^^^ ");
Console.WriteLine("^^^  ^^^           ^^^                   ^^^^^^^^^   ^^^^^  ^^^^                   ^^^^^^^ ");
await Task.Delay(500);
Console.Clear();
Console.WriteLine("             |>  ");
Console.WriteLine("        |    |    |");
Console.WriteLine("       )_)  )_)  )_)                              |>");
Console.WriteLine("      )___))___))___)\\          o o o       |    |    |");
Console.WriteLine("     )____)____)_____)\\                    (_(  (_(  (_(");
Console.WriteLine("    ____|____|____|____\\____             /(___((___((___( ");
Console.WriteLine("----\\                /                  /(____(____(_____(");
Console.WriteLine("^^^^^^        ^^^^^^  ^^^^ ^^      ____//___|____|____|_____ ");
Console.WriteLine("^^^  ^^ ^        ^^^^^^^^                 \\                /^^ ^^^^^ ^^  ^^^^");
Console.WriteLine("^^^^^^   ^^^^^^^^^^^^     ^^^^ ^^^^          ^^      ^^^    ^^^^ ^^^^^ ");
Console.WriteLine("^^^  ^^^           ^^^^^^^^^        ^^^^^^       ^^^^^^^^^   ^^^^^  ^^^^                   ^^^^^^^ ");
await Task.Delay(500);
Console.Clear();
Console.WriteLine("             |>  ");
Console.WriteLine("        |    |    |");
Console.WriteLine("       )_)  )_)  )_)                              |>");
Console.WriteLine("      )___))___))___)\\                      |    |    |");
Console.WriteLine("     )____)____)_____)\\     o o o          (_(  (_(  (_(");
Console.WriteLine("    ____|____|____|____\\____             /(___((___((___( ");
Console.WriteLine("----\\                /                  /(____(____(_____(");
Console.WriteLine("^^^^^^        ^^^^^^  ^^^^ ^^      ____//___|____|____|_____ ");
Console.WriteLine("^^^  ^^ ^        ^^^^^^^^                 \\                /^^ ^^^^^ ^^  ^^^^");
Console.WriteLine("^^^^^^   ^^^^^^^^^^^^     ^^^^ ^^^^   ^^      ^^^    ^^^^ ^^^^^ ");
Console.WriteLine("^^^  ^^^           ^^^^^^^^^        ^^^^^^ ^^^^^^^^^   ^^^^^  ^^^^                   ^^^^^^^ ");
await Task.Delay(500);
Console.Clear();
Console.WriteLine("             |>  ");
Console.WriteLine("        |    |    |");
Console.WriteLine("       )_)  )_)  )_)                              |>");
Console.WriteLine("      )___))___))___)\\                      |    |    |");
Console.WriteLine("     )____)____)_____)\\   o o              (_(  (_(  (_(");
Console.WriteLine("    ____|____|____|____\\_o__             /(___((___((___( ");
Console.WriteLine("----\\                /                  /(____(____(_____(");
Console.WriteLine("^^^^^^        ^^^^^^  ^^^^ ^^      ____//___|____|____|_____ ");
Console.WriteLine("^^^  ^^ ^        ^^^^^^^^                 \\                /^^ ^^^^^ ^^  ^^^^");
Console.WriteLine("^^^^^^   ^^^^^^^^     ^^^^ ^^^^   ^^      ^^^    ^^^^ ^^^^^ ");
Console.WriteLine("^^^  ^^^           ^^^^^^^^^        ^^^^^^ ^^^^^^   ^^^^^  ^^^^                   ^^^^^^^ ");
await Task.Delay(500);
Console.Clear();
Console.WriteLine("             |>  ");
Console.WriteLine("        |    |    |");
Console.WriteLine("       )_)  )_)  )_)                              |>");
Console.WriteLine("      )___))___))___)\\                      |    |    |");
Console.WriteLine("     )____)____)_____o\\                    (_(  (_(  (_(");
Console.WriteLine("    ____|____|____|_o__\\___               /(___((___((___( ");
Console.WriteLine("----\\                /                   /(____(____(_____(");
Console.WriteLine("^^^^^^        ^^^^^^  ^^^^ ^^       ____//__|____|____|_____ ");
Console.WriteLine("^^^  ^^ ^        ^^^ ^^^^^                 \\               /^^ ^^^^  ^ ^^  ^^^^");
Console.WriteLine("^^^^^^   ^^^^  ^^^^     ^^^^ ^^^^   ^^      ^^^    ^^^^ ^^^^^ ");
Console.WriteLine("^^^  ^^^           ^^^^^^^^^        ^^^^^^ ^^^^   ^^   ^^^^^  ^^^^                   ^^^^^^^ ");
await Task.Delay(500);
Console.Clear();
Console.WriteLine("             |>  ");
Console.WriteLine("        |    |    |");
Console.WriteLine("       )_)  )_)  )_)                              |>");
Console.WriteLine("      )___))___))___)\\                      |    |    |");
Console.WriteLine("     )____)____)_____)\\                    (_(  (_(  (_(");
Console.WriteLine("    ____|____|____|_o__\\___              /(___((___((___( ");
Console.WriteLine("----\\                /                  /(____(____(_____(");
Console.WriteLine("^^^^^^        ^^^^^^  ^^^^ ^^      ____//___|____|____|_____ ");
Console.WriteLine("^^^  ^^ ^        ^^^ ^^^^^                 \\               /^^ ^^^^  ^ ^^  ^^^^");
Console.WriteLine("^^^^^^   ^^^^  ^^^^     ^^^^ ^^^^   ^^      ^^^    ^^^^ ^^^^^ ");
Console.WriteLine("^^^  ^^^           ^^^^^^      ^^^        ^^^^    ^^ ^^^^   ^^   ^^^^^  ^^^^                   ^^^^^^^ ");
await Task.Delay(500);
Console.Clear();
if (enemyShip.Health > 31)
                {
                Console.WriteLine($"Enemy ship health: {enemyShip.Health}/{enemyShip.MaxHealth}\nYour current health: {playerShip.Health}/{playerShip.MaxHealth}\nOptions:\n1. Shoot cannons\n2. Repair your ship\n3. Leave fight");
                }
                if (enemyShip.Health < 30 && enemyShip.Health > 0)
                { 
                Console.WriteLine($"Enemy ship health: {enemyShip.Health}/{enemyShip.MaxHealth}\nYour current health: {playerShip.Health}/{playerShip.MaxHealth}\nOptions:\n1. Shoot cannons\n2. Repair your ship\n3. Leave fight\n4. Board Ship");
                }
Console.WriteLine("             |>  ");
Console.WriteLine("        |    |    |");
Console.WriteLine("       )_)  )_)  )_)                              |>");
Console.WriteLine("      )___))___))___)\\                      |    |    |");
Console.WriteLine("     )____)____)_____)\\                    (_(  (_(  (_(");
Console.WriteLine("    ____|____|____|____\\___              /(___((___((___( ");
Console.WriteLine("----\\                /                  /(____(____(_____(");
Console.WriteLine("^^^^^^        ^^^^^^  ^^^^ ^^      ____//___|____|____|_____ ");
Console.WriteLine("^^^  ^^ ^        ^^^ ^^^^^                 \\               /^^ ^^^^  ^ ^^  ^^^^");
Console.WriteLine("^^^^^^   ^^^^  ^^^^     ^^^^ ^^^^   ^^      ^^^    ^^^^ ^^^^^ ");
Console.WriteLine("^^^  ^^^           ^^^^^^      ^^^        ^^^^    ^^ ^^^^   ^^   ^^^^^  ^^^^                   ^^^^^^^ ");


} // end of animation
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
#pragma warning disable CS8604 // Possible null reference argument.
            HandleChoice(choice);
#pragma warning restore CS8604 // Possible null reference argument.
            if (playerShip.Health < 1)
            {
                Console.Clear();
                Console.WriteLine("Your ship was destroyed. Game over!");
                Environment.Exit(0);
            }
            if (enemyShip.Health <= 0)
            {
                Console.Clear();
                playerShip.Stolen();
                Console.WriteLine($"Health {playerShip.Health}/{playerShip.MaxHealth}");
                enemyShip.Health = new Random().Next(75, 151);
                enemyShip.MaxHealth = enemyShip.Health;
            }   
        }

    void StartMenu()
    {
        Console.WriteLine(" \n              |    |    | \n             )_)  )_)  )_)   \n            )___))___))___)\\ \n           )____)____)_____)\\ \n         _____|____|____|____\\____\n---------\\                  /---------\n^^^^^ ^^^^^^^^^^^^^^^^^^^^^\n^^^^      ^^^^     ^^^    ^^\n      ^^^^      ^^^ \n \n \n \nOptions:\n1. Leave Outpost \n2. Shop \n3. Quit\nEnter Choice: ");
    }
    void OutofPortMenu()
    {
        Console.Clear();
        while (true)
        {
        Console.WriteLine("\n              |    |    | \n             )_)  )_)  )_)   \n            )___))___))___)\\ \n           )____)____)_____)\\ \n         _____|____|____|____\\____\n---------\\                  /---------\n^^^^^ ^^^^^^^^^^^^^^^^^^^^^\n^^^^      ^^^^     ^^^    ^^\n      ^^^^      ^^^ \n \n \n \nOptions:\n1. Attack Ship \n2. Repair Ship \n3. Search for treausre\n4. Go back to outpost\nEnter Choice: ");
        string choice = Console.ReadLine();
#pragma warning disable CS8604 // Possible null reference argument.
                OutofPortChoice(choice);
#pragma warning restore CS8604 // Possible null reference argument.
                if (choice == "4") break;
        }
    }
    void HandleChoice(string choice)
    {
        switch (choice)
        {
            case "1":
                Console.Clear();
                OutofPortMenu();
                break;
            case "2":
                Console.Clear();
                Shop();
                break;
            case "3":
                Console.Clear();
                Quit();
                break;      
            default:
                Console.Clear();
                Console.WriteLine("Invalid choice. Try again.");
                break;  
        }
    }
   void OutofPortChoice(string choice)
    {
        switch (choice)
        {
            case "1":
                Console.Clear();
                AttackSequence();
                break;
            case "2":
                Console.Clear();
                RepairShip();
                break;
            case "3":
                Console.Clear();
                SearchTreasure();
                break;
            default:
                Console.Clear();
                Console.WriteLine("Invalid choice. Try again.");
                break;
        }
    }
    async void AttackSequence()
    {
        Console.Clear();
        enemyShip.Health = new Random().Next(75, 151);  // Reset enemy ship health
        enemyShip.MaxHealth = enemyShip.Health;
        Console.WriteLine($"Enemy ship health: {enemyShip.Health}\nYour current health: {playerShip.Health}\nDo you want to fight this ship?\n1. Fight ship\n2. No");
        string choice = Console.ReadLine();
        if (choice == "1")
        {   
            Console.Clear();
            while (enemyShip.Health > 0 && playerShip.Health > 1)
            {
                choice = Console.ReadLine();
                switch (choice)
                {
                case "1":
                    animation();
                    if (enemyShip.Health > 0)
                    {
                        playerShip.Attack(enemyShip);
                        if (enemyShip.Health <= 0)
                        {
                            Console.WriteLine("Enemy ship has been defeated!");
                            playerShip.Stolen();
                            Console.WriteLine($"Health {playerShip.Health}/{playerShip.MaxHealth}");
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
                    Console.WriteLine("You have left the fight");
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
    void RepairShip()
    {
        Console.Clear();
        playerShip.Repair();
    }
    void SearchTreasure()
    {
        Console.Clear();
        playerShip.Treasure();
    }
    void Shop()
    {
        Console.Clear();
        while (true)
        {
            Console.WriteLine($"Health: {playerShip.Health}/{playerShip.MaxHealth}, Cannons: {playerShip.Cannons}/{playerShip.MaxCannons}, Crew: {playerShip.Crew}/{playerShip.MaxCrew}, Gold: {playerShip.Bank}, Captured ships: {playerShip.Items} \n     Welcome to the Shop    \n \nOptions\n1. Buy cannons (1000 coins)\n2. Buy crew members (100 coins)\n3. Upgrade ship (5000 coins)\n4. Sell captured ship (1000 coins)\n5. Leave shop");
            string choice = Console.ReadLine();
#pragma warning disable CS8604 // Possible null reference argument.
                HandleShopChoice(choice);
#pragma warning restore CS8604 // Possible null reference argument.
                if (choice == "5") break;
        }
    }
    void HandleShopChoice(string choice)
    {
        switch (choice)
        {
            case "1":
                Console.Clear();
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
                Console.Clear();
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
                Console.Clear();
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
                Console.Clear();
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
                Console.Clear();
                break;
            default:
                Console.Clear();
                Console.WriteLine("Invalid choice. Try again.");
                break;
        }
    }
    void Quit()
    {
        Console.Clear();
        while (true)
        {
            Console.WriteLine("Are you sure you want to quit?\n1. Yes\n 2. No");

            string choice = Console.ReadLine();
            if (choice == "1")
            {
                Console.Clear();
                Console.WriteLine("Thanks for playing!");
                Environment.Exit(0);
            }
            else if (choice == "2")
            {
                Console.Clear();
                break;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid choice. Try again.");
            }
        }
    }
    }
    static void Main(string[] args)
    {
        PirateGame game = new PirateGame();
        game.Start();
    }
}