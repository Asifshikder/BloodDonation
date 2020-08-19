using BD.DATA.Entity;
using BD.REPO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DB.SERVICE.Implementation
{
    public class CityService : ICityService
    {
        private readonly IRepository<City> CityRepository;
        public CityService(IRepository<City> CityRepository)
        {
            this.CityRepository = CityRepository;
        }

        public void DeleteCity(int id)
        {
            City City = CityRepository.Get(id);
            CityRepository.Remove(City);

        }

        public City GetCity(int id)
        {
            return CityRepository.Get(id);
        }
        public IEnumerable<City> GetCitys()
        {
            return CityRepository.GetAll();
        }

        public void InsertCity(City City)
        {
            CityRepository.Insert(City);
        }

        public void UpdateCity(City City)
        {
            CityRepository.Update(City);
        }
    }
}
