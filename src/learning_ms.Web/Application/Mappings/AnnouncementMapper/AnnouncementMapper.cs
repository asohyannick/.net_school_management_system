using learning_ms.Web.Application.Common.DTOs.Announcements;
using learning_ms.Web.Domain.Entities.Announcements;
namespace learning_ms.Web.Application.Mappings.AnnouncementMapper;
using Riok.Mapperly.Abstractions;

[Mapper]
public partial class AnnouncementMapper
{
    [MapperIgnoreTarget(nameof(Announcement.Id))]
    [MapperIgnoreTarget(nameof(Announcement.PublishedAt))]
    [MapperIgnoreTarget(nameof(Announcement.IsPublished))]
    [MapperIgnoreTarget(nameof(Announcement.IsArchived))]
    [MapperIgnoreTarget(nameof(Announcement.AttachmentUrls))]
    [MapperIgnoreTarget(nameof(Announcement.ViewCount))]
    [MapperIgnoreTarget(nameof(Announcement.LikeCount))]
    [MapperIgnoreTarget(nameof(Announcement.ShareCount))]
    [MapperIgnoreTarget(nameof(Announcement.ReadCount))]
    [MapperIgnoreTarget(nameof(Announcement.CreatedBy))]
    [MapperIgnoreTarget(nameof(Announcement.CreatedByRole))]
    [MapperIgnoreTarget(nameof(Announcement.IsApproved))]
    [MapperIgnoreTarget(nameof(Announcement.ApprovedBy))]
    [MapperIgnoreTarget(nameof(Announcement.ApprovedAt))]
    [MapperIgnoreTarget(nameof(Announcement.RejectionReason))]
    [MapperIgnoreTarget(nameof(Announcement.CreatedAt))]
    [MapperIgnoreTarget(nameof(Announcement.UpdatedAt))]
    [MapperIgnoreSource(nameof(CreateAnnouncementRequestDto.Id))]
    [MapperIgnoreSource(nameof(CreateAnnouncementRequestDto.CreatedBy))]
    [MapperIgnoreSource(nameof(CreateAnnouncementRequestDto.CreatedByRole))]
    [MapperIgnoreSource(nameof(CreateAnnouncementRequestDto.PublishedAt))]
    [MapperIgnoreSource(nameof(CreateAnnouncementRequestDto.IsPublished))]
    [MapperIgnoreSource(nameof(CreateAnnouncementRequestDto.IsArchived))]
    [MapperIgnoreSource(nameof(CreateAnnouncementRequestDto.ViewCount))]
    [MapperIgnoreSource(nameof(CreateAnnouncementRequestDto.LikeCount))]
    [MapperIgnoreSource(nameof(CreateAnnouncementRequestDto.ShareCount))]
    [MapperIgnoreSource(nameof(CreateAnnouncementRequestDto.ReadCount))]
    [MapperIgnoreSource(nameof(CreateAnnouncementRequestDto.IsApproved))]
    [MapperIgnoreSource(nameof(CreateAnnouncementRequestDto.ApprovedBy))]
    [MapperIgnoreSource(nameof(CreateAnnouncementRequestDto.ApprovedAt))]
    [MapperIgnoreSource(nameof(CreateAnnouncementRequestDto.RejectionReason))]
    [MapperIgnoreSource(nameof(CreateAnnouncementRequestDto.CreatedAt))]
    [MapperIgnoreSource(nameof(CreateAnnouncementRequestDto.UpdatedAt))]
    [MapperIgnoreSource(nameof(CreateAnnouncementRequestDto.Attachments))]
    public partial Announcement ToEntity(CreateAnnouncementRequestDto dto);

    [MapperIgnoreSource(nameof(Announcement.PublishedAt))]
    [MapperIgnoreSource(nameof(Announcement.IsArchived))]
    [MapperIgnoreSource(nameof(Announcement.ViewCount))]
    [MapperIgnoreSource(nameof(Announcement.LikeCount))]
    [MapperIgnoreSource(nameof(Announcement.ShareCount))]
    [MapperIgnoreSource(nameof(Announcement.ReadCount))]
    [MapperIgnoreSource(nameof(Announcement.CreatedBy))]
    [MapperIgnoreSource(nameof(Announcement.CreatedByRole))]
    [MapperIgnoreSource(nameof(Announcement.ApprovedBy))]
    [MapperIgnoreSource(nameof(Announcement.ApprovedAt))]
    [MapperIgnoreSource(nameof(Announcement.RejectionReason))]
    [MapperIgnoreSource(nameof(Announcement.UpdatedAt))]
    public partial CreateAnnouncementResponseDto ToResponseDto(Announcement entity);
}
