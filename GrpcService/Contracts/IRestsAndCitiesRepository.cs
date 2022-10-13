using System.Collections.Generic;
using GrpcService.Models;
using WebApiService.Models;

namespace GrpcService.Contracts
{
    public interface IRestsAndCitiesRepository
    {
        /// <summary> Получение списка городов </summary>
        /// <returns>Список ресторанов</returns>
        List<CityModel> GetCitiesList();

        /// <summary> Получение списка ресторанов по определённому городу </summary>
        /// <param name="request"></param>
        /// <returns>Список ресторанов</returns>
        PaginationResult<List<RestaurantModel>> GetRestaurantListByCity(PaginationRequest request);

        /// <summary> Добавление города </summary>
        /// <param name="cityModel"></param>
        /// <returns></returns>
        bool AddCity(CityModel cityModel);

        /// <summary> Удаление города </summary>
        /// <param name="cityId">Идентификатор города</param>
        /// <returns></returns>
        bool RemoveCity(ulong cityId);

        /// <summary> Изменение города </summary>
        /// <param name="newCityName">Модель с новым наименованием города</param>
        /// <returns></returns>
        bool EditCity(CityModel newCityName);

        /// <summary> Добавление ресторана </summary>
        /// <param name="model">Модель ресторана для создания</param>
        /// <returns></returns>
        bool AddRestaurant(RestaurantModel model);

        /// <summary> Удаление ресторана </summary>
        /// <param name="restaurantId">Идентификатор ресторана</param>
        /// <returns></returns>
        bool RemoveRestaurant(ulong restaurantId);

        /// <summary> Изменение ресторана </summary>
        /// <param name="restaurantModel">Модель ресторана</param>
        /// <returns></returns>
        bool EditRestaurant(RestaurantModel restaurantModel);
    }
}
