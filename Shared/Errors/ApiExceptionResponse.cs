using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Errors
{
    public record ApiExceptionResponse
    {
        public int StatusCode { get; init; }
        public string ErrorMessage { get; init; }
    }
}
