using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace myBlockChain.udp
{
    class UDPReceive
    {
        private int portNumber;

        public UDPReceive(int port)
        {
            this.portNumber = port;
        }

        public void startReceive()
        {
            Thread ctThread = new Thread(receiveData);
            ctThread.Start();
        }

        private void receiveData()
        {
            //byte[] data = new byte[1024];
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, portNumber);
            UdpClient newsock = new UdpClient(ipep);

            Console.WriteLine("Waiting for a client...");

            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);

            byte[] data = newsock.Receive(ref sender);

            Console.WriteLine("Message received from {0}:", sender.ToString());
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, data.Length));

        }

    }
}
