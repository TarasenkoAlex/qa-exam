using System;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcService.Contracts;
using GrpcService.Models;
using Protos.RestsAndCities;
using CityProtoModel = Protos.RestsAndCities.CityModel;
using RestaurantProtoModel = Protos.RestsAndCities.RestaurantModel;
using GetRestaurantListByCityRequest = Protos.RestsAndCities.GetRestaurantListByCityRequest;
using GetRestaurantListByCityResponse = Protos.RestsAndCities.GetRestaurantListByCityResponse;
using CityModel = GrpcService.Models.CityModel;
using RestaurantModel = GrpcService.Models.RestaurantModel;

namespace GrpcService.Services
{
    public class RestsAndCitiesService : Protos.RestsAndCities.RestsAndCitiesService.RestsAndCitiesServiceBase
    {
        private readonly IRestsAndCitiesRepository _restsAndCitiesRepository;

        public RestsAndCitiesService(IRestsAndCitiesRepository restsAndCitiesRepository)
        {
            _restsAndCitiesRepository = restsAndCitiesRepository;
        }

        /// <summary> Получение списка городов </summary>
        /// <returns></returns>
        public override Task<GetCitiesListResponse> GetCitiesList(Empty request, ServerCallContext context)
        {
            return Task.FromResult(new GetCitiesListResponse
            {
                Models = { _restsAndCitiesRepository.GetCitiesList().Select(x => new CityProtoModel { Id = x.Id, Name = x.Name }) }
            });
        }

        
        /// <summary> Получение списка ресторанов по определённому городу </summary>
        /// <returns></returns>
        public override Task<GetRestaurantListByCityResponse> GetRestaurantListByCity(GetRestaurantListByCityRequest request, ServerCallContext context)
        {
            if (request.IsCenterLocation) throw new NotImplementedException();
            
            var requestModel = new PaginationRequest
            {
                CityId = request.CityId,
                SortBy = request.SortBy,
                Descending = request.Descending,
                PageNumber = request.PageNumber,
                ElementsPerPage = request.ElementsPerPage,
                Filter = request.Filter
            };
            var paginationResponse = _restsAndCitiesRepository.GetRestaurantListByCity(requestModel);

            var response = new GetRestaurantListByCityResponse
            {
                PageNumber = paginationResponse.PageNumber,
                ElementsPerPage = paginationResponse.ElementsPerPage,
                TotalElements = paginationResponse.TotalElements,
                RestaurantModels = { paginationResponse.Data.Select(x => new RestaurantProtoModel { Id = x.Id, CityId = x.CityId, Name = x.Name }) }
            };
            
            return Task.FromResult(response);
        }

        /// <summary> Добавление города </summary>
        public override Task<AddCityResponse> AddCity(AddCityRequest request, ServerCallContext context)
        {
            return Task.FromResult(new AddCityResponse
            {
                Result = _restsAndCitiesRepository.AddCity(new CityModel { Name = request.CityName })
            });
        }

        /// <summary> Удаление города </summary>
        public override Task<RemoveCityResponse> RemoveCity(RemoveCityRequest request, ServerCallContext context)
        {
            return Task.FromResult(new RemoveCityResponse
            {
                Result = _restsAndCitiesRepository.RemoveCity(request.CityId)
            });
        }

        /// <summary> Изменение города </summary>
        public override Task<EditCityResponse> EditCity(EditCityRequest request, ServerCallContext context)
        {
            return Task.FromResult(new EditCityResponse
            {
                Result = _restsAndCitiesRepository.EditCity(new CityModel { Id = request.CityId, Name = request.NewCityName })
            });
        }
        
        /// <summary> Добавление ресторана </summary>
        public override Task<AddRestaurantResponse> AddRestaurant(AddRestaurantRequest request, ServerCallContext context)
        {
            return Task.FromResult(new AddRestaurantResponse
            {
                Result = _restsAndCitiesRepository.AddRestaurant(new RestaurantModel { CityId = request.CityId, Name = request.RestaurantName })
            });
        }

        /// <summary> Удаление ресторана </summary>
        public override Task<RemoveRestaurantResponse> RemoveRestaurant(RemoveRestaurantRequest request, ServerCallContext context)
        {
            return Task.FromResult(new RemoveRestaurantResponse { Result = _restsAndCitiesRepository.RemoveRestaurant(request.RestaurantId) });
        }
        
        /// <summary> Изменение ресторана </summary>
        public override Task<EditRestaurantResponse> EditRestaurant(EditRestaurantRequest request, ServerCallContext context)
        {
            return Task.FromResult(new EditRestaurantResponse
            {
                Result = _restsAndCitiesRepository.EditRestaurant(new RestaurantModel { Id = request.RestaurantId, Name = request.NewRestaurantName })
            });
        }
    }
}
