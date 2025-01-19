using LibraryLoans.Core.Commons;
using System.Net;

namespace LibraryLoans.Core.Exceptions;

public class NotFoundRestException : BaseRestException
{
    public NotFoundRestException(string message) : base(HttpStatusCode.NotFound, message) { }
}
