using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace IKN_socket_client
{
    public class FTClientCode
    {
        public static string curMsg = "Idle";
        public static void SendFile(string fileName)
        {
            Console.WriteLine(fileName);
            try
            {
                IPAddress[] ipAddress = Dns.GetHostAddresses("localhost");
                IPEndPoint ipEnd = new IPEndPoint(ipAddress[1], 9000);
                /* Make IP end point same as Server. */
                Socket clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                /* Make a client socket to send data to server. */
                string filePath = "";
                /* File reading operation. */
                fileName = fileName.Replace("\\", "/");
                while (fileName.IndexOf("/") > -1)
                {
                    filePath += fileName.Substring(0, fileName.IndexOf("/") + 1);
                    fileName = fileName.Substring(fileName.IndexOf("/") + 1);
                }
                byte[] fileNameByte = Encoding.ASCII.GetBytes(fileName);
                Console.WriteLine("Buffering ...");
                byte[] fileData = File.ReadAllBytes(filePath + fileName);
                /* Read & store file byte data in byte array. */

                byte[] clientData = new byte[4 + fileNameByte.Length + fileData.Length];
                /* clientData will store complete bytes which will store file name length, file name & file data. */
                byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);
                /* File name length’s binary data. */
                fileNameLen.CopyTo(clientData, 0);
                fileNameByte.CopyTo(clientData, 4);

                Console.WriteLine("Connection to server ...");
                clientSock.Connect(ipEnd);

                //for (int j = 0; j < fileData.Length / 1000; j *= 1000)
                //{
                //    for (int i = 0; i < 1000; i++)
                //    {
                //        clientData[i + 4 + fileNameByte.Length] = fileData[i + j];
                //        Console.WriteLine("File sending...");
                //        Console.WriteLine();
                //        clientSock.Send(clientData);
                //    }
                //}
                fileData.CopyTo(clientData, 4 + fileNameByte.Length);


                clientSock.Send(clientData);

                Console.WriteLine("Disconnecting...");
                clientSock.Close();

                /* copy these bytes to a variable with format line [file name length] [file name] [ file content] */

                /* Trying to connection with server. */
                
                /* Now connection established, send client data to server. */
                
                /* Data send complete now close socket. */
                Console.WriteLine("File transferred.");
            }
            catch (Exception ex)
            {
                if (ex.Message == "No connection could be made because the target machine actively refused it")
                    curMsg = "File Sending fail. Because server not running.";
                else
                    curMsg = "File Sending fail." + ex.Message;
            }
        }
    }
}