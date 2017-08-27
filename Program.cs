using System;
using System.Linq;
using System.Threading.Tasks;

namespace School_Backend1
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0) 
            {
                throw new System.ArgumentException("No command line parameters");            
            }
                        
            else if (args[0].Any(char.IsDigit)) 
            {
                throw new System.ArgumentException("Command line parameter should not have a number in it");
            }

            try
            {
                ICityBikeDataFetcher fetcher = null;

                if (args.Length == 2 && args[1] == "realtime")
                {
                    fetcher = new RealTimeCityBikeDataFetcher();
                    Console.WriteLine("Realtime fetcher used");
                }
                else if (args.Length == 2 && args[1] == "offline")
                {
                    fetcher = new OfflineCityBikeDataFetcher();
                    Console.WriteLine("Offline fetcher used");
                }
                else
                {
                    fetcher = new RealTimeCityBikeDataFetcher();
                    Console.WriteLine("No fetcher specified, used Realtime fetcher");
                }

                Task<int> fetchingOperation = fetcher.GetBikeCountInStation(args[0]);

                //automatically calls Wait()
                int tulos = fetchingOperation.Result;

                Console.WriteLine(tulos + " bikes available");  
            }
            catch (Exception ex)
            //Apparently this would work if the function wasn't asynchronous:
            //catch (NotFoundException ex)
            {
                Console.WriteLine("Not found: " + ex.Message);
            }
        }
    }
}
