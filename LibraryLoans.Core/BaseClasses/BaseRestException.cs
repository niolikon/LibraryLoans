using System.Net;

namespace LibraryLoans.Core.BaseClasses;

public class BaseRestException : Exception
{
    public int StatusCode { get; }
    public object Error { get; }

    public BaseRestException(int statusCode, string errorMessage)
    {
        StatusCode = statusCode;
        Error = new { error = errorMessage };
    }
}
