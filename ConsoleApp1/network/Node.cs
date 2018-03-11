using ConsoleApp1;
using myBlockChain.dataBlock;
using myBlockChain.dataFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace myBlockChain.network
{
    abstract class Node
    {
        private int nbSimpleNode = 0;
        private int nbSuperNode = 1;
        private int portNumber = 1234;
        private String ipAddr = "127.0.0.1";
        private List<String> listDataFlooding;
        private BlockChain myBlockChaine;
        private Boolean newBlockChainReceive = false;
        private Object testBlockChain = new Object();



        public void myClient(String data, Boolean needRecieve)
        {
            ClientTCP clt = new ClientTCP(1234, "127.0.0.1", data, needRecieve);
            Thread tclt = new Thread(clt.sendData);
            tclt.Start();
        }

        public BlockChain askBlochcaine()
        {
            ClientTCP c = new ClientTCP(portNumber, ipAddr, "askBlockChain@@", true);
            c.sendData();
            String blockChainS = c.dataReceive;
                
            File.WriteAllText(@"dataFile/fileBlockChain", blockChainS);

            Json<BlockChain> j = new Json<BlockChain>(@"dataFile/fileBlockChain");

            return j.deserialize();
        }

        public void mining()
        {
            Console.WriteLine("Start mining");
            myBlockChaine = new BlockChain();
            Json<BlockChain> blockChainJson = new Json<BlockChain>("dataFile/fileBlockChain.json");
            String bloChainSJ;

            while (true)
            {
                myBlockChaine.searchBlock("test", newBlockChainReceive);
                bloChainSJ = blockChainJson.serialize(myBlockChaine);
                flooding("flooding@@"+hashData(bloChainSJ)+"@@"+bloChainSJ, "dataFile/fileIPAddr.json");
                //myClient(bloChainSJ, false);
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

        public String[] fastertPing(List<String> hostList, int nbNodeToConect)
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
                long latesTime = 0;
                for(int i=0; i<bestTime.Length; i++)
                {
                    if(bestTime[i] > latesTime)
                    {
                        latesTime = i;
                    }
                }

                if (bestTime[latesTime] > currentTime){
                    bestTime[latesTime] = currentTime;
                    bestHost[latesTime] = host;
                }
            }

            return bestHost;
        }      

        public void flooding(String data, String nameFile)
        {
            //change json in simple file
            IPAddressList l = new IPAddressList();
            Json<IPAddressList> j = new Json<IPAddressList>(@nameFile);
            l = j.deserialize();

            SplitData dataSplit = new SplitData(data);
            dataSplit.getHash();

            if (!listDataFlooding.Contains(dataSplit.getHash()))
            {
                foreach (String addr in l.listIPAddr)
                {
                    ClientTCP clt = new ClientTCP(1234, addr, data, false);
                    Thread tclt = new Thread(clt.sendData);
                    tclt.Start();
                }

                String hashString = "";
                for (int i = 0; i < dataSplit.getHash().Length; i++)
                {
                    //Console.Write(Convert.ToChar(dataSplit.getHash()[i]));
                    hashString += Convert.ToChar(dataSplit.getHash()[i]);
                }

                listDataFlooding.Add(hashString);
            }  

        }

        private Byte[] hashData(String data)
        {
            SHA256 mySHA256 = SHA256Managed.Create();
            return mySHA256.ComputeHash(Encoding.ASCII.GetBytes(data));
        }

        public virtual void askAddNetwork()
        {
            //recuperer toutes les addresse des super noeuds
            //choisir celui qui a la plus petit ping
            //demander de se rajouter au réseau
            
        }

        public void checkBlockChainReceive(String blockChainString)
        {
            
            lock (testBlockChain)
            {
                File.WriteAllText(@"dataFile/fileBlockChainReceive.json", blockChainString);
                Json<BlockChain> blockChainJ = new Json<BlockChain>(@"dataFile/fileBlockChainReceive.json");
                BlockChain blockChainReceive = blockChainJ.deserialize();

                if (blockChainReceive.blockChain.Count > myBlockChaine.blockChain.Count)
                {
                    if (blockChainReceive.isValidChain())
                    {
                        myBlockChaine = blockChainReceive;

                    }
                }
            }
            

        }

        public void addSuperNoeud(String newSuperIPAddr)
        {
            StreamWriter sw = new StreamWriter("dataFile/fileSuperIPAddr", true);
            sw.WriteLine(newSuperIPAddr);
            sw.Close();
        }

        public void addSimpleNode(String newSuperIPAddr)
        {
            StreamWriter sw = new StreamWriter("dataFile/fileSimpleIPAddr", true);
            sw.WriteLine(newSuperIPAddr);
            sw.Close();
        }
    }
}
