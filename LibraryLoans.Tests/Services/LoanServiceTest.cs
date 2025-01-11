using LibraryLoans.Core.Services;
using LibraryLoans.Core.Dtos;
using LibraryLoans.Core.Entities;
using LibraryLoans.Core.Mappers;
using LibraryLoans.Core.Repositories;
using Moq;
using FluentAssertions;

namespace LibraryLoans.Tests.Services;

public class LoanServiceTest
{
    private Mock<ILoanRepository> _repositoryMock;

    private Mock<ILoanMapper> _mapperMock;

    private LoanService _loanService;

    public LoanServiceTest()
    {
        _repositoryMock = new Mock<ILoanRepository>();
        _mapperMock = new Mock<ILoanMapper>();
        _loanService = new LoanService(_repositoryMock.Object, _mapperMock.Object);
    }
}
