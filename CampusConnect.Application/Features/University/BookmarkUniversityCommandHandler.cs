using MediatR;
using System.Threading.Tasks;
using System.Threading;
using CampusConnect.Data;
using Microsoft.EntityFrameworkCore;
using System;
using CampusConnect.Domain;

namespace CampusConnect.Application.Features.Institution;

public class BookmarkUniversityCommand : EnvelopeRequest<Guid>
{
    public Guid Id { get; set; }
    public int UserId { get; set; }
}

public class BookmarkUniversityCommandHandler(AppDbContext dbContext) : IRequestHandler<BookmarkUniversityCommand, Envelope<Guid>>
{
    public async Task<Envelope<Guid>> Handle(BookmarkUniversityCommand command, CancellationToken cancellationToken)
    {
        var existingUniversity = await dbContext.Universities
            .FirstOrDefaultAsync(x => x.Id == command.Id);

        if (existingUniversity == null)
        {
            return new Envelope<Guid>
            {
                Response = ResponseType.NotFound,
                Error = "University id not exist."
            };
        }

        if (!existingUniversity.IsActive)
        {
            return new Envelope<Guid>
            {
                Response = ResponseType.Error,
                Error = "Unable to bookmark inactive university."
            };
        }

        var existingBookmark = await dbContext.UserUniversityBookmarks
            .FirstOrDefaultAsync(b => b.UniversityId == command.Id && b.UserId == command.UserId);

        if (existingBookmark != null)
        {
            return new Envelope<Guid>
            {
                Response = ResponseType.Success
            };
        }

        var newBookmark = new UserUniversityBookmark
        {
            UserId = command.UserId,
            UniversityId = command.Id
        };

        dbContext.UserUniversityBookmarks.Add(newBookmark);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new Envelope<Guid>
        {
            Data = existingUniversity.Id,
            Response = ResponseType.Success
        };
    }
}