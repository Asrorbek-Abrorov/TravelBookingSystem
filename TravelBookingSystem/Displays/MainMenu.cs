using Spectre.Console;
using TravelBookingSystem.Displays.other;
using TravelBookingSystem.services;

namespace TravelBookingSystem.Displays
{
    public class MainMenu
    {
        private readonly TravelPackageManager travelPackageManager;
        private readonly CustomerManager customerManager;
        private readonly BookingManager bookingManager;
        private readonly PaymentManager paymentManager;
        private readonly TravelPackageMenu travelPackageMenu;
        private readonly CustomerMenu customerMenu;
        private readonly BookingMenu bookingMenu;
        private readonly PaymentMenu paymentMenu;

        public MainMenu(
            TravelPackageManager travelPackageManager,
            CustomerManager customerManager,
            BookingManager bookingManager,
            PaymentManager paymentManager,
            TravelPackageMenu travelPackageMenu,
            CustomerMenu customerMenu,
            BookingMenu bookingMenu,
            PaymentMenu paymentMenu)
        {
            this.travelPackageManager = travelPackageManager;
            this.customerManager = customerManager;
            this.bookingManager = bookingManager;
            this.paymentManager = paymentManager;
            this.travelPackageMenu = travelPackageMenu;
            this.customerMenu = customerMenu;
            this.bookingMenu = bookingMenu;
            this.paymentMenu = paymentMenu;
        }

        public void Run()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(
                    new FigletText("*** Travel Booking System ***")
                        .LeftJustified()
                        .Color(Color.Red1));
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[green]*** Main Menu ***[/]?")
                        .PageSize(10)
                        .AddChoices(new[]
                        {
                            "Manage Travel Packages",
                            "Manage Customers",
                            "Manage Bookings",
                            "Manage Payments",
                            "Exit"
                        }));

                switch (choice)
                {
                    case "Manage Travel Packages":
                        travelPackageMenu.Run();
                        break;

                    case "Manage Customers":
                        customerMenu.Run();
                        break;

                    case "Manage Bookings":
                        bookingMenu.Run();
                        break;

                    case "Manage Payments":
                        paymentMenu.Run();
                        break;

                    case "Exit":
                        keepRunning = false;
                        break;
                }
            }
        }
    }
}