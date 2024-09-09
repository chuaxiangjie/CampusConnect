using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using CampusConnect.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CampusConnect.Application.Shared.Dtos;
using CampusConnect.Application.Extensions;

namespace CampusConnect.Application.Features.Institution;

public class GetListUniversityQuery : EnvelopeRequest<IReadOnlyList<UniversityOutput>>
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string CountryCode { get; set; }
    public bool? IsBookmark { get; set; }
    public bool IsActive { get; set; }
}

public class GetListUniversityQueryHandler(AppDbContext dbContext) : IRequestHandler<GetListUniversityQuery, Envelope<IReadOnlyList<UniversityOutput>>>
{
    public async Task<Envelope<IReadOnlyList<UniversityOutput>>> Handle(GetListUniversityQuery request, CancellationToken cancellationToken)
    {
        var result = await GetUniversitiesAsync(request.UserId, request.Name, request.CountryCode, request.IsActive, request.IsBookmark);

        return new Envelope<IReadOnlyList<UniversityOutput>>()
        {
            Data = result,
            Response = ResponseType.Success
        };
    }

    private async Task<IReadOnlyList<UniversityOutput>> GetUniversitiesAsync(int userId, string name, string countryCode, bool isActive, bool? isBookmark)
    {
        var queryable = dbContext.Universities
            .Include(x => x.Country)
            .AsQueryable();

        // Apply filters
        queryable = queryable
            .WhereIf(!string.IsNullOrEmpty(name), x => x.Name.Contains(name))
            .WhereIf(!string.IsNullOrEmpty(countryCode), x => x.Country.CountryCode == countryCode)
            .Where(x => x.IsActive == isActive);

        // Apply default sort
        queryable = queryable.OrderBy(x => x.CountryId).ThenBy(x => x.Name);

        var bookmarkedUniversities = dbContext.UserUniversityBookmarks
            .AsQueryable();
        
        // Filter universities based on bookmark status
        var resultQuery = from q in queryable
            join b in bookmarkedUniversities on new { UniversityId = q.Id, UserId = userId } equals new { b.UniversityId, b.UserId } into qb
            from bookmark in qb.DefaultIfEmpty()
            // Only apply bookmark filter if it has a value, otherwise retrieve all universities
            where (!isBookmark.HasValue ||
                  (isBookmark.Value && bookmark != null) ||
                  (!isBookmark.Value && bookmark == null))
            select new
            {
                 q.Id,
                 q.Name,
                 q.CountryId,
                 q.Country.CountryCode,
                 q.Webpages,
                 q.IsActive,
                 IsBookmark = bookmark != null
            };

        var result = await resultQuery
             .Select(x => new UniversityOutput
             {
                  Id = x.Id,
                  Name = x.Name,
                  CountryId = x.CountryId,
                  CountryCode = x.CountryCode,
                  Webpages = x.Webpages,
                  IsActive = x.IsActive,
                  IsBookmark = x.IsBookmark
             })
            .ToListAsync();

        return result.AsReadOnly();
    }
}