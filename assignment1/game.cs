using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static assignment1.Program;

namespace assignment1
{
    public class Game
    {
        string text_id = "start";
        string checkpoint_id = "start";
        string next_id = "start";
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

        Map map = new Map();

        public void setup() 
        {
            player.update_items();
            map.setup();
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
                case STATE.Explore:
                    explore();
                    break;
            }
        }

        private int verify_int_input(int max, int min = 0) 
        {
            bool input_valid = false;
            int option = -1;
            while (!input_valid)
            {
                option = -1;
                string input = Console.ReadLine();
                try
                {
                    option = Int32.Parse(input) - 1;
                }
                catch
                {
                }

                if (option >= max || option < 0)
                {
                    Console.WriteLine("Please input a valid number.");
                }
                else
                {
                    input_valid = true;
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
                text_id = checkpoint_id;
                player.stat_return();
            }
        }

        private void run_dialogue() 
        {
            skip = false;
            text.option_ids = [];
            text.option_names = [];
            text.text_queue = [];
            text.pauses = [];
            int take_input_index = -1;
            string input_variable = "";
            STATE change_state = STATE.Dialogue;
            switch (text_id)
            {
                case "start":
                    text.add("Hello.", 250);
                    text.add("What is your name?");
                    take_input_index = 1;
                    input_variable = "name";
                    text.add("What a nice name.");
                    skip = true;
                    next_id = "start part 2";
                    break;
                case "start part 2":
                    text.add(". . . sorry what was your name again?");
                    text.option_ids = ["XDDCC", "name", "ena"];
                    text.option_names = ["XDDCC", player.name, "ENA"];
                    break;
                case "name":
                    text.add($"Ah yes, {player.name}. What a lovely name.");
                    skip = true;
                    next_id = "forget name";
                    break;
                case "XDDCC":
                    text.add($"Ah yes, XDDCC. What a lovely name.");
                    skip = true;
                    next_id = "forget name";
                    break;
                case "ENA":
                    text.add($"Ah yes, ENA. What a lovely name.");
                    skip = true;
                    next_id = "forget name";
                    break;
                case "forget name":
                    text.add("I won't remember that.");
                    text.add("And neither will anyone else.");
                    text.add("Goodbye, Kaneis."); // this is supposed to be greek for nobody anglocized, will have to revise based on pronunciation later.
                    
                    break;
            }

            for (int i = 0; i < text.text_queue.Count; i++)
            {
                text.write_text(i, take_input_index == i);
                if (take_input_index == i)
                {
                    switch (input_variable)
                    {
                        case "name":
                            Console.ReadLine();
                            bool input_valid = false;
                            string input = "";
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
                            string first_letter = player.name[0].ToString();
                            player.name = player.name.Remove(0, 1);
                            player.name = player.name.ToLower();
                            first_letter = first_letter.ToUpper();
                            player.name = first_letter + player.name;
                            break;
                    }
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

                    text_id = text.option_ids[verify_int_input(text.option_names.Count())];
                }
                else 
                {
                    text_id = next_id;
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
            dmg = rnd.Next(0, enemies[enemy_index].str * 2 + 1);
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
                defending_with = player.get_str();
            }
            else
            {
                defending_with = player.get_def();
            }

            defending_with = rnd.Next(0, defending_with * 2 + 1);

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
                        text_id = next_id;
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

               
                int defending_with = rnd.Next(0, player.get_str() * 2 + 1);
                Console.WriteLine($"You rolled {defending_with}.");
                dmg = rnd.Next(0, enemies[option].def * 2 + 1);
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
                            text_id = next_id;
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
                int attempt = rnd.Next(0, player.get_spd() * 2 + 1);
                if (attempt > 8)
                {
                    Console.WriteLine($"You succeeded with a roll of {attempt}.");
                    text_id = next_id;
                    state = STATE.Dialogue;
                }
                else
                {
                    Console.WriteLine($"You failed with a roll of {attempt}.");
                    state = STATE.Enemy_Attack;
                }
            }
        }

        private void explore() 
        {
            map.draw();
        }
    }
}
