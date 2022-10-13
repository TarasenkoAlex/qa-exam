using System;
using System.Collections.Generic;
using System.Linq;
using GrpcService.Models;
using WebApiService.Models;

namespace GrpcService.Data
{
    public class TestRecords
    {
        private readonly Dictionary<ulong, string> _cities = new();

        private readonly object _locker = new();
        private readonly Dictionary<ulong, string> _restaurantsNameByRestaurantId = new();
        private readonly Dictionary<ulong, HashSet<ulong>> _restaurantsByCityId = new();

        private ulong _currentCityId;
        private ulong _currentRestaurantId;

        public TestRecords()
        {
            GenerateCities();
            GenerateRestaurants();
        }

        #region public region

        public List<CityModel> GetCitiesList()
        {
            lock (_locker)
            {
                return _cities.Select(kv => new CityModel
                {
                    Id = kv.Key,
                    Name = kv.Value
                }).ToList();
            }
        }

        public PaginationResult<List<RestaurantModel>> GetRestaurantListByCity(PaginationRequest request)
        {
            lock (_locker)
            {
                if (_restaurantsByCityId.TryGetValue(request.CityId, out var restaurants))
                {
                    var data = restaurants.Select(restaurantId => new RestaurantModel
                    {
                        Id = restaurantId,
                        CityId = request.CityId,
                        Name = _restaurantsNameByRestaurantId[restaurantId]
                    }).ToList();

                    if (!string.IsNullOrEmpty(request.Filter))
                    {
                        data = data.Where(x => x.Name.Contains(request.Filter)).ToList();
                    }

                    var totalElements = data.Count;

                    data = data.Order(request.SortBy, request.Descending).ToList();
                    
                    var skipElements = request.ElementsPerPage * (request.PageNumber - 1);
                    
                    data = data
                        .Skip(skipElements > 0 ? skipElements : 0)
                        .Take(request.ElementsPerPage)
                        .ToList();

                    return new PaginationResult<List<RestaurantModel>>
                    (
                        data: data,
                        pageNumber: request.PageNumber,
                        elementsPerPage: request.ElementsPerPage,
                        totalElements: totalElements
                    );
                }

                return new PaginationResult<List<RestaurantModel>>
                (
                    data: new List<RestaurantModel>(),
                    pageNumber: -1,
                    elementsPerPage: -1,
                    totalElements: -1
                );
            }
        }

        public bool AddCity(string cityName)
        {
            lock (_locker)
            {
                if (_cities.All(x => x.Value != cityName))
                {
                    var currentCityId = GetNextCityId();
                    _cities[currentCityId] = cityName;
                    GenerateRestaurants(currentCityId, cityName);

                    return true;
                }

                return false;
            }
        }

        public bool RemoveCity(ulong cityId)
        {
            lock (_locker)
            {
                if (_cities.Remove(cityId) == false) return false;
                if (_restaurantsByCityId.Remove(cityId) == false) return false;
                
                return true;
            }
        }

        public bool EditCity(CityModel cityModel)
        {
            if (_cities.ContainsKey(cityModel.Id) == false)
            {
                return false;
            }

            _cities[cityModel.Id] = cityModel.Name;
            return true;
        }

        public bool AddRestaurant(RestaurantModel restaurantModel)
        {
            lock (_locker)
            {
                if (_cities.ContainsKey(restaurantModel.CityId) == false)
                {
                    return false;
                }
                
                if (_restaurantsNameByRestaurantId.All(x => x.Value != restaurantModel.Name))
                {
                    var currentRestaurantId = GetNextRestaurantId();

                    _restaurantsNameByRestaurantId[currentRestaurantId] = restaurantModel.Name;
                    _restaurantsByCityId[restaurantModel.CityId].Add(currentRestaurantId);

                    return true;
                }

                return true;
            }
        }

        public bool RemoveRestaurant(ulong restaurantId)
        {
            lock (_locker)
            {
                if (_restaurantsNameByRestaurantId.Remove(restaurantId) == false)
                {
                    return false;
                }

                foreach (var (cityId, restaurants) in _restaurantsByCityId)
                {
                    if (restaurants.Remove(restaurantId))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public bool EditRestaurant(RestaurantModel restaurantModel)
        {
            if (_restaurantsNameByRestaurantId.ContainsKey(restaurantModel.Id) == false)
            {
                return false;
            }

            _restaurantsNameByRestaurantId[restaurantModel.Id] = restaurantModel.Name;
            return true;
        }

        #endregion

        #region private region

        private void GenerateCities()
        {
            _cities[GetNextCityId()] = "Москва";
            _cities[GetNextCityId()] = "Cанкт-Петербург";
            _cities[GetNextCityId()] = "Нижний Новгород";
            _cities[GetNextCityId()] = "Новосибирск";
            _cities[GetNextCityId()] = "Екатеринбург";
        }

        private void GenerateRestaurants()
        {
            foreach (var (cityId, cityName) in _cities) GenerateRestaurants(cityId, cityName);
        }

        private void GenerateRestaurants(ulong cityId, string cityName)
        {
            _restaurantsByCityId[cityId] = GenerateRestaurantsByCityName(cityName).Keys.ToHashSet();
        }

        private readonly Dictionary<string, HashSet<int>> _usedNumbersByCityName = new();

        private Dictionary<ulong, string> GenerateRestaurantsByCityName(string cityName)
        {
            if (!_usedNumbersByCityName.ContainsKey(cityName))
            {
                _usedNumbersByCityName[cityName] = new HashSet<int>();
            }

            var result = new Dictionary<ulong, string>();

            var rand = new Random();
            for (var i = 1; i <= 100; i++)
            {
                var number = rand.Next(1000, 9999);
                while (_usedNumbersByCityName[cityName].Contains(number))
                {
                    number = rand.Next(1000, 9999);
                }

                _usedNumbersByCityName[cityName].Add(number);

                var currentRestaurantId = GetNextRestaurantId();
                result[currentRestaurantId] = $"{cityName}_ресторан_{number}";
                _restaurantsNameByRestaurantId[currentRestaurantId] = $"{cityName}_ресторан_{number}";
            }

            return result;
        }

        private ulong GetNextCityId()
        {
            return ++_currentCityId;
        }

        private ulong GetNextRestaurantId()
        {
            return ++_currentRestaurantId;
        }

        #endregion
    }
}
