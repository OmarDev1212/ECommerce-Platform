using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Errors
{
    public class ApiErrorResponse
    {

        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
        public IEnumerable<string>? Errors { get; set; }
        public ApiErrorResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            ErrorMessage = message ?? GetDefaultMessageForStatusCode(statusCode);
        }
        public ApiErrorResponse()
        {

        }
        private static string GetDefaultMessageForStatusCode(int statusCode) => statusCode switch
        {
            400 => "A bad request , you have made",
            401 => "Authorized, you are not",
            404 => "Resource was not found",
            _ => null
        };
    }
}
