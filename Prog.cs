using System;
using System.Threading;
using cumir;

namespace cumir
{
    public static class Prog
    {
        public static void prog()
        {
            cumir.Program.bot r = new Program.bot(0, 0);
            /*r.Down();
            r.Left();
            r.Up();
            r.Paint();
            r.Right();
            r.Paint(ConsoleColor.White);
            r.Right();*/
            r.Goto(2, 3);
            r.Control();
        }
    }
}