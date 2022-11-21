using RandomMarkers.Domain.common;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace RandomMarkers.Domain
{
    public class RandomGeoLocation
    {
        public string Id { get; private set; }
        public string Value { get; private set; }

        private RandomGeoLocation(string id ,string value)
        {
            this.Id = id;
            this.Value = value;
        }
        public static Result<RandomGeoLocation> Create(string southWest, string northEast)
        {
            if (String.IsNullOrEmpty(southWest))
                return Result.Fail<RandomGeoLocation>("southWest can't be empty");

            if (String.IsNullOrEmpty(northEast))
                return Result.Fail<RandomGeoLocation>("southWest can't be empty");


            var createdNorthEast = LatLong.Create(northEast);
            if (createdNorthEast.Failure)
            {
                return Result.Fail<RandomGeoLocation>(createdNorthEast.Error);
            }

            var createdsouthWest = LatLong.Create(southWest);
            if (createdsouthWest.Failure)
            {
                return Result.Fail<RandomGeoLocation>(createdsouthWest.Error);
            }

            var lngSpan = createdNorthEast.Value?.Longitude - createdsouthWest.Value?.Longitude;
            var latSpan = createdNorthEast.Value?.Latitude - createdsouthWest.Value?.Latitude;
           
            Random rnd = new Random();
            
            var pointx =createdsouthWest.Value?.Latitude + latSpan * rnd.NextDouble();
            var pointy =createdsouthWest.Value?.Longitude + lngSpan * rnd.NextDouble();

            var location = $"{pointx} {pointy}";
            return Result.Ok(new RandomGeoLocation("A",location.Replace(',' ,'.')));
        }

    }
}