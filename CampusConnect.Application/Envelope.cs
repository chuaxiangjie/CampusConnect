using MediatR;
using System.Text.Json.Serialization;

namespace CampusConnect.Application;

public class Envelope<T>
{
    public bool IsSuccess => Response == ResponseType.Success;

    public string Error { get; set; }

    public T Data { get; set; }

    [JsonIgnore]
    public ResponseType Response { get; set; }
}

public class EnvelopeRequest<T> : IRequest<Envelope<T>> { }