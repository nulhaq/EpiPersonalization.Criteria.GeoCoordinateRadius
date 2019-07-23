using System.ComponentModel.DataAnnotations;
using EPiServer.Personalization.VisitorGroups;

namespace EpiPersonalization.Criteria.GeoCoordinateRadius
{
    public class GeoCoordinateRadiusCriterionSettings : CriterionModelBase
    {
        [Required]
        public double CentralizeLocationLatitude { get; set; }
        [Required]
        public double CentralizeLocationLongitude { get; set; }
        [Required]
        public double Radius { get; set; }

        public override ICriterionModel Copy()
        {
            return ShallowCopy();
        }
    }
}
