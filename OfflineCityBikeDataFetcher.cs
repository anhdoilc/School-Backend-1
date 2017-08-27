using System;
using System.IO;
using System.Threading.Tasks;

namespace School_Backend1
{
    public class OfflineCityBikeDataFetcher : ICityBikeDataFetcher
    {
        public async Task<int> GetBikeCountInStation(string stationName)
        {
            const string path = @"bikes.txt";

            using (StreamReader sreader = new StreamReader(path))
            {
                //read a line from the file
                string line = await sreader.ReadLineAsync();

                while (line != null)
                {
                    //Split the lines at ':'...
                    string[] components = line.Split(':');

                    //...which hopefully results in a string array with 2 elements: station's name and availableBikes
                    if (components.Length == 2)
                    {
                        //Station's name is the first string element in the array
                        string name = components[0];
                        
                        int bikesAvailable;

                        //Try to convert the second string element into an integer and if it works,
                        //place the integer into bikesAvailable.
                        bool secondIsANumber = int.TryParse(components[1], out bikesAvailable);

                        //Check that the line is valid: there's a name and it has a number that is 0 or greater
                        if (name.Length > 0 && secondIsANumber && bikesAvailable >=0)
                        {
                            //Compares input with line in file, accepts lowercase
                            if (String.Compare(name, stationName, true) == 0)
                            {
                                //Console.WriteLine($"Input '{stationName}' => {name}, {bikesAvailable}");
                                return bikesAvailable;
                            }
                            else
                            {
                                //Query didn't match line in file
                            }
                        }
                        else
                        {
                            //The line in the file wasn't valid
                            Console.WriteLine($"String '{line}' has invalid content: name or available bikes not found");
                        }
                    }
                    else
                    {
                        //There are more or less components in the array
                        Console.WriteLine($"String '{line}' has invalid content: line has too many or too few elements");
                    }

                    //Attempt to load the next line
                    line = await sreader.ReadLineAsync();
                }
            }

            throw new NotFoundException("Station not found");
        }
    }
}