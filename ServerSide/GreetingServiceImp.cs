using Greet;
using Grpc.Core;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Greet.GreetingService;

namespace ServerSide
{
    public class GreetingServiceImp :GreetingServiceBase
    {
        public override Task<GreetingResponse> Greet(GreetingRequest request, ServerCallContext context)
        {
            string result = string.Format("Hello {0} {1}", request.Greeting.FirstName, request.Greeting.LastName);
            return Task.FromResult(new GreetingResponse() { Result = result });
        }

        public override async Task GreetManyTimes(GreetingManyTimesRequest request, IServerStreamWriter<GreetingManyTimesResponse> responseStream, ServerCallContext context)
        {
            Console.WriteLine("The server received the request : ");
            Console.WriteLine(request.ToString());

            string result = string.Format("Hello {0} {1}", request.Greeting.FirstName, request.Greeting.LastName);

            foreach(int i in Enumerable.Range(1,10))
            {
                await responseStream.WriteAsync(new GreetingManyTimesResponse()
                {
                    Result = result
                });
            }
        }

        public override async Task<LongGreetingResponse> LongGreet(IAsyncStreamReader<LongGreetingRequest> requestStream, ServerCallContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            while(await requestStream.MoveNext())
            {
                stringBuilder.AppendLine(string.Format("Hello {0} {1}",
                    requestStream.Current.Greeting.FirstName,requestStream.Current.Greeting.LastName));
            }

            return new LongGreetingResponse() { Result = stringBuilder.ToString() };
        }

        public override async Task GreetEveryone(IAsyncStreamReader<GreetEveryoneRequest> requestStream, IServerStreamWriter<GreetEveryoneResponse> responseStream, ServerCallContext context)
        {
            while (await requestStream.MoveNext())
            {
                var result = string.Format("Hello {0} {1}",
                    requestStream.Current.Greeting.FirstName,
                    requestStream.Current.Greeting.LastName);

                Console.WriteLine("Received: {0}", result);

                await responseStream.WriteAsync(new GreetEveryoneResponse() { Result = result });
            }
        }
    }
}
