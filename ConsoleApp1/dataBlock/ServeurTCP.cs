using System;
using System.Collections.Generic;
using System.Text;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;


namespace myBlockChain.dataBlock
{
    class ServeurTCP
    {

        /*   Server Program    */
        private int portNumber;
        private Boolean stop = true;

        public ServeurTCP(int portNumber)
        {
            this.portNumber = portNumber;
        }

        public void TCP()
        {
            try
            {
                IPAddress ipAd = IPAddress.Parse("172.21.5.99");
                // use local m/c IP address, and 
                // use the same in the client

                /* Initializes the Listener */
                TcpListener myList = new TcpListener(ipAd, 8001);

                /* Start Listeneting at the specified port */
                myList.Start();

                Console.WriteLine("The server is running at port 8001...");
                Console.WriteLine("The local End point is  :" +
                                  myList.LocalEndpoint);

                while(true && stop){
                    Console.WriteLine("Waiting for a connection.....");

                    Socket clientSoc = myList.AcceptSocket();
                    Console.WriteLine("Connection accepted from " + clientSoc.RemoteEndPoint);
                }
                

                
                myList.Stop();

            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }

    }
}

