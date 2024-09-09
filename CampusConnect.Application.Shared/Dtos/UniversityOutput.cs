using System;

namespace CampusConnect.Application.Shared.Dtos;

public record UniversityOutput
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CountryId { get; set; }
    public string CountryCode { get; set; }
    public string[] Webpages { get; set; }
    public bool IsActive { get; set; }
    public bool IsBookmark { get; set; }
}