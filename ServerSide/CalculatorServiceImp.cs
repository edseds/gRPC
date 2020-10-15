using Calc;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Calc.CalculatorService;

namespace ServerSide
{
    public class CalculatorServiceImp : CalculatorServiceBase
    {
        public override Task<CalculatorResponse> Calc(CalculatorRequest request, ServerCallContext context)
        {
            int result = request.Calculator.FirstNumber + request.Calculator.SecondNumber;
            return Task.FromResult(new CalculatorResponse() { Result = result });
        }

        public override async Task PrimeNumber(PrimeNumberRequest request, IServerStreamWriter<PrimeNumberResponse> responseStream, ServerCallContext context)
        {
            Console.WriteLine("The server received the request : ");
            Console.WriteLine(request.ToString());

            foreach (int i in GetPrimes(request.NumberForDecomposition.Number))
            {
                await responseStream.WriteAsync(new PrimeNumberResponse
                {
                    Result = i
                });
            }
        }

        public List<int> GetPrimes(int number)
        {
            var primes = new List<int>();

            for (int div = 2; div <= number; div++)
            {
                while (number % div == 0)
                {
                    primes.Add(div);
                    number /= div;
                }
            }
            return primes;
        }
    }
}