using Grpc.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide
{
    class Program
    {
        const int Port = 50051;

        static void Main(string[] args)
        {
            Server server = new Server()
            {
                Ports = {new ServerPort("localhost")}
            };

            try
            {

            }
            catch (IOException)
            {

                throw;
            }

        }
    }
}
