using System;

namespace CampusConnect.Domain;

public class Country
{
    public Guid Id { get; set; }
    public string CountryCode { get; set; }
    public string Name { get; set; }
}