using CoreWCF;
using FlightProvider;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace FlightProvider
{
    [DataContract]
    public class AirportSearchRequest
    {
        [DataMember]
        public string Condition { get; set; }

    }

    [DataContract]
    public class AirportSearchResult
    {
        [DataMember]
        public bool HasError { get; set; }
        [DataMember]
        public List<Airport> Airports { get; set; } = new List<Airport>();

    }

    public class Airport
    {
        [DataMember]
        public string Name { get; set; }

    }

    [ServiceContract]
    public interface IAirportSearch
    {
        [OperationContract]
        AirportSearchResult Search(AirportSearchRequest request);
    }


    public class AirportSearch : IAirportSearch
    {
        private readonly List<Airport> result = new List<Airport>
                {
                    new Airport {
                       Name = "Eskişehir Havalimanı"
                    },
                    new Airport {
                       Name = "Ankara Havalimanı"
                    },
                    new Airport
                    {
                        Name = "Erzurum Havalimanı"
                    },
                };
        public AirportSearchResult Search(AirportSearchRequest request)
        {
            return new AirportSearchResult
            {
                HasError = false,
                Airports = result.Where(q => q.Name.Contains(request.Condition, StringComparison.OrdinalIgnoreCase)).ToList(),
            };
        }
    }
}
