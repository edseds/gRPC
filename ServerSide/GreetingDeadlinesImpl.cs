using GreetDeadlines;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GreetDeadlines.GreetDeadlinesService;

namespace ServerSide
{
    public class GreetingDeadlinesImpl : GreetDeadlinesServiceBase
    {
        public override async Task<GreetDeadlinesResponse> greet_with_deadline(GreetDeadlinesRequest request, ServerCallContext context)
        {
            await Task.Delay(3000);
            return new GreetDeadlinesResponse() { Result = string.Format("Hello {0}", request.Name) };
        }
    }
}
