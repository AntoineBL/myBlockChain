using myBlockChain.dataBlock;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace myBlockChain.test
{
    class TestTCP
    {
        public static void Main()
        {
            Console.WriteLine("test TCP");

            ServeurTCP serv = new ServeurTCP(1234, "127.0.0.1");
            Thread t = new Thread(serv.TCP);
            t.Start();

            Thread.Sleep(2000);

            ClientTCP cl = new ClientTCP(1234, "127.0.0.1");
            cl.sendData("heyaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", true);

            Thread.Sleep(111111111);
        }

    }
}
