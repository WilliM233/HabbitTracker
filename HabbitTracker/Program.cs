using HabbitTracker;
using Microsoft.Data.Sqlite;

string connectionString = @"Data Source=habit-Tracker.db";

CreateDatabase(connectionString);

// Display Main Menu and Process Input
DisplayMenu(connectionString);



// Menu Processing
void DisplayMenu(string connectionString)
{
    Console.WriteLine("MAIN MENU");
    Console.WriteLine();
    Console.WriteLine("What would you like to do?");
    Console.WriteLine();
    Console.WriteLine("Type 0 to Close Application.");
    Console.WriteLine("Type 1 to View All Records.");
    Console.WriteLine("Type 2 to Insert Record.");
    Console.WriteLine("Type 3 to Delete Record.");
    Console.WriteLine("Type 4 to Update Record.");
    Console.WriteLine("---------------------------------");
    Console.WriteLine();

    ProcessInput(connectionString);
}

void ProcessInput(string connectionString)
{
    string? input = Console.ReadLine();
    switch(input)
    {
        case "0":
            Environment.Exit(0);
            break;
        case "1":
            Console.Clear();
            ViewAllRecords(connectionString);
            break;
        case "2":
            Console.Clear();
            InsertRecord(connectionString);
            break;
        case "3":
            Console.Clear();
            DeleteRecord(connectionString);
            break;
        case "4":
            Console.Clear();
            UpdateRecord(connectionString);
            break;
        default:
            Console.WriteLine("Invalid Input, enter valid number");
            ProcessInput(connectionString);
            break;
    }
}

// Database Actions

void ViewAllRecords(string connectionString)
{
    List<DrinkingWater> drinkingWater = new List<DrinkingWater>();
    using (var connection = new SqliteConnection(connectionString))
    {
        connection.Open();
        var selectCmd = connection.CreateCommand();

        selectCmd.CommandText = @"SELECT * FROM drinking_water";
        SqliteDataReader reader = selectCmd.ExecuteReader();
        while (reader.Read())
        {
            drinkingWater.Add(new DrinkingWater
            {
                Id = Convert.ToInt32(reader["Id"]),
                Date = Convert.ToString(reader["Date"]),
                Quantity = Convert.ToString(reader["Quantity"])
            });
        }
    }

    Console.WriteLine("ID    DATE    QUANTITY");
    Console.WriteLine("----------------------");
    foreach(var drink in drinkingWater)
    {
        Console.WriteLine($"{drink.Id}    {drink.Date}    {drink.Quantity}");
    }

    Console.WriteLine("Press Any Key to Return to Menu");
    Console.ReadKey();
    Console.Clear();
    DisplayMenu(connectionString);
}
void CreateDatabase(string connectionString)
{
    using (var connection = new SqliteConnection(connectionString))
    {
        connection.Open();
        var tableCmd = connection.CreateCommand();

        tableCmd.CommandText = 
            @"CREATE TABLE IF NOT EXISTS drinking_water (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Date TEXT,
                Quantity INTEGER
                )";

        tableCmd.ExecuteNonQuery();

        connection.Close();
    }
    
}

void InsertRecord(string connectionString)
{
    string? date = CollectDate();
    string? quantity = CollectAmount();

    using (var connection = new SqliteConnection(connectionString))
    {
        connection.Open();

        var insertCmd = connection.CreateCommand();

        insertCmd.CommandText =
            $"INSERT INTO drinking_water (Date,Quantity) VALUES('{date}', {quantity})";

        insertCmd.ExecuteNonQuery();

        connection.Close();
    }

    Console.Clear();
    DisplayMenu(connectionString);
}

void DeleteRecord(string connectionString)
{
    Console.WriteLine("Enter record to delete: ");
    int recordId = Convert.ToInt32(Console.ReadLine());

    using (var connection = new SqliteConnection(connectionString))
    {
        connection.Open();

        var deleteCmd = connection.CreateCommand();

        deleteCmd.CommandText =
            $"DELETE FROM drinking_water WHERE Id = '{recordId}'";

        deleteCmd.ExecuteNonQuery();
        connection.Close();
    }

    Console.Clear();
    DisplayMenu(connectionString);
}

void UpdateRecord(string connectionString)
{
    Console.WriteLine("Enter record to update: ");
    int recordId = Convert.ToInt32(Console.ReadLine());

    string? date = CollectDate();
    string? quantity = CollectAmount();

    using (var connection = new SqliteConnection(connectionString))
    {
        connection.Open();

        var updateCmd = connection.CreateCommand();

        updateCmd.CommandText =
            $"UPDATE drinking_water " +
            $"SET Date = '{date}', Quantity = '{quantity}' " +
            $"WHERE Id = '{recordId}'";
        updateCmd.ExecuteNonQuery();
        connection.Close();
    }

    Console.Clear();
    DisplayMenu(connectionString);
}

string CollectDate()
{
    Console.WriteLine("Enter the date: ");
    return Console.ReadLine();
}

string CollectAmount()
{
    Console.WriteLine("Enter quantity of water: ");
    return Console.ReadLine();
}