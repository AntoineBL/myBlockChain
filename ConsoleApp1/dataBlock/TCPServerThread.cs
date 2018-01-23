using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace myBlockChain.dataBlock
{
    class TCPServerThread
    {
        TcpClient clientSoc;

        public TCPServerThread(TcpClient clientSoc)
        {
            this.clientSoc = clientSoc;
        }

        public void startReceiveData()
        {
            Thread t = new Thread(receiveData);
            t.Start();
        }

        public void receiveData()
        {

            String dataReceive = "";
            //byte[] b = new byte[100];
            //int k = this.clientSoc.Receive(b);
            /*Console.WriteLine("Recieved...");
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
                    Console.WriteLine("i am here"+ bytesRead);
                }
                while (bytesRead != 0);
            }*/
            //for (int i = 0; i < k; i++)
            //  Console.Write(Convert.ToChar(b[i]));
            Console.WriteLine("Serveur Thread : Connection accepted.");
            NetworkStream ns = clientSoc.GetStream();

            byte[] byteTime = Encoding.ASCII.GetBytes("DateTime.Now.ToString()");


            //lecture
            byte[] bb = new byte[100];
            int k = ns.Read(bb, 0, 100);

            for (int i = 0; i < k; i++)
            {
                Console.Write(Convert.ToChar(bb[i]));
                dataReceive += Convert.ToChar(bb[i]);
            }

            SplitData sp = new SplitData();
            sp.split(dataReceive);

            //envoie de data
            try
            {
                if(sp.getGoal() == "askBlockChain")
                {
                    Byte[] s = Encoding.ASCII.GetBytes(readFile(@"dataFile/file.json"));
                    ns.Write(s, 0, s.Length);
                }
                //ns.Write(byteTime, 0, byteTime.Length);
                ns.Close();
                clientSoc.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            //Console.WriteLine("Serveur receive : " +messageBuilder);
            Console.WriteLine("Serveur send finis");
            //ASCIIEncoding asen = new ASCIIEncoding();
            //this.clientSoc.Send(asen.GetBytes("The string was recieved by the server."));
            //Console.WriteLine("\nSent Acknowledgement");
            /* clean up */
            this.clientSoc.Close();
        }


        public String readFile(String fileName)
        {
            String line = "";
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(fileName))
                {
                    // Read the stream to a string, and write the string to the console.
                    line = sr.ReadToEnd();
                    //Console.WriteLine(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return line;
        }
    }


}
