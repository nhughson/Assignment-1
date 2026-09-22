using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


namespace assignment1
{
    public class Player
    {
        public int verify_int_input(int max, int min = 0)
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


        public string name = "";
        bool to_die = false;

        public List<Item> items = [];

        public List<Party_member> party = [];


        public List<bool> checks = [false, false, false, false, false, false, false, false, false];

        string checkpoint_id;

        /* 0 - lvl 1 enter
         * 1 - lvl 2 enter
         * 2 - lvl 3 enter
         * 3 - lvl 1 enter
         * 4 - lvl 2 enter
         * 5 - lvl 3 enter
         * 6 - rolls explained
         * 7 - lvl 3 - maze - physical trait
         * 8 - lvl 3 - maze - mental trait
         */

        public int x = 3;
        public int y = 6;

        List<int> str_list = [1, 2, 4, 4, 5, 6, 6, 7, 8];
        List<int> spd_list = [1, 2, 4, 4, 5, 6, 6, 7, 8];
        List<int> def_list = [1, 2, 4, 4, 5, 6, 6, 7, 8];
        List<int> knowledge_list = [1, 2, 4, 4, 5, 6, 6, 7, 8];
        List<int> charisma_list = [1, 2, 4, 4, 5, 6, 6, 7, 8];
        List<int> sanity_list = [1, 2, 4, 4, 5, 6, 6, 7, 8];

        int str_index = 4;
        int spd_index = 4;
        int def_index = 4;
        int knowledge_index = 4;
        int charisma_index = 4;
        int sanity_index = 4;

        int base_str_index = 4;
        int base_spd_index = 4;
        int base_def_index = 4;
        int base_knowledge_index = 4;
        int base_charisma_index = 4;
        int base_sanity_index = 4;

        public int roll = 0;

        public int get_str()
        {
            return str_list[str_index];
        }
        public int get_spd()
        {
            return spd_list[spd_index];
        }
        public int get_def()
        {
            return def_list[def_index];
        }
        public int get_knowledge()
        {
            return knowledge_list[knowledge_index];
        }
        public int get_charisma()
        {
            return charisma_list[charisma_index];
        }
        public int get_sanity()
        {
            return sanity_list[sanity_index];
        }

        public void change_str(int amount)
        {
            str_index += amount;
            if (str_index < 0)
            {
                to_die = true;
                str_index = 0;
            }
            else if (str_index >= str_list.Count())
            {
                str_index = str_list.Count();
            }
        }
        public void change_spd(int amount)
        {
            spd_index += amount;
            if (spd_index < 0)
            {
                to_die = true;
                spd_index = 0;
            }
            else if (spd_index >= spd_list.Count())
            {
                spd_index = spd_list.Count();
            }
        }
        public void change_def(int amount)
        {
            def_index += amount;
            if (def_index < 0)
            {
                to_die = true;
                def_index = 0;
            }
            else if (def_index >= def_list.Count())
            {
                def_index = def_list.Count();
            }
        }
        public void change_knowledge(int amount)
        {
            knowledge_index += amount;
            if (knowledge_index < 0)
            {
                to_die = true;
                knowledge_index = 0;
            }
            else if (knowledge_index >= knowledge_list.Count())
            {
                knowledge_index = knowledge_list.Count();
            }
        }
        public void change_charisma(int amount)
        {
            charisma_index += amount;
            if (charisma_index < 0)
            {
                to_die = true;
                charisma_index = 0;
            }
            else if (charisma_index >= charisma_list.Count())
            {
                charisma_index = charisma_list.Count();
            }
        }
        public void change_sanity(int amount)
        {
            sanity_index += amount;
            if (sanity_index < 0)
            {
                to_die = true;
                sanity_index = 0;
            }
            else if (sanity_index >= sanity_list.Count())
            {
                sanity_index = sanity_list.Count();
            }
        }

