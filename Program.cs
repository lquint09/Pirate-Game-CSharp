public class Ship {
    public string Name {get; set;} 
    public int Cannons {get; set;} = 5;
    public int Crew {get; set;} = 50;
    public int MaxCrew {get; set;} = 50;
    public int MaxCannons {get; set;} = 10;
    public float Bank {get; set;} = 0;
    public float Health {get; set;} = 100;
    public float MaxHealth {get; set;} = 100;
    public int Items {get; set;} = 0;
    public int Chance {get; set;} 
    public int EnemyCrew {get; set;} 
    public float Cargo {get; set;} = 0;
    public float MaxCargo {get; set;} = 500;
    public int Cannonballs {get; set;} = 100;
    public int CursedCannonBalls {get; set;}
    public int Wood {get; set;} = 50;
    public float PlayerAttackMinDamage { get; set; } = 10.0f;
    public float PlayerAttackMaxDamage { get; set; } = 20.0f;
    public int PlayerAttackAccuracyChance { get; set; } = 10;
    public float EnemyAttackMinDamage { get; set; } = 5.0f;
    public float EnemyAttackMaxDamage { get; set; } = 15.0f;
    public int EnemyAttackAccuracyChance { get; set; } = 3; 
    public float CursedBallMinDamage { get; set; } = 15;
    public float CursedBallMaxDamage { get; set; } = 40;
    public int boardChance { get; set; } = 3;
    public float PlayerRepairMinAmount { get; set; } = 10.0f;
    public float PlayerRepairMaxAmount { get; set; } = 20.0f;
    public int TreasureMinAmount { get; set; } = 10;
    public int TreasureMaxAmount { get; set; } = 20;
    public int treasureChance; 
    public int StolenMinAmount { get; set; } = 75;
    public int StolenMaxAmount { get; set; } = 150;
    public int EnemyCrewMin {get; set;} = 25;
    public int EnemyCrewMax {get; set;} = 50;
    public int stolenCurseBallsMin {get; set;} = 1;
    public int stolenCurseBallsMax {get; set;} = 3;
    public int enemyRepairMin {get; set;} = 10;
    public int enemyRepairMax {get; set;} = 21;
    public int enemyRepairChance {get; set;} = 4;
    public int TreasureChance {get; set;} = 6; 
    private static readonly Random random = new Random();

    public Ship(string name, int cannons, int crew, int bank, int maxhealth, int inventoryItems, int chance, int maxcrew, int maxcannons, int cargo, int maxcargo, int cannonballs, int cursedcannonballs, int wood) {
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
        Cargo = cargo;
        MaxCargo = maxcargo;
        Cannonballs = cannonballs;
        CursedCannonBalls = cursedcannonballs;
        Wood = wood;
    }
    public void PlayerAttack(Ship target) {
        int chance = random.Next(1, 5);
        Cannonballs -= Cannons;
        if (chance >= PlayerAttackAccuracyChance) {
            float damage = (float)(random.NextDouble() * (PlayerAttackMaxDamage - PlayerAttackMinDamage) + PlayerAttackMinDamage) + Cannons;
            damage = (float)Math.Round(damage, 1);
            target.Health -= damage;
            target.Health = Math.Max(target.Health, 0);
        }
        else {
            Console.WriteLine("-------------\n You missed\n-------------");
        }
    }
    public void EnemyAttack(Ship target) {
        int chance = random.Next(1, 5);
        if (chance >= EnemyAttackAccuracyChance) {
            float damage = (float)(random.NextDouble() * (EnemyAttackMaxDamage - EnemyAttackMinDamage) + EnemyAttackMinDamage) + Cannons;
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
        float damage = (float)(random.NextDouble()* (CursedBallMaxDamage - CursedBallMinDamage)+ CursedBallMinDamage) + Cannons;
        damage = (float)Math.Round(damage, 1);
        target.Health -= damage;
        target.Health = Math.Max(target.Health, 0);
    }
    public void BoardChance(Ship target) {
        Chance = random.Next(1,5);
        EnemyCrew = random.Next(EnemyCrewMin,EnemyCrewMax);
        if (target.Health < 31) {
            if (Crew > EnemyCrew) {
                if (Chance >= boardChance) {
                    Items += 1;
                    Crew = Math.Max(Crew - 5, 0);
                    Console.Clear();
                    Console.WriteLine($"----------------------------------------------\n You won the board\n----------------------------------------------\n You now have {Items} captured ships\n----------------------------------------------\n Crew {Crew}/{MaxCrew}\n----------------------------------------------");
                    target.Health = 0;
                }
                else {
                    Crew = 25;
                    Health = 1;
                    Console.Clear();
                    Console.WriteLine($"----------------------------------------------\n You lost the board\n----------------------------------------------\n Crew: {Crew}/{MaxCrew}\n----------------------------------------------\n Health: {Health}/{MaxHealth}\n----------------------------------------------");
                    return;
                }
            }
        }
    }
    public void Repair() {
        float repairAmount = (float)(random.NextDouble() * (PlayerRepairMaxAmount - PlayerRepairMinAmount) + PlayerRepairMinAmount);
        Health += repairAmount;
        if (Wood > 0 && Health < MaxHealth) {
            if (repairAmount > 15) {
                Wood -= random.Next(1,2);
            }
            if (repairAmount <= 15) {
                Wood -= random.Next(3, 5);
            }
        }
        if (Wood <= 0 && Health < MaxHealth) {
            Console.WriteLine ("-------------------------------------------------------\n You do not have enough wood to repair\n-------------------------------------------------------");
        }
        if (Health > MaxHealth) {
            Console.WriteLine("-------------------------------------------------------\n Nothing to repair\n-------------------------------------------------------");
            Health = MaxHealth;
        }
        Console.WriteLine($"-------------------------------------------------------\n Health: {Health}/{MaxHealth}\n-------------------------------------------------------");
        Console.WriteLine($"-------------------------------------------------------\n Wood: {Wood}\n-------------------------------------------------------");
    }
    public void Treasure() {
        Console.Clear();
        treasureChance = random.Next(1, 10);
        if (treasureChance >= TreasureChance) {
            float treasureAmount = random.Next(TreasureMinAmount, TreasureMaxAmount);
            if (MaxCargo - Cargo < treasureAmount) {
            Cargo = MaxCargo;
            Console.WriteLine($"----------------------------------------------\n {treasureAmount} gold found\n----------------------------------------------\n You could not carry all of the gold (upgrade ship to increase)\n----------------------------------------------\n Gold: {Cargo}/{MaxCargo} gold\n----------------------------------------------");            
            }
            if (MaxCargo - Cargo >= treasureAmount) {
                Cargo += treasureAmount;
                Console.WriteLine($"----------------------------------------------\n {treasureAmount} gold found\n----------------------------------------------\n Gold: {Cargo}/{MaxCargo} gold\n----------------------------------------------");        
            }
        }
        else {
            Console.WriteLine("----------------------------------------------\n You did not find any treasure\n----------------------------------------------");
        }
    }
    public void Stolen() {
        int stolenAmount = random.Next(StolenMinAmount, StolenMaxAmount);
        int stolenCurseBalls = random.Next(stolenCurseBallsMin,stolenCurseBallsMax);
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
        Cargo = 0;
        Console.WriteLine($"----------------------------------------------\n {Cargo} Deposited in bank\n----------------------------------------------");
        Console.WriteLine($"----------------------------------------------\n You now have {Bank} gold in the bank\n----------------------------------------------");
    }
    public void EnemyRepair() {
        int chance = random.Next(1, 10);
        if (chance > enemyRepairChance) {
            float repairAmount = random.Next(enemyRepairMin, enemyRepairMax);
            Health += repairAmount;
            if (Health > MaxHealth) {
                Health = MaxHealth;
            }
            Console.WriteLine($"------------------------------------------------\n The enemy ship repaired itself to {Health}/{MaxHealth}\n------------------------------------------------");
        }
    }
}
public class PirateGame {
    bool menuanimationcancel = false;
    private Ship playerShip;
    private Ship enemyShip;
    public PirateGame() {
        playerShip = new Ship("Player Ship", 5, 50, 0, 100, 0, 0, 50, 10, 0, 500, 100, 0, 50);
        enemyShip = new Ship("Enemy Ship", new Random().Next(1, 6), new Random().Next(10, 51), 0, new Random().Next(75, 151), 0, new Random().Next(1, 6), 50, 10, 0, 0, 0, 0, 0);
    }
public async void StartGameAnimation()
{
    var frames = new[]
    {
        "                  |> \n             |    |    | \n            )_)  )_)  )_)   \n           )___))___))___)\\ \n          )____)____)_____)\\ \n        _____|____|____|____\\____ \n--------\\                  /---------\n^^^^^ ^^^^^^^  ^^^^^^^^^     ^^^^ \n^^^^      ^^^^     ^^    ^^\n      ^^^      ^^^ \n \n \n \n    Press any key to continue\n       Press 'esc' to exit",
        "                   |>\n              |    |    | \n             )_)  )_)  )_)   \n            )___))___))___)\\ \n           )____)____)_____)\\ \n         _____|____|____|____\\____\n---------\\                  /---------\n^^^^^ ^^^^^  ^^^^^^   ^^^^^^^\n^^^^      ^^^    ^^^    ^^\n      ^^^^   ^^   ^^^ \n \n \n \n    Press any key to continue\n       Press 'esc' to exit",
        "                  |> \n             |    |    | \n            )_)  )_)  )_)   \n           )___))___))___)\\ \n          )____)____)_____)\\ \n        _____|____|____|____\\____ \n--------\\                  /---------\n^^^^^ ^^^^^^^  ^^^^^^^^^     ^^^^ \n^^^^      ^^^^     ^^    ^^\n      ^^^      ^^^ \n \n \n \n    Press any key to continue\n       Press 'esc' to exit",
        "                    |> \n               |    |    | \n              )_)  )_)  )_)   \n             )___))___))___)\\ \n            )____)____)_____)\\ \n          _____|____|____|____\\____\n---------\\                  /---------\n^^^^^ ^^^^^   ^^^^^^ ^^^^   ^^^^^^\n^^^^      ^^^     ^^^    ^^\n      ^^^      ^^^  \n \n \n \n    Press any key to continue\n       Press 'esc' to exit",
        "                   |>\n              |    |    | \n             )_)  )_)  )_)   \n            )___))___))___)\\ \n           )____)____)_____)\\ \n         _____|____|____|____\\____\n---------\\                  /---------\n^^^^^ ^^^^^  ^^^^^^^^        ^^^^^\n^^^^      ^^^    ^^^    ^^\n      ^^^^   ^^   ^^^^^^^^  ^^^ \n \n \n \n    Press any key to continue\n       Press 'esc' to exit",
        "                  |> \n             |    |    | \n            )_)  )_)  )_)   \n           )___))___))___)\\ \n          )____)____)_____)\\ \n        _____|____|____|____\\____ \n--------\\                  /--------- \n^^^^^ ^^^^^^^  ^^^^^^^^^     ^^^^ \n^^^^      ^^^^     ^^    ^^ \n      ^^^      ^^^ \n \n \n \n    Press any key to continue \n       Press 'esc' to exit ",
        "                 |> \n            |    |    | \n           )_)  )_)  )_)   \n          )___))___))___)\\ \n         )____)____)_____)\\ \n       _____|____|____|____\\____ \n-------\\                  /---------\n^      ^^^^ ^^^^^^^  ^^^^^^^^^     ^^^^ \n^^^^      ^   ^^^     ^^    ^^ \n      ^^^         ^^^ \n \n \n \n    Press any key to continue \n       Press 'esc' to exit "
    };

    while (!menuanimationcancel)
    {
        foreach (var frame in frames)
        {
            Console.Clear();
            Console.WriteLine(frame);
            await Task.Delay(1000);
            if (menuanimationcancel)
            {
                break;
            }
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
                if (name == "devtools") {
                    playerShip.Name = name;
                    Console.Clear();
                    DevToolsStartMenu();
                }
                else {
                    Console.Clear();
                    #pragma warning disable CS8601 // Possible null reference assignment.
                    playerShip.Name = name;
                    StartMenu(); 
                    }
            }
        }
//--------------------------------------------
// Start of Devtools code
//--------------------------------------------       
    void DevToolsStartMenu() {
        Console.WriteLine($"Hello, {playerShip.Name}, what values would you like to edit? \n------------------------------------------------------------------");
        string requestedField = Console.ReadLine()?.ToLower();

        if (string.IsNullOrEmpty(requestedField)) {
            Console.WriteLine("Please enter a value; type 'none' to leave or type 'list' to see all editable values.");
            DevToolsStartMenu();
        }

        switch (requestedField) {
            case "list":
                ShowEditableFields();
                break;
            case "help":
                ShowHelp();
                break;
            case "clear":
                Console.Clear();
                break;
            case "none":
                Console.Clear();
                    StartMenu();
                return;
            default:
                #pragma warning disable CS8604 // Possible null reference argument.
                HandleValueEdit(requestedField);
                return;                                              
        }
        DevToolsStartMenu(); // Call the menu again to keep the flow
    }
    void ShowHelp() {
        Console.Clear();
        Console.WriteLine("List of all Commands\n------------------------------------------------------------------\n list \n help \n clear \n none \n------------------------------------------------------------------");
    }
    void ShowEditableFields() {
        Console.Clear();
        Console.WriteLine("Editable values\n------------------------------------------------------------------\ncannons\ncrew\nbank\nhealth\nitems\ncannonballs\ncursedballs\nwood\nplayer-attack-damage-min\nplayer-attack-damage-max\nplayer-attack-accuracy-chance\nenemy-repair-chance-max\nenemy-repair-chance-min\nenemy-attack-damage-min\nenemy-attack-damage-max\nenemy-attack-accuracy-chance-min\ncursedball-attack-damage-min\ncursedball-attack-damage-max\nenemy-attack-accuracy-chance-max\nboard-chance-min\nboard-chance-max\nenemy-crew-min\nenemy-crew-max\nplayer-repair-min-amount\nplayer-repair-max-amount\nenemy-repair-min\nenemy-repair-max\ntreasure-min-amount\ntreasure-max-amount\ntreasure-chance-min\ntreasure-chance-max\nstolen-min-amount\nstolen-max-amount\nstolen-cursedball-min\nsotlen-cursedball-max\n------------------------------------------------------------------");
    }
    void HandleValueEdit(string field) {
        var fieldActions = new Dictionary<string, Action<int>> {
            { "cannons", value => playerShip.Cannons = value },
            { "crew", value => playerShip.Crew = value },
            { "bank", value => playerShip.Bank = value },
            { "health", value => playerShip.Health = value },
            { "items", value => playerShip.Items = value },
            { "cannonballs", value => playerShip.Cannonballs = value },
            { "cursedballs", value => playerShip.CursedCannonBalls = value },
            { "wood", value => playerShip.Wood = value },
            { "player-attack-accuracy-chance", value => playerShip.PlayerAttackAccuracyChance = value },
            { "enemy-attack-accuracy-chance", value => enemyShip.EnemyAttackAccuracyChance = value },
            { "board-chance", value => playerShip.boardChance = value },
            { "treasure-min-amount", value => playerShip.TreasureMinAmount = value },
            { "treasure-max-amount", value => playerShip.TreasureMaxAmount = value },
            { "stolen-min-amount", value => playerShip.StolenMinAmount = value },
            { "stolen-max-amount", value => playerShip.StolenMaxAmount = value },
            { "treasure-chance", value => playerShip.treasureChance = value },
            { "enemy-crew-min", value => playerShip.EnemyCrewMin = value },
            { "enemy-crew-max", value => playerShip.EnemyCrewMax = value},
            { "stolen-cursedball-min", value => playerShip.stolenCurseBallsMin = value},
            { "stolen-cursedball-max", value => playerShip.stolenCurseBallsMax = value},
            { "enemy-repair-chance", value => playerShip.enemyRepairChance = value},
        };
        var floatFieldActions = new Dictionary<string, Action<float>> {
            { "player-attack-damage-min", value => playerShip.PlayerAttackMinDamage = value },
            { "player-attack-damage-max", value => playerShip.PlayerAttackMaxDamage = value },
            { "enemy-attack-damage-min", value => enemyShip.EnemyAttackMinDamage = value },
            { "enemy-attack-damage-max", value => enemyShip.EnemyAttackMaxDamage = value },
            { "player-repair-min-amount", value => playerShip.PlayerRepairMinAmount = value },
            { "player-repair-max-amount", value => playerShip.PlayerRepairMaxAmount = value }
        };
        // Check if the field exists in integer or float dictionaries
        if (fieldActions.ContainsKey(field)) {
            Console.Clear();
            Console.WriteLine($"What would you like to change {field} to?");
            if (int.TryParse(Console.ReadLine(), out int newValue)) {
                fieldActions[field](newValue);
                Console.Clear();
                Console.WriteLine($"{field} has been changed to {newValue}");
                AskIfContinue();
            } else {
                Console.Clear();
                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
        } else if (floatFieldActions.ContainsKey(field)) {
            Console.Clear();
            Console.WriteLine($"What would you like to change {field} to?");
            if (float.TryParse(Console.ReadLine(), out float newValue)) {
                floatFieldActions[field](newValue);
                Console.Clear();
                Console.WriteLine($"{field} has been changed to {newValue}");
                AskIfContinue();
            } else {
                Console.Clear();
                Console.WriteLine("Invalid input. Please enter a valid float.");
            }
        } else {
            Console.Clear();
            Console.WriteLine("Invalid field. Please choose a valid option.");
        }
    }
    void AskIfContinue() {
        Console.WriteLine("Would you like to change any other values?\n-------------------------------------------------------\n 1. Yes\n 2. No\n-------------------------------------------------------");
        string continueInput = Console.ReadLine();
        if (continueInput == "1") {
            Console.Clear();
            DevToolsStartMenu();
        } 
        else if (continueInput == "2") {
            Console.Clear();
            StartMenu();
        } 
        else {
            Console.Clear();
            Console.WriteLine("Invalid input. Please enter 1 to continue or 2 to exit.");
            AskIfContinue();
        }
    }
//--------------------------------------------
// End of Devtools code
//--------------------------------------------
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
                if (chance > 19) {
                    enemyShip.MaxHealth = new Random().Next(75, 151); 
                    enemyShip.Health = enemyShip.MaxHealth;
                    Console.WriteLine("-------------------------------------------------------\n An enemy ship found you\n-------------------------------------------------------");
                    enemyShip.EnemyAttack(playerShip);
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
        Console.WriteLine($"-------------------------------------------------------\n Enemy ship health: {enemyShip.Health}\n Your current health: {playerShip.Health}\n Do you want to fight this ship?\n-------------------------------------------------------\n1. Fight ship\n2. No\n-------------------------------------------------------"); 
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
                Console.WriteLine($"-------------------------------------------------------\n Cannonballs left {playerShip.Cannonballs}\n-------------------------------------------------------\n Curse cannon balls left {playerShip.CursedCannonBalls}\n-------------------------------------------------------\n Enemy ship health: {enemyShip.Health}/{enemyShip.MaxHealth}\n-------------------------------------------------------\n Your current health: {playerShip.Health}/{playerShip.MaxHealth}\n-------------------------------------------------------\n1. Shoot cannons\n2. Shoot cursed cannon ball\n3. Repair your ship\n4. Leave fight\n-------------------------------------------------------");      
            }     
            else{
                Console.WriteLine($"-------------------------------------------------------\n Cannonballs left {playerShip.Cannonballs}\n-------------------------------------------------------\n Enemy ship health: {enemyShip.Health}/{enemyShip.MaxHealth}\n-------------------------------------------------------\n Your current health: {playerShip.Health}/{playerShip.MaxHealth}\n-------------------------------------------------------\n1. Shoot cannons\n2. Repair your ship\n3. Leave fight\n-------------------------------------------------------");
            }
        }
        if (enemyShip.Health <= 30) {
            if (playerShip.CursedCannonBalls > 0) {
                Console.WriteLine($"-------------------------------------------------------\n Cannonballs left {playerShip.Cannonballs}\n-------------------------------------------------------\n Curse cannon balls left {playerShip.CursedCannonBalls}\n-------------------------------------------------------\n Enemy ship health: {enemyShip.Health}/{enemyShip.MaxHealth}\n-------------------------------------------------------\n Your current health: {playerShip.Health}/{playerShip.MaxHealth}\n-------------------------------------------------------\n1. Shoot cannons\n2. Shoot cursed cannon ball\n3. Repair your ship\n4. Board Ship\n5. Leave fight\n-------------------------------------------------------");
            }
            else {
                Console.WriteLine($"-------------------------------------------------------\n Cannonballs left {playerShip.Cannonballs}\n-------------------------------------------------------\n Enemy ship health: {enemyShip.Health}/{enemyShip.MaxHealth}\n-------------------------------------------------------\n Your current health: {playerShip.Health}/{playerShip.MaxHealth}\n-------------------------------------------------------\n1. Shoot cannons\n2. Repair your ship\n3. Board Ship\n4. Leave fight\n-------------------------------------------------------");
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
                playerShip.PlayerAttack(enemyShip);
                enemyShip.EnemyAttack(playerShip);
                FightMenu();
                break;
            case '2':
                if (playerShip.CursedCannonBalls > 0) {
                    Console.Clear();
                    playerShip.CursedBallAttack(enemyShip);
                    enemyShip.EnemyAttack(playerShip);
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
                if (playerShip.CursedCannonBalls < 0 && enemyShip.Health <= 30) {
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
        Console.WriteLine("-------------------------------------------------------\n Are you sure you want to leave fight?\n-------------------------------------------------------\n1. Yes\n2. No\n-------------------------------------------------------");
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
        Console.WriteLine("-------------------------------------------------------\n Are you sure you want to quit?\n-------------------------------------------------------\n1. Yes\n2. No\n-------------------------------------------------------");
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