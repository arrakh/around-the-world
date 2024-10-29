using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using Wibci.CountryReverseGeocode.Models;

namespace Wibci.CountryReverseGeocode.Data
{
    public class JsonDataProvider : IReverseGeocodeDataProvider
    {
        List<AreaData> _data = new List<AreaData>();

        public JsonDataProvider(string json)
        {
            _data = JsonConvert.DeserializeObject<List<AreaData>>(json);
        }

        public List<AreaData> Data
        {
            get
            {
                return _data;
            }
        }
    }
}
