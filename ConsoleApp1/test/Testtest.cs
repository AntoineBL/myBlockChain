using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace myBlockChain.test
{
    class Testtest
    {

        public static void Main()
        {
            string hote = Dns.GetHostName();
            IPHostEntry iphe = Dns.Resolve(hote);
            string ip = iphe.AddressList[0].ToString();
            Console.WriteLine("Votre Ip est: " + ip);
            Console.ReadLine();
        }
    }
}
