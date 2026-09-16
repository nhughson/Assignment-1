using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static assignment1.Program;

namespace assignment1
{
    public class Game
    {
        NPC ent = new NPC("??? Tree Person ???");


        string checkpoint_id = "start";
        
        bool skip = false;
        Player player = new Player();
        
        STATE state = STATE.Dialogue;

        public bool running = true;



        List<Enemy> enemies = [];
        int enemy_index = 0;
        int dmg = 0;

        int item_index = -1;

        Text text = new Text();
        Random rnd = new Random();

        public void setup() 
        {
            player.update_items();
        }
        public void tick() 
        {
            switch(state)
            {
                case STATE.End_Game:
                    //end game
                    running = false;
                    break;
                case STATE.Dialogue:
                    run_dialogue();
                    break;
                case STATE.Setup_Combat:
                    setup_combat();
                    break;
                case STATE.Enemy_Attack:
                    enemy_attack();
                    break;
                case STATE.Player_Choose_Defend:
                    player_defend();
                    break;
                case STATE.Player_Choose_Combat:
                    player_attack();
                    break;
            }
        }

        private int verify_int_input(int max, int min = 0) 
        {
            bool input_valid = false;
            int option = -1;
            string input = "";
            while (!input_valid)
            {
                option = -1;
                input = Console.ReadLine();
                try
                {
                    option = Int32.Parse(input) - 1;

                    if (option >= max || option < 0)
                    {
                        Console.WriteLine("Please input a valid number.");
                    }
                    else
                    {
                        input_valid = true;
                    }
                }
                catch
                {
                    Console.WriteLine("Please input a valid number.");
                }
            }
            return option;
        }

        private void player_take_damage(int dmg) 
        {
            Console.WriteLine($"You lose. You take {dmg} damage. Choose where to take damage.");
            Thread.Sleep(300);
            while (dmg > 0)
            {

                Console.WriteLine($"What would you like to take damage in?\n1. Might ({player.get_str()})\n2. Speed ({player.get_spd()})\n3. Defense ({player.get_def()})");

                int option = verify_int_input(3);

                Console.WriteLine("How much damage would you like to take?");

                int option_2 = verify_int_input(-dmg);

                dmg -= option_2;
                if (option == 0)
                {
                    player.change_str(-option_2);
                }
                else if (option == 1)
                {
                    player.change_spd(-option_2);
                }
                else if (option == 2)
                {
                    player.change_def(-option_2);
                }
            }

            if (player.check_to_die())
            {
                Console.WriteLine("You have died. Returning to checkpoint.");
                Thread.Sleep(300);
                state = STATE.Dialogue;
                text.text_id = checkpoint_id;
                player.stat_return();
            }
        }

