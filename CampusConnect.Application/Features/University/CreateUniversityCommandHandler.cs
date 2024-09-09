using MediatR;
using System.Threading.Tasks;
using System.Threading;
using CampusConnect.Data;
using Microsoft.EntityFrameworkCore;
using System;
using CampusConnect.Domain;

namespace CampusConnect.Application.Features.Institution;

public class CreateUniversityCommand : EnvelopeRequest<Guid>
{
    public string Name { get; set; }
    public Guid CountryId { get; set; }
    public string[] Webpages { get; set; }
    public bool IsActive { get; set; }
}

public class CreateUniversityCommandHandler(AppDbContext dbContext) : IRequestHandler<CreateUniversityCommand, Envelope<Guid>>
{
    public async Task<Envelope<Guid>> Handle(CreateUniversityCommand command, CancellationToken cancellationToken)
    {
        var country = await dbContext.Country.FindAsync(command.CountryId);
        if (country is null)
        {
            return new Envelope<Guid>
            {
                Response = ResponseType.Error,
                Error = "Country id not exist."
            };
        }

        var existingUniversity = await dbContext.Universities
            .FirstOrDefaultAsync(x => x.Name == command.Name && x.CountryId == command.CountryId);

        if (existingUniversity != null)
        {
            return new Envelope<Guid>
            {
                Response = ResponseType.Error,
                Error = "A university with the same name and country already exists."
            };
        }

        var newUniversity = new University
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            CountryId = command.CountryId,
            Webpages = command.Webpages,
            IsActive = command.IsActive
        };

        dbContext.Universities.Add(newUniversity);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new Envelope<Guid>
        {
            Data = newUniversity.Id,
            Response = ResponseType.Success
        };
    }
}