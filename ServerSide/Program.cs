﻿using Calc;
using Greet;
using Grpc.Core;
using System;
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
                server = new Server()
                {
                    Services =
                    {
                        GreetingService.BindService(new GreetingServiceImp()),
                        CalculatorService.BindService(new CalculatorServiceImp()),
                    },
                    Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
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
