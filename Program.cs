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
    public int CursedCannonBalls {get; set;} = 0;
    public int Wood {get; set;} = 50;
    public float PlayerAttackMinDamage { get; set; } = 10.0f;
    public float PlayerAttackMaxDamage { get; set; } = 20.0f;
    public int PlayerAttackAccuracyChance { get; set; } = 2;
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
    public int treasureChance; 
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
    //-----------------------------------------
    // Player attack function
    //-----------------------------------------
    public void PlayerAttack(Ship target) {
        int chance = random.Next(1, 5); // defines if the player actually hits the cannon shot (needs a "PlayerAttackAccuracyChance" value to work.) 
        Cannonballs -= Cannons; // removes the amount of cannon balls relative to the amount of cannons the player has.
        if (chance >= PlayerAttackAccuracyChance) {
            float damage = (float)(random.NextDouble() * (PlayerAttackMaxDamage - PlayerAttackMinDamage) + PlayerAttackMinDamage) + Cannons; // damage calculation 
            damage = (float)Math.Round(damage, 1); // rounds the damage calcuations to 1 decimal 
            target.Health -= damage; // removes health from enemy ship if the cannon ball hits
            target.Health = Math.Max(target.Health, 0);
        } else {
            Console.WriteLine("-------------\n You missed\n-------------");
        }
    }
    //-----------------------------------------
    // Enemy Attack Function
    //-----------------------------------------
    public void EnemyAttack(Ship target) {
        int chance = random.Next(1, 5); // defines if the enemy ships cannons hit the player ship
        if (chance >= EnemyAttackAccuracyChance) {
            float damage = (float)(random.NextDouble() * (EnemyAttackMaxDamage - EnemyAttackMinDamage) + EnemyAttackMinDamage) + Cannons; // damage calculation
            damage = (float)Math.Round(damage, 1); // rounding to 1 decimal
            target.Health -= damage; // removes health from player ship
            target.Health = Math.Max(target.Health, 0);
        } else {
            Console.WriteLine("-------------------------\n The enemy ship missed\n-------------------------");
        }
    }
    //-----------------------------------------
    // Attack Function for cursedball usage
    //-----------------------------------------
    public void CursedBallAttack(Ship target) {
        CursedCannonBalls -= 1;
        float damage = (float)(random.NextDouble()* (CursedBallMaxDamage - CursedBallMinDamage)+ CursedBallMinDamage) + Cannons; // cursed cannonball damage calculation
        damage = (float)Math.Round(damage, 1); // rounds to 1 decimal
        target.Health -= damage;
        target.Health = Math.Max(target.Health, 0);
    }
    //-----------------------------------------
    // function to determine what happens when board attempt is a attempted
    //-----------------------------------------
    public void BoardChance(Ship target) {
        Chance = random.Next(1,5); // devines if the player can baord thie enemy ship or not
        EnemyCrew = random.Next(EnemyCrewMin,EnemyCrewMax); // generates value for eneny crew for the player crew to go against.
        if (target.Health < 31) { // makes sure that enemy ship is less than 31 (extra security level, not sure if needed)
            if (Crew > EnemyCrew) { 
                if (Chance >= boardChance) {
                    Items += 1;
                    Crew = Math.Max(Crew - 5, 0);
                    Console.Clear();
                    Console.WriteLine($"----------------------------------------------\n You won the board\n----------------------------------------------\n You now have {Items} captured ships\n----------------------------------------------\n Crew {Crew}/{MaxCrew}\n----------------------------------------------");
                    target.Health = 0; // sets enemy ship to 0 so it performs the Stolen() fucntion
                } else {
                    Crew = 25; // sets player states to very low amounts for failing to board.
                    Health = 1; // ^
                    Console.Clear();
                    Console.WriteLine($"----------------------------------------------\n You lost the board\n----------------------------------------------\n Crew: {Crew}/{MaxCrew}\n----------------------------------------------\n Health: {Health}/{MaxHealth}\n----------------------------------------------");
                    return;
                }
            }
        }
    }
    //-----------------------------------------
    // Player repair fucntion
    //-----------------------------------------
    public void Repair() {
        float repairAmount = (float)(random.NextDouble() * (PlayerRepairMaxAmount - PlayerRepairMinAmount) + PlayerRepairMinAmount); // generates the repair mount for player ship
        Health += repairAmount; // applys repair amount to ship
        if (Wood > 0 && Health < MaxHealth) { // determines if the player ship needs to be repairs and if the health goes over max health.
            if (repairAmount > 15) {  // determines if the health amount should take 1-2 or 3-5 wood to repair
                Wood -= random.Next(1,2); // takes away determined amount of wood
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
    //-----------------------------------------
    // Fucntion to randomized amount of traesure gathered
    //-----------------------------------------
    public void Treasure() {
        Console.Clear();
        treasureChance = random.Next(1, 10); // determiens if treasure should be given to the player
        if (treasureChance >= TreasureChance) { 
            float treasureAmount = random.Next(TreasureMinAmount, TreasureMaxAmount); // determines the amount of treasure given the the player
            if (MaxCargo - Cargo < treasureAmount) {  // determines if the amount of treasure given would but the player over max cargo amount
                Cargo = MaxCargo;
                Console.WriteLine($"----------------------------------------------\n {treasureAmount} gold found\n----------------------------------------------\n You could not carry all of the gold (upgrade ship to increase)\n----------------------------------------------\n Gold: {Cargo}/{MaxCargo} gold\n----------------------------------------------");            
            }
            if (MaxCargo - Cargo >= treasureAmount) {
                Cargo += treasureAmount;
                Console.WriteLine($"----------------------------------------------\n {treasureAmount} gold found\n----------------------------------------------\n Gold: {Cargo}/{MaxCargo} gold\n----------------------------------------------");        
            }
        } else {
            Console.WriteLine("----------------------------------------------\n You did not find any treasure\n----------------------------------------------");
        }
    }
    //-----------------------------------------
    // fucntion to determine what is given to players are the sink an enemy ship
    //-----------------------------------------
    public void Stolen() {
        int stolenAmount = random.Next(StolenMinAmount, StolenMaxAmount); // determines how much gold is given to player after sinking a ship
        int stolenCurseBalls = random.Next(stolenCurseBallsMin,stolenCurseBallsMax); // determiens how many cured cannon balls are given the the player after sinking a ship
        CursedCannonBalls += stolenCurseBalls; //gives the player the amount of cursed cannon balls.
        if (MaxCargo - Cargo < stolenAmount) { // makes sure player cargo does not go over player max cargo                                                                                                                                                        
            Console.WriteLine($"---------------------------------------------------------\n Enemy ship has been defeated! \n---------------------------------------------------------\n You sank the enemy ship and found {stolenAmount} gold\n---------------------------------------------------------\n You could not carry all of it and stole {MaxCargo - Cargo} gold (upgrade ship to increase)\n---------------------------------------------------------\n You now have {MaxCargo}/{MaxCargo} gold \n---------------------------------------------------------\n You found {stolenCurseBalls} cursed cannonballs\n---------------------------------------------------------\n You now have {CursedCannonBalls} cursed cannonballs \n Health {Health}/{MaxHealth}\n---------------------------------------------------------");
            Cargo = MaxCargo;
        }
        if (MaxCargo - Cargo >= stolenAmount) {
            Cargo += stolenAmount; //gives the player the amount of cargo stolen if they do not pass their cargo limit
            Console.WriteLine($"---------------------------------------------------------\n Enemy ship has been defeated! \n---------------------------------------------------------\n You sank the enemy ship and found {stolenAmount} gold and stole it\n---------------------------------------------------------\n You now have {Cargo}/{MaxCargo} gold \n---------------------------------------------------------\n You found {stolenCurseBalls} cursed cannonballs\n---------------------------------------------------------\n You now have {CursedCannonBalls} cursed cannonballs \n---------------------------------------------------------\n Health {Health}/{MaxHealth}\n---------------------------------------------------------");
        }
    }
    //-----------------------------------------
    // fucntion to deposit gold into bank
    //-----------------------------------------
    public void Depot() {
        Bank += Cargo;
        if (Cargo > 0) {
            Console.WriteLine($"----------------------------------------------\n {Cargo} gold deposited in bank\n----------------------------------------------");
            Cargo = 0;
            Console.WriteLine($"----------------------------------------------\n You now have {Bank} gold in the bank\n----------------------------------------------");
            } else {
            Console.WriteLine("----------------------------------------------\nYou do not have any gold to deposit\n----------------------------------------------");
        }
    }
    //-----------------------------------------
    // repair function for enemy ship // actived only when plyer repairs their ship
    //-----------------------------------------
    public void EnemyRepair() {
        int chance = random.Next(1, 10); // determines if the enemy ship repairs
        if (chance > enemyRepairChance) {
            float repairAmount = random.Next(enemyRepairMin, enemyRepairMax); // repair amount for enemyship
            Health += repairAmount;
            if (Health > MaxHealth) { //makes sure that enemy health does not go over max health
                Health = MaxHealth;
            }
            Console.WriteLine($"------------------------------------------------\n The enemy ship repaired itself to {Health}/{MaxHealth}\n------------------------------------------------");
        }
    }
}
//-----------------------------------------
// start of the actual code for the game displayed
//-----------------------------------------
public class PirateGame {
    bool menuanimationcancel = false; // token system for menu animation
    private Ship playerShip;
    private Ship enemyShip;
    public PirateGame() {
        playerShip = new Ship("Player Ship", 5, 50, 0, 100, 0, 0, 50, 10, 0, 500, 100, 0, 50);
        enemyShip = new Ship("Enemy Ship", new Random().Next(1, 6), new Random().Next(10, 51), 0, new Random().Next(75, 151), 0, new Random().Next(1, 6), 50, 10, 0, 0, 0, 0, 0);
    }
    public async void StartGameAnimation() // frames for start menu animation
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
        while (!menuanimationcancel) // stops the animation when token system activated
        {
            foreach (var frame in frames)
            {
                Console.Clear();
                Console.WriteLine(frame);
                await Task.Delay(1000); // wait time between frames fps is about 1
                if (menuanimationcancel)
                {
                    break;
                }
            }
        }   
    }
    public void Start() {    
        StartGameAnimation();
        while (true) {   
            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true); // if 'esc' key is pressed closes the game
            if (keyInfo.Key == ConsoleKey.Escape) {
                Environment.Exit(0);
            } else {
            menuanimationcancel = true; // if any other key is pressed, turns off menu animation and starts game.
            Console.Clear();
            EnterNameMenu();
            }
        }
        void EnterNameMenu() { // login system for game
            Console.WriteLine("Enter your name");
            while (true) {
                string name = Console.ReadLine();
                    if (string.IsNullOrEmpty(name)) { // makes sure that player has a name 
                            Console.Clear();
                            Console.WriteLine("Please enter a name");
                        }
                    if (name == "devtools") { // gives user access to devtools if they login as devtools
                        playerShip.Name = name;
                        Console.Clear();
                        DevToolsStartMenu();
                    } else {
                        Console.Clear();
                        #pragma warning disable CS8601 // Possible null reference assignment.
                        playerShip.Name = name;
                        StartMenu(); 
                        }
                }
            }
//--------------------------------------------
// Start of Devtools code // devtools allows people logged in as 'devtools' to edit in game values and random calculations.
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
            Console.WriteLine("Editable values\n------------------------------------------------------------------\ncannons\ncrew\nbank\nhealth\nitems\ncannonballs\ncursedballs\nwood\ncago\nmax-cargo\nplayer-attack-damage-min\nplayer-attack-damage-max\nplayer-attack-accuracy-chance\nenemy-repair-chance\nenemy-attack-damage-min\nenemy-attack-damage-max\nenemy-attack-accuracy-chance\ncursedball-attack-damage-min\ncursedball-attack-damage-max\nboard-chance\nenemy-crew-min\nenemy-crew-max\nplayer-repair-min-amount\nplayer-repair-max-amount\nenemy-repair-min\nenemy-repair-max\ntreasure-min-amount\ntreasure-max-amount\ntreasure-chance\nstolen-min-amount\nstolen-max-amount\nstolen-cursedball-min\nstolen-cursedball-max\n------------------------------------------------------------------");
        }
        void HandleValueEdit(string field) {
            var intFieldActions = new Dictionary<string, Action<int>> {
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
                { "treasure-chance", value => playerShip.TreasureChance = value },
                { "enemy-crew-min", value => playerShip.EnemyCrewMin = value },
                { "enemy-crew-max", value => playerShip.EnemyCrewMax = value},
                { "stolen-cursedball-min", value => playerShip.stolenCurseBallsMin = value},
                { "stolen-cursedball-max", value => playerShip.stolenCurseBallsMax = value},
                { "enemy-repair-chance", value => playerShip.enemyRepairChance = value},
                { "cargo", value => playerShip.Cargo = value},
                { "max-cargo", value => playerShip.MaxCargo = value },
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
            if (intFieldActions.ContainsKey(field)) {
                Console.Clear();
                int newValue = GetValidInput($"Enter a new value for {field}:", input => (int.TryParse(input, out var value), value));
                intFieldActions[field](newValue);
                Console.Clear();
                Console.WriteLine($"{field} has been changed to {newValue}");
                AskIfContinue();
            } else if (floatFieldActions.ContainsKey(field)) {
                float newValue = GetValidInput($"Enter a new value for {field}:", input => (float.TryParse(input, out var value), value));
                floatFieldActions[field](newValue);
                Console.Clear();
                Console.WriteLine($"{field} has been changed to {newValue}");
                AskIfContinue();
            } else {
                Console.Clear();
                Console.WriteLine("Invalid field. Please choose a valid option.\n-------------------------------------------------------");
                DevToolsStartMenu();
            }
        }
        T GetValidInput<T>(string prompt, Func<string, (bool, T)> tryParseFunc) {
            while (true) {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();
                var (isValid, value) = tryParseFunc(input);
                if (isValid) return value;
                Console.WriteLine($"Invalid input. Please enter a valid {typeof(T).Name.ToLower()}.\n-------------------------------------------------------");
            }
        }
        void AskIfContinue() {
            Console.WriteLine("Would you like to change any other values?\n-------------------------------------------------------\n 1. Yes\n 2. No\n-------------------------------------------------------");
            string continueInput = Console.ReadLine();
            if (continueInput == "1") {
                Console.Clear();
                DevToolsStartMenu();
            } else if (continueInput == "2") {
                Console.Clear();
                StartMenu();
            } else {
                Console.Clear();
                Console.WriteLine("Invalid input. Please enter 1 to continue or 2 to exit.\n-------------------------------------------------------");
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
                    Console.Clear(); // goes out to open ocean (gives player more otions)
                    OutofPortMenu();
                    break;
                case '2':  
                    Console.Clear(); // goes the shop
                    ShopMenu();
                    break;
                case '3':
                    Console.Clear(); // deposits player cargo into player bank
                    playerShip.Depot();
                    StartMenu();
                    break;
                case '4':  
                    Console.Clear(); // starts quit sequence 
                    QuitMenu();
                    break;
                case (char)ConsoleKey.Escape: // 'esc' as back
                    Console.Clear();
                    Start();
                    break;
                default:
                    Console.Clear(); //throws 'invalid option' error
                    Console.WriteLine("-----------------------------\n  Invalid choice. Try again.\n-----------------------------");
                    StartMenu();
                    break;
            }
        }
        void OutofPortMenu() {
            Console.WriteLine("\n              |    |    | \n             )_)  )_)  )_)   \n            )___))___))___)\\ \n           )____)____)_____)\\ \n         _____|____|____|____\\____\n---------\\                  /---------------------------\n^^^^^ ^^^^^^^^^^^ ^^^^^^^^^^^^  ^^^^^^^^^^\n^^^^      ^^^^  ^^^^^^^^^^^^^^^^^^^^^^   ^^^    ^^\n      ^^^^ ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^     ^^^ \n \n \n \n-------------------------------------------------------\n1. Attack Ship \n2. Repair Ship \n3. Search for treausre\n4. Go back to outpost\n-------------------------------------------------------");
            while (true) {
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
                OutofPortChoiceHandler(keyInfo.KeyChar);
            }
        }
        void OutofPortChoiceHandler(char choice) {
            switch (choice) {
                case '1':
                    enemyShip.MaxHealth = new Random().Next(75, 151);  // generates enemy health and starts attack sequence
                    enemyShip.Health = enemyShip.MaxHealth; // sets enemy health to max health
                    Console.Clear();
                    AttackMenu();
                    break;
                case '2':
                    Console.Clear(); // repair function for playership (does not try to repair enemy ship)
                    playerShip.Repair();
                    OutofPortMenu();
                    break;
                case '3':
                    int chance = new Random().Next(1 ,20); // determiens if the player ship gets discoverd by an enemy ship
                    Console.Clear();
                    if (chance >= 18) {
                        enemyShip.MaxHealth = new Random().Next(75, 151); // determiens enemyship health
                        enemyShip.Health = enemyShip.MaxHealth; // sets enemy ship health
                        Console.WriteLine("-------------------------------------------------------\n An enemy ship found you\n-------------------------------------------------------");
                        enemyShip.EnemyAttack(playerShip); // attack player ship
                        FightMenu();
                        break;
                    } else {
                        playerShip.Treasure(); // if the player ship did not get found by enemy ship gives player ship treasure amount
                        OutofPortMenu();
                        break;
                    }
                case '4':
                    Console.Clear(); // goes back to startmenu
                    StartMenu();
                    break;
                case (char)ConsoleKey.Escape: // 'esc' as back option on by default
                    Console.Clear();
                    StartMenu();
                    break;
                default: // throws 'invalid option' error
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
        void AttackChoiceHandler(char choice) { // lets the player decided if player wants to fight the current enemy ship that has been generated (if the choose not to and try to fight another one, it will regenerate enemy ship stats)
            switch (choice) {
                case '1':
                    Console.Clear(); // continues fight will generaged enemy ship stats
                    FightMenu();
                    break;
                case '2': // cancels  the fight
                    Console.Clear(); 
                    OutofPortMenu();
                    break;
                case (char)ConsoleKey.Escape: // 'esc' as back
                    Console.Clear(); 
                    OutofPortMenu();
                    break;
                default: // throws 'invalid option' error
                    Console.Clear();
                    Console.WriteLine("-----------------------------\n  Invalid choice. Try again.\n-----------------------------");
                    AttackMenu();
                    break;
            }
        }
        void FightMenu() {
            if (enemyShip.Health > 30) { // don't mess with this, this is the most scuffe logic ever used in code (determiens what options the player should have depends on if they are allowed to board and if they have the ability to use cursed cannon balls)
                if (playerShip.CursedCannonBalls > 0){
                    Console.WriteLine($"-------------------------------------------------------\n Cannonballs left {playerShip.Cannonballs}\n-------------------------------------------------------\n Curse cannon balls left {playerShip.CursedCannonBalls}\n-------------------------------------------------------\n Enemy ship health: {enemyShip.Health}/{enemyShip.MaxHealth}\n-------------------------------------------------------\n Your current health: {playerShip.Health}/{playerShip.MaxHealth}\n-------------------------------------------------------\n1. Shoot cannons\n2. Shoot cursed cannon ball\n3. Repair your ship\n4. Leave fight\n-------------------------------------------------------");      
                } else {
                    Console.WriteLine($"-------------------------------------------------------\n Cannonballs left {playerShip.Cannonballs}\n-------------------------------------------------------\n Enemy ship health: {enemyShip.Health}/{enemyShip.MaxHealth}\n-------------------------------------------------------\n Your current health: {playerShip.Health}/{playerShip.MaxHealth}\n-------------------------------------------------------\n1. Shoot cannons\n2. Repair your ship\n3. Leave fight\n-------------------------------------------------------");
                }
            }   
            if (enemyShip.Health <= 30) {
                if (playerShip.CursedCannonBalls > 0) {
                    Console.WriteLine($"-------------------------------------------------------\n Cannonballs left {playerShip.Cannonballs}\n-------------------------------------------------------\n Curse cannon balls left {playerShip.CursedCannonBalls}\n-------------------------------------------------------\n Enemy ship health: {enemyShip.Health}/{enemyShip.MaxHealth}\n-------------------------------------------------------\n Your current health: {playerShip.Health}/{playerShip.MaxHealth}\n-------------------------------------------------------\n1. Shoot cannons\n2. Shoot cursed cannon ball\n3. Repair your ship\n4. Board Ship\n5. Leave fight\n-------------------------------------------------------");
                } else {
                    Console.WriteLine($"-------------------------------------------------------\n Cannonballs left {playerShip.Cannonballs}\n-------------------------------------------------------\n Enemy ship health: {enemyShip.Health}/{enemyShip.MaxHealth}\n-------------------------------------------------------\n Your current health: {playerShip.Health}/{playerShip.MaxHealth}\n-------------------------------------------------------\n1. Shoot cannons\n2. Repair your ship\n3. Board Ship\n4. Leave fight\n-------------------------------------------------------");
                }
            }
            if (playerShip.Health <= 0) {   // if player ship health goes below or is equal to 0 game closes
                Console.Clear();
                Console.WriteLine("----------------------------------------------\n    Your ship was destroyed. Game over!\n----------------------------------------------");
                Environment.Exit(1);
            }   
            if (enemyShip.Health <= 0) { // runs stolen function if enemyship is sunk
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
            switch (choice) { // both enemy and palyer ship attack eachother 
                case '1':
                    Console.Clear();
                    playerShip.PlayerAttack(enemyShip);
                    enemyShip.EnemyAttack(playerShip);
                    FightMenu();
                    break;
                case '2':
                    if (playerShip.CursedCannonBalls > 0) { // uses players cursed cannon balls if player has them
                        Console.Clear();
                        playerShip.CursedBallAttack(enemyShip);
                        enemyShip.EnemyAttack(playerShip);
                        FightMenu();
                    } else { // if player doesn't have cursed cannon balls it will repair the ship
                        Console.Clear();
                        playerShip.Repair();
                        enemyShip.EnemyRepair();
                    }
                    FightMenu();
                    break;
                case '3':
                    if (playerShip.CursedCannonBalls > 0 && enemyShip.Health > 30) { // if player has cursed cannon balls it will repair the enemy ship 
                        Console.Clear();
                        playerShip.Repair();
                        enemyShip.EnemyRepair();
                        FightMenu();
                    }
                    if (playerShip.CursedCannonBalls > 0 && enemyShip.Health <= 30) { // if the player has cursed cannons and can board the ship, it will repair the ship
                        Console.Clear();
                        playerShip.Repair();
                        enemyShip.EnemyRepair();
                        FightMenu();
                        break;
                    }
                    if (playerShip.CursedCannonBalls <= 0 && enemyShip.Health > 30) { // if the player does not have them it will let the player leave the fight
                        Console.Clear();
                        LeaveFightMenu();
                        break;
                    }
                    if (playerShip.CursedCannonBalls <= 0 && enemyShip.Health <= 30) { // if the player has the ability to board and does not ahve cursed cannon balls, it will attempt to board enemy ship
                        Console.Clear();
                        playerShip.BoardChance(enemyShip);
                        OutofPortMenu();
                        break;
                    } 
                    break;
                case '4': 
                    if (playerShip.CursedCannonBalls > 0 && enemyShip.Health > 30) { // if the player ship has cused cannonballs and can't board enemy ship its leaves the fight
                        Console.Clear();
                        LeaveFightMenu();
                        break;
                    }
                    if (playerShip.CursedCannonBalls > 0 && enemyShip.Health <= 30) { // if the player ship has curse cannon balls and can board, it will board the ship 
                        Console.Clear();
                        playerShip.BoardChance(enemyShip);
                        OutofPortMenu();
                        break;
                    }
                    if (playerShip.CursedCannonBalls <= 0 && enemyShip.Health > 30) { // if the player doesn't have cannon balls and can't board the ship it will thorw an 'invalid option' error 
                        Console.Clear();
                        Console.WriteLine("-----------------------------\n  Invalid choice. Try again.\n-----------------------------");
                        FightMenu();
                        break;
                    }
                    if (playerShip.CursedCannonBalls <= 0 && enemyShip.Health <= 30) { // if the player has no cursed cannon balls and can board the ship it will let the palyer leave the fight.
                        Console.Clear();
                        LeaveFightMenu();
                        break;
                    }
                    break;
                case '5':
                    if (playerShip.CursedCannonBalls < 0 && enemyShip.Health <= 30) { // if player has cursed cannon balls and can board the enemy ship, this option will let them leave the fight.
                        Console.Clear();
                        LeaveFightMenu();
                        break;
                    } else {
                        Console.Clear(); // if they can not board the ship and don't have cursed cannon balls, throw an 'invald option' error.
                        Console.WriteLine("-----------------------------\n  Invalid choice. Try again.\n-----------------------------");
                        FightMenu();
                        break; 
                    }
                case (char)ConsoleKey.Escape: // 'esc' as back 
                    Console.Clear();
                    OutofPortMenu();
                    break;
                default:
                    Console.Clear(); // throws 'invalid option' error
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
                    OutofPortMenu(); // leaves fight and sends back to open ocean
                    break;
                case '2':
                    Console.Clear();
                    FightMenu(); // continues with fight
                    break;
                case (char)ConsoleKey.Escape: // 'esc' as back option
                    Console.Clear();
                    FightMenu();
                    break;
                default:
                    Console.Clear(); // throws 'invalid option' error
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
                    if (playerShip.Bank < 100) { // throws 'not enough gold' error is player doesn't have enough gold.                   
                        Console.WriteLine("-------------------------------\n You don't have enough coins \n-------------------------------\n \n \n");
                    } else {
                        playerShip.Bank -= 100;
                        playerShip.Cannonballs += 10; // removes correct amount of gold and gives player corret amount of cannons.                         
                        Console.WriteLine($"-----------------------------------------------\n You now have {playerShip.Cannonballs} cannonballs \n-----------------------------------------------\n \n \n");
                    }
                    ShopMenu();
                    break;
                case '2':
                    Console.Clear();
                    if (playerShip.Bank < 300) { // throws 'not enough gold' error is player doesn't have enough gold.  
                        Console.WriteLine("-------------------------------\n You don't have enough coins \n-------------------------------\n \n \n");
                    } else {
                        playerShip.Bank -= 300;
                        playerShip.CursedCannonBalls += 2; // removes correct amount of gold and gives player correct amount of cursed cannon balls
                        Console.WriteLine($"-----------------------------------------------\n You now have {playerShip.CursedCannonBalls} cursed cannonballs \n-----------------------------------------------\n \n \n");
                    }
                    ShopMenu();
                    break;
                case '3':
                    Console.Clear();
                    if (playerShip.Bank < 100) { // throws 'not enough gold' error is player doesn't have enough gold.  
                        Console.WriteLine("-------------------------------\n You don't have enough coins \n-------------------------------\n \n \n");
                    } else {
                        playerShip.Bank -= 100;
                        playerShip.Wood += 10; //gives palyer correct amount of wood and removes correct amount of coins
                        Console.WriteLine($"-----------------------------------------------\n You now have {playerShip.Wood} wood \n-----------------------------------------------\n \n \n");
                    }
                    ShopMenu();
                    break;
                case '4':
                    Console.Clear();
                    if (playerShip.Bank < 1000) { // throws 'not enough gold' error is player doesn't have enough gold.  
                        Console.WriteLine("-------------------------------\n You don't have enough coins \n-------------------------------\n \n \n");
                    } else if (playerShip.Cannons < playerShip.MaxCannons) {
                        playerShip.Bank -= 1000;
                        playerShip.Cannons += 1; //gives player correcnt amount of cannons and takes gold
                        Console.WriteLine($"---------------------------------------------\n You now have {playerShip.Cannons} cannons \n---------------------------------------------\n \n \n");
                    } else { // makes sure that player cannont go past max cannon limit
                        Console.WriteLine("----------------------------------------------------------------------\n You have reached your max cannons amount (upgrade ship to increase) \n----------------------------------------------------------------------\n \n \n");
                    }
                    ShopMenu();
                    break;
                case '5':
                    Console.Clear();
                    if (playerShip.Bank < 100) { // throws 'not enough gold' error is player doesn't have enough gold.                        
                        Console.WriteLine("-------------------------------\n You don't have enough coins \n-------------------------------\n \n \n");
                    } else if (playerShip.Crew < playerShip.MaxCrew) { 
                        playerShip.Bank -= 100;
                        playerShip.Crew += 10;
                        if (playerShip.Crew > playerShip.MaxCrew) {
                            playerShip.Crew = playerShip.MaxCrew; // makes sure that playerr cannont go over limit (will scam you if you have less than 10 crew members missing from max crew limit)
                        }                                
                        Console.WriteLine($"-----------------------------------------------\n You now have {playerShip.Crew} crew members \n-----------------------------------------------\n \n \n");
                    } else {
                        Console.WriteLine("--------------------------------------------------------------------\n  You have reached your max crew amount (upgrade ship to increase) \n--------------------------------------------------------------------\n \n \n");
                    }   
                    ShopMenu();
                    break;
                case '6':
                    Console.Clear();
                    if (playerShip.Bank < 5000) { // throws 'not enough gold' error is player doesn't have enough gold.                          
                        Console.WriteLine("-------------------------------\n You don't have enough coins \n-------------------------------\n \n \n");
                    } else {
                        playerShip.Bank -= 5000; // applies buffs to player ship and removes 5000 coins
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
                    if (playerShip.Items < 1) { // throws 'no inventory items' error                                                       
                        Console.WriteLine("-------------------------------------\n You don't have any captured ships \n-------------------------------------\n \n \n");
                    } else {
                        playerShip.Items -= 1;
                        playerShip.Bank += 1000; // sells captured ship for 1000 coins  
                        Console.WriteLine("---------------------------------\n You have sold a captured ship \n---------------------------------\n \n \n");
                    }
                    ShopMenu();
                    break;
                case '8':
                    Console.Clear(); // leave shop
                    StartMenu();
                    break;
                case (char)ConsoleKey.Escape: // 'esc' as back option
                    Console.Clear();
                    StartMenu();
                    break;
                default:
                    Console.Clear(); // throws 'invalid option' error
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
                    Environment.Exit(1); // closes the game 
                    break;
                case '2':
                    Console.Clear();
                    StartMenu(); // goes back to game if player deson't want to leave game
                    break;
                case (char)ConsoleKey.Escape: // 'esc' as back otion on by defualt
                    Console.Clear();
                    StartMenu();
                    break;
                default:
                    Console.Clear(); // thorws 'invalid option' error
                    Console.WriteLine("------------------------------\n Invalid choice. Try again. \n------------------------------");
                    QuitMenu();
                    break;    
            }
        }
    }
    static void Main(string[] args) { // starts the game when program started
        PirateGame game = new PirateGame();
        game.Start();
    }
}