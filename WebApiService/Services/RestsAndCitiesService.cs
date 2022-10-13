using System;
using System.Collections.Generic;
using System.Linq;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Protos.RestsAndCities;
using WebApiService.Contracts;
using WebApiService.Models;
using CityModel = WebApiService.Models.CityModel;
using RestaurantModel = WebApiService.Models.RestaurantModel;

namespace WebApiService.Services
{
    public class RestsAndCitiesService : IRestsAndCitiesService
    {
        private readonly Protos.RestsAndCities.RestsAndCitiesService.RestsAndCitiesServiceClient _client;
        
        public RestsAndCitiesService(Protos.RestsAndCities.RestsAndCitiesService.RestsAndCitiesServiceClient client)
        {
            _client = client;
        }
        
        public List<CityModel> GetCitiesList()
        {
            return _client.GetCitiesList(new Empty()).Models.Select(x => new CityModel { Id = x.Id, Name = x.Name }).ToList();
        }

        public PaginationResult<List<RestaurantModel>> GetRestaurantListByCity(PaginationRequestModel requestModel)
        {
            var response = _client.GetRestaurantListByCity(new GetRestaurantListByCityRequest
            {
                CityId = requestModel.CityId,
                PageNumber = requestModel.PageNumber,
                ElementsPerPage = requestModel.ElementsPerPage,
                SortBy = requestModel.SortBy,
                Descending = requestModel.Descending,
                Filter = requestModel.Filter,
                IsCenterLocation = requestModel.IsCenterLocation
            });

            return new PaginationResult<List<RestaurantModel>>
            (
                data:response.RestaurantModels.Select(x => new RestaurantModel { Id = x.Id, Name = x.Name, CityId = x.CityId }).ToList(),
                pageNumber: requestModel.PageNumber,
                elementsPerPage: requestModel.ElementsPerPage,
                totalElements: response.TotalElements
            );
        }

        public bool AddCity(CityModel cityName)
        {
            var response = _client.AddCity(new AddCityRequest { CityName = cityName.Name });
            return response.Result;
        }

        public bool RemoveCity(ulong cityId)
        {
            var response = _client.RemoveCity(new RemoveCityRequest { CityId = cityId });
            return response.Result;
        }

        public bool EditCity(CityModel newCityName)
        {
            var response = _client.EditCity(new EditCityRequest { CityId = newCityName.Id, NewCityName = newCityName.Name });
            return response.Result;
        }

        public bool AddRestaurant(RestaurantModel restaurantModel)
        {
            var response = _client.AddRestaurant(new AddRestaurantRequest { CityId = restaurantModel.CityId, RestaurantName = restaurantModel.Name });
            return response.Result;
        }

        public bool RemoveRestaurant(ulong restaurantId)
        {
            var response = _client.RemoveRestaurant(new RemoveRestaurantRequest { RestaurantId = restaurantId });
            return response.Result;
        }

        public bool EditRestaurant(RestaurantModel model)
        {
            var response = _client.EditRestaurant(new EditRestaurantRequest { RestaurantId = model.Id, NewRestaurantName = model.Name });
            return response.Result;
        }
    }
}
