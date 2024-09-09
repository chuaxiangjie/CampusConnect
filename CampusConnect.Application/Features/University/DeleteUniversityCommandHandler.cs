using MediatR;
using System.Threading.Tasks;
using System.Threading;
using CampusConnect.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace CampusConnect.Application.Features.Institution;

public class DeleteUniversityCommand : EnvelopeRequest<Guid>
{
    public Guid Id { get; set; }
}

public class DeleteUniversityCommandHandler(AppDbContext dbContext) : IRequestHandler<DeleteUniversityCommand, Envelope<Guid>>
{
    public async Task<Envelope<Guid>> Handle(DeleteUniversityCommand command, CancellationToken cancellationToken)
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

        dbContext.Universities.Remove(existingUniversity);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new Envelope<Guid>
        {
            Data = existingUniversity.Id,
            Response = ResponseType.Success
        };
    }
}