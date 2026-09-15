using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment1
{
    public class Enemy(string in_name)
    {

        public string name = in_name;
        public int str = 4;
        public int def = 4;
        public int spd = 4;
        public int knowledge = 4;
        public int charisma = 4;
        public int sanity = 4;
        public void setup()
        {
            switch (name)
            {
                case "Zombie":
                    str = 4;
                    def = 4;
                    spd = 4;
                    knowledge = 4;
                    charisma = 4;
                    sanity = 4;
                    break;
            }
        }

        public bool take_dmg(int dmg)
        {
            while(dmg > 0) 
            {
                if (str > def && str > spd)
                {
                    str--;
                }
                else if (def > str && def > spd)
                {
                    def--;
                }
                else 
                {
                    spd--;
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
