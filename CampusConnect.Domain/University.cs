using System;

namespace CampusConnect.Domain;

public class University : IAuditable, ISoftDelete
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Country Country { get; set; }
    public Guid CountryId { get; set; }
    public string[] Webpages { get; set; }
    public bool IsActive { get; set; }
    public DateTime Created { get; set; }
    public DateTime? LastModified { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}