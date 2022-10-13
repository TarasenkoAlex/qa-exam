using System.Collections.Generic;
using GrpcService.Contracts;
using GrpcService.Data;
using GrpcService.Models;
using WebApiService.Models;

namespace GrpcService.Repositories
{
    public class RestsAndCitiesRepositoryStub : IRestsAndCitiesRepository
    {
        private readonly TestRecords _testRecords;

        public RestsAndCitiesRepositoryStub(TestRecords testRecords)
        {
            _testRecords = testRecords;
        }

        public List<CityModel> GetCitiesList()
        {
            return _testRecords.GetCitiesList();
        }

        public PaginationResult<List<RestaurantModel>> GetRestaurantListByCity(PaginationRequest request)
        {
            return _testRecords.GetRestaurantListByCity(request);
        }

        public bool AddCity(CityModel cityModel)
        {
            return _testRecords.AddCity(cityModel.Name);
        }

        public bool RemoveCity(ulong cityId)
        {
            return _testRecords.RemoveCity(cityId);
        }

        public bool EditCity(CityModel cityModel)
        {
            return _testRecords.EditCity(cityModel);
        }

        public bool AddRestaurant(RestaurantModel restaurantModel)
        {
            return _testRecords.AddRestaurant(restaurantModel);
        }

        public bool RemoveRestaurant(ulong restaurantId)
        {
            return _testRecords.RemoveRestaurant(restaurantId);
        }

        public bool EditRestaurant(RestaurantModel restaurantModel)
        {
            return _testRecords.EditRestaurant(restaurantModel);
        }
    }
}