        private void run_dialogue() 
        {
            skip = false;
            
            text.option_ids = [];
            text.option_names = [];

            text.reset();

            int take_input_index = -1;
            string input_variable = "";

            //string church_bg = "|\r\n                                   ,|,\r\n                                   |||\r\n                                  / | \\\r\n                                  | | |\r\n                                  | | |\r\n                                 /  |  \\\r\n                                 |  |  |\r\n                                 |  |   \\\r\n                                /    \\  |\r\n                                |    |  |\r\n                                |    |   \\\r\n                               /     |   |\r\n                8              |     |   |\r\n              \"\"8\"\"           /      |    \\\r\n                8            /        \\   ,\\\r\n              ,d8888888888888|========|=\"\" |\r\n            ,d\"  \"88888888888|  ,aa,  |  a |\r\n          ,d\"      \"888888888|  8  8  |  8 |\r\n       ,d8888888b,   \"8888888|  8aa8  |  8,|\r\n     ,d\"  \"8888888b,   \"88888|========|=\"\" |\r\n   ,d\"      \"8888888b,   \"888|  a  a  |  a |\r\n ,d\"   ,aa,   \"8888888b,   \"8|  8  8  |  8,|\r\n/|    d\"  \"b    |\"\"\"\"\"\"|     |========|=\"\" |\r\n |    8    8    |      |     |  ,aa,  |  a |\r\n |    8aaaa8    |      |     |  8  8  |  8 |\r\n |              |      |     |  \"\"\"\"  | ,,=|\r\n |aaaaaaaaaaaaaa|======\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\"\r\n            Normand  Veilleux\r\n";

            string check_stat_id = "";

            STATE change_state = STATE.Dialogue;
            switch (text.text_id)
            {
                case "start":
                    text.add("Hello.", 150);
                    text.add("What is your name?");
                    take_input_index = 1;
                    input_variable = "name";
                    skip = true;
                    text.next_id = "start part 2";
                    break;
                case "start part 2":
                    if (player.name.Equals("Kaneis"))
                    {
                        text.add("What a nice name.");
                        text.add("Interesting. That is correct, that is your name.");
                        skip = true;
                        text.next_id = "Kaneis";
                    }
                    else if (player.name.Equals("Oracle")) 
                    {
                        text.add("That isn't your name. Let's retry then, shall we?");
                        skip = true;
                        text.next_id = "start";
                    }
                    else
                    {
                        text.add("What a nice name.");
                        text.add(". . . sorry what was your name again?");
                        text.option_ids = ["XDDCC", "name", "ENA"];
                        text.option_names = ["XDDCC", player.name, "ENA"];
                    }
                    break;
                case "name":
                    text.add($"Ah yes, {player.name}. What a lovely name.");
                    skip = true;
                    text.next_id = "forget name";
                    break;
                case "XDDCC":
                    text.add($"Ah yes, XDDCC. What a lovely name.");
                    skip = true;
                    text.next_id = "forget name";
                    break;
                case "ENA":
                    text.add($"Ah yes, ENA. What a lovely name.");
                    skip = true;
                    text.next_id = "forget name";
                    break;
                case "forget name":
                    text.add("I won't remember that.");
                    text.add("And neither will anyone else.");
                    skip = true;
                    text.next_id = "Kaneis";
                    break;
                case "Kaneis":
                    text.add("Goodbye, Kaneis.");
                    text.add("You feel groggy.");
                    text.add("You can feel grass under you.");
                    text.add("It is blindingly bright.");
                    text.add("Your eyes are slow to adjust.");
                    text.add("But as they do, you can see a building. It is a church. Would you like to analyse the church?");
                    text.option_ids = ["analyse the church", "approach the church"];
                    text.option_names = ["Yes", "No"];
                    break;
                case "analyse the church":
                    text.add("You must do a knowledge roll to analyse the church.");
                    check_stat_id = "knowledge";
                    skip = true;
                    text.next_id = "analyse church results";
                    break;
                case "analyse church results":
                    text.add($"You rolled {player.roll}.");
                    if (player.roll > 3)
                    {
                        if (player.roll > 6)
                        {
                            if (player.roll > 9)
                            {
                                text.add("The church is run down. It appears to have residents, but residents of the non-human variety. You have a bad feeling about this church.");
                            }
                            else
                            {
                                text.add("The church is run down. It looks lived in. You can tell it isn't meant to be in use...");
                            }
                        }
                        else
                        {
                            text.add("The church is run down. However, it looks lived in.");
                        }
                    }
                    else
                    {
                        text.add("You could not analyse the church.");
                    }
                    skip = true;
                    text.next_id = "approach the church";
                    break;
                case "approach the church":
                    text.add("You feel a strange compulsion.");
                    text.add("You feel compelled to approach the church.");
                    text.add("You are not in control of your body.");
                    text.add("The church is surprisingly bright on the insides, light flowing and dancing through the broken windows.");
                    text.add("There is a person at the altar.");
                    text.add("You get closer, and realise it is not a person. It appears to be a tree, but the tree is watching you.");
                    text.add($"{ent.name}: 'Hello, Kaneis! It is so good to see you again.'\nWould you like to ask the Tree-Person any questions?");
                    text.option_ids = ["ent kaneis?", "ent name?", "ent church?"];
                    text.option_names = ["Kaneis?", "Your name?", "The church?"];
                    break;
                case "ent kaneis?":
                    text.add($"{ent.name}: 'Kaneis? Kaneis is you! Who else would I be talking to? You are the only Kaneis here.'");
                    text.option_ids = ["ent name?", "ent church?", "done with ent"];
                    text.option_names = ["Your name?", "The church?", "No more questions."];
                    break;
                case "ent name?":
                    text.add($"{ent.name}: 'My name? You know my name. My name is -INCOMPLETE-'"); // i need to think of a name
                    ent.name = "-INCOMPLETE-";
                    text.option_ids = ["ent kaneis?", "ent church?", "done with ent"];
                    text.option_names = ["Kaneis?", "The church?", "No more questions."];
                    break;
                case "ent church?":
                    text.add($"{ent.name}: 'This is our home. This is where we are meant to be.'");
                    text.option_ids = ["ent kaneis?", "ent name?", "done with ent"];
                    text.option_names = ["Kaneis?", "Your name?", "No more questions."];
                    break;
                case "done with ent":
                    text.add($"{ent.name}: 'You are done with questions? You seem to have forgotten a lot. Oh well, it is the nature of this place.'");
                    text.add($"{ent.name}: 'I will reiterate with you. We have only one mission, Kaneis.'");
                    text.add($"{ent.name}: 'We must keep Purgatory from leaking into this realm.'");
                    text.add($"{ent.name}: 'You are my soldier, my warrior into the darkness. You are the most crucial part of this mission.'");
                    text.add($"{ent.name}: 'Since I suppose you have forgotten your companion, I will allow you to choose a new one.' {ent.name} goes to open the door to the bell tower, where 4 crates are stored, containing a dog, cat, crow and a rat.");
                    text.option_ids = ["take dog", "take cat", "take crow", "take rat"];
                    text.option_names = ["Dog", "Cat", "Crow", "Rat"];
                    break;
                case "take dog":
                    text.add($"{ent.name}: 'Ah yes. The dog. It will defend you will all of its might. Despite looking a bit like a stray, it is very charismatic and it is a very good companion. Are you sure you wish to take the dog?'");
                    text.option_ids = ["yes take dog", "rechoose companion"];
                    text.option_names = ["Yes", "No"];
                    break;
                case "yes take dog":
                    text.add($"{ent.name}: 'Good choice. Name your companion.'");
                    take_input_index = 0;
                    input_variable = "dog name";
                    skip = true;
                    text.next_id = "companion_named";
                    break;
                case "take cat":
                    text.add($"{ent.name}: 'Ah yes. The cat. It is a fast one, surely. And it is quite a smart cat, though it is not quite as strong. Are you sure you wish to take the cat?'");
                    text.option_ids = ["yes take cat", "rechoose companion"];
                    text.option_names = ["Yes", "No"];
                    break;
                case "yes take cat":
                    text.add($"{ent.name}: 'Good choice. Name your companion.'");
                    take_input_index = 0;
                    input_variable = "cat name";
                    skip = true;
                    text.next_id = "companion_named";
                    break;
                case "take crow":
                    text.add($"{ent.name}: 'Ah yes. The crow. The crow is smart, an incredible puzzle solver. It is quite a nimble little bird, and quite evenly strong and resilient. Are you sure you wish to take the crow?'");
                    text.option_ids = ["yes take crow", "rechoose companion"];
                    text.option_names = ["Yes", "No"];
                    break;
                case "yes take crow":
                    text.add($"{ent.name}: 'Good choice. Name your companion.'");
                    take_input_index = 0;
                    input_variable = "crow name";
                    skip = true;
                    text.next_id = "companion_named";
                    break;
                case "take rat":
                    text.add($"{ent.name}: 'Ah yes. The rat. It is a bit fragile, but it is fast and strong. Not incredibly charismatic, but it is surprisingly smart. Are you sure you wish to take the rat?'");
                    text.option_ids = ["yes take rat", "rechoose companion"];
                    text.option_names = ["Yes", "No"];
                    break;
                case "yes take rat":
                    text.add($"{ent.name}: 'Good choice. Name your companion.'");
                    take_input_index = 0;
                    input_variable = "rat name";
                    skip = true;
                    text.next_id = "companion_named";
                    break;
                case "rechoose companion":
                    text.add("Choose the dog, the cat, the crow or the rat.");
                    text.option_ids = ["take dog", "take cat", "take crow", "take rat"];
                    text.option_names = ["Dog", "Cat", "Crow", "Rat"];
                    break;
                case "companion_named":
                    string name = player.party[player.party.Count() - 1].name;
                    switch (name)
                    {
                        case "Kaneis":
                            text.add($"{ent.name}: '{name}. That's definitely going to get confusing.");
                            break;
                        case "Oracle":
                            text.add($"{ent.name}: 'I wouldn't have chosen that name.");
                            break;
                        default:
                            text.add($"{ent.name}: '{name}. What a good choice.");
                            break;
                    }
                    skip = true;
                    text.next_id = "go to purgatory?";
                    break;
                case "go to purgatory?":
                    text.add($"{ent.name}: 'So, Kaneis, are you ready to go to Purgatory with {player.party[player.party.Count() - 1].name}?'");
                    text.option_ids = ["level select", "explore church start"];
                    text.option_names = ["Yes", "No, let me explore the church"];
                    break;
                case "level select":

                    break;
                case "explore church start":
                    text.add("Where would you like to go?");
                    text.option_ids = ["church belltower", "church pews", "church grounds", "go to purgatory?"];
                    text.option_names = ["Belltower", "Pews", "Grounds", $"{ent.name}"];
                    break;
                case "church belltower":
                    text.add("The door to the belltower creaks loudly as you push it open. Looking up the winding spiral staircase gives you vertigo. You must make a sanity roll to ascend.");
                    check_stat_id = "sanity";
                    skip = true;
                    text.next_id = "church belltower sanity";
                    break;
                case "church belltower sanity":
                    text.add($"You rolled {player.roll}.");
                    if (player.roll > 6)
                    {
                        text.add("You manage to climb the stairs. It is a grueling climb, you can hear the wind whistling past outside. Each stair feels like a mountain, and eventually, the wind feels like it's pushing down the stairs. You must make a defense roll to progress.");
                        check_stat_id = "def";
                        skip = true;
                        text.next_id = "church belltower def";
                    }
                    else 
                    {
                        text.add("You do not manage to climb the stairs.");
                        text.add("You close the doors behind you as you leave into the foyer.");
                        skip = true;
                        text.next_id = "explore church start";
                    }
                    break;
                case "church belltower def":
                    text.add($"You rolled {player.roll}.");
                    if (player.roll > 6)
                    {
                        text.add("You manage to climb go on. The stairs try and push you back, but you resist. You power your way to the top, voices screaming in your ears telling you to return but you persist.");
                        text.add("It is silent at the top.");
                        text.add("The bell hangs idle, rusted. You can see far from up here, but all that there is to see is a dense, swaying forest.");
                        if (!player.inventory_check("Bell"))
                        {
                            text.add("Would you like to take the bell?");
                            text.option_ids = ["take bell", "leave bell"];
                            text.option_names = ["Yes", "No"];
                        }
                        else
                        {
                            text.add("There is not much to do up here. You return down the stairs, which proves a lot easier than climbing them.");
                            skip = true;
                            text.next_id = "explore church start";
                        }

                    }
                    else 
                    {
                        text.add("You do not manage to climb the stairs.");
                        text.add("You close the doors behind you as you leave into the foyer.");
                        skip = true;
                        text.next_id = "explore church start";
                    }
                    break;
                case "take bell":
                    text.add("You take the bell.");
                    player.inventory_add(new Item("Bell"));
                    skip = true;
                    text.next_id = "leave bell";
                    break;
                case "leave bell":
                    text.add("There is not much to do up here. You return down the stairs, which proves a lot easier than climbing them.");
                    skip = true;
                    text.next_id = "explore church start";
                    break;
                case "church pews":
                    text.add("You walk through the pews. Would you like to look closer?");
                    check_stat_id = "knowledge";
                    text.option_ids = ["church pews look", "explore church start"];
                    text.option_names = ["Yes", "No"];
                    break;
                case "church pews look":
                    text.add("You make a knowledge roll.");
                    text.add($"You rolled {player.roll}.");
                    if (player.roll > 3)
                    {
                        if (player.roll > 6)
                        {
                            if (player.roll > 16)
                            {
                                if (!player.inventory_check("Holy Grail"))
                                {
                                    text.add("You found the Holy Grail.");
                                    player.inventory_add(new Item("Holy Grail"));
                                }
                                else
                                {
                                    text.add("There was nothing in the pews.");
                                }
                            }
                            else if (!player.inventory_check("Dagger"))
                            {
                                text.add("You found a Dagger.");
                                player.inventory_add(new Item("Dagger"));
                            }
                            else
                            {
                                text.add("There was nothing in the pews.");
                            }
                        }
                        else if (!player.inventory_check("Plank"))
                        {
                            text.add("You found a Plank.");
                            player.inventory_add(new Item("Plank"));
                        }
                        else 
                        {
                            text.add("There was nothing in the pews.");
                        }
                    }
                    else 
                    {
                        text.add("There was nothing in the pews.");
                    }
                    skip = true;
                    text.next_id = "explore church start";
                    break;
                case "church grounds":
                    text.add("You walk outside the church. There is a graveyard to your left, a maze to your right and dense forests surrounding you.");
                    text.option_ids = ["church graveyard", "church maze", "church forest", "explore church start"];
                    text.option_names = ["Go to the Graveyard", "Go to the Maze", "Go to the Forest", "Go back inside"];
                    break;
                case "church graveyard":
                    text.add($"You head into the graveyard. {player.party[player.party.Count() - 1].name} looks saddened and sits on one of the gravestones. The maze is behind you, the church is to your left.");
                    text.option_ids = ["church maze", "church forest", "explore church start"];
                    text.option_names = ["Go to the Maze", "Go to the Forest", "Go back inside"];
                    break;
                case "church maze":
                    text.add("You must attmempt a knowledge roll to pass through the maze.");
                    check_stat_id = "knowledge";
                    skip = true;
                    text.next_id = "maze check";
                    break;
                case "maze check":
                    text.add($"You rolled {player.roll}.");
                    if (player.roll > 8)
                    {
                        if (!player.inventory_check("Minotaur Head"))
                        {
                            text.add("You found a Minotaur Head.");
                            player.inventory_add(new Item("Minotaur Head"));
                        }
                        else
                        {
                            text.add("There was nothing in the maze.");
                        }
                    }
                    else 
                    {
                        text.add("You were not sucessful navigating the maze.");
                    }
                    skip = true;
                    text.next_id = "explore church start";
                        break;
                case "church forest":
                    text.add("The trees seem to get denser and denser the closer you get, to the point where they form a wall blocking you from entering the forest. Would you like to try and climb the trunk?");
                    check_stat_id = "str";
                    text.option_ids = ["climb forest", "church grounds"];
                    text.option_names = ["Yes", "No"];
                    break;
                case "climb forest":
                    text.add($"You rolled {player.roll} in strength.");
                    text.add("You failed to climb the trees.");
                    skip = true;
                    text.next_id = "church forest";
                    break;
            }

            for (int i = 0; i < text.text_queue.Count; i++)
            {
                text.write_text(i, take_input_index == i);
                if (take_input_index == i)
                {
                    Console.Write("Input:  ");
                    bool input_valid = false;
                    string input = "";
                    string first_letter = "";
                    switch (input_variable)
                    {
                        case "name":
                            while (!input_valid) 
                            {
                                input = Console.ReadLine();
                                if (input.Length < 1)
                                {
                                    Console.WriteLine("Please input a name.");
                                }
                                else 
                                {
                                    input_valid = true;
                                }
                            }
                            player.name = input;
                            first_letter = player.name[0].ToString();
                            player.name = player.name.Remove(0, 1);
                            first_letter = first_letter.ToUpper();
                            player.name = first_letter + player.name;
                            break;
                        case "dog name":
                            while (!input_valid) 
                            {
                                input = Console.ReadLine();
                                if (input.Length < 1)
                                {
                                    Console.WriteLine("Please input a name.");
                                }
                                else 
                                {
                                    input_valid = true;
                                }
                            }
                            Party_member dog = new Party_member("Dog");
                            first_letter = input[0].ToString();
                            input = input.Remove(0, 1);
                            first_letter = first_letter.ToUpper();
                            input = first_letter + input;
                            dog.setup(player, input);
                            break;
                        case "cat name":
                            while (!input_valid) 
                            {
                                input = Console.ReadLine();
                                if (input.Length < 1)
                                {
                                    Console.WriteLine("Please input a name.");
                                }
                                else 
                                {
                                    input_valid = true;
                                }
                            }
                            Party_member cat = new Party_member("Cat");
                            first_letter = input[0].ToString();
                            input = input.Remove(0, 1);
                            first_letter = first_letter.ToUpper();
                            input = first_letter + input;
                            cat.setup(player, input);
                            break;
                        case "crow name":
                            while (!input_valid) 
                            {
                                input = Console.ReadLine();
                                if (input.Length < 1)
                                {
                                    Console.WriteLine("Please input a name.");
                                }
                                else 
                                {
                                    input_valid = true;
                                }
                            }
                            Party_member crow = new Party_member("Crow");
                            first_letter = input[0].ToString();
                            input = input.Remove(0, 1);
                            first_letter = first_letter.ToUpper();
                            input = first_letter + input;
                            crow.setup(player, input);
                            break;
                        case "rat name":
                            while (!input_valid) 
                            {
                                input = Console.ReadLine();
                                if (input.Length < 1)
                                {
                                    Console.WriteLine("Please input a name.");
                                }
                                else 
                                {
                                    input_valid = true;
                                }
                            }
                            Party_member rat = new Party_member("Rat");
                            first_letter = input[0].ToString();
                            input = input.Remove(0, 1);
                            first_letter = first_letter.ToUpper();
                            input = first_letter + input;
                            rat.setup(player, input);
                            break;
                    }
                }
            }
            if (check_stat_id != "")
            {
                switch (check_stat_id)
                {
                    case "str":
                        player.roll = player.check_str(rnd);
                        break;
                    case "spd":
                        player.roll = player.check_spd(rnd);
                        break;
                    case "def":
                        player.roll = player.check_def(rnd);
                        break;
                    case "knowledge":
                        player.roll = player.check_knowledge(rnd);
                        break;
                    case "sanity":
                        player.roll = player.check_sanity(rnd);
                        break;
                    case "charisma":
                        player.roll = player.check_charisma(rnd);
                        break;
                }
            }

            if (change_state == STATE.Dialogue)
            {
                if (!skip)
                {
                    for (int i = 0; i < text.option_names.Count; i++)
                    {
                        Console.WriteLine((i + 1).ToString() + ".  " + text.option_names[i]);
                    }

                    text.text_id = text.option_ids[verify_int_input(text.option_names.Count())];
                }
                else 
                {
                    text.text_id = text.next_id;
                }
            }
            else
            {
                state = change_state;
            }
        }

