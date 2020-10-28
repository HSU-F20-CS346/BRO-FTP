using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace BRO_FTP
{
    class SendFile
    {
        void sendFile(string file, BinaryWriter send)
        {
            byte[] package = new byte[0];
            byte[] payloadInfoBytes = new byte[0];
            try
            {
                //string extension = Path.GetExtension(file);
                string payloadInfo = "1 " + file.Length + ' ' + file + " 0 ";

                payloadInfoBytes = Encoding.UTF8.GetBytes(payloadInfo);

                package = File.ReadAllBytes(file);

                send.Write(IPAddress.HostToNetworkOrder(payloadInfoBytes.Length));
                send.Write(payloadInfoBytes);

                send.Write(IPAddress.HostToNetworkOrder(package.Length));
                send.Write(package);



            }
            catch (Exception)
            {

            }
        }

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
}
