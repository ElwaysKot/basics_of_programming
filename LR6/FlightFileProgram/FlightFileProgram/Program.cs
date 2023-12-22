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
            Console.WriteLine("Выберите один из следующих вариантов:");
            Console.WriteLine("1.Показать все рейсы, выполняемые конкретной авиакомпанией");
            Console.WriteLine("2.Показать все рейсы, которые в настоящее время задерживаются");
            Console.WriteLine("3.Показать все рейсы, вылетающие в определенный день");
            Console.WriteLine("4.Показать все рейсы, которые отправляются и прибывают в указанный период времени");
            Console.WriteLine("5.Показать все рейсы, прибывшие за последний час или в указанный период времени");
            Console.WriteLine("6.Выйти из программы");

            int option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    var airlines = flightSystem.GetAirlines();
                    Console.WriteLine("Доступные авиакомпании:");
                    foreach (var airline1 in airlines)
                    {
                        Console.WriteLine(airline1);
                    }
                    Console.Write("Введите название авиакомпании: ");
                    string airline = Console.ReadLine();
                    var flightsByAirline = flightSystem.GetFlightsByAirline(airline);
                    if (flightsByAirline.Count == 0)
                    {
                        Console.WriteLine("Рейсов этой авиакомпании не найдено.");
                    }
                    else
                    {
                        Console.WriteLine("Рейсы по " + airline + ":");
                        foreach (var flight in flightsByAirline)
                        {
                            Console.WriteLine(flight.FlightNumber + " Время отправления: " + flight.DepartureTime);
                        }
                    }
                    break;
                case 2:
                    var delayedFlights = flightSystem.GetDelayedFlights();
                    if (delayedFlights.Count == 0)
                    {
                        Console.WriteLine("Задержанных рейсов не обнаружено.");
                    }
                    else
                    {
                        Console.WriteLine("Задержанные рейсы:");
                        foreach (var flight in delayedFlights)
                        {
                            Console.WriteLine(flight.FlightNumber + " Время задержки: " + flight.DepartureTime);
                        }
                    }
                    break;
                case 3:
                    Console.Write("Введите дату (format: year-month-day): ");
                    DateTime date = DateTime.Parse(Console.ReadLine());
                    var flightsOnDate = flightSystem.GetFlightsOnDate(date);
                    if (flightsOnDate.Count == 0)
                    {
                        Console.WriteLine("На эту дату рейсов не найдено.");
                    }
                    else
                    {
                        Console.WriteLine("Рейсы на " + date.ToString("yyyy-MM-dd") + ":");
                        foreach (var flight in flightsOnDate)
                        {
                            Console.WriteLine(flight.FlightNumber + " Время отправления: " + flight.DepartureTime);
                        }
                    }
                    break;
                case 4:
                    Console.Write("Введите время начала (format: year-month-day-hours:minutes:seconds): ");
                    DateTime startTime = DateTime.Parse(Console.ReadLine());
                    Console.Write("Введите время окончания (format: year-month-day-hours:minutes:seconds): ");
                    DateTime endTime = DateTime.Parse(Console.ReadLine());
                    var flightsInTimeRange = flightSystem.GetFlightsInTimeRange(startTime, endTime);
                    if (flightsInTimeRange.Count == 0)
                    {
                        Console.WriteLine("В этом временном диапазоне рейсов не найдено.");
                    }
                    else
                    {
                        Console.WriteLine("Рейсы с " + startTime.ToString("yyyy-MM-ddTHH:mm:ss") + " до " + endTime.ToString("yyyy-MM-ddTHH:mm:ss") + ":");
                        foreach (var flight in flightsInTimeRange)
                        {
                            Console.WriteLine(flight.FlightNumber + " Время отправления: " + flight.DepartureTime);
                        }
                    }
                    break;
                case 5:
                    Console.Write("Показать все рейсы, прибывшие за последний час или за указанный период времени (1/2)? ");
                    string useLastHour = Console.ReadLine();
                    List<Flight> flightsInLastHourOrTimeRange;
                    if (useLastHour.ToLower() == "1")
                    {
                        DateTime currentTime = DateTime.Now;
                        DateTime oneHourAgo = currentTime.AddHours(-1);
                        flightsInLastHourOrTimeRange = flightSystem.GetFlightsInTimeRange(oneHourAgo, currentTime);
                        if (flightsInLastHourOrTimeRange.Count == 0)
                        {
                            Console.WriteLine("За последний час ни одного рейса не прибыло.");
                        }
                        else
                        {
                            Console.WriteLine("Рейсы, прибывшие за последний час:");
                            foreach (var flight in flightsInLastHourOrTimeRange)
                            {
                                Console.WriteLine(flight.FlightNumber + " Время прибытия: " + flight.ArrivalTime);
                            }
                        }
                    }
                    else
                    {
                        Console.Write("Введите время начала (format: year-month-day-hours:minutes:seconds): ");
                        DateTime startRange = DateTime.Parse(Console.ReadLine());
                        Console.Write("Введите время окончания (format: year-month-day-hours:minutes:seconds): ");
                        DateTime endRange = DateTime.Parse(Console.ReadLine());
                        flightsInLastHourOrTimeRange = flightSystem.GetFlightsInTimeRange(startRange, endRange);
                        if (flightsInLastHourOrTimeRange.Count == 0)
                        {
                            Console.WriteLine("В этом временном диапазоне рейсов не найдено.");
                        }
                        else
                        {
                            Console.WriteLine("Рейсы с " + startRange.ToString("yyyy-MM-ddTHH:mm:ss") + " до " + endRange.ToString("yyyy-MM-ddTHH:mm:ss") + ":");
                            foreach (var flight in flightsInLastHourOrTimeRange)
                            {
                                Console.WriteLine(flight.FlightNumber + " Время прибытия: " + flight.ArrivalTime);
                            }
                        }
                    }
                    break;

                case 6:
                    return;
                default:
                    Console.WriteLine("Неизвестный вариант. Пожалуйста, попробуйте еще раз.");
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
                Console.WriteLine("Ошибка при чтении: " + ex.Message);
            }
            catch (JsonSerializationException ex)
            {
                Console.WriteLine("Ошибка при десериализации: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Неизвестная ошибка: " + ex.Message);
            }
        }
        public void ProcessRequest(Func<Flight, bool> query)
        {
            var result = flights.Where(query).ToList();
            Console.WriteLine($"Количество рейсов, соответствующих запросу: {result.Count}");
            string jsonResult = JsonConvert.SerializeObject(result);
            File.WriteAllText("queryResult.json", jsonResult);
            Console.WriteLine("Результат записан в 'queryResult.json'");
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
