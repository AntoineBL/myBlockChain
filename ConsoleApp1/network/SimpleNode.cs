using System;
using System.Collections.Generic;
using System.Text;
using myBlockChain.dataBlock;
using System.IO;

namespace myBlockChain.network
{
    class SimpleNode : Node
    {

        private const int nbConnection = 1;

        public override void askAddNetwork()
        {
            List<String> listIPAddr = new List<string>();
            //recuperer toutes les addresse des super noeuds
            string line;

            // Read the file and display it line by line.  
            StreamReader file = new StreamReader(@"dataFile/fileSuperIPAddr");
            while ((line = file.ReadLine()) != null)
            {
                //Console.WriteLine(line);
                listIPAddr.Add(line);
            }

            file.Close();

            //choisir celui qui a la plus petit ping
            String[] tabIPAddr =  fastertPing(listIPAddr, nbConnection);

            //demander de se rajouter au réseau
            for(int i=0; i<tabIPAddr.Length; i++)
            {
                ClientTCP clt = new ClientTCP(1234, tabIPAddr[i], "askConnecionSimpleNode@@"+"" + "@@"+"", false);
            }

        }
    }
}
