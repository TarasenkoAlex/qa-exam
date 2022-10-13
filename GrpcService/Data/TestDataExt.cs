using System;
using System.Collections.Generic;
using System.Linq;
using GrpcService.Models;

namespace GrpcService.Data
{
    public static class TestDataExt
    {
        public static IEnumerable<RestaurantModel> Order(this IEnumerable<RestaurantModel> data, string sortBy, bool descending)
        {
            if (descending)
            {
                if (string.Equals(sortBy, nameof(RestaurantModel.Name), StringComparison.CurrentCultureIgnoreCase))
                {
                    return data.OrderByDescending(x => x.Name);
                }
                
                if (string.Equals(sortBy, nameof(RestaurantModel.Id), StringComparison.CurrentCultureIgnoreCase))
                {
                    return data.OrderByDescending(x => x.Id);
                }
            }
            else
            {
                if (string.Equals(sortBy, nameof(RestaurantModel.Name), StringComparison.CurrentCultureIgnoreCase))
                {
                    return data.OrderBy(x => x.Name);
                }
                
                if (string.Equals(sortBy, nameof(RestaurantModel.Id), StringComparison.CurrentCultureIgnoreCase))
                {
                    return data.OrderBy(x => x.Id);
                }
            }

            return data;
        }
    }
}
