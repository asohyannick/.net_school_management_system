using learning_ms.Web.Application.Common.DTOs.Book;
using learning_ms.Web.Domain.Entities.Books;
namespace learning_ms.Web.Application.Mappings.BookMapper;
using Riok.Mapperly.Abstractions;

[Mapper]
public partial class BookMapper
{
  [MapperIgnoreTarget(nameof(Book.Id))]
  [MapperIgnoreTarget(nameof(Book.CoverImageUrl))]
  [MapperIgnoreTarget(nameof(Book.IsActive))]
  [MapperIgnoreTarget(nameof(Book.CreatedAt))]
  [MapperIgnoreTarget(nameof(Book.UpdatedAt))]
  [MapperIgnoreTarget(nameof(Book.Library))]
  [MapperIgnoreTarget(nameof(Book.BookLoans))]
  [MapperIgnoreSource(nameof(CreateBookRequestDto.Id))]
  [MapperIgnoreSource(nameof(CreateBookRequestDto.IsActive))]
  [MapperIgnoreSource(nameof(CreateBookRequestDto.CreatedAt))]
  [MapperIgnoreSource(nameof(CreateBookRequestDto.UpdatedAt))]
  [MapperIgnoreSource(nameof(CreateBookRequestDto.CoverImage))]
  public partial Book ToEntity(CreateBookRequestDto dto);

  [MapperIgnoreSource(nameof(Book.UpdatedAt))]
  [MapperIgnoreSource(nameof(Book.Library))]
  [MapperIgnoreSource(nameof(Book.BookLoans))]
  public partial CreateBookResponseDto ToResponseDto(Book entity);
}
