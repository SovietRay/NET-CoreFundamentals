using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurants();
        IEnumerable<Restaurant> GetRestaurantsByName(string name);
        Restaurant GetById(int id);
        Restaurant Update(Restaurant updatedRestaurent);
        Restaurant Add(Restaurant newRestaurant);
        int Commit();
    }
    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> restaurants;
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
                {
                    new Restaurant {Id = 1, Name = "Papa Pizza", Location = "Moscow", Cuisine = CuisineType.Italian},
                    new Restaurant {Id = 2, Name = "Maharadja!", Location = "Balashiha", Cuisine = CuisineType.Indian},
                    new Restaurant {Id = 3, Name = "Arriba", Location = "Moscow", Cuisine = CuisineType.Mexican},
                    new Restaurant {Id = 4, Name = "Stop chili", Location = "Butovo", Cuisine = CuisineType.Mexican},
                    new Restaurant {Id = 5, Name = "Gugugug", Location = "Balashiha", Cuisine = CuisineType.None}
                };
        }
        public IEnumerable<Restaurant> GetRestaurants()
        {
            return from r in restaurants
                   orderby r.Name
                   select r;
        }
        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }
        public Restaurant GetById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }
        public Restaurant Update(Restaurant updatedRestaurent)
        {
            var restaurant = restaurants.SingleOrDefault(r => r.Id == updatedRestaurent.Id);
            if (restaurant != null)
            {
                restaurant.Name = updatedRestaurent.Name;
                restaurant.Location = updatedRestaurent.Location;
                restaurant.Cuisine = updatedRestaurent.Cuisine;
            }
            return restaurant;
        }
        public Restaurant Add(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            return newRestaurant;
        }
        public int Commit()
        {
            return 0;
        }
    }
}
