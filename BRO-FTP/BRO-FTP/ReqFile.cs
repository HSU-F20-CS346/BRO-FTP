using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;


namespace BRO_FTP
{
    class Req
    {
        public static void reqFile(string file, BinaryWriter writer)
        {
            byte[] package = new byte[0];
            byte[] payloadInfoBytes = new byte[0];
            try
            {
                //string extension = Path.GetExtension(file);
                string payloadInfo = "1 " + file.Length + ' ' + file + " 0 ";

                payloadInfoBytes = Encoding.UTF8.GetBytes(payloadInfo);

                package = File.ReadAllBytes(file);

                writer.Write(IPAddress.HostToNetworkOrder(payloadInfoBytes.Length));
                writer.Write(payloadInfoBytes);

                writer.Write(IPAddress.HostToNetworkOrder(package.Length));
                writer.Write(package);
            }
            catch (Exception ex)
            {
                Console.WriteLine("error: {0}", ex.Message);
                throw ex;
            }
        }
        public static void listReq(BinaryWriter writer)
        {
            byte[] package = new byte[0];
            byte[] payloadInfoBytes = new byte[0];
            try
            {
                //string extension = Path.GetExtension(file);
                string payloadInfo = "3";

                payloadInfoBytes = Encoding.UTF8.GetBytes(payloadInfo);

                writer.Write(IPAddress.HostToNetworkOrder(payloadInfoBytes.Length));
                writer.Write(payloadInfoBytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine("error: {0}", ex.Message);
                throw ex;
            }
        }

    }
}
