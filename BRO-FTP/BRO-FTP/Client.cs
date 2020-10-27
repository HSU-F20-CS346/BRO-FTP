using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace BRO_FTP
{    class Listener
    {
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
                Thread.Sleep(5000);
                Connect("192.168.1.108", "13000");
            });
            thread.Start();


            
            TcpListener server = null;

            // creates a TCP listener on local IP and waiting for a connection to be made
            try
            {
                // Local IP and port to listen on
                int port = 13000;
                IPAddress localAddr = IPAddress.Parse("192.168.1.100");

                // new TCP listener creation
                server = new TcpListener(localAddr, port);

                // Start listening for connections
                server.Start();

                // Enter the listening loop waiting for connection
                while (true)
                {
                    Console.Write("Waiting for a connection... ");

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
    }
}
