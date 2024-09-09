using System;

namespace CampusConnect.Domain;

public class UserUniversityBookmark : IHasCreationTime
{
    public int UserId { get; set; }
    public Guid UniversityId { get; set; }
    public University University { get; set; }
    public DateTime Created { get; set; }
}