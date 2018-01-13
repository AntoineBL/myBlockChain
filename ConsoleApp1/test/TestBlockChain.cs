using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace myBlockChain.test
{
    class TestBlockChain
    {
        static public void Main()
        {
            Console.WriteLine("my Block Chain");
            BlockChain myBlockChaine = new BlockChain();
            Console.WriteLine(myBlockChaine.getBlockI(0).index + " " + myBlockChaine.getBlockI(0).data);
            for (int i=1; i<10; i++)
            {
                //Thread.Sleep(1000);
                myBlockChaine.searchBlock("test");
                Console.WriteLine(myBlockChaine.getBlockI(i).index+" "+ myBlockChaine.getBlockI(i).data);
            }

            Console.WriteLine(myBlockChaine.isValidChain());
            Thread.Sleep(10000);


        }
    }
}
