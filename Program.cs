using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Windows.Input;

namespace snakeIncSharp
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Snake Game = new Snake(25,25);
            
            Game.Play();
            
            Console.ReadLine();
        }
    }
}
