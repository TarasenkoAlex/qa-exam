using System.Collections.Generic;
using WebApiService.Models;

namespace WebApiService.Contracts
{
    public interface IRestsAndCitiesService
    {
        /// <summary> Получение списка городов </summary>
        /// <returns></returns>
        List<CityModel> GetCitiesList();

        /// <summary> Получение списка ресторанов по определённому городу </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        PaginationResult<List<RestaurantModel>> GetRestaurantListByCity(PaginationRequestModel requestModel);

        /// <summary> Добавление города </summary>
        /// <param name="cityName">Наименование города</param>
        bool AddCity(CityModel cityName);

        /// <summary> Удаление города </summary>
        /// <param name="cityId">Идентификатор города</param>
        bool RemoveCity(ulong cityId);

        /// <summary> Изменение города </summary>
        /// <param name="newCityName">Модель с новым именем города</param>
        bool EditCity(CityModel newCityName);

        /// <summary> Добавление ресторана </summary>
        /// <param name="restaurantModel">Модель ресторана для создания</param>
        /// <returns></returns>
        bool AddRestaurant(RestaurantModel restaurantModel);

        /// <summary> Удаление ресторана </summary>
        /// <param name="restaurantId">Идентификатор ресторана</param>
        /// <returns></returns>
        bool RemoveRestaurant(ulong restaurantId);

        /// <summary> Изменение ресторана </summary>
        /// <param name="model">Модель ресторана</param>
        bool EditRestaurant(RestaurantModel model);
    }
}
