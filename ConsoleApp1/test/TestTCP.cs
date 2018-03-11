using myBlockChain.dataBlock;
using myBlockChain.network;
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
            Node n = new SimpleNode();
            ServeurTCP serv = new ServeurTCP(1234, "127.0.0.1", n);
            Thread t = new Thread(serv.TCP);
            t.Start();

            Thread.Sleep(2000);

            ClientTCP cl = new ClientTCP(1234, "127.0.0.1", "heyaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", true);
            cl.sendData();

            Thread.Sleep(111111111);
        }

    }
}
