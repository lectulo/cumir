using System;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using cumir;

namespace cumir
{
    public class Program
    {
        static Thread tr = Thread.CurrentThread;
        static bool maze = true;                                                        // create maze?
        const string path = @"C:\s.txt";                                                // path to map
        const int mazeX = 0;
        const int mazeY = 0;



        public static class mp                                                          // maps's class
        {
            public const int x0 = 25;                                                   // map width
            public const int y0 = 13;                                                   // map height

            public static string[,] map = new string[x0, y0];
            public static ConsoleColor[,] colMap = new ConsoleColor[x0, y0];
        }

        public class vec
        {
            public int x;
            public int y;

            public vec()
            {
                x = 0;
                y = 0;
            }
            public vec(int X, int Y)
            {
                x = X;
                y = Y;
            }

            public static bool operator ==(vec vc1, vec vc2)
            {
                return (vc1.x == vc2.x && vc1.y == vc2.y);
            }
            public static bool operator !=(vec vc1, vec vc2)
            {
                return (vc1.x != vc2.x || vc1.y != vc2.y);
            }
        }

        public class bot                                                                // robot's class
        {
            ConsoleColor paintCol = ConsoleColor.Blue;
            ConsoleColor botCol = ConsoleColor.Green;                                   // color of robot
            string model = "@";                                                         // robot's texture
            int st = 100;                                                               // delay time

            public vec pos = new vec();
            bool ok = true;
            bool contr = false;

            public bot()
            {
                Console.SetCursorPosition(pos.x + 1, pos.y + 1);
                Console.Write(model);
            }
            public bot(int x1, int y1)
            {
                pos.x = x1;
                pos.y = y1;
                Console.SetCursorPosition(x1 + 1, y1 + 1);
                Console.Write(model);
            }

            public void Paint()
            {
                Console.SetCursorPosition(pos.x + 1, pos.y + 1);
                Console.BackgroundColor = paintCol;
                Console.ForegroundColor = botCol;
                Console.Write(model);
                Console.BackgroundColor = ConsoleColor.Black;
                Program.mp.colMap[pos.x, pos.y] = paintCol;
            }

            public void Paint(ConsoleColor cc)
            {
                Console.SetCursorPosition(pos.x + 1, pos.y + 1);
                Console.BackgroundColor = cc;
                Console.ForegroundColor = botCol;
                Console.Write(model);
                Console.BackgroundColor = ConsoleColor.Black;
                Program.mp.colMap[pos.x, pos.y] = cc;
            }

            public void Up()
            {
                Console.BackgroundColor = Program.mp.colMap[pos.x, pos.y];
                Console.ForegroundColor = botCol;
                Console.SetCursorPosition(pos.x + 1, pos.y + 1);

                if (pos.y > 0 && Program.mp.map[pos.x, pos.y - 1] == " ")
                {
                    Console.Write(" ");
                    Console.SetCursorPosition(pos.x + 1, pos.y);
                    pos.y--;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    ok = false;
                }

                Console.BackgroundColor = Program.mp.colMap[pos.x, pos.y];
                Console.Write(model);
                Console.BackgroundColor = ConsoleColor.Black;
                if (!ok) tr.Suspend();
                if (!contr) Thread.Sleep(st);
            }

            public void Down()
            {
                Console.BackgroundColor = Program.mp.colMap[pos.x, pos.y];
                Console.ForegroundColor = botCol;
                Console.SetCursorPosition(pos.x + 1, pos.y + 1);

                if (pos.y < Program.mp.y0 - 1 && Program.mp.map[pos.x, pos.y + 1] == " ")
                {
                    Console.Write(" ");
                    Console.SetCursorPosition(pos.x + 1, pos.y + 2);
                    pos.y++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    ok = false;
                }

                Console.BackgroundColor = Program.mp.colMap[pos.x, pos.y];
                Console.Write(model);
                if (!ok) tr.Suspend();
                if (!contr) Thread.Sleep(st);
            }

