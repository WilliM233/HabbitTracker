// Display Main Menu and Process Input
DisplayMenu();
ProcessInput();



void DisplayMenu()
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
}

void ProcessInput()
{
    string? input = Console.ReadLine();
    switch(input)
    {
        case "0":
            Environment.Exit(0);
            break;
        case "1":
            break;
        case "2":
            break;
        case "3":
            break;
        case "4":
            break;
        default:
            Console.WriteLine("Invalid Input, enter valid number");
            ProcessInput();
            break;
    }
}