using System.Net;

namespace LibraryLoans.Core.Commons;

public class BaseRestException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public object Error { get; }

    public BaseRestException(HttpStatusCode statusCode, string errorMessage) : base(errorMessage)
    {
        StatusCode = statusCode;
        Error = new { error = errorMessage };
    }
}
