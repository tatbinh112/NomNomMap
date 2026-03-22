using NomNomMap.Api.Data;
using NomNomMap.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace NomNomMap.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlacesController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly GeometryFactory _geometryFactory;

    public PlacesController(AppDbContext db)
    {
        _db = db;
        _geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
    }

    [HttpGet("near")]
    public async Task<IActionResult> Near(
        [FromQuery] double lat,
        [FromQuery] double lng,
        [FromQuery] double radiusKm = 3)
    {
        // Validate input cơ bản
        if (lat < -90 || lat > 90 || lng < -180 || lng > 180 || radiusKm <= 0 || radiusKm > 100)
        {
            return BadRequest("Invalid lat/lng/radiusKm.");
        }

        var userPoint = _geometryFactory.CreatePoint(new Coordinate(lng, lat));
        var radiusMeters = radiusKm * 1000;

        // Lưu ý: IsWithinDistance cần tham số useSpheroid (bool)
        var result = await _db.Places
            .Where(p => EF.Functions.IsWithinDistance(
                p.Location,
                userPoint,
                radiusMeters,
                true))
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.Address,
                lat = p.Location.Y,
                lng = p.Location.X,
                p.AvgRating
            })
            .ToListAsync();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePlaceRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.Name))
            return BadRequest("Name is required.");

        if (req.Lat < -90 || req.Lat > 90 || req.Lng < -180 || req.Lng > 180)
            return BadRequest("Invalid latitude/longitude.");

        var place = new Place
        {
            Name = req.Name.Trim(),
            Address = req.Address?.Trim() ?? string.Empty,
            AvgRating = req.AvgRating,
            Location = _geometryFactory.CreatePoint(new Coordinate(req.Lng, req.Lat))
        };

        _db.Places.Add(place);
        await _db.SaveChangesAsync();

        return Ok(new
        {
            place.Id,
            place.Name,
            place.Address,
            lat = place.Location.Y,
            lng = place.Location.X,
            place.AvgRating
        });
    }

    public record CreatePlaceRequest(
        string Name,
        string Address,
        double Lat,
        double Lng,
        double AvgRating);
}