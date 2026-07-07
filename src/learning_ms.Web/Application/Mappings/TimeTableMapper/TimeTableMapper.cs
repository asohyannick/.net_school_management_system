using learning_ms.Web.Application.Common.DTOs.TimeTable;
using learning_ms.Web.Domain.Entities.TimeTable;
using Riok.Mapperly.Abstractions;
namespace learning_ms.Web.Application.Mappings.TimeTableMapper;

[Mapper]
public partial class TimeTableMapper
{
  [MapperIgnoreTarget(nameof(TimeTable.Id))]
  [MapperIgnoreTarget(nameof(TimeTable.IsBreak))]
  [MapperIgnoreTarget(nameof(TimeTable.IsActive))]
  [MapperIgnoreTarget(nameof(TimeTable.CreatedAt))]
  [MapperIgnoreTarget(nameof(TimeTable.UpdatedAt))]
  [MapperIgnoreSource(nameof(CreateTimeTableRequestDto.Id))]
  [MapperIgnoreSource(nameof(CreateTimeTableRequestDto.IsActive))]
  [MapperIgnoreSource(nameof(CreateTimeTableRequestDto.IsBreak))]
  [MapperIgnoreSource(nameof(CreateTimeTableRequestDto.CreatedAt))]
  [MapperIgnoreSource(nameof(CreateTimeTableRequestDto.UpdatedAt))]
  public partial TimeTable ToEntity(CreateTimeTableRequestDto dto);

  [MapperIgnoreSource(nameof(TimeTable.UpdatedAt))]
  public partial CreateTimeTableResponseDto ToResponseDto(TimeTable entity);
}
