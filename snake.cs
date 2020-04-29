using System;
using System.Collections.Generic;
using System.Threading;

namespace snakeIncSharp
{
    class Snake
    {

        int Width, Hieght;
        int Fruit_x, Fruit_y;
        int SnakeHead_x, SnakeHead_y;
        LinkedList<int[]> SnakeShape = new LinkedList<int[]>();
        int Score = 0;
        bool GameOver = false;
        enum direction
        {
            up,
            down,
            right,
            left
        }
        public Snake(int x, int y)
        {
            this.Width = 2 * x;
            this.Hieght = y;
            this.SnakeHead_x = Width / 2;
            this.SnakeHead_y = Hieght / 2;
            int[] Head = { this.SnakeHead_x, this.SnakeHead_y };
            SnakeShape.AddLast(Head);
        }

        private void UpdateSnakeShape(direction moveto)
        {
            switch (moveto)
            {
                case direction.up:
                    SnakeHead_y--;
                    break;
                case direction.down:
                    SnakeHead_y++;
                    break;
                case direction.right:
                    SnakeHead_x++;
                    break;
                case direction.left:
                    SnakeHead_x--;
                    break;
                default:
                    break;
            }

            // remove the tale
            int[] tale = SnakeShape.Last.Value;
            Console.SetCursorPosition(tale[0], tale[1]);
            Console.Write(' ');

            //move the snake to the next position by its coordinates
            // put first let's check if its head get crashed into the walls or its body
            int[] NewHead = { SnakeHead_x, SnakeHead_y };
            if (NewHead[0] == Width || NewHead[0] <= 0 || NewHead[1] > Hieght || NewHead[1] == 0)
                GameOver = true;
            foreach (int[] item in SnakeShape)
            {
                if (NewHead[0] == item[0] && NewHead[1] == item[1])
                {
                    GameOver = true;
                }
            }
            SnakeShape.AddFirst(NewHead);
            if (!(Fruit_x == SnakeHead_x && Fruit_y == SnakeHead_y))
                // remeove the old coordinates of the tale
                // but only if the snake didn't catch the fruit
                SnakeShape.Remove(SnakeShape.Last);
            else
            {
                // if the fruit has been eaten then put another one and update the score
                PrintFruit();
                Score += 10;
            }

        }
        private void PrintSnakeShape()
        {
            // we have every place of the snake body so lets draw the whole body
            foreach (int[] item in SnakeShape)
            {
                Console.SetCursorPosition(item[0], item[1]);
                Console.Write('*');
            }

        }
        private void PrintScore()
        {
            // print the current score at appropriate
            Console.SetCursorPosition(0, Hieght + 3);
            Console.WriteLine("Score = " + Score);
        }
        public void PrintBoarder()
        {
            //draw the walls of the game
            Console.Clear();
            Console.SetCursorPosition(0, 0);

            //top
            for (int i = 0; i <= Width; i++)
                Console.Write('+');
            Console.WriteLine();

            //both parallel sides
            for (int i = 0; i < Hieght; i++)
            {
                for (int j = 0; j <= Width; j++)
                    if (j == 0 || j == Width)
                        Console.Write('+');
                    else Console.Write(' ');
                Console.WriteLine();
            }
            //buttom
            for (int i = 0; i <= Width; i++)
                Console.Write('+');
        }
        public void PrintFruit()
        {
            //clear the old fruit and put a new one
            Console.SetCursorPosition(Fruit_x, Fruit_y);
            Console.WriteLine(" ");
            Random r = new Random();
            Fruit_x = r.Next(2, Width - 2);
            Fruit_y = r.Next(2, Hieght - 2);
            Console.SetCursorPosition(Fruit_x, Fruit_y);
            Console.Write('@');
        }
        public void Play()
        {
            PrintBoarder();
            PrintFruit();
            // we ned to know the direction of the distination
            // the distination doesn't change unless the user do
            //but now let's start with the snake moves up 
            direction change = direction.up;
            while (!GameOver)
            {
                Thread.Sleep(100);
                //if the user entered any key when playing 
                if (Console.KeyAvailable)
                {
                    // let's see what was the key for
                    ConsoleKey dir = Console.ReadKey().Key;
                    switch (dir)
                    {
                        case ConsoleKey.RightArrow:
                            change = direction.right;
                            break;
                        case ConsoleKey.LeftArrow:
                            change = direction.left;
                            break;

                        case ConsoleKey.UpArrow:
                            change = direction.up;
                            break;
                        case ConsoleKey.DownArrow:
                            change = direction.down;
                            break;
                    }

                }
                UpdateSnakeShape(change);
                PrintSnakeShape();
                PrintScore();
            }
            Console.SetCursorPosition(Width / 2, Hieght / 2);
            Console.WriteLine("Game over");
        }//done
    }
}
