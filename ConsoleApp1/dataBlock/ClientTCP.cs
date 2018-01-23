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
        TcpClient tcpclnt;

        public ClientTCP(int portNumber, String ipAddr)
        {
            this.portNumber = portNumber;
            this.ipAddr = ipAddr;
            this.tcpclnt = new TcpClient();
            tcpclnt.Connect(this.ipAddr, portNumber);
        }

        public String receiveData()
        {
             
            return null;

        }

        public String sendData(String data, Boolean needReceive)
        {
            String dataReceive = "";

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

                    for (int i = 0; i < k; i++)
                    {
                        //Console.Write(Convert.ToChar(bb[i]));
                        dataReceive += Convert.ToChar(bb[i]);
                    }
                        
                        
                }
                
                
                tcpclnt.Close();
                Console.WriteLine("Client : close");
            }

            catch (Exception e)
            {
                Console.WriteLine("Client : Error..... " + e.StackTrace);
            }


            return dataReceive;
        }
    }
}
