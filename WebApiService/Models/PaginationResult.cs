using System.Text.Json.Serialization;

namespace WebApiService.Models
{
    public class PaginationResult<TData>
    {
        public PaginationResult(TData data, int pageNumber, int elementsPerPage, int totalElements)
        {
            Data = data;
            PageNumber = pageNumber;
            ElementsPerPage = elementsPerPage;
            TotalElements = totalElements;
        }

        /// <summary> Данные </summary>
        [JsonPropertyName("data")]
        public TData Data { get; }
        
        /// <summary> Номер страницы </summary>
        [JsonPropertyName("pageNumber")]
        public int PageNumber { get; }
        
        /// <summary> Размер страницы </summary>
        [JsonPropertyName("pageSize")]
        public int ElementsPerPage { get; }
        
        /// <summary> Общее количество элементов </summary>
        [JsonPropertyName("totalElements")]
        public int TotalElements { get; }
    }
}
