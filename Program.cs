public class Ship
{
    public string Name {get; set;}
    public int Cannons {get; set;}
    public int Crew {get; set;}
    public int MaxCrew {get; set;}
    public int MaxCannons {get; set;}
    public float Bank {get; set;}
    public float Health {get; set;}
    public float MaxHealth {get; set;}
    public int Items {get; set;}
    public int Chance {get; set;}
    public int EnemyCrew {get; set;}
    public float Cargo {get; set;}
    public float MaxCargo {get; set;}
    private static readonly Random random = new Random();
    public Ship(string name, int cannons, int crew, int bank, int maxhealth, int inventoryItems, int chance, int maxcrew, int maxcannons, int enemycrew, int cargo, int maxcargo)
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
        Cargo = cargo;
        MaxCargo = maxcargo;

    }
    public void Attack(Ship target) // player ship attack function
    {
        int chance = random.Next(1, 11);
        if (chance >= 2)
        {
            float damage = (float)(random.NextDouble() * (21.0 - 10.0) + 10.0) + Cannons;
            damage = (float)Math.Round(damage, 1);
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
            float damage = (float)(random.NextDouble() * (21.0 - 10.0) + 10.0) + Cannons;
            damage = (float)Math.Round(damage, 1);            
            target.Health -= damage;
            target.Health = Math.Max(target.Health, 0);
        }
        else
        {
            Console.WriteLine("-------------------------\n The enemy ship missed\n-------------------------");
        }
    }
    public void BoardChance(Ship target) // boarding procudure  
    {
        Chance = random.Next(1,5);
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
    public void Repair() // repair function for player ship
    {
        float repairAmount = random.Next(10, 21);
        Health += repairAmount;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        Console.WriteLine($"-------------------------------------------------------\nHealth: {Health}/{MaxHealth}\n-------------------------------------------------------");
    }
    public void Treasure()
    {
        Console.Clear();
        float treasureAmount = random.Next(10, 21);
        if (MaxCargo - Cargo < treasureAmount)
        {Cargo = MaxCargo;
        Console.WriteLine($"----------------------------------------------\n{treasureAmount} gold found\n----------------------------------------------\nYou could not carry all of the gold\n----------------------------------------------\nGold: {Cargo}/{MaxCargo} gold\n----------------------------------------------");            
        }
        if (MaxCargo - Cargo >= treasureAmount)
        {
            Cargo += treasureAmount;
            Console.WriteLine($"----------------------------------------------\n{treasureAmount} gold found\n----------------------------------------------\nGold: {Cargo}/{MaxCargo} gold\n----------------------------------------------");        
        }
    }
    public void Stolen() // defines how much gold is given after a ship has been sunk
    {
        float stolenAmount = random.Next(0, 250);
        if (MaxCargo - Cargo < stolenAmount)
        {                                                                                                                                                        
            Console.WriteLine($"-------------------------------------------------------\n Enemy ship has been defeated! \n-------------------------------------------------------\n You sank the enemy ship and found {stolenAmount} gold\n-------------------------------------------------------\n You could not carry all of it and stole {MaxCargo - Cargo} gold\n-------------------------------------------------------\n You now have {MaxCargo}/{MaxCargo} gold \n-------------------------------------------------------\n Health {Health}/{MaxHealth}\n-------------------------------------------------------");
            Cargo = MaxCargo;
        }
        if (MaxCargo - Cargo >= stolenAmount)
        {
            Cargo += stolenAmount;
            Console.WriteLine($"---------------------------------------------------------\n Enemy ship has been defeated! \n---------------------------------------------------------\n You sank the enemy ship and found {stolenAmount} gold and stole it\n---------------------------------------------------------\n You now have {Cargo}/{MaxCargo} gold \n---------------------------------------------------------\n Health {Health}/{MaxHealth}\n---------------------------------------------------------");
        }
    }
    public void Depot()
    {
        Bank += Cargo;
        Console.WriteLine($"----------------------------------------------\n{Cargo} Deposited in bank\n----------------------------------------------");
        Console.WriteLine($"----------------------------------------------\nYou now have {Bank} gold in the bank\n----------------------------------------------");
    }
    public void EnemyRepair() // enemy ship repair function
    {
        int chance = random.Next(1, 11);
        if (chance > 7)
        {
            float repairAmount = random.Next(10, 21);
            Health += repairAmount;
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
            Console.WriteLine($"------------------------------------------------\nThe enemy ship repaired itself to {Health}/{MaxHealth}\n------------------------------------------------");
        }
    }
}
public class PirateGame
{
    bool menuanimationcancel = false;
    private Ship playerShip;
    private Ship enemyShip;

