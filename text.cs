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


        List<string> screens = [];
        List<int> pauses = [];


        public string text_id = "start";
        public string next_id = "start";

        int max_length = 76;
        bool debug = false;


        public void reset() 
        {
            text_queue = [];
            pauses = [];
            screens = [@"
   .        .        .        .        .        .        .        .        .
     .         .         .        _......____._        .         .
   .          .          . ..--'"""" .           """"""""""""---...          .
                   _...--""""        ................       `-.              .
                .-'        ...:'::::;:::%:.::::::_;;:...     `-.
             .-'       ..::::'''''   _...---'"""""""":::+;_::.      `.      .
  .        .' .    ..::::'      _.-""""               :::)::.       `.
         .      ..;:::'     _.-'         .             f::'::    o  _
        /     .:::%'  .  .-""                        .-.  ::;;:.   /"" ""x
  .   .'  """"::.::'    .-""     _.--'""""""-.           (   )  ::.::  |_.-' |
     .'    ::;:'    .'     .-"" .d@@b.   \    .    . `-'   ::%::   \_ _/    .
    .'    :,::'    /   . _'    8@@@@8   j      .-'       :::::      "" o
    | .  :.%:' .  j     (_)    `@@@P'  .'   .-""         ::.::    .  f
    |    ::::     (        -..____...-'  .-""          .::::'       /
.   |    `:`::    `.                ..--'        .  .::'::   .    /
    j     `:::::    `-._____...---""""             .::%:::'       .'  .
     \      ::.:%..             .       .    ...:,::::'       .'
 .    \       `:::`:..                ....::::.::::'       .-'          .
       \    .   ``:::%::`::.......:::::%::.::::''       .-'
      . `.        . ``::::::%::::.::;;:::::'''      _.-'          .
  .       `-..     .    .   ````'''''         . _.-'     .          .
         .    """"--...____    .   ______......--' .         .         .
  .        .        .    """"""""""""""""     .        .        .        .        .
"];
        }

        public void add(string txt, int pause = 40)
        {
            if (txt != "")
            {
                screens.Add(screens[screens.Count() - 1]);
                text_queue.Add(txt);
                pauses.Add(pause);
            }
        }

        public void write_screen(int i) 
        {
            Console.Clear();
            
            Console.Write(screens[i]);
        }

        public void write_raw(string text, int pause = 40) 
        {
            int sleep_time = 0;
            
            int line_break = 0;
            for (int draw_char = 0; draw_char < text.Length; draw_char++)
            {
                if (line_break % max_length == 0 && line_break != 0)
                {
                    Console.Write('\n');
                }
                sleep_time = pause;
                Console.Write(text[draw_char]);
                if (text[draw_char] == '.' || text[draw_char] == '!' || text[draw_char] == '?' || text[draw_char] == ',' || text[draw_char] == ':' || text[draw_char] == ';')
                {
                    sleep_time *= 3;
                }
                if (text[draw_char] == '\n')
                {
                    line_break = 0;
                }
                Thread.Sleep(sleep_time);
                line_break++;
            }
            Console.Write('\n');
        }
        public void write_raw(string text, bool new_line, int pause = 40) 
        {
            int sleep_time = 0;
            
            int line_break = 0;
            for (int draw_char = 0; draw_char < text.Length; draw_char++)
            {
                if (line_break % max_length == 0 && line_break != 0)
                {
                    Console.Write('\n');
                }
                sleep_time = pause;
                Console.Write(text[draw_char]);
                if (text[draw_char] == '.' || text[draw_char] == '!' || text[draw_char] == '?' || text[draw_char] == ',' || text[draw_char] == ':' || text[draw_char] == ';')
                {
                    sleep_time *= 3;
                }
                if (text[draw_char] == '\n')
                {
                    line_break = 0;
                }
                Thread.Sleep(sleep_time);
                line_break++;
            }
            if (new_line)
            {
                Console.Write('\n');
            }
        }

        public void write_text(int i, bool input)
        {

            Console.Write("\n");

            write_screen(i);
            write_raw(text_queue[i], pauses[i]);

            if (!input)
            {

                Thread.Sleep(pauses[i] * 3);
                if (!(option_ids.Count() != 0 && i == text_queue.Count()-1))
                {
                    if (!debug)
                    {
                        Console.Write("Press any key to continue:   ");
                        ConsoleKeyInfo info = Console.ReadKey();
                        if (info.Key == ConsoleKey.F6) 
                        {
                            debug = true;
                        }
                    }
                    else 
                    {
                        Console.Write("Press any key to continue:   ");
                        string info = Console.ReadLine();
                        if (info != "") 
                        {
                            text_id = info;
                            next_id = info;
                            for (int j = 0; j < option_ids.Count() - 1; j++) 
                            {
                                option_ids[j] = info;
                            }
                        }
                    }
                }
            }
        }
    }
}
