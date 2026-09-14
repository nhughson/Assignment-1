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
            }
        }
    }
}
