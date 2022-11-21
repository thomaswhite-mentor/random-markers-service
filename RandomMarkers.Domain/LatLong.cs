using RandomMarkers.Domain.common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomMarkers.Domain
{
    public class LatLong
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        private LatLong(double latitude , double longitude) {
            Latitude = latitude;
            Longitude = longitude;
        }
        public static Result<LatLong> Create(string latLong)
        {
            if (!latLong.Contains(','))
            {
                return Result.Fail<LatLong>("The coordinates should be comma, separated");

            }
            if (string.IsNullOrWhiteSpace(latLong))
                return Result.Fail<LatLong>("location can't be empty");

            try
            {
                float latitude = float.Parse(latLong.Split(',')[0].Trim(), CultureInfo.InvariantCulture);
                float longitude = float.Parse(latLong.Split(',')[1].Trim(), CultureInfo.InvariantCulture);
                return Result.Ok(new LatLong(latitude, longitude));
            }
            catch
            {
                return Result.Fail<LatLong>("location can't be empty");
            }
        }
    }
}
