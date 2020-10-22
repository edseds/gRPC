using Calc;
using Greet;
using GreetDeadlines;
using Grpc.Core;
using Sqrt;
using System;
using System.Collections.Generic;
using System.IO;

namespace ServerSide
{
    class Program
    {
        const int Port = 50051;

        static void Main(string[] args)
        {
            Server server = null;
            try
            {
                var serverCert = File.ReadAllText("ssl/server.crt");
                var serverKey = File.ReadAllText("ssl/server.key");
                var keypair = new KeyCertificatePair(serverCert, serverKey);
                var cacert = File.ReadAllText("ssl/ca.crt");

                var credentials = new SslServerCredentials(
                    new List<KeyCertificatePair>() { keypair }, cacert, true);

                server = new Server()
                {
                    Services =
                    {
                        GreetingService.BindService(new GreetingServiceImp()),
                        CalculatorService.BindService(new CalculatorServiceImp()),
                        SqrtService.BindService(new SqrtServiceImpl()),
                        GreetDeadlinesService.BindService(new GreetingDeadlinesImpl())
                    },

                    Ports = { new ServerPort("localhost", Port, credentials) }
                };

                server.Start();

                Console.WriteLine("The server is listening on the port {0}", Port);
                Console.ReadKey();
            }
            catch (IOException e)
            {
                Console.WriteLine("The server failed to start: ", e.Message);
            }
            finally
            {
                if (server != null)
                {
                    server.ShutdownAsync().Wait();
                }
            }

        }
    }
}
