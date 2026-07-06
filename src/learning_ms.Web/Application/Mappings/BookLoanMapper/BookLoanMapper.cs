using learning_ms.Web.Application.Common.DTOs.BookLoan;
using learning_ms.Web.Domain.Entities.BookLoans;
namespace learning_ms.Web.Application.Mappings.BookLoanMapper;
using Riok.Mapperly.Abstractions;
[Mapper]
public partial class BookLoanMapper
{
    [MapperIgnoreTarget(nameof(BookLoan.Id))]
    [MapperIgnoreTarget(nameof(BookLoan.ReturnDate))]
    [MapperIgnoreTarget(nameof(BookLoan.FineAmount))]
    [MapperIgnoreTarget(nameof(BookLoan.FinePaid))]
    [MapperIgnoreTarget(nameof(BookLoan.Status))]
    [MapperIgnoreTarget(nameof(BookLoan.CreatedAt))]
    [MapperIgnoreTarget(nameof(BookLoan.UpdatedAt))]
    [MapperIgnoreTarget(nameof(BookLoan.IssuedBy))]
    [MapperIgnoreTarget(nameof(BookLoan.ReceivedBy))]
    [MapperIgnoreTarget(nameof(BookLoan.Book))]
    [MapperIgnoreTarget(nameof(BookLoan.Library))]
    [MapperIgnoreSource(nameof(CreateBookLoanRequestDto.Id))]
    [MapperIgnoreSource(nameof(CreateBookLoanRequestDto.IssuedBy))]
    [MapperIgnoreSource(nameof(CreateBookLoanRequestDto.ReceivedBy))]
    [MapperIgnoreSource(nameof(CreateBookLoanRequestDto.ReturnDate))]
    [MapperIgnoreSource(nameof(CreateBookLoanRequestDto.FineAmount))]
    [MapperIgnoreSource(nameof(CreateBookLoanRequestDto.FinePaid))]
    [MapperIgnoreSource(nameof(CreateBookLoanRequestDto.Status))]
    [MapperIgnoreSource(nameof(CreateBookLoanRequestDto.CreatedAt))]
    [MapperIgnoreSource(nameof(CreateBookLoanRequestDto.UpdatedAt))]
    public partial BookLoan ToEntity(CreateBookLoanRequestDto dto);

    [MapperIgnoreSource(nameof(BookLoan.ReceivedBy))]
    [MapperIgnoreSource(nameof(BookLoan.UpdatedAt))]
    [MapperIgnoreSource(nameof(BookLoan.Book))]
    [MapperIgnoreSource(nameof(BookLoan.Library))]
    public partial CreateBookLoanResponseDto ToResponseDto(BookLoan entity);
}
