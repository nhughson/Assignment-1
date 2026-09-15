using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment1
{
    public class Text
    {
        public List<string> option_ids = [];
        public List<string> option_names = [];
        public List<string> text_queue = [];
        public List<int> pauses = [];
        public void add(string txt, int pause = 40)
        {
            text_queue.Add(txt);
            pauses.Add(pause);
        }
        public void write_text(int i, bool input)
        {
            Console.Clear();
            int sleep_time = 0;
            for (int draw_char = 0; draw_char < text_queue[i].Length; draw_char++)
            {
                Console.Write(text_queue[i][draw_char]);
                sleep_time = pauses[i];
                if (text_queue[i][draw_char] == '.' || text_queue[i][draw_char] == '!' || text_queue[i][draw_char] == '?' || text_queue[i][draw_char] == ',' || text_queue[i][draw_char] == ':' || text_queue[i][draw_char] == ';')
                {
                    sleep_time *= 3;
                }
                Thread.Sleep(sleep_time);
            }
            Console.Write("\n");
            if (!input)
            {
                Thread.Sleep(pauses[i] * 3);
                if (!(option_ids.Count() != 0 && i == text_queue.Count()-1))
                {
                    Console.Write("Press Enter to continue:   ");
                    Console.ReadLine();
                }
            }
        }
    }
}
