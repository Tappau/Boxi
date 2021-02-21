using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

/// <summary>
/// Could also use 'AutoWrapper'
/// </summary>
namespace Boxi.Api.MiddleWare
{
    public static class ResponseWrapperExtensions
    {
        public static IApplicationBuilder UseConsistantApiResponses(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiResponseWrapper>();
        }
    }

    public class ApiResponseWrapper
    {
        private readonly RequestDelegate _nextRequestDelegate;

        public ApiResponseWrapper(RequestDelegate nextRequest)
        {
            _nextRequestDelegate = nextRequest;
        }

        public async Task Invoke(HttpContext context)
        {
            var currentBody = context.Response.Body;

            using (var memoryStream = new MemoryStream())
            {
                //set the currentResponse to memoryStream.
                context.Response.Body = memoryStream;

                await _nextRequestDelegate(context);

                //reset the body
                context.Response.Body = currentBody;
                memoryStream.Seek(0, SeekOrigin.Begin);

                var readToEnd = new StreamReader(memoryStream).ReadToEnd();
                var objResult = JsonConvert.DeserializeObject(readToEnd);
                var result =
                    ConsistantApiResponse.Create((HttpStatusCode) context.Response.StatusCode, objResult);
                await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
            }
        }
    }

    public class ConsistantApiResponse
    {
        private ConsistantApiResponse(HttpStatusCode responseStatusCode, object objResult, string errorMessage)
        {
            RequestId = Guid.NewGuid().ToString();
            StatusCode = (int) responseStatusCode;
            ErrorMessage = errorMessage;
            Result = objResult;
        }

        public object Result { get; set; }

        public string ErrorMessage { get; set; }

        public int StatusCode { get; set; }

        public string RequestId { get; }

        public static object Create(HttpStatusCode responseStatusCode, object objResult = null,
            string errorMessage = null)
        {
            return new ConsistantApiResponse(responseStatusCode, objResult, errorMessage);
        }
    }
}