        public bool take_physical_dmg(int amt, Text text) 
        {
            while (amt > 0)
            {

                Console.WriteLine($"What would you like to take damage in?\n1. Might ({get_str()})\n2. Speed ({get_spd()})\n3. Defense ({get_def()})");

                int option = verify_int_input(3);

                Console.WriteLine("How much damage would you like to take?");

                int option_2 = verify_int_input(amt);

                amt -= option_2+1;
                if (option == 0)
                {
                    change_str(-option_2);
                }
                else if (option == 1)
                {
                    change_spd(-option_2);
                }
                else if (option == 2)
                {
                    change_def(-option_2);
                }
            }

            if (check_to_die())
            {
                Console.WriteLine("You have died. Returning to checkpoint.");
                Thread.Sleep(300);
                text.text_id = checkpoint_id;
                stat_reset();
                return true;
            }
            return false;
        }
        public bool take_mental_dmg(int amt, Text text) 
        {
            while (amt > 0)
            {

                Console.WriteLine($"What would you like to take damage in?\n1. Knowledge ({get_knowledge()})\n2. Charisma ({get_charisma()})\n3. Sanity ({get_sanity()})");

                int option = verify_int_input(3);

                Console.WriteLine("How much damage would you like to take?");

                int option_2 = verify_int_input(amt);

                amt -= option_2+1;
                if (option == 0)
                {
                    change_knowledge(-option_2);
                }
                else if (option == 1)
                {
                    change_charisma(-option_2);
                }
                else if (option == 2)
                {
                    change_sanity(-option_2);
                }
            }

            if (check_to_die())
            {
                Console.WriteLine("You have died. Returning to checkpoint.");
                Thread.Sleep(300);
                text.text_id = checkpoint_id;
                stat_reset();
                return true;
            }
            return false;
        }
        public void gain_physical_trait(int amt) 
        {
            while (amt > 0)
            {

                Console.WriteLine($"Where would you like to spend your point(s)?\n1. Might ({get_str()})\n2. Speed ({get_spd()})\n3. Defense ({get_def()})");

                int option = verify_int_input(3);

                Console.WriteLine("How many points would you like to spend?");

                int option_2 = verify_int_input(amt);

                amt -= option_2+1;
                if (option == 0)
                {
                    change_str(option_2);
                }
                else if (option == 1)
                {
                    change_spd(option_2);
                }
                else if (option == 2)
                {
                    change_def(option_2);
                }
            }
        }
        public void gain_mental_trait(int amt) 
        {
            while (amt > 0)
            {

                Console.WriteLine($"Where would you like to spend your point(s)?\n1. Knowledge ({get_knowledge()})\n2. Charisma ({get_charisma()})\n3. Sanity ({get_sanity()})");

                int option = verify_int_input(3);

                Console.WriteLine("How many points would you like to spend?");

                int option_2 = verify_int_input(amt);

                amt -= option_2+1;
                if (option == 0)
                {
                    change_knowledge(option_2);
                }
                else if (option == 1)
                {
                    change_charisma(option_2);
                }
                else if (option == 2)
                {
                    change_sanity(option_2);
                }
            }

        }

        public int check_str(Random rnd)
        {
            int amt = 0;
            int max = get_str();
            for (int i = 0; i < max; i++) 
            {
                amt += rnd.Next(0, 3);
            }
            return amt;
        }
        public int check_spd(Random rnd)
        {
            int amt = 0;
            int max = get_spd();
            for (int i = 0; i < max; i++)
            {
                amt += rnd.Next(0, 3);
            }
            return amt;
        }
        public int check_def(Random rnd)
        {
            int amt = 0;
            int max = get_def();
            for (int i = 0; i < max; i++)
            {
                amt += rnd.Next(0, 3);
            }
            return amt;
        }
        public int check_knowledge(Random rnd)
        {
            int amt = 0;
            int max = get_knowledge();
            for (int i = 0; i < max; i++)
            {
                amt += rnd.Next(0, 3);
            }
            return amt;
        }
        public int check_charisma(Random rnd)
        {
            int amt = 0;
            int max = get_charisma();
            for (int i = 0; i < max; i++)
            {
                amt += rnd.Next(0, 3);
            }
            return amt;
        }
        public int check_sanity(Random rnd)
        {
            int amt = 0;
            int max = get_sanity();
            for (int i = 0; i < max; i++)
            {
                amt += rnd.Next(0, 3);
            }
            return amt;
        }


        public bool check_to_die()
        {
            bool check = to_die;
            to_die = false;
            return check;
        }

        public void stat_reset()
        {
            str_index = base_str_index;
            spd_index = base_spd_index;
            def_index = base_def_index;
            knowledge_index = base_knowledge_index;
            charisma_index = base_charisma_index;
            sanity_index = base_sanity_index;
        }

        public void update_items()
        {
            for (int i = 0; i <= items.Count() - 1; i++)
            {
                items[i].setup();
            }
        }

        public bool inventory_check(string in_name)
        {
            for (int i = 0; i < items.Count(); i++)
            {
                if (items[i].name.Equals(in_name))
                {
                    return true;
                }
            }
            return false;
        }

        public void inventory_add(Item item)
        {
            items.Add(item);
            update_items();
        }

        public string check_to_add_item(string id,string succeed_message,string fail_message) 
        {
            if (!inventory_check(id))
            {
                inventory_add(new Item(id));
                return succeed_message;
            }
            return fail_message;
        }

        public string companion_message(string dog_text, string cat_text, string rat_text, string crow_text) 
        {
            string pet_name = party[party.Count() - 1].name;
            switch (party[party.Count() - 1].type)
            {
                case "Dog":
                    return dog_text;
                case "Cat":
                    return cat_text;
                case "Rat":
                    return rat_text;
                case "Crow":
                    return crow_text;
            }
            return "";
        }

        public void set_checkpoint(Text text)
        {
            checkpoint_id = text.text_id;
        }
        public void set_checkpoint(string id)
        {
            checkpoint_id = id;
        }

        /*public int hp = 30;
        public int max_hp = 30;
        public int atk = 5;
        public int def = 3;
        public int spd = 5;*/
    }
}
