using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace BRO_FTP
{    class Listener
    {
        static void Connect(String server, String message)
        {
            while (true)
            {
                try
                {
                    // Create a TcpClient.

                    int port = Int32.Parse(message);
                    TcpClient client = new TcpClient(server, port);

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

            // Thread for client connection
            Thread thread = new Thread(() =>
            {
                Thread.Sleep(5000);
                Connect("127.0.0.1", "13000");
            });
            thread.Start();


            TcpListener server = null;
            try
            {
                // Set the TcpListener on port 13001.
                int port = 13001;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                // Enter the listening loop.
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
