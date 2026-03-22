using NetTopologySuite.Geometries;

namespace NomNomMap.Api.Models;

public class Place
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public Point Location { get; set; } = default!; // SRID 4326
    public double AvgRating { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
