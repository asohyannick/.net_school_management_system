using learning_ms.Web.Application.Common.DTOs.DiscussionForum;
using learning_ms.Web.Domain.Entities.DiscussionForums;
namespace learning_ms.Web.Application.Mappings.DiscussionForumMapper;
using Riok.Mapperly.Abstractions;

[Mapper]
public partial class DiscussionForumMapper
{
    [MapperIgnoreTarget(nameof(DiscussionForum.Id))]
    [MapperIgnoreTarget(nameof(DiscussionForum.Slug))]
    [MapperIgnoreTarget(nameof(DiscussionForum.CreatedBy))]
    [MapperIgnoreTarget(nameof(DiscussionForum.CreatedByRole))]
    [MapperIgnoreTarget(nameof(DiscussionForum.ViewCount))]
    [MapperIgnoreTarget(nameof(DiscussionForum.LikeCount))]
    [MapperIgnoreTarget(nameof(DiscussionForum.ReplyCount))]
    [MapperIgnoreTarget(nameof(DiscussionForum.IsPinned))]
    [MapperIgnoreTarget(nameof(DiscussionForum.IsLocked))]
    [MapperIgnoreTarget(nameof(DiscussionForum.IsApproved))]
    [MapperIgnoreTarget(nameof(DiscussionForum.IsReported))]
    [MapperIgnoreTarget(nameof(DiscussionForum.ReportReason))]
    [MapperIgnoreTarget(nameof(DiscussionForum.IsDeleted))]
    [MapperIgnoreTarget(nameof(DiscussionForum.AttachmentUrls))]
    [MapperIgnoreTarget(nameof(DiscussionForum.LastReplyAt))]
    [MapperIgnoreTarget(nameof(DiscussionForum.LastReplyBy))]
    [MapperIgnoreTarget(nameof(DiscussionForum.CreatedAt))]
    [MapperIgnoreTarget(nameof(DiscussionForum.UpdatedAt))]
    [MapperIgnoreSource(nameof(CreateDiscussionForumRequestDto.Id))]
    [MapperIgnoreSource(nameof(CreateDiscussionForumRequestDto.Slug))]
    [MapperIgnoreSource(nameof(CreateDiscussionForumRequestDto.CreatedBy))]
    [MapperIgnoreSource(nameof(CreateDiscussionForumRequestDto.CreatedByRole))]
    [MapperIgnoreSource(nameof(CreateDiscussionForumRequestDto.ViewCount))]
    [MapperIgnoreSource(nameof(CreateDiscussionForumRequestDto.LikeCount))]
    [MapperIgnoreSource(nameof(CreateDiscussionForumRequestDto.ReplyCount))]
    [MapperIgnoreSource(nameof(CreateDiscussionForumRequestDto.IsPinned))]
    [MapperIgnoreSource(nameof(CreateDiscussionForumRequestDto.IsLocked))]
    [MapperIgnoreSource(nameof(CreateDiscussionForumRequestDto.IsApproved))]
    [MapperIgnoreSource(nameof(CreateDiscussionForumRequestDto.IsReported))]
    [MapperIgnoreSource(nameof(CreateDiscussionForumRequestDto.ReportReason))]
    [MapperIgnoreSource(nameof(CreateDiscussionForumRequestDto.IsDeleted))]
    [MapperIgnoreSource(nameof(CreateDiscussionForumRequestDto.LastReplyAt))]
    [MapperIgnoreSource(nameof(CreateDiscussionForumRequestDto.LastReplyBy))]
    [MapperIgnoreSource(nameof(CreateDiscussionForumRequestDto.CreatedAt))]
    [MapperIgnoreSource(nameof(CreateDiscussionForumRequestDto.UpdatedAt))]
    [MapperIgnoreSource(nameof(CreateDiscussionForumRequestDto.Attachments))]
    public partial DiscussionForum ToEntity(CreateDiscussionForumRequestDto dto);

    [MapperIgnoreSource(nameof(DiscussionForum.ViewCount))]
    [MapperIgnoreSource(nameof(DiscussionForum.LikeCount))]
    [MapperIgnoreSource(nameof(DiscussionForum.ReplyCount))]
    [MapperIgnoreSource(nameof(DiscussionForum.IsReported))]
    [MapperIgnoreSource(nameof(DiscussionForum.ReportReason))]
    [MapperIgnoreSource(nameof(DiscussionForum.IsDeleted))]
    [MapperIgnoreSource(nameof(DiscussionForum.LastReplyAt))]
    [MapperIgnoreSource(nameof(DiscussionForum.LastReplyBy))]
    [MapperIgnoreSource(nameof(DiscussionForum.UpdatedAt))]
    public partial CreateDiscussionForumResponseDto ToResponseDto(DiscussionForum entity);
}
