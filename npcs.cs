using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace assignment1
{
    public class NPC(string in_name)
    {
        public string name = in_name;
    }

    public class Party_member(string in_name) 
    {
        public string name = in_name;
        public string type = in_name;
        int str = 4;
        int def = 4;
        int spd = 4;
        int knowledge = 4;
        int charisma = 4;
        int sanity = 4;
        public bool stunned = false;

        public void setup(Player player, string new_name = "")
        {
            switch (name)
            {
                case "Dog":
                    str = 4;
                    def = 6;
                    spd = 3;
                    knowledge = 2;
                    charisma = 4;
                    sanity = 5;
                    break;
                case "Cat":
                    str = 3;
                    def = 5;
                    spd = 5;
                    knowledge = 4;
                    charisma = 3;
                    sanity = 4;
                    break;
                case "Crow":
                    str = 4;
                    def = 4;
                    spd = 5;
                    knowledge = 5;
                    charisma = 3;
                    sanity = 3;
                    break;
                case "Rat":
                    str = 6;
                    def = 3;
                    spd = 4;
                    knowledge = 4;
                    charisma = 3;
                    sanity = 4;
                    break;
            }
            if (new_name != "")
            {
                name = new_name;
            }
            player.party.Add(this);
        }


        public int check_str(Random rnd)
        {
            int amt = 0;
            int max = str;
            for (int i = 0; i < max; i++)
            {
                amt += rnd.Next(0, 3);
            }
            return amt;
        }
        public int check_spd(Random rnd)
        {
            int amt = 0;
            int max = spd;
            for (int i = 0; i < max; i++)
            {
                amt += rnd.Next(0, 3);
            }
            return amt;
        }
        public int check_def(Random rnd)
        {
            int amt = 0;
            int max = def;
            for (int i = 0; i < max; i++)
            {
                amt += rnd.Next(0, 3);
            }
            return amt;
        }
        public int check_knowledge(Random rnd)
        {
            int amt = 0;
            int max = knowledge;
            for (int i = 0; i < max; i++)
            {
                amt += rnd.Next(0, 3);
            }
            return amt;
        }
        public int check_charisma(Random rnd)
        {
            int amt = 0;
            int max = charisma;
            for (int i = 0; i < max; i++)
            {
                amt += rnd.Next(0, 3);
            }
            return amt;
        }
        public int check_sanity(Random rnd)
        {
            int amt = 0;
            int max = sanity;
            for (int i = 0; i < max; i++)
            {
                amt += rnd.Next(0, 3);
            }
            return amt;
        }
    }
}
