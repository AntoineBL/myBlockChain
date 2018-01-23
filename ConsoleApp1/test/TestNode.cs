using ConsoleApp1;
using myBlockChain.dataFile;
using myBlockChain.network;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;

namespace myBlockChain.test
{
    class TestNode
    {

        static public void Main()
        {
            Node n = new Node();
            IPAddressList ip = new IPAddressList();


            //String s = n.readFile("dataFile/file.json");
            //Console.WriteLine(s);


            String ipAd = "172.21.5.99";
            ip.addIPAddr(ipAd);
            ipAd = "172.21.5.99"; ip.addIPAddr(ipAd);
            ip.addIPAddr(ipAd);
            ipAd = "172.21.5.99"; ip.addIPAddr(ipAd);
            ip.addIPAddr(ipAd);
            ipAd = "172.21.5.99"; ip.addIPAddr(ipAd);
            ip.addIPAddr(ipAd);


            Json<IPAddressList> j = new Json<IPAddressList>(@"dataFile/fileIPAddr.json");
            String s = j.serialize( ip);
            Console.WriteLine(s);

            n.flooding("azer", @"dataFile/fileIPAddr.json");

            n.myServeur();
            BlockChain b = n.askBlochcaine();
            Console.WriteLine("\n\n write new blockchain receive");

            b.toString();

            Thread.Sleep(111111);


        }

    }
}
