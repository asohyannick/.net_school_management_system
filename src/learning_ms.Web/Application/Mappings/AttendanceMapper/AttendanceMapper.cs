using learning_ms.Web.Application.Common.DTOs.Attendance;
using learning_ms.Web.Domain.Entities.Attendances;
namespace learning_ms.Web.Application.Mappings.AttendanceMapper;
using Riok.Mapperly.Abstractions;
[Mapper]
public partial class AttendanceMapper
{
  [MapperIgnoreTarget(nameof(Attendance.Id))]
  [MapperIgnoreTarget(nameof(Attendance.IsLate))]
  [MapperIgnoreTarget(nameof(Attendance.MinutesLate))]
  [MapperIgnoreTarget(nameof(Attendance.RecordedBy))]
  [MapperIgnoreTarget(nameof(Attendance.CreatedAt))]
  [MapperIgnoreTarget(nameof(Attendance.UpdatedAt))]
  [MapperIgnoreTarget(nameof(Attendance.CreatedBy))]
  [MapperIgnoreTarget(nameof(Attendance.UpdatedBy))]
  [MapperIgnoreTarget(nameof(Attendance.Course))]
  [MapperIgnoreTarget(nameof(Attendance.Tutor))]
  [MapperIgnoreSource(nameof(CreateAttendanceRequestDto.Id))]
  [MapperIgnoreSource(nameof(CreateAttendanceRequestDto.RecordedBy))]
  [MapperIgnoreSource(nameof(CreateAttendanceRequestDto.IsLate))]
  [MapperIgnoreSource(nameof(CreateAttendanceRequestDto.MinutesLate))]
  [MapperIgnoreSource(nameof(CreateAttendanceRequestDto.CreatedAt))]
  [MapperIgnoreSource(nameof(CreateAttendanceRequestDto.UpdatedAt))]
  [MapperIgnoreSource(nameof(CreateAttendanceRequestDto.CreatedBy))]
  [MapperIgnoreSource(nameof(CreateAttendanceRequestDto.UpdatedBy))]
  public partial Attendance ToEntity(CreateAttendanceRequestDto dto);

  [MapperIgnoreSource(nameof(Attendance.CreatedBy))]
  [MapperIgnoreSource(nameof(Attendance.UpdatedBy))]
  [MapperIgnoreSource(nameof(Attendance.UpdatedAt))]
  [MapperIgnoreSource(nameof(Attendance.Course))]
  [MapperIgnoreSource(nameof(Attendance.Tutor))]
  public partial CreateAttendanceResponseDto ToResponseDto(Attendance entity);
}
