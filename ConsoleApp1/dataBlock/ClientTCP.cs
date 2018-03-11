using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace myBlockChain.dataBlock
{
    class ClientTCP
    {

        private int portNumber;
        private String ipAddr;
        private TcpClient tcpclnt;
        private String data;
        private Boolean needReceive;
        public String dataReceive { get; set; }

        public ClientTCP(int portNumber, String ipAddr, String data, Boolean needReceive)
        {
            this.portNumber = portNumber;
            this.ipAddr = ipAddr;
            this.tcpclnt = new TcpClient();
            tcpclnt.Connect(this.ipAddr, portNumber);
            this.data = data;
            this.needReceive = needReceive;
            
        }

        public void sendData()
        {

            try
            {
                
                Console.WriteLine("Client : Connecting.....");

                
                // use the ipaddress as in the server program

                Console.WriteLine("Client : Connected");
                Stream stm = tcpclnt.GetStream();

                //send data
                ASCIIEncoding asen = new ASCIIEncoding();

                byte[] ba = asen.GetBytes(data);
                Console.WriteLine("Client : Transmitting.....");
                
                
                stm.Write(ba, 0, ba.Length);

                //reception data
                if (needReceive)
                {
                    byte[] bb = new byte[100000];
                    int k = stm.Read(bb, 0, 100000);
                    this.dataReceive = "";

                    for (int i = 0; i < k; i++)
                    {
                        //Console.Write(Convert.ToChar(bb[i]));
                        this.dataReceive += Convert.ToChar(bb[i]);
                    }

                    
                }
                
                
                tcpclnt.Close();
                Console.WriteLine("Client : close");
            }

            catch (Exception e)
            {
                Console.WriteLine("Client : Error..... " + e.StackTrace);
            }
            
        }
    }
}
