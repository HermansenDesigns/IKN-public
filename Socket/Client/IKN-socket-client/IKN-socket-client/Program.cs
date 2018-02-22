using System;

namespace IKN_socket_client
{
    //FILE TRANSFER USING C#.NET SOCKET - CLIENT

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                try
                {
                    FTClientCode.SendFile(args[0]);
                }
                catch (Exception)
                {
                    Console.WriteLine(FTClientCode.curMsg);
                }
                finally
                {
                    Console.WriteLine(FTClientCode.curMsg);
                }
            }
        }
    }
}