    public PirateGame()
    {
        playerShip = new Ship("Player Ship", 5, 50, 0, 100, 0, 0, 50, 10, 0, 0, 500);
        enemyShip = new Ship("Enemy Ship", new Random().Next(1, 6), new Random().Next(10, 51), 0, new Random().Next(75, 151), 0, new Random().Next(1, 6), 50, 10, new Random().Next(10, 51), 0, 0);
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
        menuanimationcancel = false;
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
            Console.Clear();
            EnterNameMenu(); // Perform action based on the key press
            }
        }
    void EnterNameMenu()
    {
        Console.WriteLine("Enter your name");
        while (true)
        {
            string name = Console.ReadLine();
                 if (string.IsNullOrEmpty(name))
                    {
                        Console.Clear();
                        Console.WriteLine("Please enter a name");
                    }
                else
                    {
                       Console.Clear();  
                       playerShip.Name = name;
                       StartMenu(); 
                    }
        }
    }
    void StartMenu()
    {
        Console.WriteLine(" \n              |    |    | \n             )_)  )_)  )_)   \n            )___))___))___)\\ \n           )____)____)_____)\\ \n         _____|____|____|____\\____\n---------\\                  /---------------------------\n^^^^^ ^^^^^^^^^         ^^^^^^^^^^^^^     ^^^^^^^\n^^^^      ^^^^     ^^^           ^^^^^^^^^^^^^^^^  ^^\n      ^^^^   ^^^^^^^^^^^^^^^^^^^   ^^^ \n \n \n \n-------------------------------------------------------\n1. Leave Outpost \n2. Shop \n3. Deposit gold\n4. Quit\n-------------------------------------------------------");
        while (true)
        {
            // Read key without displaying it
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

            // Pass the key character to the StartMenuHandler method
            StaratMenuHandler(keyInfo.KeyChar);
        }
    }
    void StaratMenuHandler(char choice)
    {
        switch (choice)
        {
            case '1':  // Use single quotes for char literals
                Console.Clear();
                OutofPortMenu();
                break;
            case '2':  
                Console.Clear();
                ShopMenu();
                break;
            case '3':
                Console.Clear();
                playerShip.Depot();
                StartMenu();
                break;
            case '4':  
                Console.Clear();
                QuitMenu();
                break;
            case (char)ConsoleKey.Escape:
                Console.Clear();
                Start();
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
        if (playerShip.Health <= 0)
        {   
            Console.Clear();
            Console.WriteLine("----------------------------------------------\n    Your ship was destroyed. Game over!\n----------------------------------------------");
            Environment.Exit(1);
        }
        Console.WriteLine("\n              |    |    | \n             )_)  )_)  )_)   \n            )___))___))___)\\ \n           )____)____)_____)\\ \n         _____|____|____|____\\____\n---------\\                  /---------------------------\n^^^^^ ^^^^^^^^^^^ ^^^^^^^^^^^^  ^^^^^^^^^^\n^^^^      ^^^^  ^^^^^^^^^^^^^^^^^^^^^^   ^^^    ^^\n      ^^^^ ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^     ^^^ \n \n \n \n-------------------------------------------------------\n1. Attack Ship \n2. Repair Ship \n3. Search for treausre\n4. Go back to outpost\n-------------------------------------------------------");
        while (true)
        {
            // Read key without displaying it
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

            // Pass the key character to the HandleChoice method
            OutofPortChoiceHandler(keyInfo.KeyChar);
        }
    }
    void OutofPortChoiceHandler(char choice)
    {
        switch (choice)
        {
            case '1':
                enemyShip.MaxHealth = new Random().Next(75, 151); 
                enemyShip.Health = enemyShip.MaxHealth;
                Console.Clear();
                AttackMenu();
                break;
            case '2':
                Console.Clear();
                playerShip.Repair();
                OutofPortMenu();
                break;
            case '3':
                int chance = new Random().Next(1,20); // possible chance to find a ship while looking for treasure
                Console.Clear();
                if (chance <= 2)
                {
                    enemyShip.MaxHealth = new Random().Next(75, 151); 
                    enemyShip.Health = enemyShip.MaxHealth;
                    Console.WriteLine("-------------------------------------------------------\nAn enemy ship found you\n-------------------------------------------------------");
                    enemyShip.Assault(playerShip);
                    FightMenu();
                    break;
                }
                else
                {
                playerShip.Treasure();
                OutofPortMenu();
                break;
                }

            case '4':
                Console.Clear();
                StartMenu();
                break;
            case (char)ConsoleKey.Escape: // c# is dumb and doesn't let me use 'esc'
                Console.Clear();
                StartMenu();
                break;

            default:
                Console.Clear();
                Console.WriteLine("-----------------------------\n  Invalid choice. Try again.\n-----------------------------");
                OutofPortMenu();
                break;
        }
    }
    void AttackMenu()
    {
        Console.WriteLine($"-------------------------------------------------------\nEnemy ship health: {enemyShip.Health}\nYour current health: {playerShip.Health}\nDo you want to fight this ship?\n-------------------------------------------------------\n1. Fight ship\n2. No\n-------------------------------------------------------"); 
        while (true)
            {
                // Read key without displaying it
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

                // Pass the key character to the HandleChoice method
                AttackChoiceHandler(keyInfo.KeyChar);
            }
        }
    void AttackChoiceHandler(char choice)
    {
        switch (choice)
        {
            case '1':
                Console.Clear();
                FightMenu();
                break;
            case '2':
                Console.Clear();
                OutofPortMenu();
                break;
            case (char)ConsoleKey.Escape:
                Console.Clear();
                OutofPortMenu();
                break;
            default:
                Console.Clear();
                Console.WriteLine("-----------------------------\n  Invalid choice. Try again.\n-----------------------------");
                AttackMenu();
                break;
        }
    }
    void FightMenu()
    {
        if (enemyShip.Health > 30)
        {
        Console.WriteLine($"-------------------------------------------------------\nEnemy ship health: {enemyShip.Health}/{enemyShip.MaxHealth}\n-------------------------------------------------------\nYour current health: {playerShip.Health}/{playerShip.MaxHealth}\n-------------------------------------------------------\n1. Shoot cannons\n2. Repair your ship\n3. Leave fight\n-------------------------------------------------------");
        }
        if (enemyShip.Health <= 30)
        {
        Console.WriteLine($"-------------------------------------------------------\nEnemy ship health: {enemyShip.Health}/{enemyShip.MaxHealth}\n-------------------------------------------------------\nYour current health: {playerShip.Health}/{playerShip.MaxHealth}\n-------------------------------------------------------\n1. Shoot cannons\n2. Repair your ship\n3. Leave fight\n4. Board Ship\n-------------------------------------------------------");
    
        }
        if (playerShip.Health <= 0)
        {   
            Console.Clear();
            Console.WriteLine("----------------------------------------------\n    Your ship was destroyed. Game over!\n----------------------------------------------");
            Environment.Exit(1);
        }   
        if (enemyShip.Health <= 0)
        {
            Console.Clear();
            playerShip.Stolen();
            OutofPortMenu();
            return;
        }
        while (true)
        {
            // Read key without displaying it
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

            // Pass the key character to the HandleChoice method
            FightChoiceHandler(keyInfo.KeyChar);
        }
    }
    void FightChoiceHandler(char choice)
    {
        switch (choice)
        {
            case '1':
                Console.Clear();
                playerShip.Attack(enemyShip);
                enemyShip.Assault(playerShip);
                FightMenu();
                break;
            case '2':  // Use single quotes for char literals
                Console.Clear();
                playerShip.Repair();
                enemyShip.EnemyRepair();
                FightMenu();
                break;
            case '3':  // Use single quotes for char literals
                Console.Clear();
                LeaveFightMenu();
                break;
            case '4':
                if (enemyShip.Health > 30)
                {
                Console.Clear();
                Console.WriteLine("-----------------------------\n  Invalid choice. Try again.\n-----------------------------");
                FightMenu();
                break; 
                }
                if (enemyShip.Health <= 30)
                {
                   Console.Clear();
                   playerShip.BoardChance(enemyShip);
                   OutofPortMenu();
                   break;
                }
                break;
            case (char)ConsoleKey.Escape:
                Console.Clear();
                OutofPortMenu();
                break;
            default:
                Console.Clear();
                Console.WriteLine("-----------------------------\n  Invalid choice. Try again.\n-----------------------------");
                FightMenu();
                break;
        }
    }
    void LeaveFightMenu()
    {
        Console.WriteLine("-------------------------------------------------------\nAre you sure you want to leave fight?\n-------------------------------------------------------\n1. Yes\n2. No\n-------------------------------------------------------");
        while (true)
        {
            // Read key without displaying it
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

            // Pass the key character to the HandleChoice method
            LeaveFightChoiceHandler(keyInfo.KeyChar);
        }
    }
    void LeaveFightChoiceHandler(char choice)
    {
        switch (choice)
        {
            case '1':
                Console.Clear();
                OutofPortMenu();
                break;

            case '2':
                Console.Clear();
                FightMenu();
                break;
            case (char)ConsoleKey.Escape:
                Console.Clear();
                FightMenu();
                break;
             default:
                Console.Clear();
                Console.WriteLine("-----------------------------\n  Invalid choice. Try again.\n-----------------------------");
                LeaveFightMenu();
                break;
        }
    }
    void ShopMenu()
    {
        while (true)
        {
            Console.WriteLine($"Welcome to the shop, {playerShip.Name}");
            Console.WriteLine($"--------------------------------------------------------------------------------------------------------------------------------------------------\n Stats: Health: {playerShip.Health}/{playerShip.MaxHealth} | Cannons:{playerShip.Cannons}/{playerShip.MaxCannons} | Crew: {playerShip.Crew}/{playerShip.MaxCrew} | Gold: {playerShip.Bank} | Captured ships: {playerShip.Items} \n--------------------------------------------------------------------------------------------------------------------------------------------------\n------------------------------------------------------\nI       1. Buy cannons (1000 coins)                  I\nI       2. Hire crew members (100 coins)             I\nI       3. Upgrade ship (5000 coins)                 I\nI       4. Sell captured ship (1000 coins)           I\nI       5. Leave shop                                I\n------------------------------------------------------");
        while (true)
        {
            // Read key without displaying it
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

            // Pass the key character to the HandleChoice method
            ShopChoiceHandler(keyInfo.KeyChar);
        }
        }
    }
    void ShopChoiceHandler(char choice)
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
                    playerShip.MaxCargo += 500;   
                    Console.WriteLine($"---------------------------------------------------------------------------------------------------------------------\n  Upgraded ship: Health to {playerShip.MaxHealth}, Crew to {playerShip.MaxCrew}, Cannons to {playerShip.MaxCannons}, Max cargo to {playerShip.MaxCargo} \n---------------------------------------------------------------------------------------------------------------------");
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
            case (char)ConsoleKey.Escape:
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
            case (char)ConsoleKey.Escape:
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
}
    static void Main(string[] args)
    {
        PirateGame game = new PirateGame();
        game.Start();
    }
}