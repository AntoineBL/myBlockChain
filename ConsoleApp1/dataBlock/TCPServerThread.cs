using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace myBlockChain.dataBlock
{
    class TCPServerThread
    {
        Socket clientSoc;

        public TCPServerThread(Socket clientSoc)
        {
            this.clientSoc = clientSoc;
        }

        public void receiveData()
        {
            //byte[] b = new byte[100];
            //int k = this.clientSoc.Receive(b);
            Console.WriteLine("Recieved...");
            int bytesRead = 0;
            StringBuilder messageBuilder = new StringBuilder();
            using (NetworkStream ns = new NetworkStream(clientSoc))
            {
                int messageChunkSize = 10;
                do
                {
                    byte[] chunks = new byte[messageChunkSize];
                    bytesRead = ns.Read(chunks, 0, chunks.Length);
                    messageBuilder.Append(Encoding.UTF8.GetString(chunks));
                }
                while (bytesRead != 0);
            }
            //for (int i = 0; i < k; i++)
            //  Console.Write(Convert.ToChar(b[i]));

            Console.WriteLine(messageBuilder);

            //ASCIIEncoding asen = new ASCIIEncoding();
            //this.clientSoc.Send(asen.GetBytes("The string was recieved by the server."));
            //Console.WriteLine("\nSent Acknowledgement");
            /* clean up */
            this.clientSoc.Close();
        }
    }
}
