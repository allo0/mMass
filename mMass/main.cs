using System;
using System.Collections.Generic;



namespace mMass
{
    class main
    {
        static void Main(string[] args)
        {
            int y = -3;
            int x = Math.Abs(y);

            modBasics newMod = new modBasics();

            newMod.move();
            Console.WriteLine("Hellao World! {0}",x);
        }
    }
}
