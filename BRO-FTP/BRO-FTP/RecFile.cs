using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace BRO_FTP
{
    class Rec
    {
        public static void recFile(TcpClient client)
        {           
            BufferedStream stream;
            BinaryReader reader;
            BinaryWriter writer;

            byte[] package = new byte[0];
            byte[] payloadInfoBytes = new byte[0];

            try
            {   
                stream = new BufferedStream(client.GetStream());
                reader = new BinaryReader(stream);
                writer = new BinaryWriter(stream);

                ////string extension = Path.GetExtension(file);
                //string payloadInfo = "1 " + file.Length + ' ' + file + " 0 ";

                //payloadInfoBytes = Encoding.UTF8.GetBytes(payloadInfo);

                //package = File.ReadAllBytes(file);

                //send.Write(IPAddress.HostToNetworkOrder(payloadInfoBytes.Length));
                //send.Write(payloadInfoBytes);

                //send.Write(IPAddress.HostToNetworkOrder(package.Length));
                //send.Write(package);
            }
            catch (Exception ex)
            {
                Console.WriteLine("error: {0}", ex.Message);
                throw ex;
            }


            while(true)
            {
                int infoLength = IPAddress.NetworkToHostOrder(reader.ReadInt32());
                byte[] infoBytes = reader.ReadBytes(infoLength);
                string[] payloadInfo = Encoding.UTF8.GetString(infoBytes).Split(' ');


                switch (payloadInfo[0])
                {
                    case "1":
                        int payloadLength = IPAddress.NetworkToHostOrder(reader.ReadInt32());
                        byte[] payloadBytes = reader.ReadBytes(payloadLength);
                        File.WriteAllBytes(Path.Join(Directory.GetCurrentDirectory(), payloadInfo[2]), payloadBytes);
                        break;
                    case "2":
                        Send.sendFile(payloadInfo[2], writer);
                        break;
                    case "3":
                        Req.listRes(writer);
                        break;
                    case "4":
                        Console.Write(All of the items listed in payload)
                }
                   

                //ProcessReq(payloadInfo);


            }
        }
        public static void listRes(TcpClient client)
        {
            BufferedStream stream;
            BinaryReader reader;
            BinaryWriter writer;

            byte[] package = new byte[0];
            byte[] payloadInfoBytes = new byte[0];

            try
            {
                stream = new BufferedStream(client.GetStream());
                reader = new BinaryReader(stream);
                writer = new BinaryWriter(stream);

                ////string extension = Path.GetExtension(file);
                //string payloadInfo = "1 " + file.Length + ' ' + file + " 0 ";

                //payloadInfoBytes = Encoding.UTF8.GetBytes(payloadInfo);

                //package = File.ReadAllBytes(file);

                //send.Write(IPAddress.HostToNetworkOrder(payloadInfoBytes.Length));
                //send.Write(payloadInfoBytes);

                //send.Write(IPAddress.HostToNetworkOrder(package.Length));
                //send.Write(package);
            }
            catch (Exception ex)
            {
                Console.WriteLine("error: {0}", ex.Message);
                throw ex;
            }


            while (true)
            {
                int infoLength = IPAddress.NetworkToHostOrder(reader.ReadInt32());
                byte[] infoBytes = reader.ReadBytes(infoLength);
                string[] payloadInfo = Encoding.UTF8.GetString(infoBytes).Split(' ');



                /*
                 byte[] FileConstructer(string file)
                 {
                     byte[] package = new byte[0];
                     byte[] payload = new byte[0];
                     try 
                     {
                         string payloadInfo = "1 " + file.Length + ' ' + file + " 0 ";

                         payload = Encoding.UTF8.GetBytes(payloadInfo);

                         package = File.ReadAllBytes(file);

                         package.CopyTo(payload, payload.Length);
                         return package;
                     }
                     catch(Exception ex)
                     {
                         throw new Exception(ex.Message);
                     }
                */

            }
}