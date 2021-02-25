using System;
using System.Threading;
using cumir;

namespace cumir
{
    public static class Prog
    {
        public static void prog()
        {
            cumir.Program.bot r = new Program.bot(5, 5);
            r.Down();
            r.Left();
            r.Up();
            r.Paint();
            r.Right();
            r.Paint(ConsoleColor.White);
            r.Right();
            r.Control();
        }
    }
}