using ConsoleApp1;
using myBlockChain.dataBlock;
using myBlockChain.dataFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;

namespace myBlockChain.network
{
    class Node
    {
        private int nbSimpleNode = 0;
        private int nbSuperNode = 1;
        private int portNumber = 1234;
        private String ipAddr = "127.0.0.1";

        public void myServeur()
        {
            ServeurTCP serv = new ServeurTCP(1234, "127.0.0.1");
            Thread t = new Thread(serv.TCP);
            t.Start();

            Thread.Sleep(2000);
        }

        public BlockChain askBlochcaine()
        {
            ClientTCP c = new ClientTCP(portNumber, ipAddr);
            String blockChainS = c.sendData("askBlockChain@@", true);

            File.WriteAllText(@"dataFile/fileBlockChain", blockChainS);

            Json<BlockChain> j = new Json<BlockChain>(@"dataFile/fileBlockChain");

            return j.deserialize();
        }

        public void mining()
        {
            Console.WriteLine("Start mining");
            BlockChain myBlockChaine = new BlockChain();

            while (true)
            {
                myBlockChaine.searchBlock("test");
            }
        }

        public String readFile(String fileName)
        {
            String line = "";
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(fileName))
                {
                    // Read the stream to a string, and write the string to the console.
                    line = sr.ReadToEnd();
                    Console.WriteLine(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return line;
        }

        public void closeFile()
        {

        }

        public long ping(String host)
        {
            Ping p = new Ping();
            long time = 0;
            PingReply reply = p.Send(host);
            if (reply.Status == IPStatus.Success)
            {
                time = reply.RoundtripTime;
            }
            return time;
        }

        public String[] fastestPing(String[] hostList, int nbNodeToConect)
        {
            long[] bestTime = new long[nbNodeToConect];
            String[] bestHost  = new String[nbNodeToConect];

            for(int i=0; i <  bestTime.Length; i++)
            {
                 bestTime[i]= 99999999;
            }

            foreach(String host in hostList)
            {
                long currentTime = ping(host);
                long lastesTime = 0;
                for(int i=0; i<bestTime.Length; i++)
                {
                    if(bestTime[i] > lastesTime)
                    {
                        lastesTime = i;
                    }
                }

                if (bestTime[lastesTime] > currentTime){
                    bestTime[lastesTime] = currentTime;
                    bestHost[lastesTime] = host;
                }
            }

            return bestHost;
        }      

        public void flooding(String data, String nameFile)
        {
            
            IPAddressList l = new IPAddressList();
            Json<IPAddressList> j = new Json<IPAddressList>(@nameFile);
            l = j.deserialize();
            l.ToString();
            
        }
    }
}
