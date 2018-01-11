using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ConsoleApp1.udp
{
    class UDPSend
    {
        //private Socket sending_socket;
        //private IPAddress send_to_address;
        //private IPEndPoint sending_end_point;
        //private List<IPAddress> send_to_addressList;
        //private List<IPEndPoint> sending_end_pointList;
        private int portNumber;
        private String serverHostName = null;
        private String data;

        public UDPSend(String hostName, int port, String data)
        {
            this.serverHostName = hostName;
            this.portNumber = port;
            this.data = data;
        }

        public void startSending()
        {
            Thread ctThread = new Thread(sendData);
            ctThread.Start();
        }

        private void sendData()
        {
            
            Boolean exception_thrown = false;

            Socket sending_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPAddress send_to_address = IPAddress.Parse(this.serverHostName);

            IPEndPoint sending_end_point = new IPEndPoint(send_to_address, this.portNumber);

            Console.WriteLine("2");
            // the socket object must have an array of bytes to send.

            // this loads the string entered by the user into an array of bytes.

            byte[] send_buffer = Encoding.ASCII.GetBytes(data);

            // Remind the user of where this is going.

            Console.WriteLine("sending to address: {0} port: {1}",
            sending_end_point.Address,
            sending_end_point.Port);
            try

            {
                sending_socket.SendTo(send_buffer, sending_end_point);
            }
            catch (Exception send_exception)
            {
                exception_thrown = true;
                Console.WriteLine(" Exception {0}", send_exception.Message);
            }
            if (exception_thrown == false)
            {
                Console.WriteLine("Message has been sent to the broadcast address");
            }
            else

            {
                exception_thrown = false;
                Console.WriteLine("The exception indicates the message was not sent.");
            }

            Console.WriteLine("end sending");
         
        } // end of main()

    } // end of class Program
}
