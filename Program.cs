﻿using System;
using System.Threading;
using System.IO;
using cumir;

namespace cumir
{
    class Program
    {
        public static Thread tr = Thread.CurrentThread;
        public const string path = @"C:\s.txt";                                         // path to map

        static class mp                                                                 // maps's class
        {
            public const int x0 = 20;                                                   // map width
            public const int y0 = 10;                                                   // map height

            public static string[,] map = new string[x0, y0];
            public static ConsoleColor[,] colMap = new ConsoleColor[x0, y0];
        }

        public class vec
        {
            public int x = 0;
            public int y = 0;
        }

        public class bot                                                                // robot's class
        {
            ConsoleColor paintCol = ConsoleColor.Blue;
            ConsoleColor botCol = ConsoleColor.Green;                                   // color of robot
            string model = "@";                                                         // robot's texture
            int st = 300;                                                               // delay time

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
        }

        public static void Main(string[] args)
        {
            FileInfo file = new FileInfo(path);
            Console.CursorVisible = false;

            if (file.Exists)                                                                    // map reader
            {
                StreamReader sr = new StreamReader(path);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();
                for (int y = 0; y <= Program.mp.y0 + 1; y++)
                {
                    string m = sr.ReadLine();

                    for (int x = 0; x <= Program.mp.x0 + 1; x++)
                    {
                        Console.SetCursorPosition(x, y);

                        if (x == 0 || y == 0 || x == Program.mp.x0 + 1 || y == Program.mp.y0 + 1) Console.Write("#");
                        else
                        {
                            Program.mp.map[x - 1, y - 1] = m[x].ToString();
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