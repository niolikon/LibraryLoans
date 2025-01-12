using LibraryLoans.Core.BaseClasses;
using System.Net;

namespace LibraryLoans.Core.Exceptions;

public class ConflictRestException: BaseRestException
{
    public ConflictRestException(string message): base(HttpStatusCode.Conflict, message) { }
}
