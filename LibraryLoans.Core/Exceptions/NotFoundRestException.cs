using LibraryLoans.Core.BaseClasses;
using System.Net;

namespace LibraryLoans.Core.Exceptions;

public class NotFoundRestException : BaseRestException
{
    public NotFoundRestException(string message) : base(HttpStatusCode.NotFound, message) { }
}
