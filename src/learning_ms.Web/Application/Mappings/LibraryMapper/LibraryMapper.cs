using learning_ms.Web.Application.Common.DTOs.Library;
using learning_ms.Web.Domain.Entities.Libraries;
namespace learning_ms.Web.Application.Mappings.LibraryMapper;
using Riok.Mapperly.Abstractions;

[Mapper]
public partial class LibraryMapper
{
  [MapperIgnoreTarget(nameof(Library.Id))]
  [MapperIgnoreTarget(nameof(Library.Code))]
  [MapperIgnoreTarget(nameof(Library.LibraryImage))]
  [MapperIgnoreTarget(nameof(Library.IsActive))]
  [MapperIgnoreTarget(nameof(Library.CreatedAt))]
  [MapperIgnoreTarget(nameof(Library.UpdatedAt))]
  [MapperIgnoreTarget(nameof(Library.Books))]
  [MapperIgnoreTarget(nameof(Library.BookLoans))]
  [MapperIgnoreSource(nameof(CreateLibraryRequestDto.Id))]
  [MapperIgnoreSource(nameof(CreateLibraryRequestDto.Code))]
  [MapperIgnoreSource(nameof(CreateLibraryRequestDto.IsActive))]
  [MapperIgnoreSource(nameof(CreateLibraryRequestDto.CreatedAt))]
  [MapperIgnoreSource(nameof(CreateLibraryRequestDto.UpdatedAt))]
  [MapperIgnoreSource(nameof(CreateLibraryRequestDto.LibraryImages))]
  public partial Library ToEntity(CreateLibraryRequestDto dto);

  [MapperIgnoreSource(nameof(Library.UpdatedAt))]
  [MapperIgnoreSource(nameof(Library.Books))]
  [MapperIgnoreSource(nameof(Library.BookLoans))]
  public partial CreateLibraryResponseDto ToResponseDto(Library entity);
}
