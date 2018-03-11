using myBlockChain.network;
using System;
using System.Net;
using System.Net.Sockets;

namespace myBlockChain.dataBlock
{
    class ServeurTCP
    {

        /*   Server Program    */
        private int portNumber;
        private String ipAddr;
        public Boolean stop  { get; set; }
        Node node;

        public ServeurTCP(int portNumber, String ipAddr, Node node)
        {
            stop = false;
            this.portNumber = portNumber;
            this.ipAddr = ipAddr;
            this.node = node;
        }

        public void TCP()
        {
            try
            {
                IPAddress ipAd = IPAddress.Parse(this.ipAddr);
               
                // use local m/c IP address, and 
                // use the same in the client

                /* Initializes the Listener */
                TcpListener myList = new TcpListener(this.portNumber);

                /* Start Listeneting at the specified port */
                myList.Start();

                Console.WriteLine("Serveur : The server is running at port 8001...");
                Console.WriteLine("Serveur : The local End point is  :" +
                                  myList.LocalEndpoint);

                while(true && !stop){
                    Console.WriteLine("Serveur : Waiting for a connection.....");

                    //Socket clientSoc = myList.AcceptSocket();
                    TcpClient clientSoc = myList.AcceptTcpClient();
                    Console.WriteLine("Serveur : Connection accepted from " + clientSoc.ToString());
                    TCPServerThread serverThread = new TCPServerThread(clientSoc, this.node);
                    serverThread.startReceiveData();
                }
                
                myList.Stop();

            }
            catch (Exception e)
            {
                Console.WriteLine("Serveur : Error..... " + e.StackTrace);
            }
        }

    }
}

