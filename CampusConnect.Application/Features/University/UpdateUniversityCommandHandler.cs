using MediatR;
using System.Threading.Tasks;
using System.Threading;
using CampusConnect.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CampusConnect.Application.Features.Institution;

public class UpdateUniversityCommand : EnvelopeRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CountryId { get; set; }
    public string[] Webpages { get; set; }
    public bool IsActive { get; set; }
}

public class UpdateUniversityCommandHandler(AppDbContext dbContext) : IRequestHandler<UpdateUniversityCommand, Envelope<Guid>>
{
    public async Task<Envelope<Guid>> Handle(UpdateUniversityCommand command, CancellationToken cancellationToken)
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

        var country = await dbContext.Country.FindAsync(command.CountryId);
        if (country is null)
        {
            return new Envelope<Guid>
            {
                Response = ResponseType.Error,
                Error = "Country id not exist."
            };
        }

        var hasDuplicate = await dbContext.Universities
            .Where(x => x.Id != command.Id && x.CountryId == command.CountryId && x.Name == command.Name)
            .AnyAsync();

        if (hasDuplicate)
        {
            return new Envelope<Guid>
            {
                Response = ResponseType.Error,
                Error = "A university with the same name and country already exists."
            };
        }

        existingUniversity.Name = command.Name;
        existingUniversity.CountryId = command.CountryId;
        existingUniversity.Webpages = command.Webpages;
        existingUniversity.IsActive = command.IsActive;

        dbContext.Universities.Update(existingUniversity);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new Envelope<Guid>
        {
            Data = existingUniversity.Id,
            Response = ResponseType.Success
        };
    }
}