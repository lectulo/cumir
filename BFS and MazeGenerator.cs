using System;
using System.Collections.Generic;
using cumir;

namespace cumir
{
    public static class BFS
    {
        public class cell
        {
            public Program.vec lastPos = new Program.vec();
            public bool visible = true;
        }

        public static Stack<string> Search(Program.vec sPos, Program.vec tPos)
        {
            cell[,] map = new cell[Program.mp.x0, Program.mp.y0];
            Queue<Program.vec> all = new Queue<Program.vec>();
            all.Enqueue(sPos);

            for(int y = 0; y < Program.mp.y0; y++)
            {
                for (int x = 0; x < Program.mp.x0; x++)
                {
                    map[x, y] = new cell();
                    if (Program.mp.map[x, y] != " ") map[x, y].visible = false;
                }
            }

            
            while(all.Count > 0)
            {
                Program.vec current = all.Dequeue();
                if(current.x > 0 && map[current.x - 1, current.y].visible)
                {
                    map[current.x - 1, current.y].lastPos = current;
                    map[current.x - 1, current.y].visible = false;
                    all.Enqueue(new Program.vec(current.x - 1, current.y));
                }
                if (current.x < Program.mp.x0 - 1 && map[current.x + 1, current.y].visible)
                {
                    map[current.x + 1, current.y].lastPos = current;
                    map[current.x + 1, current.y].visible = false;
                    all.Enqueue(new Program.vec(current.x + 1, current.y));
                }
                if (current.y > 0 && map[current.x, current.y - 1].visible)
                {
                    map[current.x, current.y - 1].lastPos = current;
                    map[current.x, current.y - 1].visible = false;
                    all.Enqueue(new Program.vec(current.x, current.y - 1));
                }
                if (current.y < Program.mp.y0 - 1 && map[current.x, current.y + 1].visible)
                {
                    map[current.x, current.y + 1].lastPos = current;
                    map[current.x, current.y + 1].visible = false;
                    all.Enqueue(new Program.vec(current.x, current.y + 1));
                }
                if (current == tPos) return Build(map, current, sPos);
            }

            return new Stack<string>();
        }

        static Stack<string> Build(cell[,] map, Program.vec pos, Program.vec sPos)
        {
            Stack<string> ret = new Stack<string>();
            while(pos != sPos)
            {
                if (map[pos.x, pos.y].lastPos == new Program.vec(pos.x - 1, pos.y)) ret.Push("RIGHT");
                else if (map[pos.x, pos.y].lastPos == new Program.vec(pos.x + 1, pos.y)) ret.Push("LEFT");
                else if (map[pos.x, pos.y].lastPos == new Program.vec(pos.x, pos.y - 1)) ret.Push("DOWN");
                else if (map[pos.x, pos.y].lastPos == new Program.vec(pos.x, pos.y + 1)) ret.Push("UP");
                pos = map[pos.x, pos.y].lastPos;
            }

            return ret;
        }
    }

    public static class MazeGenerator
    {
        static string[,] map = new string[Program.mp.x0, Program.mp.y0];

        public static string[,] Generate(int sx, int sy)
        {
            Stack<Program.vec> active = new Stack<Program.vec>();
            bool[,] mapb = new bool[Program.mp.x0, Program.mp.y0];
            Random rand = new Random();

            for (int y = 0; y < Program.mp.y0; y++)
            {
                for(int x = 0; x < Program.mp.x0; x++)
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
