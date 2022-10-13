namespace GrpcService.Models
{
    public class RestaurantModel
    {
        /// <summary> Идентификатор </summary>
        public ulong Id { get; init; }

        /// <summary> Идентификатор города </summary>
        public ulong CityId { get; init; }
        
        /// <summary> Наименование ресторана </summary>
        public string Name { get; init; }
    }
}
