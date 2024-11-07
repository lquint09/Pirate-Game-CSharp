public class Ship {
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
    public int Cannonballs {get; set;}
    public int CursedCannonBalls {get; set;}
    public int Wood {get; set;} 
    private static readonly Random random = new Random();
    public Ship(string name, int cannons, int crew, int bank, int maxhealth, int inventoryItems, int chance, int maxcrew, int maxcannons, int enemycrew, int cargo, int maxcargo, int cannonballs, int cursedcannonballs, int wood) {
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
        Cannonballs = cannonballs;
        CursedCannonBalls = cursedcannonballs;
        Wood = wood;
    }
    public void Attack(Ship target) {
        int chance = random.Next(1, 11);
        Cannonballs -= 1;
        if (chance >= 2) {
            float damage = (float)(random.NextDouble() * (21.0 - 10.0) + 10.0) + Cannons;
            damage = (float)Math.Round(damage, 1);
            target.Health -= damage;
            target.Health = Math.Max(target.Health, 0);
        }
        else {
            Console.WriteLine("-------------\n You missed\n-------------");

        }
    }
    public void Assault(Ship target) {
        int chance = random.Next(1, 11);
        if (chance >= 2) {
            float damage = (float)(random.NextDouble() * (21.0 - 10.0) + 10.0) + Cannons;
            damage = (float)Math.Round(damage, 1);            
            target.Health -= damage;
            target.Health = Math.Max(target.Health, 0);
        }
        else {
            Console.WriteLine("-------------------------\n The enemy ship missed\n-------------------------");
        }
    }
    public void CursedBallAttack(Ship target) {
        CursedCannonBalls -= 1;
        float damage = (float)(random.NextDouble()* (40 - 15)+ 10) + Cannons;
        damage = (float)Math.Round(damage, 1);
        target.Health -= damage;
        target.Health = Math.Max(target.Health, 0);
    }
    public void BoardChance(Ship target) {
        Chance = random.Next(1,5);
        if (target.Health < 31) {
            if (Crew > EnemyCrew) {
                if (Chance >= 2) {
                    Items += 1;
                    Crew = Math.Max(Crew - 5, 0);
                    Console.Clear();
                    Console.WriteLine($"----------------------------------------------\nYou won the board\n----------------------------------------------\nYou now have {Items} captured ships\n----------------------------------------------\nCrew {Crew}/{MaxCrew}\n----------------------------------------------");
                    target.Health = 0;
                    
                }
                else {
                    Crew = 25;
                    Health = 1;
                    Console.Clear();
                    Console.WriteLine($"----------------------------------------------\nYou lost the board\n----------------------------------------------\nCrew: {Crew}/{MaxCrew}\n----------------------------------------------\nHealth: {Health}/{MaxHealth}\n----------------------------------------------");
                    return;
                }
            }
        }
    }
    public void Repair() {
        float repairAmount = random.Next(10, 21);
        Health += repairAmount;
        if (Wood > 0 )
        {
            Wood -= 1;
            if (Health > MaxHealth) {
                Health = MaxHealth;
            }
            Console.WriteLine($"-------------------------------------------------------\nHealth: {Health}/{MaxHealth}\n-------------------------------------------------------");
            Console.WriteLine($"-------------------------------------------------------\nWood: {Wood}\n-------------------------------------------------------");
        }
        else {
            Console.WriteLine ("-------------------------------------------------------\nYou do not have enough wood to repair\n-------------------------------------------------------");
        }
    }

    public void Treasure() {
        Console.Clear();
        float treasureAmount = random.Next(10, 21);
        if (MaxCargo - Cargo < treasureAmount) {
        Cargo = MaxCargo;
        Console.WriteLine($"----------------------------------------------\n{treasureAmount} gold found\n----------------------------------------------\nYou could not carry all of the gold (upgrade ship to increase)\n----------------------------------------------\nGold: {Cargo}/{MaxCargo} gold\n----------------------------------------------");            
        }
        if (MaxCargo - Cargo >= treasureAmount) {
            Cargo += treasureAmount;
            Console.WriteLine($"----------------------------------------------\n{treasureAmount} gold found\n----------------------------------------------\nGold: {Cargo}/{MaxCargo} gold\n----------------------------------------------");        
        }
    }
    public void Stolen() {
        float stolenAmount = random.Next(0, 250);
        int stolenCurseBalls = random.Next(1,3);
        CursedCannonBalls += stolenCurseBalls;
        if (MaxCargo - Cargo < stolenAmount) {                                                                                                                                                        
            Console.WriteLine($"---------------------------------------------------------\n Enemy ship has been defeated! \n---------------------------------------------------------\n You sank the enemy ship and found {stolenAmount} gold\n---------------------------------------------------------\n You could not carry all of it and stole {MaxCargo - Cargo} gold (upgrade ship to increase)\n---------------------------------------------------------\n You now have {MaxCargo}/{MaxCargo} gold \n---------------------------------------------------------\n You found {stolenCurseBalls} cursed cannonballs\n---------------------------------------------------------\n You now have {CursedCannonBalls} cursed cannonballs \n Health {Health}/{MaxHealth}\n---------------------------------------------------------");
            Cargo = MaxCargo;
        }
        if (MaxCargo - Cargo >= stolenAmount) {
            Cargo += stolenAmount;
            Console.WriteLine($"---------------------------------------------------------\n Enemy ship has been defeated! \n---------------------------------------------------------\n You sank the enemy ship and found {stolenAmount} gold and stole it\n---------------------------------------------------------\n You now have {Cargo}/{MaxCargo} gold \n---------------------------------------------------------\n You found {stolenCurseBalls} cursed cannonballs\n---------------------------------------------------------\n You now have {CursedCannonBalls} cursed cannonballs \n---------------------------------------------------------\n Health {Health}/{MaxHealth}\n---------------------------------------------------------");
        }
    }
    public void Depot() {
        Bank += Cargo;
        Console.WriteLine($"----------------------------------------------\n{Cargo} Deposited in bank\n----------------------------------------------");
        Console.WriteLine($"----------------------------------------------\nYou now have {Bank} gold in the bank\n----------------------------------------------");
    }
    public void EnemyRepair() {
        int chance = random.Next(1, 11);
        if (chance > 7) {
            float repairAmount = random.Next(10, 21);
            Health += repairAmount;
            if (Health > MaxHealth) {
                Health = MaxHealth;
            }
            Console.WriteLine($"------------------------------------------------\nThe enemy ship repaired itself to {Health}/{MaxHealth}\n------------------------------------------------");
        }
    }
}
public class PirateGame {
    bool menuanimationcancel = false;
    private Ship playerShip;
    private Ship enemyShip;
    public PirateGame() {
        playerShip = new Ship("Player Ship", 5, 50, 0, 100, 0, 0, 50, 10, 0, 0, 500, 100, 0, 50);
        enemyShip = new Ship("Enemy Ship", new Random().Next(1, 6), new Random().Next(10, 51), 0, new Random().Next(75, 151), 0, new Random().Next(1, 6), 50, 10, new Random().Next(10, 51), 0, 0, 0, 0, 0);
    }
    public async void StartGameAnimation() {
        while (!menuanimationcancel) { 
            Console.Clear();
            Console.WriteLine("                  |> \n             |    |    | \n            )_)  )_)  )_)   \n           )___))___))___)\\ \n          )____)____)_____)\\ \n        _____|____|____|____\\____ \n--------\\                  /---------\n^^^^^ ^^^^^^^  ^^^^^^^^^     ^^^^ \n^^^^      ^^^^     ^^    ^^\n      ^^^      ^^^ \n \n \n \n    Press any key to continue\n       Press 'esc' to exit");
            await Task.Delay(1500);
            if (menuanimationcancel) { 
                break;
            }
            Console.Clear();
            Console.WriteLine("                   |>\n              |    |    | \n             )_)  )_)  )_)   \n            )___))___))___)\\ \n           )____)____)_____)\\ \n         _____|____|____|____\\____\n---------\\                  /---------\n^^^^^ ^^^^^  ^^^^^^   ^^^^^^^\n^^^^      ^^^    ^^^    ^^\n      ^^^^   ^^   ^^^ \n \n \n \n    Press any key to continue\n       Press 'esc' to exit");
            await Task.Delay(1500);
            if (menuanimationcancel) {
                break;
            }
            Console.Clear();
            Console.WriteLine("                    |> \n               |    |    | \n              )_)  )_)  )_)   \n             )___))___))___)\\ \n            )____)____)_____)\\ \n          _____|____|____|____\\____\n----------\\                  /---------\n^^^^^ ^^^^^   ^^^^^^ ^^^^   ^^^^^^\n^^^^      ^^^     ^^^    ^^\n      ^^^      ^^^  \n \n \n \n    Press any key to continue\n       Press 'esc' to exit");
            await Task.Delay(1500);
            if (menuanimationcancel) {
                break;
            }
            Console.Clear();
            Console.WriteLine("                   |>\n              |    |    | \n             )_)  )_)  )_)   \n            )___))___))___)\\ \n           )____)____)_____)\\ \n         _____|____|____|____\\____\n---------\\                  /---------\n^^^^^ ^^^^^  ^^^^^^^^        ^^^^^\n^^^^      ^^^    ^^^    ^^\n      ^^^^   ^^   ^^^^^^^^  ^^^ \n \n \n \n    Press any key to continue\n       Press 'esc' to exit");
            await Task.Delay(1500); 
            if (menuanimationcancel) {
                break;
            }
            Console.Clear();
            Console.WriteLine("                  |> \n             |    |    | \n            )_)  )_)  )_)   \n           )___))___))___)\\ \n          )____)____)_____)\\ \n        _____|____|____|____\\____ \n--------\\                  /--------- \n^^^^^ ^^^^^^^  ^^^^^^^^^     ^^^^ \n^^^^      ^^^^     ^^    ^^ \n      ^^^      ^^^ \n \n \n \n    Press any key to continue \n       Press 'esc' to exit ");
            await Task.Delay(1500);
            if (menuanimationcancel) {
                break;
            }     
            Console.Clear();
            Console.WriteLine("                 |> \n            |    |    | \n           )_)  )_)  )_)   \n          )___))___))___)\\ \n         )____)____)_____)\\ \n       _____|____|____|____\\____ \n-------\\                  /---------\n^      ^^^^ ^^^^^^^  ^^^^^^^^^     ^^^^ \n^^^^      ^   ^^^     ^^    ^^ \n      ^^^         ^^^ \n \n \n \n    Press any key to continue \n       Press 'esc' to exit ");
            await Task.Delay(1500);
            if (menuanimationcancel) {
                break;
            }       
        }
    }
    public void Start() {   
        menuanimationcancel = false;
        StartGameAnimation();
        while (true) {   
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
            if (keyInfo.Key == ConsoleKey.Escape) {
                Environment.Exit(0);
            }
            else {
            menuanimationcancel = true;
            Console.Clear();
            EnterNameMenu();
            }
        }
    void EnterNameMenu() {
        Console.WriteLine("Enter your name");
        while (true) {
            string name = Console.ReadLine();
                 if (string.IsNullOrEmpty(name)) {
                        Console.Clear();
                        Console.WriteLine("Please enter a name");
                    }
                else {
                       Console.Clear();  
                       playerShip.Name = name;
                       StartMenu(); 
                    }
        }
    }
    void StartMenu() {
        Console.WriteLine(" \n              |    |    | \n             )_)  )_)  )_)   \n            )___))___))___)\\ \n           )____)____)_____)\\ \n         _____|____|____|____\\____\n---------\\                  /---------------------------\n^^^^^ ^^^^^^^^^         ^^^^^^^^^^^^^     ^^^^^^^\n^^^^      ^^^^     ^^^           ^^^^^^^^^^^^^^^^  ^^\n      ^^^^   ^^^^^^^^^^^^^^^^^^^   ^^^ \n \n \n \n-------------------------------------------------------\n1. Leave Outpost \n2. Shop \n3. Deposit gold\n4. Quit\n-------------------------------------------------------");
        while (true) {
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
            StartMenuHandler(keyInfo.KeyChar);
        }
    }
    void StartMenuHandler(char choice) {
        switch (choice) {
            case '1':
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
    void OutofPortMenu() {
        if (playerShip.Health <= 0) {
            Console.Clear();
            Console.WriteLine("----------------------------------------------\n    Your ship was destroyed. Game over!\n----------------------------------------------");
            Environment.Exit(1);
        }
        Console.WriteLine("\n              |    |    | \n             )_)  )_)  )_)   \n            )___))___))___)\\ \n           )____)____)_____)\\ \n         _____|____|____|____\\____\n---------\\                  /---------------------------\n^^^^^ ^^^^^^^^^^^ ^^^^^^^^^^^^  ^^^^^^^^^^\n^^^^      ^^^^  ^^^^^^^^^^^^^^^^^^^^^^   ^^^    ^^\n      ^^^^ ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^     ^^^ \n \n \n \n-------------------------------------------------------\n1. Attack Ship \n2. Repair Ship \n3. Search for treausre\n4. Go back to outpost\n-------------------------------------------------------");
        while (true) {
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
            OutofPortChoiceHandler(keyInfo.KeyChar);
        }
    }
    void OutofPortChoiceHandler(char choice) {
        switch (choice) {
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
                int chance = new Random().Next(1,20);
                Console.Clear();
                if (chance <= 2) {
                    enemyShip.MaxHealth = new Random().Next(75, 151); 
                    enemyShip.Health = enemyShip.MaxHealth;
                    Console.WriteLine("-------------------------------------------------------\nAn enemy ship found you\n-------------------------------------------------------");
                    enemyShip.Assault(playerShip);
                    FightMenu();
                    break;
                }
                else {
                playerShip.Treasure();
                OutofPortMenu();
                break;
                }

            case '4':
                Console.Clear();
                StartMenu();
                break;
            case (char)ConsoleKey.Escape:
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
    void AttackMenu() {
        Console.WriteLine($"-------------------------------------------------------\nEnemy ship health: {enemyShip.Health}\nYour current health: {playerShip.Health}\nDo you want to fight this ship?\n-------------------------------------------------------\n1. Fight ship\n2. No\n-------------------------------------------------------"); 
        while (true) {
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
                AttackChoiceHandler(keyInfo.KeyChar);
            }
        }
    void AttackChoiceHandler(char choice) {
        switch (choice) {
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
    void FightMenu() {
        if (enemyShip.Health > 30) {
            if (playerShip.CursedCannonBalls > 0){
                Console.WriteLine($"-------------------------------------------------------\nCannonballs left {playerShip.Cannonballs}\n-------------------------------------------------------\nCurse cannon balls left {playerShip.CursedCannonBalls}\n-------------------------------------------------------\nEnemy ship health: {enemyShip.Health}/{enemyShip.MaxHealth}\n-------------------------------------------------------\nYour current health: {playerShip.Health}/{playerShip.MaxHealth}\n-------------------------------------------------------\n1. Shoot cannons\n2. Shoot cursed cannon ball\n3. Repair your ship\n4. Leave fight\n-------------------------------------------------------");      
            }     
            else{
                Console.WriteLine($"-------------------------------------------------------\nCannonballs left {playerShip.Cannonballs}\n-------------------------------------------------------\nEnemy ship health: {enemyShip.Health}/{enemyShip.MaxHealth}\n-------------------------------------------------------\nYour current health: {playerShip.Health}/{playerShip.MaxHealth}\n-------------------------------------------------------\n1. Shoot cannons\n2. Repair your ship\n3. Leave fight\n-------------------------------------------------------");
            }
        }
        if (enemyShip.Health <= 30) {
            if (playerShip.CursedCannonBalls > 0) {
                Console.WriteLine($"-------------------------------------------------------\nCannonballs left {playerShip.Cannonballs}\n-------------------------------------------------------\nCurse cannon balls left {playerShip.CursedCannonBalls}\n-------------------------------------------------------\nEnemy ship health: {enemyShip.Health}/{enemyShip.MaxHealth}\n-------------------------------------------------------\nYour current health: {playerShip.Health}/{playerShip.MaxHealth}\n-------------------------------------------------------\n1. Shoot cannons\n2. Shoot cursed cannon ball\n3. Repair your ship\n4. Board Ship\n5. Leave fight\n-------------------------------------------------------");
            }
            else {
                Console.WriteLine($"-------------------------------------------------------\nCannonballs left {playerShip.Cannonballs}\n-------------------------------------------------------\nEnemy ship health: {enemyShip.Health}/{enemyShip.MaxHealth}\n-------------------------------------------------------\nYour current health: {playerShip.Health}/{playerShip.MaxHealth}\n-------------------------------------------------------\n1. Shoot cannons\n2. Repair your ship\n3. Board Ship\n4. Leave fight\n-------------------------------------------------------");
            }
        }
        if (playerShip.Health <= 0) {   
            Console.Clear();
            Console.WriteLine("----------------------------------------------\n    Your ship was destroyed. Game over!\n----------------------------------------------");
            Environment.Exit(1);
        }   
        if (enemyShip.Health <= 0) {
            Console.Clear();
            playerShip.Stolen();
            OutofPortMenu();
            return;
        }
        while (true) {
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
            FightChoiceHandler(keyInfo.KeyChar);
        }
    }
    void FightChoiceHandler(char choice) {
        switch (choice) {
            case '1':
                Console.Clear();
                playerShip.Attack(enemyShip);
                enemyShip.Assault(playerShip);
                FightMenu();
                break;
            case '2':
                if (playerShip.CursedCannonBalls > 0){
                    Console.Clear();
                    playerShip.CursedBallAttack(enemyShip);
                    enemyShip.Assault(playerShip);
                    FightMenu();
                }
                else {
                    Console.Clear();
                    playerShip.Repair();
                    enemyShip.EnemyRepair();
                }
                FightMenu();
                break;
            case '3':
                if (playerShip.CursedCannonBalls > 0 && enemyShip.Health > 30) {
                    Console.Clear();
                    playerShip.Repair();
                    enemyShip.EnemyRepair();
                    FightMenu();
                }
                if (playerShip.CursedCannonBalls > 0 && enemyShip.Health <= 30) {
                    Console.Clear();
                    playerShip.Repair();
                    enemyShip.EnemyRepair();
                    FightMenu();
                    break;
                }
                if (playerShip.CursedCannonBalls <= 0 && enemyShip.Health > 30) {
                    Console.Clear();
                    LeaveFightMenu();
                    break;
                }
                if (playerShip.CursedCannonBalls <= 0 && enemyShip.Health <= 30) {
                    Console.Clear();
                    playerShip.BoardChance(enemyShip);
                    OutofPortMenu();
                    break;
                } 
                break;
            case '4':
                if (playerShip.CursedCannonBalls > 0 && enemyShip.Health > 30) {
                    Console.Clear();
                    LeaveFightMenu();
                    break;
                }
                if (playerShip.CursedCannonBalls > 0 && enemyShip.Health <= 30) {
                    Console.Clear();
                    playerShip.BoardChance(enemyShip);
                    OutofPortMenu();
                    break;
                }
                if (playerShip.CursedCannonBalls <= 0 && enemyShip.Health > 30) {
                    Console.Clear();
                    Console.WriteLine("-----------------------------\n  Invalid choice. Try again.\n-----------------------------");
                    FightMenu();
                    break;
                }
                if (playerShip.CursedCannonBalls <= 0 && enemyShip.Health <= 30) {
                    Console.Clear();
                    LeaveFightMenu();
                    break;
                }
                break;
            case '5':
             if (playerShip.CursedCannonBalls < 0 && enemyShip.Health <= 30){
                Console.Clear();
                LeaveFightMenu();
                break;
             }
            else {
                Console.Clear();
                Console.WriteLine("-----------------------------\n  Invalid choice. Try again.\n-----------------------------");
                FightMenu();
                break; 
            }
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
    void LeaveFightMenu() {
        Console.WriteLine("-------------------------------------------------------\nAre you sure you want to leave fight?\n-------------------------------------------------------\n1. Yes\n2. No\n-------------------------------------------------------");
        while (true) {
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
            LeaveFightChoiceHandler(keyInfo.KeyChar);
        }
    }
    void LeaveFightChoiceHandler(char choice) {
        switch (choice) {
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
    void ShopMenu() {
        while (true) {
            Console.WriteLine($"Welcome to the shop, {playerShip.Name}");  
            Console.WriteLine($"--------------------------------------------------------------------------------------------------------------------------------------------------\n Stats: Health: {playerShip.Health}/{playerShip.MaxHealth} | Cannonballs: {playerShip.Cannonballs} | Cursed cannonballs: {playerShip.CursedCannonBalls} | Wood: {playerShip.Wood} | Cannons: {playerShip.Cannons}/{playerShip.MaxCannons} | Crew: {playerShip.Crew}/{playerShip.MaxCrew} | Gold: {playerShip.Bank} | Captured ships: {playerShip.Items} \n--------------------------------------------------------------------------------------------------------------------------------------------------\n------------------------------------------------------\nI       1. Buy connonballs (100 coins)               I\nI       2. Buy cursed cannonballs (300 coins)        I\nI       3. Buy wood (100) coins                      I\nI       4. Buy cannons (1000 coins)                  I\nI       5. Hire crew members (100 coins)             I\nI       6. Upgrade ship (5000 coins)                 I\nI       7. Sell captured ship (1000 coins)           I\nI       8. Leave shop                                I\n------------------------------------------------------");
            while (true) {
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
                ShopChoiceHandler(keyInfo.KeyChar);
            }
        }
    }
    void ShopChoiceHandler(char choice) {
        switch (choice) {
             case '1':
                Console.Clear();
                if (playerShip.Bank < 100) {                              
                    Console.WriteLine("-------------------------------\n You don't have enough coins \n-------------------------------\n \n \n");
                }
                else {
                    playerShip.Bank -= 100;
                    playerShip.Cannonballs += 10;                             
                    Console.WriteLine($"-----------------------------------------------\n You now have {playerShip.Cannonballs} cannonballs \n-----------------------------------------------\n \n \n");
                }
                ShopMenu();
                break;
            case '2':
                Console.Clear();
                if (playerShip.Bank < 300) {
                    Console.WriteLine("-------------------------------\n You don't have enough coins \n-------------------------------\n \n \n");
                }
                else {
                    playerShip.Bank -= 300;
                    playerShip.CursedCannonBalls += 2;
                    Console.WriteLine($"-----------------------------------------------\n You now have {playerShip.CursedCannonBalls} cursed cannonballs \n-----------------------------------------------\n \n \n");
                }
                ShopMenu();
                break;
            case '3':
                Console.Clear();
                if (playerShip.Bank < 100) {
                    Console.WriteLine("-------------------------------\n You don't have enough coins \n-------------------------------\n \n \n");
                }
                else {
                    playerShip.Bank -= 100;
                    playerShip.Wood += 10;
                    Console.WriteLine($"-----------------------------------------------\n You now have {playerShip.Wood} wood \n-----------------------------------------------\n \n \n");
                }
                ShopMenu();
                break;
            case '4':
                Console.Clear();
                if (playerShip.Bank < 1000) {
                    Console.WriteLine("-------------------------------\n you don't have enough coins \n-------------------------------\n \n \n");
                }
                else if (playerShip.Cannons < playerShip.MaxCannons) {
                    playerShip.Bank -= 1000;
                    playerShip.Cannons += 1;         
                    Console.WriteLine($"---------------------------------------------\n You now have {playerShip.Cannons} cannons \n---------------------------------------------\n \n \n");
                }
                else {
                    Console.WriteLine("----------------------------------------------------------------------\n You have reached your max cannons amount (upgrade ship to increase) \n----------------------------------------------------------------------\n \n \n");
                }
                ShopMenu();
                break;
            case '5':
                Console.Clear();
                if (playerShip.Bank < 100) {                              
                    Console.WriteLine("-------------------------------\n You don't have enough coins \n-------------------------------\n \n \n");
                }
                else if (playerShip.Crew < playerShip.MaxCrew) {
                    playerShip.Bank -= 100;
                    playerShip.Crew += 10;
                    if (playerShip.Crew > playerShip.MaxCrew) {
                        playerShip.Crew = playerShip.MaxCrew;
                    }                                
                    Console.WriteLine($"-----------------------------------------------\n You now have {playerShip.Crew} crew members \n-----------------------------------------------\n \n \n");
                }
                else {
                    Console.WriteLine("--------------------------------------------------------------------\n  You have reached your max crew amount (upgrade ship to increase) \n--------------------------------------------------------------------\n \n \n");
                }
                ShopMenu();
                break;
            case '6':
                Console.Clear();
                if (playerShip.Bank < 5000) {                              
                    Console.WriteLine("-------------------------------\n You don't have enough coins \n-------------------------------\n \n \n");
                }
                else {
                    playerShip.Bank -= 5000;
                    playerShip.MaxHealth += 50;
                    playerShip.MaxCrew += 50;
                    playerShip.MaxCannons += 5;   
                    playerShip.MaxCargo += 500;   
                    Console.WriteLine($"---------------------------------------------------------------------------------------------------------------------\n  Upgraded ship: Health to {playerShip.MaxHealth}, Crew to {playerShip.MaxCrew}, Cannons to {playerShip.MaxCannons}, Max cargo to {playerShip.MaxCargo} \n---------------------------------------------------------------------------------------------------------------------\n \n \n");
                }
                ShopMenu();
                break;
            case '7':
                Console.Clear();
                if (playerShip.Items < 1) {                                                                     
                    Console.WriteLine("-------------------------------------\n You don't have any captured ships \n-------------------------------------\n \n \n");
                }
                else {
                    playerShip.Items -= 1;
                    playerShip.Bank += 1000;        
                    Console.WriteLine("---------------------------------\n You have sold a captured ship \n---------------------------------\n \n \n");
                }
                ShopMenu();
                break;
            case '8':
                Console.Clear();
                StartMenu();
                break;
            case (char)ConsoleKey.Escape:
                Console.Clear();
                StartMenu();
                break;
            default:
                Console.Clear();                
                Console.WriteLine("------------------------------\n Invalid choice. Try again. \n------------------------------\n \n \n");
                ShopMenu();
                break;
        }
    }
    void QuitMenu() {
        Console.WriteLine("-------------------------------------------------------\nAre you sure you want to quit?\n-------------------------------------------------------\n1. Yes\n2. No\n-------------------------------------------------------");
        while (true) {
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
            QuitChoiceHandler(keyInfo.KeyChar);
        }
    }
    void QuitChoiceHandler(char choice) {
        switch (choice) {
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
    static void Main(string[] args) {
        PirateGame game = new PirateGame();
        game.Start();
    }
}