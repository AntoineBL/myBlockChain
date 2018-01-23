using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace myBlockChain.network
{
    class IPAddressList
    {
        public List<String> listIPAddr { get; set; }

        public IPAddressList()
        {
            listIPAddr  = new List<String>();
        }

        public void addIPAddr(String ipAddr)
        {
            listIPAddr.Add(ipAddr);
        }


        public void ToString()
        {
            foreach(String ip in listIPAddr)
            {
                Console.Write(ip + " ");
            }
            Console.WriteLine();
        }
    }
}
