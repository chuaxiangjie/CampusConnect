using System;

namespace CampusConnect.Domain;

public interface IAuditable : IHasCreationTime, IHasModificationTime
{
}

public interface IHasCreationTime
{
    public DateTime Created { get; set; }
}

public interface IHasModificationTime
{
    public DateTime? LastModified { get; set; }
}