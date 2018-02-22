using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace IKN_socket_server
{
    class FTServerCode
    {
        IPEndPoint ipEnd;
        Socket sock;
        public FTServerCode()
        {
            ipEnd = new IPEndPoint(IPAddress.Any, 9000);
            //Make IP end point to accept any IP address with port no 9000.
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            //Here creating new socket object with protocol type and transfer data type
            sock.Bind(ipEnd);
            //Bind end point with newly created socket.
        }
        public static string receivedPath = "C:\\Users\\KaspersLaptop\\Desktop\\receivefolder";
        public static string curMsg = "Stopped";
        public void StartServer()
        {
            try
            {
                Console.WriteLine("Starting...");
                sock.Listen(100);
                /* That socket object can handle maximum 100 client connection at a time & 
                waiting for new client connection */
                Console.WriteLine("Running and waiting to receive file.");
                Socket clientSock = sock.Accept();
                /* When request comes from client that accept it and return new socket object for handle that client. */
                byte[] clientData = new byte[500 * 1024];
                int receivedBytesLen = clientSock.Receive(clientData);
                Console.WriteLine("Receiving data...");
                Console.WriteLine(receivedBytesLen);
                int fileNameLen = BitConverter.ToInt32(clientData, 0);
                /* I've sent byte array data from client in that format like 
                [file name length in byte][file name] [file data], so need to know 
                first how long the file name is. */
                string fileName = Encoding.ASCII.GetString(clientData, 4, fileNameLen);
                /* Read file name */
                Console.WriteLine(fileName);
                Console.WriteLine(fileNameLen);

                BinaryWriter bWrite = new BinaryWriter(File.Open(receivedPath + "/" + fileName, FileMode.Append)); ;
                /* Make a Binary stream writer to saving the receiving data from client. */
                bWrite.Write(clientData, 4 + fileNameLen, receivedBytesLen - 4 - fileNameLen);
                /* Read remain data (which is file content) and save it by using binary writer. */
                Console.WriteLine("Saving file...");
                bWrite.Close();
                clientSock.Close();
                /* Close binary writer and client socket */
                curMsg = "Received & Saved file; Server Stopped.";
            }
            catch (Exception)
            {
                curMsg = "File Receiving error.";
            }
        }
    }
}