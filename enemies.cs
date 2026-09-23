using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment1
{
    public class Enemy(string in_name, string in_type)
    {
        public string name = in_name;
        public string type = in_type;
        public string desc = "";
        int str = 4;
        int def = 4;
        int spd = 4;
        int knowledge = 4;
        int charisma = 4;
        int sanity = 4;
        
        public void setup()
        {
            switch (type)
            {
                case "lvl 1 - entity 1":
                    str = 3;
                    def = 3;
                    spd = 3;
                    knowledge = 3;
                    charisma = 3;
                    sanity = 3;
                    desc = "A strange, zombie-like creature. It's hands are frostbitten and its eyes are sunken into dark sockets.";
                    break;
            }
        }

        public bool take_dmg(int dmg)
        {
            while(dmg > 0) 
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
