using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace assignment1
{
    public class Player
    {
        public string name = "";
        bool to_die = false;

        public List<Item> items = [];

        public List<Party_member> party = [];

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

        public int check_str(Random rnd)
        {
            return rnd.Next(0, get_str() * 2 + 1);
        }
        public int check_spd(Random rnd)
        {
            return rnd.Next(0, get_spd() * 2 + 1);
        }
        public int check_def(Random rnd)
        {
            return rnd.Next(0, get_def() * 2 + 1);
        }
        public int check_knowledge(Random rnd)
        {
            return rnd.Next(0, get_knowledge() * 2 + 1);
        }
        public int check_charisma(Random rnd)
        {
            return rnd.Next(0, get_charisma() * 2 + 1);
        }
        public int check_sanity(Random rnd)
        {
            return rnd.Next(0, get_sanity() * 2 + 1);
        }


        public bool check_to_die()
        {
            bool check = to_die;
            to_die = false;
            return check;
        }

        public void stat_return()
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

        /*public int hp = 30;
        public int max_hp = 30;
        public int atk = 5;
        public int def = 3;
        public int spd = 5;*/
    }
}
