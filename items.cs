using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment1
{
    public class Item(string in_name)
    {
        public string name = in_name;
        public int bonus_str = 0;
        public int bonus_spd = 0;
        public int bonus_def = 0;
        public int bonus_knowledge = 0;
        public int bonus_sanity = 0;
        public int bonus_charisma = 0;
        public void setup() 
        {
            switch (name) 
            {
                case "Stick":
                    bonus_str = 1;
                    break;
                case "Bell":
                    bonus_str = 3;
                    bonus_spd = -1;
                    bonus_sanity = -1;
                    break;
                case "Plank":
                    bonus_str = 2;
                    bonus_spd = -3;
                    break;
                case "Dagger":
                    bonus_str = 2;
                    break;
                case "Minotaur Head":
                    bonus_def = 2;
                    bonus_sanity = 2;
                    break;
                case "Holy Grail":
                    bonus_str = 3;
                    bonus_spd = 3;
                    bonus_def = 3;
                    bonus_knowledge = 3;
                    bonus_sanity = 3;
                    bonus_charisma = 3;
                    break;
            }
        }
    }
}
