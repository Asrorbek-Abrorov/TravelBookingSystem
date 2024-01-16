using Spectre.Console;
using TravelBookingSystem.services;

namespace TravelBookingSystem.Displays.other
{
    public class TravelPackageMenu
    {
        private readonly TravelPackageManager travelPackageManager;

        public TravelPackageMenu(TravelPackageManager travelPackageManager)
        {
            this.travelPackageManager = travelPackageManager;
        }

        public void Run()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(
                    new FigletText("* Travel Package Menu *")
                        .LeftJustified()
                        .Color(Color.Red1));
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[green]*** Travel Package Menu ***[/]?")
                        .PageSize(10)
                        .AddChoices(new[]
                        {
                            "View All Travel Packages",
                            "View Travel Package by ID",
                            "Add Travel Package",
                            "Update Travel Package",
                            "Remove Travel Package",
                            "Go Back"
                        }));

                switch (choice)
                {
                    case "View All Travel Packages":
                        ViewAllTravelPackages();
                        break;

                    case "View Travel Package by ID":
                        ViewTravelPackageById();
                        break;

                    case "Add Travel Package":
                        AddTravelPackage();
                        break;

                    case "Update Travel Package":
                        UpdateTravelPackage();
                        break;

                    case "Remove Travel Package":
                        RemoveTravelPackage();
                        break;

                    case "Go Back":
                        keepRunning = false;
                        break;
                }
            }
        }

        private void ViewAllTravelPackages()
        {
            var travelPackages = travelPackageManager.GetAllTravelPackagesAsync().Result;

            if (travelPackages.Count == 0)
            {
                AnsiConsole.WriteLine("No travel packages found.");
            }
            else
            {
                foreach (var travelPackage in travelPackages)
                {
                    AnsiConsole.WriteLine($"Travel Package ID: {travelPackage.Id}");
                    AnsiConsole.WriteLine($"Name: {travelPackage.Name}");
                    AnsiConsole.WriteLine($"Destination: {travelPackage.Destination}");
                    AnsiConsole.WriteLine($"Duration: {travelPackage.Duration} days");
                    AnsiConsole.WriteLine($"Price: {travelPackage.Price}");
                    AnsiConsole.WriteLine($"Available Spots: {travelPackage.AvailableSpots}");
                    AnsiConsole.WriteLine($"Itinerary: {travelPackage.Itinerary}");
                    AnsiConsole.WriteLine();
                }
            }

            AnsiConsole.WriteLine("Press Enter to continue...");
            Console.ReadKey();
        }

        private void ViewTravelPackageById()
        {
            int travelPackageId = AnsiConsole.Ask<int>("Enter the travel package ID:");

            var travelPackage = travelPackageManager.GetTravelPackageByIdAsync(travelPackageId).Result;

            if (travelPackage == null)
            {
                AnsiConsole.WriteLine($"Travel Package with ID {travelPackageId} not found.");
            }
            else
            {
                AnsiConsole.WriteLine($"Travel Package ID: [bold]{travelPackage.Id}[/]");
                AnsiConsole.WriteLine($"Name: {travelPackage.Name}");
                AnsiConsole.WriteLine($"Destination: {travelPackage.Destination}");
                AnsiConsole.WriteLine($"Duration: {travelPackage.Duration} days");
                AnsiConsole.WriteLine($"Price: {travelPackage.Price}");
                AnsiConsole.WriteLine($"Available Spots: {travelPackage.AvailableSpots}");
                AnsiConsole.WriteLine($"Itinerary: {travelPackage.Itinerary}");
            }

            AnsiConsole.WriteLine("Press Enter to continue...");
            Console.ReadKey();
        }

        private void AddTravelPackage()
        {
            string name = AnsiConsole.Ask<string>("Enter the travel package name:");
            string destination = AnsiConsole.Ask<string>("Enter the destination:");
            int duration = AnsiConsole.Ask<int>("Enter the duration in days:");
            decimal price = AnsiConsole.Ask<decimal>("Enter the price:");
            int availableSpots = AnsiConsole.Ask<int>("Enter the available spots:");
            string itinerary = AnsiConsole.Ask<string>("Enter the itinerary:");

            TravelPackage newTravelPackage = new TravelPackage
            {
                Name = name,
                Destination = destination,
                Duration = duration,
                Price = price,
                AvailableSpots = availableSpots,
                Itinerary = itinerary
            };

            travelPackageManager.AddTravelPackageAsync(newTravelPackage).Wait();

            AnsiConsole.WriteLine("Travel package added successfully!");
            AnsiConsole.WriteLine("Press Enter to continue...");
            Console.ReadKey();
        }

        private void UpdateTravelPackage()
        {
            int travelPackageId = AnsiConsole.Ask<int>("Enter the travel package ID:");

            var travelPackage = travelPackageManager.GetTravelPackageByIdAsync(travelPackageId).Result;

            if (travelPackage == null)
            {
                AnsiConsole.WriteLine($"Travel Package with ID {travelPackageId} not found.");
            }
            else
            {
                string newName = AnsiConsole.Ask<string>("Enter the new name:");
                string newDestination = AnsiConsole.Ask<string>("Enter the new destination:");
                int newDuration = AnsiConsole.Ask<int>("Enterthe new duration in days:");
                decimal newPrice = AnsiConsole.Ask<decimal>("Enter the new price:");
                int newAvailableSpots = AnsiConsole.Ask<int>("Enter the new available spots:");
                string newItinerary = AnsiConsole.Ask<string>("Enter the new itinerary:");

                travelPackage.Name = newName;
                travelPackage.Destination = newDestination;
                travelPackage.Duration = newDuration;
                travelPackage.Price = newPrice;
                travelPackage.AvailableSpots = newAvailableSpots;
                travelPackage.Itinerary = newItinerary;

                travelPackageManager.UpdateTravelPackageAsync(travelPackage).Wait();

                AnsiConsole.WriteLine("Travel package updated successfully!");
            }

            AnsiConsole.WriteLine("Press Enter to continue...");
            Console.ReadKey();
        }

        private void RemoveTravelPackage()
        {
            int travelPackageId = AnsiConsole.Ask<int>("Enter the travel package ID:");

            var travelPackage = travelPackageManager.GetTravelPackageByIdAsync(travelPackageId).Result;

            if (travelPackage == null)
            {
                AnsiConsole.WriteLine($"Travel Package with ID {travelPackageId} not found.");
            }
            else
            {
                travelPackageManager.RemoveTravelPackageAsync(travelPackageId).Wait();
                AnsiConsole.WriteLine("Travel package removed successfully!");
            }

            AnsiConsole.WriteLine("Press Enter to continue...");
            Console.ReadKey();
        }
    }
}