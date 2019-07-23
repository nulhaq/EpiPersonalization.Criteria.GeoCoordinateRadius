using System.Device.Location;
using System.Security.Principal;
using System.Web;
using EpiPersonalization.Criteria.GeoCoordinateRadius.Models;
using EPiServer.Personalization.VisitorGroups;

namespace EpiPersonalization.Criteria.GeoCoordinateRadius
{
    [VisitorGroupCriterion(
        Category = "Geolocation",
        DisplayName = "GeoCoordinate & Radius",
        Description = "Checks visitor's location falls in specified radius")]

    public class GeoCoordinateRadiusCriterion : CriterionBase<GeoCoordinateRadiusCriterionSettings>
    {
        public override bool IsMatch(IPrincipal principal, HttpContextBase httpContext)
        {
            //user location
            var visitorlocation = GetVisitorLatLong();
            double sLatitude = visitorlocation.Latitude;
            double sLongitude = visitorlocation.Longitude;

            // center point location
            double eLatitude = Model.CentralizeLocationLatitude;
            double eLongitude = Model.CentralizeLocationLongitude;

            var sCoord = new GeoCoordinate(sLatitude, sLongitude);
            var eCoord = new GeoCoordinate(eLatitude, eLongitude);

            var distance = sCoord.GetDistanceTo(eCoord);

            if (distance <= Model.Radius)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private GeoLocation GetVisitorLatLong()
        {
            GeoCoordinateWatcher watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);
            var userLocation = new GeoLocation();
            watcher.Start();
            System.Device.Location.GeoCoordinate coord = watcher.Position.Location;
            if (!watcher.Position.Location.IsUnknown)
            {
                userLocation.Latitude = coord.Latitude;
                userLocation.Longitude = coord.Longitude;
            }

            return userLocation;
        }
    }
}
