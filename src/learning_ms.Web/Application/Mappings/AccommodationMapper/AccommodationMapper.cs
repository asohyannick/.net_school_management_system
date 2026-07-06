using learning_ms.Web.Application.Common.DTOs.Accommodations;
using learning_ms.Web.Domain.Entities.Accommodations;
namespace learning_ms.Web.Application.Mappings.AccommodationMapper;
using Riok.Mapperly.Abstractions;
[Mapper]
public partial class AccommodationMapper
{
  [MapperIgnoreTarget(nameof(Accommodation.Id))]
  [MapperIgnoreTarget(nameof(Accommodation.HostelImage))]
  [MapperIgnoreTarget(nameof(Accommodation.OccupiedBeds))]
  [MapperIgnoreTarget(nameof(Accommodation.IsAvailable))]
  [MapperIgnoreTarget(nameof(Accommodation.IsActive))]
  [MapperIgnoreTarget(nameof(Accommodation.CreatedAt))]
  [MapperIgnoreTarget(nameof(Accommodation.UpdatedAt))]
  [MapperIgnoreTarget(nameof(Accommodation.StudentAccommodations))]
  [MapperIgnoreSource(nameof(CreateAccommodationRequestDto.HostelImages))]
  [MapperIgnoreSource(nameof(CreateAccommodationRequestDto.Id))]
  [MapperIgnoreSource(nameof(CreateAccommodationRequestDto.OccupiedBeds))]
  [MapperIgnoreSource(nameof(CreateAccommodationRequestDto.IsAvailable))]
  [MapperIgnoreSource(nameof(CreateAccommodationRequestDto.IsActive))]
  [MapperIgnoreSource(nameof(CreateAccommodationRequestDto.CreatedAt))]
  [MapperIgnoreSource(nameof(CreateAccommodationRequestDto.UpdatedAt))]
  public partial Accommodation ToEntity(CreateAccommodationRequestDto dto);

 [MapperIgnoreSource(nameof(Accommodation.StudentAccommodations))]
 [MapperIgnoreSource(nameof(Accommodation.UpdatedAt))]
 [MapProperty(nameof(Accommodation.HostelImage), nameof(CreateAccommodationResponseDto.HostelImageUrls))]
 [MapProperty(nameof(Accommodation.AvailableBeds), nameof(CreateAccommodationResponseDto.AvailableBeds))]
 public partial CreateAccommodationResponseDto ToResponseDto(Accommodation entity);
}
