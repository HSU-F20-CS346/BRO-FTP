using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace BRO_FTP
{    class Listener
    {

        static void TCPListener()
        {
            TcpListener server = null;

            // creates a TCP listener on local IP and waiting for a connection to be made
            try
            {
                // Local IP and port to listen on
                int port = 4000;
                //IPAddress localAddr = IPAddress.Parse("127.0.0.2"); //Needs your IP to function
                IPAddress[] localAddresses = Dns.GetHostAddresses(Dns.GetHostName());
                IPAddress localAddr = localAddresses[localAddresses.Length-1]; //Needs your IP to function

                // new TCP listener creation
                server = new TcpListener(localAddr, port);

                // Start listening for connections
                server.Start();

                // Enter the listening loop waiting for connection
                while (true)
                {
                    //Console.Write("Waiting for a connection... ");

                    // look for client to accept
                    TcpClient client = server.AcceptTcpClient();

                    if (client.Connected)
                    {
                        Console.WriteLine("Connection from secondary!");
                        break;
                    }

                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }

        }

        // Pings an IP and port trying to connect
        static void Connect(String IPAddress, String portNum) 
        {
            while (true)
            {
                try
                {
                    // sets port num and IPAddress
                    int port = Int32.Parse(portNum);
                    TcpClient client = new TcpClient(IPAddress, port);


                    if (client.Connected)
                    {
                        Console.WriteLine("Connected to secondary");
                        break;
                    }
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("No connection");
                    Thread.Sleep(5000);
                }
                catch (SocketException)
                {
                    Console.WriteLine("No connection");
                    Thread.Sleep(5000);
                }
            }
        }


        public static void Main()
        {
            
            // Starts connection thread
            Thread thread = new Thread(() =>
            {
                //Thread.Sleep(1000);
                TCPListener();
                //Thread.Sleep(3000);
            });

            thread.Start();
            
            Console.Write("Enter the IP address: ");
            string ipadd = Console.ReadLine();
            Connect(ipadd, "4000"); // Needs target IP to function
          
            //Thread reqHandler = new Thread(() =>
            //{
            //    while (running)
            //    {
            //        BinaryReader 
            //    }
            //}
        }
    }
}
