using TravelBookingSystem.services;
using Spectre.Console;

namespace TravelBookingSystem.Displays.other
{
    public class PaymentMenu
    {
        private readonly PaymentManager paymentManager;
        private readonly BookingManager bookingManager;

        public PaymentMenu(PaymentManager paymentManager, BookingManager bookingManager)
        {
            this.paymentManager = paymentManager;
            this.bookingManager = bookingManager;
        }

        public void Run()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(
                    new FigletText("* Payment Menu *")
                        .LeftJustified()
                        .Color(Color.Red1));
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[green]*** Payment Menu ***[/]?")
                        .PageSize(10)
                        .AddChoices(new[]
                        {
                            "View All Payments",
                            "View Payment by ID",
                            "Add Payment",
                            "Update Payment",
                            "Remove Payment",
                            "Go Back"
                        }));

                switch (choice)
                {
                    case "View All Payments":
                        ViewAllPayments();
                        break;

                    case "View Payment by ID":
                        ViewPaymentById();
                        break;

                    case "Add Payment":
                        AddPayment();
                        break;

                    case "Update Payment":
                        UpdatePayment();
                        break;

                    case "Remove Payment":
                        RemovePayment();
                        break;

                    case "Go Back":
                        keepRunning = false;
                        break;
                }
            }
        }

        private void ViewAllPayments()
        {
            var payments = paymentManager.GetAllPaymentsAsync().Result;

            if (payments.Count == 0)
            {
                AnsiConsole.WriteLine("No payments found.");
            }
            else
            {
                foreach (var payment in payments)
                {
                    AnsiConsole.WriteLine($"Payment ID: {payment.Id}");
                    AnsiConsole.WriteLine($"Booking ID: {payment.BookingId}");
                    AnsiConsole.WriteLine($"Amount: {payment.Amount}");
                    AnsiConsole.WriteLine($"Date: {payment.Date}");
                    AnsiConsole.WriteLine();
                }
            }

            AnsiConsole.WriteLine("Press Enter to continue...");
            Console.ReadKey();
        }

        private void ViewPaymentById()
        {
            int paymentId = AnsiConsole.Ask<int>("Enter the payment ID:");

            var payment = paymentManager.GetPaymentByIdAsync(paymentId).Result;

            if (payment == null)
            {
                AnsiConsole.WriteLine($"Payment with ID {paymentId} not found.");
            }
            else
            {
                var booking = bookingManager.GetBookingByIdAsync(payment.BookingId).Result;

                AnsiConsole.WriteLine($"Payment ID: [bold]{payment.Id}[/]");
                AnsiConsole.WriteLine($"Booking ID: {payment.BookingId}");

                if (booking != null)
                {
                    AnsiConsole.WriteLine($"Customer ID: {booking.CustomerId}");
                    AnsiConsole.WriteLine($"Date: {booking.Date}");
                    AnsiConsole.WriteLine($"Amount: {booking.Amount}");
                }

                AnsiConsole.WriteLine($"Amount: {payment.Amount}");
                AnsiConsole.WriteLine($"Date: {payment.Date}");
            }

            AnsiConsole.WriteLine("Press Enter to continue...");
            Console.ReadKey();
        }

        private void AddPayment()
        {
            int bookingId = AnsiConsole.Ask<int>("Enter the booking ID:");

            var booking = bookingManager.GetBookingByIdAsync(bookingId).Result;
            if (booking == null)
            {
                AnsiConsole.WriteLine($"Booking with ID {bookingId} not found.");
                AnsiConsole.WriteLine("Press Enter to continue...");
                Console.ReadKey();
                return;
            }

            decimal amount = AnsiConsole.Ask<decimal>("Enter the amount:");
            DateTime date = AnsiConsole.Ask<DateTime>("Enter the date (yyyy-MM-dd):");

            Payment newPayment = new Payment
            {
                BookingId = bookingId,
                Amount = amount,
                Date = date
            };

            paymentManager.AddPaymentAsync(newPayment).Wait();

            AnsiConsole.WriteLine("Payment added successfully!");
            AnsiConsole.WriteLine("Press Enter to continue...");
            Console.ReadKey();
        }

        private void UpdatePayment()
        {
            int paymentId = AnsiConsole.Ask<int>("Enter the payment ID:");

            var payment = paymentManager.GetPaymentByIdAsync(paymentId).Result;

            if (payment == null)
            {
                AnsiConsole.WriteLine($"Payment with ID {paymentId} not found.");
            }
            else
            {
                decimal newAmount = AnsiConsole.Ask<decimal>("Enter the new amount:");
                DateTime newDate = AnsiConsole.Ask<DateTime>("Enter the new date (yyyy-MM-dd):");

                payment.Amount = newAmount;
                payment.Date = newDate;

                paymentManager.UpdatePaymentAsync(payment).Wait();

                AnsiConsole.WriteLine("Payment updated successfully!");
            }

            AnsiConsole.WriteLine("Press Enter to continue...");
            Console.ReadKey();
        }

        private void RemovePayment()
        {
            int paymentId = AnsiConsole.Ask<int>("Enter the payment ID:");

            var payment = paymentManager.GetPaymentByIdAsync(paymentId).Result;

            if (payment == null)
            {
                AnsiConsole.WriteLine($"Payment with ID {paymentId} not found.");
            }
            else
            {
                paymentManager.RemovePaymentAsync(paymentId).Wait();

                AnsiConsole.WriteLine("Payment removed successfully!");
            }

            AnsiConsole.WriteLine("Press Enter to continue...");
            Console.ReadKey();
        }
    }
}