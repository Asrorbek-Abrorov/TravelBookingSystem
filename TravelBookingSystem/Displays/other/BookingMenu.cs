using Spectre.Console;
using TravelBookingSystem.services;

namespace TravelBookingSystem.Displays.other
{
    public class BookingMenu
    {
        private readonly BookingManager bookingManager;
        private readonly CustomerManager customerManager;

        public BookingMenu(BookingManager bookingManager, CustomerManager customerManager)
        {
            this.bookingManager = bookingManager;
            this.customerManager = customerManager;
        }

        public void Run()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(
                    new FigletText("* Booking Menu *")
                        .LeftJustified()
                        .Color(Color.Red1));
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[green]*** Booking Menu ***[/]?")
                        .PageSize(10)
                        .AddChoices(new[]
                        {
                            "View All Bookings",
                            "View Booking by ID",
                            "Add Booking",
                            "Update Booking",
                            "Remove Booking",
                            "Go Back"
                        }));

                switch (choice)
                {
                    case "View All Bookings":
                        ViewAllBookings();
                        break;

                    case "View Booking by ID":
                        ViewBookingById();
                        break;

                    case "Add Booking":
                        AddBooking();
                        break;

                    case "Update Booking":
                        UpdateBooking();
                        break;

                    case "Remove Booking":
                        RemoveBooking();
                        break;

                    case "Go Back":
                        keepRunning = false;
                        break;
                }
            }
        }

        private void ViewAllBookings()
        {
            var bookings = bookingManager.GetAllBookingsAsync().Result;

            if (bookings.Count == 0)
            {
                AnsiConsole.WriteLine("No bookings found.");
            }
            else
            {
                foreach (var booking in bookings)
                {
                    AnsiConsole.WriteLine($"Booking ID: {booking.Id}");
                    AnsiConsole.WriteLine($"Customer ID: {booking.CustomerId}");
                    AnsiConsole.WriteLine($"Date: {booking.Date}");
                    AnsiConsole.WriteLine($"Amount: {booking.Amount}");
                    AnsiConsole.WriteLine();
                }
            }

            AnsiConsole.WriteLine("Press Enter to continue...");
            Console.ReadKey();
        }

        private void ViewBookingById()
        {
            int bookingId = AnsiConsole.Ask<int>("Enter the booking ID:");

            var booking = bookingManager.GetBookingByIdAsync(bookingId).Result;

            if (booking == null)
            {
                AnsiConsole.WriteLine($"Booking with ID {bookingId} not found.");
            }
            else
            {
                var customer = customerManager.GetCustomerByIdAsync(booking.CustomerId).Result;

                AnsiConsole.WriteLine($"Booking ID: [bold]{booking.Id}[/]");
                AnsiConsole.WriteLine($"Customer ID: {booking.CustomerId}");

                if (customer != null)
                {
                    AnsiConsole.WriteLine($"Customer Name: {customer.Name}");
                    AnsiConsole.WriteLine($"Customer Contact Details: {customer.ContactDetails}");
                    AnsiConsole.WriteLine($"Customer Payment Information: {customer.PaymentInformation}");
                }

                AnsiConsole.WriteLine($"Date: {booking.Date}");
                AnsiConsole.WriteLine($"Amount: {booking.Amount}");
            }

            AnsiConsole.WriteLine("Press Enter to continue...");
            Console.ReadKey();
        }

        private void AddBooking()
        {
            int customerId = AnsiConsole.Ask<int>("Enter the customer ID:");

            var customer = customerManager.GetCustomerByIdAsync(customerId).Result;
            if (customer == null)
            {
                AnsiConsole.WriteLine($"Customer with ID {customerId} not found.");
                AnsiConsole.WriteLine("Press Enter to continue...");
                Console.ReadKey();
                return;
            }

            DateTime bookingDate = AnsiConsole.Ask<DateTime>("Enter the booking date:");
            decimal amount = AnsiConsole.Ask<decimal>("Enter the booking amount:");

            Booking newBooking = new Booking
            {
                CustomerId = customerId,
                Date = bookingDate,
                Amount = amount
            };

            bookingManager.AddBookingAsync(newBooking).Wait();

            AnsiConsole.WriteLine("Booking added successfully!");
            AnsiConsole.WriteLine("Press Enter to continue...");
            Console.ReadKey();
        }

        private void UpdateBooking()
        {
            int bookingId = AnsiConsole.Ask<int>("Enter the booking ID:");

            var booking = bookingManager.GetBookingByIdAsync(bookingId).Result;

            if (booking == null)
            {
                AnsiConsole.WriteLine($"Booking with ID {bookingId} not found.");
            }
            else
            {
                decimal newAmount = AnsiConsole.Ask<decimal>("Enter the new booking amount:");
                booking.Amount = newAmount;

                bookingManager.UpdateBookingAsync(booking).Wait();

                AnsiConsole.WriteLine("Booking updated successfully!");
            }

            AnsiConsole.WriteLine("Press Enter to continue...");
            Console.ReadKey();
        }

        private void RemoveBooking()
        {
            int bookingId = AnsiConsole.Ask<int>("Enter the booking ID:");

            var booking = bookingManager.GetBookingByIdAsync(bookingId).Result;

            if (booking == null)
            {
                AnsiConsole.WriteLine($"Booking with ID {bookingId} not found.");
            }
            else
            {
                bookingManager.RemoveBookingAsync(bookingId).Wait();

                AnsiConsole.WriteLine("Booking removed successfully!");
            }

            AnsiConsole.WriteLine("Press Enter to continue...");
            Console.ReadKey();
        }
    }
}