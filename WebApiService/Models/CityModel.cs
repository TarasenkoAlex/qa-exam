using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApiService.Models
{
    public class CityModel
    {
        /// <summary> Идентификатор города. </summary>
        [BindNever]
        [JsonPropertyName("id")]
        public ulong Id { get; set; }

        /// <summary> Наимнование города </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
