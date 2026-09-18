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

        public bool take_dmg(int dmg)
        {
            while (dmg > 0)
            {
                if (str > def && str > spd)
                {
                    str -= 2;
                }
                else if (def > str && def > spd)
                {
                    def -= 2;
                }
                else
                {
                    spd -= 2;
                }
                dmg--;
            }
            if (def <= 0 || str <= 0 || spd <= 0)
            {
                return true;
            }
            return false;
        }


        public int check_str(Random rnd)
        {
            return rnd.Next(0, str * 2 + 1);
        }
        public int check_spd(Random rnd)
        {
            return rnd.Next(0, spd * 2 + 1);
        }
        public int check_def(Random rnd)
        {
            return rnd.Next(0, def * 2 + 1);
        }
        public int check_knowledge(Random rnd)
        {
            return rnd.Next(0, knowledge * 2 + 1);
        }
        public int check_charisma(Random rnd)
        {
            return rnd.Next(0, charisma * 2 + 1);
        }
        public int check_sanity(Random rnd)
        {
            return rnd.Next(0, sanity * 2 + 1);
        }
    }
}
