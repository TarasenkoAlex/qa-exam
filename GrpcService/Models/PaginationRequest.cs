namespace GrpcService.Models
{
    public class PaginationRequest
    {
        /// <summary> Идентификатор города </summary>
        public ulong CityId { get; init; }

        /// <summary> Атрибут сортировки </summary>
        public string SortBy { get; init; }

        /// <summary> Сортировать ли по убыванию </summary>
        public bool Descending { get; init; }

        /// <summary> Номер страницы </summary>
        public int PageNumber { get; init; }

        /// <summary> Количество элементов на странице </summary>
        public int ElementsPerPage { get; init; }

        /// <summary> Фильтр для поиска </summary>
        public string Filter { get; init; }
    }
}
