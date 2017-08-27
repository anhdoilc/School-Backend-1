using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace School_Backend1

{    public class RealTimeCityBikeDataFetcher : ICityBikeDataFetcher
    {
        public async Task<int> GetBikeCountInStation(string stationName)
        {
            HttpClient client = new HttpClient();
            Task<string> stringRetreiverTask = client.GetStringAsync("https://api.digitransit.fi/routing/v1/routers/hsl/bike_rental");
            string response = await stringRetreiverTask;

            Stationss stations = JsonConvert.DeserializeObject<School_Backend1.Stationss>(response);

            foreach (Station station in stations.stations)
            {
                //Compares input with fetched line, accepts lowercase
                if (String.Compare(station.name, stationName, true) == 0)
                {
                    //Console.WriteLine($"Name={station.name}, bikesAvailable={station.bikesAvailable}");
                    return station.bikesAvailable;
                }
            }

            throw new NotFoundException("Station not found");
        }
    }
}