using System.Text.Json.Serialization;

namespace WebApiService.Models
{
    public class PaginationRequestModel
    {
        /// <summary> Идентификатор города </summary>
        [JsonPropertyName("cityId")]
        public ulong CityId { get; set; }

        /// <summary> Атрибут сортировки. Может принимать значения = {Id, Name}. Необязательный параметр </summary>
        [JsonPropertyName("sortBy")]
        public string SortBy { get; set; } = string.Empty;

        /// <summary> Сортировать ли по убыванию. Необязательный параметр </summary>
        [JsonPropertyName("descending")]
        public bool Descending { get; set; }

        /// <summary> Кол-во элементов на странице </summary>
        [JsonPropertyName("elementsPerPage")]
        public int ElementsPerPage { get; set; }
        
        /// <summary> Номер страницы </summary>
        [JsonPropertyName("pageNumber")]
        public int PageNumber { get; set; }

        /// <summary> Фильтр для поиска по наименованию ресторана. Необязательный параметр </summary>
        [JsonPropertyName("filter")]
        public string Filter { get; set; } = string.Empty;
        
        /// <summary> Фильтр по расположению в черте центра города. Необязательный параметр </summary>
        [JsonPropertyName("isCenterLocation")]
        public bool IsCenterLocation { get; set; }
    }
}
