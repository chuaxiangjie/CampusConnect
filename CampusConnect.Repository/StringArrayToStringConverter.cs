using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace CampusConnect.Data;

public class StringArrayToStringConverter : ValueConverter<string[], string>
{
    public StringArrayToStringConverter()
        : base(
            v => string.Join(",", v),
            v => v.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
    {
    }
}