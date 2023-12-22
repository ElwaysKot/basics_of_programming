using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Json file name: ");
        string filePath = Console.ReadLine();

        FlightInformationSystem flightSystem = new FlightInformationSystem();
        flightSystem.LoadData(filePath);

        while (true)
        {
            Console.WriteLine("Choose one of the next options:");
            Console.WriteLine("1.Return all flights operated by a particular airline");
            Console.WriteLine("2.Return all flights that are currently delayed");
            Console.WriteLine("3.Return all flights that depart on a particular day");
            Console.WriteLine("4.Return all flights that depart and arrive in the specified time period");
            Console.WriteLine("5.Return all flights that arrived in the last hour or in the specified time period");
            Console.WriteLine("6.Exit the program");

            int option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    var airlines = flightSystem.GetAirlines();
                    Console.WriteLine("Available airlines:");
                    foreach (var airline1 in airlines)
                    {
                        Console.WriteLine(airline1);
                    }
                    Console.Write("Enter the name of the airline: ");
                    string airline = Console.ReadLine();
                    var flightsByAirline = flightSystem.GetFlightsByAirline(airline);
                    if (flightsByAirline.Count == 0)
                    {
                        Console.WriteLine("No flights found for this airline.");
                    }
                    else
                    {
                        Console.WriteLine("Flights by " + airline + ":");
                        foreach (var flight in flightsByAirline)
                        {
                            Console.WriteLine(flight.FlightNumber + " Departure Time: " + flight.DepartureTime);
                        }
                    }
                    break;
                case 2:
                    var delayedFlights = flightSystem.GetDelayedFlights();
                    if (delayedFlights.Count == 0)
                    {
                        Console.WriteLine("No delayed flights found.");
                    }
                    else
                    {
                        Console.WriteLine("Delayed flights:");
                        foreach (var flight in delayedFlights)
                        {
                            Console.WriteLine(flight.FlightNumber + " Delayed Time: " + flight.DepartureTime);
                        }
                    }
                    break;
                case 3:
                    Console.Write("Enter the date (format: year-month-day): ");
                    DateTime date = DateTime.Parse(Console.ReadLine());
                    var flightsOnDate = flightSystem.GetFlightsOnDate(date);
                    if (flightsOnDate.Count == 0)
                    {
                        Console.WriteLine("No flights found on this date.");
                    }
                    else
                    {
                        Console.WriteLine("Flights on " + date.ToString("yyyy-MM-dd") + ":");
                        foreach (var flight in flightsOnDate)
                        {
                            Console.WriteLine(flight.FlightNumber + " Departure Time: " + flight.DepartureTime);
                        }
                    }
                    break;
                case 4:
                    Console.Write("Enter the starting time (format: year-month-day-hours:minutes:seconds): ");
                    DateTime startTime = DateTime.Parse(Console.ReadLine());
                    Console.Write("Enter the end time (format: year-month-day-hours:minutes:seconds): ");
                    DateTime endTime = DateTime.Parse(Console.ReadLine());
                    var flightsInTimeRange = flightSystem.GetFlightsInTimeRange(startTime, endTime);
                    if (flightsInTimeRange.Count == 0)
                    {
                        Console.WriteLine("No flights found in this time range.");
                    }
                    else
                    {
                        Console.WriteLine("Flights from " + startTime.ToString("yyyy-MM-ddTHH:mm:ss") + " to " + endTime.ToString("yyyy-MM-ddTHH:mm:ss") + ":");
                        foreach (var flight in flightsInTimeRange)
                        {
                            Console.WriteLine(flight.FlightNumber + " Departure Time: " + flight.DepartureTime);
                        }
                    }
                    break;
                case 5:
                    Console.Write("Return all flights that arrived in the last hour or in the specified time period (1/2)? ");
                    string useLastHour = Console.ReadLine();
                    List<Flight> flightsInLastHourOrTimeRange;
                    if (useLastHour.ToLower() == "1")
                    {
                        DateTime currentTime = DateTime.Now;
                        DateTime oneHourAgo = currentTime.AddHours(-1);
                        flightsInLastHourOrTimeRange = flightSystem.GetFlightsInTimeRange(oneHourAgo, currentTime);
                        if (flightsInLastHourOrTimeRange.Count == 0)
                        {
                            Console.WriteLine("No flights arrived in the last hour.");
                        }
                        else
                        {
                            Console.WriteLine("Flights that arrived in the last hour:");
                            foreach (var flight in flightsInLastHourOrTimeRange)
                            {
                                Console.WriteLine(flight.FlightNumber + " Arrival Time: " + flight.ArrivalTime);
                            }
                        }
                    }
                    else
                    {
                        Console.Write("Enter the start time (format: year-month-day-hours:minutes:seconds): ");
                        DateTime startRange = DateTime.Parse(Console.ReadLine());
                        Console.Write("Enter the end time (format: year-month-day-hours:minutes:seconds): ");
                        DateTime endRange = DateTime.Parse(Console.ReadLine());
                        flightsInLastHourOrTimeRange = flightSystem.GetFlightsInTimeRange(startRange, endRange);
                        if (flightsInLastHourOrTimeRange.Count == 0)
                        {
                            Console.WriteLine("No flights found in this time range.");
                        }
                        else
                        {
                            Console.WriteLine("Flights from " + startRange.ToString("yyyy-MM-ddTHH:mm:ss") + " to " + endRange.ToString("yyyy-MM-ddTHH:mm:ss") + ":");
                            foreach (var flight in flightsInLastHourOrTimeRange)
                            {
                                Console.WriteLine(flight.FlightNumber + " Arrival Time: " + flight.ArrivalTime);
                            }
                        }
                    }
                    break;

                case 6:
                    return;
                default:
                    Console.WriteLine("Unknown option. Please try again.");
                    break;
            }

        }
    }
    public enum FlightStatus
    {
        OnTime,
        Delayed,
        Cancelled,
        Boarding,
        InFlight
    }
    public class Flight
    {
        public string? FlightNumber { get; set; }
        public string? Airline { get; set; }
        public string? Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public FlightStatus Status { get; set; }
        public TimeSpan Duration { get; set; }
        public string? AircraftType { get; set; }
        public string? Terminal { get; set; }
    }
    public class FlightData
    {
        public List<Flight> Flights { get; set; }
    }
    public class FlightInformationSystem
    {
        private List<Flight> flights;
        public FlightInformationSystem()
        {
            flights = new List<Flight>();
        }
        public void AddFlight(Flight flight)
        {
            flights.Add(flight);
        }
        public void RemoveFlight(Flight flight)
        {
            flights.Remove(flight);
        }
        public Flight SearchFlight(string flightNumber)
        {
            return flights.Find(flight => flight.FlightNumber == flightNumber);
        }
        public void LoadData(string filePath)
        {
            try
            {
                string jsonData = File.ReadAllText(filePath);
                FlightData flightData = JsonConvert.DeserializeObject<FlightData>(jsonData);
                flights = flightData.Flights;
            }
            catch (JsonReaderException ex)
            {
                Console.WriteLine("Error while reading: " + ex.Message);
            }
            catch (JsonSerializationException ex)
            {
                Console.WriteLine("Error while deserialization: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unknown error: " + ex.Message);
            }
        }
        public void ProcessRequest(Func<Flight, bool> query)
        {
            var result = flights.Where(query).ToList();
            Console.WriteLine($"Number of flights matching the query: {result.Count}");
            string jsonResult = JsonConvert.SerializeObject(result);
            File.WriteAllText("queryResult.json", jsonResult);
            Console.WriteLine("The result has been written to 'queryResult.json'");
        }
        public void UpdateFlightStatus()
        {
            DateTime currentTime = DateTime.Now;
            foreach (var flight in flights)
            {
                if (flight.DepartureTime > currentTime)
                {
                    flight.Status = FlightStatus.OnTime;
                }
                else if (flight.DepartureTime <= currentTime && flight.ArrivalTime > currentTime)
                {
                    flight.Status = FlightStatus.InFlight;
                }
                else
                {
                    flight.Status = FlightStatus.Delayed;
                }
            }
        }
        public List<Flight> GetFlightsByAirline(string airline)
        {
            var result = flights.Where(flight => flight.Airline == airline)
                                .OrderBy(flight => flight.DepartureTime)
                                .ToList();
            return result;
        }
        public List<Flight> GetDelayedFlights()
        {
            var result = flights.Where(flight => flight.Status == FlightStatus.Delayed)
                                .OrderBy(flight => flight.DepartureTime)
                                .ToList();
            return result;
        }
        public List<Flight> GetFlightsOnDate(DateTime date)
        {
            var result = flights.Where(flight => flight.DepartureTime.Date == date.Date)
                                .OrderBy(flight => flight.DepartureTime)
                                .ToList();
            return result;
        }
        public List<Flight> GetFlightsInTimeRange(DateTime startTime, DateTime endTime)
        {
            var result = flights.Where(flight => flight.DepartureTime >= startTime && flight.ArrivalTime <= endTime)
                                .OrderBy(flight => flight.DepartureTime)
                                .ToList();
            return result;
        }
        public List<Flight> GetFlightsInLastHour()
        {
            DateTime oneHourAgo = DateTime.Now.AddHours(-1);
            var result = flights.Where(flight => flight.ArrivalTime >= oneHourAgo)
                                .OrderBy(flight => flight.ArrivalTime)
                                .ToList();
            return result;
        }
        public List<string> GetAirlines()
        {
            return flights.Select(flight => flight.Airline).Distinct().ToList();
        }
    }
}