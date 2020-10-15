﻿using Calc;
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

        static async Task Main(string[] args)
        {
            Channel channel = new Channel(target, ChannelCredentials.Insecure);

            await channel.ConnectAsync().ContinueWith((task) =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
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

            #region "Unary"
            //var request = new GreetingRequest()
            //{
            //    Greeting = greeting
            //};

            //var response = client.Greet(request);
            //Console.WriteLine(response.Result);
            #endregion

            #region "Server streaming"
            //var request = new GreetingManyTimesRequest
            //{
            //    Greeting = greeting
            //};

            //var response = client.GreetManyTimes(request);

            //while (await response.ResponseStream.MoveNext())
            //{
            //    Console.WriteLine(response.ResponseStream.Current.Result);
            //    await Task.Delay(200);
            //}

            #endregion
            #region "Calculator"
            var calcClient = new CalculatorService.CalculatorServiceClient(channel);

            //var calc = new Calculator
            //{
            //    FirstNumber = 10,
            //    SecondNumber = 10
            //};

            //var calcRequest = new CalculatorRequest()
            //{
            //    Calculator = calc
            //};

            //var responseCalc = calcClient.Calc(calcRequest);
            //Console.WriteLine(responseCalc.Result);
            #endregion

            #region "Number to Prime Numbers"

            NumberForDecomposition numberForDecomposition = new NumberForDecomposition()
            {
                Number = 120
            };

            var primeNumberRequest = new PrimeNumberRequest()
            {
                NumberForDecomposition = numberForDecomposition
            };


            var responsePrimeNumbers = calcClient.PrimeNumber(primeNumberRequest);

            while (await responsePrimeNumbers.ResponseStream.MoveNext())
            {
                Console.WriteLine(responsePrimeNumbers.ResponseStream.Current.Result);
                await Task.Delay(200);
            }
            #endregion

            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }
    }
}
