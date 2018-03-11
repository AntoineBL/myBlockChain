using System;
using System.Collections.Generic;
using System.Text;
using myBlockChain.dataBlock;
using System.IO;

namespace myBlockChain.network
{
    class SuperNode : Node
    {


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

            //demander de se rajouter au réseau
            foreach(String addr in listIPAddr)
            {
                ClientTCP clt = new ClientTCP(1234, addr, "askConnecionSuperNode@@" + "" + "@@" + "", false);
            }

        }
    }
}
