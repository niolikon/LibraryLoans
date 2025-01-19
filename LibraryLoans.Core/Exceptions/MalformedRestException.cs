using LibraryLoans.Core.Commons;
using System.Net;

namespace LibraryLoans.Core.Exceptions;

public class MalformedRestException : BaseRestException
{
    public MalformedRestException(string message) : base(HttpStatusCode.BadRequest, message) { }
}
