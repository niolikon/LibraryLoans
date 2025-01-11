using System.Net;

namespace LibraryLoans.Core.BaseClasses;

public class BaseRestException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public object Error { get; }

    public BaseRestException(HttpStatusCode statusCode, string errorMessage)
    {
        StatusCode = statusCode;
        Error = new { error = errorMessage };
    }
}
