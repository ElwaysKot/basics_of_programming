
using System;
using System.Collections.Generic;

// Main Application Class
public class TableReservationApp
{
    static void Main(string[] args)
    {
        ReservationManager manager = new ReservationManager();
        manager.AddRestaurant("A", 10);
        manager.AddRestaurant("B", 5);

        Console.WriteLine(manager.BookTable("A", new DateTime(2023, 12, 25), 3)); // True
        Console.WriteLine(manager.BookTable("A", new DateTime(2023, 12, 25), 3)); // False
    }
}

// Reservation Manager Class
public class ReservationManager
{
    // res
    public List<Restaurant> restaurants;

    public ReservationManager()
    {
        restaurants = new List<Restaurant>();
    }

    // Add Restaurant Method
    public void AddRestaurant(string name, int tableCount)
    {
        try
        {
            Restaurant restaurant = new Restaurant();
            restaurant.Name = name;
            restaurant.Tables = new RestaurantTable[tableCount];
            for (int i = 0; i < tableCount; i++)
            {
                restaurant.Tables[i] = new RestaurantTable();
            }
            restaurants.Add(restaurant);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    // Load Restaurants From
    // File
    private void LoadRestaurantsFromFile(string filePrint)
    {
        try
        {
            string[] loads = File.ReadAllLines(filePrint);
            foreach (string load in loads)
            {
                var parts = load.Split(',');
                if (parts.Length == 2 && int.TryParse(parts[1], out int tableCount))
                {
                    AddRestaurant(parts[0], tableCount);
                }
                else
                {
                    Console.WriteLine(load);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    //Find All Free Tables
    public List<string> FindAllFreeTables(DateTime date)
    {
        try
        { 
            List<string> free = new List<string>();
            foreach (var restaurant in restaurants)
            {
                for (int i = 0; i < restaurant.Tables.Length; i++)
                {
                    if (!restaurant.Tables[i].IsBooked(date))
                    {
                        free.Add($"{restaurant.Name} - Table {i + 1}");
                    }
                }
            }
            return free;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            return new List<string>();
        }
    }

    public bool BookTable(string restaurantName, DateTime date, int tableNumber)
    {
        foreach (var restaurant in restaurants)
        {
            if (restaurant.Name == restaurantName)
            {
                if (tableNumber < 0 || tableNumber >= restaurant.Tables.Length)
                {
                    throw new Exception(null); //Invalid table number
                }

                return restaurant.Tables[tableNumber].Book(date);
            }
        }

        throw new Exception(null); //Restaurant not found
    }

    public void SortRestaurantsByAvailability(DateTime date)
    {
        try
        { 
            bool swapped;
            do
            {
                swapped = false;
                for (int i = 0; i < restaurants.Count - 1; i++)
                {
                    int availableTablesCurrent = AvailableTablesInRestaurantAndDateTime(restaurants[i], date); // available tables current
                    int availableTablesNext = AvailableTablesInRestaurantAndDateTime(restaurants[i + 1], date); // available tables next

                    if (availableTablesCurrent < availableTablesNext)
                    {
                        // Swap restaurants
                        var temp = restaurants[i];
                        restaurants[i] = restaurants[i + 1];
                        restaurants[i + 1] = temp;
                        swapped = true;
                    }
                }
            } while (swapped);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    // count available tables in a restaurant
    public int AvailableTablesInRestaurantAndDateTime(Restaurant restaurants, DateTime date)
    {
        try
        {
            int count = 0;
            foreach (var table in restaurants.Tables)
            {
                if (!table.IsBooked(date))
                {
                    count++;
                }
            }
            return count;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            return 0;
        }
    }
}

// Restaurant Class
public class Restaurant
{
    public string? Name { get; set; } //name
    public RestaurantTable[] Tables { get; set; } // tables
}

// Table Class
public class RestaurantTable
{
    private List<DateTime> bookedDates; //booked dates


    public RestaurantTable()
    {
        bookedDates = new List<DateTime>();
    }

    // book
    public bool Book(DateTime date)
    {
        try
        { 
            if (bookedDates.Contains(date))
            {
                return false;
            }
            //add to bd
            bookedDates.Add(date);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            return false;
        }
    }

    // is booked
    public bool IsBooked(DateTime date)
    {
        return bookedDates.Contains(date);
    }
}
