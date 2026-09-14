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
        public List<bool> pauses = [];
        public void add(string txt, bool pause = true)
        {
            text_queue.Add(txt);
            pauses.Add(pause);
        }
        public void write_text(int i)
        {
            Console.WriteLine(text_queue[i]);
            if (pauses[i] == true)
            {
                Thread.Sleep(text_queue[i].Length * 100);
            }
        }
    }
}
