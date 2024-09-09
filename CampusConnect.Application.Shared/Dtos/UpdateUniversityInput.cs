using System;
using System.ComponentModel.DataAnnotations;

namespace CampusConnect.Application.Shared.Dtos;

public class UpdateUniversityInput
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    public Guid CountryId { get; set; }

    [ValidUrlArray]
    public string[] Webpages { get; set; }
    public bool IsActive { get; set; }
}