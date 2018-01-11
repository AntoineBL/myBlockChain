using ConsoleApp1;
using ConsoleApp1.udp;
using myBlockChain.blockChain;
using myBlockChain.udp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace myBlockChain.test
{
    class TestUDP
    {

        public static void Main()
        {
            Console.WriteLine("test udp");

            BlockChain myBlockChaine = new BlockChain();
            for (int i = 1; i < 10; i++)
            {
                //Thread.Sleep(1000);
                myBlockChaine.searchBlock("test");
                //Console.WriteLine(myBlockChaine.getBlockI(i).getIndex() + " " + myBlockChaine.getBlockI(i).getData());
            }

            Json j = new Json("file.json", myBlockChaine);
            String s = j.serialize();

            UDPReceive re = new UDPReceive(1234);
            re.startReceive();
            Console.WriteLine("next");
            Thread.Sleep(1000);
            UDPSend u = new UDPSend("127.0.0.1", 1234, s);
            u.startSending();
            Console.WriteLine("end");

            

            Thread.Sleep(10000);
        }
    }
}
