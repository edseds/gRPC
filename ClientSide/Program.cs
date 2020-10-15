using Calc;
using Dummy;
using Greet;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSide
{
    class Program
    {
        const string target = "127.0.0.1:50051";

        static void Main(string[] args)
        {
            Channel channel = new Channel(target, ChannelCredentials.Insecure);

            channel.ConnectAsync().ContinueWith((task) =>
            {
                if(task.Status == TaskStatus.RanToCompletion)
                {
                    Console.WriteLine("The client connected successfuly");
                }
            });

            // Greeting
            var client = new GreetingService.GreetingServiceClient(channel);

            var greeting = new Greeting()
            {
                FirstName = "Johan",
                LastName = "Clementes"
            };

            var request = new GreetingRequest()
            {
                Greeting = greeting
            };

            var response = client.Greet(request);
            Console.WriteLine(response.Result);

            //Calculator
            var calcClient = new CalculatorService.CalculatorServiceClient(channel);

            var calc = new Calculator
            {
                FirstNumber = 10,
                SecondNumber = 10
            };

            var calcRequest = new CalculatorRequest()
            {
                Calculator = calc
            };

            var responseCalc = calcClient.Calc(calcRequest);
            Console.WriteLine(responseCalc.Result);

            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }
    }
}
