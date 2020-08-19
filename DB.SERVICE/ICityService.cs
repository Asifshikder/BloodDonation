using BD.DATA.Entity;
using System;
using System.Collections.Generic;

namespace DB.SERVICE
{
    public interface ICityService
    {
        IEnumerable<City> GetCitys();
        City GetCity(int id);
        void InsertCity(City City);
        void UpdateCity(City City);
        void DeleteCity(int id);
    }
}
