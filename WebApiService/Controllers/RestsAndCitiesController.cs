using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using WebApiService.Contracts;
using WebApiService.Models;

namespace WebApiService.Controllers
{
    [Route("api/rests-and-cities")]
    public class RestsAndCitiesController : Controller
    {
        private readonly IRestsAndCitiesService _restsAndCitiesService;

        public RestsAndCitiesController(IRestsAndCitiesService restsAndCitiesService)
        {
            _restsAndCitiesService = restsAndCitiesService;
        }

        /// <summary> Получение списка городов </summary>
        [HttpGet("get-cities-list")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(List<CityModel>), (int)HttpStatusCode.OK)]
        public IActionResult GetCitiesList()
        {
            return Ok(_restsAndCitiesService.GetCitiesList());
        }

        /// <summary> Получение списка ресторанов по определённому городу </summary>
        /// <param name="request">
        /// <example>
        /// <code>
        /// Пример входных данных<br/>
        /// {<br/>
        ///    "cityId": 1,<br/>
        ///    "elementsPerPage": 3,<br/>
        ///    "pageNumber": 1,<br/>
        ///    "isCenterLocation": false<br/>
        /// }<br/>
        /// </code>
        /// </example>
        /// </param>
        /// <returns>
        /// <example>
        /// Пример выходных данных
        /// <code>
        /// {
        ///   "data": [
        ///     {
        ///       "id": 1,
        ///       "cityId": 1,
        ///       "name": "Москва_ресторан_9426"
        ///     },
        ///     {
        ///       "id": 2,
        ///       "cityId": 1,
        ///       "name": "Москва_ресторан_5052"
        ///     },
        ///     {
        ///       "id": 3,
        ///       "cityId": 1,
        ///       "name": "Москва_ресторан_8041"
        ///     }
        ///   ],
        ///   "pageNumber": 1,
        ///   "pageSize": 3,
        ///   "totalElements": 100
        /// }
        /// </code>
        /// </example>
        /// </returns>
        [HttpPost("restaurants-list")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PaginationResult<List<RestaurantModel>>), (int)HttpStatusCode.OK)]
        public IActionResult GetRestaurantListByCity([FromBody] PaginationRequestModel request)
        {
            return Ok(_restsAndCitiesService.GetRestaurantListByCity(request));
        }

        /// <summary> Добавление города </summary>
        /// <param name="cityModel">
        /// Модель города
        /// <example>
        /// <code>
        /// Пример входных данных<br/>
        /// {<br/>
        ///    "id": -1, // Не требует заполнения. Генерируется автоматически системой<br/>
        ///    "name": "Иваново"<br/>
        /// }<br/>
        /// </code>
        /// </example>
        /// </param>
        [HttpPost("add-city")]
        public IActionResult AddCity([FromBody] CityModel cityModel)
        {
            return _restsAndCitiesService.AddCity(cityModel) ? Ok() : new StatusCodeResult(Constants.HANDLED_SERVER_ERROR_CODE);
        }

        /// <summary> Удаление города </summary>
        /// <param name="cityId">Идентификатор города</param>
        [HttpDelete("remove-city/{cityId}")]
        public IActionResult RemoveCity([FromRoute] ulong cityId)
        {
            return _restsAndCitiesService.RemoveCity(cityId) ? Ok() : new StatusCodeResult(Constants.HANDLED_SERVER_ERROR_CODE);
        }

        /// <summary> Изменение города </summary>
        /// <param name="cityId">Идентификатор города</param>
        /// <param name="cityModel">
        /// Модель города
        /// <example>
        /// <code>
        /// Пример<br/>
        /// {<br/>
        ///    "id": -1, // Не требует заполнения.<br/>
        ///    "name": "Иваново"<br/>
        /// }<br/>
        /// </code>
        /// </example>
        /// </param>
        [HttpPost("edit-city/{cityId}")]
        public IActionResult EditCity([FromRoute] ulong cityId, [FromBody] CityModel cityModel)
        {
            cityModel.Id = cityId;
            var operationResult = _restsAndCitiesService.EditCity(cityModel);

            return operationResult ? Ok() : new StatusCodeResult(Constants.HANDLED_SERVER_ERROR_CODE);
        }

        /// <summary> Добавление ресторана </summary>
        /// <param name="cityId">Идентификатор города</param>
        /// <param name="restaurantModel">
        /// Модель ресторана
        /// <example>
        /// <code>
        /// Пример<br/>
        /// {<br/>
        ///    "id": -1, // Не требует заполнения<br/>
        ///    "name": "Новый ресторан"<br/>
        /// }<br/>
        /// </code>
        /// </example>
        /// </param>
        [HttpPost("{cityId}/add-restaurant")]
        public IActionResult AddRestaurant([FromRoute] ulong cityId, [FromBody] RestaurantModel restaurantModel)
        {
            restaurantModel.CityId = cityId;
            var operationResult = _restsAndCitiesService.AddRestaurant(restaurantModel);

            return operationResult ? Ok() : new StatusCodeResult(Constants.HANDLED_SERVER_ERROR_CODE);
        }

        /// <summary> Удаление ресторана </summary>
        /// /// <param name="restaurantId">Идентификатор ресторана</param>
        [HttpDelete("remove-restaurant/{restaurantId}")]
        public IActionResult RemoveRestaurant([FromRoute] ulong restaurantId)
        {
            return _restsAndCitiesService.RemoveRestaurant(restaurantId) ? Ok() : new StatusCodeResult(Constants.HANDLED_SERVER_ERROR_CODE);
        }

        /// <summary> Изменение ресторана </summary>
        /// <param name="restaurantId">Идентификатор ресторана</param>
        /// <param name="restaurantModel">
        /// Модель ресторана
        /// <example>
        /// <code>
        /// Пример<br/>
        /// {<br/>
        ///    "id": -1, // Не требует заполнения<br/>
        ///    "name": "Новый ресторан"<br/>
        /// }
        /// </code>
        /// </example>
        /// </param>
        [HttpPost("edit-restaurant/{restaurantId}")]
        public IActionResult EditRestaurant([FromRoute] ulong restaurantId, [FromBody] RestaurantModel restaurantModel)
        {
            restaurantModel.Id = restaurantId;
            var operationResult = _restsAndCitiesService.EditRestaurant(restaurantModel);

            return operationResult ? Ok() : new StatusCodeResult(Constants.HANDLED_SERVER_ERROR_CODE);
        }
    }
}
