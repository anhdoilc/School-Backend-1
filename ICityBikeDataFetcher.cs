using System.Threading.Tasks;

namespace School_Backend1
{
    public interface ICityBikeDataFetcher
    {
         Task<int> GetBikeCountInStation(string stationName);
    }
}