            public void Left()
            {
                Console.BackgroundColor = Program.mp.colMap[pos.x, pos.y];
                Console.ForegroundColor = botCol;
                Console.SetCursorPosition(pos.x + 1, pos.y + 1);

                if (pos.x > 0 && Program.mp.map[pos.x - 1, pos.y] == " ")
                {
                    Console.Write(" ");
                    Console.SetCursorPosition(pos.x, pos.y + 1);
                    pos.x--;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    ok = false;
                }

                Console.BackgroundColor = Program.mp.colMap[pos.x, pos.y];
                Console.Write(model);
                Console.BackgroundColor = ConsoleColor.Black;
                if (!ok) tr.Suspend();
                if (!contr) Thread.Sleep(st);
            }

            public void Right()
            {
                Console.BackgroundColor = Program.mp.colMap[pos.x, pos.y];
                Console.ForegroundColor = botCol;
                Console.SetCursorPosition(pos.x + 1, pos.y + 1);

                if (pos.x < Program.mp.x0 - 1 && Program.mp.map[pos.x + 1, pos.y] == " ")
                {
                    Console.Write(" ");
                    Console.SetCursorPosition(pos.x + 2, pos.y + 1);
                    pos.x++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    ok = false;
                }

                Console.BackgroundColor = Program.mp.colMap[pos.x, pos.y];
                Console.Write(model);
                Console.BackgroundColor = ConsoleColor.Black;
                if (!ok) tr.Suspend();
                if (!contr) Thread.Sleep(st);
            }

            public void Control()
            {
                contr = true;
                while (true)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.W: this.Up(); break;
                        case ConsoleKey.S: this.Down(); break;
                        case ConsoleKey.D: this.Right(); break;
                        case ConsoleKey.A: this.Left(); break;
                        case ConsoleKey.E: this.Paint(); break;
                        case ConsoleKey.Q: this.Paint(ConsoleColor.Black); break;
                        case ConsoleKey.Z: this.Paint(ConsoleColor.Red); break;
                        case ConsoleKey.X: this.Paint(ConsoleColor.White); break;
                    }
                }
            }

            public void Goto(int x, int y)
            {
                Stack<string> moves = new Stack<string>(new Stack<string>(cumir.BFS.Search(this.pos, new vec(x, y))));
                while(moves.Count > 0)
                {
                    if (moves.Peek() == "UP") this.Up();
                    else if (moves.Peek() == "DOWN") this.Down();
                    else if (moves.Peek() == "LEFT") this.Left();
                    else if (moves.Peek() == "RIGHT") this.Right();
                    moves.Pop();
                }
            }
        }

        public static void Main(string[] args)
        {
            //Console.SetBufferSize(Program.mp.x0 + 2, Program.mp.y0 + 2);
            Console.ForegroundColor = ConsoleColor.White;
            FileInfo file = new FileInfo(path);
            Console.CursorVisible = false;

            if (file.Exists)                                                                    // map reader
            {
                StreamReader sr = new StreamReader(path);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();

                if(maze) Program.mp.map = cumir.MazeGenerator.Generate(mazeX, mazeY);

                for (int y = 0; y <= Program.mp.y0 + 1; y++)
                {
                    string m = sr.ReadLine();

                    for (int x = 0; x <= Program.mp.x0 + 1; x++)
                    {
                        Console.SetCursorPosition(x, y);

                        if (x == 0 || y == 0 || x == Program.mp.x0 + 1 || y == Program.mp.y0 + 1) Console.Write("#");
                        else
                        {
                            if(!maze)Program.mp.map[x - 1, y - 1] = m[x].ToString();
                            if (Program.mp.map[x - 1, y - 1] == null) Program.mp.map[x - 1, y - 1] = " ";
                            Program.mp.colMap[x - 1, y - 1] = ConsoleColor.Black;
                            Console.Write(Program.mp.map[x - 1, y - 1]);
                        }
                    }
                }

                sr.Dispose();
            }
            else                                                                                // map creator
            {
                FileStream f = File.Create(path);
                f.Close();
                using (StreamWriter sw = new StreamWriter(path))
                {
                    for (int y = 0; y < Program.mp.y0 + 2; y++)
                    {
                        string m = "#";
                        for (int i = 0; i < Program.mp.x0; i++)
                        {
                            if (y == 0 || y == Program.mp.y0 + 1) m += "#";
                            else m += " ";
                        }
                        m += "#";
                        sw.WriteLine(m);
                    }
                }

                Environment.Exit(-1);
            }

            Prog.prog();

            Console.ReadKey();
        }
    }
}