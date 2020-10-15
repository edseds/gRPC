using Calc;
using Grpc.Core;
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
    }
}
