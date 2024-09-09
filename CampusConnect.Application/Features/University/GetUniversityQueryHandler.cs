using MediatR;
using System.Threading.Tasks;
using System.Threading;
using CampusConnect.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CampusConnect.Application.Shared.Dtos;
using System;

namespace CampusConnect.Application.Features.Institution;

public class GetUniversityQuery : EnvelopeRequest<UniversityOutput>
{
    public int UserId { get; set; }
    public Guid Id { get; set; }
}

public class GetUniversityQueryHandler(AppDbContext dbContext) : IRequestHandler<GetUniversityQuery, Envelope<UniversityOutput>>
{
    public async Task<Envelope<UniversityOutput>> Handle(GetUniversityQuery request, CancellationToken cancellationToken)
    {
        var queryable = dbContext.Universities
           .Include(x => x.Country)
           .AsQueryable();

        var university = await queryable.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (university == null)
            return new Envelope<UniversityOutput>
            {
                Response = ResponseType.NotFound
            };

        var bookmark = await dbContext.UserUniversityBookmarks
            .SingleOrDefaultAsync(x => x.UniversityId == university.Id && x.UserId == request.UserId);

        var result = new UniversityOutput
        {
            Id = university.Id,
            Name = university.Name,
            CountryId = university.CountryId,
            CountryCode = university.Country.CountryCode,
            Webpages = university.Webpages,
            IsActive = university.IsActive,
            IsBookmark = bookmark is not null
        };

        return new Envelope<UniversityOutput>()
        {
            Data = result,
            Response = ResponseType.Success
        };
    }
}