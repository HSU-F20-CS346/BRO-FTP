using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
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


            while (true)
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
                        Rec.listRes(client);
                        break;

                }


                //ProcessReq(payloadInfo);


            }
        }
        public static void listRes(TcpClient client)
        {
            BufferedStream stream;
            BinaryWriter writer;

            byte[] package = new byte[0];
            byte[] payloadInfoBytes = new byte[0];

            try
            {
                stream = new BufferedStream(client.GetStream());
                writer = new BinaryWriter(stream);

                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "share_folder");
                string[] files = Directory.GetFiles(path);
                

                //System.IO.DriveInfo di = new System.IO.DriveInfo(Directory.GetCurrentDirectory());
                //System.IO.DirectoryInfo dirInfo = di.;
                //System.IO.FileInfo[] fileNames = dirInfo.GetFiles("*.*");

                string payloadstr = "";
                for (int i = 0; i < files.Length; i++)
                {
                    payloadstr += Path.GetFileName(files[i]) + "\n";
                }

                byte[] payload = Encoding.UTF8.GetBytes(payloadstr);
                writer.Write(IPAddress.HostToNetworkOrder(payload.Length));
                writer.Write(payload);

                writer.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error: {0}", ex.Message);
                throw ex;
            }

        }

    }        


}