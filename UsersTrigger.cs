using System.Net;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace TOTVS.Tests.Function
{
    public static class UsersTrigger
    {
        [Function("users")]
        public static async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequestData req, FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("UsersTrigger");
            try
            {
                logger.LogInformation("Users function was trigged");

                var goRestAccessToken = Environment.GetEnvironmentVariable("GO_REST_ACCESS_TOKEN");
                var goRestApi = Environment.GetEnvironmentVariable("GO_REST_API_URL");

                using var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var request = new HttpRequestMessage(HttpMethod.Get, new Uri($"{goRestApi}/v1/users"));
                var apiResponse = await client.SendAsync(request);

                if (apiResponse is null)
                    throw new Exception($"Internal comunication with GoRestApi failure. | Error={apiResponse.ReasonPhrase}");

                logger.LogInformation("Users function was finished. | StatusCode={statusCode}", (int)apiResponse.StatusCode);

                var resp = req.CreateResponse(apiResponse.StatusCode);
                resp.Headers.Add("Content-Type", "application/json; charset=utf8");
                resp.WriteString(await apiResponse.Content.ReadAsStringAsync());
                return resp;
            }
            catch (Exception e)
            {
                logger.LogError(e, "Execution users function error");
                var resp = req.CreateResponse(HttpStatusCode.InternalServerError);
                resp.Headers.Add("Content-Type", "application/json; charset=utf8");
                resp.WriteString(JsonSerializer.Serialize(new { message = e.Message }));
                return resp;
            }
        }
    }
}
