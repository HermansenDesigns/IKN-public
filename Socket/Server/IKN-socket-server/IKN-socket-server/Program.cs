using System;

namespace IKN_socket_server
{
    class Program
    {
        static void Main(string[] args)
        {
            FTServerCode fts = new FTServerCode();
            while (true)
            {
                fts.StartServer();
            }
        }
    }
}
