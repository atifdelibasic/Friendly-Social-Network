using System;

namespace Friendly.Model.Requests.City
{
    public class CreateCityRequest
    {
        public string Name { get; set; }
        public int CountryId { get; set; }
    }
}
