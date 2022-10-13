using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApiService.Models
{
    public class RestaurantModel
    {
        /// <summary> Идентификатор ресторана </summary>
        [BindNever] 
        [JsonPropertyName("id")]
        public ulong Id { get; set; }

        /// <summary> Идентификатор города </summary>
        [BindNever]
        [JsonPropertyName("cityId")]
        public ulong CityId { get; set; }

        /// <summary> Наименование ресторана </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
