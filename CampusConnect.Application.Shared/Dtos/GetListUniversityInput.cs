namespace CampusConnect.Application.Shared.Dtos;

public class GetListUniversityInput
{
    public string Name { get; set; }
    public string CountryCode { get; set; }
    public bool? IsBookmark { get; set; }
    public bool IsActive { get; set; } = true;
}