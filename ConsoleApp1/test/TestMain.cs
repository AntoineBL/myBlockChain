using myBlockChain.dataBlock;
using myBlockChain.network;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace myBlockChain.test
{
    class TestMain
    {

        public static void Main()
        {
            
            SimpleNode node = new SimpleNode();

            ServeurTCP serv = new ServeurTCP(1234, "127.0.0.1", node);
            Thread tserv = new Thread(serv.TCP);
            tserv.Start();
        }
    }
}
