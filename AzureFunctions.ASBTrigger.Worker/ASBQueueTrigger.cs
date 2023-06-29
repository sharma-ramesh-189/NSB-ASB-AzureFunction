using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using NServiceBus;


class ASBQueueTrigger
{
    readonly IFunctionEndpoint functionEndpoint;

    public ASBQueueTrigger(IFunctionEndpoint functionEndpoint)
    {
        this.functionEndpoint = functionEndpoint;
    }

    [Function("ASB-QueueTrigger")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req,
        FunctionContext executionContext)
    {
        LogInformation(executionContext);
        await SendMessageAsync(executionContext);
        return await GenerateResponse(req);
    }

    private async Task SendMessageAsync(FunctionContext executionContext)
    {
        var sendOptions = new SendOptions();
        sendOptions.RouteToThisEndpoint();

        await functionEndpoint.Send(new TriggerMessage(), sendOptions, executionContext);
    }

    private static void LogInformation(FunctionContext executionContext)
    {
        var logger = executionContext.GetLogger<ASBQueueTrigger>();
        logger.LogInformation("C# HTTP trigger function received a request.");
    }

    private static async Task<HttpResponseData> GenerateResponse(HttpRequestData req)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteStringAsync($"{nameof(TriggerMessage)} sent.");
        return response;
    }
}
