using Microsoft.Extensions.Configuration;
using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;
using RandomMarkers.Domain;


internal class Program
{
    private static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
                            .AddJsonFile($"appsettings.json");

        var config = configuration.Build();

        int countOfOperation = 10;
        int.TryParse(config.GetSection("CountOfPOIPerMinutes").Value, out countOfOperation);


        Console.WriteLine("====== NetMQ POI Publisher ======");
        Console.Title = "NetMQ POI Publisher";

        bool stopRequested = false;

        // Wire up the CTRL+C handler
        Console.CancelKeyPress += (sender, e) => stopRequested = true;

        Console.WriteLine("Publishing POI updates...");

        using (var publisher = new PublisherSocket())
        {
            publisher.Bind("tcp://127.0.0.1:5556");

            var rng = new Random();

            while (!stopRequested)
            {
                for (int i = 1; i <= countOfOperation; i++)
                {
                    var randomGeoLocation = RandomGeoLocation.Create(config.GetSection("SouthWest").Value, config.GetSection("NorthEast").Value);
                    if (randomGeoLocation.Failure)
                    {
                        Console.WriteLine(randomGeoLocation.Error);
                        continue;
                    }
                    var json = JsonConvert.SerializeObject(randomGeoLocation.Value);
                    publisher.SendMoreFrame("status").SendFrame(json);
                    
                    DateTime start = DateTime.UtcNow;

                    Console.WriteLine(json);
                    int left = (int)(start.AddSeconds(60 / countOfOperation + 1) - DateTime.UtcNow).TotalMilliseconds;
                    if (left > 0)
                    {
                        Thread.Sleep(left);
                    }
                }
            }
        }
    }
}