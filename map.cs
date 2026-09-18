using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment1
{
    enum ROOM_TYPE 
    {
        blank,
        occurence,
        item,
    }

    public class Room
    {
        string name = "";
        char discovered_char = 'O';
        bool discovered = false;

        public char get_char() 
        {
            if (discovered) 
            {
                return discovered_char;
            }
            return '#';
        }

        ROOM_TYPE type = ROOM_TYPE.blank;

    }
    public class Map(int in_width = 7, int in_height = 5)
    {
        int width = in_width;
        int height = in_height;
        Room[,] map = new Room[in_height, in_width];
        public void setup() 
        {
            
            for (int i = 0; i < height; i++) 
            {
                for (int j = 0; j < width; j++) 
                {
                    Room room = new Room();
                    map[i,j] = room;
                }
            }
        }
        public void draw(Player player) 
        {
            Console.Clear();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (!(player.x == j && player.y == i))
                    {
                        Console.Write(map[i, j].get_char());
                    }
                    else 
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.Write('P');
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                }
                Console.Write('\n');
            }
            if (player.y == 6) 
            {
                for (int i = 0; i < player.x; i++)
                {
                    Console.Write(' ');
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.Write('W');
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            Console.Read();
        }
    }
}