        private void setup_combat() 
        {
            enemy_index = 0;
            enemies = [new Enemy("Zombie"), new Enemy("Zombie")];

            Console.Write("You have been attacked by");

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].setup();
                if (i < enemies.Count - 1)
                {
                    Console.Write(" a ");
                }
                else
                {
                    Console.Write(" and a ");
                }
                Console.Write(enemies[i].name);
            }
            Console.Write("!\n");

            Thread.Sleep(800);

            state = STATE.Enemy_Attack;
        }

        private void enemy_attack() 
        {
            //combat stuff
            state = STATE.Player_Choose_Defend;

            /*
            for (int i = 0; i < enemies.Count; i++)
            {
                if (state == STATE.Enemy_Attack)
                {
                    player.hp -= enemies[i].atk-player.def;
                    Console.WriteLine($"You have been attacked by {enemies[i].name}. Your HP is now {player.hp}.");
                    Thread.Sleep(800);
                }
                if (player.hp <= 0) 
                {
                    Console.WriteLine("You have died. Returning to checkpoint.");
                    state = STATE.Player_Choose_Combat;
                    text_id = checkpoint_id;
                    player.hp = player.max_hp;
                }
            }
            */
            dmg = enemies[enemy_index].check_str(rnd);
            Console.WriteLine($"You have been attacked by {enemies[enemy_index].name} using Might. They rolled {dmg}.");
            Thread.Sleep(800);
        }

        private void player_defend() 
        {
            

            //rewrite later
            Console.WriteLine("How would you like to defend?\n1. Might\n2. Defense");
            int option = verify_int_input(2);

            int defending_with = 0;


            if (player.items.Count() > 0)
            {

                Console.WriteLine("Would you like to use any items? Your options are:");
                Console.WriteLine("1. Do not use any items");
                for (int i = 0; i <= player.items.Count() - 1; i++)
                {
                    Console.WriteLine($"{i + 2}. {player.items[i].name}");
                }

                item_index = verify_int_input(player.items.Count()+1);

                if (item_index == 1) 
                {
                    item_index = -1;
                }

            }

            if (option == 0)
            {
                defending_with = player.check_str(rnd);
            }
            else
            {
                defending_with = player.check_def(rnd);
            }


            Console.WriteLine($"You rolled {defending_with}.");
            Thread.Sleep(300);

            if (item_index != -1)
            {
                if (option == 0)
                {
                    defending_with += player.items[item_index].bonus_str;
                }
                else
                {
                    defending_with += player.items[item_index].bonus_def;
                }

                Console.WriteLine($"Your item changed that to {defending_with}.");
                Thread.Sleep(300);
            }

            defending_with -= dmg;

            if (option == 1)
            {
                defending_with /= 2;
            }

            if (defending_with > 0)
            {
                Console.WriteLine($"You win this round. The enemy takes {defending_with} damage.");
                Thread.Sleep(300);
                if (enemies[enemy_index].take_dmg(defending_with))
                {
                    Console.WriteLine($"You killed {enemies[enemy_index].name}.");
                    Thread.Sleep(300);
                    enemies.RemoveAt(enemy_index);
                    enemy_index--;
                    if (enemies.Count() == 0)
                    {
                        Console.WriteLine("You win!");
                        Thread.Sleep(300);
                        text.text_id = text.next_id;
                        state = STATE.Dialogue;
                    }
                }
            }
            else if (defending_with < 0)
            {
                player_take_damage(-defending_with);
            }

            if (state == STATE.Player_Choose_Defend)
            {
                enemy_index++;
                state = STATE.Enemy_Attack;
                if (enemy_index >= enemies.Count())
                {
                    state = STATE.Player_Choose_Combat;
                    enemy_index = 0;
                }
            }
            /*enemies[option].hp -= player.atk - enemies[option].def;
            if (enemies[option].hp <= 0)
            {
                Console.WriteLine($"You attacked {enemies[option].name}. It is now dead.");
                enemies.RemoveAt(option);
            }
            else
            {
                Console.WriteLine($"You attacked {enemies[option].name}. Its hp is now {enemies[option].hp}.");
            }
            if (enemies.Count() == 0)
            {
                Console.WriteLine("You win!");
                Thread.Sleep(300);
                text_id = next_id;
                state = STATE.Dialogue;
            }
            else
            {
                state = STATE.Enemy_Attack;
            }*/
        }


        private void player_attack() 
        {

            Console.WriteLine("Would you like to:\n1. Attack an enemy\n2. Attempt to escape with a speed roll of 8+");

            int option = verify_int_input(2);

            if (option == 0)
            {
                state = STATE.Enemy_Attack;

                Console.WriteLine("Which enemy would you like to attack?");
                for (int i = 0; i < enemies.Count; i++)
                {
                    Console.WriteLine($"{i + 1}.  {enemies[i].name}");
                }

                option = verify_int_input(enemies.Count());

               
                int defending_with = player.check_str(rnd);
                Console.WriteLine($"You rolled {defending_with}.");
                dmg = enemies[option].check_def(rnd);
                Console.WriteLine($"The enemy rolled {dmg}.");

                defending_with -= dmg;

                defending_with /= 2;

                if (defending_with > 0)
                {
                    Console.WriteLine($"You win this round. The enemy takes {defending_with} damage.");
                    Thread.Sleep(300);
                    if (enemies[enemy_index].take_dmg(defending_with))
                    {
                        Console.WriteLine($"You killed {enemies[enemy_index].name}.");
                        Thread.Sleep(300);
                        enemies.RemoveAt(enemy_index);
                        enemy_index--;
                        if (enemies.Count() == 0)
                        {
                            Console.WriteLine("You win!");
                            Thread.Sleep(300);
                            text.text_id = text.next_id;
                            state = STATE.Dialogue;
                        }
                    }
                }
                else if (defending_with < 0)
                {
                    player_take_damage(-defending_with);
                }

            }
            else
            {
                int attempt = player.check_spd(rnd);
                if (attempt > 8)
                {
                    Console.WriteLine($"You succeeded with a roll of {attempt}.");
                    text.text_id = text.next_id;
                    state = STATE.Dialogue;
                }
                else
                {
                    Console.WriteLine($"You failed with a roll of {attempt}.");
                    state = STATE.Enemy_Attack;
                }
            }
        }
    }
}
