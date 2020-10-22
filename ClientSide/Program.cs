using Calc;
using Greet;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var calcClient = new CalculatorService.CalculatorServiceClient(channel);

            var greeting = new Greeting()
            {
                FirstName = "Johan",
                LastName = "Clementes"
            };

            #region "Unary"
            //DoSimpleGreet(client, new GreetingRequest() { Greeting = greeting });
            #endregion

            #region "Server streaming"
            //DoManyGreetings(client, greeting);
            #endregion

            #region "Calculator"
            //Calc(calcClient);
            #endregion

            #region "Number to Prime Numbers"
            //await GetPrimeNumbers(calcClient);
            #endregion

            #region "Client streaming"
            //await DoLongGreet(client, greeting);
            #endregion

            #region "Client streaming Avg"
            //await GetAvg(calcClient);
            #endregion

            #region "Bi-Di streaming"
            //await DoGreetEveryone(client);
            #endregion

            #region "Find Max Number"
            await FindMaxNumber(calcClient);
            #endregion

            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }

        public static void DoSimpleGreet(GreetingService.GreetingServiceClient client, GreetingRequest request)
        {
            var response = client.Greet(request);
            Console.WriteLine(response.Result);
        }

        public static async Task DoManyGreetings(GreetingService.GreetingServiceClient client, Greeting greeting)
        {
            var request = new GreetingManyTimesRequest
            {
                Greeting = greeting
            };

            var response = client.GreetManyTimes(request);

            while (await response.ResponseStream.MoveNext())
            {
                Console.WriteLine(response.ResponseStream.Current.Result);
                await Task.Delay(200);
            }
        }

        public static async Task DoLongGreet(GreetingService.GreetingServiceClient client, Greeting greeting)
        {
            var request = new LongGreetingRequest() { Greeting = greeting };
            var stream = client.LongGreet();

            foreach (int i in Enumerable.Range(1, 10))
            {
                await stream.RequestStream.WriteAsync(request);
            }

            await stream.RequestStream.CompleteAsync();
            var response = await stream.ResponseAsync;
            Console.WriteLine(response.Result);

        }

        public static void Calc(CalculatorService.CalculatorServiceClient client)
        {
            var calc = new Calculator
            {
                FirstNumber = 10,
                SecondNumber = 10
            };

            var calcRequest = new CalculatorRequest()
            {
                Calculator = calc
            };

            var responseCalc = client.Calc(calcRequest);
            Console.WriteLine(responseCalc.Result);
        }

        public static async Task GetPrimeNumbers(CalculatorService.CalculatorServiceClient client)
        {
            NumberForDecomposition numberForDecomposition = new NumberForDecomposition()
            {
                Number = 120
            };

            var primeNumberRequest = new PrimeNumberRequest()
            {
                NumberForDecomposition = numberForDecomposition
            };


            var responsePrimeNumbers = client.PrimeNumber(primeNumberRequest);

            while (await responsePrimeNumbers.ResponseStream.MoveNext())
            {
                Console.WriteLine(responsePrimeNumbers.ResponseStream.Current.Result);
                await Task.Delay(200);
            }
        }

        public static async Task GetAvg(CalculatorService.CalculatorServiceClient client)
        {
            List<int> providedNumberList = new List<int>()
            { 1,2,3,4};
            var stream = client.ComputeAvg();
            foreach (var number in providedNumberList)
            {
                await stream.RequestStream.WriteAsync(new ComputeAvgRequest() { Number = number });
            }

            await stream.RequestStream.CompleteAsync();
            var resposne = await stream.ResponseAsync;
            Console.WriteLine(resposne.Avg);
        }

        public static async Task DoGreetEveryone(GreetingService.GreetingServiceClient client)
        {
            var stream = client.GreetEveryone();
            var resposneReaderTask = Task.Run(async () =>
            {
                while(await stream.ResponseStream.MoveNext())
                {
                    Console.WriteLine("Received: {0}", stream.ResponseStream.Current.Result);
                }
            });

            List<Greeting> greetingList = new List<Greeting>()
            {
                new Greeting() {FirstName = "1111111",LastName="222222" },
                new Greeting() {FirstName = "3333333",LastName="444444" },
                new Greeting() {FirstName = "55555555",LastName="6666666" }
            };

            foreach(var greeting in greetingList)
            {
                Console.WriteLine("Sending: {0}", greeting.ToString());
                await stream.RequestStream.WriteAsync(new GreetEveryoneRequest()
                {
                    Greeting = greeting
                });
            }

            await stream.RequestStream.CompleteAsync();
            await resposneReaderTask;
        }

        public static async Task FindMaxNumber(CalculatorService.CalculatorServiceClient client)
        {
            var stream = client.FindMaxNumber();

            var responseReaderTask = Task.Run(async () =>
            {
                while(await stream.ResponseStream.MoveNext())
                {
                    Console.WriteLine(stream.ResponseStream.Current.Max);
                }
            });

            List<int> numberList = new List<int>()
            {1,5,3,6,2,20 };

            foreach(var number in numberList)
            {
                await stream.RequestStream.WriteAsync(new FindMaxNumberRequest() { Number = number });
            }

            await stream.RequestStream.CompleteAsync();
            await responseReaderTask;

        }
    }
}
