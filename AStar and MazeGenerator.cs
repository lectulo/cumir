using System;
using System.Collections.Generic;
using cumir;

namespace cumir
{
    public static class AStar
    {
        class cell
        {
            public int distanceT;
            public int distanceS;
            public int cost;
        }

        static List<Program.vec> way = new List<Program.vec>();
        static List<Program.vec> activeCells = new List<Program.vec>();
        static cell[,] map = new cell[Program.mp.x0, Program.mp.y0];

        /*public static List<Program.vec> search(Program.vec pos, Program.vec target)
        {
            
        }*/
    }

    public static class MazeGenerator
    {
        static string[,] map = new string[Program.mp.x0, Program.mp.y0];

        public static string[,] Generate(int sx, int sy)
        {
            Stack<Program.vec> active = new Stack<Program.vec>();
            bool[,] mapb = new bool[Program.mp.x0, Program.mp.y0];
            Random rand = new Random();

            for (int x = 0; x < Program.mp.x0; x++)
            {
                for(int y = 0; y < Program.mp.y0; y++)
                {
                    if (x % 2 == 1 || y % 2 == 1) map[x, y] = "#";
                    mapb[x, y] = false;
                }
            }
            active.Push(new Program.vec(sx, sy));
            mapb[sx, sy] = true;

            while(true)
            {
                if((active.Peek().x < Program.mp.x0 - 2 ? mapb[active.Peek().x + 2, active.Peek().y] : true)
                    && (active.Peek().y < Program.mp.y0 - 2 ? mapb[active.Peek().x, active.Peek().y + 2] : true)
                    && (active.Peek().x > 1 ? mapb[active.Peek().x - 2, active.Peek().y] : true)
                    && (active.Peek().y > 1 ? mapb[active.Peek().x, active.Peek().y - 2] : true))
                    active.Pop();
                else
                {
                    switch (rand.Next(0, 4))
                    {
                        case 0:
                            {
                                if (active.Peek().y > 1 && !mapb[active.Peek().x, active.Peek().y - 2])
                                {
                                    map[active.Peek().x, active.Peek().y - 1] = " ";
                                    active.Push(new Program.vec(active.Peek().x, active.Peek().y - 2));
                                    mapb[active.Peek().x, active.Peek().y] = true;
                                }
                                break;
                            }
                        case 1:
                            {
                                if (active.Peek().y < Program.mp.y0 - 2 && !mapb[active.Peek().x, active.Peek().y + 2])
                                {
                                    map[active.Peek().x, active.Peek().y + 1] = " ";
                                    active.Push(new Program.vec(active.Peek().x, active.Peek().y + 2));
                                    mapb[active.Peek().x, active.Peek().y] = true;
                                }
                                break;
                            }
                        case 2:
                            {
                                if (active.Peek().x > 1 && !mapb[active.Peek().x - 2, active.Peek().y])
                                {
                                    map[active.Peek().x - 1, active.Peek().y] = " ";
                                    active.Push(new Program.vec(active.Peek().x - 2, active.Peek().y));
                                    mapb[active.Peek().x, active.Peek().y] = true;
                                }
                                break;
                            }
                        case 3:
                            {
                                if (active.Peek().x < Program.mp.x0 - 2 && !mapb[active.Peek().x + 2, active.Peek().y])
                                {
                                    map[active.Peek().x + 1, active.Peek().y] = " ";
                                    active.Push(new Program.vec(active.Peek().x + 2, active.Peek().y));
                                    mapb[active.Peek().x, active.Peek().y] = true;
                                }
                                break;
                            }

                    }
                }
                if (active.Count == 0) break;
            }

            return map;
        }
    }
}
