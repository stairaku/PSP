using System.Text;
using Mapster;
using Pims.Api.Models.Requests.Geocoder;
using GModel = Pims.Geocoder.Models;

namespace Pims.Api.Areas.Tools.Mappers
{
    /// <summary>
    /// AddressMap class, maps the model properties.
    /// </summary>
    public class GeoAddressResponseMap : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GModel.FeatureModel, GeoAddressResponse>()
                .Map(dest => dest.SiteId, src => src.Properties.SiteID)
                .Map(dest => dest.FullAddress, src => src.Properties.FullAddress)
                .Map(dest => dest.Address1, src => GetAddress1(src.Properties))
                .Map(dest => dest.AdministrativeArea, src => GetAdministrativeArea(src.Properties))
                .Map(dest => dest.ProvinceCode, src => src.Properties.ProvinceCode)
                .Map(dest => dest.Longitude, src => GetLongtitude(src.Geometry))
                .Map(dest => dest.Latitude, src => GetLatitude(src.Geometry))
                .Map(dest => dest.Score, src => src.Properties.Score);
        }

        /// <summary>
        /// Create an address based on the model property values.
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        private static string GetAddress1(GModel.PropertyModel properties)
        {
            var address = new StringBuilder();
            if (!string.IsNullOrWhiteSpace($"{properties.CivicNumber}"))
            {
                address.Append($"{properties.CivicNumber} ");
            }

            if (properties.IsStreetTypePrefix && !string.IsNullOrWhiteSpace(properties.StreetType))
            {
                address.Append($"{properties.StreetType} ");
            }

            if (properties.IsStreetDirectionPrefix && !string.IsNullOrWhiteSpace(properties.StreetDirection))
            {
                address.Append($"{properties.StreetDirection} ");
            }

            if (!string.IsNullOrWhiteSpace(properties.StreetName))
            {
                address.Append(properties.StreetName);
            }

            if (!string.IsNullOrWhiteSpace(properties.StreetQualifier))
            {
                address.Append($" {properties.StreetQualifier}");
            }

            if (!properties.IsStreetDirectionPrefix && !string.IsNullOrWhiteSpace(properties.StreetDirection))
            {
                address.Append($" {properties.StreetDirection}");
            }

            if (!properties.IsStreetTypePrefix && !string.IsNullOrWhiteSpace(properties.StreetType))
            {
                address.Append($" {properties.StreetType}");
            }

            return address.ToString();
        }

        /// <summary>
        /// Get the administrative area name (city, municipality, district, etc.) based on the model property values.
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        private static string GetAdministrativeArea(GModel.PropertyModel properties)
        {
            return properties.LocalityName;
        }

        /// <summary>
        /// Get the latitude from the property value.
        /// </summary>
        /// <param name="geometry"></param>
        /// <returns></returns>
        private static double GetLatitude(GModel.GeometryModel geometry)
        {
            if (geometry.Coordinates?.Length == 2)
            {
                return geometry.Coordinates[1];
            }

            return 0;
        }

        /// <summary>
        /// Get the longitude from the property value.
        /// </summary>
        /// <param name="geometry"></param>
        /// <returns></returns>
        private static double GetLongtitude(GModel.GeometryModel geometry)
        {
            if (geometry.Coordinates?.Length >= 1)
            {
                return geometry.Coordinates[0];
            }

            return 0;
        }
    }
}
